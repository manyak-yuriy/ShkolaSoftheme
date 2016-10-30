using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConstruct
{
    class Engine
    {
        public enum EngineType
        {
            Gasoline,
            Electric,
            Diesel
        }

        public EngineType Type
        {
            get; private set;
        }

        public Engine(EngineType engineType)
        {
            Type = engineType;
        }
    }

    class Color
    {
        public enum ColorValue
        {
            Red,
            Green,
            Blue,
            Yellow,
            Black,
            White
        }

        public ColorValue Value
        {
            get; private set;
        }

        public Color(ColorValue colorValue)
        {
            Value = colorValue; 
        }
    }

    class Transmission
    {
        public enum TransmissionType
        {
            Manual,
            Automatic,
            Semiautomatic
        }

        public TransmissionType Type
        {
            get; set;
        }

        public Transmission(TransmissionType transmissionType)
        {
            Type = transmissionType;
        }
    }

    static class CarConstructor
    {
        public class Car
        {
            public Transmission Transmission
            {
                get; set;
            }

            public Color Color
            {
                get; set;
            }

            public Engine Engine
            {
                get; set;
            }

            public override string ToString()
            {
                return String.Format("The car is {0}, has {1} transmission, {2} engine\n", Color.Value, Transmission.Type, Engine.Type);
            }

        }
        public static Car Construct(Transmission transmission, Color color, Engine engine)
        {
            return new Car { Transmission = transmission, Color = color, Engine = engine};
        }
        public static void ReConstruct(Car car, Engine newEngine)
        {
            car.Engine = newEngine;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CarConstructor.Car car = CarConstructor.Construct(new Transmission(Transmission.TransmissionType.Automatic),
                                                              new Color(Color.ColorValue.Green),
                                                              new Engine(Engine.EngineType.Gasoline));
            // Replace the engine with a diesel engine
            CarConstructor.ReConstruct(car, new Engine(Engine.EngineType.Diesel));

            Console.WriteLine(car);
            Console.ReadKey();
        }
    }
}
