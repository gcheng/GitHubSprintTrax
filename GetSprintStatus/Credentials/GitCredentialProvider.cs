using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GetSprintStatus.Credentials
{
    /// <summary>
    /// An <see cref="ICredentialProvider">ICredentialProvider</see> that
    /// checks the current git credential helper for credentials, and
    /// gets them from there if configured.
    /// </summary>
    internal class GitCredentialProvider : ICredentialProvider
    {
        private readonly string host;
        private Credentials credentials;

        public GitCredentialProvider(string host)
        {
            this.host = host;
        }

        #region ICredentialProvider Members

        public Credentials GetCredentials()
        {
            try
            {

                if (credentials == null)
                {
                    ReadCredentialsFromGit();
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch (Exception )
// ReSharper restore EmptyGeneralCatchClause
            {
                // Something failed, let other handlers handle it
            }
            return credentials;
        }

        #endregion

        private void ReadCredentialsFromGit()
        {
            string helper = ReadCredentialsHelper();
            if (helper == null) return;

            IEnumerable<string> responseLines = ExecuteHelper(helper);
            if (responseLines == null) return;

            CreateCredentials(responseLines);
        }

        private static string ReadCredentialsHelper()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = "-c 'git config --get credential.helper'",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using (Process p = Process.Start(startInfo))
            {
                string helper = p.StandardOutput.ReadToEnd();
                return helper;
            }
        }

        private IEnumerable<string> ExecuteHelper(string helperCommand)
        {
            helperCommand = helperCommand.Trim();
            bool useBash = false;

            if (helperCommand.StartsWith("!"))
            {
                helperCommand = helperCommand.Substring(1);
                useBash = true;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = useBash ? "bash" : helperCommand,
                Arguments = useBash
                    ? string.Format("-c '{0} get", helperCommand)
                    : "get",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using (Process p = Process.Start(startInfo))
            {
                p.StandardInput.WriteLine(string.Format("host={0}", host));
                p.StandardInput.WriteLine();
                p.StandardInput.Close();

                var lines = new List<string>();
                string line;
                while ((line = p.StandardOutput.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                return lines;
            }
        }

        private void CreateCredentials(IEnumerable<string> helperResponseLines)
        {
            Dictionary<string, string> results = ParseHelperLines(helperResponseLines);
            credentials = new Credentials(results["username"], results["password"]);
        }

        private Dictionary<string, string> ParseHelperLines(IEnumerable<string> lines)
        {
            var results = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                int firstEqualsIndex = line.IndexOf('=');
                string key = line.Substring(0, firstEqualsIndex);
                string value = line.Substring(firstEqualsIndex + 1);
                results[key] = value;
            }
            return results;
        }
    }
}                                                                               