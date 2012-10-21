using System;
using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi.SerializationTypes
{
    internal class MilestoneData
    {
        public string Url { get; set; }
        public int Number { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public RelatedUserData Creator { get; set; }

        [JsonProperty("open_issues")]
        public int OpenIssues { get; set; }

        [JsonProperty("closed_issues")]
        public int ClosedIssues { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("due_on")]
        public DateTimeOffset? DueOn { get; set; }
    }
}