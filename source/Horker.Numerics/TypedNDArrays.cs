using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.Numerics
{

    /// <summary>
    /// This class is a type-specific version of NDArray for the double type.
    /// </summary>
    public partial class DoubleNDArray : NumericNDArray<double>
    {
        public DoubleNDArray(NDArray impl)
            : base(impl)
        {
        }

        public DoubleNDArray(double[] data, int[] shape)
            : base (data, shape)
        {
        }

        public override double[] ToArray()
        {
            return _impl.ToArray<double>();
        }
    }

    /// <summary>
    /// This class is a type-specific version of NDArray for the long type.
    /// </summary>
    public partial class LongNDArray : NumericNDArray<long>
    {
        public LongNDArray(NDArray impl)
            : base(impl)
        {
        }

        public LongNDArray(long[] data, int[] shape)
            : base (data, shape)
        {
        }

        public override long[] ToArray()
        {
            return _impl.ToArray<long>();
        }
    }

    /// <summary>
    /// This class is a type-specific version of NDArray for the int type.
    /// </summary>
    public partial class IntNDArray : NumericNDArray<int>
    {
        public IntNDArray(NDArray impl)
            : base(impl)
        {
        }

        public IntNDArray(int[] data, int[] shape)
            : base (data, shape)
        {
        }

        public override int[] ToArray()
        {
            return _impl.ToArray<int>();
        }
    }

    /// <summary>
    /// This class is a type-specific version of NDArray for the byte type.
    /// </summary>
    public partial class ByteNDArray : NumericNDArray<byte>
    {
        public ByteNDArray(NDArray impl)
            : base(impl)
        {
        }

        public ByteNDArray(byte[] data, int[] shape)
            : base (data, shape)
        {
        }

        public override byte[] ToArray()
        {
            return _impl.ToArray<byte>();
        }
    }

    /// <summary>
    /// This class is a type-specific version of NDArray for the sbyte type.
    /// </summary>
    public partial class SByteNDArray : NumericNDArray<sbyte>
    {
        public SByteNDArray(NDArray impl)
            : base(impl)
        {
        }

        public SByteNDArray(sbyte[] data, int[] shape)
            : base (data, shape)
        {
        }

        public override sbyte[] ToArray()
        {
            return _impl.ToArray<sbyte>();
        }
    }
}
