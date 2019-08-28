using Horker.MXNet.Operators;

namespace Horker.MXNet.Core
{
    public partial class NDArray : NDArrayOrSymbol
    {
        public static NDArray operator+(NDArray lhs, NDArray rhs)
        {
            return Op.BroadcastAdd(lhs, rhs);
        }

        public static NDArray operator+(NDArray lhs, float rhs)
        {
            return Op.PlusScalar(lhs, rhs);
        }

        public static NDArray operator+(float lhs, NDArray rhs)
        {
            return Op.PlusScalar(rhs, lhs);
        }

        public static NDArray operator-(NDArray lhs, NDArray rhs)
        {
            return Op.BroadcastSub(lhs, rhs);
        }

        public static NDArray operator-(NDArray lhs, float rhs)
        {
            return Op.MinusScalar(lhs, rhs);
        }

        public static NDArray operator-(float lhs, NDArray rhs)
        {
            return Op.RminusScalar(rhs, lhs);
        }

        public static NDArray operator*(NDArray lhs, NDArray rhs)
        {
            return Op.BroadcastMul(lhs, rhs);
        }

        public static NDArray operator*(NDArray lhs, float rhs)
        {
            return Op.DivScalar(lhs, rhs);
        }

        public static NDArray operator*(float lhs, NDArray rhs)
        {
            return Op.DivScalar(rhs, lhs);
        }

        public static NDArray operator/(NDArray lhs, NDArray rhs)
        {
            return Op.BroadcastDiv(lhs, rhs);
        }

        public static NDArray operator/(NDArray lhs, float rhs)
        {
            return Op.DivScalar(lhs, rhs);
        }

        public static NDArray operator/(float lhs, NDArray rhs)
        {
            return Op.RdivScalar(rhs, lhs);
        }

        public static NDArray operator%(NDArray lhs, NDArray rhs)
        {
            return Op.BroadcastMod(lhs, rhs);
        }

        public static NDArray operator%(NDArray lhs, float rhs)
        {
            return Op.ModScalar(lhs, rhs);
        }

        public static NDArray operator%(float lhs, NDArray rhs)
        {
            return Op.RmodScalar(rhs, lhs);
        }

        public static NDArray operator&(NDArray lhs, NDArray rhs)
        {
            return Op.BroadcastLogicalAnd(lhs, rhs);
        }

        public static NDArray operator&(NDArray lhs, float rhs)
        {
            return Op.LogicalAndScalar(lhs, rhs);
        }

        public static NDArray operator&(float lhs, NDArray rhs)
        {
            return Op.LogicalAndScalar(rhs, lhs);
        }

        public static NDArray operator|(NDArray lhs, NDArray rhs)
        {
            return Op.BroadcastLogicalOr(lhs, rhs);
        }

        public static NDArray operator|(NDArray lhs, float rhs)
        {
            return Op.LogicalOrScalar(lhs, rhs);
        }

        public static NDArray operator|(float lhs, NDArray rhs)
        {
            return Op.LogicalOrScalar(rhs, lhs);
        }

        public static NDArray operator^(NDArray lhs, NDArray rhs)
        {
            return Op.BroadcastLogicalXor(lhs, rhs);
        }

        public static NDArray operator^(NDArray lhs, float rhs)
        {
            return Op.LogicalXorScalar(lhs, rhs);
        }

        public static NDArray operator^(float lhs, NDArray rhs)
        {
            return Op.LogicalXorScalar(rhs, lhs);
        }

    }
}
