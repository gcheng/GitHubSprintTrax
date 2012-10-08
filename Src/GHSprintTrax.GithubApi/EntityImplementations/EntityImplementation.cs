using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

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

        private HttpRequestMessage CreateMessage(string uri, HttpMethod method)
        {
            var message = new HttpRequestMessage
            {
                Method = method, 
                RequestUri = new Uri(rootUri + uri)
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
    }
}
