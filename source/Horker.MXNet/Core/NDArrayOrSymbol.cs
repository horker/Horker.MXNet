using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    public class NDArrayOrSymbol : DisposableObject
    {
        private IntPtr _handle = IntPtr.Zero;

        public IntPtr Handle
        {
            get
            {
                if (Disposed)
                    throw new ObjectDisposedException("Handle");

                return _handle;
            }

            protected set => _handle = value;
        }

        protected override void DisposeUnmanagedResource()
        {
            CApi.MXNDArrayFree(_handle);
        }
    }
}
