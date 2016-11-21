using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
    public static class StringReplacer
    {
        public static object syncObj = 0;

        public static int filesOpen = 0;
        public static void ReplaceInDir(string rootPath, string source, string dest, string[] suffixFilter, string logFileName)
        {

            if (!Directory.Exists(rootPath))
            {
                throw new ArgumentException("Invalid path name: {0}", rootPath);
            }

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

                try
                {
                    Parallel.ForEach(files, (f) => { ReplaceInFile(f, source, dest, suffixFilter, logFileName); });
                }
                catch (AggregateException ae)
                {
                    ae.Handle((ex) => {
                        Console.WriteLine(ex.Message);
                        return true;
                    });
                }

                foreach (var dir in nestedDirs)
                {
                    directories.Enqueue(dir);
                }
            }

        }

        public static void ReplaceInFile(string fileName, string source, string dest, string[] suffixFilter, string logFileName)
        {

            bool passed = false;

            if (fileName == logFileName)
                return;

            foreach (var filter in suffixFilter)
                if (fileName.EndsWith(filter))
                {
                    passed = true;
                    break;
                }

            if (!passed)
                return;

            StringBuilder logBuf = new StringBuilder();
            //Console.WriteLine("Reading from {0}", fileName);
            string[] lines = File.ReadAllLines(fileName);
            for (int i = 0; i < lines.Length; i++)
            {
                string newLine = lines[i].Replace(source, dest);
                if (lines[i] != newLine)
                {
                    logBuf.Append(String.Format("File: {0}", fileName));
                    logBuf.Append(String.Format("   -- {0}", lines[i]));
                    logBuf.Append(String.Format("   ++ {0}", newLine));
                    logBuf.Append(Environment.NewLine);

                    lines[i] = newLine;
                }

            }

            File.WriteAllLines(fileName, lines);
            //Console.WriteLine("Writing to {0}", fileName);

            lock (syncObj)
            {
                using (StreamWriter logFile = File.AppendText(@"D:\test\log.txt"))
                {
                    logFile.Write(logBuf.ToString());
                }
            }

        }
    }
}
