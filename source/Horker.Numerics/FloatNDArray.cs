using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.Numerics
{
    // CUT ABOVE
    /// <summary>
    /// This class is a type-specific version of NDArray for the float type.
    /// </summary>
    public class FloatNDArray : NDArray<float>
    {
        private static DType _dtype;

        static FloatNDArray() {
            _dtype = DType.FromType(typeof(float));
        }

        private NDArray _impl;

        public override int[] Shape => _impl.Shape.Dimensions;
        public override long Size => _impl.Size;

        public FloatNDArray(NDArray impl)
        {
            _impl = impl;
        }

        public FloatNDArray(float[] data, int[] shape)
        {
            _impl = NDArray.FromArray<float>(data, shape);
        }

        public override float[] ToArray()
        {
            return _impl.ToArray<float>();
        }

        public override string ToString()
        {
            return this.ToStringInShortFormat(true);
        }

        public FloatNDArray Add(FloatNDArray rhs)
        {
            var impl = Op.BroadcastAdd(_impl, rhs._impl);
            return new FloatNDArray(impl);
        }
    }

    // CUT BELOW
}
