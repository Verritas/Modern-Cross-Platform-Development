using System;
using System.Collections.Generic;
using System.Numerics;

namespace Exersice03
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<UInt64, string> nums = new Dictionary<UInt64, string>();
            nums.Add(1000000000000000000, "quintillion");
            nums.Add(1000000000000000, "quadrillion");
            nums.Add(1000000000000, "trillion");
            nums.Add(1000000000, "billion");
            nums.Add(1000000, "million");
            nums.Add(1000, "thousand");
            nums.Add(100, "hundred");
            nums.Add(10, "ten");
            nums.Add(1, "one");
            
            var num = (UInt64)1211241234;
            foreach (var item in nums)
            {
                UInt64 div = num / Convert.ToUInt64(item.Key);
                if (div!=0) {
                    Console.Write($"{div} {getS(div, item.Value)} ");
                }
                num = num - div*Convert.ToUInt64(item.Key);
            }
        }

        private static string getS(BigInteger num, string str) {
            if ((num==1)||(num==0)) return str;
            else return str+"s";
        }
    }
}
