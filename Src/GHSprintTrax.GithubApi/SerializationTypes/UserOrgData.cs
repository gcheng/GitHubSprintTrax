using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi.SerializationTypes
{
    class UserOrgData
    {
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        public string Login { get; set; }

        public int Id { get; set; }

        public string Url { get; set; }
    }
}
