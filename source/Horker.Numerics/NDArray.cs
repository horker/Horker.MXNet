using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.Numerics
{
    public abstract class NDArray<T> : NDArrayObject
    {
        public Type DataType => typeof(T);

        public abstract int[] Shape { get; }
        public abstract long Size { get; }

        public abstract T[] ToArray();

        public static NDArray<T> Create(T[] data, int[] shape = null)
        {
            if (shape == null)
                shape = new[] { data.Length };

            if (data is double[] d)
                return new DoubleNDArray(d, shape) as NDArray<T>;
            if (data is float[] f)
                return new FloatNDArray(f, shape) as NDArray<T>;
            if (data is long[] l)
                return new LongNDArray(l, shape) as NDArray<T>;
            if (data is int[] i)
                return new IntNDArray(i, shape) as NDArray<T>;
            if (data is sbyte[] sb)
                return new SByteNDArray(sb, shape) as NDArray<T>;
            if (data is byte[] b)
                return new ByteNDArray(b, shape) as NDArray<T>;

            return new GenericNDArray<T>(data, shape);
        }

        private static NDArray<T> Create(NDArray impl)
        {
            if (typeof(T) == typeof(double))
                return new DoubleNDArray(impl) as NDArray<T>;
            if (typeof(T) == typeof(float))
                return new FloatNDArray(impl) as NDArray<T>;
            if (typeof(T) == typeof(long))
                return new LongNDArray(impl) as NDArray<T>;
            if (typeof(T) == typeof(int))
                return new IntNDArray(impl) as NDArray<T>;
            if (typeof(T) == typeof(sbyte))
                return new SByteNDArray(impl) as NDArray<T>;
            if (typeof(T) == typeof(byte))
                return new ByteNDArray(impl) as NDArray<T>;

            throw new InvalidOperationException($"NDArray does not support type {typeof(T).Name}");
        }

        public static NDArray<T> Zeros(int[] shape)
        {
            var impl = Op.Zeros(shape, null, DType.FromType(typeof(T)));
            return Create(impl);
        }

        public static NDArray<T> Ones(int[] shape)
        {
            var impl = Op.Ones(shape, null, DType.FromType(typeof(T)));
            return Create(impl);
        }

        public static NDArray<T> operator+(NDArray<T> lhs, NDArray<T> rhs)
        {
            if (typeof(T) == typeof(double))
                return (lhs as DoubleNDArray).Add(rhs as DoubleNDArray) as NDArray<T>;
            if (typeof(T) == typeof(float))
                return (lhs as FloatNDArray).Add(rhs as FloatNDArray) as NDArray<T>;
            if (typeof(T) == typeof(long))
                return (lhs as LongNDArray).Add(rhs as LongNDArray) as NDArray<T>;
            if (typeof(T) == typeof(int))
                return (lhs as IntNDArray).Add(rhs as IntNDArray) as NDArray<T>;
            if (typeof(T) == typeof(sbyte))
                return (lhs as SByteNDArray).Add(rhs as SByteNDArray) as NDArray<T>;
            if (typeof(T) == typeof(byte))
                return (lhs as ByteNDArray).Add(rhs as ByteNDArray) as NDArray<T>;

            throw new InvalidOperationException($"Operation + is not supported for type {typeof(T).Name}");
        }
    }
}
