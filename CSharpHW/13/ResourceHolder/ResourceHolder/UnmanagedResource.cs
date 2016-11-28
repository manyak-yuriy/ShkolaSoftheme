using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceHolderApp
{
    class UnmanagedResource : Resource
    {
        public UnmanagedResource() : base()
        {

        }
        public override void Close()
        {
            base.Close();
            Console.WriteLine("Closing unmanaged resource...");
        }
    }
}
