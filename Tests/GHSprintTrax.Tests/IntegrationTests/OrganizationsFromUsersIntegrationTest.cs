using System.Collections.Generic;
using GHSprintTrax.GithubApi;
using GHSprintTrax.Tests.TestSupport;
using Xunit;

namespace GHSprintTrax.Tests.IntegrationTests
{
    public class OrganizationsFromUsersIntegrationTest
        : UserPasswordUsingFixture,
            IUseFixture<GetAuthorizationFixtureSetup>
    {
        private GithubService service;

        #region IUseFixture<GetAuthorizationFixtureSetup> Members

        public void SetFixture(GetAuthorizationFixtureSetup data)
        {
            Authorization auth = data.GetAuthorization(Username, Password);
            service = new GithubService(auth);
        }

        #endregion

        // This test is written assuming my personal github account, where
        // I'm a member of one organization. If you're not in an organization,
        // you'll need to ignore this test.
        [Fact]
        public void CanGetOrganizationsFromAuthenticatedUser()
        {
            var orgs = new List<UserOrganization>(service.CurrentUser.GetInfo().GetOrgs());

            Assert.Equal(1, orgs.Count);
            Assert.Equal("WindowsAzure", orgs[0].Login);
        }
    }
}