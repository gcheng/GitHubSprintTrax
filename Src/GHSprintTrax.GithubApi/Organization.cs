using System;
using System.Net.Http;
using GHSprintTrax.GithubApi.EntityImplementations;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi
{
    public class Organization : EntityImplementation
    {
        private readonly OrganizationData orgData;

        public Organization(OrganizationData orgData, HttpClient client) 
            : base(client, orgData.Url)
        {
            this.orgData = orgData;
        }

        #region data get / set

        public int Id { get { return orgData.Id; } }
        public string Login { get { return orgData.Login; } }
        public string Url { get { return orgData.Url; } }
        public string AvatarUrl { get { return orgData.AvatarUrl; } }
        public string Name { get { return orgData.Name; } }
        public string Company { get { return orgData.Company; } }
        public string Blog { get { return orgData.Blog; } }
        public string Location { get { return orgData.Location; } }
        public string Email { get { return orgData.Email; } }
        public int PublicRepos { get { return orgData.PublicRepos; } }
        public int PublicGists { get { return orgData.PublicGists; } }
        public int Followers { get { return orgData.Followers; } }
        public int Following { get { return orgData.Following; } }
        public string HtmlUrl { get { return orgData.HtmlUrl; } }
        public DateTimeOffset CreatedAt { get { return orgData.CreatedAt; } }
        public string Type { get { return orgData.Type; } }
        #endregion
    }
}
