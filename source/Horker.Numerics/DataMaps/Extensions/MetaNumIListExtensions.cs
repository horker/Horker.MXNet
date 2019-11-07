using Horker.Numerics.DataMaps.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions.Internal
{
    public static partial class MetaNumIListExtensions
    {
        // CUT ABOVE
        public static MetaFloat Correlation(this IList<MetaNum> self, IList<MetaNum> other, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return MetaFloat.NaN;

            if (skipNaN)
            {
                var s1 = new List<MetaNum>(self.Count);
                var s2 = new List<MetaNum>(self.Count);

                for (var i = 0; i < self.Count; ++i)
                {
                    if (TypeTrait<MetaNum>.IsNaN(self[i]) || TypeTrait<MetaNum>.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
        }

        public static MetaFloat Cor(this IList<MetaNum> self, IList<MetaNum> other, bool skipNaN = true)
        {
            return Correlation(self, other, skipNaN);
        }

        public static MetaFloat Covariance(this IList<MetaNum> self, IList<MetaNum> other, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return MetaFloat.NaN;

            var mean0 = self.Mean(skipNaN);
            var mean1 = other.Mean(skipNaN);

            if (MetaFloat.IsNaN(mean0) || MetaFloat.IsNaN(mean1))
                return MetaFloat.NaN;

            int actualCount = self.Count;

            MetaFloat c = (MetaFloat)0.0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<MetaNum>.IsNaN(self[i]) || TypeTrait<MetaNum>.IsNaN(other[i]))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return MetaFloat.NaN;
                    }
                }
                var a = (MetaFloat)self[i] - mean0;
                var b = (MetaFloat)other[i] - mean1;
                c += a * b;
            }

            if (unbiased)
                return c / (actualCount - 1);
            else
                return c / actualCount;
        }

        public static MetaFloat Cov(this IList<MetaNum> self, IList<MetaNum> other, bool unbiased = true, bool skipNaN = true)
        {
            return Covariance(self, other, unbiased, skipNaN);
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
                if (TypeTrait<MetaNum>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary Describe(this IList<MetaNum> self)
        {
            var sorted = self.RemoveNaN();
            if (sorted.Count > 0)
                sorted.SortFill();

            var summary = new Summary();
            summary.Count = self.Count;
            summary.NaN = self.CountNaN();
            summary.Unique = self.CountUnique();
            summary.Mean = Mean(self);
            summary.Std = StandardDeviation(self);
            summary.Min = sorted.Count > 0 ? (object)sorted[0] : null;
            summary.Q25 = sorted.Quantile(.25, false, true);
            summary.Median = sorted.Quantile(.5, false, true);
            summary.Q75 = sorted.Quantile(.75, false, true);
            summary.Max = sorted.Count > 0 ? (object)sorted[sorted.Count - 1] : null;

            return summary;
        }

        public static MetaFloat Quantile(this IList<MetaNum> self, double p, bool skipNaN = true, bool isSorted = false)
        {
            if (self.Count == 0)
                return MetaFloat.NaN;

            // TODO use partial sort

            IList<MetaNum> sorted;
            if (skipNaN)
            {
                sorted = self.RemoveNaN();
                if (sorted.Count == 0)
                    return MetaFloat.NaN;
                sorted.SortFill();
            }
            else
            {
                if (isSorted)
                {
                    sorted = self;
                }
                else
                {
                    var a = self.ToArray();
                    Array.Sort(a);
                    sorted = a;
                }
            }

            double lowThreshold = 1.0 / (sorted.Count + 1);
            double highThreshold = sorted.Count / (double)(sorted.Count + 1);

            if (p < lowThreshold)
                return (MetaFloat)sorted[0];

            if (p >= highThreshold)
                return (MetaFloat)sorted[sorted.Count - 1];

            double h = (sorted.Count + 1) * p;
            double hc = Math.Floor(h);

            int i = (int)hc;
            if (i > 0)
                i--;

            int i2 = i + 1;
            if (i2 == sorted.Count)
                i2--;

            return (MetaFloat)((double)sorted[i] + (h - hc) * ((double)sorted[i2] - (double)sorted[i]));
        }

        public static List<MetaNum> RemoveNaN(this IList<MetaNum> self)
        {
            var result = new List<MetaNum>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<MetaNum>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<MetaNum> FillNaN(this IList<MetaNum> self, MetaNum fillValue)
        {
            var result = new List<MetaNum>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<MetaNum>.IsNaN(value))
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
                if (TypeTrait<MetaNum>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static MetaFloat Kurtosis(this IList<MetaNum> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return MetaFloat.NaN;

            var mean = (double)self.Mean();

            double n = self.Count;

            double s2 = 0;
            double s4 = 0;

            for (int i = 0; i < self.Count; i++)
            {
                double dev = (double)self[i] - mean;

                s2 += dev * dev;
                s4 += dev * dev * dev * dev;
            }

            double m2 = s2 / n;
            double m4 = s4 / n;

            if (unbiased)
            {
                double v = s2 / (n - 1);

                double a = (n * (n + 1)) / ((n - 1) * (n - 2) * (n - 3));
                double b = s4 / (v * v);
                double c = ((n - 1) * (n - 1)) / ((n - 2) * (n - 3));

                return (MetaFloat)(a * b - 3 * c);
            }
            else
            {
                return (MetaFloat)(m4 / (m2 * m2) - 3);
            }
        }

        public static MetaNum Max(this IList<MetaNum> self)
        {
            if (self.Count == 0)
                return TypeTrait<MetaNum>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<MetaNum>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            MetaNum max = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
            }

            return max;
        }

        public static MetaNum Min(this IList<MetaNum> self)
        {
            if (self.Count == 0)
                return TypeTrait<MetaNum>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<MetaNum>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            MetaNum min = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
            }

            return min;
        }

        public static MetaFloat Mean(this IList<MetaNum> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return MetaFloat.NaN;

            MetaFloat mean = (MetaFloat)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                MetaFloat v = (MetaFloat)value;
                if (MetaFloat.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                        return MetaFloat.NaN;
                }

                mean += v;
            }

            return mean / actualCount;
        }

        public static MetaFloat Median(this IList<MetaNum> self, bool skipNaN = true, bool isSorted = false)
        {
            return Quantile(self, .5, skipNaN, isSorted);
        }

        public static MetaNum Mode(this IList<MetaNum> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return TypeTrait<MetaNum>.GetNaNOrRaiseException("No elements");

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
                while (TypeTrait<MetaNum>.IsNaN(values[i]))
                    ++i;
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

        public static MetaFloat Skewness(this IList<MetaNum> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return MetaFloat.NaN;

            double mean = (double)self.Mean();
            double n = self.Count;

            double s2 = 0;
            double s3 = 0;

            for (int i = 0; i < self.Count; ++i)
            {
                double dev = (double)self[i] - mean;

                s2 += dev * dev;
                s3 += dev * dev * dev;
            }

            double m2 = s2 / n;
            double m3 = s3 / n;

            double g = m3 / (Math.Pow(m2, 3 / 2.0));

            if (unbiased)
            {
                double a = Math.Sqrt(n * (n - 1));
                double b = n - 2;
                return (MetaFloat)((a / b) * g);
            }
            else
            {
                return (MetaFloat)g;
            }
        }

        public static MetaFloat Variance(this IList<MetaNum> self, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count == 0)
                return MetaFloat.NaN;

            MetaFloat mean = Mean(self, skipNaN);
            if (MetaFloat.IsNaN(mean))
                return MetaFloat.NaN;

            MetaFloat variance = (MetaFloat)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                MetaFloat v = (MetaFloat)value;
                if (TypeTrait<MetaFloat>.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return MetaFloat.NaN;
                    }
                }

                MetaFloat x = v - mean;
                variance += x * x;
            }

            if (unbiased)
                return variance / (actualCount - 1);
            else
                return variance / actualCount;
        }

        public static MetaFloat Var(this IList<MetaNum> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // Histogram

        private static Tuple<int[], int> CollectHistogram(IList<MetaNum> data, HistogramInterval intervals)
        {
            var result = new int[intervals.BinCount];
            var total = 0;

            foreach (var value in data)
            {
                if (TypeTrait<MetaNum>.IsNaN(value))
                    continue;

                var bin = (int)Math.Floor(((double)value - intervals.AdjustedLower) / intervals.BinWidth);
                ++result[bin];
                ++total;
            }

            return Tuple.Create(result, total);
        }

        public static HistogramBin[] Histogram(this IList<MetaNum> self, int binCount = -1, double binWidth = double.NaN)
        {
            var min = (double)self.Min();
            var max = (double)self.Max();
            HistogramInterval intervals;

            if (!double.IsNaN(binWidth))
            {
                intervals = HistogramHelper.GetHistogramIntervalFromBinWidth(min, max, binWidth);
            }
            else
            {
                if (binCount < 0)
                    binCount = HistogramHelper.GetBinCount(min, max, self.Count);

                intervals = HistogramHelper.GetHistogramIntervalFromBinCount(min, max, binCount);
            }

            var collect = CollectHistogram(self, intervals);
            var counts = collect.Item1;
            var total = collect.Item2;
            return HistogramBin.CreateHistogram(intervals, counts, total);
        }

        // CUT BELOW
    }
}
