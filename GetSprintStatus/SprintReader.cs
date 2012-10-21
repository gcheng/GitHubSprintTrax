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

        private readonly Regex devEstimateRegex = new Regex(@"^Dev Estimate:\s*(?<estimate>\d+(\.\d+)?)\s*$",
            RegexOptions.Multiline | RegexOptions.IgnoreCase);

        private readonly GithubService github;
        private readonly string ownerLogin;
        private readonly string repositoryName;

        private readonly List<string> stateLabels = new List<string>
        {Pending, InProgress, ReadyForTest, InTest};

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
            currentMilestone = repository.GetMilestones().First(m => m.Title == "Current Sprint");
        }

        private void FindIssues()
        {
            issues = repository.GetIssues(currentMilestone).ToList();
        }

        private SprintStats CalculateStatistics()
        {
            var stats = new SprintStats(repository);
            foreach (Issue issue in issues)
            {
                CalculateStatistics(issue, stats);
            }
            return stats;
        }

        private void CalculateStatistics(Issue issue, SprintStats stats)
        {
            float devEstimate = 0;
            float testEstimate = 0;
            ParseError err = null;

            err = ParseEstimates(issue, out devEstimate, out testEstimate);

            if (err != null)
            {
                stats.AddError(err);
                return;
            }

            ParseState(issue, stats, devEstimate, testEstimate);
        }

        private ParseError ParseEstimates(Issue issue, out float devEstimate, out float testEstimate)
        {
            devEstimate = 0;
            testEstimate = 0;

            Match devMatches = devEstimateRegex.Match(issue.Body);
            Match testMatches = testEstimateRegex.Match(issue.Body);

            if (!devMatches.Success)
            {
                return new ParseError(issue, "Dev Estimate not found in issue");
            }

            if (!testMatches.Success)
            {
                return new ParseError(issue, "Test Estimate not found in issue");
            }

            devEstimate = float.Parse(devMatches.Groups["estimate"].Value);
            testEstimate = float.Parse(testMatches.Groups["estimate"].Value);

            return null;
        }

        private void ParseState(Issue issue, SprintStats stats, float devEstimate, float testEstimate)
        {
            var statesInIssue = new Dictionary<string, int>
            {
                {Pending, 0},
                {InProgress, 0},
                {ReadyForTest, 0},
                {InTest, 0}
            };

            foreach (string label in issue.LabelNames.Select(l => l.ToLowerInvariant()))
            {
                if (stateLabels.Contains(label))
                {
                    statesInIssue[label]++;
                }
            }

            if (statesInIssue.Values.Sum() == 0)
            {
                stats.AddError(new ParseError(issue, "Issue doesn't have a state"));
                return;
            }

            if (statesInIssue.Values.Sum() > 1)
            {
                stats.AddError(new ParseError(issue, "Issue has multiple state labels"));
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