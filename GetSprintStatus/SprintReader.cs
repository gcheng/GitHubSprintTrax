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
            var today = DateTimeOffset.Now;
            var milestones = repository.GetMilestones().ToList();

            currentMilestone = milestones.FirstOrDefault(m => m.DueOn != null && m.DueOn.Value >= today) ??
                milestones.First(m => m.Title == "Current Sprint");
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
                ParseEstimates(issue, stats, out devEstimate, out testEstimate);
                stats.AddIssue(issue, devEstimate, testEstimate);
            }
        }

        private void ParseEstimates(Issue issue, IStatCalculator stats, out float devEstimate, out float testEstimate)
        {
            Match devMatches = devEstimateRegex.Match(issue.Body);
            Match testMatches = testEstimateRegex.Match(issue.Body);

            if (!devMatches.Success)
            {
                stats.AddError(issue, "Dev estimate not found in issue");
            }

            if (!testMatches.Success)
            {
                stats.AddError(issue, "Test estimate not found in issue");
            }

            float.TryParse(devMatches.Groups["estimate"].Value, out devEstimate);
            float.TryParse(testMatches.Groups["estimate"].Value, out testEstimate);
        }
    }
}