using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;
using GHSprintTrax.Tests.TestSupport;
using Xunit;

namespace GHSprintTrax.Tests.IntegrationTests
{
    public class OrganizationsIntegrationTest
        : UserPasswordUsingFixture,
        IUseFixture<GetAuthorizationFixtureSetup>
    {
        private GithubService service;

        public void SetFixture(GetAuthorizationFixtureSetup data)
        {
            service = new GithubService(data.GetAuthorization(Username, Password));
        }

        [Fact]
        public void CanGetOrganizationByName()
        {
            var org = service.GetOrganization("WindowsAzure");

            Assert.Equal("WindowsAzure", org.Login);
        }
    }
}
