using System;
using GetSprintStatus.Credentials;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentialProvider = new CompositeCredentialProvider()
                .Add(new GitCredentialProvider("github.com"))
                .Add(new AskUserCredentialProvider());

            var auth = AuthManager.GetAuthorization(credentialProvider);
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
