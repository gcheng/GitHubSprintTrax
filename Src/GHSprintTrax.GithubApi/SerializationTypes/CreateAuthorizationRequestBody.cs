using System.Collections.Generic;
using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi.SerializationTypes
{
    public class CreateAuthorizationRequestBody
    {
        [JsonProperty("note", NullValueHandling =  NullValueHandling.Ignore)]
        public string Note { get; set; }

        [JsonProperty("note_url", NullValueHandling = NullValueHandling.Ignore)]
        public string NoteUrl { get; set; }

        [JsonProperty("scopes", NullValueHandling =  NullValueHandling.Ignore)]
        public IList<string> Scopes { get; set; }
    }
}