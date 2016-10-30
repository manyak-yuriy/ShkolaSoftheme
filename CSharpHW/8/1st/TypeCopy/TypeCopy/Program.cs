using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeCopy
{
    class UserRef
    {
        public UserRef(UserRef another)
        {
            FirstName = another.FirstName;
            LastName = another.LastName;
            ID = another.ID;
        }

        public UserRef()
        {

        }
        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public int ID
        {
            get; set;
        }

        public static bool AreEqual(UserRef a, UserRef b)
        {
            return a.FirstName == b.FirstName && a.LastName == b.LastName && a.ID == b.ID;
        }

        public UserRef ShallowCopy()
        {
            return new UserRef(this);
        }
    }

    public struct UserValue
    {
        public UserValue(string firstName, string lastName, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            ID = id;
        }
        public UserValue(UserValue another)
        {
            FirstName = another.FirstName;
            LastName = another.LastName;
            ID = another.ID;
        }
        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public int ID
        {
            get; set;
        }

        public static bool AreEqual(UserValue a, UserValue b)
        {
            return a.FirstName == b.FirstName && a.LastName == b.LastName && a.ID == b.ID;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

        // Ref type
            UserRef ref1 = new UserRef { FirstName = "First1", LastName = "Last1", ID = 1};

            // Copy just the reference for the ref type
            UserRef ref2 = ref1;
            ref2.FirstName = "First changed";
            Console.WriteLine("{0}", UserRef.AreEqual(ref1, ref2)); // true

            ref1 = new UserRef { FirstName = "First1", LastName = "Last1", ID = 1 };
            // Create a shallow copy for the ref type
            ref2 = ref1.ShallowCopy();
            ref2.FirstName = "First changed";
            Console.WriteLine("{0}", UserRef.AreEqual(ref1, ref2)); //false

        // Value type
            UserValue val1 = new UserValue("First", "Last", 1);

            // copy value type
            UserValue val2 = val1;
            val2.LastName = "Changed last name";

            Console.WriteLine("{0}", UserValue.AreEqual(val1, val2)); //false

            Console.ReadKey();
        }
    }
}
