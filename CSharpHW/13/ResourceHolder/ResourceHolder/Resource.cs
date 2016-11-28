using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceHolderApp
{
    class Resource
    {
        public bool IsUsed { get; private set; }

        public Resource()
        {
            IsUsed = true;
        }

        public virtual void Close()
        {
            if (IsUsed)
            {
                IsUsed = false;
                Console.WriteLine("Closing resource...");
            }
            else
                Console.WriteLine("Resource is already closed.");
        }
    }
}
