using System;
using GetSprintStatus.CommandLine;

namespace GetSprintStatus.Credentials
{
    /// <summary>
    /// Credential provider that asks the user for username and password
    /// </summary>
    class AskUserCredentialProvider : ICredentialProvider
    {
        private Credentials credentials;

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
    }
}
