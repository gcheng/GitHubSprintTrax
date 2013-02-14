using GHSprintTrax.GithubApi;
using GetSprintStatus.Formatting;

namespace GetSprintStatus.Stats
{
    class CumulativeFlowStats : StatCalculatorBase
    {
        public float Pending { get; private set; }
        public float InProgress { get; private set; }
        public float ReadyForTest { get; private set; }
        public float InTest { get; private set; }
        public float Done { get; private set; }

        public override void AddIssue(Issue issue, float devEstimate, float testEstimate)
        {
            var issueStates = ValidateIssueStates(issue);
            if (issueStates == null)
            {
                return;
            }

            if (issue.IsClosed)
            {
                Done += devEstimate + testEstimate;
            }
            else
            {
                Pending += ((devEstimate + testEstimate)*(issueStates.IsPending ? 1 : 0));
                InProgress += (devEstimate*(issueStates.IsInProgress ? 1 : 0));
                ReadyForTest += (testEstimate*(issueStates.IsReadyForTest ? 1 : 0));
                InTest += (testEstimate*(issueStates.IsInTest ? 1 : 0));
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
    }
}
