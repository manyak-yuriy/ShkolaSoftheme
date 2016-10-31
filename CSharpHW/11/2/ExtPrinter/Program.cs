using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintApp;

namespace ExtPrinter
{

    public static class PrinterExt
    {
        public static void PrintAll(this PrintApp.Printer printer, params string[] msgArr)
        {
            for (int i = 0; i < msgArr.Length; i++)
                printer.Print(msgArr[i]);
        }

        public static void PrintAll(this PrintApp.ColourPrinter printer, params Tuple<string, ConsoleColor>[] msgColArr)
        {
            for (int i = 0; i < msgColArr.Length; i++)
                printer.Print(msgColArr[i].Item1, msgColArr[i].Item2);
        }

        public static void PrintAll(this PrintApp.PhotoPrinter printer, params PrintApp.Photo[] photoArr)
        {
            for (int i = 0; i < photoArr.Length; i++)
                printer.Print(photoArr[i]);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            printer.PrintAll("This", "is", "a", "long", "message");

            Console.WriteLine();

            ColourPrinter colPrinter = new ColourPrinter();
            colPrinter.PrintAll(new Tuple<string, ConsoleColor>("Colourful", ConsoleColor.DarkBlue), new Tuple<string, ConsoleColor>("message", ConsoleColor.Red));

            Console.WriteLine();

            PhotoPrinter photoPr = new PhotoPrinter();
            photoPr.PrintAll(new Photo(), new Photo("Custom photo"), new Photo());

            Console.ReadKey();
        }
    }
}
