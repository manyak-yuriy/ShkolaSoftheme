using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.IO.Compression;
using MultiThreading;

namespace TPLMultiTasking
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootDir = @"D:\test";

            Console.WriteLine("Input path: ");
            string filePath = Console.ReadLine();

            Console.WriteLine("Input source string: ");
            string source = Console.ReadLine();

            Console.WriteLine("Input destination string: ");
            string dest = Console.ReadLine();

            if (filePath.Length == 0)
                filePath = rootDir;

            StringReplacer.ReplaceInDir(filePath, source, dest, new string[] {".dat", ".txt"});

            Console.ReadKey();
        }

    }
}
