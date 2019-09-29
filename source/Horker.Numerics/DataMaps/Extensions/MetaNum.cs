namespace Horker.Numerics.DataMaps
{
    public class MetaNum
    {
        private double _value;

        public MetaNum(double value)
        {
            _value = value;
        }

        public static MetaNum operator +(MetaNum self) { return (MetaNum)0; }
        public static MetaNum operator -(MetaNum self) { return (MetaNum)0; }

        public static MetaNum operator +(MetaNum lhs, MetaNum rhs) { return (MetaNum)0; }
        public static MetaNum operator -(MetaNum lhs, MetaNum rhs) { return (MetaNum)0; }
        public static MetaNum operator *(MetaNum lhs, MetaNum rhs) { return (MetaNum)0; }
        public static MetaNum operator /(MetaNum lhs, MetaNum rhs) { return (MetaNum)0; }

        public static MetaNum operator +(MetaNum lhs, double rhs) { return (MetaNum)0; }
        public static MetaNum operator -(MetaNum lhs, double rhs) { return (MetaNum)0; }
        public static MetaNum operator *(MetaNum lhs, double rhs) { return (MetaNum)0; }
        public static MetaNum operator /(MetaNum lhs, double rhs) { return (MetaNum)0; }

        public static MetaNum operator +(double lhs, MetaNum rhs) { return (MetaNum)0; }
        public static MetaNum operator -(double lhs, MetaNum rhs) { return (MetaNum)0; }
        public static MetaNum operator *(double lhs, MetaNum rhs) { return (MetaNum)0; }
        public static MetaNum operator /(double lhs, MetaNum rhs) { return (MetaNum)0; }

        public static bool operator <(MetaNum lhs, MetaNum rhs) { return false; }
        public static bool operator <=(MetaNum lhs, MetaNum rhs) { return false; }
        public static bool operator >(MetaNum lhs, MetaNum rhs) { return false; }
        public static bool operator >=(MetaNum lhs, MetaNum rhs) { return false; }

        public static bool operator <(MetaNum lhs, double rhs) { return false; }
        public static bool operator <=(MetaNum lhs, double rhs) { return false; }
        public static bool operator >(MetaNum lhs, double rhs) { return false; }
        public static bool operator >=(MetaNum lhs, double rhs) { return false; }

        public static bool operator <(double lhs, MetaNum rhs) { return false; }
        public static bool operator <=(double lhs, MetaNum rhs) { return false; }
        public static bool operator >(double lhs, MetaNum rhs) { return false; }
        public static bool operator >=(double lhs, MetaNum rhs) { return false; }

        public static explicit operator MetaNum(double value)
        {
            return new MetaNum(0);
        }

        public static explicit operator double(MetaNum value)
        {
            return 0;
        }
    }

    public class MetaFloat
    {
        public static MetaFloat NaN = new MetaFloat(0);

        public static bool IsNaN(MetaFloat value)
        {
            return true;
        }

        private double _value;

        public MetaFloat(double value)
        {
            _value = value;
        }

        public static MetaFloat operator +(MetaFloat self) { return (MetaFloat)0; }
        public static MetaFloat operator -(MetaFloat self) { return (MetaFloat)0; }

        public static MetaFloat operator +(MetaFloat lhs, MetaFloat rhs) { return (MetaFloat)0; }
        public static MetaFloat operator -(MetaFloat lhs, MetaFloat rhs) { return (MetaFloat)0; }
        public static MetaFloat operator *(MetaFloat lhs, MetaFloat rhs) { return (MetaFloat)0; }
        public static MetaFloat operator /(MetaFloat lhs, MetaFloat rhs) { return (MetaFloat)0; }

        public static MetaFloat operator +(MetaFloat lhs, double rhs) { return (MetaFloat)0; }
        public static MetaFloat operator -(MetaFloat lhs, double rhs) { return (MetaFloat)0; }
        public static MetaFloat operator *(MetaFloat lhs, double rhs) { return (MetaFloat)0; }
        public static MetaFloat operator /(MetaFloat lhs, double rhs) { return (MetaFloat)0; }

        public static MetaFloat operator +(double lhs, MetaFloat rhs) { return (MetaFloat)0; }
        public static MetaFloat operator -(double lhs, MetaFloat rhs) { return (MetaFloat)0; }
        public static MetaFloat operator *(double lhs, MetaFloat rhs) { return (MetaFloat)0; }
        public static MetaFloat operator /(double lhs, MetaFloat rhs) { return (MetaFloat)0; }

        public static bool operator <(MetaFloat lhs, MetaFloat rhs) { return false; }
        public static bool operator <=(MetaFloat lhs, MetaFloat rhs) { return false; }
        public static bool operator >(MetaFloat lhs, MetaFloat rhs) { return false; }
        public static bool operator >=(MetaFloat lhs, MetaFloat rhs) { return false; }

        public static bool operator <(MetaFloat lhs, double rhs) { return false; }
        public static bool operator <=(MetaFloat lhs, double rhs) { return false; }
        public static bool operator >(MetaFloat lhs, double rhs) { return false; }
        public static bool operator >=(MetaFloat lhs, double rhs) { return false; }

        public static bool operator <(double lhs, MetaFloat rhs) { return false; }
        public static bool operator <=(double lhs, MetaFloat rhs) { return false; }
        public static bool operator >(double lhs, MetaFloat rhs) { return false; }
        public static bool operator >=(double lhs, MetaFloat rhs) { return false; }

        public static explicit operator MetaFloat(double value)
        {
            return new MetaFloat(0);
        }

        public static explicit operator double(MetaFloat value)
        {
            return 0;
        }

        public static explicit operator MetaFloat(MetaNum value)
        {
            return new MetaFloat(0);
        }

        public static explicit operator MetaNum(MetaFloat value)
        {
            return new MetaNum(0);
        }
    }
}
