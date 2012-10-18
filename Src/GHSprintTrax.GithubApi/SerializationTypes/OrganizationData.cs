using System;
using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi.SerializationTypes
{
    class OrganizationData : UserOrgData
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Blog { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }

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
    }
}
