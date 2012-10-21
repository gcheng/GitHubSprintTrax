using System;
using GetSprintStatus.Credentials;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    internal class Program
    {
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

            var formatter = new Formatter(Console.Out);
            formatter.WriteStatistics(stats);
        }
    }
}