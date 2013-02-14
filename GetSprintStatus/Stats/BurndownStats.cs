using GHSprintTrax.GithubApi;
using GetSprintStatus.Formatting;

namespace GetSprintStatus.Stats
{
    class BurndownStats : StatCalculatorBase
    {
        public float DevRemaining
        {
            get { return Pending + InProgress; }
        }

        public float TestRemaining { get; private set; }

        public float Pending { get; private set; }

        public float InProgress { get; private set; }

        public float ReadyForTest { get; private set; }

        public float InTest { get; private set; }


        public override void AddIssue(Issue issue, float devEstimate, float testEstimate)
        {
            if (issue.IsClosed)
            {
                return; // don't care about closed issues
            }

            var issueStates = ValidateIssueStates(issue);
            if (issueStates == null)
            {
                return;
            }

            TestRemaining += testEstimate;
            Pending += (devEstimate * (issueStates.IsPending ? 1 : 0));
            InProgress += (devEstimate * (issueStates.IsInProgress ? 1 : 0));
            ReadyForTest += (testEstimate * (issueStates.IsReadyForTest ? 1 : 0));
            InTest += (testEstimate * (issueStates.IsInTest ? 1 : 0));
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
    }
}
