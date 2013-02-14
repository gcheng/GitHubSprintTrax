using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi
{
    public class Label
    {
        private readonly IssueData.LabelData data;
        private readonly Repository repo;

        internal Label(IssueData.LabelData data, Repository repo)
        {
            this.data = data;
            this.repo = repo;
        }

        public string Url { get { return data.Url; } }
        public string Name { get { return data.Name; } }
        public string Color { get { return data.Color; } }
    }
}
