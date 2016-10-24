using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigurePrinter
{
    public enum FigureType
    {
        square,
        triangle,
        romb
    }

    public abstract class Figure
    {
        protected int size;
        public Figure(int size)
        {
            this.size = size;
        }
        public abstract void draw();
    }

    public class Square: Figure
    {
        public Square(int size): base(size)
        {

        }

        public override void draw()
        {
            Console.WriteLine("A square with a side of {0}", size);

            for (int i = 0; i < size + 1; i++)
            {
                for (int j = 0; j < size + 1; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
        }
    }

    public class Triangle : Figure
    {
        public Triangle(int size) : base(size)
        {

        }

        public override void draw()
        {
            Console.WriteLine("A triangle with a cathetus of {0}", size);

            for (int i = 0; i < size + 1; i++)
            {
                for (int j = 0; j < i + 1; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
        }
    }

    public class Romb : Figure
    {
        public Romb(int size) : base(size)
        {

        }

        public override void draw()
        {
            Console.WriteLine("A romb with a semidiagonal of {0}", size);

            for (int i = size; i >= 1; i--)
                drawLine(i);
            drawLine(0);
            for (int i = 1; i <= size; i++)
                drawLine(i);
        }

        const char blank = ' ';
        private void drawLine(int numOfBlanks)
        {
            for (int j = 1; j <= numOfBlanks; j++)
                Console.Write(blank);
            for (int j = 1; j <= size - numOfBlanks; j++)
                Console.Write('*');
            Console.Write('*');
            for (int j = 1; j <= size - numOfBlanks; j++)
                Console.Write('*');
            for (int j = 1; j <= numOfBlanks; j++)
                Console.Write(blank);
            Console.WriteLine();
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                FigureType figureType = getFigure("Input figure type: T - triangle, S - square, R - romb: ");

                int size = getFigureSize("Input figure size: ");

                Figure figure = null;

                switch (figureType)
                {
                    case FigureType.square:
                        {
                            figure = new Square(size);
                            break;
                        }
                    case FigureType.triangle:
                        {
                            figure = new Triangle(size);
                            break;
                        }
                    case FigureType.romb:
                        {
                            figure = new Romb(size);
                            break;
                        }
                }

                figure.draw();
                Console.ReadKey();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            Console.ReadKey();
        }
  

        const int minSize = 1;
        const int maxSize = 30;

        private static int getFigureSize(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            string input = Console.ReadLine();
            int size = int.Parse(input);

            if (size > maxSize)
                throw new ArgumentException("The figure is too big to draw!");

            if (size < minSize)
                throw new ArgumentException("The size is too small for a figure of this type!");

            return size;
        }
        private static FigureType getFigure(string message)
        {
            for (;;)
            {
                Console.WriteLine(message);
                char res = Console.ReadKey().KeyChar;
                switch (res)
                {
                    case 'T':
                        return FigureType.triangle;
                    case 'S':
                        return FigureType.square;
                    case 'R':
                        return FigureType.romb; 
                }
                Console.WriteLine("\nInvalid figure. Try again");
            }
            
        }
    }
}
