using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.Numerics
{
    // CUT ABOVE
    /// <summary>
    /// This class is a type-specific version of NDArray for the float type.
    /// </summary>
    public partial class FloatNDArray : NumericNDArray<float>
    {
        public FloatNDArray(NDArray impl)
            : base(impl)
        {
        }

        public FloatNDArray(float[] data, int[] shape)
            : base (data, shape)
        {
        }

        public override float[] ToArray()
        {
            return _impl.ToArray<float>();
        }
    }

    // CUT BELOW
}
