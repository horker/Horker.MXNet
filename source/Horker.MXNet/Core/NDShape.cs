using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    public class NDShape
    {
        private readonly int[] _dimensions;
        private string _stringRepr;

        // You must not modify the elements of this value.
        public int[] Dimensions => _dimensions;

        public int this[int i] => _dimensions[i];

        public int NDimensions => _dimensions.Length;

        public int Size => _dimensions.Aggregate((d, sum) => sum * d);

        public NDShape(int [] shape)
        {
            _dimensions = shape.ToArray();
            _stringRepr = null;
        }

        public NDShape(IntPtr dimensions, int count)
        {
            _dimensions = new int[count];
            Marshal.Copy(dimensions, _dimensions, 0, count);
        }

        public override string ToString()
        {
            if (_stringRepr == null)
                _stringRepr = string.Format("({0})", string.Join(",", _dimensions));

            return _stringRepr;
        }

        public static implicit operator NDShape(int[] dimensions)
        {
            return new NDShape(dimensions);
        }

        public static implicit operator string(NDShape shape)
        {
            return shape.ToString();
        }

        // Equality functions

        public override int GetHashCode()
        {
            return _dimensions.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;

            return obj is NDShape d && this == d;
        }

        public static bool operator ==(NDShape lhs, NDShape rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;

            var lhsIsNull = ReferenceEquals(lhs, null);
            var rhsIsNull = ReferenceEquals(rhs, null);

            if (lhsIsNull && rhsIsNull)
                return true;

            if (lhsIsNull && !rhsIsNull || !lhsIsNull && rhsIsNull)
                return false;

            return lhs._dimensions == rhs._dimensions;
        }

        public static bool operator !=(NDShape lhs, NDShape rhs)
        {
            return !(lhs == rhs);
        }
    }
}
