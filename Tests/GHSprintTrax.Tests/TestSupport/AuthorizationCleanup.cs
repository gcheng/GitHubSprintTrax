using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;

namespace GHSprintTrax.Tests.TestSupport
{
    /// <summary>
    /// A test data fixture that's used to set up Github authorizations
    /// for the given user, and then clean up the created authorizations
    /// afterwards.
    /// </summary>
    public class AuthorizationCleanup : IDisposable
    {
        private AuthorizationAPI authClient = null;
        private string prefix;

        public void Initialize(string username, string password, string prefix)
        {
            if (authClient == null)
            {
                authClient = new AuthorizationAPI(username, password);
                this.prefix = prefix;
                DeletePrefixedAuthorizations();
            }
        }

        public void Dispose()
        {
            DeletePrefixedAuthorizations();
        }

        private void DeletePrefixedAuthorizations()
        {
            foreach (var authorization in authClient.ListAuthorizations())
            {
                if (authorization.Note != null && authorization.Note.StartsWith(prefix))
                {
                    authClient.DeleteAuthorization(authorization);
                }
            }
        }
    }
}
