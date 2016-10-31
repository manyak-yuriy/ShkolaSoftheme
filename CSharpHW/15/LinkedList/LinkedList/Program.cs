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

        public void DeleteLeft()  // one node to the left
        {
            var oldLeftNode = LeftNode;
            var newLeftNode = oldLeftNode.LeftNode;

            oldLeftNode.LeftNode = oldLeftNode.RightNode = null;

            if (newLeftNode != null)
                newLeftNode.RightNode = this;

            LeftNode = newLeftNode;
        }

        public int CountToLeft()   // not inclusive
        {
            if (LeftNode != null)
                return 1 + LeftNode.CountToLeft();
            else
                return 0;
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

        public void DeleteToLeftOnValue(TData data)   // not inclusive
        {
            if (LeftNode.Data.Equals(data))
                DeleteLeft();
            else
                LeftNode.DeleteToLeftOnValue(data);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Node<string> zeroNode = new Node<string>("zero");
            var leftMost = zeroNode.InsertLeft("first");
            leftMost = zeroNode.InsertLeft("second");
            leftMost = zeroNode.InsertLeft("third");
            leftMost = zeroNode.InsertLeft("fourth");

            int count = zeroNode.CountToLeft();  // 4

            zeroNode.DeleteToLeftOnValue("second");

            count = zeroNode.CountToLeft(); // 3

            string[] elem = zeroNode.ToArrayToLeft();

            Console.ReadKey();
        }
    }
}
