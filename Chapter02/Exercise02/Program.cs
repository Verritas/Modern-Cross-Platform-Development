using System;
using System.Collections.Generic;
namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            // sbyte byte short ushort int uint long ulong float double decimal
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("Type    Bytes of memory               Min                            Max");
            Console.WriteLine($"Sbyte  {sizeof(sbyte), 3} {sbyte.MinValue, 30} {sbyte.MaxValue, 30}");
            Console.WriteLine($"Byte   {sizeof(byte), 3} {byte.MinValue, 30} {byte.MaxValue, 30}");
            Console.WriteLine($"Short  {sizeof(short), 3} {short.MinValue, 30} {short.MaxValue, 30}");
            Console.WriteLine($"Ushort {sizeof(ushort), 3} {ushort.MinValue, 30} {ushort.MaxValue, 30}");
            Console.WriteLine($"Int    {sizeof(int), 3} {int.MinValue, 30} {int.MaxValue, 30}");
            Console.WriteLine($"Uint   {sizeof(uint), 3} {uint.MinValue, 30} {uint.MaxValue, 30}");
            Console.WriteLine($"Long   {sizeof(long), 3} {long.MinValue, 30} {long.MaxValue, 30}");
            Console.WriteLine($"ULong  {sizeof(ulong), 3} {ulong.MinValue, 30} {ulong.MaxValue, 30}");
            Console.WriteLine($"Float  {sizeof(float), 3} {float.MinValue, 30} {float.MaxValue, 30}");
            Console.WriteLine($"Double {sizeof(double), 3} {double.MinValue, 30} {double.MaxValue, 30}");
            Console.WriteLine($"Decimal{sizeof(decimal), 3} {decimal.MinValue, 30} {decimal.MaxValue, 30}");
            Console.WriteLine("------------------------------------------------------------------------");
        }
    }
}
