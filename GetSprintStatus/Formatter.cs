using System.IO;

namespace GetSprintStatus
{
    internal class Formatter
    {
        private readonly TextWriter output;

        public Formatter(TextWriter output)
        {
            this.output = output;
        }

        public void WriteStatistics(SprintStats stats)
        {
            output.WriteLine("Repository {0}", stats.RepoName);
            WriteStats(stats);
            WriteErrors(stats);
        }

        private void WriteStats(SprintStats stats)
        {
            output.WriteLine("Dev Remaining: {0}", stats.DevRemaining);
            output.WriteLine("Test Remaining: {0}", stats.TestRemaining);
            output.WriteLine("Pending: {0}", stats.Pending);
            output.WriteLine("In Progress: {0}", stats.InProgress);
            output.WriteLine("Ready For Test: {0}", stats.ReadyForTest);
            output.WriteLine("In Test: {0}", stats.InTest);
            output.WriteLine();
        }

        private void WriteErrors(SprintStats stats)
        {
            bool errorHeaderWritten = false;

            foreach (ParseError error in stats.Errors)
            {
                if (!errorHeaderWritten)
                {
                    WriteErrorHeader();
                    errorHeaderWritten = true;
                }
                WriteError(error);
            }
        }

        private void WriteErrorHeader()
        {
            output.WriteLine("The following issues have issues:");
            output.WriteLine();
        }

        private void WriteError(ParseError error)
        {
            int titleLength = error.Issue.Title.Length;
            titleLength = titleLength > 65 ? 65 : titleLength;

            output.WriteLine("{0}: {1}", error.Issue.Number, error.Issue.Title.Substring(0, titleLength));
            output.WriteLine(error.Reason);
            output.WriteLine();
        }
    }
}