using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    internal static class IntPtrConverter
    {
        public static T[] ToArray<T>(IntPtr ptr, int count)
            where T: unmanaged
        {
            unsafe
            {
                var array = new T[count];
                var p = (T*)ptr;
                for (var i = 0; i < count; i++)
                    array[i] = p[i];

                return array;
            }
        }
    }
}
