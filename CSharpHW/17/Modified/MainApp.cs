using System;

namespace _02_Iterator
{
    
    class MainApp
    {
        static void Main()
        {
            // Build a collection
            Collection<string> collection = new Collection<string>();

            collection[0] = "Item 0";
            collection[1] = "Item 1";
            collection[2] = "Item 2";
            collection[3] = "Item 3";
            collection[3] = "Item 4";
            collection[3] = "Item 5";
            collection[3] = "Item 6";

            // Create iterator
            Iterator<string> iterator = new Iterator<string>(collection);

            // Skip every other item
            iterator.Step = 2;

            Console.WriteLine("Iterating over collection:");

            for (string item = iterator.First(); !iterator.IsDone; item = iterator.Next())
            {
                Console.WriteLine(item);
            }

            // Wait for user
            Console.ReadKey();
        }
    }
}