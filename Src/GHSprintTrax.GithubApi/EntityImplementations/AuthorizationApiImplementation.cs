using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using GHSprintTrax.GithubApi.RequestBodyTypes;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    class AuthorizationApiImplementation : EntityImplementation, IAuthorizationAPI
    {
        public AuthorizationApiImplementation(HttpClient client, string rootUri)
            : base(client, rootUri)
        {
        }

        public Authorization CreateAuthorization(string note, string noteUri, IEnumerable<string> scopes)
        {
            var request = new CreateAuthorizationRequestBody { Note = note, NoteUrl = noteUri };
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
    }
}
