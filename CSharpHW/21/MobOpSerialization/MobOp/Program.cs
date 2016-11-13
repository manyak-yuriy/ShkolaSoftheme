using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Xml;
using ProtoBuf;
using ProtoBuf.Meta;

namespace MobOp
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class MobileOperator
    {
        [DataContract]
        [Serializable]
        [ProtoContract]
        public class LogItem
        {
            [DataMember]
            [ProtoMember(1)]
            public DateTime Date { get; set; }

            [DataMember]
            [ProtoMember(2)]
            public int FromNumber;

            [DataMember]
            [ProtoMember(3)]
            public int ToNumber;

            [DataMember]
            [ProtoMember(4)]
            public Tevent EventType;
            public enum Tevent
            {
                Call, Sms
            }
        }

        [DataMember]
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
    [DataContract]
    [ProtoContract]
    public class MobileAccount
    {
        [DataMember]
        [ProtoMember(1)]
        public MobileOperator MobOp { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public int Number { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public Dictionary<int, string> AddressBook { get; set; }
        public MobileAccount(MobileOperator mo, int number)
        {
            MobOp = mo;
            Number = number;
            AddressBook = new Dictionary<int, string>();
            AddressBook.Add(911, "Emergency");
            AddressBook.Add(42, "Phone Service");
        }

        public MobileAccount()
        {
            
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

            //Console.WriteLine("{0} received sms from {1}: \n {2}\n", toNumber, sender, text);
        }

        public void HandleCall(int fromNumber, int toNumber)
        {
            // Authorization can be much more complicated
            if (toNumber != Number)
                return;

            string sender = AddressBook.ContainsKey(fromNumber) ? AddressBook[fromNumber] : fromNumber.ToString();

            //Console.WriteLine("{0} received a call from {1}: \n", toNumber, sender);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MobileOperator mo = new MobileOperator();

            // generate a list of random accounts
            List<MobileAccount> accounts = new List<MobileAccount>();

            const int numOfAcc = 100;
            const int maxNumberValue = 1000000;

            const int maxAddrBookSize = 30;

            const int maxPhoneCallNum = 20;

            Random r = new Random();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (int accNum = 0; accNum < numOfAcc; accNum++)
            {
                MobileAccount newMobAcc = new MobileAccount(mo, r.Next(0, maxNumberValue));

                for (int i = 0; i < r.Next(maxAddrBookSize); i++)
                    newMobAcc.AddressBook.Add(r.Next(maxNumberValue), "DefName" + r.Next(maxNumberValue).ToString());

                for (int j = 0; j < maxPhoneCallNum; j++)
                    newMobAcc.MakeCall(r.Next(maxNumberValue));

                accounts.Add(newMobAcc);
            }

            watch.Stop();
            Console.WriteLine("Initialization: {0} ms", watch.ElapsedMilliseconds);

            long jsonTime = 0;
            long xmlTime = 0;
            long binTime = 0;
            long protoTime = 0;
                 
            for (int iter = 0; iter < 5; iter++)
            {
                Console.WriteLine("Iteration № {0}",iter + 1);

                watch = System.Diagnostics.Stopwatch.StartNew();
                // JSON serialization

                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<MobileAccount>));
                using (FileStream fs = new FileStream("accounts.json", FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, accounts);
                }

                watch.Stop();
                Console.WriteLine("    JSON serialization: {0} ms", watch.ElapsedMilliseconds);
                jsonTime += watch.ElapsedMilliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                // XML serialization

                var serializer = new DataContractSerializer(typeof(List<MobileAccount>));
                using (FileStream fs = new FileStream("accounts.xml", FileMode.Create))
                {
                    serializer.WriteObject(fs, accounts);
                }


                watch.Stop();
                Console.WriteLine("    XML serialization: {0} ms", watch.ElapsedMilliseconds);
                xmlTime += watch.ElapsedMilliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                // Binary serialization

                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream("accounts.bin", FileMode.Create))
                {
                    formatter.Serialize(fs, accounts);
                }

                watch.Stop();
                Console.WriteLine("    Binary serialization: {0} ms", watch.ElapsedMilliseconds);
                binTime += watch.ElapsedMilliseconds;

                watch = System.Diagnostics.Stopwatch.StartNew();
                // ProtoBuf

                using (FileStream fs = new FileStream("accounts.proto", FileMode.Create))
                {
                    Serializer.Serialize(fs, accounts);
                }

                watch.Stop();
                Console.WriteLine("    ProtoBuf serialization: {0} ms", watch.ElapsedMilliseconds);
                protoTime += watch.ElapsedMilliseconds;

                Console.WriteLine();
            }

            Console.WriteLine("Total time: \n JSON: {0} ms \n XML: {1} ms \n binary: {2} ms \n proto: {3} ms", 
                                              jsonTime, 
                                              xmlTime, 
                                              binTime,
                                              protoTime);
            

            Console.ReadKey();
        }
    }
}
