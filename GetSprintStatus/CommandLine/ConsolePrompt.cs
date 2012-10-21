using System;

namespace GetSprintStatus.CommandLine
{
    internal class ConsolePrompt
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

            ConsoleKeyInfo key = Console.ReadKey(true);
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