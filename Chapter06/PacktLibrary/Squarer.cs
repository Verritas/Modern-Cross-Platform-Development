using System;
using System.Threading;

namespace Packt.Shared
{
    public static class Squarer
    {
        public static double Square<t> (t input)  where t:IConvertible {
            double d = input.ToDouble(Thread.CurrentThread.CurrentCulture);

            return d * d;
        }
    }
}