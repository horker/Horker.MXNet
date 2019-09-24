﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        public static void SortFill(this IList<double> self)
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

        public static void CumulativeSumFill(this IList<double> self)
        {
            double sum = (double)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static int CountNaN(this IList<double> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary<double> Describe(this IList<double> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            double sum = (double)0;
            foreach (var e in sorted)
                sum += (double)e;

            var summary = new Summary<double>();
            summary.Count = count;
            summary.NaNCount = CountNaN(self);
            summary.Min = sorted[0];
            summary.Q1 = (double)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Mean = (double)(sum / count);
            summary.Median = (double)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q3 = (double)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static List<float> Sort(this IList<float> self)
        {
            var result = new List<float>(self);
            result.Sort();
            return result;
        }

        public static void SortFill(this IList<float> self)
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

        public static void CumulativeSumFill(this IList<float> self)
        {
            float sum = (float)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static int CountNaN(this IList<float> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary<float> Describe(this IList<float> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            float sum = (float)0;
            foreach (var e in sorted)
                sum += (float)e;

            var summary = new Summary<float>();
            summary.Count = count;
            summary.NaNCount = CountNaN(self);
            summary.Min = sorted[0];
            summary.Q1 = (float)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Mean = (float)(sum / count);
            summary.Median = (float)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q3 = (float)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static List<long> Sort(this IList<long> self)
        {
            var result = new List<long>(self);
            result.Sort();
            return result;
        }

        public static void SortFill(this IList<long> self)
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

        public static void CumulativeSumFill(this IList<long> self)
        {
            long sum = (long)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static int CountNaN(this IList<long> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary<long> Describe(this IList<long> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            long sum = (long)0;
            foreach (var e in sorted)
                sum += (long)e;

            var summary = new Summary<long>();
            summary.Count = count;
            summary.NaNCount = CountNaN(self);
            summary.Min = sorted[0];
            summary.Q1 = (long)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Mean = (long)(sum / count);
            summary.Median = (long)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q3 = (long)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static List<int> Sort(this IList<int> self)
        {
            var result = new List<int>(self);
            result.Sort();
            return result;
        }

        public static void SortFill(this IList<int> self)
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

        public static void CumulativeSumFill(this IList<int> self)
        {
            int sum = (int)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static int CountNaN(this IList<int> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary<int> Describe(this IList<int> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            int sum = (int)0;
            foreach (var e in sorted)
                sum += (int)e;

            var summary = new Summary<int>();
            summary.Count = count;
            summary.NaNCount = CountNaN(self);
            summary.Min = sorted[0];
            summary.Q1 = (int)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Mean = (int)(sum / count);
            summary.Median = (int)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q3 = (int)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static List<short> Sort(this IList<short> self)
        {
            var result = new List<short>(self);
            result.Sort();
            return result;
        }

        public static void SortFill(this IList<short> self)
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

        public static void CumulativeSumFill(this IList<short> self)
        {
            short sum = (short)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static int CountNaN(this IList<short> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary<short> Describe(this IList<short> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            short sum = (short)0;
            foreach (var e in sorted)
                sum += (short)e;

            var summary = new Summary<short>();
            summary.Count = count;
            summary.NaNCount = CountNaN(self);
            summary.Min = sorted[0];
            summary.Q1 = (short)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Mean = (short)(sum / count);
            summary.Median = (short)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q3 = (short)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static List<byte> Sort(this IList<byte> self)
        {
            var result = new List<byte>(self);
            result.Sort();
            return result;
        }

        public static void SortFill(this IList<byte> self)
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

        public static void CumulativeSumFill(this IList<byte> self)
        {
            byte sum = (byte)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static int CountNaN(this IList<byte> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary<byte> Describe(this IList<byte> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            byte sum = (byte)0;
            foreach (var e in sorted)
                sum += (byte)e;

            var summary = new Summary<byte>();
            summary.Count = count;
            summary.NaNCount = CountNaN(self);
            summary.Min = sorted[0];
            summary.Q1 = (byte)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Mean = (byte)(sum / count);
            summary.Median = (byte)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q3 = (byte)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static List<sbyte> Sort(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self);
            result.Sort();
            return result;
        }

        public static void SortFill(this IList<sbyte> self)
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

        public static void CumulativeSumFill(this IList<sbyte> self)
        {
            sbyte sum = (sbyte)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static int CountNaN(this IList<sbyte> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary<sbyte> Describe(this IList<sbyte> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            sbyte sum = (sbyte)0;
            foreach (var e in sorted)
                sum += (sbyte)e;

            var summary = new Summary<sbyte>();
            summary.Count = count;
            summary.NaNCount = CountNaN(self);
            summary.Min = sorted[0];
            summary.Q1 = (sbyte)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Mean = (sbyte)(sum / count);
            summary.Median = (sbyte)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q3 = (sbyte)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static List<decimal> Sort(this IList<decimal> self)
        {
            var result = new List<decimal>(self);
            result.Sort();
            return result;
        }

        public static void SortFill(this IList<decimal> self)
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

        public static void CumulativeSumFill(this IList<decimal> self)
        {
            decimal sum = (decimal)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static int CountNaN(this IList<decimal> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary<decimal> Describe(this IList<decimal> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            decimal sum = (decimal)0;
            foreach (var e in sorted)
                sum += (decimal)e;

            var summary = new Summary<decimal>();
            summary.Count = count;
            summary.NaNCount = CountNaN(self);
            summary.Min = sorted[0];
            summary.Q1 = (decimal)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Mean = (decimal)(sum / count);
            summary.Median = (decimal)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q3 = (decimal)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }
    }
}