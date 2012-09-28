using System.Net.Http;
using GHSprintTrax.GithubApi.EntityImplementations;
using GHSprintTrax.GithubApi.MessageHandlers;

namespace GHSprintTrax.GithubApi
{
    public class GithubService
    {
        private readonly HttpClient client;
        private readonly string rootUri;

        public GithubService(Authorization authorization, string rootUri = Constants.GithubUri)
        {
            client = new HttpClient(new OAuth2Handler(authorization));
            this.rootUri = rootUri;
        }

        public IAuthenticatedUserAPI CurrentUser
        {
            get { return new AuthenticatedUserApiImplementation(client, rootUri + "/user"); }
        }
        public IUserAPI Users
        {
            get { return new UserApiImplementation(client, rootUri + "/users"); }
        }
    }
}
