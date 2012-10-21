using System;
using System.Collections.Generic;
using GetSprintStatus.Credentials;
using GetSprintStatus.Formatting;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            CompositeCredentialProvider credentialProvider = new CompositeCredentialProvider()
                .Add(new GitCredentialProvider("github.com"))
                .Add(new AskUserCredentialProvider());

            Authorization auth = AuthManager.GetAuthorization(credentialProvider);
            var github = new GithubService(auth);

            string ownerLogin = args[0];
            string repositoryName = args[1];

            var reader = new SprintReader(github, ownerLogin, repositoryName);
            SprintStats stats = reader.GetSprintStatistics();

            var formatters = new List<IFormatter>
            {
                new ClipboardFormatter(),
                new ConsoleFormatter(Console.Out),
                new ErrorFormatter(Console.Error)
            };

            formatters.ForEach(f => f.WriteStatistics(stats));
        }
    }
}
