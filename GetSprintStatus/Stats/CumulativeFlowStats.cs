using System;
using System.Collections.Generic;
using GHSprintTrax.GithubApi;
using GetSprintStatus.Formatting;

namespace GetSprintStatus.Stats
{
    class CumulativeFlowStats : IStatCalculator
    {
        private readonly List<ParseError> errors = new List<ParseError>();

        public string RepoName { get; private set; }

        public string Milestone { get; private set; }

        public float Pending { get; private set; }
        public float InProgress { get; private set; }
        public float ReadyForTest { get; private set; }
        public float InTest { get; private set; }
        public float Done { get; private set; }

        public void Start(string title, string milestone)
        {
            RepoName = title;
            Milestone = milestone;
        }

        public void AddIssue(Issue issue, float devEstimate, float testEstimate)
        {
            throw new NotImplementedException();
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

        public IEnumerable<ParseError> Errors { get { return errors; } }
    }
}
