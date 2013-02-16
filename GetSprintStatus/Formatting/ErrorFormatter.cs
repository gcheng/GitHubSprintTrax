using System.Collections.Generic;
using System.IO;
using System.Linq;
using GHSprintTrax.GithubApi;
using GetSprintStatus.Stats;

namespace GetSprintStatus.Formatting
{
    class ErrorFormatter : IFormatter
    {
        private readonly TextWriter output;

        public ErrorFormatter(TextWriter output)
        {
            this.output = output;
        }

        public void Visit(BurndownStats stats)
        {
            Output(stats);
        }

        public void Visit(CumulativeFlowStats stats)
        {
            Output(stats);
        }

        public void Visit(ContentTableStats stats)
        {
            Output(stats);
        }

        private void Output(IStatCalculator stats)
        {
            bool errorHeaderWritten = false;

            var errors = stats.Errors
                .GroupBy(e => e.Issue.Number);

            foreach (var group in errors)
            {
                if (!errorHeaderWritten)
                {
                    WriteErrorHeader();
                    errorHeaderWritten = true;
                }
                WriteErrors(group);
            }
        }

        private void WriteErrorHeader()
        {
            output.WriteLine("The following work items have issues:");
            output.WriteLine("=====================================");
            output.WriteLine();
        }

        private void WriteErrors(IEnumerable<ParseError> group)
        {
            var errorGroup = group.ToList();
            Issue firstIssue = errorGroup.First().Issue;

            output.WriteLine("{0}: {1}", firstIssue.Number, firstIssue.Title.Clip(65));
            output.WriteLine(firstIssue.HtmlUrl);
            foreach (var error in errorGroup)
            {
                output.WriteLine("    {0}", error.Reason);
            }
            output.WriteLine();
        }
    }

    static class StringExtensions
    {
        public static string Clip(this string s, int length, string ellipsis = "...")
        {
            if (s.Length <= length)
            {
                return s;
            }

            return s.Substring(0, length - ellipsis.Length) + ellipsis;
        }
    }
}
