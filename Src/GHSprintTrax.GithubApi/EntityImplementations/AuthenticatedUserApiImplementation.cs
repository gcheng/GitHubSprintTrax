using System.Net.Http;

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
            return response.Content.ReadAsAsync<User>().Result;
        }
    }
}
