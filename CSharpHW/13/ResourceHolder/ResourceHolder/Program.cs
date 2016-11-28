using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceHolderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ResourceHolderDerived rh = new ResourceHolderDerived(new UnmanagedResource(), new ManagedResource(), 
                                                                 new UnmanagedResource(), new ManagedResource());

            rh.Dispose();

            // Calling dispose twice

            Console.WriteLine();

            rh.Dispose();

            Console.WriteLine();

            // Using finalizer

            ResourceHolderDerived rhFinalized = new ResourceHolderDerived(new UnmanagedResource(), new ManagedResource(),
                                                                 new UnmanagedResource(), new ManagedResource());

            GC.Collect();
            
            Console.ReadKey();
        }
    }
}
