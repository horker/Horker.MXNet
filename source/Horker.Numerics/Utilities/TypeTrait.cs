using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Utilities
{
    public static class TypeTrait<T>
    {
        private static T GetNaN()
        {
            if (typeof(T) == typeof(double))
                return (T)(object)double.NaN;
            if (typeof(T) == typeof(float))
                return (T)(object)float.NaN;
            if (typeof(T) == typeof(long))
                return (T)(object)0;
            if (typeof(T) == typeof(int))
                return (T)(object)0;
            if (typeof(T) == typeof(short))
                return (T)(object)0;
            if (typeof(T) == typeof(byte))
                return (T)(object)0;
            if (typeof(T) == typeof(sbyte))
                return (T)(object)0;
            if (typeof(T) == typeof(decimal))
                return (T)(object)0;
            if (typeof(T) == typeof(string))
                return (T)(object)string.Empty;
            if (typeof(T) == typeof(bool))
                return (T)(object)false;
            if (typeof(T) == typeof(DateTime))
                return (T)(object)DateTime.MinValue;
            if (typeof(T) == typeof(DateTimeOffset))
                return (T)(object)DateTimeOffset.MinValue;

            if (typeof(T).IsValueType)
                return (T)Activator.CreateInstance(typeof(T));

            return (T)(object)null;
        }

        public static T GetNaNOrRaiseException(string message)
        {
            if (typeof(T) == typeof(double))
                return (T)(object)double.NaN;

            if (typeof(T) == typeof(float))
                return (T)(object)float.NaN;

            throw new InvalidOperationException(message);
        }

        public static bool IsNaN(T value)
        {
            if (typeof(T) == typeof(double))
                return double.IsNaN((double)(object)value);
            if (typeof(T) == typeof(float))
                return float.IsNaN((float)(object)value);
            if (typeof(T) == typeof(long))
                return false;
            if (typeof(T) == typeof(int))
                return false;
            if (typeof(T) == typeof(short))
                return false;
            if (typeof(T) == typeof(byte))
                return false;
            if (typeof(T) == typeof(sbyte))
                return false;
            if (typeof(T) == typeof(decimal))
                return false;
            if (typeof(T) == typeof(string))
                return string.IsNullOrWhiteSpace((string)(object)value);
            if (typeof(T) == typeof(bool))
                return false;
            if (typeof(T) == typeof(DateTime))
                return DateTime.MinValue.Equals(value);
            if (typeof(T) == typeof(DateTimeOffset))
                return DateTimeOffset.MinValue.Equals(value);

            return value == null;
        }

        public static T GetZero()
        {
            if (typeof(T) == typeof(double))
                return (T)(object)0.0;
            if (typeof(T) == typeof(float))
                return (T)(object)0.0f;
            if (typeof(T) == typeof(long))
                return (T)(object)0;
            if (typeof(T) == typeof(int))
                return (T)(object)0;
            if (typeof(T) == typeof(short))
                return (T)(object)0;
            if (typeof(T) == typeof(byte))
                return (T)(object)0;
            if (typeof(T) == typeof(sbyte))
                return (T)(object)0;
            if (typeof(T) == typeof(decimal))
                return (T)(object)0;

            throw new InvalidCastException("Type {typeof(T)} is not numeric type");
        }

        public static T GetOne()
        {
            if (typeof(T) == typeof(double))
                return (T)(object)1.0;
            if (typeof(T) == typeof(float))
                return (T)(object)1.0f;
            if (typeof(T) == typeof(long))
                return (T)(object)1;
            if (typeof(T) == typeof(int))
                return (T)(object)1;
            if (typeof(T) == typeof(short))
                return (T)(object)1;
            if (typeof(T) == typeof(byte))
                return (T)(object)1;
            if (typeof(T) == typeof(sbyte))
                return (T)(object)1;
            if (typeof(T) == typeof(decimal))
                return (T)(object)1;

            throw new InvalidCastException("Type {typeof(T)} is not numeric type");
        }

        public static T GetMinusOne()
        {
            if (typeof(T) == typeof(double))
                return (T)(object)-1.0;
            if (typeof(T) == typeof(float))
                return (T)(object)-1.0f;
            if (typeof(T) == typeof(long))
                return (T)(object)-1;
            if (typeof(T) == typeof(int))
                return (T)(object)-1;
            if (typeof(T) == typeof(short))
                return (T)(object)-1;
            if (typeof(T) == typeof(byte))
                return (T)(object)-1;
            if (typeof(T) == typeof(sbyte))
                return (T)(object)-1;
            if (typeof(T) == typeof(decimal))
                return (T)(object)-1;

            throw new InvalidCastException("Type {typeof(T)} is not numeric type");
        }
    }
}
