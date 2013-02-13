namespace GetSprintStatus.Formatting
{
    internal interface IFormatter
    {
        void Visit(BurndownStats stats);
    }
}
