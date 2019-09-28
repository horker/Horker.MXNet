﻿using System;
using System.Collections;
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

        public static List<double> CumulativeMax(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            if (self.Count == 0)
                return result;

            double max = (double)self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
                result.Add(max);
            }

            return result;
        }

        public static void CumulativeMaxFill(this IList<double> self)
        {
            if (self.Count == 0)
                return;

            double max = (double)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
                self[i] = max;
            }
        }

        public static List<double> CumulativeMin(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            if (self.Count == 0)
                return result;

            double min = (double)self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
                result.Add(min);
            }

            return result;
        }

        public static void CumulativeMinFill(this IList<double> self)
        {
            if (self.Count == 0)
                return;

            double min = (double)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
                self[i] = min;
            }
        }

        public static List<double> CumulativeProduct(this IList<double> self)
        {
            var result = new List<double>(self.Count);
            double product = (double)1;

            foreach (var value in self)
            {
                product *= value;
                result.Add(product);
            }

            return result;
        }

        public static void CumulativeProductFill(this IList<double> self)
        {
            double product = (double)1;

            var i = 0;
            foreach (var value in self)
            {
                product *= value;
                self[i++] = product;
            }
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
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<double> GetUnique(this IList<double> self)
        {
            var unique = new HashSet<double>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<double> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<double> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            double sum = (double)0;
            foreach (var e in sorted)
                sum += (double)e;

            var summary = new Summary();
            summary.Count = count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            summary.Mean = (double)(sum / count);
            summary.Min = sorted[0];
            summary.Q25 = (double)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Median = (double)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q75 = (double)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static IList<double> RemoveNaN(this IList<double> self)
        {
            var result = new List<double>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<double> FillNaN(this IList<double> self, double fillValue)
        {
            var result = new List<double>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill(this IList<double> self, double fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
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

        public static List<float> CumulativeMax(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            if (self.Count == 0)
                return result;

            float max = (float)self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
                result.Add(max);
            }

            return result;
        }

        public static void CumulativeMaxFill(this IList<float> self)
        {
            if (self.Count == 0)
                return;

            float max = (float)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
                self[i] = max;
            }
        }

        public static List<float> CumulativeMin(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            if (self.Count == 0)
                return result;

            float min = (float)self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
                result.Add(min);
            }

            return result;
        }

        public static void CumulativeMinFill(this IList<float> self)
        {
            if (self.Count == 0)
                return;

            float min = (float)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
                self[i] = min;
            }
        }

        public static List<float> CumulativeProduct(this IList<float> self)
        {
            var result = new List<float>(self.Count);
            float product = (float)1;

            foreach (var value in self)
            {
                product *= value;
                result.Add(product);
            }

            return result;
        }

        public static void CumulativeProductFill(this IList<float> self)
        {
            float product = (float)1;

            var i = 0;
            foreach (var value in self)
            {
                product *= value;
                self[i++] = product;
            }
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
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<float> GetUnique(this IList<float> self)
        {
            var unique = new HashSet<float>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<float> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<float> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            float sum = (float)0;
            foreach (var e in sorted)
                sum += (float)e;

            var summary = new Summary();
            summary.Count = count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            summary.Mean = (float)(sum / count);
            summary.Min = sorted[0];
            summary.Q25 = (float)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Median = (float)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q75 = (float)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static IList<float> RemoveNaN(this IList<float> self)
        {
            var result = new List<float>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<float> FillNaN(this IList<float> self, float fillValue)
        {
            var result = new List<float>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill(this IList<float> self, float fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
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

        public static List<long> CumulativeMax(this IList<long> self)
        {
            var result = new List<long>(self.Count);

            if (self.Count == 0)
                return result;

            long max = (long)self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
                result.Add(max);
            }

            return result;
        }

        public static void CumulativeMaxFill(this IList<long> self)
        {
            if (self.Count == 0)
                return;

            long max = (long)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
                self[i] = max;
            }
        }

        public static List<long> CumulativeMin(this IList<long> self)
        {
            var result = new List<long>(self.Count);

            if (self.Count == 0)
                return result;

            long min = (long)self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
                result.Add(min);
            }

            return result;
        }

        public static void CumulativeMinFill(this IList<long> self)
        {
            if (self.Count == 0)
                return;

            long min = (long)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
                self[i] = min;
            }
        }

        public static List<long> CumulativeProduct(this IList<long> self)
        {
            var result = new List<long>(self.Count);
            long product = (long)1;

            foreach (var value in self)
            {
                product *= value;
                result.Add(product);
            }

            return result;
        }

        public static void CumulativeProductFill(this IList<long> self)
        {
            long product = (long)1;

            var i = 0;
            foreach (var value in self)
            {
                product *= value;
                self[i++] = product;
            }
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
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<long> GetUnique(this IList<long> self)
        {
            var unique = new HashSet<long>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<long> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<long> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            long sum = (long)0;
            foreach (var e in sorted)
                sum += (long)e;

            var summary = new Summary();
            summary.Count = count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            summary.Mean = (long)(sum / count);
            summary.Min = sorted[0];
            summary.Q25 = (long)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Median = (long)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q75 = (long)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static IList<long> RemoveNaN(this IList<long> self)
        {
            var result = new List<long>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<long> FillNaN(this IList<long> self, long fillValue)
        {
            var result = new List<long>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill(this IList<long> self, long fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
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

        public static List<int> CumulativeMax(this IList<int> self)
        {
            var result = new List<int>(self.Count);

            if (self.Count == 0)
                return result;

            int max = (int)self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
                result.Add(max);
            }

            return result;
        }

        public static void CumulativeMaxFill(this IList<int> self)
        {
            if (self.Count == 0)
                return;

            int max = (int)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
                self[i] = max;
            }
        }

        public static List<int> CumulativeMin(this IList<int> self)
        {
            var result = new List<int>(self.Count);

            if (self.Count == 0)
                return result;

            int min = (int)self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
                result.Add(min);
            }

            return result;
        }

        public static void CumulativeMinFill(this IList<int> self)
        {
            if (self.Count == 0)
                return;

            int min = (int)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
                self[i] = min;
            }
        }

        public static List<int> CumulativeProduct(this IList<int> self)
        {
            var result = new List<int>(self.Count);
            int product = (int)1;

            foreach (var value in self)
            {
                product *= value;
                result.Add(product);
            }

            return result;
        }

        public static void CumulativeProductFill(this IList<int> self)
        {
            int product = (int)1;

            var i = 0;
            foreach (var value in self)
            {
                product *= value;
                self[i++] = product;
            }
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
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<int> GetUnique(this IList<int> self)
        {
            var unique = new HashSet<int>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<int> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<int> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            int sum = (int)0;
            foreach (var e in sorted)
                sum += (int)e;

            var summary = new Summary();
            summary.Count = count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            summary.Mean = (int)(sum / count);
            summary.Min = sorted[0];
            summary.Q25 = (int)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Median = (int)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q75 = (int)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static IList<int> RemoveNaN(this IList<int> self)
        {
            var result = new List<int>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<int> FillNaN(this IList<int> self, int fillValue)
        {
            var result = new List<int>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill(this IList<int> self, int fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
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

        public static List<short> CumulativeMax(this IList<short> self)
        {
            var result = new List<short>(self.Count);

            if (self.Count == 0)
                return result;

            short max = (short)self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
                result.Add(max);
            }

            return result;
        }

        public static void CumulativeMaxFill(this IList<short> self)
        {
            if (self.Count == 0)
                return;

            short max = (short)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
                self[i] = max;
            }
        }

        public static List<short> CumulativeMin(this IList<short> self)
        {
            var result = new List<short>(self.Count);

            if (self.Count == 0)
                return result;

            short min = (short)self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
                result.Add(min);
            }

            return result;
        }

        public static void CumulativeMinFill(this IList<short> self)
        {
            if (self.Count == 0)
                return;

            short min = (short)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
                self[i] = min;
            }
        }

        public static List<short> CumulativeProduct(this IList<short> self)
        {
            var result = new List<short>(self.Count);
            short product = (short)1;

            foreach (var value in self)
            {
                product *= value;
                result.Add(product);
            }

            return result;
        }

        public static void CumulativeProductFill(this IList<short> self)
        {
            short product = (short)1;

            var i = 0;
            foreach (var value in self)
            {
                product *= value;
                self[i++] = product;
            }
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
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<short> GetUnique(this IList<short> self)
        {
            var unique = new HashSet<short>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<short> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<short> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            short sum = (short)0;
            foreach (var e in sorted)
                sum += (short)e;

            var summary = new Summary();
            summary.Count = count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            summary.Mean = (short)(sum / count);
            summary.Min = sorted[0];
            summary.Q25 = (short)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Median = (short)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q75 = (short)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static IList<short> RemoveNaN(this IList<short> self)
        {
            var result = new List<short>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<short> FillNaN(this IList<short> self, short fillValue)
        {
            var result = new List<short>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill(this IList<short> self, short fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
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

        public static List<byte> CumulativeMax(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);

            if (self.Count == 0)
                return result;

            byte max = (byte)self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
                result.Add(max);
            }

            return result;
        }

        public static void CumulativeMaxFill(this IList<byte> self)
        {
            if (self.Count == 0)
                return;

            byte max = (byte)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
                self[i] = max;
            }
        }

        public static List<byte> CumulativeMin(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);

            if (self.Count == 0)
                return result;

            byte min = (byte)self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
                result.Add(min);
            }

            return result;
        }

        public static void CumulativeMinFill(this IList<byte> self)
        {
            if (self.Count == 0)
                return;

            byte min = (byte)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
                self[i] = min;
            }
        }

        public static List<byte> CumulativeProduct(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);
            byte product = (byte)1;

            foreach (var value in self)
            {
                product *= value;
                result.Add(product);
            }

            return result;
        }

        public static void CumulativeProductFill(this IList<byte> self)
        {
            byte product = (byte)1;

            var i = 0;
            foreach (var value in self)
            {
                product *= value;
                self[i++] = product;
            }
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
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<byte> GetUnique(this IList<byte> self)
        {
            var unique = new HashSet<byte>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<byte> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<byte> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            byte sum = (byte)0;
            foreach (var e in sorted)
                sum += (byte)e;

            var summary = new Summary();
            summary.Count = count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            summary.Mean = (byte)(sum / count);
            summary.Min = sorted[0];
            summary.Q25 = (byte)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Median = (byte)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q75 = (byte)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static IList<byte> RemoveNaN(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<byte> FillNaN(this IList<byte> self, byte fillValue)
        {
            var result = new List<byte>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill(this IList<byte> self, byte fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
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

        public static List<sbyte> CumulativeMax(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);

            if (self.Count == 0)
                return result;

            sbyte max = (sbyte)self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
                result.Add(max);
            }

            return result;
        }

        public static void CumulativeMaxFill(this IList<sbyte> self)
        {
            if (self.Count == 0)
                return;

            sbyte max = (sbyte)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
                self[i] = max;
            }
        }

        public static List<sbyte> CumulativeMin(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);

            if (self.Count == 0)
                return result;

            sbyte min = (sbyte)self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
                result.Add(min);
            }

            return result;
        }

        public static void CumulativeMinFill(this IList<sbyte> self)
        {
            if (self.Count == 0)
                return;

            sbyte min = (sbyte)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
                self[i] = min;
            }
        }

        public static List<sbyte> CumulativeProduct(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);
            sbyte product = (sbyte)1;

            foreach (var value in self)
            {
                product *= value;
                result.Add(product);
            }

            return result;
        }

        public static void CumulativeProductFill(this IList<sbyte> self)
        {
            sbyte product = (sbyte)1;

            var i = 0;
            foreach (var value in self)
            {
                product *= value;
                self[i++] = product;
            }
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
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<sbyte> GetUnique(this IList<sbyte> self)
        {
            var unique = new HashSet<sbyte>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<sbyte> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<sbyte> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            sbyte sum = (sbyte)0;
            foreach (var e in sorted)
                sum += (sbyte)e;

            var summary = new Summary();
            summary.Count = count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            summary.Mean = (sbyte)(sum / count);
            summary.Min = sorted[0];
            summary.Q25 = (sbyte)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Median = (sbyte)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q75 = (sbyte)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static IList<sbyte> RemoveNaN(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<sbyte> FillNaN(this IList<sbyte> self, sbyte fillValue)
        {
            var result = new List<sbyte>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill(this IList<sbyte> self, sbyte fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
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

        public static List<decimal> CumulativeMax(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);

            if (self.Count == 0)
                return result;

            decimal max = (decimal)self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
                result.Add(max);
            }

            return result;
        }

        public static void CumulativeMaxFill(this IList<decimal> self)
        {
            if (self.Count == 0)
                return;

            decimal max = (decimal)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
                self[i] = max;
            }
        }

        public static List<decimal> CumulativeMin(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);

            if (self.Count == 0)
                return result;

            decimal min = (decimal)self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
                result.Add(min);
            }

            return result;
        }

        public static void CumulativeMinFill(this IList<decimal> self)
        {
            if (self.Count == 0)
                return;

            decimal min = (decimal)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
                self[i] = min;
            }
        }

        public static List<decimal> CumulativeProduct(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);
            decimal product = (decimal)1;

            foreach (var value in self)
            {
                product *= value;
                result.Add(product);
            }

            return result;
        }

        public static void CumulativeProductFill(this IList<decimal> self)
        {
            decimal product = (decimal)1;

            var i = 0;
            foreach (var value in self)
            {
                product *= value;
                self[i++] = product;
            }
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
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<decimal> GetUnique(this IList<decimal> self)
        {
            var unique = new HashSet<decimal>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<decimal> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<decimal> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            decimal sum = (decimal)0;
            foreach (var e in sorted)
                sum += (decimal)e;

            var summary = new Summary();
            summary.Count = count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            summary.Mean = (decimal)(sum / count);
            summary.Min = sorted[0];
            summary.Q25 = (decimal)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Median = (decimal)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q75 = (decimal)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        public static IList<decimal> RemoveNaN(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<decimal> FillNaN(this IList<decimal> self, decimal fillValue)
        {
            var result = new List<decimal>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill(this IList<decimal> self, decimal fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static void SortFill(this IList<bool> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.GetGenericTypeDefinition() == typeof(List<>))
            {
                var l = self as List<bool>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static int CountNaN(this IList<bool> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<bool> GetUnique(this IList<bool> self)
        {
            var unique = new HashSet<bool>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<bool> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<bool> self)
        {
            var summary = new Summary();
            summary.Count = self.Count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            return summary;
        }

        public static IList<bool> RemoveNaN(this IList<bool> self)
        {
            var result = new List<bool>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<bool> FillNaN(this IList<bool> self, bool fillValue)
        {
            var result = new List<bool>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(value);
                else
                    result.Add(fillValue);
            }

            return result;
        }

        public static void FillNaNFill(this IList<bool> self, bool fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static void SortFill(this IList<DateTime> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.GetGenericTypeDefinition() == typeof(List<>))
            {
                var l = self as List<DateTime>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static int CountNaN(this IList<DateTime> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<DateTime> GetUnique(this IList<DateTime> self)
        {
            var unique = new HashSet<DateTime>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<DateTime> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<DateTime> self)
        {
            var summary = new Summary();
            summary.Count = self.Count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            return summary;
        }

        public static IList<DateTime> RemoveNaN(this IList<DateTime> self)
        {
            var result = new List<DateTime>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<DateTime> FillNaN(this IList<DateTime> self, DateTime fillValue)
        {
            var result = new List<DateTime>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(value);
                else
                    result.Add(fillValue);
            }

            return result;
        }

        public static void FillNaNFill(this IList<DateTime> self, DateTime fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static void SortFill(this IList<DateTimeOffset> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.GetGenericTypeDefinition() == typeof(List<>))
            {
                var l = self as List<DateTimeOffset>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static int CountNaN(this IList<DateTimeOffset> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<DateTimeOffset> GetUnique(this IList<DateTimeOffset> self)
        {
            var unique = new HashSet<DateTimeOffset>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<DateTimeOffset> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<DateTimeOffset> self)
        {
            var summary = new Summary();
            summary.Count = self.Count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            return summary;
        }

        public static IList<DateTimeOffset> RemoveNaN(this IList<DateTimeOffset> self)
        {
            var result = new List<DateTimeOffset>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<DateTimeOffset> FillNaN(this IList<DateTimeOffset> self, DateTimeOffset fillValue)
        {
            var result = new List<DateTimeOffset>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(value);
                else
                    result.Add(fillValue);
            }

            return result;
        }

        public static void FillNaNFill(this IList<DateTimeOffset> self, DateTimeOffset fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static void SortFill(this IList<string> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.GetGenericTypeDefinition() == typeof(List<>))
            {
                var l = self as List<string>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static int CountNaN(this IList<string> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<string> GetUnique(this IList<string> self)
        {
            var unique = new HashSet<string>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<string> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<string> self)
        {
            var summary = new Summary();
            summary.Count = self.Count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            return summary;
        }

        public static IList<string> RemoveNaN(this IList<string> self)
        {
            var result = new List<string>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<string> FillNaN(this IList<string> self, string fillValue)
        {
            var result = new List<string>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(value);
                else
                    result.Add(fillValue);
            }

            return result;
        }

        public static void FillNaNFill(this IList<string> self, string fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }
    }

	public static partial class IListExtensions
	{

        public static void SortFill(this IList self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.GetGenericTypeDefinition() == typeof(List<>))
            {
                var l = self as List<object>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static int CountNaN(this IList self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList GetUnique(this IList self)
        {
            var unique = new HashSet<object>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList self)
        {
            var summary = new Summary();
            summary.Count = self.Count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            return summary;
        }

        public static IList RemoveNaN(this IList self)
        {
            var result = new List<object>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList FillNaN(this IList self, object fillValue)
        {
            var result = new List<object>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(value);
                else
                    result.Add(fillValue);
            }

            return result;
        }

        public static void FillNaNFill(this IList self, object fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }
    }
}
