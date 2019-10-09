using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions
{
    public static partial class TypeTrait
    {
        public static bool IsNaN(double self) { return double.IsNaN(self); }
        public static bool IsNaN(float self) { return double.IsNaN(self); }
        public static bool IsNaN(long self) { return false; }
        public static bool IsNaN(int self) { return false; }
        public static bool IsNaN(short self) { return false; }
        public static bool IsNaN(byte self) { return false; }
        public static bool IsNaN(sbyte self) { return false; }
        public static bool IsNaN(decimal self) { return false; }
        public static bool IsNaN(string self) { return string.IsNullOrWhiteSpace(self); }
        public static bool IsNaN(bool self) { return false; }
        public static bool IsNaN(object self) { return self == null; }
    }

    public static class NaN<T>
    {
        public static T Value = GetNaN();

        private static T GetNaN()
        {
            if (typeof(T) == typeof(double)) return (T)(object)double.NaN;
            if (typeof(T) == typeof(float)) return (T)(object)float.NaN;
            if (typeof(T) == typeof(long)) return (T)(object)0;
            if (typeof(T) == typeof(int)) return (T)(object)0;
            if (typeof(T) == typeof(short)) return (T)(object)0;
            if (typeof(T) == typeof(byte)) return (T)(object)0;
            if (typeof(T) == typeof(sbyte)) return (T)(object)0;
            if (typeof(T) == typeof(decimal)) return (T)(object)0;
            if (typeof(T) == typeof(string)) return (T)(object)string.Empty;
            if (typeof(T) == typeof(bool)) return (T)(object)false;

            if (typeof(T).IsValueType)
            {
                var constructor = typeof(T).GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                    return (T)(object)constructor.Invoke(new object[0]);
            }
            
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
    }
}
