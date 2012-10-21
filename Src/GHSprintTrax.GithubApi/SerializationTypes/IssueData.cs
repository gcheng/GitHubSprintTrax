using System;
using System.Collections.Generic;

namespace GHSprintTrax.GithubApi.SerializationTypes
{
    internal class IssueData
    {
        public string Url { get; set; }

        public string html_url { get; set; }

        public int Number { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public RelatedUserData User { get; set; }

        public List<LabelData> Labels { get; set; }

        public RelatedUserData Assignee { get; set; }

        public MilestoneData Milestone { get; set; }

        public int Comments { get; set; }

        public DateTimeOffset? closed_at { get; set; }
        public DateTimeOffset created_at { get; set; }
        public DateTimeOffset updated_at { get; set; }

        #region Nested type: LabelData

        public class LabelData
        {
            public string Url { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }

        #endregion

        #region Nested type: PullRequestData

        public class PullRequestData
        {
            public string html_url { get; set; }
            public string diff_url { get; set; }
            public string patch_url { get; set; }
        }

        #endregion
    }
}