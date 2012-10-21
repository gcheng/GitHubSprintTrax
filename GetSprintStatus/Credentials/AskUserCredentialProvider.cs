using GetSprintStatus.CommandLine;

namespace GetSprintStatus.Credentials
{
    /// <summary>
    /// Credential provider that asks the user for username and password
    /// </summary>
    internal class AskUserCredentialProvider : ICredentialProvider
    {
        private Credentials credentials;

        #region ICredentialProvider Members

        public Credentials GetCredentials()
        {
            if (credentials == null)
            {
                string username = ConsolePrompt.Prompt("User");
                string password = ConsolePrompt.PromptSecret("Password");
                credentials = new Credentials(username, password);
            }
            return credentials;
        }

        #endregion
    }
}