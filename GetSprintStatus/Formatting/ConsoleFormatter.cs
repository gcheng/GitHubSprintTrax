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
            WriteHeader(stats);
            WriteBurndownStats(stats);
        }

        public void Visit(CumulativeFlowStats stats)
        {
            WriteHeader(stats);
            WriteCFDStats(stats);
        }

        private void WriteHeader(IStatCalculator stats)
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

        private void WriteCFDStats(CumulativeFlowStats stats)
        {
            output.WriteLine("Pending: {0}", stats.Pending);
            output.WriteLine("In Progress: {0}", stats.InProgress);
            output.WriteLine("Ready For Test: {0}", stats.ReadyForTest);
            output.WriteLine("In Test: {0}", stats.InTest);
            output.WriteLine("Done: {0}", stats.Done);
            output.WriteLine();
        }
    }
}
