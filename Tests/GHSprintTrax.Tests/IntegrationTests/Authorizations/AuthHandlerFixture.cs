using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;
using GHSprintTrax.GithubApi.Authorizations;
using GHSprintTrax.Tests.TestSupport;
using Xunit;
using Xunit.Extensions;

namespace GHSprintTrax.Tests.IntegrationTests.Authorizations
{
    public class AuthHandlerFixture : UserPasswordUsingFixture, IDisposable
    {
        private readonly AuthorizationAPI authService;

        public AuthHandlerFixture()
        {
            authService = new AuthorizationAPI(Username, Password);
        }

        public void Dispose()
        {
            foreach (var authorization in authService.ListAuthorizations())
            {
                if (authorization.Note != null && authorization.Note.StartsWith("testAuthorization"))
                {
                    authService.DeleteAuthorization(authorization);
                }
            }
        }

        [Fact]
        public void CanCreateAnAuthorization()
        {
            var newAuthorization = authService.CreateAuthorization(note: "testAuthorization",
                noteUri: "urn:example:testAuthorization");

            Assert.Equal("testAuthorization", newAuthorization.Note);
            Assert.True(newAuthorization.Id > 0);
        }

        [Fact]
        public void CanCreateAuthorizationWithScope()
        {
            var newAuthorization = authService.CreateAuthorization(note: "testAuthorization2-with scope",
            scopes: new string[] { "repo", "gist" });

            Assert.Contains("repo", newAuthorization.Scopes);
            Assert.Contains("gist", newAuthorization.Scopes);

        }


        [Fact]
        public void CanRetrieveAnAuthorizationById()
        {
            const string expectedNote = "testAuthorization2-retrieval";
            var authToRetrieve = authService.CreateAuthorization(note: expectedNote);

            Authorization retrieved = authService.GetAuthorization(authToRetrieve.Id);

            Assert.Equal(authToRetrieve.Id, retrieved.Id);
            Assert.Equal(expectedNote, retrieved.Note);
        }



        [Fact]
        public void CanRetrieveAllAuthorizations()
        {
            string[] expectedNotes = new string[] { "testAuthorization3-retrieval", "testAuthorization4-retrieval" };
            foreach (var note in expectedNotes)
            {
                authService.CreateAuthorization(note: note);
            }

            List<Authorization> authorizations = authService.ListAuthorizations().ToList();

            Assert.True(authorizations.Count >= 2);

            List<string> notes = authorizations.Select(a => a.Note).ToList();
            Assert.Contains(expectedNotes[0], notes);
            Assert.Contains(expectedNotes[1], notes);
        }
    }
}
