using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi
{
    public class Issue
    {
        private IssueData data;
        private readonly Repository repo;

        internal Issue(IssueData data, Repository repo)
        {
            this.data = data;
            this.repo = repo;
        }

        #region get properties

        public string Url { get { return data.Url; } }
        public string HtmlUrl { get { return data.html_url; } }
        public int Number { get { return data.Number; } }
        public string State { get { return data.State; } }
        public string Title { get { return data.Title; } }
        public string Body { get { return data.Body; } }
        public string UserLogin { get { return data.User.Login; } }
        public string UserUrl { get { return data.User.Url; } }
        public List<string> LabelNames
        {
            get { return data.Labels.Select(ld => ld.Name).ToList(); }
        }

        public Milestone Milestone {get { return new Milestone(data.Milestone, repo); } }

        #endregion
    }
}
