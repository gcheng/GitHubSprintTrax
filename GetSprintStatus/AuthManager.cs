using System.Linq;
using GetSprintStatus.Credentials;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    internal static class AuthManager
    {
        private const string GithubAuthNote = "SprintReader-4eb63d45-07cd-4420-915a-26ced4da0d52";

        public static Authorization GetAuthorization(ICredentialProvider credentialProvider)
        {
            Credentials.Credentials credentials = credentialProvider.GetCredentials();

            var authService = new AuthorizationService(credentials.Username, credentials.Password);

            return GetAuthorization(authService);
        }

        private static Authorization GetAuthorization(AuthorizationService authService)
        {
            Authorization auth = authService.ListAuthorizations().FirstOrDefault(a => a.Note == GithubAuthNote);

            if (auth == null)
            {
                auth = CreateSprintStatAuthorization(authService);
            }

            return auth;
        }

        private static Authorization CreateSprintStatAuthorization(AuthorizationService authService)
        {
            Authorization auth = authService.CreateAuthorization(note: GithubAuthNote,
                scopes: new[] {"repo", "public_repo", "repo:status"});
            return auth;
        }
    }
}