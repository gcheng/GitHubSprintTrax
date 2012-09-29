using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    class AuthenticatedUserApiImplementation : EntityImplementation, IAuthenticatedUserAPI
    {
        public AuthenticatedUserApiImplementation(HttpClient client, string rootUri)
            : base(client, rootUri)
        {
        }

        public User GetInfo()
        {
            var response = GetResponse("", HttpMethod.Get);
            return new User(response.Content.ReadAsAsync<UserData>().Result, Client);
        }
    }
}
