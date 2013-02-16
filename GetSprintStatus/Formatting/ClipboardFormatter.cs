using System.Windows;
using GetSprintStatus.Stats;

namespace GetSprintStatus.Formatting
{
    /// <summary>
    /// Places statistics onto the clipboard in a format ready to
    /// paste into sprint tracking spreadsheet
    /// </summary>
    class ClipboardFormatter : IFormatter
    {
        public void Visit(BurndownStats stats)
        {
            string text = string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                stats.DevRemaining, stats.TestRemaining,
                stats.InProgress,
                stats.ReadyForTest, stats.InTest);

            Clipboard.SetText(text, TextDataFormat.Text);
        }

        public void Visit(CumulativeFlowStats stats)
        {
            string text = string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                stats.Pending, stats.InProgress, stats.ReadyForTest, stats.InTest, stats.Done);

            Clipboard.SetText(text, TextDataFormat.Text);
        }

        public void Visit(ContentTableStats stats)
        {
            // Doesn't go to the clipboard
        }
    }
}
