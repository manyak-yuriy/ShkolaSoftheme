using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuningAtelier
{
    public class Car
    {
        public enum Color
        {
            Green,
            Yellow,
            Red,
            Black,
            White
        }
        public string Model { get; private set; }
        public Color MainColor { get; set; }
        public int Year { get; private set; }

        public Car(): this("Default model", Color.Black)
        {
        }
        public Car(string model, Color mainColor): this(model, mainColor, DateTime.Now.Year)
        {
        }
        public Car(string model, Color mainColor, int year)
        {
            Model = model;
            MainColor = mainColor;
            Year = year;
        }

        public override string ToString()
        {
            return string.Format("Model: {0}; Color: {1}; Production year: {2} \n\n", Model, MainColor, Year);
        }
    }

    public static class TuningAtelier
    {
        private static Car.Color TuningColor = Car.Color.Red;

        public static void TuneCar(Car car)
        {
            car.MainColor = TuningColor;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Car[] cars = new Car[3] { new Car(), new Car("Ford F-150", Car.Color.Yellow, 1995), new Car()};

            TuningAtelier.TuneCar(cars[0]);

            foreach (Car car in cars)
            {
                Console.WriteLine(car.ToString());
            }
            Console.ReadKey();
        }
    }
}
