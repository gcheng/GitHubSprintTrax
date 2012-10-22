using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GHSprintTrax.GithubApi.EntityImplementations;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi
{
    /// <summary>
    /// Information returned by the Github user APIs
    /// </summary>
    public class User : EntityImplementation
    {
        private readonly UserData userData;

        internal User(UserData userData, HttpClient client)
            : base(client, userData.Url)
        {
            this.userData = userData;
        }

        #region Data get/set

        public string Login
        {
            get { return userData.Login; }
        }

        public int Id
        {
            get { return userData.Id; }
        }

        public string AvatarUrl
        {
            get { return userData.AvatarUrl; }
        }

        public string GravatarId
        {
            get { return userData.GravatarId; }
        }

        public string Url
        {
            get { return userData.Url; }
        }

        public string Name
        {
            get { return userData.Name; }
        }

        public string Company
        {
            get { return userData.Company; }
        }

        public string Blog
        {
            get { return userData.Blog; }
        }

        public string Location
        {
            get { return userData.Location; }
        }

        public string Email
        {
            get { return userData.Email; }
        }

        public bool Hireable
        {
            get { return userData.Hireable; }
        }

        public string Bio
        {
            get { return userData.Bio; }
        }

        public int PublicRepos
        {
            get { return userData.PublicRepos; }
        }

        public int PublicGists
        {
            get { return userData.PublicGists; }
        }

        public int Followers
        {
            get { return userData.Followers; }
        }

        public int Following
        {
            get { return userData.Following; }
        }

        public string HtmlUrl
        {
            get { return userData.HtmlUrl; }
        }

        public DateTimeOffset CreatedAt
        {
            get { return userData.CreatedAt; }
        }

        public string Type
        {
            get { return userData.Type; }
        }

        public int TotalPrivateRepos
        {
            get { return userData.TotalPrivateRepos; }
        }

        public int OwnedPrivateRepos
        {
            get { return userData.OwnedPrivateRepos; }
        }

        public int PrivateGists
        {
            get { return userData.PrivateGists; }
        }

        public int DiskUsage
        {
            get { return userData.DiskUsage; }
        }

        public int Collaborators
        {
            get { return userData.Collaborators; }
        }

        public string PlanName
        {
            get { return userData.Plan.Name; }
        }

        public int PlanSpace
        {
            get { return userData.Plan.Space; }
        }

        public int PlanCollaborators
        {
            get { return userData.Plan.Collaborators; }
        }

        public int PlanPrivateRepos
        {
            get { return userData.Plan.PrivateRepos; }
        }

        #endregion

        public IEnumerable<UserOrganization> GetOrgs()
        {
            return GetPagedList<UserOrganization, UserOrgData, GetListOptions>(
                "/orgs", null, od => new UserOrganization(od, Client));
        }
    }
}