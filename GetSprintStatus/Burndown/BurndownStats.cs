using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;
using GetSprintStatus.Formatting;

namespace GetSprintStatus
{
    class BurndownStats : IStatCalculator
    {
        private const string PendingLabel = "pending";
        private const string InProgressLabel = "in progress";
        private const string ReadyForTestLabel = "ready for test";
        private const string InTestLabel = "in test";
        private const string BlockedLabel = "blocked";
        private const string HoldLabel = "hold";

        private readonly List<string> stateLabels = new List<string> { PendingLabel, InProgressLabel, ReadyForTestLabel, InTestLabel };
        private readonly List<string> blockedLabels = new List<string> { BlockedLabel, HoldLabel };

        public string RepoName { get; private set; }
        public string Milestone { get; private set; }

        private List<ParseError> errors = new List<ParseError>();

        public IEnumerable<ParseError> Errors
        {
            get { return errors; }
        }

        public float DevRemaining
        {
            get { return Pending + InProgress; }
        }

        public float TestRemaining { get; private set; }

        public float Pending { get; private set; }

        public float InProgress { get; private set; }

        public float ReadyForTest { get; private set; }

        public float InTest { get; private set; }

        public void Start(string title, string milestone)
        {
            RepoName = title;
            Milestone = milestone;
        }

        public void AddIssue(Issue issue, float devEstimate, float testEstimate)
        {
            if (issue.IsClosed)
            {
                return; // don't care about closed issues
            }

            var issueStates = GithubConventions.GetIssueStates(issue);
            
            if (issueStates[GithubConventions.BlockedLabel] +
                issueStates[GithubConventions.HoldLabel] > 0)
            {
                AddError(issue, "Blocked");
                issueStates.Remove(GithubConventions.BlockedLabel);
                issueStates.Remove(GithubConventions.HoldLabel);
            }

            if (issueStates.Values.Sum() == 0)
            {
                AddError(issue, "Issue doesn't have a state");
                return;
            }

            if (issueStates.Values.Sum() > 1)
            {
                AddError(issue, "Issue has multiple state labels");
                return;
            }

            TestRemaining += testEstimate;
            Pending += (devEstimate * issueStates[PendingLabel]);
            InProgress += (devEstimate * issueStates[InProgressLabel]);
            ReadyForTest += (testEstimate * issueStates[ReadyForTestLabel]);
            InTest += (testEstimate * issueStates[InTestLabel]);
        }

        public void AddError(Issue issue, string reason)
        {
            if (!issue.IsClosed)
            {
                errors.Add(new ParseError(issue, reason));
            }
        }

        public void Accept(IFormatter formatter)
        {
            formatter.Visit(this);
        }
    }
}
