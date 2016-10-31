using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class Node<TData> where TData: IComparable<TData>
    {

        public TData Data
        {
            get; set;
        }

        public Node<TData> LeftNode
        {
            get; set;
        }

        public Node<TData> RightNode
        {
            get; set;
        }

        public Node(TData data)
        {
            Data = data;
            LeftNode = null;
            RightNode = null;
        }

        public Node<TData> InsertLeft(TData newData) // one node to the left
        {
            Node<TData> newNode = new Node<TData>(newData);
            newNode.LeftNode = LeftNode;
            newNode.RightNode = this;

            
            if (LeftNode != null)
               LeftNode.RightNode = newNode;

            this.LeftNode = newNode;

            return newNode;
        }


        public Node<TData> InsertRight(TData newData) // one node to the left
        {
            Node<TData> newNode = new Node<TData>(newData);
            newNode.RightNode = RightNode;
            newNode.LeftNode = this;


            if (RightNode != null)
                RightNode.LeftNode = newNode;

            this.RightNode = newNode;

            return newNode;
        }
        public void DeleteLeft()  // one node to the left
        {
            var oldLeftNode = LeftNode;
            var newLeftNode = oldLeftNode.LeftNode;

            oldLeftNode.LeftNode = oldLeftNode.RightNode = null;

            if (newLeftNode != null)
                newLeftNode.RightNode = this;

            LeftNode = newLeftNode;
        }

        public void DeleteRight()  // one node to the left
        {
            var oldRightNode = RightNode;
            var newRightNode = oldRightNode.RightNode;

            oldRightNode.RightNode = oldRightNode.LeftNode = null;

            if (newRightNode != null)
                newRightNode.LeftNode = this;

            RightNode = newRightNode;
        }

        public Node<TData> Delete()
        {
            Node<TData> node = null;

            if (LeftNode != null)
            {
                node = LeftNode;
                node.DeleteRight();
                return node;
            }

            if (RightNode != null)
            {
                node = RightNode;
                node.DeleteLeft();
                return node;
            }

            return null;
        }

        public int CountToLeft()   // not inclusive
        {
            if (LeftNode != null)
                return 1 + LeftNode.CountToLeft();
            else
                return 0;
        }

        public int CountToRight()   // not inclusive
        {
            if (RightNode != null)
                return 1 + RightNode.CountToRight();
            else
                return 0;
        }

        public int Count()
        {
            return 1 + CountToLeft() + CountToRight();
        }

        public bool ContainsToLeft(TData data) // not inclusive
        {
  
            if (LeftNode != null)
            {
                if (LeftNode.Data.Equals(data))
                    return true;
                return LeftNode.ContainsToLeft(data);
            }

            return false;
        }

        public bool ContainsToRight(TData data) // not inclusive
        {

            if (RightNode != null)
            {
                if (RightNode.Data.Equals(data))
                    return true;
                return RightNode.ContainsToRight(data);
            }

            return false;
        }

        public bool Contains(TData data)
        {
            return ContainsToLeft(data) || ContainsToRight(data);
        }

        public TData[] ToArrayToLeft()  // not inclusive
        {
            int count = CountToLeft();

            TData[] res = new TData[count];

            Node<TData> node = this.LeftNode;
            int i = 0;

            while (node != null)
            {
                res[i] = node.Data;
                i++;
                node = node.LeftNode;
            }

            return res;
        }

        public TData[] ToArrayToRight()  // not inclusive
        {
            int count = CountToRight();

            TData[] res = new TData[count];

            Node<TData> node = this.RightNode;
            int i = 0;

            while (node != null)
            {
                res[i] = node.Data;
                i++;
                node = node.RightNode;
            }

            return res;
        }

        public TData[] ToArray()
        {
            TData[] res1 = ToArrayToLeft();
            TData[] res2 = ToArrayToRight();

            TData[] res = new TData[res1.Length + res2.Length + 1];

            for (int i = 0; i < res1.Length; i++)
                res[i] = res1[i];

            res[res1.Length] = Data;

            for (int j = 0; j < res2.Length; j++)
                res[res1.Length + 1 + j] = res2[j];

            return res;
        }

        public void DeleteToLeftOnValue(TData data)   // not inclusive
        {
            if (LeftNode.Data.Equals(data))
                DeleteLeft();
            else
                LeftNode.DeleteToLeftOnValue(data);
        }

        public void DeleteToRightOnValue(TData data)   // not inclusive
        {
            if (RightNode.Data.Equals(data))
                DeleteRight();
            else
                RightNode.DeleteToRightOnValue(data);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Node<string> zeroNode = new Node<string>("zero");
            
            zeroNode.InsertLeft("second");
            zeroNode.InsertLeft("third");
            zeroNode.InsertLeft("fourth");

            int count = zeroNode.CountToLeft();  // 3

            Console.WriteLine("Count before: {0}", count);

            zeroNode.DeleteToLeftOnValue("second");

            count = zeroNode.CountToLeft(); // 2

            Console.WriteLine("Count after: {0}", count);

            string[] elements = zeroNode.ToArrayToLeft();

            Console.WriteLine("Elements:");

            foreach (var e in elements)
            {
                Console.WriteLine("{0} ", e);
            }

            Console.ReadKey();
        }
    }
}
