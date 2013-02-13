using GHSprintTrax.GithubApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GetSprintStatus
{
    /// <summary>
    /// Class that encapsulates the conventions we use for
    /// marking up Git issues
    /// </summary>
    static class GithubConventions
    {
        private static readonly Regex devEstimateRegex = new Regex(@"^Dev Estimate:\s*(?<estimate>\d+(\.\d+)?)\s*$",
            RegexOptions.Multiline | RegexOptions.IgnoreCase);
        private static readonly Regex testEstimateRegex = new Regex(@"^Test Estimate:\s*(?<estimate>\d+(\.\d+)?)\s*$",
            RegexOptions.Multiline | RegexOptions.IgnoreCase);

        public const string PendingLabel = "pending";
        public const string InProgressLabel = "in progress";
        public const string ReadyForTestLabel = "ready for test";
        public const string InTestLabel = "in test";
        public const string BlockedLabel = "blocked";
        public const string HoldLabel = "hold";

        public static Milestone GetCurrentMilestone(Repository repository)
        {
            var today = DateTimeOffset.Now;
            var milestones = repository.GetMilestones().ToList();

            return milestones.FirstOrDefault(m => m.DueOn != null && m.DueOn.Value >= today) ??
                milestones.First(m => m.Title == "Current Sprint");

        }

        public static void ParseEstimates(Issue issue, IStatCalculator stats, out float devEstimate, out float testEstimate)
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

        private static List<string> allLabels = new List<string>() {
            PendingLabel, 
            InProgressLabel,
            ReadyForTestLabel,
            InTestLabel,
            BlockedLabel,
            HoldLabel
        };

        public static Dictionary<string, int> GetIssueStates(Issue issue)
        {
            var results = new Dictionary<string, int>() {
                { PendingLabel, 0 },
                { InProgressLabel, 0 },
                { ReadyForTestLabel, 0 },
                { InTestLabel, 0 },
                { HoldLabel, 0 },
                { BlockedLabel, 0 }
            };

            var issueLabels = issue.LabelNames.Select(l => l.ToLowerInvariant()).ToList();
            for (int i = 0; i < allLabels.Count; ++i)
            {
                if (issueLabels.Contains(allLabels[i].ToLowerInvariant()))
                {
                    results[allLabels[i]]++;
                }
            }
            return results;
        }
    }
}
