using System;
using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi.SerializationTypes
{
    internal class UserData
    {
        public string Login { get; set; }

        public int Id { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("gravatar_id")]
        public string GravatarId { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Blog { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public bool Hireable { get; set; }

        public string Bio { get; set; }

        [JsonProperty("public_repos")]
        public int PublicRepos { get; set; }

        [JsonProperty("public_gists")]
        public int PublicGists { get; set; }

        public int Followers { get; set; }

        public int Following { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        public string Type { get; set; }

        [JsonProperty("total_private_repos")]
        public int TotalPrivateRepos { get; set; }

        [JsonProperty("owned_private_repos")]
        public int OwnedPrivateRepos { get; set; }

        [JsonProperty("private_gists")]
        public int PrivateGists { get; set; }

        [JsonProperty("disk_usage")]
        public int DiskUsage { get; set; }

        public int Collaborators { get; set; }

        public PlanInfo Plan { get; set; }

        #region Nested type: PlanInfo

        public class PlanInfo
        {
            public string Name { get; set; }
            public int Space { get; set; }
            public int Collaborators { get; set; }

            [JsonProperty("private_repos")]
            public int PrivateRepos { get; set; }
        }

        #endregion
    }
}