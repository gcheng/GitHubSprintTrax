using System.Collections.Generic;
using System.Linq;
using GHSprintTrax.GithubApi;
using GHSprintTrax.Tests.TestSupport;
using Xunit;

namespace GHSprintTrax.Tests.IntegrationTests
{
    public class RepositoryIntegrationTest
        : UserPasswordUsingFixture,
            IUseFixture<GetAuthorizationFixtureSetup>
    {
        private const string repoName = "GHSprintTrax";
        private const string ownerName = "christav";

        private Authorization authorization;
        private GithubService github;
        private Repository repo;

        #region IUseFixture<GetAuthorizationFixtureSetup> Members

        public void SetFixture(GetAuthorizationFixtureSetup data)
        {
            authorization = data.GetAuthorization(Username, Password);
            github = new GithubService(authorization);
            repo = github.GetRepository(ownerName, repoName);
        }

        #endregion

        [Fact]
        public void CanRetrieveRepositoryByName()
        {
            Assert.NotEqual(0, repo.Id);
            Assert.Equal(repoName, repo.Name);
            Assert.True(repo.HasIssues);
        }

        [Fact]
        public void CanGetMilestonesFromRepository()
        {
            List<Milestone> milestones = repo.GetMilestones().ToList();

            Assert.True(milestones.Count >= 2);
            Assert.True(milestones.Select(m => m.Title).Contains("Milestone 1"));
            Assert.True(milestones.Select(m => m.Title).Contains("Milestone 2"));
        }

        [Fact]
        public void CanGetIssuesForMilestone()
        {
            Milestone milestone1 = repo.GetMilestones().First(m => m.Title == "Milestone 1");

            List<Issue> issues = milestone1.GetIssues().ToList();

            Assert.True(issues.Count >= 1);

            Issue expectedIssue = issues.First(i => i.Title == "Create authorization should default to async");

            Assert.Equal(ownerName, expectedIssue.UserLogin);
            Assert.True(expectedIssue.LabelNames.Contains("enhancement"));
        }
    }
}