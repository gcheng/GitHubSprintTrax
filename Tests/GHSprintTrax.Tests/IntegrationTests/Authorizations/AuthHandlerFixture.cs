using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;
using GHSprintTrax.Tests.TestSupport;
using Xunit;
using Xunit.Extensions;

namespace GHSprintTrax.Tests.IntegrationTests.Authorizations
{
    public class AuthHandlerFixture : UserPasswordUsingFixture
    {
        [Fact]
        public void canCreateAnAuthorization()
        {
            var auth = new AuthorizationAPI(Username, Password);

            var newAuthorization = auth.CreateAuthorization(note: "testAuthorization",
                noteUri: "urn:example:testAuthorization");

            Assert.Equal("testAuthorization", newAuthorization.Note);
            Assert.True(newAuthorization.Id > 0);
        }
    }
}
