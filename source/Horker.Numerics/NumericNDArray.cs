using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.Numerics
{

    /// <summary>
    /// This class is a type-specific version of NDArray for the double type.
    /// </summary>
    public class DoubleNDArray : NDArray<double>
    {
        private static DType _dtype;

        static DoubleNDArray() {
            _dtype = DType.FromType(typeof(double));
        }

        private NDArray _impl;

        public override int[] Shape => _impl.Shape.Dimensions;
        public override long Size => _impl.Size;

        public DoubleNDArray(NDArray impl)
        {
            _impl = impl;
        }

        public DoubleNDArray(double[] data, int[] shape)
        {
            _impl = NDArray.FromArray<double>(data, shape);
        }

        public override double[] ToArray()
        {
            return _impl.ToArray<double>();
        }

        public override string ToString()
        {
            return this.ToStringInShortFormat(true);
        }

        public DoubleNDArray Add(DoubleNDArray rhs)
        {
            var impl = Op.BroadcastAdd(_impl, rhs._impl);
            return new DoubleNDArray(impl);
        }
    }

    /// <summary>
    /// This class is a type-specific version of NDArray for the long type.
    /// </summary>
    public class LongNDArray : NDArray<long>
    {
        private static DType _dtype;

        static LongNDArray() {
            _dtype = DType.FromType(typeof(long));
        }

        private NDArray _impl;

        public override int[] Shape => _impl.Shape.Dimensions;
        public override long Size => _impl.Size;

        public LongNDArray(NDArray impl)
        {
            _impl = impl;
        }

        public LongNDArray(long[] data, int[] shape)
        {
            _impl = NDArray.FromArray<long>(data, shape);
        }

        public override long[] ToArray()
        {
            return _impl.ToArray<long>();
        }

        public override string ToString()
        {
            return this.ToStringInShortFormat(true);
        }

        public LongNDArray Add(LongNDArray rhs)
        {
            var impl = Op.BroadcastAdd(_impl, rhs._impl);
            return new LongNDArray(impl);
        }
    }

    /// <summary>
    /// This class is a type-specific version of NDArray for the int type.
    /// </summary>
    public class IntNDArray : NDArray<int>
    {
        private static DType _dtype;

        static IntNDArray() {
            _dtype = DType.FromType(typeof(int));
        }

        private NDArray _impl;

        public override int[] Shape => _impl.Shape.Dimensions;
        public override long Size => _impl.Size;

        public IntNDArray(NDArray impl)
        {
            _impl = impl;
        }

        public IntNDArray(int[] data, int[] shape)
        {
            _impl = NDArray.FromArray<int>(data, shape);
        }

        public override int[] ToArray()
        {
            return _impl.ToArray<int>();
        }

        public override string ToString()
        {
            return this.ToStringInShortFormat(true);
        }

        public IntNDArray Add(IntNDArray rhs)
        {
            var impl = Op.BroadcastAdd(_impl, rhs._impl);
            return new IntNDArray(impl);
        }
    }

    /// <summary>
    /// This class is a type-specific version of NDArray for the byte type.
    /// </summary>
    public class ByteNDArray : NDArray<byte>
    {
        private static DType _dtype;

        static ByteNDArray() {
            _dtype = DType.FromType(typeof(byte));
        }

        private NDArray _impl;

        public override int[] Shape => _impl.Shape.Dimensions;
        public override long Size => _impl.Size;

        public ByteNDArray(NDArray impl)
        {
            _impl = impl;
        }

        public ByteNDArray(byte[] data, int[] shape)
        {
            _impl = NDArray.FromArray<byte>(data, shape);
        }

        public override byte[] ToArray()
        {
            return _impl.ToArray<byte>();
        }

        public override string ToString()
        {
            return this.ToStringInShortFormat(true);
        }

        public ByteNDArray Add(ByteNDArray rhs)
        {
            var impl = Op.BroadcastAdd(_impl, rhs._impl);
            return new ByteNDArray(impl);
        }
    }

    /// <summary>
    /// This class is a type-specific version of NDArray for the sbyte type.
    /// </summary>
    public class SByteNDArray : NDArray<sbyte>
    {
        private static DType _dtype;

        static SByteNDArray() {
            _dtype = DType.FromType(typeof(sbyte));
        }

        private NDArray _impl;

        public override int[] Shape => _impl.Shape.Dimensions;
        public override long Size => _impl.Size;

        public SByteNDArray(NDArray impl)
        {
            _impl = impl;
        }

        public SByteNDArray(sbyte[] data, int[] shape)
        {
            _impl = NDArray.FromArray<sbyte>(data, shape);
        }

        public override sbyte[] ToArray()
        {
            return _impl.ToArray<sbyte>();
        }

        public override string ToString()
        {
            return this.ToStringInShortFormat(true);
        }

        public SByteNDArray Add(SByteNDArray rhs)
        {
            var impl = Op.BroadcastAdd(_impl, rhs._impl);
            return new SByteNDArray(impl);
        }
    }
}
