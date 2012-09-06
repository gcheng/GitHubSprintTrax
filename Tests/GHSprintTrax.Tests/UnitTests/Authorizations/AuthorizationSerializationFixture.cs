using System.Collections.Generic;
using GHSprintTrax.GithubApi.Authorization;
using GHSprintTrax.Tests.TestSupport;
using Newtonsoft.Json;
using Xunit;
using Xunit.Extensions;

namespace GHSprintTrax.Tests.UnitTests.Authorizations
{
    /// <summary>
    /// Tests around the serialization of various data structures
    /// used in the Github Authorization API
    /// </summary>
    public class AuthorizationSerializationFixture : TestClass
    {
        [Fact]
        public void DefaultCreateAuthorizationRequestBodySerializesAsEmptyJsonObject()
        {
            var body = new CreateAuthorizationRequestBody();
            string serialized = JsonConvert.SerializeObject(body);

            Assert.Matches("^\\s*{\\s*}\\s*$", serialized);
        }

        [Fact]
        public void AuthorizationRequestBodyWithNoteSerializesNoteAsProperty()
        {
            var body = new CreateAuthorizationRequestBody()
            {
                Note = "this is a note"
            };

            string serialized = JsonConvert.SerializeObject(body);

            Assert.Matches("^\\s*{\\s*\"note\"\\s*:\\s*\"this is a note\"\\s*}\\s*$", serialized);
        }

        [Fact]
        public void AuthorizationRequestBodyWithScopesSerializesThemAsArray()
        {
            var body = new CreateAuthorizationRequestBody()
            {
                Scopes = new List<string>() { "repo", "user", "gist"}
            };

            string serialized = JsonConvert.SerializeObject(body);

            Assert.Matches("\\{", "\"scopes\"", ":", "\\[", "\"repo\"", ",", "\"user\"", ",", "\"gist\"", "]", serialized);
        }

        [Fact]
        public void AuthorizationRequestBodyWithNoteUrlAndScopesSerializesAll()
        {
            var body = new CreateAuthorizationRequestBody()
            {
                Note = "this is a note",
                NoteUrl = "http://some/app/url",
                Scopes = new List<string> {"repo", "gist"}
            };

            string serialized = JsonConvert.SerializeObject(body);

            Assert.Matches("\"note\"", ":", "\"" + body.Note + "\"", serialized);
            Assert.Matches("\"note_url\"", ":", "\"" + body.NoteUrl + "\"", serialized);
            Assert.Matches("\"scopes\"", ":", "\\[", "\"repo\"", ",", "\"gist\"", "]", serialized);
        }
    }
}
