using System;
using System.Text.RegularExpressions;

namespace Exersice02
{
    class Program
    {
        static void Main(string[] args)
        {
            do {
            Console.WriteLine("Please print a regular expression: ");
            string regexString = Console.ReadLine();

            Console.WriteLine("Please print some input: ");
            string input = Console.ReadLine();
            Regex regex = new Regex(regexString);
            
            Console.WriteLine("{0} mathes {1}? {2}", arg0:input, arg1:regexString, arg2:regex.IsMatch(input).ToString());
            Console.WriteLine("Press ESC to end or any key to try again");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
