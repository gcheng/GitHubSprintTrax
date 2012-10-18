using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi.SerializationTypes
{
    public class RepositoryData
    {
        public string Url { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("clone_url")]
        public string CloneUrl { get; set; }

        [JsonProperty("git_url")]
        public string GitUrl { get; set; }

        [JsonProperty("ssh_url")]
        public string SshUrl { get; set; }

        [JsonProperty("svn_url")]
        public string SvnUrl { get; set; }

        [JsonProperty("mirror_url")]
        public string MirrorUrl { get; set; }

        public int Id { get; set; }

        public class RepositoryOwnerData
        {
            public string Login { get; set; }

            public int Id { get; set; }

            [JsonProperty("avatar_url")]
            public string AvatarUrl { get; set; }

            [JsonProperty("gravatar_id")]
            public string GravatarId { get; set; }

            public string Url { get; set; }
        }

        public RepositoryOwnerData Owner { get; set; }

        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        public string Description { get; set; }

        // There's a ton of other properties here, but since Issues is what I'm really after,
        // that's what I'm going to jump to.

        [JsonProperty("has_issues")]
        public bool HasIssues { get; set; }
    }
}
