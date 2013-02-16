using System.IO;
using GetSprintStatus.Stats;

namespace GetSprintStatus.Formatting
{
    internal class ConsoleFormatter : IFormatter
    {
        private readonly TextWriter output;

        public ConsoleFormatter(TextWriter output)
        {
            this.output = output;
        }

        public void Visit(BurndownStats stats)
        {
            WriteHeader("Burndown data", stats);
            WriteBurndownStats(stats);
        }

        public void Visit(CumulativeFlowStats stats)
        {
            WriteHeader("Cumulative flow data", stats);
            WriteCFDStats(stats);
        }

        public void Visit(ContentTableStats stats)
        {
            WriteHeader("Sprint Contents", stats);
            WriteSprintContents(stats);
        }

        private void WriteHeader(string label, IStatCalculator stats)
        {
            output.WriteLine("{0}: Repository {1}, milestone {2}", 
                label, stats.RepoName, stats.Milestone);
        }

        private void WriteBurndownStats(BurndownStats stats)
        {
            output.WriteLine("Dev Remaining: {0}", stats.DevRemaining);
            output.WriteLine("Test Remaining: {0}", stats.TestRemaining);
            output.WriteLine("Pending: {0}", stats.Pending);
            output.WriteLine("In Progress: {0}", stats.InProgress);
            output.WriteLine("Ready For Test: {0}", stats.ReadyForTest);
            output.WriteLine("In Test: {0}", stats.InTest);
            output.WriteLine();
        }

        private void WriteCFDStats(CumulativeFlowStats stats)
        {
            output.WriteLine("Pending: {0}", stats.Pending);
            output.WriteLine("In Progress: {0}", stats.InProgress);
            output.WriteLine("Ready For Test: {0}", stats.ReadyForTest);
            output.WriteLine("In Test: {0}", stats.InTest);
            output.WriteLine("Done: {0}", stats.Done);
            output.WriteLine();
        }

        private void WriteSprintContents(ContentTableStats stats)
        {
            const string format = "{0,-16} {1,-4} {2,-4} {3,-6} {4}";
            output.WriteLine(format, "State", "Dev", "Test", "Number", "Title");
            output.WriteLine("------------------------------------------");
            foreach (var issue in stats.Issues)
            {
                output.WriteLine(format, issue.State, issue.Dev, issue.Test, issue.Number, issue.Title.Clip(65));
            }
        }
    }
}
