using System.IO;

namespace GetSprintStatus.Formatting
{
    class ErrorFormatter : IFormatter
    {
        private TextWriter output;

        public ErrorFormatter(TextWriter output)
        {
            this.output = output;
        }

        public void WriteStatistics(SprintStats stats)
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
