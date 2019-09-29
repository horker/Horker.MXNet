using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions
{
    public static partial class MetaNumIListExtensions
    {
        // CUT ABOVE
        public static List<MetaNum> Sort(this IList<MetaNum> self)
        {
            var result = new List<MetaNum>(self);
            result.Sort();
            return result;
        }

        public static void SortFill(this IList<MetaNum> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<MetaNum>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static List<MetaNum> CumulativeMax(this IList<MetaNum> self)
        {
            var result = new List<MetaNum>(self.Count);

            if (self.Count == 0)
                return result;

            MetaNum max = (MetaNum)self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
                result.Add(max);
            }

            return result;
        }

        public static void CumulativeMaxFill(this IList<MetaNum> self)
        {
            if (self.Count == 0)
                return;

            MetaNum max = (MetaNum)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
                self[i] = max;
            }
        }

        public static List<MetaNum> CumulativeMin(this IList<MetaNum> self)
        {
            var result = new List<MetaNum>(self.Count);

            if (self.Count == 0)
                return result;

            MetaNum min = (MetaNum)self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
                result.Add(min);
            }

            return result;
        }

        public static void CumulativeMinFill(this IList<MetaNum> self)
        {
            if (self.Count == 0)
                return;

            MetaNum min = (MetaNum)self[0];

            for (var i = 0; i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
                self[i] = min;
            }
        }

        public static List<MetaNum> CumulativeProduct(this IList<MetaNum> self)
        {
            var result = new List<MetaNum>(self.Count);
            MetaNum product = (MetaNum)1;

            foreach (var value in self)
            {
                product *= value;
                result.Add(product);
            }

            return result;
        }

        public static void CumulativeProductFill(this IList<MetaNum> self)
        {
            MetaNum product = (MetaNum)1;

            var i = 0;
            foreach (var value in self)
            {
                product *= value;
                self[i++] = product;
            }
        }

        public static List<MetaNum> CumulativeSum(this IList<MetaNum> self)
        {
            var result = new List<MetaNum>(self.Count);
            MetaNum sum = (MetaNum)0;

            foreach (var value in self)
            {
                sum += value;
                result.Add(sum);
            }

            return result;
        }

        public static void CumulativeSumFill(this IList<MetaNum> self)
        {
            MetaNum sum = (MetaNum)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        public static int CountNaN(this IList<MetaNum> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<MetaNum> GetUnique(this IList<MetaNum> self)
        {
            var unique = new HashSet<MetaNum>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<MetaNum> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<MetaNum> self)
        {
            var count = self.Count;
            var sorted = self.ToArray();
            Array.Sort(sorted);

            var even = count % 2 == 0;
            var q = count % 4 == 0;

            MetaNum sum = (MetaNum)0;
            foreach (var e in sorted)
                sum += (MetaNum)e;

            var summary = new Summary();
            summary.Count = count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            summary.Mean = (MetaNum)(sum / count);
            summary.Min = sorted[0];
            summary.Q25 = (MetaNum)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Median = (MetaNum)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q75 = (MetaNum)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];
            summary.Std = StandardDeviation(self);

            return summary;
        }

        public static IList<MetaNum> RemoveNaN(this IList<MetaNum> self)
        {
            var result = new List<MetaNum>(self.Count);
            foreach (var value in self)
                if (!TypeTrait.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static IList<MetaNum> FillNaN(this IList<MetaNum> self, MetaNum fillValue)
        {
            var result = new List<MetaNum>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill(this IList<MetaNum> self, MetaNum fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static MetaNum Max(this IList<MetaNum> self, bool skipNaN = true)
        {
            MetaNum max = self[0];

            foreach (var value in self)
            {
                if (value > max)
                    max = value;
            }

            return max;
        }

        public static MetaNum Min(this IList<MetaNum> self, bool skipNaN = true)
        {
            MetaNum min = self[0];

            foreach (var value in self)
            {
                if (value < min)
                    min = value;
            }

            return min;
        }

        public static MetaFloat Mean(this IList<MetaNum> self, bool skipNaN = true)
        {
            MetaFloat mean = (MetaFloat)0.0;

            foreach (var value in self)
            {
                MetaFloat v = (MetaFloat)value;
                if (skipNaN && MetaFloat.IsNaN(v))
                    return MetaFloat.NaN;

                mean += v;
            }

            return mean / self.Count;
        }

        public static MetaFloat Median(this IList<MetaNum> self, bool skipNaN = true)
        {
            // TODO: Accord.NET uses partial sort for efficiency.

            var values = self.ToArray();
            Array.Sort(values);

            if (values.Length % 2 == 0)
                return ((MetaFloat)values[values.Length / 2 - 1] + (MetaFloat)values[values.Length / 2]) / 2;
            else
                return (MetaFloat)values[values.Length / 2];
        }

        public static MetaNum Mode(this IList<MetaNum> self, bool skipNaN = true)
        {
            var values = self.ToArray();
            Array.Sort(values);

            MetaNum currentValue = values[0];
            MetaNum bestValue = currentValue;
            var currentCount = 1;
            var bestCount = 1;

            int i = 1;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                if (typeof(MetaNum) == typeof(double))
                {
                    while (double.IsNaN((double)values[i]))
                        ++i;
                }
                else if (typeof(MetaNum) == typeof(float))
                {
                    while (float.IsNaN((float)values[i]))
                        ++i;
                }
            }

            for (; i < values.Length; ++i) {
                if (currentValue == values[i])
                    currentCount += 1;
                else
                {
                    currentValue = values[i];
                    currentCount = 1;
                }

                if (currentCount > bestCount)
                {
                    bestCount = currentCount;
                    bestValue = currentValue;
                }
            }

            return bestValue;
        }

        public static MetaFloat StandardDeviation(this IList<MetaNum> self, bool unbiased = true, bool skipNaN = true)
        {
            var variance = Variance(self, unbiased, skipNaN);
            return (MetaFloat)Math.Sqrt((double)variance);
        }

        public static MetaFloat Std(this IList<MetaNum> self, bool unbiased = true, bool skipNaN = true)
        {
            return StandardDeviation(self, unbiased, skipNaN);
        }

        public static MetaFloat Variance(this IList<MetaNum> self, bool unbiased = true, bool skipNaN = true)
        {
            MetaFloat mean = Mean(self, skipNaN);
            if (skipNaN && MetaFloat.IsNaN(mean))
                return MetaFloat.NaN;

            MetaFloat variance = (MetaFloat)0.0;

            foreach (var value in self)
            {
                MetaFloat v = (MetaFloat)value;
                if (skipNaN && MetaFloat.IsNaN(v))
                    return MetaFloat.NaN;

                MetaFloat x = v - mean;
                variance += x * x;
            }

            if (unbiased)
            {
                // Sample variance
                return variance / self.Count;
            }
            else
            {
                // Population variance
                return variance / self.Count;
            }
        }

        public static MetaFloat Var(this IList<MetaNum> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // CUT BELOW
    }
}
