using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;
using GHSprintTrax.Tests.TestSupport;
using Xunit;

namespace GHSprintTrax.Tests.IntegrationTests
{
    public class MilestoneFixture
        : UserPasswordUsingFixture,
        IUseFixture<GetAuthorizationFixtureSetup>
    {
        private Authorization authorization;
        private GithubService github;
        private Repository repo;

        public void SetFixture(GetAuthorizationFixtureSetup data)
        {
            authorization = data.GetAuthorization(Username, Password);
            github = new GithubService(authorization);
            repo = github.GetRepository("WindowsAzure", "azure-sdk-tools");
        }

        [Fact]
        public void CanGetMilestones()
        {
            var milestones = repo.GetMilestones().ToList();
            Assert.Equal(6, milestones.Count);
        }

        [Fact]
        public void CanGetMilestoneDueDates()
        {
            var milestones = repo.GetMilestones().Where(m => m.DueOn != null).ToList();
            Assert.Equal(1, milestones.Count);
            Assert.Equal("0.6.11", milestones[0].Title);
        }
    }
}
