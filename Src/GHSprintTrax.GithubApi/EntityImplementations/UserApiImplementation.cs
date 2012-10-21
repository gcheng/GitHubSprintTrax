using System.Net.Http;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    internal class UserApiImplementation : EntityImplementation, IUserAPI
    {
        public UserApiImplementation(HttpClient client, string rootUri)
            : base(client, rootUri)
        {
        }

        public User GetAuthenticatedUser()
        {
            HttpResponseMessage response = GetResponse("/user", HttpMethod.Get);
            return response.Content.ReadAsAsync<User>().Result;
        }
    }
}