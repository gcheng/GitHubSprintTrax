using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    internal class SprintReader
    {
        private readonly Regex devEstimateRegex = new Regex(@"^Dev Estimate:\s*(?<estimate>\d+(\.\d+)?)\s*$",
            RegexOptions.Multiline | RegexOptions.IgnoreCase);
        private readonly Regex testEstimateRegex = new Regex(@"^Test Estimate:\s*(?<estimate>\d+(\.\d+)?)\s*$",
            RegexOptions.Multiline | RegexOptions.IgnoreCase);

        private readonly GithubService github;
        private readonly string ownerLogin;
        private readonly string repositoryName;

        private Milestone currentMilestone;
        private List<Issue> openIssues;
        private List<Issue> closedIssues;

        private Repository repository;

        public SprintReader(GithubService github, string ownerLogin, string repositoryName)
        {
            this.github = github;
            this.ownerLogin = ownerLogin;
            this.repositoryName = repositoryName;
        }

        public void GetSprintStatistics(IStatCalculator stats)
        {
            repository = github.GetRepository(ownerLogin, repositoryName);
            FindCurrentMilestone();
            FindIssues();

            stats.Start(repository.Name, currentMilestone.Title);
            CalculateStatistics(stats);
        }

        private void FindCurrentMilestone()
        {
            currentMilestone = GithubConventions.GetCurrentMilestone(repository);
        }

        private void FindIssues()
        {
            openIssues = repository.GetIssues(o => { o.Milestone = currentMilestone; }).ToList();
            closedIssues = repository.GetIssues(o =>
            {
                o.Milestone = currentMilestone;
                o.State = IssueState.Closed;
            }).ToList();
        }

        private void CalculateStatistics(IStatCalculator stats)
        {
            float devEstimate;
            float testEstimate;

            foreach (Issue issue in openIssues.Concat(closedIssues))
            {
                GithubConventions.ParseEstimates(issue, stats, out devEstimate, out testEstimate);
                stats.AddIssue(issue, devEstimate, testEstimate);
            }
        }
    }
}