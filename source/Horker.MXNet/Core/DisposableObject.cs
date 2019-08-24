using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    /// <summary>
    /// This class implements the Disposable pattern.
    /// </summary>
    public abstract class DisposableObject : IDisposable
    {
        protected bool _disposed = false;

        /// <summary>
        /// Returns whether this object is diposed.
        /// </summary>
        public bool Disposed => _disposed;

        /// <summary>
        /// Disposes managed objects.
        /// </summary>
        protected virtual void DisposeManagedObjects()
        {
        }

        /// <summary>
        /// Disposes unmanaged resources.
        /// </summary>
        protected abstract void DisposeUnmanagedResource();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    DisposeManagedObjects();

                DisposeUnmanagedResource();

                _disposed = true;
            }
        }

        ~DisposableObject()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose this object and release the unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
