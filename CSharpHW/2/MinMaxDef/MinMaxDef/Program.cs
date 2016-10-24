using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxDef
{
    class Program
    {
        static void Main(string[] args)
        {
            //byte
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "byte", byte.MinValue, byte.MaxValue, default(byte));

            //sbyte
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "sbyte", sbyte.MinValue, sbyte.MaxValue, default(sbyte));

            //short
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "short", short.MinValue, short.MaxValue, default(short));

            //ushort
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "ushort", ushort.MinValue, ushort.MaxValue, default(ushort));

            //int
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "int", int.MinValue, int.MaxValue, default(int));

            //uint
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "uint", uint.MinValue, uint.MaxValue, default(uint));

            //long
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "long", long.MinValue, long.MaxValue, default(long));

            //ulong
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "ulong", ulong.MinValue, ulong.MaxValue, default(ulong));

            //float
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "float", float.MinValue, float.MaxValue, default(float));

            //double
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "double", double.MinValue, double.MaxValue, default(double));

            //decimal
            Console.WriteLine("type: {0}\nmin: {1}\nmax: {2}\ndefault:{3}\n", "decimal", decimal.MinValue, decimal.MaxValue, default(decimal));

            Console.ReadKey();
        }
    }
}
