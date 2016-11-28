using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceHolderApp
{
    class ResourceHolderBase: IDisposable
    {
        private bool _disposed;

        private UnmanagedResource _uResource;
        private ManagedResource _mResource;
        public ResourceHolderBase(UnmanagedResource uResource, ManagedResource mResource)
        {
            _disposed = false;

            _uResource = uResource;
            _mResource = mResource;
        }

        ~ResourceHolderBase()
        {
            Console.WriteLine("ResourceHolderBase destructor is called.");
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _mResource.Close();
                    Console.WriteLine("Closed managed resource in ResourceHolderBase.");
                }

                _uResource.Close();
                Console.WriteLine("Closed unmanaged resource in ResourceHolderBase.");

                _disposed = true;
            }
            else
                Console.WriteLine("ResourceHolderBase.Dispose(): an attempt to call again!!");
        }
    }
}
