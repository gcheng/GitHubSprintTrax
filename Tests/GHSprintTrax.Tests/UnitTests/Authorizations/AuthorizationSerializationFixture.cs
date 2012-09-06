using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Xunit;

using GHSprintTrax.Tests.TestSupport;
using Xunit.Extensions;
using GHSprintTrax.GithubApi.Authorization;

namespace GHSprintTrax.Tests.UnitTests.Authorizations
{
    /// <summary>
    /// Tests around the serialization of various data structures
    /// used in the Github Authorization API
    /// </summary>
    public class AuthorizationSerializationFixture : TestClass
    {
        [Fact]
        public void DefaultCreateAuthorizationRequestBodySerializesAsEmptyJsonObject()
        {
            var body = new CreateAuthorizationRequestBody();
            string serialized = JsonConvert.SerializeObject(body);

            Assert.Matches("^\\s*{\\s*}\\s*$", serialized);
        }
    }
}
