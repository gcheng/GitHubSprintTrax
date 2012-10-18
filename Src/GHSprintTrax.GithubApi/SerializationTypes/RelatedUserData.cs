using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi.SerializationTypes
{
    internal class RelatedUserData
    {
        public string Login { get; set; }
        public int Id { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("gravatar_id")]
        public string GravatarId { get; set; }

        public string Url { get; set; }
    }
}