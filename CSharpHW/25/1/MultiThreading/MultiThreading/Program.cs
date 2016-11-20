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

            TraverseDir(rootDir);

            Console.ReadKey();
        }

        static void TraverseDir(object pathObj)
        {
            string rootPath = (string)pathObj;
            Console.WriteLine("thread {1}: Started travesing {0}", rootPath, Thread.CurrentThread.ManagedThreadId);

            if (!Directory.Exists(rootPath))
            {
                throw new ArgumentException("Invalid path name: {0}", rootPath);
            }

            int processorCnt = System.Environment.ProcessorCount;

            Queue<string> directories = new Queue<string>();

            directories.Enqueue(rootPath);

            while (directories.Any())
            {
                string currentDir = directories.Dequeue();

                string[] subDirs = { };
                string[] files = { };

                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
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
                {
                    procFileCnt++;
                    if (procFileCnt < processorCnt)
                    {
                        var thread = new Thread(CompressFile);
                        Console.WriteLine("Thread {0}: spawned compressing thread {1}", Thread.CurrentThread.ManagedThreadId, thread.ManagedThreadId);
                        thread.Start(file);
                    }
                    else
                        CompressFile(file);
                    //Console.WriteLine(file);
                }

                int procDirCnt = 0;
                foreach (var dir in subDirs)
                {
                    procDirCnt++;
                    if (procDirCnt < processorCnt)
                    {
                        var thread = new Thread(TraverseDir);
                        Console.WriteLine("Thread {0}: spawned traversing thread {1}", Thread.CurrentThread.ManagedThreadId, thread.ManagedThreadId);
                        thread.Start(dir);
                    }
                        
                    else
                        directories.Enqueue(dir);
                }
            }

            Console.WriteLine("thread {0}: Exit", Thread.CurrentThread.ManagedThreadId);
        }

        private static void CompressFile(object fileNameObj)
        {
            string fileName = (string)fileNameObj;

            if (fileName.EndsWith(".zip"))
            {
                Console.WriteLine("   {0} is already a zip-file", fileName);
                return;
            }

            if (File.Exists(fileName + ".zip"))
            {
                Console.WriteLine("   Zip file for {0} already exists", fileName);
                return;
            }

            Console.WriteLine("   thread {1}: Compressing {0}", fileName, Thread.CurrentThread.ManagedThreadId);

            using (ZipArchive zipFile = ZipFile.Open(fileName + ".zip", ZipArchiveMode.Create))
            {
                zipFile.CreateEntryFromFile(fileName, fileName);
            }
        }
    }
}
