using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static int maxRand = 100000;
        private static void initArr(out int[] arr, int size)
        {
            arr = new int[size + 1];
            var rand = new Random();

            for (int i = 0; i < size / 2; i++)
            {
                var val = rand.Next(0, maxRand);

                arr[2 * i] = val;
                arr[2 * i + 1] = val;
            }

            arr[size] = rand.Next(0, maxRand);

        }
        static void Main(string[] args)
        {
            int size = 10;
            int[] arr;
            initArr(out arr, size);
            Console.ReadKey();
        }
    }
}
