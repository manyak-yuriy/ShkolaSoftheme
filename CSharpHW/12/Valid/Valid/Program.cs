using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valid
{
    public interface IUser
    {
        string Name
        {
            get; set;
        }

        string Password
        {
            get; set;
        }

        string Email
        {
            get; set;
        }

        DateTime LastVisited
        {
            get; set;
        }

        string GetFullInfo();

        bool Equals(object obj);
    }

    public class User: IUser
    {
        public string Name
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public DateTime LastVisited
        {
            get; set;
        }
        public string GetFullInfo()
        {
            return String.Format("User name: {0}, password: {1}, email: {2}\n", Name, Password, Email); 
        }

        public override bool Equals(object obj)
        {
            User user = obj as User;

            if (user == null)
                return false;

            return ((Name == user.Name) || (Email == user.Email)) && Password == user.Password;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public interface IValidator
    {
        void ValidateUser(IUser user);
    }

    public class Validator : IValidator
    {
        private List<IUser> users;

        public Validator()
        {
            users = new List<IUser>();
        }
        public void ValidateUser(IUser user)
        {
            foreach (IUser usr in users)
                if (user.Equals(usr))
                {
                    Console.WriteLine("Success!");
                    Console.WriteLine("Last visited: {0}\n", user.LastVisited);
                    return;
                }
            AddUser(user);
        }

        private void AddUser(IUser user)
        {
            user.LastVisited = DateTime.Now;
            users.Add(user);
            Console.WriteLine("No such user in the database! Added new user to DB");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string name = "";
            string pass = "";
            string email = "";

            IValidator validator = new Validator();

            do
            {
                Console.WriteLine("Input user data:");

                Console.Write("User name: ");
                name = Console.ReadLine();

                Console.Write("User email: ");
                email = Console.ReadLine();

                Console.Write("User pass: ");
                pass = Console.ReadLine();

                validator.ValidateUser(new User(name, email, pass));

            } while (name != "exit" || email != "exit" || pass != "exit");
        }
    }
}
