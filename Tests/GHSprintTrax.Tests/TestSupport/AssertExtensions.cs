using Xunit.Extensions;
using System.Text.RegularExpressions;

namespace GHSprintTrax.Tests.TestSupport
{
    /// <summary>
    /// Extension methods that hang off xunit.extensions.Assertions
    /// to provide nicer assert matching.
    /// </summary>
    public static class AssertExtensions
    {
        /// <summary>
        /// Extension method - assert that a string matches the given regex
        /// </summary>
        /// <param name="assert">Assertions object</param>
        /// <param name="regex">Regular expression to match</param>
        /// <param name="value">Value that should match regex</param>
        public static void Matches(this Assertions assert, string regex, string value)
        {
            assert.True(Regex.IsMatch(value, regex),
                string.Format("Expected string \"{0}\" to match regular expression \"{1}\"", value, regex));
        }

        public static void Matches(this Assertions assert, params string[] tokens)
        {
            var value = tokens[tokens.Length - 1];
            var regex = string.Join("\\s*", tokens, 0, tokens.Length - 1);

            assert.True(Regex.IsMatch(value, regex),
                string.Format("Expected string \"{0}\" to match regular expression \"{1}\"", value, regex)
            );
        }
    }
}
