using System.Collections.Generic;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    internal class SprintStats
    {
        private readonly List<ParseError> errors = new List<ParseError>();

        public SprintStats(string repo, string milestone)
        {
            RepoName = repo;
            MilestoneName = milestone;
        }

        public string RepoName { get; private set; }

        public string MilestoneName { get; private set; }

        public IEnumerable<ParseError> Errors
        {
            get { return errors; }
        }

        public int TotalOpenIssues { get; private set; }
        public int TotalClosedIssues { get; private set; }

        public float DevRemaining
        {
            get { return Pending + InProgress; }
        }

        public float TestRemaining { get; private set; }

        public float Total { get { return DevRemaining + TestRemaining + Done; } }

        public float Done { get; private set; }

        public float Pending { get; private set; }

        public float InProgress { get; private set; }

        public float ReadyForTest { get; private set; }

        public float InTest { get; private set; }

        public void AddError(Issue issue, string reason)
        {
            errors.Add(new ParseError(issue, reason));
        }

        public void AddOpenIssue()
        {
            ++TotalOpenIssues;
        }

        public void AddClosedIssue()
        {
            ++TotalClosedIssues;
        }

        public void AddTest(float points)
        {
            TestRemaining += points;
        }

        public void AddPending(float points)
        {
            Pending += points;
        }

        public void AddInProgress(float points)
        {
            InProgress += points;
        }

        public void AddReadyForTest(float points)
        {
            ReadyForTest += points;
        }

        public void AddInTest(float points)
        {
            InTest += points;
        }

        public void AddDone(float points)
        {
            Done += points;
        }
    }
}