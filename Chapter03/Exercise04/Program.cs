using System;

namespace Exercise04
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            int b = 0;
            try {
                Console.Write("Enter a number between 0 to 225: ");
                a = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter a number between 0 to 225: ");
                b = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(a / b);
            }
            catch(FormatException ex) {
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");
            }
            catch(DivideByZeroException ex) {
                Console.WriteLine(ex.GetType());
            }
            catch(Exception ex) {
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");
            }
        }
    }
}
