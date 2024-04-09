using System;
using System.Diagnostics;

namespace ShellBit.Source
{
    internal class Shell
    {
        private readonly string _name = "ShellBit";
        private readonly string _version = "1.1.0 GOLD";

        private string _command;
        private string _keyword;
        private string _parameter;
        private Process _process;

        public void Parser(string keyword, string parameter)
        {
            _keyword = keyword.ToLower();

            if (_keyword == "--version")
                Console.WriteLine($"bit ({_name}) version {_version}");

            else
            {
                if (parameter != null)
                {
                    _parameter = parameter.ToLower();

                    if (_keyword == "kill")
                    {
                        if (_parameter == "pbs")
                            _command = $"/c {Command.Kill} \"PROCFIT BUSINESS SUITE.exe\"";

                        else
                            _command = $"/c {Command.Kill} \"{_parameter}.exe\"";
                    }

                    else
                        Console.WriteLine("ERROR: Unrecognized keyword. Please provide a valid keyword.");
                }

                else
                    Console.WriteLine("ERROR: Invalid parameter. Please provide a valid parameter.");
            }

            if (_command != null)
            {
                string password = GetPassword("Password: ");

                while (password != "0001")
                {
                    Console.WriteLine("ERROR: Invalid password.");

                    if (GetInput("QUEST: Are you want to try again? [Y/N]\n>>> ").ToLower() == "n")
                        break;

                    Console.WriteLine();
                    password = GetPassword("Password: ");
                }

                if (password == "0001")
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = _command,
                        UseShellExecute = false,
                        RedirectStandardOutput = true, // Redirecting output for error handling
                    };

                    _process = new Process
                    {
                        StartInfo = processStartInfo
                    };

                    _process.Start();
                    _process.WaitForExit();

                    int code = _process.ExitCode;

                    if (code != 0)
                        Console.WriteLine($"ERROR: The command \"{processStartInfo.Arguments.Remove(0, 3)}\" failed with an exit code \"{code}\".");

                    else
                        Console.WriteLine("Command successfully executed.");
                }
            }
        }

        private string GetPassword(string text)
        {
            string password = "";
            ConsoleKeyInfo keyInfo;

            Console.Write(text);

            do
            {
                keyInfo = Console.ReadKey(true);

                // Ignore any key other than a printable character
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*"); // Display '*' instead of the actual character
                }

                // Break the loop if the user presses the Enter key
            } while (keyInfo.Key != ConsoleKey.Enter);

            // For new line after password entry
            Console.WriteLine("\n");
            return password;
        }

        private string GetInput(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }
    }
}
