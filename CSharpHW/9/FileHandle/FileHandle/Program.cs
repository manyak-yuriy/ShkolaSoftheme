using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileHandle
{
    public struct FileHandle
    {
        [Flags]
        public enum FileAccess
        {
            Read = 1,
            Write = 2
        }
        public int FileSize
        {
            get; set;
        }
        public string FileName
        {
            get; set;
        }

        public string FilePath
        {
            get; set;
        }

        public FileAccess FileAccessEnum
        {
            get; set;
        }

        private FileHandle(string fileName, string filePath, FileAccess fileAccess)
        {
            FileName = fileName;
            FilePath = filePath;
            FileAccessEnum = fileAccess;
            FileSize = 42;
        }

        public static FileHandle OpenForRead(string fileName, string filePath)
        {
            return new FileHandle(fileName, filePath, FileAccess.Read);
        }

        public static FileHandle OpenForWrite(string fileName, string filePath)
        {
            return new FileHandle(fileName, filePath, FileAccess.Write);
        }

        public static FileHandle OpenFile(string fileName, string filePath, FileAccess fileAccess)
        {
            return new FileHandle(fileName, filePath, fileAccess);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // open for read
            FileHandle fileRead = FileHandle.OpenForRead("file.txt", "C://Docs/");

            // open for write
            FileHandle fileWrite = FileHandle.OpenForWrite("file.txt", "C://Docs/");

            // open for read and write
            FileHandle file = FileHandle.OpenFile("file.txt", "C://Docs/", FileHandle.FileAccess.Read | FileHandle.FileAccess.Write);

            Console.ReadKey();
        }
    }
}
