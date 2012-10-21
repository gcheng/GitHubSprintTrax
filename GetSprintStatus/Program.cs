using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GHSprintTrax.GithubApi;

namespace GetSprintStatus
{
    class Program
    {
        static void Main(string[] args)
        {
            var auth = AuthManager.GetAuthorization();
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
