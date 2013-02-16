using System;
using System.Collections.Generic;
using GetSprintStatus.Credentials;
using GetSprintStatus.Formatting;
using GHSprintTrax.GithubApi;
using GetSprintStatus.Stats;
using NDesk.Options;

namespace GetSprintStatus
{
    internal class Program
    {
        private GithubService github;
        private readonly string ownerLogin;
        private readonly string repository;

        private List<IFormatter> formatters;
        private readonly IStatCalculator stats;

        Program(IStatCalculator stats, string ownerLogin, string repository, bool listErrors)
        {
            this.stats = stats;
            this.ownerLogin = ownerLogin;
            this.repository = repository;

            connectToGithub();
            createFormatters(listErrors);
        }

        private void Go()
        {
            CalculateStats();
            ShowResults();
        }

        private void connectToGithub()
        {
            var credentialProvider = new CompositeCredentialProvider()
                .Add(new GitCredentialProvider("github.com"))
                .Add(new AskUserCredentialProvider());

            var authorization = AuthManager.GetAuthorization(credentialProvider);
            github = new GithubService(authorization);
        }

        private void createFormatters(bool listErrors)
        {
            formatters = new List<IFormatter>
            {
                new ClipboardFormatter(),
                new ConsoleFormatter(Console.Out)
            };

            if (listErrors)
            {
                formatters.Add(new ErrorFormatter(Console.Error));
            }
        }

        private void CalculateStats()
        {
            var reader = new SprintReader(github, ownerLogin, repository);
            reader.GetSprintStatistics(stats);
        }

        private void ShowResults()
        {
            formatters.ForEach(f => stats.Accept(f));
        }

        [STAThread]
        private static void Main(string[] args)
        {
            bool calcCFD = false;
            bool showErrors = true;
            bool showHelp = false;

            var p = new OptionSet
            {
                { "b|burndown", "Get burndown chart data", v => { calcCFD = false; }},
                { "c|cfd", "Get cumulative flow chart data", v => { calcCFD = true; }},
                { "e|errors", "Turn on or off list of issues with state errors (defaults to on)", 
                    v => { showErrors = v != null; } },
                { "h|?|help", "Display this help message", v => showHelp = v != null }
            };
            List<String> extras = p.Parse(args);

            if (showHelp)
            {
                ShowHelp(p);
                return;
            } 
            
            if (extras.Count < 2)
            {
                Console.WriteLine("Error: Must specify owner and name of repository");
                ShowHelp(p);
                return;
            }

            string ownerLogin = extras[0];
            string repositoryName = extras[1];

            IStatCalculator stats;
            if (calcCFD)
            {
                stats = new CumulativeFlowStats();
            }
            else
            {
                stats = new BurndownStats();
            }

            var program = new Program(stats, ownerLogin, repositoryName, showErrors);
            program.Go();
        }

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: GetSprintStatus [Options] <repo owner> <repo name>");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
