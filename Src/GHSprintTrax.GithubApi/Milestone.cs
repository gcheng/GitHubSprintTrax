using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi.SerializationTypes;

namespace GHSprintTrax.GithubApi
{
    public class Milestone
    {
        private readonly MilestoneData data;

        internal Milestone(MilestoneData data)
        {
            this.data = data;
        }

        #region milestone properties

        public string Url { get { return data.Url; } }

        public int Number { get { return data.Number; } }

        public string State { get { return data.State; } }

        public string Title { get { return data.Title; } }

        public string Description { get { return data.Description; } }

        public string CreatorLogin { get { return data.Creator.Login; } }
        public int CreatorId { get { return data.Creator.Id; } }
        public string CreatorUrl { get { return data.Creator.Url; } }

        public int OpenIssues { get { return data.OpenIssues; } }
        public int ClosedIssues { get { return data.ClosedIssues; } }
        public DateTimeOffset CreatedAt { get { return data.CreatedAt; } }
        public DateTimeOffset? DueOn { get { return data.DueOn; } }

        #endregion

    }
}
