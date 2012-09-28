using System;
using GHSprintTrax.GithubApi;

namespace GHSprintTrax.Tests.TestSupport
{
    public class GetAuthorizationFixtureSetup : IDisposable
    {
        private AuthorizationService authService;
        private Authorization authorization;

        public Authorization GetAuthorization(string username, string password)
        {
            if (authorization == null)
            {
                authService = new AuthorizationService(username, password);
                authorization = authService.CreateAuthorization("testGHTestSuite",
                    scopes: new[] {"user", "repo"});
            }
            return authorization;
        }

        public void Dispose()
        {
            if (authorization != null)
            {
                authService.DeleteAuthorization(authorization);
                authorization = null;
            }
        }
    }
}
