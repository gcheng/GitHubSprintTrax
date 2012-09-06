using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi.Authorization
{
    /// <summary>
    /// This class stores the data for an authorization
    /// as returned by the Github API.
    /// </summary>
    public class Authorization
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public IList<string> Scopes
        {
            get;
            set;
        }

        public string Token { get; set; }
        public AppInfo App { get; set; }
        public string Note { get; set; }

        [JsonProperty("note_url")]
        public string NoteUrl { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        public class AppInfo
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }
    }
}
