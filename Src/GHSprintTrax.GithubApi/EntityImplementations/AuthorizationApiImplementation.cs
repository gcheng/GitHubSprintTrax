using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    internal class AuthorizationApiImplementation : EntityImplementation, IAuthorizationAPI
    {
        public AuthorizationApiImplementation(HttpClient client, string rootUri)
            : base(client, rootUri)
        {
        }

        #region IAuthorizationAPI Members

        public Authorization CreateAuthorization(string note, string noteUri, IEnumerable<string> scopes)
        {
            var request = new CreateAuthorizationRequestBody {Note = note, NoteUrl = noteUri};
            if (scopes != null)
            {
                request.Scopes = new List<string>(scopes);
            }

            HttpResponseMessage response = GetResponse("/authorizations", HttpMethod.Post, request);
            AuthorizationData authData = response.Content.ReadAsAsync<AuthorizationData>().Result;
            return new Authorization(authData);
        }

        public Authorization GetAuthorization(int authId)
        {
            HttpResponseMessage response = GetResponse(string.Format("/authorizations/{0}", authId), HttpMethod.Get);
            return new Authorization(response.Content.ReadAsAsync<AuthorizationData>().Result);
        }

        public IEnumerable<Authorization> ListAuthorizations()
        {
            return GetPagedList<Authorization, AuthorizationData, GetListOptions>(
                "/authorizations", null, ad => new Authorization(ad));
        }

        public void DeleteAuthorization(int id)
        {
            GetResponse(string.Format("/authorizations/{0}", id), HttpMethod.Delete);
        }

        public void DeleteAuthorization(Authorization authorization)
        {
            DeleteAuthorization(authorization.Id);
        }

        #endregion
    }
}