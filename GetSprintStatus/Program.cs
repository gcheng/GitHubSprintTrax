using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        private readonly List<Tuple<string, string>> repoNames;
        private List<IFormatter> formatters;
        private readonly IStatCalculator stats;

        Program(IStatCalculator stats, IEnumerable<Tuple<string, string>> repoNames, bool listErrors)
        {
            this.stats = stats;
            this.repoNames = new List<Tuple<string, string>>(repoNames);

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
            foreach (var repoName in repoNames)
            {
                var reader = new SprintReader(github, repoName.Item1, repoName.Item2);
                reader.GetSprintStatistics(stats);
            }
        }

        private void ShowResults()
        {
            formatters.ForEach(f => stats.Accept(f));
        }

        [STAThread]
        private static void Main(string[] args)
        {
            IStatCalculator stats = null;
            bool showErrors = true;
            bool showHelp = false;

            var p = new OptionSet
            {
                { "b|burndown", "Get burndown chart data", v => { 
                    stats = new BurndownStats();
                }},
                { "c|cfd", "Get cumulative flow chart data", v => { stats = new CumulativeFlowStats(); }},
                { "t|table", "Summarize issues in sprint", v => { stats = new ContentTableStats(); }},
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

            IList<Tuple<string, string>> repos = GetRepoNames(extras);

            if (stats == null)
            {
                stats = new BurndownStats();
            }
        
            var program = new Program(stats, repos, showErrors);
            program.Go();
        }

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: GetSprintStatus [Options] <repo owner> <repo name>");
            p.WriteOptionDescriptions(Console.Out);
        }

        private static IList<Tuple<string, string>> GetRepoNames(IList<string> extraParams)
        {
            if (extraParams.Count == 2)
            {
                // owner/repo on command line
                return new List<Tuple<string, string>>() {Tuple.Create(extraParams[0], extraParams[1])};
            }
           
            if (extraParams.Count == 1)
            {
                // project name - look up owner/repo names in app config file
                string repos = ConfigurationManager.AppSettings[extraParams[0]];
                if (repos == null)
                {
                    throw new Exception("Unknown project name");
                }

                return repos.Split(',').Select(repo => repo.Split('/')).Select(ownerRepo => Tuple.Create(ownerRepo[0], ownerRepo[1])).ToList();
            }

            throw new Exception("Must specify project name or owner and repo name on command line");
        } 
    }
}
