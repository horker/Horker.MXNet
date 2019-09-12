namespace Horker.Numerics.DataMaps
{
    public class MetaNum
    {
        double Value;

        public MetaNum(double value)
        {
            Value = value;
        }

        public static MetaNum operator +(MetaNum self) { return (MetaNum)0; }
        public static MetaNum operator -(MetaNum self) { return (MetaNum)0; }

        public static MetaNum operator +(MetaNum lhs, MetaNum rhs) { return (MetaNum)0; }
        public static MetaNum operator -(MetaNum lhs, MetaNum rhs) { return (MetaNum)0; }
        public static MetaNum operator *(MetaNum lhs, MetaNum rhs) { return (MetaNum)0; }
        public static MetaNum operator /(MetaNum lhs, MetaNum rhs) { return (MetaNum)0; }

        public static bool operator <(MetaNum lhs, MetaNum rhs) { return false; }
        public static bool operator <=(MetaNum lhs, MetaNum rhs) { return false; }
        public static bool operator >(MetaNum lhs, MetaNum rhs) { return false; }
        public static bool operator >=(MetaNum lhs, MetaNum rhs) { return false; }

        public static explicit operator MetaNum(double value)
        {
            return new MetaNum(value);
        }

        public static explicit operator double(MetaNum value)
        {
            return value.Value;
        }
    }

    public class MetaFloat : MetaNum
    {
        public MetaFloat(int value)
            : base(value)
        { }

        public static implicit operator double(MetaFloat value)
        {
            return 0;
        }
    }
}
