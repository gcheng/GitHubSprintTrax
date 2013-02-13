using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;
using GetSprintStatus.Formatting;

namespace GetSprintStatus
{
    interface IStatCalculator
    {
        void Start(string title, string milestone);

        void AddIssue(Issue issue, float devEstimate, float testEstimate);

        void AddError(Issue issue, string reason);

        void Accept(IFormatter formatter);

        IEnumerable<ParseError> Errors { get; }
    }
}
