using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    /// <summary>
    /// Error returned when an issue fails parsing for some reason.
    /// </summary>
    internal class ParseError
    {
        public ParseError(Issue issue, string reason)
        {
            Issue = issue;
            Reason = reason;
        }

        public Issue Issue { get; private set; }
        public string Reason { get; private set; }
    }
}