using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceHolderApp
{
    class ResourceHolderDerived : ResourceHolderBase
    {
        private bool _disposed;

        private UnmanagedResource _uResourceOwn;
        private ManagedResource _mResourceOwn;

        public ResourceHolderDerived(UnmanagedResource uResource, ManagedResource mResource, UnmanagedResource uResourceOwn, ManagedResource mResourceOwn) : base(uResource, mResource)
        {
            _disposed = false;

            _uResourceOwn = uResourceOwn;
            _mResourceOwn = mResourceOwn;
        }

        ~ResourceHolderDerived()
        {
            Console.WriteLine("ResourceHolderDerived destructor is called.");
            Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _mResourceOwn.Close();
                    Console.WriteLine("Closed managed resource in ResourceHolderDerived.");
                }

                _uResourceOwn.Close();
                Console.WriteLine("Closed unmanaged resource in ResourceHolderDerived.");

                _disposed = true;

                base.Dispose(disposing);
            }
            else
                Console.WriteLine("ResourceHolderDerived.Dispose(): an attempt to call again!");
        }
    }
}
