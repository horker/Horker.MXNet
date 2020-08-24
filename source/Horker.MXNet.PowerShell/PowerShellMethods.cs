using MxNet;
using MxNet.IO;
using OpenCvSharp.Aruco;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using System.Threading;

namespace Horker.MxNet.PowerShell
{
    public class NDArrayIterMethods
    {
        public static readonly Type TargetType = typeof(NDArrayIter);

        // By PowerShell's bug, calling methods that matches some specific conditions
        // causes a runtime exception. See https://github.com/PowerShell/PowerShell/issues/7686 for details.
        // A workaround is to call such methods indirectly via PowerShell methods.
        // NDArrayIter.End() is one of such methods, so we have defined a wrapper method.
        public static PSObject End(PSObject self)
        {
            var iter = (NDArrayIter)self.BaseObject;
            return iter.End();
        }
    }

    public class NDArrayMethods
    {
        public static readonly Type TargetType = typeof(NDArray);

        public static PSObject ToArray(PSObject self)
        {
            var array = self.BaseObject as NDArray;

            var dtype = DType.GetType(array.GetDType());

            if (dtype == DType.Float64)
                return new PSObject(array.GetValues<double>());
            if (dtype == DType.Float32)
                return new PSObject(array.GetValues<float>());
            if (dtype == DType.Int64)
                return new PSObject(array.GetValues<long>());
            if (dtype == DType.Int32)
                return new PSObject(array.GetValues<int>());
            if (dtype == DType.Int8)
                return new PSObject(array.GetValues<sbyte>());
            if (dtype == DType.UInt8)
                return new PSObject(array.GetValues<byte>());

            throw new ArgumentException($"Type {dtype} cannot be displayed");
        }

        public static PSObject To2DArray(PSObject self)
        {
            var array = self.BaseObject as NDArray;

            var dtype = DType.GetType(array.GetDType());

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

            var dtype = DType.GetType(array.GetDType());

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

            var dtype = DType.GetType(array.GetDType());

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

            var dtype = DType.GetType(array.GetDType());

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

            var dtype = DType.GetType(array.GetDType());

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

            var dtype = DType.GetType(array.GetDType());

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
