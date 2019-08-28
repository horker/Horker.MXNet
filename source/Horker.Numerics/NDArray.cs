using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.Numerics
{
    public abstract partial class NDArray<T> : NDArrayObject
    {
        public Type DataType => typeof(T);

        public abstract int[] Shape { get; }
        public abstract long Size { get; }

        public abstract T[] ToArray();

        public static NDArray<T> operator+(NDArray<T> lhs, NDArray<T> rhs)
        {
            if (typeof(T) == typeof(double))
                return (lhs as DoubleNDArray).BroadcastAdd(rhs as DoubleNDArray) as NDArray<T>;
            if (typeof(T) == typeof(float))
                return (lhs as FloatNDArray).BroadcastAdd(rhs as FloatNDArray) as NDArray<T>;
            if (typeof(T) == typeof(long))
                return (lhs as LongNDArray).BroadcastAdd(rhs as LongNDArray) as NDArray<T>;
            if (typeof(T) == typeof(int))
                return (lhs as IntNDArray).BroadcastAdd(rhs as IntNDArray) as NDArray<T>;
            if (typeof(T) == typeof(sbyte))
                return (lhs as SByteNDArray).BroadcastAdd(rhs as SByteNDArray) as NDArray<T>;
            if (typeof(T) == typeof(byte))
                return (lhs as ByteNDArray).BroadcastAdd(rhs as ByteNDArray) as NDArray<T>;

            throw new InvalidOperationException($"Operation + is not supported for type {typeof(T).Name}");
        }

        public static NDArray<T> operator-(NDArray<T> lhs, NDArray<T> rhs)
        {
            if (typeof(T) == typeof(double))
                return (lhs as DoubleNDArray).BroadcastSub(rhs as DoubleNDArray) as NDArray<T>;
            if (typeof(T) == typeof(float))
                return (lhs as FloatNDArray).BroadcastSub(rhs as FloatNDArray) as NDArray<T>;
            if (typeof(T) == typeof(long))
                return (lhs as LongNDArray).BroadcastSub(rhs as LongNDArray) as NDArray<T>;
            if (typeof(T) == typeof(int))
                return (lhs as IntNDArray).BroadcastSub(rhs as IntNDArray) as NDArray<T>;
            if (typeof(T) == typeof(sbyte))
                return (lhs as SByteNDArray).BroadcastSub(rhs as SByteNDArray) as NDArray<T>;
            if (typeof(T) == typeof(byte))
                return (lhs as ByteNDArray).BroadcastSub(rhs as ByteNDArray) as NDArray<T>;

            throw new InvalidOperationException($"Operation - is not supported for type {typeof(T).Name}");
        }

        public static NDArray<T> operator*(NDArray<T> lhs, NDArray<T> rhs)
        {
            if (typeof(T) == typeof(double))
                return (lhs as DoubleNDArray).BroadcastMul(rhs as DoubleNDArray) as NDArray<T>;
            if (typeof(T) == typeof(float))
                return (lhs as FloatNDArray).BroadcastMul(rhs as FloatNDArray) as NDArray<T>;
            if (typeof(T) == typeof(long))
                return (lhs as LongNDArray).BroadcastMul(rhs as LongNDArray) as NDArray<T>;
            if (typeof(T) == typeof(int))
                return (lhs as IntNDArray).BroadcastMul(rhs as IntNDArray) as NDArray<T>;
            if (typeof(T) == typeof(sbyte))
                return (lhs as SByteNDArray).BroadcastMul(rhs as SByteNDArray) as NDArray<T>;
            if (typeof(T) == typeof(byte))
                return (lhs as ByteNDArray).BroadcastMul(rhs as ByteNDArray) as NDArray<T>;

            throw new InvalidOperationException($"Operation * is not supported for type {typeof(T).Name}");
        }

        public static NDArray<T> operator/(NDArray<T> lhs, NDArray<T> rhs)
        {
            if (typeof(T) == typeof(double))
                return (lhs as DoubleNDArray).BroadcastDiv(rhs as DoubleNDArray) as NDArray<T>;
            if (typeof(T) == typeof(float))
                return (lhs as FloatNDArray).BroadcastDiv(rhs as FloatNDArray) as NDArray<T>;
            if (typeof(T) == typeof(long))
                return (lhs as LongNDArray).BroadcastDiv(rhs as LongNDArray) as NDArray<T>;
            if (typeof(T) == typeof(int))
                return (lhs as IntNDArray).BroadcastDiv(rhs as IntNDArray) as NDArray<T>;
            if (typeof(T) == typeof(sbyte))
                return (lhs as SByteNDArray).BroadcastDiv(rhs as SByteNDArray) as NDArray<T>;
            if (typeof(T) == typeof(byte))
                return (lhs as ByteNDArray).BroadcastDiv(rhs as ByteNDArray) as NDArray<T>;

            throw new InvalidOperationException($"Operation / is not supported for type {typeof(T).Name}");
        }

        public static NDArray<T> operator%(NDArray<T> lhs, NDArray<T> rhs)
        {
            if (typeof(T) == typeof(double))
                return (lhs as DoubleNDArray).BroadcastMod(rhs as DoubleNDArray) as NDArray<T>;
            if (typeof(T) == typeof(float))
                return (lhs as FloatNDArray).BroadcastMod(rhs as FloatNDArray) as NDArray<T>;
            if (typeof(T) == typeof(long))
                return (lhs as LongNDArray).BroadcastMod(rhs as LongNDArray) as NDArray<T>;
            if (typeof(T) == typeof(int))
                return (lhs as IntNDArray).BroadcastMod(rhs as IntNDArray) as NDArray<T>;
            if (typeof(T) == typeof(sbyte))
                return (lhs as SByteNDArray).BroadcastMod(rhs as SByteNDArray) as NDArray<T>;
            if (typeof(T) == typeof(byte))
                return (lhs as ByteNDArray).BroadcastMod(rhs as ByteNDArray) as NDArray<T>;

            throw new InvalidOperationException($"Operation % is not supported for type {typeof(T).Name}");
        }
    }
}
