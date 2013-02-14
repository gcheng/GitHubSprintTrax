using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;
using GetSprintStatus.Formatting;

namespace GetSprintStatus.Stats
{
    abstract class StatCalculatorBase : IStatCalculator
    {
        private readonly List<ParseError> errors = new List<ParseError>(); 

        public virtual void Start(string title, string milestone)
        {
            RepoName = title;
            Milestone = milestone;
        }

        public abstract void AddIssue(Issue issue, float devEstimate, float testEstimate);

        public virtual void AddError(Issue issue, string reason)
        {
            errors.Add(new ParseError(issue, reason));
        }

        public abstract void Accept(IFormatter formatter);

        public IEnumerable<ParseError> Errors { get { return errors; } }
        public string RepoName { get; private set; }
        public string Milestone { get; private set; }

        protected Dictionary<string, int> ValidateIssueStates(Issue issue)
        {
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
                return null;
            }

            if (issueStates.Values.Sum() > 1)
            {
                AddError(issue, "Issue has multiple state labels");
                return null;
            }

            return issueStates;
        }
    }
}
