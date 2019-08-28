using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Core;

namespace Horker.MXNet.PowerShell
{
    public static class NDArrayMethods
    {
        public static PSObject ToArray(PSObject self)
        {
            var array = self.BaseObject as NDArray;

            var dtype = array.DType;

            if (dtype == DType.Float64)
                return new PSObject(array.ToArray<double>());
            if (dtype == DType.Float32)
                return new PSObject(array.ToArray<float>());
            if (dtype == DType.Int64)
                return new PSObject(array.ToArray<long>());
            if (dtype == DType.Int32)
                return new PSObject(array.ToArray<int>());
            if (dtype == DType.Int8)
                return new PSObject(array.ToArray<sbyte>());
            if (dtype == DType.UInt8)
                return new PSObject(array.ToArray<byte>());

            throw new ArgumentException($"Type {dtype} cannot be displayed");

        }
    }
}
