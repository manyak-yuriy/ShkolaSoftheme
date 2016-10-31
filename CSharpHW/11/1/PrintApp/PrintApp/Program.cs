using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintApp
{
    public class Photo
    {
        public string Data
        {
            get; set;
        }
        public Photo(string data)
        {
            Data = data;
        }

        public Photo()
        {
            Data = "--------------" + "\n" +
                   "|--Photo-----|" + "\n" +
                   "|------------|" + "\n" +
                   "|----*.png---|" + "\n" +
                   "--------------" + "\n";
        }

        public override string ToString()
        {
            return Data;
        }
    }

    public class Printer
    {
        public virtual void Print(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("Printer.Print() was just called");
        }
    }

    public class ColourPrinter : Printer
    {
        public override void Print(string msg)
        {
            base.Print(msg);
            Console.WriteLine("ColourPrinter.Print() was just called");
        }

        public void Print(string msg, ConsoleColor color)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = defaultColor;
        }
    }

    public class PhotoPrinter : Printer
    {
        public override void Print(string msg)
        {
            base.Print(msg);
            Console.WriteLine("PhotoPrinter.Print() was just called");
        }

        public void Print(Photo photo)
        {
            Console.WriteLine(photo);
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            Printer colPrinter = new ColourPrinter();
            Printer photoPrinter = new PhotoPrinter();

            // virtual methods are called
            printer.Print("message");
            colPrinter.Print("message");
            photoPrinter.Print("message");

            // specialised methods

            (colPrinter as ColourPrinter).Print("message", ConsoleColor.Blue);

            (photoPrinter as PhotoPrinter).Print(new Photo());

            Console.ReadKey();
        }
    }
}
