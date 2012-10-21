using Xunit;
using Xunit.Extensions;

namespace GHSprintTrax.Tests.Experiments
{
    /// <summary>
    /// A simple set of tests to make sure we have xunit and
    /// company configured correctly.
    /// </summary>
    public class BootstrapFixture : TestClass
    {
        [Fact]
        public void TestShouldRun()
        {
            Assert.True(true);
        }

        [Fact]
        public void ThisTestShouldRunToo()
        {
            Assert.False(false);
        }
    }
}