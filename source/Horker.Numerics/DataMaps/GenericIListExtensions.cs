using System;
using System.Collections.Generic;

namespace Horker.Numerics.DataMaps.Extensions
{
	public static partial class GenericIListExtensions
	{

        public static List<double> Sort(this IList<double> self)
        {
            var result = new List<double>(self);
            result.Sort();
            return result;
        }

        public static void FillSort(this IList<double> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<double>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static List<double> CumulativeSum(this IList<double> self)
        {
            var result = new List<double>(self.Count);
            double sum = (double)0;

            foreach (var value in self)
            {
                sum += value;
                result.Add(sum);
            }

            return result;
        }

        public static void FillCumulativeSum(this IList<double> self)
        {
            double sum = (double)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static List<float> Sort(this IList<float> self)
        {
            var result = new List<float>(self);
            result.Sort();
            return result;
        }

        public static void FillSort(this IList<float> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<float>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static List<float> CumulativeSum(this IList<float> self)
        {
            var result = new List<float>(self.Count);
            float sum = (float)0;

            foreach (var value in self)
            {
                sum += value;
                result.Add(sum);
            }

            return result;
        }

        public static void FillCumulativeSum(this IList<float> self)
        {
            float sum = (float)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static List<long> Sort(this IList<long> self)
        {
            var result = new List<long>(self);
            result.Sort();
            return result;
        }

        public static void FillSort(this IList<long> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<long>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static List<long> CumulativeSum(this IList<long> self)
        {
            var result = new List<long>(self.Count);
            long sum = (long)0;

            foreach (var value in self)
            {
                sum += value;
                result.Add(sum);
            }

            return result;
        }

        public static void FillCumulativeSum(this IList<long> self)
        {
            long sum = (long)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static List<int> Sort(this IList<int> self)
        {
            var result = new List<int>(self);
            result.Sort();
            return result;
        }

        public static void FillSort(this IList<int> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<int>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static List<int> CumulativeSum(this IList<int> self)
        {
            var result = new List<int>(self.Count);
            int sum = (int)0;

            foreach (var value in self)
            {
                sum += value;
                result.Add(sum);
            }

            return result;
        }

        public static void FillCumulativeSum(this IList<int> self)
        {
            int sum = (int)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static List<short> Sort(this IList<short> self)
        {
            var result = new List<short>(self);
            result.Sort();
            return result;
        }

        public static void FillSort(this IList<short> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<short>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static List<short> CumulativeSum(this IList<short> self)
        {
            var result = new List<short>(self.Count);
            short sum = (short)0;

            foreach (var value in self)
            {
                sum += value;
                result.Add(sum);
            }

            return result;
        }

        public static void FillCumulativeSum(this IList<short> self)
        {
            short sum = (short)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static List<byte> Sort(this IList<byte> self)
        {
            var result = new List<byte>(self);
            result.Sort();
            return result;
        }

        public static void FillSort(this IList<byte> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<byte>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static List<byte> CumulativeSum(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);
            byte sum = (byte)0;

            foreach (var value in self)
            {
                sum += value;
                result.Add(sum);
            }

            return result;
        }

        public static void FillCumulativeSum(this IList<byte> self)
        {
            byte sum = (byte)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static List<sbyte> Sort(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self);
            result.Sort();
            return result;
        }

        public static void FillSort(this IList<sbyte> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<sbyte>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static List<sbyte> CumulativeSum(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);
            sbyte sum = (sbyte)0;

            foreach (var value in self)
            {
                sum += value;
                result.Add(sum);
            }

            return result;
        }

        public static void FillCumulativeSum(this IList<sbyte> self)
        {
            sbyte sum = (sbyte)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static List<decimal> Sort(this IList<decimal> self)
        {
            var result = new List<decimal>(self);
            result.Sort();
            return result;
        }

        public static void FillSort(this IList<decimal> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<decimal>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static List<decimal> CumulativeSum(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);
            decimal sum = (decimal)0;

            foreach (var value in self)
            {
                sum += value;
                result.Add(sum);
            }

            return result;
        }

        public static void FillCumulativeSum(this IList<decimal> self)
        {
            decimal sum = (decimal)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }
    }
}