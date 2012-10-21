using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetSprintStatus.CommandLine
{
    class ConsolePrompt
    {
        public static string Prompt(string message)
        {
            Console.Write("{0}: ", message);
            return Console.ReadLine();
        }

        public static string PromptSecret(string message)
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
