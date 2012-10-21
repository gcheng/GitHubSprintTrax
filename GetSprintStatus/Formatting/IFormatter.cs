namespace GetSprintStatus.Formatting
{
    internal interface IFormatter
    {
        void WriteStatistics(SprintStats stats);
    }
}