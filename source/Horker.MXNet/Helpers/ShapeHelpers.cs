using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    public static class ShapeHelpers
    {
        public static IEnumerable<int[]> Enumerate(int[] shape)
        {
            var ndims = shape.Length;
            var dims = new int[ndims];
            while (dims[0] < shape[0])
            {
                yield return dims;
                ++dims[ndims - 1];
                for (var i = ndims - 1; i > 0; --i)
                {
                    if (dims[i] < shape[i])
                        break;
                    dims[i] = 0;
                    ++dims[i - 1];
                }
            }
        }
    }
}
