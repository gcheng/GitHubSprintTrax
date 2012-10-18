using System.Net.Http;
using GHSprintTrax.GithubApi.EntityImplementations;
using GHSprintTrax.GithubApi.MessageHandlers;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi
{
    public class GithubService : EntityImplementation
    {
        public GithubService(Authorization authorization, string rootUri = Constants.GithubUri)
            : base(new HttpClient(new OAuth2Handler(authorization)), rootUri)
        {
        }

        public IAuthenticatedUserAPI CurrentUser
        {
            get { return new AuthenticatedUserApiImplementation(Client, RootUri + "/user"); }
        }

        public Organization GetOrganization(string orgLogin)
        {
            string uri = string.Format("/orgs/{0}", orgLogin);
            var response = GetResponse(uri, HttpMethod.Get);
            return new Organization(response.Content.ReadAsAsync<OrganizationData>().Result, Client);
        }

        public IUserAPI Users
        {
            get { return new UserApiImplementation(Client, RootUri + "/users"); }
        }

        public Repository GetRepository(string ownerLogin, string repoName)
        {
            string uri = string.Format("/repos/{0}/{1}", ownerLogin, repoName);
            var response = GetResponse(uri, HttpMethod.Get);
            return new Repository(response.Content.ReadAsAsync<RepositoryData>().Result, Client);
        }
    }
}
