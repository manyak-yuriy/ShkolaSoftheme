using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human
{
    class Human
    {
        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public DateTime BirthDate
        {
            get; set;
        }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - BirthDate.Year;
            }
        }

        public override bool Equals(object obj)
        {
            Human another = obj as Human;

            if (object.ReferenceEquals(another, null))
                return false;

            return FirstName == another.FirstName && LastName == another.LastName && BirthDate == another.BirthDate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator==(Human first, Human second)
        {
            return first.Equals(second);
        }

        public static bool operator!=(Human first, Human second)
        {
            return !first.Equals(second);
        }

        public Human(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public Human(string firstName, string lastName): this(firstName, lastName, DateTime.Now)
        {
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0}", new Human("Bob", "Dylan") == new Human("Jimmy", "Page"));   // false

            Console.WriteLine("{0}", new Human("Bill", "Gates") == new Human("Bill", "Gates"));   // true

            Console.ReadKey();
        }
    }
}
