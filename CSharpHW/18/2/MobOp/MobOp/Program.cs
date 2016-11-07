using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobOp
{
    class MobileOperator
    {
        public delegate void SmsHandler(int fromNumber, int toNumber, string text);

        public delegate void CallHandler(int fromNumber, int toNumber);

        public event SmsHandler NewSms;
        public event CallHandler NewCall;

        public void BroadCastCall(int fromNumber, int toNumber)
        {
            if (NewCall != null)
                NewCall.Invoke(fromNumber, toNumber);
        }

        public void BroadCastSms(int fromNumber, int toNumber, string text)
        {
            if (NewSms != null)
                NewSms.Invoke(fromNumber, toNumber, text);
        }
    }

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

            acc1.SendSms(345, "Hello Peter!");

            Console.ReadKey();
        }
    }
}
