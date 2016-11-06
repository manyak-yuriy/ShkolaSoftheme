using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public class Dictionary<TKey, TValue>
    {
        private class Node<TKey, TValue>
        {
            public TKey Key { get; }
            public TValue Value { get; set; }
            public Node<TKey, TValue> Next { get; set; }

            public Node(TKey key, TValue value, Node<TKey, TValue> node)
            {
                Key = key;
                Value = value;
                Next = node;
            }
        }

        private Node<TKey, TValue>[] _hashTable;
        private int _rowCnt;

        public int Count { get; private set; }

        private const double LoadRatio = 0.75;
        private const int ExpRatio = 2;
        private const int InitRowCnt = 37;

        public Dictionary()
        {
            _rowCnt = InitRowCnt;
            Count = 0;
            _hashTable = new Node<TKey, TValue>[_rowCnt];

            for (int i = 0; i < _rowCnt; i++)
                _hashTable[i] = new Node<TKey, TValue>(default(TKey), default(TValue), null);   // dummy node for a linked list
        }

        private void CheckSaturation()
        {
            if (_rowCnt > LoadRatio * Count)
                return;

            List<TKey> keys = new List<TKey>();
            List<TValue> values = new List<TValue>();

            for (int i = 0; i < _rowCnt; i++)
            {
                Node<TKey, TValue> node = _hashTable[i].Next;
                while (node != null)
                {
                    keys.Add(node.Key);
                    values.Add(node.Value);
                    node = node.Next;
                }
            }

            _rowCnt *= ExpRatio;
            _hashTable = new Node<TKey, TValue>[_rowCnt];

            for (int i = 0; i < _rowCnt; i++)
                _hashTable[i] = new Node<TKey, TValue>(default(TKey), default(TValue), null);

            for (int i = 0; i < keys.Count; i++)
                this.Add(keys[i], values[i]);
        }

        public TValue this[TKey key]
        {
            get { return GetValue(key); }
            set
            {
                Node<TKey, TValue> prevNode = FindPrevious(key);
                if (prevNode == null)
                    Add(key, value);
                else
                    prevNode.Next.Value = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            CheckSaturation();
            int row = Math.Abs(key.GetHashCode() % _rowCnt);

            Node<TKey, TValue> node = new Node<TKey, TValue>(key, value, _hashTable[row].Next);
            _hashTable[row].Next = node;
            Count++;
        }

        private Node<TKey, TValue> FindPrevious(TKey key)
        {
            int row = Math.Abs(key.GetHashCode() % _rowCnt);

            Node<TKey, TValue> node = _hashTable[row];

            while (node.Next != null)
            {
                if (node.Next.Key.Equals(key))
                    return node;
                node = node.Next;
            }

            return null;
        }

        public bool Contains(TKey key)
        {
            return FindPrevious(key) != null;
        }

        public TValue GetValue(TKey key)
        {
            Node<TKey, TValue> prevNode = FindPrevious(key);
            if (prevNode == null)
                throw new ArgumentException(string.Format("Dictionary does no contain the key {0}", key));
            return prevNode.Next.Value;
        }

        public bool Delete(TKey key)
        {
            Node<TKey, TValue> prevNode = FindPrevious(key);
            if (prevNode == null)
                return false;

            prevNode.Next = prevNode.Next.Next;
            Count--;
            return true;
        }

        public void Clear()
        {
            Count = 0;
            for (int i = 0; i < _rowCnt; i++)
                _hashTable[i] = null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                dict["Yuriy"] = "5";
                dict["Andrew"] = "4";
                dict["Peter"] = "3";
                dict["Abraham"] = "2";

                Console.WriteLine("{0} {1}", dict["Abraham"], dict.Count);   // 2 5

                dict["Abraham"] = "5";

                Console.WriteLine("{0} {1}", dict["Abraham"], dict.Count);   // 2 5


            }
            catch (Exception exc)
            {
                Console.WriteLine("{0}", exc.Message);
            }
            finally
            {
                Console.ReadKey();
            }
            
        }
    }
}
