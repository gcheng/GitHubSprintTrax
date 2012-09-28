using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using GHSprintTrax.GithubApi.Authorizations;
using GHSprintTrax.GithubApi.MessageHandlers;

namespace GHSprintTrax.GithubApi
{
    public class AuthorizationAPI
    {
        private readonly HttpClient client;

        public AuthorizationAPI(string user, string password)
        {
            client = new HttpClient(new BasicAuthHandler(user, password));
        }

        public Authorization CreateAuthorization(string note = null, string noteUri = null, IEnumerable<string> scopes = null)
        {
            var request = new CreateAuthorizationRequestBody {Note = note, NoteUrl = noteUri};
            if (scopes != null)
            {
                request.Scopes = new List<string>(scopes);
            }

            HttpResponseMessage response = GetResponse("/authorizations", HttpMethod.Post, request);
            return response.Content.ReadAsAsync<Authorization>().Result;
        }
        
        public Authorization GetAuthorization(int authId)
        {
            HttpResponseMessage response = GetResponse(string.Format("/authorizations/{0}", authId), HttpMethod.Get);
            return response.Content.ReadAsAsync<Authorization>().Result;
        }

        public IEnumerable<Authorization> ListAuthorizations()
        {
            HttpResponseMessage response = GetResponse("/authorizations", HttpMethod.Get);
            List<Authorization> results = response.Content.ReadAsAsync<List<Authorization>>().Result;
            return results;
        }

        public void DeleteAuthorization(int id)
        {
            GetResponse(string.Format("/authorizations/{0}", id), HttpMethod.Delete);
        }

        public void DeleteAuthorization(Authorization authorization)
        {
            DeleteAuthorization(authorization.Id);
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

        private HttpResponseMessage GetResponse<TContent>(string uri, HttpMethod method, TContent content)
        {
            HttpRequestMessage message = CreateMessage(uri, method);
            message.Content = new ObjectContent<TContent>(content, new JsonMediaTypeFormatter(),
                Constants.apiMimeType);
            HttpResponseMessage response = client.SendAsync(message).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }

        private HttpResponseMessage GetResponse(string uri, HttpMethod method)
        {
            HttpRequestMessage message = CreateMessage(uri, method);
            HttpResponseMessage response = client.SendAsync(message).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
