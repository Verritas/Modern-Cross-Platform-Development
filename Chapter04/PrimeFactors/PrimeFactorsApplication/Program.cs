using System;

namespace PrimeFactorsApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write the number 1..1000: ");
            var t = Console.ReadLine();
            if (Int32.TryParse(t, out int number)) {
                var line = PrimeFactorsLib.PrimeFactors.GetPrimeFactors(number);
                Console.WriteLine(line);
            }
        }
    }
}
