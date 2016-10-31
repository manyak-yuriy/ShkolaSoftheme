using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotery
{
    public static class Util
    {
        public static int? GetInt(int min, int max)
        {
            Console.Write("Input number from {0} to {1}: ", min, max);
            int result;
            string inp = Console.ReadLine();

            bool success = int.TryParse(inp, out result);

            if (!success || result < min || result > max)
                return null;

            return result;
        }
    }
    class Ticket
    {
        private static int count = 6;

        private static byte minNum = 0;
        private static byte maxNum = 9;

        private readonly byte[] _numbers;

        public static int Count
        {
            get { return count; }
        }

        public Ticket()
        {
            _numbers = new byte[count];
        }

        public byte this[int index]
        {
            get { return _numbers[index]; }
            private set { _numbers[index] = value; }
        }

        public static Ticket getFromConsole()
        {
            Ticket ticket = new Ticket();
            for (int i = 0; i < count; i++)
            {
                int? num = Util.GetInt(minNum, maxNum);
                while (num == null)
                {
                    Console.WriteLine("Incorrect number. Try again: ");
                    num = Util.GetInt(minNum, maxNum);
                }
                ticket[i] = (byte) (num ?? 0);
            }
            return ticket;
        }

        public static Ticket getRandTicket()
        {
            Ticket ticket = new Ticket();
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                ticket[i] = (byte)rand.Next(minNum, maxNum + 1);
            }
            return ticket;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a new ticket: ");
            Ticket ticket = Ticket.getFromConsole();

            Ticket randTicket = Ticket.getRandTicket();

            bool isWinner = true;
            for (int i = 0; i < Ticket.Count; i++)
                if (ticket[i] != randTicket[i])
                {
                    isWinner = false;
                    break;
                }

            if (isWinner)
                Console.WriteLine("You are the winner!");
            else
                Console.WriteLine("You lost.");
            Console.ReadKey();
        }
    }
}
