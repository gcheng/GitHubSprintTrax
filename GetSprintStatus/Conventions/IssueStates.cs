using System.Linq;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus.Conventions
{
    class IssueStates
    {
        private const string PendingLabel = "pending";
        private const string InProgressLabel = "in progress";
        private const string ReadyForTestLabel = "ready for test";
        private const string InTestLabel = "in test";
        private const string BlockedLabel = "blocked";
        private const string HoldLabel = "hold";

        public IssueStates(Issue issue)
        {
            if (issue.IsClosed)
            {
                IsDone = true;
            }

            var issueLabels = issue.LabelNames.Select(l => l.ToLowerInvariant()).ToList();
            HasState = true;
            int numStates = 0;
            foreach (string label in issueLabels)
            {
                switch (label)
                {
                    case BlockedLabel:
                    case HoldLabel:
                        IsBlocked = true;
                        break;

                    case PendingLabel:
                        IsPending = true;
                        ++numStates;
                        break;

                    case InProgressLabel:
                        IsInProgress = true;
                        ++numStates;
                        break;

                    case ReadyForTestLabel:
                        IsReadyForTest = true;
                        ++numStates;
                        break;

                    case InTestLabel:
                        IsInTest = true;
                        ++numStates;
                        break;
                }
            }

            if (numStates == 0)
            {
                HasState = false;
            }

            if (numStates > 1)
            {
                HasManyStates = true;
            }
        }

        public bool IsBlocked { get; private set; }
        public bool IsPending { get; private set; }
        public bool IsInProgress { get; private set; }
        public bool IsReadyForTest { get; private set; }
        public bool IsInTest { get; private set; }
        public bool IsDone { get; private set; }

        public bool HasState { get; private set; }
        public bool HasManyStates { get; private set; }
    }
}
