using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    class SprintStats
    {
        private readonly List<ParseError> errors = new List<ParseError>();
        private int totalIssues;
        private float test;
        private float pending;
        private float inProgress;
        private float readyForTest;
        private float inTest;

        public SprintStats(Repository repo)
        {
            RepoName = repo.FullName;
        }

        public void AddError(ParseError error)
        {
            errors.Add(error);
        }

        public string RepoName { get; private set; }
        public IEnumerable<ParseError> Errors { get { return errors; } }
        public int TotalIssues { get { return totalIssues; } }
        public float DevRemaining { get { return pending + inProgress; } }
        public float TestRemaining { get { return test; } }
        public float Pending { get { return pending; } }
        public float InProgress { get { return inProgress; } }
        public float ReadyForTest { get { return readyForTest; } }
        public float InTest { get { return inTest; } }

        public void AddIssue()
        {
            ++totalIssues;
        }

        public void AddTest(float points)
        {
            test += points;
        }

        public void AddPending(float points)
        {
            pending += points;
        }

        public void AddInProgress(float points)
        {
            inProgress += points;
        }

        public void AddReadyForTest(float points)
        {
            readyForTest += points;
        }

        public void AddInTest(float points)
        {
            inTest += points;
        }
    }
}
