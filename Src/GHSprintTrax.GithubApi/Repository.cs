using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using GHSprintTrax.GithubApi.EntityImplementations;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi
{
    public class Repository : EntityImplementation
    {
        private readonly RepositoryData repoData;

        internal Repository(RepositoryData repoData, HttpClient client)
            : base(client, repoData.Url)
        {
            this.repoData = repoData;
        }

        #region get/set repo data

        public string Url { get { return repoData.Url; } }

        public string HtmlUrl { get { return repoData.HtmlUrl; } }

        public string CloneUrl { get { return repoData.CloneUrl; } }

        public string GitUrl { get { return repoData.GitUrl; } }

        public string SshUrl { get { return repoData.SshUrl; } }

        public string SvnUrl { get { return repoData.SvnUrl; } }

        public string MirrorUrl { get { return repoData.MirrorUrl; } }

        public int Id { get { return repoData.Id; } }

        public string OwnerLogin { get { return repoData.Owner.Login; } }

        public int OwnerId { get { return repoData.Owner.Id; } }

        public string OwnerAvatarUrl { get { return repoData.Owner.AvatarUrl; } }

        public string OwnerGravatarId { get { return repoData.Owner.GravatarId; } }

        public string OwnerUrl { get { return repoData.Owner.Url; } }

        public string Name { get { return repoData.Name; } }

        public string FullName { get { return repoData.FullName; } }

        public string Description { get { return repoData.Description; } }

        // TODO: Build out other properties as we get to them

        public bool HasIssues { get { return repoData.HasIssues; } }
        #endregion

        public IEnumerable<Milestone> GetMilestones()
        {
            var response = GetResponse("/milestones", HttpMethod.Get);
            return response.Content.ReadAsAsync<List<MilestoneData>>().Result
                .Select(md => new Milestone(md)).ToList();

        }
    }
}
