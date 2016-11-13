using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MobOp
{
    class MobileOperator
    {
        public class LogItem
        {
            public DateTime Date { get; set; }

            public int FromNumber;

            public int ToNumber;

            public Tevent EventType;
            public enum Tevent
            {
                Call, Sms
            }
        }

        public List<LogItem> Log = new List<LogItem>();

        public delegate void SmsHandler(int fromNumber, int toNumber, string text);

        public delegate void CallHandler(int fromNumber, int toNumber);

        public event SmsHandler NewSms;
        public event CallHandler NewCall;

        public void BroadCastCall(int fromNumber, int toNumber)
        {
            Log.Add(new LogItem() {FromNumber = fromNumber, ToNumber = toNumber, Date = DateTime.Now, EventType = LogItem.Tevent.Call});
            if (NewCall != null)
                NewCall.Invoke(fromNumber, toNumber);
        }

        public void BroadCastSms(int fromNumber, int toNumber, string text)
        {
            Log.Add(new LogItem() { FromNumber = fromNumber, ToNumber = toNumber, Date = DateTime.Now, EventType = LogItem.Tevent.Sms });
            if (NewSms != null)
                NewSms.Invoke(fromNumber, toNumber, text);
        }
    }

    [Serializable]
    class MobileAccount
    {
        public MobileOperator MobOp { get; }
        public int Number { get; }
        public Dictionary<int, string> AddressBook { get; set; }
        public MobileAccount(MobileOperator mo, int number)
        {
            MobOp = mo;
            Number = number;
            AddressBook = new Dictionary<int, string>();
            AddressBook.Add(911, "Emergency");
            AddressBook.Add(42, "Phone Service");
        }
        public void SendSms(int toNumber, string text)
        {
            MobOp.BroadCastSms(Number, toNumber, text);
        }

        public void MakeCall(int toNumber)
        {
            MobOp.BroadCastCall(Number, toNumber);
        }

        public void HandleSms(int fromNumber, int toNumber, string text)
        {
            // Authorization can be much more complicated
            if (toNumber != Number)
                return;

            string sender = AddressBook.ContainsKey(fromNumber) ? AddressBook[fromNumber] : fromNumber.ToString();

            Console.WriteLine("{0} received sms from {1}: \n {2}\n", toNumber, sender, text);
        }

        public void HandleCall(int fromNumber, int toNumber)
        {
            // Authorization can be much more complicated
            if (toNumber != Number)
                return;

            string sender = AddressBook.ContainsKey(fromNumber) ? AddressBook[fromNumber] : fromNumber.ToString();

            Console.WriteLine("{0} received a call from {1}: \n", toNumber, sender);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MobileOperator mo = new MobileOperator();

            MobileAccount acc1 = new MobileAccount(mo, 123);
            mo.NewCall += acc1.HandleCall;
            mo.NewSms += acc1.HandleSms;

            MobileAccount acc2 = new MobileAccount(mo, 345);
            mo.NewCall += acc2.HandleCall;
            mo.NewSms += acc2.HandleSms;

            acc2.AddressBook.Add(123, "Peter Parker");
            acc2.AddressBook.Add(874, "Alan Turing");
            acc2.AddressBook.Add(748, "Paul Allan");
            acc2.AddressBook.Add(838, "John Doe");
            acc2.AddressBook.Add(883, "Niels Bohr");
            acc2.AddressBook.Add(432, "Albert Einstein");

            acc1.MakeCall(345);
            acc1.MakeCall(432);
            acc2.MakeCall(874);
            acc2.MakeCall(432);
            acc2.MakeCall(432);
            acc1.MakeCall(345);
            acc1.MakeCall(911);
            acc1.MakeCall(345);
            acc1.MakeCall(911);
            acc1.MakeCall(911);
            acc2.MakeCall(911);
            acc2.MakeCall(983);
            acc2.MakeCall(763);

            acc2.SendSms(6543, "Empty1");
            acc2.SendSms(748, "Empty2");
            acc1.SendSms(883, "Empty3");

            var d = 
                from record in acc2.AddressBook
                where record.Value.Length > 9
                orderby record.Key ascending 
                select record;

            Console.WriteLine("Address book filtering:");

            foreach (var rec in d)
            {
                Console.WriteLine("{0} {1}", rec.Key, rec.Value);
            }

            Console.WriteLine("\n5 most called numbers:");

            var result =
                from r in mo.Log
                where r.EventType == MobileOperator.LogItem.Tevent.Call
                group r by r.ToNumber
                into g
                orderby g.Count() descending 
                select new {Num = g.Key, Count = g.Count()};

            foreach (var rec in result)
            {
                Console.WriteLine("Number:  {0}  Count:  {1}  ", rec.Num, rec.Count);
            }

            Console.WriteLine("\n5 most active numbers:");

            var callTable =
                from r in mo.Log
                where r.EventType == MobileOperator.LogItem.Tevent.Call
                group r by r.FromNumber
                into g
                orderby g.Count() descending
                select new { Num = g.Key, CallCount = g.Count() };

            var smsTable =
                from r in mo.Log
                where r.EventType == MobileOperator.LogItem.Tevent.Sms
                group r by r.FromNumber
                into g
                orderby g.Count() descending
                select new { Num = g.Key, SmsCount = g.Count() };

            var rating =
                from c in callTable
                join s in smsTable on c.Num equals s.Num
                select new {Num = s.Num, Rate = c.CallCount * 2 + s.SmsCount};

            rating =
                from r in rating
                orderby r.Rate descending 
                select r;

            foreach (var rec in rating)
            {
                Console.WriteLine("Number:  {0}  Rating:  {1}  ", rec.Num, rec.Rate);
            }

            Console.ReadKey();
        }
    }
}
