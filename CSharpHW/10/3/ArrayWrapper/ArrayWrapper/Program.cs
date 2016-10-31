using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayWrapper
{
    class ExtArray
    {
        private double[] arr;

        // number of elements currently present
        private int currLen;
        // elements currently present + reserved space
        private int fullLen;

        public int Length
        {
            get { return currLen; }
        }

        public ExtArray(int initLength)
        {
            arr = new double[initLength];
            currLen = fullLen = initLength;
        }

        public void Add(double val)
        {
            if (currLen == fullLen)
            {
                fullLen = currLen * 2;
                double[] temp = new double[currLen];
                for (int i = 0; i < currLen; i++)
                    temp[i] = arr[i];

                arr = new double[fullLen];
                for (int i = 0; i < currLen; i++)
                    arr[i] = temp[i];
            }
            arr[currLen] = val;
            currLen++;
        }

        // returns null if index is out of range
        public double? getByIndex(int index)
        {
            if (index > currLen - 1 || index < 0)
                return null;
            return arr[index];
        }

        // throws an exception if index is out of range
        public double this[int index]
        {
            get { return arr[index]; }
            set { arr[index] = value; }
        }

        public bool Contains(double val)
        {
            for (int i = 0; i < currLen; i++)
                if (arr[i] == val)
                    return true;
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                ExtArray a = new ExtArray(3);
                a[0] = 1;
                a[1] = 2;
                a[2] = 3;

                a.Add(4);
                a.Add(5);

                //a[-1] = 0;


                for (int i = 0; i < 100; i++)
                    a.Add(i);

                for (int i = 0; i < a.Length; i++)
                    Console.Write("{0} ", a[i]);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
          
            Console.ReadKey();
        }
    }
}
