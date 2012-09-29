using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void SetFixture(GetAuthorizationFixtureSetup data)
        {
            var auth = data.GetAuthorization(Username, Password);
            service = new GithubService(auth);
        }

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
