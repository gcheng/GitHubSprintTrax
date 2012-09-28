using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;
using GHSprintTrax.Tests.TestSupport;
using Xunit;

namespace GHSprintTrax.Tests.IntegrationTests
{
    public class UserIntegrationFixture : UserPasswordUsingFixture, IUseFixture<GetAuthorizationFixtureSetup>
    {
        private Authorization authorization;
        private GithubService github;

        public void SetFixture(GetAuthorizationFixtureSetup data)
        {
            authorization = data.GetAuthorization(Username, Password);
            github = new GithubService(authorization);
        }

        [Fact]
        public void CanRetrieveAuthenticatedUser()
        {
            User currentUser = github.Users.GetAuthenticatedUser();

            Assert.Equal(Username, currentUser.Login);
        }
    }
}
