using System.Collections.Generic;
using System.Net.Http;
using GHSprintTrax.GithubApi.EntityImplementations;
using GHSprintTrax.GithubApi.MessageHandlers;

namespace GHSprintTrax.GithubApi
{
    public class AuthorizationService : IAuthorizationAPI
    {
        private readonly AuthorizationApiImplementation impl;

        public AuthorizationService(string user, string password)
        {
            impl = new AuthorizationApiImplementation(new HttpClient(new BasicAuthHandler(user, password)), Constants.GithubUri);
        }

        public Authorization CreateAuthorization(string note = null, string noteUri = null, IEnumerable<string> scopes = null)
        {
            return impl.CreateAuthorization(note, noteUri, scopes);
        }
        
        public Authorization GetAuthorization(int authId)
        {
            return impl.GetAuthorization(authId);
        }

        public IEnumerable<Authorization> ListAuthorizations()
        {
            return impl.ListAuthorizations();
        }

        public void DeleteAuthorization(int id)
        {
            impl.DeleteAuthorization(id);
        }

        public void DeleteAuthorization(Authorization authorization)
        {
            impl.DeleteAuthorization(authorization);
        }
    }
}
