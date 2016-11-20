using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.IO.Compression;


namespace MultiThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootDir = @"D:\test";

            Console.WriteLine("Input path: ");
            string filePath = Console.ReadLine();

            if (filePath.Length == 0)
                filePath = rootDir;

            TraverseDirAndExtract(rootDir);

            Console.ReadKey();
        }

        static void TraverseDirAndExtract(object pathObj)
        {
            string rootPath = (string)pathObj;

            if (!Directory.Exists(rootPath))
            {
                throw new ArgumentException("Invalid path name: {0}", rootPath);
            }

            Console.WriteLine("thread {1}: Started travesing {0}", rootPath, Thread.CurrentThread.ManagedThreadId);

            int processorCnt = System.Environment.ProcessorCount;

            Queue<string> directories = new Queue<string>();

            directories.Enqueue(rootPath);

            while (directories.Any())
            {
                string currentDir = directories.Dequeue();

                string[] nestedDirs = { };
                string[] files = { };

                try
                {
                    nestedDirs = Directory.GetDirectories(currentDir);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }

                try
                {
                    files = Directory.GetFiles(currentDir);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }

                int procFileCnt = 0;
                foreach (var file in files)
                    if (file.EndsWith(".zip"))
                    {
                        procFileCnt++;
                        if (procFileCnt < processorCnt)
                        {
                            var thread = new Thread(DeCompressFile);
                            Console.WriteLine("Thread {0}: spawned decompressing thread {1}", Thread.CurrentThread.ManagedThreadId, thread.ManagedThreadId);
                            thread.Start(file);
                        }
                        else
                            DeCompressFile(file);
                        //Console.WriteLine(file);
                    }

                int procDirCnt = 0;
                foreach (var dir in nestedDirs)
                {
                    procDirCnt++;
                    if (procDirCnt < processorCnt)
                    {
                        var thread = new Thread(TraverseDirAndExtract);
                        Console.WriteLine("Thread {0}: spawned traversing thread {1}", Thread.CurrentThread.ManagedThreadId, thread.ManagedThreadId);
                        thread.Start(dir);
                    }
                    else
                        directories.Enqueue(dir);
                }
            }

            Console.WriteLine("thread {0}: Exit", Thread.CurrentThread.ManagedThreadId);
        }

        private static void DeCompressFile(object fileNameObj)
        {
            string fileName = (string)fileNameObj;

            if (!fileName.EndsWith(".zip"))
            {
                Console.WriteLine("   {0} is not a zip-file: decompression canceled", fileName);
                return;
            }

            try
            {
                FileInfo fi = new FileInfo(fileName);
                ZipFile.ExtractToDirectory(fileName, fi.DirectoryName);
                Console.WriteLine("   thread {1}: Decompressing {0}", fileName, Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception exc)
            {
                Console.WriteLine("    " + exc.Message);
            }

        }
    }
}
