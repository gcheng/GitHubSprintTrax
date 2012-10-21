using System.Collections.Generic;
using System.Linq;
using GHSprintTrax.GithubApi;
using GHSprintTrax.Tests.TestSupport;
using Xunit;

namespace GHSprintTrax.Tests.IntegrationTests.Authorizations
{
    public class AuthHandlerFixture : UserPasswordUsingFixture, IUseFixture<AuthorizationCleanup>
    {
        private readonly AuthorizationService authService;

        public AuthHandlerFixture()
        {
            authService = new AuthorizationService(Username, Password);
        }

        #region IUseFixture<AuthorizationCleanup> Members

        public void SetFixture(AuthorizationCleanup data)
        {
            data.Initialize(Username, Password, "test");
        }

        #endregion

        [Fact]
        public void CanCreateAnAuthorization()
        {
            Authorization newAuthorization = authService.CreateAuthorization("testAuthorization",
                "urn:example:testAuthorization");

            Assert.Equal("testAuthorization", newAuthorization.Note);
            Assert.True(newAuthorization.Id > 0);
        }

        [Fact]
        public void CanCreateAuthorizationWithScope()
        {
            Authorization newAuthorization = authService.CreateAuthorization("testAuthorization2-with scope",
                scopes: new[] {"repo", "gist"});

            Assert.Contains("repo", newAuthorization.Scopes);
            Assert.Contains("gist", newAuthorization.Scopes);
        }

        [Fact]
        public void CanRetrieveAnAuthorizationById()
        {
            const string expectedNote = "testAuthorization2-retrieval";
            Authorization authToRetrieve = authService.CreateAuthorization(expectedNote);

            Authorization retrieved = authService.GetAuthorization(authToRetrieve.Id);

            Assert.Equal(authToRetrieve.Id, retrieved.Id);
            Assert.Equal(expectedNote, retrieved.Note);
        }

        [Fact]
        public void CanRetrieveAllAuthorizations()
        {
            var expectedNotes = new[] {"testAuthorization3-retrieval", "testAuthorization4-retrieval"};
            foreach (string note in expectedNotes)
            {
                authService.CreateAuthorization(note);
            }

            List<Authorization> authorizations = authService.ListAuthorizations().ToList();

            Assert.True(authorizations.Count >= 2);

            List<string> notes = authorizations.Select(a => a.Note).ToList();
            Assert.Contains(expectedNotes[0], notes);
            Assert.Contains(expectedNotes[1], notes);
        }
    }
}