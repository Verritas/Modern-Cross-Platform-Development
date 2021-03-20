using System;
using System.Globalization;
using static System.Console;

namespace Internationalization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CultureInfo globalization = CultureInfo.CurrentCulture;
            CultureInfo localization = CultureInfo.CurrentUICulture;
            WriteLine("The current globalization culture is {0}: {1}",
            arg0:globalization.Name, globalization.DisplayName);
            WriteLine();

            WriteLine("en-US: English (United States)"); 
            WriteLine("da-DK: Danish (Denmark)"); 
            WriteLine("fr-CA: French (Canada)"); 
            Write("Enter an ISO culture code: ");
            string newCulture = ReadLine();

            if (!string.IsNullOrWhiteSpace(newCulture)) {
                CultureInfo ci = new CultureInfo(newCulture);
                CultureInfo.CurrentCulture = ci;
                CultureInfo.CurrentUICulture = ci;
            }
            WriteLine();

            WriteLine("Enter your name: ");
            string name = ReadLine();

            WriteLine("Enter your date of birth: ");
            string dob = ReadLine();

            WriteLine("Enter your salary: ");
            string salary = ReadLine();

            DateTime date = DateTime.Parse(dob);
            int minutes = (int)DateTime.Today.Subtract(date).TotalMinutes;
            decimal earns = decimal.Parse(salary);

            WriteLine(
                "{0} was born on a {1:dddd}, is {2:N0} minutes old, and earns {3:C}",
                name, date, minutes, earns);
        }
    }
}
