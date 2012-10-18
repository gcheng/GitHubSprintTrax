﻿using System.Collections.Generic;
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
        const string repoName = "GHSprintTrax";
        const string ownerName = "christav";
        
        private Authorization authorization;
        private GithubService github;
        private Repository repo;


        public void SetFixture(GetAuthorizationFixtureSetup data)
        {
            authorization = data.GetAuthorization(Username, Password);
            github = new GithubService(authorization);
            repo = github.GetRepository(ownerName, repoName);
        }

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
    }
}