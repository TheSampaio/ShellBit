using System;

namespace ShellBit.Source
{
    internal class Program
    {
        static public void Main(string[] args)
        {
            Shell shell = new Shell();

            if (args.Length > 1)
                shell.Parser(args[0], args[1]);

            else if (args.Length > 0)
                shell.Parser(args[0], null);

            else
                Console.WriteLine("There are no arguments.");
        }
    }
}
