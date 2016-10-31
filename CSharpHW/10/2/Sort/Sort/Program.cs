using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{

    // QuickSort implementation
    static class Sorter
    {
        private static Random rand = new Random();

        public static void Swap(ref double a, ref double b)
        {
            /*
            a = a - b;
            b += a;
            a = b - a;
            */
            var temp = a;
            a = b;
            b = temp;
        }
        public static void Sort(double[] arr, int l, int r) 
        {
            //double mid = arr[rand.Next(l, r + 1)];
            double mid = arr[(l + r) / 2];

            int ll = l;
            int rr = r;

            while (ll <= rr)
            {
                while (arr[ll] < mid)
                    ll++;
                while (arr[rr] > mid)
                    rr--;

                if (ll <= rr)
                {
                    Swap(ref arr[ll], ref arr[rr]);
                    ll++;
                    rr--;
                }
            }

            if (rr > l)
                Sort(arr, l, rr);

            if (r > ll)
                Sort(arr, ll, r);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int size = 2000;
            int maxRand = 1000000;
            var r = new Random();
            double[] arr = new double[size];

            for (int i = 0; i < size; i++)
                arr[i] = r.Next(0, maxRand);

            Console.WriteLine("Unsorted:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} ", arr[i]);
            }

            Sorter.Sort(arr, 0, size - 1);

            Console.WriteLine("\nSorted:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} ", arr[i]);
            }

            Console.ReadKey();
        }
    }
}
