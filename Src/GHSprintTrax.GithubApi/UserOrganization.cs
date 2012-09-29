using System.Net.Http;
using GHSprintTrax.GithubApi.EntityImplementations;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi
{
    public class UserOrganization : EntityImplementation
    {
        private readonly UserOrgData orgData;

        public UserOrganization(UserOrgData orgData, HttpClient client)
            : base(client, orgData.Url)
        {
            this.orgData = orgData;
        }

        public string AvatarUrl { get { return orgData.AvatarUrl; } }
        public string Login { get { return orgData.Login; } }
        public int Id { get { return orgData.Id; } }
        public string Url { get { return orgData.Url; } }
    }
}
