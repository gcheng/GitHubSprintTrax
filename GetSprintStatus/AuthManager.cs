using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    static class AuthManager
    {
        private const string GithubAuthNote = "SprintReader-4eb63d45-07cd-4420-915a-26ced4da0d52";
    
        public static Authorization GetAuthorization()
        {
            string userName = Prompt("User");
            string password = PromptSecret("Password");

            var authService = new AuthorizationService(userName, password);

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
            Authorization auth;
            auth = authService.CreateAuthorization(note: GithubAuthNote, scopes: new[]
            {
                "repo", "public_repo", "repo:status"
            });
            return auth;
        }

        private static string Prompt(string message)
        {
            Console.Write("{0}: ", message);
            return Console.ReadLine();
        }

        private static string PromptSecret(string message)
        {
            Console.Out.Write("{0}: ", message);

            string secret = "";

            var key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && secret.Length > 0)
                {
                    secret = secret.Substring(0, secret.Length - 1);
                    Console.Write("\b \b");
                }
                else
                {
                    Console.Write("*");
                    secret += key.KeyChar;
                }
                key = Console.ReadKey(true);
            }
            Console.WriteLine();
            return secret;
        }    
    }
}
