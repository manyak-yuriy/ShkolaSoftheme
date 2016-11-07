using System;
using System.CodeDom;
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

        public delegate void NotificationHandler(int toNumber, OperatorNotification opNotif);

        public event SmsHandler NewSms;
        public event CallHandler NewCall;
        public event NotificationHandler NewNotif;

        public MobileOperator(decimal smsCost, decimal callCost)
        {
            SmsCost = smsCost;
            CallCost = callCost;
        }
        public decimal SmsCost { get; private set; }
        public decimal CallCost { get; private set; }

        public void BroadCastNotification(int toNumber, OperatorNotification opNotif)
        {
            if (NewNotif != null)
                NewNotif.Invoke(toNumber, opNotif);
        }
         
        public void BroadCastCall(int fromNumber, int toNumber, ref decimal balance)
        {
            balance -= CallCost;
            if (balance < 0)
                BroadCastNotification(fromNumber, new OperatorNotification("Negative balance"));
            if (NewCall != null)
                NewCall.Invoke(fromNumber, toNumber);
        }

        public void BroadCastSms(int fromNumber, int toNumber, string text, ref decimal balance)
        {
            balance -= SmsCost;
            if (balance < 0)
                BroadCastNotification(fromNumber, new OperatorNotification("Negative balance"));
            if (NewSms != null)
                NewSms.Invoke(fromNumber, toNumber, text);
        }
    }

    public class OperatorMessage: Attribute
    {
        public enum msgType
        {
            Info,
            Warn
        }

        public msgType Type { get; set; }

        public OperatorMessage(msgType type)
        {
            Type = type;
        }
    }

    [OperatorMessage(OperatorMessage.msgType.Warn)]
    public class OperatorNotification
    {
        public string Message { get; set; }

        public OperatorNotification(string message)
        {
            Message = message;
        }
    }

    class MobileAccount
    {
        public MobileOperator MobOp { get; }
        public int Number { get; }

        public decimal Balance { get; set; }

        public MobileAccount(MobileOperator mo, int number, decimal balance)
        {
            MobOp = mo;
            Number = number;
            Balance = balance;
        }
        public void SendSms(int toNumber, string text)
        {
            decimal currBal = Balance;
            MobOp.BroadCastSms(Number, toNumber, text, ref currBal);
            Balance = currBal;
        }

        public void MakeCall(int toNumber)
        {
            decimal currBal = Balance;
            MobOp.BroadCastCall(Number, toNumber, ref currBal);

            Balance = currBal;
        }

        public void HandleSms(int fromNumber, int toNumber, string text)
        {
            // Authorization can be much more complicated
            if (toNumber != Number)
                return;
            
            Console.WriteLine("{0} received sms from {1}: \n {2}\n", toNumber, fromNumber, text);
        }

        public void HandleCall(int fromNumber, int toNumber)
        {
            // Authorization can be much more complicated
            if (toNumber != Number)
                return;

            Console.WriteLine("{0} received a call from {1}: \n", toNumber, fromNumber);
        }

        public void HandleNotification(int toNumber, OperatorNotification opNotif)
        {
            // Authorization can be much more complicated
            if (toNumber != Number)
                return;

            OperatorMessage[] MyAttributes =
                                 (OperatorMessage[])Attribute.GetCustomAttributes(opNotif.GetType(), typeof(OperatorMessage));

            switch (MyAttributes[0].Type)
            {
                case OperatorMessage.msgType.Info:
                {
                    // Do nothing
                    break;
                }
                case OperatorMessage.msgType.Warn:
                {
                    ConsoleColor old = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(String.Format("Account with number {0} received the following notification: {1}", toNumber, opNotif.Message));
                    Console.ForegroundColor = old;
                    break;
                }
            }
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MobileOperator mo = new MobileOperator(1, 2);

            MobileAccount acc1 = new MobileAccount(mo, 123, 3);
            mo.NewCall += acc1.HandleCall;
            mo.NewSms += acc1.HandleSms;
            mo.NewNotif += acc1.HandleNotification;

            MobileAccount acc2 = new MobileAccount(mo, 345, 5);
            mo.NewCall += acc2.HandleCall;
            mo.NewSms += acc2.HandleSms;
            mo.NewNotif += acc2.HandleNotification;

            acc1.SendSms(345, "hello world");
            acc1.SendSms(343, "hello world");
            acc1.SendSms(21, "hello world");
            acc1.SendSms(432, "hello world");
            acc1.SendSms(432, "hello world");

            // send notification to mobileAccount
            mo.BroadCastNotification(123, new OperatorNotification("You are warned!"));

            Console.ReadKey();
        }
    }
}
