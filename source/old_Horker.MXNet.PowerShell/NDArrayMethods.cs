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

        public static PSObject To2DArray(PSObject self)
        {
            var array = self.BaseObject as NDArray;

            var dtype = array.DType;

            if (dtype == DType.Float64)
                return new PSObject(array.To2DArray<double>());
            if (dtype == DType.Float32)
                return new PSObject(array.To2DArray<float>());
            if (dtype == DType.Int64)
                return new PSObject(array.To2DArray<long>());
            if (dtype == DType.Int32)
                return new PSObject(array.To2DArray<int>());
            if (dtype == DType.Int8)
                return new PSObject(array.To2DArray<sbyte>());
            if (dtype == DType.UInt8)
                return new PSObject(array.To2DArray<byte>());

            throw new ArgumentException($"Type {dtype} cannot be displayed");
        }

        public static PSObject To3DArray(PSObject self)
        {
            var array = self.BaseObject as NDArray;

            var dtype = array.DType;

            if (dtype == DType.Float64)
                return new PSObject(array.To3DArray<double>());
            if (dtype == DType.Float32)
                return new PSObject(array.To3DArray<float>());
            if (dtype == DType.Int64)
                return new PSObject(array.To3DArray<long>());
            if (dtype == DType.Int32)
                return new PSObject(array.To3DArray<int>());
            if (dtype == DType.Int8)
                return new PSObject(array.To3DArray<sbyte>());
            if (dtype == DType.UInt8)
                return new PSObject(array.To3DArray<byte>());

            throw new ArgumentException($"Type {dtype} cannot be displayed");
        }

        public static PSObject To4DArray(PSObject self)
        {
            var array = self.BaseObject as NDArray;

            var dtype = array.DType;

            if (dtype == DType.Float64)
                return new PSObject(array.To4DArray<double>());
            if (dtype == DType.Float32)
                return new PSObject(array.To4DArray<float>());
            if (dtype == DType.Int64)
                return new PSObject(array.To4DArray<long>());
            if (dtype == DType.Int32)
                return new PSObject(array.To4DArray<int>());
            if (dtype == DType.Int8)
                return new PSObject(array.To4DArray<sbyte>());
            if (dtype == DType.UInt8)
                return new PSObject(array.To4DArray<byte>());

            throw new ArgumentException($"Type {dtype} cannot be displayed");
        }

        public static PSObject To2DJagged(PSObject self)
        {
            var array = self.BaseObject as NDArray;

            var dtype = array.DType;

            if (dtype == DType.Float64)
                return new PSObject(array.To2DJagged<double>());
            if (dtype == DType.Float32)
                return new PSObject(array.To2DJagged<float>());
            if (dtype == DType.Int64)
                return new PSObject(array.To2DJagged<long>());
            if (dtype == DType.Int32)
                return new PSObject(array.To2DJagged<int>());
            if (dtype == DType.Int8)
                return new PSObject(array.To2DJagged<sbyte>());
            if (dtype == DType.UInt8)
                return new PSObject(array.To2DJagged<byte>());

            throw new ArgumentException($"Type {dtype} cannot be displayed");
        }

        public static PSObject To3DJagged(PSObject self)
        {
            var array = self.BaseObject as NDArray;

            var dtype = array.DType;

            if (dtype == DType.Float64)
                return new PSObject(array.To3DJagged<double>());
            if (dtype == DType.Float32)
                return new PSObject(array.To3DJagged<float>());
            if (dtype == DType.Int64)
                return new PSObject(array.To3DJagged<long>());
            if (dtype == DType.Int32)
                return new PSObject(array.To3DJagged<int>());
            if (dtype == DType.Int8)
                return new PSObject(array.To3DJagged<sbyte>());
            if (dtype == DType.UInt8)
                return new PSObject(array.To3DJagged<byte>());

            throw new ArgumentException($"Type {dtype} cannot be displayed");
        }

        public static PSObject To4DJagged(PSObject self)
        {
            var array = self.BaseObject as NDArray;

            var dtype = array.DType;

            if (dtype == DType.Float64)
                return new PSObject(array.To4DJagged<double>());
            if (dtype == DType.Float32)
                return new PSObject(array.To4DJagged<float>());
            if (dtype == DType.Int64)
                return new PSObject(array.To4DJagged<long>());
            if (dtype == DType.Int32)
                return new PSObject(array.To4DJagged<int>());
            if (dtype == DType.Int8)
                return new PSObject(array.To4DJagged<sbyte>());
            if (dtype == DType.UInt8)
                return new PSObject(array.To4DJagged<byte>());

            throw new ArgumentException($"Type {dtype} cannot be displayed");
        }

    }
}
