using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    public abstract class EntityImplementation
    {
        private readonly string rootUri;
        private readonly HttpClient client;

        protected EntityImplementation(HttpClient client, string rootUri)
        {
            this.client = client;
            this.rootUri = rootUri;
        }

        protected HttpClient Client { get { return client; } }

        protected string RootUri { get { return rootUri; } }

        private HttpRequestMessage CreateMessage(string uri, HttpMethod method, NameValueCollection queryParameters = null)
        {
            var requestUri = new UriBuilder(rootUri + uri);
            if (queryParameters != null)
            {
                requestUri.Query = String.Join("&", queryParameters.AllKeys
                    .SelectMany(key => queryParameters.GetValues(key)
                        .Select(
                            value =>
                                String.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))).ToArray());
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
    }
}
