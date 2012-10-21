using System.Net.Http;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    internal class AuthenticatedUserApiImplementation : EntityImplementation, IAuthenticatedUserAPI
    {
        public AuthenticatedUserApiImplementation(HttpClient client, string rootUri)
            : base(client, rootUri)
        {
        }

        #region IAuthenticatedUserAPI Members

        public User GetInfo()
        {
            HttpResponseMessage response = GetResponse("", HttpMethod.Get);
            return new User(response.Content.ReadAsAsync<UserData>().Result, Client);
        }

        #endregion
    }
}