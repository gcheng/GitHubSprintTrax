using System.Collections.Generic;
using GHSprintTrax.GithubApi;
using GetSprintStatus.Conventions;
using GetSprintStatus.Formatting;

namespace GetSprintStatus.Stats
{
    abstract class StatCalculatorBase : IStatCalculator
    {
        private string headerMessage;
        private readonly List<ParseError> errors = new List<ParseError>();

        public void Start(string message)
        {
            headerMessage = message;
        }

        public virtual void StartRepository(string title, string milestone)
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
        public string HeaderMessage { get { return headerMessage; } }

        protected IssueStates ValidateIssueStates(Issue issue)
        {
            var issueStates = new IssueStates(issue);
            if(issueStates.IsBlocked) {
                AddError(issue, "Blocked");
            }

            if (!issueStates.IsDone && !issueStates.HasState)
            {
                AddError(issue, "Issue doesn't have a state");
                return null;
            }

            if (!issueStates.IsDone && issueStates.HasManyStates)
            {
                AddError(issue, "Issue has multiple state labels");
                return null;
            }

            return issueStates;
        }
    }
}
