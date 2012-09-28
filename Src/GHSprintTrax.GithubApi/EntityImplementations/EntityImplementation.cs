using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    public class EntityImplementation
    {
        protected HttpClient client;

        public EntityImplementation(HttpClient client)
        {
            this.client = client;
        }

        private static HttpRequestMessage CreateMessage(string uri, HttpMethod method)
        {
            var message = new HttpRequestMessage
            {
                Method = method, 
                RequestUri = new Uri(Constants.GithubUri + uri)
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