using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    internal class SprintReader
    {
        private const string Pending = "pending";
        private const string InProgress = "in progress";
        private const string ReadyForTest = "ready for test";
        private const string InTest = "in test";
        private const string Blocked = "blocked";
        private const string Hold = "hold";

        private readonly Regex devEstimateRegex = new Regex(@"^Dev Estimate:\s*(?<estimate>\d+(\.\d+)?)\s*$",
            RegexOptions.Multiline | RegexOptions.IgnoreCase);

        private readonly GithubService github;
        private readonly string ownerLogin;
        private readonly string repositoryName;

        private readonly List<string> stateLabels = new List<string>
        {Pending, InProgress, ReadyForTest, InTest};

        private readonly List<string> blockedLabels = new List<string> { Blocked, Hold };

        private readonly Regex testEstimateRegex = new Regex(@"^Test Estimate:\s*(?<estimate>\d+(\.\d+)?)\s*$",
            RegexOptions.Multiline | RegexOptions.IgnoreCase);

        private Milestone currentMilestone;
        private List<Issue> issues;
        private Repository repository;

        public SprintReader(GithubService github, string ownerLogin, string repositoryName)
        {
            this.github = github;
            this.ownerLogin = ownerLogin;
            this.repositoryName = repositoryName;
        }

        public SprintStats GetSprintStatistics()
        {
            repository = github.GetRepository(ownerLogin, repositoryName);
            FindCurrentMilestone();
            FindIssues();

            return CalculateStatistics();
        }

        private void FindCurrentMilestone()
        {
            var today = DateTimeOffset.Now;
            var milestones = repository.GetMilestones();

            currentMilestone = milestones.FirstOrDefault(m => m.DueOn != null && m.DueOn.Value >= today) ??
                milestones.First(m => m.Title == "Current Sprint");
        }

        private void FindIssues()
        {
            issues = repository.GetIssues(o => { o.Milestone = currentMilestone; }).ToList();
        }

        private SprintStats CalculateStatistics(SprintStats stats = null)
        {
            if (stats == null)
            {
                stats = new SprintStats(repository.Name, currentMilestone.Title);
            }
            foreach (Issue issue in issues)
            {
                CalculateStatistics(issue, stats);
            }
            return stats;
        }

        private void CalculateStatistics(Issue issue, SprintStats stats)
        {
            float devEstimate;
            float testEstimate;

            stats.AddIssue();
            ParseEstimates(issue, stats, out devEstimate, out testEstimate);
            ParseState(issue, stats, devEstimate, testEstimate);
        }

        private void ParseEstimates(Issue issue, SprintStats stats, out float devEstimate, out float testEstimate)
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

        private void ParseState(Issue issue, SprintStats stats, float devEstimate, float testEstimate)
        {
            var statesInIssue = new Dictionary<string, int>
            {
                {Pending, 0},
                {InProgress, 0},
                {ReadyForTest, 0},
                {InTest, 0},
            };

            foreach (string label in issue.LabelNames.Select(l => l.ToLowerInvariant()))
            {
                if (stateLabels.Contains(label))
                {
                    statesInIssue[label]++;
                }

                if(blockedLabels.Contains(label))
                {
                    stats.AddError(issue, "Blocked");
                }
            }

            if (statesInIssue.Values.Sum() == 0)
            {
                stats.AddError(issue, "Issue doesn't have a state");
                return;
            }

            if (statesInIssue.Values.Sum() > 1)
            {
                stats.AddError(issue, "Issue has multiple state labels");
                return;
            }

            stats.AddTest(testEstimate);
            stats.AddPending(devEstimate*statesInIssue[Pending]);
            stats.AddInProgress(devEstimate*statesInIssue[InProgress]);
            stats.AddReadyForTest(testEstimate*statesInIssue[ReadyForTest]);
            stats.AddInTest(testEstimate*statesInIssue[InTest]);
        }
    }
}