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

        public void WriteStatistics(SprintStats stats)
        {
            output.WriteLine("Repository {0}", stats.RepoName);
            output.WriteLine("For milestone {0}", stats.MilestoneName);
            WriteStats(stats);
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
            output.WriteLine("Total open work items: {0}", stats.TotalOpenIssues);
            output.WriteLine("Total closed work items: {0}", stats.TotalClosedIssues);
            output.WriteLine("Total work in sprint: {0}", stats.Total);
            output.WriteLine("Total work done so far: {0}", stats.Done);
            output.WriteLine();
        }
    }
}