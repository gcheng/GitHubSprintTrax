using System;
using System.Collections.Generic;
using GHSprintTrax.GithubApi.SerializationTypes;
using Newtonsoft.Json;

namespace GHSprintTrax.GithubApi
{
    /// <summary>
    /// This class stores the data for an authorization
    /// as returned by the Github API.
    /// </summary>
    public class Authorization
    {
        private readonly AuthorizationData authData;
        private readonly List<string> scopes;

        internal Authorization(AuthorizationData authData)
        {
            this.authData = authData;
            scopes = new List<string>(authData.Scopes);
        }

        public int Id { get { return authData.Id; } }
        public string Url { get { return authData.Url; } }

        public IList<string> Scopes
        {
            get { return scopes; }
        }

        public string Token { get { return authData.Token; } }
        
        public string AppName { get { return authData.App.Name; } }

        public string AppUrl { get { return authData.App.Url; } }

        public string Note { get { return authData.Note; } }

        public string NoteUrl { get { return authData.NoteUrl; } }

        public DateTimeOffset UpdatedAt { get { return authData.UpdatedAt; } }

        public DateTimeOffset CreatedAt { get { return authData.CreatedAt; } }
    }
}
