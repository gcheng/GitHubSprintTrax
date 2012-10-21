using System;
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
        private AuthorizationService authClient;
        private string prefix;

// ReSharper disable ParameterHidesMember

        #region IDisposable Members

        public void Dispose()
        {
            DeletePrefixedAuthorizations();
        }

        #endregion

        public void Initialize(string username, string password, string prefix)
// ReSharper restore ParameterHidesMember
        {
            if (authClient == null)
            {
                authClient = new AuthorizationService(username, password);
                this.prefix = prefix;
                DeletePrefixedAuthorizations();
            }
        }

        private void DeletePrefixedAuthorizations()
        {
            foreach (Authorization authorization in authClient.ListAuthorizations())
            {
                if (authorization.Note != null && authorization.Note.StartsWith(prefix))
                {
                    authClient.DeleteAuthorization(authorization);
                }
            }
        }
    }
}