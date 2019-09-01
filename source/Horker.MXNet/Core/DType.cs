using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    public enum DTypeEnum
    {
        None = -1,
        Float32 = 0,
        Float64 = 1,
        Float16 = 2,
        UInt8 = 3,
        Int32 = 4,
        Int8 = 5,
        Int64 = 6,
        Auto = 7 // Used as operator arguments
    }

    public class DType
    {
        private static readonly string[] _names = new [] {
            "float32",
            "float64",
            "float16",
            "uint8",
            "int32",
            "int8",
            "int64",
            "auto"
        };

        private static readonly Type[] _types = new[]
        {
            typeof(float),
            typeof(double),
            null,
            typeof(byte),
            typeof(int),
            typeof(sbyte),
            typeof(long),
            null
        };

        private static DType[] _instances;

        static DType()
        {
            _instances = new DType[_names.Length];

            for (var i = 0; i < _names.Length; ++i)
                _instances[i] = new DType((DTypeEnum)i);

            Float32 = _instances[0];
            Float64 = _instances[1];
            Float16 = _instances[2];
            UInt8 = _instances[3];
            Int32 = _instances[4];
            Int8 = _instances[5];
            Int64 = _instances[6];
            Auto = _instances[7];

            DefaultDType = _instances[0];
        }

        public static DType Float32 { get; private set; }
        public static DType Float64 { get; private set; }
        public static DType Float16 { get; private set; }
        public static DType UInt8 { get; private set; }
        public static DType Int32 { get; private set; }
        public static DType Int8 { get; private set; }
        public static DType Int64 { get; private set; }
        public static DType Auto { get; private set; }

        public static DType DefaultDType { get; set; }

        private DTypeEnum _dtype;

        public string TypeName => _names[(int)_dtype];

        public Type RuntimeType => _types[(int)_dtype];

        public DType(DTypeEnum dtype)
        {
            _dtype = dtype;
        }

        public DType(string name)
        {
            var n = name.ToLower();
            for (var i = 0; i < _names.Length; ++i)
            {
                if (_names[i] == name)
                {
                    _dtype = (DTypeEnum)i;
                    return;
                }
            }

            throw new ArgumentException("Invalid type name");
        }

        public static DType FromType(Type type)
        {
            if (type == typeof(double))
                return DType.Float64;

            if (type == typeof(float))
                return DType.Float32;

            if (type == typeof(long))
                return DType.Int64;

            if (type == typeof(int))
                return DType.Int32;

            if (type == typeof(sbyte))
                return DType.Int8;

            if (type == typeof(byte))
                return DType.UInt8;

            throw new ArgumentException($"Unsupported type for DType: {type.FullName}");
        }

        public override string ToString()
        {
            return _names[(int)_dtype];
        }

        public static implicit operator string(DType dtype)
        {
            return _names[(int)dtype._dtype];
        }

        public static implicit operator int(DType dtype)
        {
            return (int)dtype._dtype;
        }

        // Equality functions

        public override int GetHashCode()
        {
            unchecked
            {
                return (int)_dtype * 397;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;

            return obj is DType d && this == d;
        }

        public static bool operator ==(DType lhs, DType rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;

            var lhsIsNull = ReferenceEquals(lhs, null);
            var rhsIsNull = ReferenceEquals(rhs, null);

            if (lhsIsNull && rhsIsNull)
                return true;

            if (lhsIsNull && !rhsIsNull || !lhsIsNull && rhsIsNull)
                return false;

            return lhs._dtype == rhs._dtype;
        }

        public static bool operator !=(DType lhs, DType rhs)
        {
            return !(lhs == rhs);
        }
    }
}
