using System.Collections.Generic;
using System.Linq;
using GHSprintTrax.GithubApi;
using GetSprintStatus.Conventions;
using GetSprintStatus.Formatting;

namespace GetSprintStatus.Stats
{
    class ContentTableStats : StatCalculatorBase
    {
        public class IssueEntry
        {
            public string State { get; private set; }
            public float Dev { get; private set; }
            public float Test { get; private set; }
            public long Number { get; private set; }
            public string Title { get; private set; }

            public IssueEntry(string state, float dev, float test, long number, string title)
            {
                State = state;
                Dev = dev;
                Test = test;
                Number = number;
                Title = title;
            }
        }

        private readonly List<IssueEntry> issues = new List<IssueEntry>();

        public IEnumerable<IssueEntry> Issues
        {
            get { return issues.OrderBy(i => i.State, new StateComparer()).ThenBy(i => i.Number); }
        }

        public override void AddIssue(Issue issue, float devEstimate, float testEstimate)
        {
            IssueStates states = ValidateIssueStates(issue);
            if (states != null)
            {
                issues.Add(new IssueEntry(states.ToString(), devEstimate, testEstimate, issue.Number, issue.Title));
            }
        }

        public override void AddError(Issue issue, string reason)
        {
            if (!issue.IsClosed)
            {
                base.AddError(issue, reason);
            }
        }

        public override void Accept(IFormatter formatter)
        {
            formatter.Visit(this);
        }

        private class StateComparer : IComparer<string>
        {
            private static readonly List<string> stateOrder = new List<string>
            {
                "Pending", "In Progress", "Ready for Test", "In Test", "Done"
            };

            public int Compare(string x, string y)
            {
                return stateOrder.FindIndex(s => s == x) - stateOrder.FindIndex(s => s == y);
            }
        }
    }
}
