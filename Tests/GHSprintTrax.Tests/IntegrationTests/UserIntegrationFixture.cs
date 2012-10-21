using GHSprintTrax.GithubApi;
using GHSprintTrax.Tests.TestSupport;
using Xunit;

namespace GHSprintTrax.Tests.IntegrationTests
{
    public class UserIntegrationFixture : UserPasswordUsingFixture, IUseFixture<GetAuthorizationFixtureSetup>
    {
        private Authorization authorization;
        private GithubService github;

        #region IUseFixture<GetAuthorizationFixtureSetup> Members

        public void SetFixture(GetAuthorizationFixtureSetup data)
        {
            authorization = data.GetAuthorization(Username, Password);
            github = new GithubService(authorization);
        }

        #endregion

        [Fact]
        public void CanRetrieveAuthenticatedUser()
        {
            User currentUser = github.CurrentUser.GetInfo();

            Assert.Equal(Username, currentUser.Login);
        }
    }
}