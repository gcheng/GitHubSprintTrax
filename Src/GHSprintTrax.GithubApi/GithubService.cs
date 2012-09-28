using System.Net.Http;
using GHSprintTrax.GithubApi.EntityImplementations;
using GHSprintTrax.GithubApi.MessageHandlers;

namespace GHSprintTrax.GithubApi
{
    public class GithubService
    {
        private readonly HttpClient client;

        public GithubService(Authorization authorization)
        {
            client = new HttpClient(new OAuth2Handler(authorization));
        }

        public IUserAPI Users
        {
            get { return new UserApiImplementation(client); }
        }

    }
}
