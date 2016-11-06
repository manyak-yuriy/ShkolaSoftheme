using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContainerImplem
{
    public class Stack<T>
    {
        private class Node<TData>
        {
            public TData Value { get; }
            public Node<TData> Next { get; }

            public Node(TData value, Node<TData> node)
            {
                Value = value;
                Next = node;
            }
        }

        private Node<T> _head;

        public Stack()
        {
            _head = null;
        }

        public Stack<T> Push(T value)
        {
            Node<T> node = new Node<T>(value, _head);
            _head = node;
            return this;
        }

        public T Pop()
        {
            T value = Peek();
            _head = _head.Next;
            return value;
        }

        public T Peek()
        {
            if (_head == null)
                throw new InvalidOperationException("The stack is empty!");

            T value = _head.Value;
            return value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Stack<int> si = new Stack<int>();

                si.Push(1).Push(2).Push(3);

                Console.WriteLine(si.Peek());   //  3
                Console.WriteLine(si.Pop());   //  3
                Console.WriteLine(si.Pop());   //  2
                Console.WriteLine(si.Pop());   //  1
                Console.WriteLine(si.Pop());   //  Exc
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
