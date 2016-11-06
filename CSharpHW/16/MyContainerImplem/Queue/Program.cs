using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedList;

namespace Queue
{
    // Using LinkedList structure implementation from task 15
    public class Queue<T> where T : IComparable<T>
    {
        private LinkedList.Node<T> _first;
        private LinkedList.Node<T> _last;

        public Queue()
        {
            _first = _last = null;
        }

        public bool IsEmpty
        {
            get { return _first == null; }
        }

        public Queue<T> EnQueue(T value)
        {
            if (IsEmpty) // queue is empty
            {
                _first = _last = new Node<T>(value);
            }
            else
            {
                _last.InsertRight(value);
                _last = _last.RightNode;
            }

            return this;
        }

        public T DeQueue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The queue is empty!");

            T result = _first.Data;
            Node<T> newFirst = _first.RightNode;

            if (newFirst != null)
            {
                newFirst.DeleteLeft();
                _first = newFirst;
            }
            else
            {
                _first = _last = null;
            }

            return result;
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The queue is empty!");

            return _first.Data;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Queue<int> q = new Queue<int>();

                Console.WriteLine(q.IsEmpty); // true

                q.EnQueue(1).EnQueue(2).EnQueue(3);

                Console.WriteLine(q.Peek()); // 1
                Console.WriteLine(q.DeQueue()); // 1
                Console.WriteLine(q.DeQueue()); // 2

                q.EnQueue(100);

                Console.WriteLine(q.DeQueue()); // 3
                Console.WriteLine(q.DeQueue()); // 100

                Console.WriteLine(q.DeQueue());
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
