using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    public struct ObjectPin : IDisposable
    {
        private GCHandle _gcHandle;

        public ObjectPin(object value)
        {
            _gcHandle = GCHandle.Alloc(value, GCHandleType.Pinned);
        }

        public IntPtr Address => _gcHandle.AddrOfPinnedObject();

        public void Dispose()
        {
            _gcHandle.Free();
        }
    }
}
