using System;
using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi.SerializationTypes
{
    class MilestoneData
    {
        public string Url { get; set; }
        public int Number { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public class MilestoneCreator
        {
            public string Login { get; set; }
            public int Id { get; set; }
            [JsonProperty("avatar_url")]
            public string AvatarUrl { get; set; }
            [JsonProperty("gravatar_id")]
            public string GravatarId { get; set; }

            public string Url { get; set; }
        }

        public MilestoneCreator Creator { get; set; }
        
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
