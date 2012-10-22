using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    public abstract class EntityImplementation
    {
        private readonly HttpClient client;
        private readonly string rootUri;

        protected EntityImplementation(HttpClient client, string rootUri)
        {
            this.client = client;
            this.rootUri = rootUri;
        }

        protected HttpClient Client
        {
            get { return client; }
        }

        protected string RootUri
        {
            get { return rootUri; }
        }

        private HttpRequestMessage CreateMessage(string uri, HttpMethod method,
            NameValueCollection queryParameters = null)
        {
            var requestUri = new UriBuilder(rootUri + uri);
            if (queryParameters != null)
            {
                requestUri.Query = String.Join("&", queryParameters.AllKeys
                    .SelectMany(key => queryParameters.GetValues(key)
                        .Select(
                            value =>
                                String.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))).
                    ToArray());
            }

            var message = new HttpRequestMessage
            {
                Method = method,
                RequestUri = requestUri.Uri
            };

            message.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(Constants.apiMimeType));
            return message;
        }

        protected HttpResponseMessage GetResponse<TContent>(string uri, HttpMethod method, TContent content)
        {
            HttpRequestMessage message = CreateMessage(uri, method);
            message.Content = new ObjectContent<TContent>(content, new JsonMediaTypeFormatter(),
                Constants.apiMimeType);
            HttpResponseMessage response = client.SendAsync(message).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }

        protected HttpResponseMessage GetResponse(string uri, HttpMethod method)
        {
            HttpRequestMessage message = CreateMessage(uri, method);
            HttpResponseMessage response = client.SendAsync(message).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }

        protected HttpResponseMessage GetResponse(string uri, NameValueCollection queryParameters)
        {
            HttpRequestMessage message = CreateMessage(uri, HttpMethod.Get, queryParameters);
            HttpResponseMessage response = client.SendAsync(message).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }

        protected IEnumerable<TPublic> GetPagedList<TPublic, TSerialization, TOptions>(string initialUri,
            Action<TOptions> optionSetter, Func<TSerialization, TPublic> selector)
            where TOptions : GetListOptions, new()
        {
            var options = new TOptions();
            if (optionSetter != null)
            {
                optionSetter(options);
            }

            HttpResponseMessage response = GetResponse(initialUri, options.GetParameters());

            while (response != null)
            {
                IEnumerable<TPublic> results = 
                    response.Content.ReadAsAsync<List<TSerialization>>().Result
                    .Select(selector);

                foreach (var result in results)
                {
                    yield return result;
                }

                if (response.Headers.Contains("Link"))
                {
                    string requestUrl = ParseLinkHeader(response.Headers.GetValues("Link"));
                    if (requestUrl == null)
                    {
                        yield break;
                    }

                    var message = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                    message.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(Constants.apiMimeType));

                    response = Client.SendAsync(message).Result;
                    response.EnsureSuccessStatusCode();
                }
                else
                {
                    response = null;
                }
            }
        }

        private static readonly Regex relNextRegex = new Regex(@"\<(?<link>.*)\>; rel=""next""", RegexOptions.Compiled);

        private static string ParseLinkHeader(IEnumerable<string> headers)
        {
            // Find first one that contains rel="next", that's the one we care about
            foreach (string header in headers)
            {
                Match match = relNextRegex.Match(header);
                if (match.Success)
                {
                    return match.Groups["link"].Value;
                }
            }
            return null;
        }
    }
}