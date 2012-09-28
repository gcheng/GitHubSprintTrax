using System.Net.Http;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    class UserApiImplementation : EntityImplementation, IUserAPI
    {
        public UserApiImplementation(HttpClient client)
            : base(client)
        {
        }

        public User GetAuthenticatedUser()
        {
            var response = GetResponse("/user", HttpMethod.Get);
            return response.Content.ReadAsAsync<User>().Result;
        }
    }
}
