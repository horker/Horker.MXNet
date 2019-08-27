using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.Numerics
{
    public partial class NumericNDArray<T> : NDArray<T>
        where T : struct
    {
        static NumericNDArray()
        {
            _dtype = DType.FromType(typeof(T));
        }

        private static DType _dtype;

        public static DType DType => _dtype;

        protected NDArray _impl;

        public override int[] Shape => _impl.Shape.Dimensions;
        public override long Size => _impl.Size;

        public NumericNDArray(NDArray impl)
        {
            _impl = impl;
        }

        public NumericNDArray(T[] data, int[] shape)
        {
            _impl = NDArray.FromArray(data, shape);
        }

        public override T[] ToArray()
        {
            return _impl.ToArray<T>();
        }

        public override string ToString()
        {
            return this.ToStringInShortFormat(true);
        }

        // Factory methods

        public static NumericNDArray<T> Create(T[] data, int[] shape = null)
        {
            if (shape == null)
                shape = new[] { data.Length };

            if (data is double[] d)
                return new DoubleNDArray(d, shape) as NumericNDArray<T>;
            if (data is float[] f)
                return new FloatNDArray(f, shape) as NumericNDArray<T>;
            if (data is long[] l)
                return new LongNDArray(l, shape) as NumericNDArray<T>;
            if (data is int[] i)
                return new IntNDArray(i, shape) as NumericNDArray<T>;
            if (data is sbyte[] sb)
                return new SByteNDArray(sb, shape) as NumericNDArray<T>;
            if (data is byte[] b)
                return new ByteNDArray(b, shape) as NumericNDArray<T>;

            throw new InvalidOperationException($"NumericNDArray does not support type {typeof(T).Name}");
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

            throw new InvalidOperationException($"NumericNDArray does not support type {typeof(T).Name}");
        }

        public static NumericNDArray<T> Zeros(int[] shape)
        {
            var impl = Op.Zeros(shape, null, DType.FromType(typeof(T)));
            return new NumericNDArray<T>(impl);
        }

        public static NumericNDArray<T> Ones(int[] shape)
        {
            var impl = Op.Ones(shape, null, DType.FromType(typeof(T)));
            return new NumericNDArray<T>(impl);
        }

    }
}
