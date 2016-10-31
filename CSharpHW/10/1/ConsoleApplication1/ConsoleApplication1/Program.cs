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

        private static void shuffleArray(int[] arr, int size)
        {
            var rand = new Random();
            for (int i = 0; i < size; i++)
            {
                int randInd = rand.Next(i + 1, size + 1);

                int temp = arr[randInd];
                arr[randInd] = arr[i];
                arr[i] = temp;
            }
        }

        private static int findUnique(int[] arr, int size)
        {
            int res = 0;
            for (int i = 0; i <= size; i++)
                res = res ^ arr[i];
            return res;
        }
        static void Main(string[] args)
        {
            int size = 10000;
            int[] arr;
            initArr(out arr, size);
            shuffleArray(arr, size);

            Console.WriteLine("Array:");
            foreach (int elem in arr)
                Console.Write("{0} ", elem);
            Console.WriteLine();

            Console.WriteLine("Unique number: {0}", findUnique(arr, size));

            Console.ReadKey();
        }
    }
}
