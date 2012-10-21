using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    /// <summary>
    /// Error returned when an issue fails parsing for some reason.
    /// </summary>
    class ParseError
    {
        public Issue Issue { get; private set; }
        public string Reason { get; private set; }

        public ParseError(Issue issue, string reason)
        {
            Issue = issue;
            Reason = reason;
        }
    }
}
