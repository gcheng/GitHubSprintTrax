using System;
using System.Collections.Generic;
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

            var message = CreateMessage("/authorizations", HttpMethod.Post);
            message.Content = new ObjectContent<CreateAuthorizationRequestBody>(request, new JsonMediaTypeFormatter(),
                Constants.apiMimeType);

            HttpResponseMessage response = client.SendAsync(message).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<Authorization>().Result;
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
    }
}
