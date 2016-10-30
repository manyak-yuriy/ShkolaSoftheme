using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = y;
            Y = y;
        }

        public Point() : this(0, 0)
        {
        }

        public Point(Point p) : this(p.X, p.Y)
        {
        }
    }

    public class ShapeDescriptor
    {
        public enum ShapeType
        {
            Point,
            Line,
            Triangle,
            Polygon
        }

        public ShapeType Type
        {
            get; private set;
        }

        public Point[] Points
        {
            get; private set;
        }

        public ShapeDescriptor(params Point[] points)
        {
            int len = points.Length;
            switch (len)
            {
                case 0:
                    {
                        throw new ArgumentException("Shape can be defined by at least one point!");
                    }
                case 1:
                    {
                        Type = ShapeType.Point;
                        break;
                    }
                case 2:
                    {
                        Type = ShapeType.Line;
                        break;
                    }
                case 3:
                    {
                        Type = ShapeType.Triangle;
                        break;
                    }
                default:
                    {
                        Type = ShapeType.Polygon;
                        break;
                    }
            }
            Points = new Point[len];

            for (int i = 0; i < len; i++)
                Points[i] = new Point(points[i]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ShapeDescriptor polygon = new ShapeDescriptor(new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(1, 0));
                Console.WriteLine("{0}", polygon.Type);

                ShapeDescriptor triangle = new ShapeDescriptor(new Point(0, 0), new Point(0, 1), new Point(1, 1));
                Console.WriteLine("{0}", triangle.Type);

                ShapeDescriptor empty = new ShapeDescriptor();
                Console.WriteLine("{0}", empty.Type);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
