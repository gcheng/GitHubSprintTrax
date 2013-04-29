﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus.Conventions
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

        private const string PartnerLabelColor = "02d7e1";

        public static Milestone GetCurrentMilestone(Repository repository)
        {
            var today = DateTimeOffset.Now;
            var milestones = repository.GetMilestones()
                .Concat(repository.GetMilestones(o => { o.State = MilestoneState.Closed; }))
                .ToList();

            return milestones.Where(m => m.DueOn != null && m.DueOn.Value >= today).OrderBy(m => m.DueOn).FirstOrDefault() ??
                milestones.First(m => m.Title == "Current Sprint");

        }

        public static bool IsPartnerIssue(Issue issue)
        {
            return issue.Labels.Any(l => l.Color == PartnerLabelColor);
        }

        public static void ParseEstimates(Issue issue, IStatisticsCalculator stats, out float devEstimate, out float testEstimate)
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
