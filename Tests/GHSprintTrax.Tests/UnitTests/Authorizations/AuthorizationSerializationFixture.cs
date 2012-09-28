using System;
using System.Collections.Generic;
using GHSprintTrax.GithubApi;
using GHSprintTrax.GithubApi.RequestBodyTypes;
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
            var body = new CreateAuthorizationRequestBody
            {
                Note = "this is a note"
            };

            string serialized = JsonConvert.SerializeObject(body);

            Assert.Matches("^\\s*{\\s*\"note\"\\s*:\\s*\"this is a note\"\\s*}\\s*$", serialized);
        }

        [Fact]
        public void AuthorizationRequestBodyWithScopesSerializesThemAsArray()
        {
            var body = new CreateAuthorizationRequestBody
            {
                Scopes = new List<string> { "repo", "user", "gist"}
            };

            string serialized = JsonConvert.SerializeObject(body);

            Assert.Matches("\\{", "\"scopes\"", ":", "\\[", "\"repo\"", ",", "\"user\"", ",", "\"gist\"", "]", serialized);
        }

        [Fact]
        public void AuthorizationRequestBodyWithNoteUrlAndScopesSerializesAll()
        {
            var body = new CreateAuthorizationRequestBody
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

        [Fact]
        public void SampleAuthorizationDeserializesCorrectly()
        {
            const string sampleAuthorization = @"{
  ""id"": 1,
  ""url"": ""https://api.github.com/authorizations/1"",
  ""scopes"": [
    ""public_repo""
  ],
  ""token"": ""abc123"",
  ""app"": {
    ""url"": ""http://my-github-app.com"",
    ""name"": ""my github app""
  },
  ""note"": ""optional note"",
  ""note_url"": ""http://optional/note/url"",
  ""updated_at"": ""2011-09-06T20:39:23Z"",
  ""created_at"": ""2011-09-06T17:26:27Z""
}";

            var deserialized = JsonConvert.DeserializeObject<Authorization>(sampleAuthorization);

            Assert.Equal(1, deserialized.Id);
            Assert.Equal("https://api.github.com/authorizations/1", deserialized.Url);
            Assert.Equal(1, deserialized.Scopes.Count);
            Assert.Equal("public_repo", deserialized.Scopes[0]);
            Assert.Equal("abc123", deserialized.Token);
            Assert.Equal("http://my-github-app.com", deserialized.App.Url);
            Assert.Equal("my github app", deserialized.App.Name);
            Assert.Equal("optional note", deserialized.Note);
            Assert.Equal("http://optional/note/url", deserialized.NoteUrl);
            Assert.Equal(new DateTimeOffset(2011, 9, 6, 20, 39, 23, TimeSpan.Zero), deserialized.UpdatedAt);
            Assert.Equal(new DateTimeOffset(2011, 9, 6, 17, 26, 27, TimeSpan.Zero), deserialized.CreatedAt);
        }
    }
}
