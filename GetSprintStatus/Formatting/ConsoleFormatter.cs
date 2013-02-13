using System.IO;

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
            WriteBurndownHeader(stats);
            WriteBurndownStats(stats);
        }

        private void WriteBurndownHeader(BurndownStats stats)
        {
            output.WriteLine("Repository {0}", stats.RepoName);
            output.WriteLine("For milestone {0}", stats.Milestone);
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
    }
}