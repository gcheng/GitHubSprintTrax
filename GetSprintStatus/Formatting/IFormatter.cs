using GetSprintStatus.Stats;

namespace GetSprintStatus.Formatting
{
    internal interface IFormatter
    {
        void Visit(BurndownStats stats);
        void Visit(CumulativeFlowStats stats);
        void Visit(ContentTableStats stats);
    }
}
