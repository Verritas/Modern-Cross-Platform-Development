using System;
using static System.Console;

namespace CheckingForOverflow
{
    class Program
    {
        static void Main(string[] args)
        {
            unchecked {
            int x = int.MinValue-1;
            WriteLine($"Initial value: {x}");
            x++;
            WriteLine($"After incremeting: {x}");
            x++;
            WriteLine($"After incremeting: {x}");
            x++;
            WriteLine($"After incremeting: {x}");
            }
        }
    }
}
