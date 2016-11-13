using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;
using System.IO.Compression;


namespace MobOp
{
    [DataContract]
    [Serializable]
    public class MobileOperator
    {
        [DataContract]
        [Serializable]
        public class LogItem
        {
            [DataMember]
            public DateTime Date { get; set; }

            [DataMember]
            public int FromNumber;

            [DataMember]
            public int ToNumber;

            [DataMember]
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
    public class MobileAccount
    {
        [DataMember]
        public MobileOperator MobOp { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
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
        // generates a list of random accounts
        static List<MobileAccount> generateAccounts()
        {
            MobileOperator mo = new MobileOperator();

            List<MobileAccount> accounts = new List<MobileAccount>();

            const int numOfAcc = 100;
            const int maxNumberValue = 1000000;

            const int maxAddrBookSize = 30;

            const int maxPhoneCallNum = 20;

            Random r = new Random();

            for (int accNum = 0; accNum < numOfAcc; accNum++)
            {
                MobileAccount newMobAcc = new MobileAccount(mo, r.Next(0, maxNumberValue));

                for (int i = 0; i < r.Next(maxAddrBookSize); i++)
                    newMobAcc.AddressBook.Add(r.Next(maxNumberValue), "DefName" + r.Next(maxNumberValue).ToString());

                for (int j = 0; j < maxPhoneCallNum; j++)
                    newMobAcc.MakeCall(r.Next(maxNumberValue));

                accounts.Add(newMobAcc);
            }

            return accounts;
        }

        static void saveToFile(string fileName, List<MobileAccount> accounts, bool compress)
        {
            var serializer = new DataContractSerializer(typeof(List<MobileAccount>));
            string path = fileName + ".xml";

            if (compress)
            {
                Directory.CreateDirectory(@"temp\");
                path = @"temp\" + path;
            }
                

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.WriteObject(fs, accounts);
            }

            if (compress)
            {
                ZipFile.CreateFromDirectory("temp", fileName + ".zip");
                Directory.Delete(@"temp\", true);
            }
                
        }

        static List<MobileAccount> restoreFromFile(string fileName, bool decompress)
        {
            List<MobileAccount> accRestored;

            string path = fileName + ".xml";

            if (decompress)
            {
                path = fileName + ".zip";
                ZipFile.ExtractToDirectory(path, @"tempext\");
                path = @"tempext\" + fileName + ".xml";
            }

            var serializer = new DataContractSerializer(typeof(List<MobileAccount>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                fs.Position = 0;
                accRestored = (List<MobileAccount>)serializer.ReadObject(fs);
            }

            if (decompress)
                Directory.Delete(@"tempext\", true);

            return accRestored;
        }

        public const string FileName = "backup";
        
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();

            List<MobileAccount> accounts = generateAccounts();

            Console.WriteLine("Initialization: {0} ms", watch.ElapsedMilliseconds);

            // XML serialization
            watch.Restart();

            saveToFile(FileName, accounts, true);

            Console.WriteLine("Serialization to xml: {0} ms", watch.ElapsedMilliseconds);

            watch.Restart();

            List<MobileAccount> accRestored = restoreFromFile(FileName, true);

            Console.WriteLine("Deserialization from xml: {0} ms", watch.ElapsedMilliseconds);
  
            Console.ReadKey();
        }
    }
}
