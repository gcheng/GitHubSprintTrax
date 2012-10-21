using System.Collections.Generic;
using System.IO;
using System.Linq;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus.Formatting
{
    class ErrorFormatter : IFormatter
    {
        private readonly TextWriter output;

        public ErrorFormatter(TextWriter output)
        {
            this.output = output;
        }

        public void WriteStatistics(SprintStats stats)
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
            output.WriteLine("The following issues have issues:");
            output.WriteLine("=================================");
            output.WriteLine();
        }

        private void WriteErrors(IEnumerable<ParseError> group)
        {
            Issue firstIssue = group.First().Issue;
            int titleLength = firstIssue.Title.Length;
            titleLength = titleLength > 65 ? 65 : titleLength;

            output.WriteLine("{0}: {1}", firstIssue.Number, firstIssue.Title.Substring(0, titleLength));
            foreach (ParseError error in group)
            {
                output.WriteLine("    {0}", error.Reason);
            }
            output.WriteLine();
        }
    }
}
