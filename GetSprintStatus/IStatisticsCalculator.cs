using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;
using GetSprintStatus.Formatting;

namespace GetSprintStatus
{
    interface IStatisticsCalculator
    {
        void Start(string message);

        void StartRepository(string title, string milestone);

        void AddIssue(Issue issue, float devEstimate, float testEstimate);

        void AddError(Issue issue, string reason);

        void Accept(IFormatter formatter);

        IEnumerable<ParseError> Errors { get; }

        string RepoName { get; }
        string Milestone { get; }
        string HeaderMessage { get; }
    }
}
