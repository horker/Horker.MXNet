using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horker.Numerics.DataMaps.Utilities;

namespace Horker.Numerics.DataMaps.Extensions
{
	public static partial class GenericIListExtensions
	{

        public static double Correlation(this IList<double> self, IList<double> other, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            if (skipNaN)
            {
                var s1 = new List<double>(self.Count);
                var s2 = new List<double>(self.Count);

                for (var i = 0; i < self.Count; ++i)
                {
                    if (TypeTrait<double>.IsNaN(self[i]) || TypeTrait<double>.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
        }

        public static double Cor(this IList<double> self, IList<double> other, bool skipNaN = true)
        {
            return Correlation(self, other, skipNaN);
        }

        public static double Covariance(this IList<double> self, IList<double> other, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            var mean0 = self.Mean(skipNaN);
            var mean1 = other.Mean(skipNaN);

            if (double.IsNaN(mean0) || double.IsNaN(mean1))
                return double.NaN;

            int actualCount = self.Count;

            double c = (double)0.0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<double>.IsNaN(self[i]) || TypeTrait<double>.IsNaN(other[i]))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }
                var a = (double)self[i] - mean0;
                var b = (double)other[i] - mean1;
                c += a * b;
            }

            if (unbiased)
                return c / (actualCount - 1);
            else
                return c / actualCount;
        }

        public static double Cov(this IList<double> self, IList<double> other, bool unbiased = true, bool skipNaN = true)
        {
            return Covariance(self, other, unbiased, skipNaN);
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
                if (TypeTrait<double>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary Describe(this IList<double> self)
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

        public static double Quantile(this IList<double> self, double p, bool skipNaN = true, bool isSorted = false)
        {
            if (self.Count == 0)
                return double.NaN;

            // TODO use partial sort

            IList<double> sorted;
            if (skipNaN)
            {
                sorted = self.RemoveNaN();
                if (sorted.Count == 0)
                    return double.NaN;
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
                return (double)sorted[0];

            if (p >= highThreshold)
                return (double)sorted[sorted.Count - 1];

            double h = (sorted.Count + 1) * p;
            double hc = Math.Floor(h);

            int i = (int)hc;
            if (i > 0)
                i--;

            int i2 = i + 1;
            if (i2 == sorted.Count)
                i2--;

            return (double)((double)sorted[i] + (h - hc) * ((double)sorted[i2] - (double)sorted[i]));
        }

        public static List<double> RemoveNaN(this IList<double> self)
        {
            var result = new List<double>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<double>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<double> FillNaN(this IList<double> self, double fillValue)
        {
            var result = new List<double>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<double>.IsNaN(value))
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
                if (TypeTrait<double>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static double Kurtosis(this IList<double> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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

                return (double)(a * b - 3 * c);
            }
            else
            {
                return (double)(m4 / (m2 * m2) - 3);
            }
        }

        public static double Max(this IList<double> self)
        {
            if (self.Count == 0)
                return TypeTrait<double>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<double>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            double max = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
            }

            return max;
        }

        public static double Min(this IList<double> self)
        {
            if (self.Count == 0)
                return TypeTrait<double>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<double>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            double min = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
            }

            return min;
        }

        public static double Mean(this IList<double> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (double.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                        return double.NaN;
                }

                mean += v;
            }

            return mean / actualCount;
        }

        public static double Median(this IList<double> self, bool skipNaN = true, bool isSorted = false)
        {
            return Quantile(self, .5, skipNaN, isSorted);
        }

        public static double Mode(this IList<double> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return TypeTrait<double>.GetNaNOrRaiseException("No elements");

            var values = self.ToArray();
            Array.Sort(values);

            double currentValue = values[0];
            double bestValue = currentValue;
            var currentCount = 1;
            var bestCount = 1;

            int i = 1;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait<double>.IsNaN(values[i]))
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

        public static double StandardDeviation(this IList<double> self, bool unbiased = true, bool skipNaN = true)
        {
            var variance = Variance(self, unbiased, skipNaN);
            return (double)Math.Sqrt((double)variance);
        }

        public static double Std(this IList<double> self, bool unbiased = true, bool skipNaN = true)
        {
            return StandardDeviation(self, unbiased, skipNaN);
        }

        public static double Skewness(this IList<double> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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
                return (double)((a / b) * g);
            }
            else
            {
                return (double)g;
            }
        }

        public static double Variance(this IList<double> self, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait<double>.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }

                double x = v - mean;
                variance += x * x;
            }

            if (unbiased)
                return variance / (actualCount - 1);
            else
                return variance / actualCount;
        }

        public static double Var(this IList<double> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // Histogram

        private static Tuple<int[], int> CollectHistogram(IList<double> data, HistogramInterval intervals)
        {
            var result = new int[intervals.BinCount];
            var total = 0;

            foreach (var value in data)
            {
                if (TypeTrait<double>.IsNaN(value))
                    continue;

                var bin = (int)Math.Floor(((double)value - intervals.AdjustedLower) / intervals.BinWidth);
                ++result[bin];
                ++total;
            }

            return Tuple.Create(result, total);
        }

        public static HistogramBin[] Histogram(this IList<double> self, int binCount = -1, double binWidth = double.NaN)
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

        public static float Correlation(this IList<float> self, IList<float> other, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return float.NaN;

            if (skipNaN)
            {
                var s1 = new List<float>(self.Count);
                var s2 = new List<float>(self.Count);

                for (var i = 0; i < self.Count; ++i)
                {
                    if (TypeTrait<float>.IsNaN(self[i]) || TypeTrait<float>.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
        }

        public static float Cor(this IList<float> self, IList<float> other, bool skipNaN = true)
        {
            return Correlation(self, other, skipNaN);
        }

        public static float Covariance(this IList<float> self, IList<float> other, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return float.NaN;

            var mean0 = self.Mean(skipNaN);
            var mean1 = other.Mean(skipNaN);

            if (float.IsNaN(mean0) || float.IsNaN(mean1))
                return float.NaN;

            int actualCount = self.Count;

            float c = (float)0.0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<float>.IsNaN(self[i]) || TypeTrait<float>.IsNaN(other[i]))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return float.NaN;
                    }
                }
                var a = (float)self[i] - mean0;
                var b = (float)other[i] - mean1;
                c += a * b;
            }

            if (unbiased)
                return c / (actualCount - 1);
            else
                return c / actualCount;
        }

        public static float Cov(this IList<float> self, IList<float> other, bool unbiased = true, bool skipNaN = true)
        {
            return Covariance(self, other, unbiased, skipNaN);
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
                if (TypeTrait<float>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary Describe(this IList<float> self)
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

        public static float Quantile(this IList<float> self, double p, bool skipNaN = true, bool isSorted = false)
        {
            if (self.Count == 0)
                return float.NaN;

            // TODO use partial sort

            IList<float> sorted;
            if (skipNaN)
            {
                sorted = self.RemoveNaN();
                if (sorted.Count == 0)
                    return float.NaN;
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
                return (float)sorted[0];

            if (p >= highThreshold)
                return (float)sorted[sorted.Count - 1];

            double h = (sorted.Count + 1) * p;
            double hc = Math.Floor(h);

            int i = (int)hc;
            if (i > 0)
                i--;

            int i2 = i + 1;
            if (i2 == sorted.Count)
                i2--;

            return (float)((double)sorted[i] + (h - hc) * ((double)sorted[i2] - (double)sorted[i]));
        }

        public static List<float> RemoveNaN(this IList<float> self)
        {
            var result = new List<float>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<float>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<float> FillNaN(this IList<float> self, float fillValue)
        {
            var result = new List<float>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<float>.IsNaN(value))
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
                if (TypeTrait<float>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static float Kurtosis(this IList<float> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return float.NaN;

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

                return (float)(a * b - 3 * c);
            }
            else
            {
                return (float)(m4 / (m2 * m2) - 3);
            }
        }

        public static float Max(this IList<float> self)
        {
            if (self.Count == 0)
                return TypeTrait<float>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<float>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            float max = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
            }

            return max;
        }

        public static float Min(this IList<float> self)
        {
            if (self.Count == 0)
                return TypeTrait<float>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<float>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            float min = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
            }

            return min;
        }

        public static float Mean(this IList<float> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return float.NaN;

            float mean = (float)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                float v = (float)value;
                if (float.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                        return float.NaN;
                }

                mean += v;
            }

            return mean / actualCount;
        }

        public static float Median(this IList<float> self, bool skipNaN = true, bool isSorted = false)
        {
            return Quantile(self, .5, skipNaN, isSorted);
        }

        public static float Mode(this IList<float> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return TypeTrait<float>.GetNaNOrRaiseException("No elements");

            var values = self.ToArray();
            Array.Sort(values);

            float currentValue = values[0];
            float bestValue = currentValue;
            var currentCount = 1;
            var bestCount = 1;

            int i = 1;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait<float>.IsNaN(values[i]))
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

        public static float StandardDeviation(this IList<float> self, bool unbiased = true, bool skipNaN = true)
        {
            var variance = Variance(self, unbiased, skipNaN);
            return (float)Math.Sqrt((double)variance);
        }

        public static float Std(this IList<float> self, bool unbiased = true, bool skipNaN = true)
        {
            return StandardDeviation(self, unbiased, skipNaN);
        }

        public static float Skewness(this IList<float> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return float.NaN;

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
                return (float)((a / b) * g);
            }
            else
            {
                return (float)g;
            }
        }

        public static float Variance(this IList<float> self, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count == 0)
                return float.NaN;

            float mean = Mean(self, skipNaN);
            if (float.IsNaN(mean))
                return float.NaN;

            float variance = (float)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                float v = (float)value;
                if (TypeTrait<float>.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return float.NaN;
                    }
                }

                float x = v - mean;
                variance += x * x;
            }

            if (unbiased)
                return variance / (actualCount - 1);
            else
                return variance / actualCount;
        }

        public static float Var(this IList<float> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // Histogram

        private static Tuple<int[], int> CollectHistogram(IList<float> data, HistogramInterval intervals)
        {
            var result = new int[intervals.BinCount];
            var total = 0;

            foreach (var value in data)
            {
                if (TypeTrait<float>.IsNaN(value))
                    continue;

                var bin = (int)Math.Floor(((double)value - intervals.AdjustedLower) / intervals.BinWidth);
                ++result[bin];
                ++total;
            }

            return Tuple.Create(result, total);
        }

        public static HistogramBin[] Histogram(this IList<float> self, int binCount = -1, double binWidth = double.NaN)
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

        public static double Correlation(this IList<long> self, IList<long> other, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            if (skipNaN)
            {
                var s1 = new List<long>(self.Count);
                var s2 = new List<long>(self.Count);

                for (var i = 0; i < self.Count; ++i)
                {
                    if (TypeTrait<long>.IsNaN(self[i]) || TypeTrait<long>.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
        }

        public static double Cor(this IList<long> self, IList<long> other, bool skipNaN = true)
        {
            return Correlation(self, other, skipNaN);
        }

        public static double Covariance(this IList<long> self, IList<long> other, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            var mean0 = self.Mean(skipNaN);
            var mean1 = other.Mean(skipNaN);

            if (double.IsNaN(mean0) || double.IsNaN(mean1))
                return double.NaN;

            int actualCount = self.Count;

            double c = (double)0.0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<long>.IsNaN(self[i]) || TypeTrait<long>.IsNaN(other[i]))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }
                var a = (double)self[i] - mean0;
                var b = (double)other[i] - mean1;
                c += a * b;
            }

            if (unbiased)
                return c / (actualCount - 1);
            else
                return c / actualCount;
        }

        public static double Cov(this IList<long> self, IList<long> other, bool unbiased = true, bool skipNaN = true)
        {
            return Covariance(self, other, unbiased, skipNaN);
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
                if (TypeTrait<long>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary Describe(this IList<long> self)
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

        public static double Quantile(this IList<long> self, double p, bool skipNaN = true, bool isSorted = false)
        {
            if (self.Count == 0)
                return double.NaN;

            // TODO use partial sort

            IList<long> sorted;
            if (skipNaN)
            {
                sorted = self.RemoveNaN();
                if (sorted.Count == 0)
                    return double.NaN;
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
                return (double)sorted[0];

            if (p >= highThreshold)
                return (double)sorted[sorted.Count - 1];

            double h = (sorted.Count + 1) * p;
            double hc = Math.Floor(h);

            int i = (int)hc;
            if (i > 0)
                i--;

            int i2 = i + 1;
            if (i2 == sorted.Count)
                i2--;

            return (double)((double)sorted[i] + (h - hc) * ((double)sorted[i2] - (double)sorted[i]));
        }

        public static List<long> RemoveNaN(this IList<long> self)
        {
            var result = new List<long>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<long>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<long> FillNaN(this IList<long> self, long fillValue)
        {
            var result = new List<long>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<long>.IsNaN(value))
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
                if (TypeTrait<long>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static double Kurtosis(this IList<long> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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

                return (double)(a * b - 3 * c);
            }
            else
            {
                return (double)(m4 / (m2 * m2) - 3);
            }
        }

        public static long Max(this IList<long> self)
        {
            if (self.Count == 0)
                return TypeTrait<long>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<long>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            long max = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
            }

            return max;
        }

        public static long Min(this IList<long> self)
        {
            if (self.Count == 0)
                return TypeTrait<long>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<long>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            long min = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
            }

            return min;
        }

        public static double Mean(this IList<long> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (double.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                        return double.NaN;
                }

                mean += v;
            }

            return mean / actualCount;
        }

        public static double Median(this IList<long> self, bool skipNaN = true, bool isSorted = false)
        {
            return Quantile(self, .5, skipNaN, isSorted);
        }

        public static long Mode(this IList<long> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return TypeTrait<long>.GetNaNOrRaiseException("No elements");

            var values = self.ToArray();
            Array.Sort(values);

            long currentValue = values[0];
            long bestValue = currentValue;
            var currentCount = 1;
            var bestCount = 1;

            int i = 1;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait<long>.IsNaN(values[i]))
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

        public static double StandardDeviation(this IList<long> self, bool unbiased = true, bool skipNaN = true)
        {
            var variance = Variance(self, unbiased, skipNaN);
            return (double)Math.Sqrt((double)variance);
        }

        public static double Std(this IList<long> self, bool unbiased = true, bool skipNaN = true)
        {
            return StandardDeviation(self, unbiased, skipNaN);
        }

        public static double Skewness(this IList<long> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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
                return (double)((a / b) * g);
            }
            else
            {
                return (double)g;
            }
        }

        public static double Variance(this IList<long> self, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait<double>.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }

                double x = v - mean;
                variance += x * x;
            }

            if (unbiased)
                return variance / (actualCount - 1);
            else
                return variance / actualCount;
        }

        public static double Var(this IList<long> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // Histogram

        private static Tuple<int[], int> CollectHistogram(IList<long> data, HistogramInterval intervals)
        {
            var result = new int[intervals.BinCount];
            var total = 0;

            foreach (var value in data)
            {
                if (TypeTrait<long>.IsNaN(value))
                    continue;

                var bin = (int)Math.Floor(((double)value - intervals.AdjustedLower) / intervals.BinWidth);
                ++result[bin];
                ++total;
            }

            return Tuple.Create(result, total);
        }

        public static HistogramBin[] Histogram(this IList<long> self, int binCount = -1, double binWidth = double.NaN)
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

        public static double Correlation(this IList<int> self, IList<int> other, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            if (skipNaN)
            {
                var s1 = new List<int>(self.Count);
                var s2 = new List<int>(self.Count);

                for (var i = 0; i < self.Count; ++i)
                {
                    if (TypeTrait<int>.IsNaN(self[i]) || TypeTrait<int>.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
        }

        public static double Cor(this IList<int> self, IList<int> other, bool skipNaN = true)
        {
            return Correlation(self, other, skipNaN);
        }

        public static double Covariance(this IList<int> self, IList<int> other, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            var mean0 = self.Mean(skipNaN);
            var mean1 = other.Mean(skipNaN);

            if (double.IsNaN(mean0) || double.IsNaN(mean1))
                return double.NaN;

            int actualCount = self.Count;

            double c = (double)0.0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<int>.IsNaN(self[i]) || TypeTrait<int>.IsNaN(other[i]))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }
                var a = (double)self[i] - mean0;
                var b = (double)other[i] - mean1;
                c += a * b;
            }

            if (unbiased)
                return c / (actualCount - 1);
            else
                return c / actualCount;
        }

        public static double Cov(this IList<int> self, IList<int> other, bool unbiased = true, bool skipNaN = true)
        {
            return Covariance(self, other, unbiased, skipNaN);
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
                if (TypeTrait<int>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary Describe(this IList<int> self)
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

        public static double Quantile(this IList<int> self, double p, bool skipNaN = true, bool isSorted = false)
        {
            if (self.Count == 0)
                return double.NaN;

            // TODO use partial sort

            IList<int> sorted;
            if (skipNaN)
            {
                sorted = self.RemoveNaN();
                if (sorted.Count == 0)
                    return double.NaN;
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
                return (double)sorted[0];

            if (p >= highThreshold)
                return (double)sorted[sorted.Count - 1];

            double h = (sorted.Count + 1) * p;
            double hc = Math.Floor(h);

            int i = (int)hc;
            if (i > 0)
                i--;

            int i2 = i + 1;
            if (i2 == sorted.Count)
                i2--;

            return (double)((double)sorted[i] + (h - hc) * ((double)sorted[i2] - (double)sorted[i]));
        }

        public static List<int> RemoveNaN(this IList<int> self)
        {
            var result = new List<int>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<int>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<int> FillNaN(this IList<int> self, int fillValue)
        {
            var result = new List<int>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<int>.IsNaN(value))
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
                if (TypeTrait<int>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static double Kurtosis(this IList<int> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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

                return (double)(a * b - 3 * c);
            }
            else
            {
                return (double)(m4 / (m2 * m2) - 3);
            }
        }

        public static int Max(this IList<int> self)
        {
            if (self.Count == 0)
                return TypeTrait<int>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<int>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            int max = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
            }

            return max;
        }

        public static int Min(this IList<int> self)
        {
            if (self.Count == 0)
                return TypeTrait<int>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<int>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            int min = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
            }

            return min;
        }

        public static double Mean(this IList<int> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (double.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                        return double.NaN;
                }

                mean += v;
            }

            return mean / actualCount;
        }

        public static double Median(this IList<int> self, bool skipNaN = true, bool isSorted = false)
        {
            return Quantile(self, .5, skipNaN, isSorted);
        }

        public static int Mode(this IList<int> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return TypeTrait<int>.GetNaNOrRaiseException("No elements");

            var values = self.ToArray();
            Array.Sort(values);

            int currentValue = values[0];
            int bestValue = currentValue;
            var currentCount = 1;
            var bestCount = 1;

            int i = 1;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait<int>.IsNaN(values[i]))
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

        public static double StandardDeviation(this IList<int> self, bool unbiased = true, bool skipNaN = true)
        {
            var variance = Variance(self, unbiased, skipNaN);
            return (double)Math.Sqrt((double)variance);
        }

        public static double Std(this IList<int> self, bool unbiased = true, bool skipNaN = true)
        {
            return StandardDeviation(self, unbiased, skipNaN);
        }

        public static double Skewness(this IList<int> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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
                return (double)((a / b) * g);
            }
            else
            {
                return (double)g;
            }
        }

        public static double Variance(this IList<int> self, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait<double>.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }

                double x = v - mean;
                variance += x * x;
            }

            if (unbiased)
                return variance / (actualCount - 1);
            else
                return variance / actualCount;
        }

        public static double Var(this IList<int> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // Histogram

        private static Tuple<int[], int> CollectHistogram(IList<int> data, HistogramInterval intervals)
        {
            var result = new int[intervals.BinCount];
            var total = 0;

            foreach (var value in data)
            {
                if (TypeTrait<int>.IsNaN(value))
                    continue;

                var bin = (int)Math.Floor(((double)value - intervals.AdjustedLower) / intervals.BinWidth);
                ++result[bin];
                ++total;
            }

            return Tuple.Create(result, total);
        }

        public static HistogramBin[] Histogram(this IList<int> self, int binCount = -1, double binWidth = double.NaN)
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

        public static double Correlation(this IList<short> self, IList<short> other, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            if (skipNaN)
            {
                var s1 = new List<short>(self.Count);
                var s2 = new List<short>(self.Count);

                for (var i = 0; i < self.Count; ++i)
                {
                    if (TypeTrait<short>.IsNaN(self[i]) || TypeTrait<short>.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
        }

        public static double Cor(this IList<short> self, IList<short> other, bool skipNaN = true)
        {
            return Correlation(self, other, skipNaN);
        }

        public static double Covariance(this IList<short> self, IList<short> other, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            var mean0 = self.Mean(skipNaN);
            var mean1 = other.Mean(skipNaN);

            if (double.IsNaN(mean0) || double.IsNaN(mean1))
                return double.NaN;

            int actualCount = self.Count;

            double c = (double)0.0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<short>.IsNaN(self[i]) || TypeTrait<short>.IsNaN(other[i]))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }
                var a = (double)self[i] - mean0;
                var b = (double)other[i] - mean1;
                c += a * b;
            }

            if (unbiased)
                return c / (actualCount - 1);
            else
                return c / actualCount;
        }

        public static double Cov(this IList<short> self, IList<short> other, bool unbiased = true, bool skipNaN = true)
        {
            return Covariance(self, other, unbiased, skipNaN);
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
                if (TypeTrait<short>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary Describe(this IList<short> self)
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

        public static double Quantile(this IList<short> self, double p, bool skipNaN = true, bool isSorted = false)
        {
            if (self.Count == 0)
                return double.NaN;

            // TODO use partial sort

            IList<short> sorted;
            if (skipNaN)
            {
                sorted = self.RemoveNaN();
                if (sorted.Count == 0)
                    return double.NaN;
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
                return (double)sorted[0];

            if (p >= highThreshold)
                return (double)sorted[sorted.Count - 1];

            double h = (sorted.Count + 1) * p;
            double hc = Math.Floor(h);

            int i = (int)hc;
            if (i > 0)
                i--;

            int i2 = i + 1;
            if (i2 == sorted.Count)
                i2--;

            return (double)((double)sorted[i] + (h - hc) * ((double)sorted[i2] - (double)sorted[i]));
        }

        public static List<short> RemoveNaN(this IList<short> self)
        {
            var result = new List<short>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<short>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<short> FillNaN(this IList<short> self, short fillValue)
        {
            var result = new List<short>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<short>.IsNaN(value))
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
                if (TypeTrait<short>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static double Kurtosis(this IList<short> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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

                return (double)(a * b - 3 * c);
            }
            else
            {
                return (double)(m4 / (m2 * m2) - 3);
            }
        }

        public static short Max(this IList<short> self)
        {
            if (self.Count == 0)
                return TypeTrait<short>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<short>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            short max = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
            }

            return max;
        }

        public static short Min(this IList<short> self)
        {
            if (self.Count == 0)
                return TypeTrait<short>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<short>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            short min = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
            }

            return min;
        }

        public static double Mean(this IList<short> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (double.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                        return double.NaN;
                }

                mean += v;
            }

            return mean / actualCount;
        }

        public static double Median(this IList<short> self, bool skipNaN = true, bool isSorted = false)
        {
            return Quantile(self, .5, skipNaN, isSorted);
        }

        public static short Mode(this IList<short> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return TypeTrait<short>.GetNaNOrRaiseException("No elements");

            var values = self.ToArray();
            Array.Sort(values);

            short currentValue = values[0];
            short bestValue = currentValue;
            var currentCount = 1;
            var bestCount = 1;

            int i = 1;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait<short>.IsNaN(values[i]))
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

        public static double StandardDeviation(this IList<short> self, bool unbiased = true, bool skipNaN = true)
        {
            var variance = Variance(self, unbiased, skipNaN);
            return (double)Math.Sqrt((double)variance);
        }

        public static double Std(this IList<short> self, bool unbiased = true, bool skipNaN = true)
        {
            return StandardDeviation(self, unbiased, skipNaN);
        }

        public static double Skewness(this IList<short> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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
                return (double)((a / b) * g);
            }
            else
            {
                return (double)g;
            }
        }

        public static double Variance(this IList<short> self, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait<double>.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }

                double x = v - mean;
                variance += x * x;
            }

            if (unbiased)
                return variance / (actualCount - 1);
            else
                return variance / actualCount;
        }

        public static double Var(this IList<short> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // Histogram

        private static Tuple<int[], int> CollectHistogram(IList<short> data, HistogramInterval intervals)
        {
            var result = new int[intervals.BinCount];
            var total = 0;

            foreach (var value in data)
            {
                if (TypeTrait<short>.IsNaN(value))
                    continue;

                var bin = (int)Math.Floor(((double)value - intervals.AdjustedLower) / intervals.BinWidth);
                ++result[bin];
                ++total;
            }

            return Tuple.Create(result, total);
        }

        public static HistogramBin[] Histogram(this IList<short> self, int binCount = -1, double binWidth = double.NaN)
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

        public static double Correlation(this IList<byte> self, IList<byte> other, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            if (skipNaN)
            {
                var s1 = new List<byte>(self.Count);
                var s2 = new List<byte>(self.Count);

                for (var i = 0; i < self.Count; ++i)
                {
                    if (TypeTrait<byte>.IsNaN(self[i]) || TypeTrait<byte>.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
        }

        public static double Cor(this IList<byte> self, IList<byte> other, bool skipNaN = true)
        {
            return Correlation(self, other, skipNaN);
        }

        public static double Covariance(this IList<byte> self, IList<byte> other, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            var mean0 = self.Mean(skipNaN);
            var mean1 = other.Mean(skipNaN);

            if (double.IsNaN(mean0) || double.IsNaN(mean1))
                return double.NaN;

            int actualCount = self.Count;

            double c = (double)0.0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<byte>.IsNaN(self[i]) || TypeTrait<byte>.IsNaN(other[i]))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }
                var a = (double)self[i] - mean0;
                var b = (double)other[i] - mean1;
                c += a * b;
            }

            if (unbiased)
                return c / (actualCount - 1);
            else
                return c / actualCount;
        }

        public static double Cov(this IList<byte> self, IList<byte> other, bool unbiased = true, bool skipNaN = true)
        {
            return Covariance(self, other, unbiased, skipNaN);
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
                if (TypeTrait<byte>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary Describe(this IList<byte> self)
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

        public static double Quantile(this IList<byte> self, double p, bool skipNaN = true, bool isSorted = false)
        {
            if (self.Count == 0)
                return double.NaN;

            // TODO use partial sort

            IList<byte> sorted;
            if (skipNaN)
            {
                sorted = self.RemoveNaN();
                if (sorted.Count == 0)
                    return double.NaN;
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
                return (double)sorted[0];

            if (p >= highThreshold)
                return (double)sorted[sorted.Count - 1];

            double h = (sorted.Count + 1) * p;
            double hc = Math.Floor(h);

            int i = (int)hc;
            if (i > 0)
                i--;

            int i2 = i + 1;
            if (i2 == sorted.Count)
                i2--;

            return (double)((double)sorted[i] + (h - hc) * ((double)sorted[i2] - (double)sorted[i]));
        }

        public static List<byte> RemoveNaN(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<byte>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<byte> FillNaN(this IList<byte> self, byte fillValue)
        {
            var result = new List<byte>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<byte>.IsNaN(value))
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
                if (TypeTrait<byte>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static double Kurtosis(this IList<byte> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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

                return (double)(a * b - 3 * c);
            }
            else
            {
                return (double)(m4 / (m2 * m2) - 3);
            }
        }

        public static byte Max(this IList<byte> self)
        {
            if (self.Count == 0)
                return TypeTrait<byte>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<byte>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            byte max = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
            }

            return max;
        }

        public static byte Min(this IList<byte> self)
        {
            if (self.Count == 0)
                return TypeTrait<byte>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<byte>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            byte min = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
            }

            return min;
        }

        public static double Mean(this IList<byte> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (double.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                        return double.NaN;
                }

                mean += v;
            }

            return mean / actualCount;
        }

        public static double Median(this IList<byte> self, bool skipNaN = true, bool isSorted = false)
        {
            return Quantile(self, .5, skipNaN, isSorted);
        }

        public static byte Mode(this IList<byte> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return TypeTrait<byte>.GetNaNOrRaiseException("No elements");

            var values = self.ToArray();
            Array.Sort(values);

            byte currentValue = values[0];
            byte bestValue = currentValue;
            var currentCount = 1;
            var bestCount = 1;

            int i = 1;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait<byte>.IsNaN(values[i]))
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

        public static double StandardDeviation(this IList<byte> self, bool unbiased = true, bool skipNaN = true)
        {
            var variance = Variance(self, unbiased, skipNaN);
            return (double)Math.Sqrt((double)variance);
        }

        public static double Std(this IList<byte> self, bool unbiased = true, bool skipNaN = true)
        {
            return StandardDeviation(self, unbiased, skipNaN);
        }

        public static double Skewness(this IList<byte> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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
                return (double)((a / b) * g);
            }
            else
            {
                return (double)g;
            }
        }

        public static double Variance(this IList<byte> self, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait<double>.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }

                double x = v - mean;
                variance += x * x;
            }

            if (unbiased)
                return variance / (actualCount - 1);
            else
                return variance / actualCount;
        }

        public static double Var(this IList<byte> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // Histogram

        private static Tuple<int[], int> CollectHistogram(IList<byte> data, HistogramInterval intervals)
        {
            var result = new int[intervals.BinCount];
            var total = 0;

            foreach (var value in data)
            {
                if (TypeTrait<byte>.IsNaN(value))
                    continue;

                var bin = (int)Math.Floor(((double)value - intervals.AdjustedLower) / intervals.BinWidth);
                ++result[bin];
                ++total;
            }

            return Tuple.Create(result, total);
        }

        public static HistogramBin[] Histogram(this IList<byte> self, int binCount = -1, double binWidth = double.NaN)
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

        public static double Correlation(this IList<sbyte> self, IList<sbyte> other, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            if (skipNaN)
            {
                var s1 = new List<sbyte>(self.Count);
                var s2 = new List<sbyte>(self.Count);

                for (var i = 0; i < self.Count; ++i)
                {
                    if (TypeTrait<sbyte>.IsNaN(self[i]) || TypeTrait<sbyte>.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
        }

        public static double Cor(this IList<sbyte> self, IList<sbyte> other, bool skipNaN = true)
        {
            return Correlation(self, other, skipNaN);
        }

        public static double Covariance(this IList<sbyte> self, IList<sbyte> other, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            var mean0 = self.Mean(skipNaN);
            var mean1 = other.Mean(skipNaN);

            if (double.IsNaN(mean0) || double.IsNaN(mean1))
                return double.NaN;

            int actualCount = self.Count;

            double c = (double)0.0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<sbyte>.IsNaN(self[i]) || TypeTrait<sbyte>.IsNaN(other[i]))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }
                var a = (double)self[i] - mean0;
                var b = (double)other[i] - mean1;
                c += a * b;
            }

            if (unbiased)
                return c / (actualCount - 1);
            else
                return c / actualCount;
        }

        public static double Cov(this IList<sbyte> self, IList<sbyte> other, bool unbiased = true, bool skipNaN = true)
        {
            return Covariance(self, other, unbiased, skipNaN);
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
                if (TypeTrait<sbyte>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary Describe(this IList<sbyte> self)
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

        public static double Quantile(this IList<sbyte> self, double p, bool skipNaN = true, bool isSorted = false)
        {
            if (self.Count == 0)
                return double.NaN;

            // TODO use partial sort

            IList<sbyte> sorted;
            if (skipNaN)
            {
                sorted = self.RemoveNaN();
                if (sorted.Count == 0)
                    return double.NaN;
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
                return (double)sorted[0];

            if (p >= highThreshold)
                return (double)sorted[sorted.Count - 1];

            double h = (sorted.Count + 1) * p;
            double hc = Math.Floor(h);

            int i = (int)hc;
            if (i > 0)
                i--;

            int i2 = i + 1;
            if (i2 == sorted.Count)
                i2--;

            return (double)((double)sorted[i] + (h - hc) * ((double)sorted[i2] - (double)sorted[i]));
        }

        public static List<sbyte> RemoveNaN(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<sbyte>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<sbyte> FillNaN(this IList<sbyte> self, sbyte fillValue)
        {
            var result = new List<sbyte>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<sbyte>.IsNaN(value))
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
                if (TypeTrait<sbyte>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static double Kurtosis(this IList<sbyte> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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

                return (double)(a * b - 3 * c);
            }
            else
            {
                return (double)(m4 / (m2 * m2) - 3);
            }
        }

        public static sbyte Max(this IList<sbyte> self)
        {
            if (self.Count == 0)
                return TypeTrait<sbyte>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<sbyte>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            sbyte max = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
            }

            return max;
        }

        public static sbyte Min(this IList<sbyte> self)
        {
            if (self.Count == 0)
                return TypeTrait<sbyte>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<sbyte>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            sbyte min = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
            }

            return min;
        }

        public static double Mean(this IList<sbyte> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (double.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                        return double.NaN;
                }

                mean += v;
            }

            return mean / actualCount;
        }

        public static double Median(this IList<sbyte> self, bool skipNaN = true, bool isSorted = false)
        {
            return Quantile(self, .5, skipNaN, isSorted);
        }

        public static sbyte Mode(this IList<sbyte> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return TypeTrait<sbyte>.GetNaNOrRaiseException("No elements");

            var values = self.ToArray();
            Array.Sort(values);

            sbyte currentValue = values[0];
            sbyte bestValue = currentValue;
            var currentCount = 1;
            var bestCount = 1;

            int i = 1;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait<sbyte>.IsNaN(values[i]))
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

        public static double StandardDeviation(this IList<sbyte> self, bool unbiased = true, bool skipNaN = true)
        {
            var variance = Variance(self, unbiased, skipNaN);
            return (double)Math.Sqrt((double)variance);
        }

        public static double Std(this IList<sbyte> self, bool unbiased = true, bool skipNaN = true)
        {
            return StandardDeviation(self, unbiased, skipNaN);
        }

        public static double Skewness(this IList<sbyte> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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
                return (double)((a / b) * g);
            }
            else
            {
                return (double)g;
            }
        }

        public static double Variance(this IList<sbyte> self, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait<double>.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }

                double x = v - mean;
                variance += x * x;
            }

            if (unbiased)
                return variance / (actualCount - 1);
            else
                return variance / actualCount;
        }

        public static double Var(this IList<sbyte> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // Histogram

        private static Tuple<int[], int> CollectHistogram(IList<sbyte> data, HistogramInterval intervals)
        {
            var result = new int[intervals.BinCount];
            var total = 0;

            foreach (var value in data)
            {
                if (TypeTrait<sbyte>.IsNaN(value))
                    continue;

                var bin = (int)Math.Floor(((double)value - intervals.AdjustedLower) / intervals.BinWidth);
                ++result[bin];
                ++total;
            }

            return Tuple.Create(result, total);
        }

        public static HistogramBin[] Histogram(this IList<sbyte> self, int binCount = -1, double binWidth = double.NaN)
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

        public static double Correlation(this IList<decimal> self, IList<decimal> other, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            if (skipNaN)
            {
                var s1 = new List<decimal>(self.Count);
                var s2 = new List<decimal>(self.Count);

                for (var i = 0; i < self.Count; ++i)
                {
                    if (TypeTrait<decimal>.IsNaN(self[i]) || TypeTrait<decimal>.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
        }

        public static double Cor(this IList<decimal> self, IList<decimal> other, bool skipNaN = true)
        {
            return Correlation(self, other, skipNaN);
        }

        public static double Covariance(this IList<decimal> self, IList<decimal> other, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count != other.Count)
                return double.NaN;

            var mean0 = self.Mean(skipNaN);
            var mean1 = other.Mean(skipNaN);

            if (double.IsNaN(mean0) || double.IsNaN(mean1))
                return double.NaN;

            int actualCount = self.Count;

            double c = (double)0.0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<decimal>.IsNaN(self[i]) || TypeTrait<decimal>.IsNaN(other[i]))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }
                var a = (double)self[i] - mean0;
                var b = (double)other[i] - mean1;
                c += a * b;
            }

            if (unbiased)
                return c / (actualCount - 1);
            else
                return c / actualCount;
        }

        public static double Cov(this IList<decimal> self, IList<decimal> other, bool unbiased = true, bool skipNaN = true)
        {
            return Covariance(self, other, unbiased, skipNaN);
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
                if (TypeTrait<decimal>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static Summary Describe(this IList<decimal> self)
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

        public static double Quantile(this IList<decimal> self, double p, bool skipNaN = true, bool isSorted = false)
        {
            if (self.Count == 0)
                return double.NaN;

            // TODO use partial sort

            IList<decimal> sorted;
            if (skipNaN)
            {
                sorted = self.RemoveNaN();
                if (sorted.Count == 0)
                    return double.NaN;
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
                return (double)sorted[0];

            if (p >= highThreshold)
                return (double)sorted[sorted.Count - 1];

            double h = (sorted.Count + 1) * p;
            double hc = Math.Floor(h);

            int i = (int)hc;
            if (i > 0)
                i--;

            int i2 = i + 1;
            if (i2 == sorted.Count)
                i2--;

            return (double)((double)sorted[i] + (h - hc) * ((double)sorted[i2] - (double)sorted[i]));
        }

        public static List<decimal> RemoveNaN(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<decimal>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<decimal> FillNaN(this IList<decimal> self, decimal fillValue)
        {
            var result = new List<decimal>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<decimal>.IsNaN(value))
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
                if (TypeTrait<decimal>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static double Kurtosis(this IList<decimal> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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

                return (double)(a * b - 3 * c);
            }
            else
            {
                return (double)(m4 / (m2 * m2) - 3);
            }
        }

        public static decimal Max(this IList<decimal> self)
        {
            if (self.Count == 0)
                return TypeTrait<decimal>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<decimal>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            decimal max = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] > max)
                    max = self[i];
            }

            return max;
        }

        public static decimal Min(this IList<decimal> self)
        {
            if (self.Count == 0)
                return TypeTrait<decimal>.GetNaNOrRaiseException("No elements");

            var i = 0;
            while (TypeTrait<decimal>.IsNaN(self[0]) && i < self.Count - 1)
                ++i;

            decimal min = self[i];

            for (++i;  i < self.Count; ++i)
            {
                if (self[i] < min)
                    min = self[i];
            }

            return min;
        }

        public static double Mean(this IList<decimal> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (double.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                        return double.NaN;
                }

                mean += v;
            }

            return mean / actualCount;
        }

        public static double Median(this IList<decimal> self, bool skipNaN = true, bool isSorted = false)
        {
            return Quantile(self, .5, skipNaN, isSorted);
        }

        public static decimal Mode(this IList<decimal> self, bool skipNaN = true)
        {
            if (self.Count == 0)
                return TypeTrait<decimal>.GetNaNOrRaiseException("No elements");

            var values = self.ToArray();
            Array.Sort(values);

            decimal currentValue = values[0];
            decimal bestValue = currentValue;
            var currentCount = 1;
            var bestCount = 1;

            int i = 1;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait<decimal>.IsNaN(values[i]))
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

        public static double StandardDeviation(this IList<decimal> self, bool unbiased = true, bool skipNaN = true)
        {
            var variance = Variance(self, unbiased, skipNaN);
            return (double)Math.Sqrt((double)variance);
        }

        public static double Std(this IList<decimal> self, bool unbiased = true, bool skipNaN = true)
        {
            return StandardDeviation(self, unbiased, skipNaN);
        }

        public static double Skewness(this IList<decimal> self, bool unbiased = true)
        {
            if (self.Count == 0)
                return double.NaN;

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
                return (double)((a / b) * g);
            }
            else
            {
                return (double)g;
            }
        }

        public static double Variance(this IList<decimal> self, bool unbiased = true, bool skipNaN = true)
        {
            if (self.Count == 0)
                return double.NaN;

            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait<double>.IsNaN(v))
                {
                    if (skipNaN)
                    {
                        --actualCount;
                        continue;
                    }
                    else
                    {
                        return double.NaN;
                    }
                }

                double x = v - mean;
                variance += x * x;
            }

            if (unbiased)
                return variance / (actualCount - 1);
            else
                return variance / actualCount;
        }

        public static double Var(this IList<decimal> self, bool unbiased = true, bool skipNaN = true)
        {
            return Variance(self, unbiased, skipNaN);
        }

        // Histogram

        private static Tuple<int[], int> CollectHistogram(IList<decimal> data, HistogramInterval intervals)
        {
            var result = new int[intervals.BinCount];
            var total = 0;

            foreach (var value in data)
            {
                if (TypeTrait<decimal>.IsNaN(value))
                    continue;

                var bin = (int)Math.Floor(((double)value - intervals.AdjustedLower) / intervals.BinWidth);
                ++result[bin];
                ++total;
            }

            return Tuple.Create(result, total);
        }

        public static HistogramBin[] Histogram(this IList<decimal> self, int binCount = -1, double binWidth = double.NaN)
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

        public static IList<double> Abs(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Abs((double)self[i]));

            return result;
        }

        public static void AbsFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Abs((double)self[i]));
        }

        public static IList<double> Acos(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Acos((double)self[i]));

            return result;
        }

        public static void AcosFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Acos((double)self[i]));
        }

        public static IList<double> Asin(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Asin((double)self[i]));

            return result;
        }

        public static void AsinFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Asin((double)self[i]));
        }

        public static IList<double> Atan(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Atan((double)self[i]));

            return result;
        }

        public static void AtanFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Atan((double)self[i]));
        }

        public static IList<double> Atan2(this IList<double> self, double x)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Atan2((double)self[i], x));

            return result;
        }

        public static void Atan2Fill(this IList<double> self, double x)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Atan2((double)self[i], x));
        }

        public static IList<double> Ceiling(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Ceiling((double)self[i]));

            return result;
        }

        public static void CeilingFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Ceiling((double)self[i]));
        }

        public static IList<double> Cos(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Cos((double)self[i]));

            return result;
        }

        public static void CosFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Cos((double)self[i]));
        }

        public static IList<double> Exp(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Exp((double)self[i]));

            return result;
        }

        public static void ExpFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Exp((double)self[i]));
        }

        public static IList<double> Floor(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Floor((double)self[i]));

            return result;
        }

        public static void FloorFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Floor((double)self[i]));
        }

        public static IList<double> Log(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log((double)self[i]));

            return result;
        }

        public static void LogFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Log((double)self[i]));
        }

        public static IList<double> Log10(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log10((double)self[i]));

            return result;
        }

        public static void Log10Fill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Log10((double)self[i]));
        }

        public static IList<double> Negate(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)-self[i]);

            return result;
        }

        public static void NegateFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)-self[i]);
        }

        public static IList<double> Pow(this IList<double> self, double exp)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Pow((double)self[i], exp));

            return result;
        }

        public static void PowFill(this IList<double> self, double exp)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Pow((double)self[i], exp));
        }

        public static IList<double> Round(this IList<double> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Round((double)self[i], digits, mode));

            return result;
        }

        public static void RoundFill(this IList<double> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Round((double)self[i], digits, mode));
        }

        public static IList<double> Sign(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sign((double)self[i]));

            return result;
        }

        public static void SignFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Sign((double)self[i]));
        }

        public static IList<double> Sin(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sin((double)self[i]));

            return result;
        }

        public static void SinFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Sin((double)self[i]));
        }

        public static IList<double> Sinh(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sinh((double)self[i]));

            return result;
        }

        public static void SinhFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Sinh((double)self[i]));
        }

        public static IList<double> Sqrt(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sqrt((double)self[i]));

            return result;
        }

        public static void SqrtFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Sqrt((double)self[i]));
        }

        public static IList<double> Tan(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tan((double)self[i]));

            return result;
        }

        public static void TanFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Tan((double)self[i]));
        }

        public static IList<double> Tanh(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tanh((double)self[i]));

            return result;
        }

        public static void TanhFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Tanh((double)self[i]));
        }

        public static IList<double> Truncate(this IList<double> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Truncate((double)self[i]));

            return result;
        }

        public static void TruncateFill(this IList<double> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)((double)Math.Truncate((double)self[i]));
        }

        public static IList<float> Abs(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Abs((double)self[i]));

            return result;
        }

        public static void AbsFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Abs((double)self[i]));
        }

        public static IList<float> Acos(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Acos((double)self[i]));

            return result;
        }

        public static void AcosFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Acos((double)self[i]));
        }

        public static IList<float> Asin(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Asin((double)self[i]));

            return result;
        }

        public static void AsinFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Asin((double)self[i]));
        }

        public static IList<float> Atan(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Atan((double)self[i]));

            return result;
        }

        public static void AtanFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Atan((double)self[i]));
        }

        public static IList<float> Atan2(this IList<float> self, double x)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Atan2((double)self[i], x));

            return result;
        }

        public static void Atan2Fill(this IList<float> self, double x)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Atan2((double)self[i], x));
        }

        public static IList<float> Ceiling(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Ceiling((double)self[i]));

            return result;
        }

        public static void CeilingFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Ceiling((double)self[i]));
        }

        public static IList<float> Cos(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Cos((double)self[i]));

            return result;
        }

        public static void CosFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Cos((double)self[i]));
        }

        public static IList<float> Exp(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Exp((double)self[i]));

            return result;
        }

        public static void ExpFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Exp((double)self[i]));
        }

        public static IList<float> Floor(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Floor((double)self[i]));

            return result;
        }

        public static void FloorFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Floor((double)self[i]));
        }

        public static IList<float> Log(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Log((double)self[i]));

            return result;
        }

        public static void LogFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Log((double)self[i]));
        }

        public static IList<float> Log10(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Log10((double)self[i]));

            return result;
        }

        public static void Log10Fill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Log10((double)self[i]));
        }

        public static IList<float> Negate(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)-self[i]);

            return result;
        }

        public static void NegateFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)-self[i]);
        }

        public static IList<float> Pow(this IList<float> self, double exp)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Pow((double)self[i], exp));

            return result;
        }

        public static void PowFill(this IList<float> self, double exp)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Pow((double)self[i], exp));
        }

        public static IList<float> Round(this IList<float> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Round((double)self[i], digits, mode));

            return result;
        }

        public static void RoundFill(this IList<float> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Round((double)self[i], digits, mode));
        }

        public static IList<float> Sign(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Sign((double)self[i]));

            return result;
        }

        public static void SignFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Sign((double)self[i]));
        }

        public static IList<float> Sin(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Sin((double)self[i]));

            return result;
        }

        public static void SinFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Sin((double)self[i]));
        }

        public static IList<float> Sinh(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Sinh((double)self[i]));

            return result;
        }

        public static void SinhFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Sinh((double)self[i]));
        }

        public static IList<float> Sqrt(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Sqrt((double)self[i]));

            return result;
        }

        public static void SqrtFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Sqrt((double)self[i]));
        }

        public static IList<float> Tan(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Tan((double)self[i]));

            return result;
        }

        public static void TanFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Tan((double)self[i]));
        }

        public static IList<float> Tanh(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Tanh((double)self[i]));

            return result;
        }

        public static void TanhFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Tanh((double)self[i]));
        }

        public static IList<float> Truncate(this IList<float> self)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)Math.Truncate((double)self[i]));

            return result;
        }

        public static void TruncateFill(this IList<float> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)((float)Math.Truncate((double)self[i]));
        }

        public static IList<long> Abs(this IList<long> self)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)Math.Abs((double)self[i]));

            return result;
        }

        public static void AbsFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((long)Math.Abs((double)self[i]));
        }

        public static IList<long> Acos(this IList<long> self)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)Math.Acos((double)self[i]));

            return result;
        }

        public static void AcosFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((long)Math.Acos((double)self[i]));
        }

        public static IList<long> Asin(this IList<long> self)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)Math.Asin((double)self[i]));

            return result;
        }

        public static void AsinFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((long)Math.Asin((double)self[i]));
        }

        public static IList<long> Atan(this IList<long> self)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)Math.Atan((double)self[i]));

            return result;
        }

        public static void AtanFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((long)Math.Atan((double)self[i]));
        }

        public static IList<long> Atan2(this IList<long> self, double x)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)Math.Atan2((double)self[i], x));

            return result;
        }

        public static void Atan2Fill(this IList<long> self, double x)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((long)Math.Atan2((double)self[i], x));
        }

        public static IList<double> Ceiling(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Ceiling((double)self[i]));

            return result;
        }

        public static void CeilingFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Ceiling((double)self[i]));
        }

        public static IList<double> Cos(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Cos((double)self[i]));

            return result;
        }

        public static void CosFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Cos((double)self[i]));
        }

        public static IList<double> Exp(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Exp((double)self[i]));

            return result;
        }

        public static void ExpFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Exp((double)self[i]));
        }

        public static IList<double> Floor(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Floor((double)self[i]));

            return result;
        }

        public static void FloorFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Floor((double)self[i]));
        }

        public static IList<double> Log(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log((double)self[i]));

            return result;
        }

        public static void LogFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Log((double)self[i]));
        }

        public static IList<double> Log10(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log10((double)self[i]));

            return result;
        }

        public static void Log10Fill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Log10((double)self[i]));
        }

        public static IList<long> Negate(this IList<long> self)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)-self[i]);

            return result;
        }

        public static void NegateFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((long)-self[i]);
        }

        public static IList<double> Pow(this IList<long> self, double exp)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Pow((double)self[i], exp));

            return result;
        }

        public static void PowFill(this IList<long> self, double exp)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Pow((double)self[i], exp));
        }

        public static IList<double> Round(this IList<long> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Round((double)self[i], digits, mode));

            return result;
        }

        public static void RoundFill(this IList<long> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Round((double)self[i], digits, mode));
        }

        public static IList<double> Sign(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sign((double)self[i]));

            return result;
        }

        public static void SignFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Sign((double)self[i]));
        }

        public static IList<double> Sin(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sin((double)self[i]));

            return result;
        }

        public static void SinFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Sin((double)self[i]));
        }

        public static IList<double> Sinh(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sinh((double)self[i]));

            return result;
        }

        public static void SinhFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Sinh((double)self[i]));
        }

        public static IList<double> Sqrt(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sqrt((double)self[i]));

            return result;
        }

        public static void SqrtFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Sqrt((double)self[i]));
        }

        public static IList<double> Tan(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tan((double)self[i]));

            return result;
        }

        public static void TanFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Tan((double)self[i]));
        }

        public static IList<double> Tanh(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tanh((double)self[i]));

            return result;
        }

        public static void TanhFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Tanh((double)self[i]));
        }

        public static IList<double> Truncate(this IList<long> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Truncate((double)self[i]));

            return result;
        }

        public static void TruncateFill(this IList<long> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)((double)Math.Truncate((double)self[i]));
        }

        public static IList<int> Abs(this IList<int> self)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)Math.Abs((double)self[i]));

            return result;
        }

        public static void AbsFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((int)Math.Abs((double)self[i]));
        }

        public static IList<int> Acos(this IList<int> self)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)Math.Acos((double)self[i]));

            return result;
        }

        public static void AcosFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((int)Math.Acos((double)self[i]));
        }

        public static IList<int> Asin(this IList<int> self)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)Math.Asin((double)self[i]));

            return result;
        }

        public static void AsinFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((int)Math.Asin((double)self[i]));
        }

        public static IList<int> Atan(this IList<int> self)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)Math.Atan((double)self[i]));

            return result;
        }

        public static void AtanFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((int)Math.Atan((double)self[i]));
        }

        public static IList<int> Atan2(this IList<int> self, double x)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)Math.Atan2((double)self[i], x));

            return result;
        }

        public static void Atan2Fill(this IList<int> self, double x)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((int)Math.Atan2((double)self[i], x));
        }

        public static IList<double> Ceiling(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Ceiling((double)self[i]));

            return result;
        }

        public static void CeilingFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Ceiling((double)self[i]));
        }

        public static IList<double> Cos(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Cos((double)self[i]));

            return result;
        }

        public static void CosFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Cos((double)self[i]));
        }

        public static IList<double> Exp(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Exp((double)self[i]));

            return result;
        }

        public static void ExpFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Exp((double)self[i]));
        }

        public static IList<double> Floor(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Floor((double)self[i]));

            return result;
        }

        public static void FloorFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Floor((double)self[i]));
        }

        public static IList<double> Log(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log((double)self[i]));

            return result;
        }

        public static void LogFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Log((double)self[i]));
        }

        public static IList<double> Log10(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log10((double)self[i]));

            return result;
        }

        public static void Log10Fill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Log10((double)self[i]));
        }

        public static IList<int> Negate(this IList<int> self)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)-self[i]);

            return result;
        }

        public static void NegateFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((int)-self[i]);
        }

        public static IList<double> Pow(this IList<int> self, double exp)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Pow((double)self[i], exp));

            return result;
        }

        public static void PowFill(this IList<int> self, double exp)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Pow((double)self[i], exp));
        }

        public static IList<double> Round(this IList<int> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Round((double)self[i], digits, mode));

            return result;
        }

        public static void RoundFill(this IList<int> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Round((double)self[i], digits, mode));
        }

        public static IList<double> Sign(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sign((double)self[i]));

            return result;
        }

        public static void SignFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Sign((double)self[i]));
        }

        public static IList<double> Sin(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sin((double)self[i]));

            return result;
        }

        public static void SinFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Sin((double)self[i]));
        }

        public static IList<double> Sinh(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sinh((double)self[i]));

            return result;
        }

        public static void SinhFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Sinh((double)self[i]));
        }

        public static IList<double> Sqrt(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sqrt((double)self[i]));

            return result;
        }

        public static void SqrtFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Sqrt((double)self[i]));
        }

        public static IList<double> Tan(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tan((double)self[i]));

            return result;
        }

        public static void TanFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Tan((double)self[i]));
        }

        public static IList<double> Tanh(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tanh((double)self[i]));

            return result;
        }

        public static void TanhFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Tanh((double)self[i]));
        }

        public static IList<double> Truncate(this IList<int> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Truncate((double)self[i]));

            return result;
        }

        public static void TruncateFill(this IList<int> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)((double)Math.Truncate((double)self[i]));
        }

        public static IList<short> Abs(this IList<short> self)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)Math.Abs((double)self[i]));

            return result;
        }

        public static void AbsFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((short)Math.Abs((double)self[i]));
        }

        public static IList<short> Acos(this IList<short> self)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)Math.Acos((double)self[i]));

            return result;
        }

        public static void AcosFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((short)Math.Acos((double)self[i]));
        }

        public static IList<short> Asin(this IList<short> self)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)Math.Asin((double)self[i]));

            return result;
        }

        public static void AsinFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((short)Math.Asin((double)self[i]));
        }

        public static IList<short> Atan(this IList<short> self)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)Math.Atan((double)self[i]));

            return result;
        }

        public static void AtanFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((short)Math.Atan((double)self[i]));
        }

        public static IList<short> Atan2(this IList<short> self, double x)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)Math.Atan2((double)self[i], x));

            return result;
        }

        public static void Atan2Fill(this IList<short> self, double x)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((short)Math.Atan2((double)self[i], x));
        }

        public static IList<double> Ceiling(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Ceiling((double)self[i]));

            return result;
        }

        public static void CeilingFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Ceiling((double)self[i]));
        }

        public static IList<double> Cos(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Cos((double)self[i]));

            return result;
        }

        public static void CosFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Cos((double)self[i]));
        }

        public static IList<double> Exp(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Exp((double)self[i]));

            return result;
        }

        public static void ExpFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Exp((double)self[i]));
        }

        public static IList<double> Floor(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Floor((double)self[i]));

            return result;
        }

        public static void FloorFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Floor((double)self[i]));
        }

        public static IList<double> Log(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log((double)self[i]));

            return result;
        }

        public static void LogFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Log((double)self[i]));
        }

        public static IList<double> Log10(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log10((double)self[i]));

            return result;
        }

        public static void Log10Fill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Log10((double)self[i]));
        }

        public static IList<short> Negate(this IList<short> self)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)-self[i]);

            return result;
        }

        public static void NegateFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((short)-self[i]);
        }

        public static IList<double> Pow(this IList<short> self, double exp)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Pow((double)self[i], exp));

            return result;
        }

        public static void PowFill(this IList<short> self, double exp)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Pow((double)self[i], exp));
        }

        public static IList<double> Round(this IList<short> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Round((double)self[i], digits, mode));

            return result;
        }

        public static void RoundFill(this IList<short> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Round((double)self[i], digits, mode));
        }

        public static IList<double> Sign(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sign((double)self[i]));

            return result;
        }

        public static void SignFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Sign((double)self[i]));
        }

        public static IList<double> Sin(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sin((double)self[i]));

            return result;
        }

        public static void SinFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Sin((double)self[i]));
        }

        public static IList<double> Sinh(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sinh((double)self[i]));

            return result;
        }

        public static void SinhFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Sinh((double)self[i]));
        }

        public static IList<double> Sqrt(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sqrt((double)self[i]));

            return result;
        }

        public static void SqrtFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Sqrt((double)self[i]));
        }

        public static IList<double> Tan(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tan((double)self[i]));

            return result;
        }

        public static void TanFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Tan((double)self[i]));
        }

        public static IList<double> Tanh(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tanh((double)self[i]));

            return result;
        }

        public static void TanhFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Tanh((double)self[i]));
        }

        public static IList<double> Truncate(this IList<short> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Truncate((double)self[i]));

            return result;
        }

        public static void TruncateFill(this IList<short> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)((double)Math.Truncate((double)self[i]));
        }

        public static IList<byte> Abs(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)Math.Abs((double)self[i]));

            return result;
        }

        public static void AbsFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((byte)Math.Abs((double)self[i]));
        }

        public static IList<byte> Acos(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)Math.Acos((double)self[i]));

            return result;
        }

        public static void AcosFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((byte)Math.Acos((double)self[i]));
        }

        public static IList<byte> Asin(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)Math.Asin((double)self[i]));

            return result;
        }

        public static void AsinFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((byte)Math.Asin((double)self[i]));
        }

        public static IList<byte> Atan(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)Math.Atan((double)self[i]));

            return result;
        }

        public static void AtanFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((byte)Math.Atan((double)self[i]));
        }

        public static IList<byte> Atan2(this IList<byte> self, double x)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)Math.Atan2((double)self[i], x));

            return result;
        }

        public static void Atan2Fill(this IList<byte> self, double x)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((byte)Math.Atan2((double)self[i], x));
        }

        public static IList<double> Ceiling(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Ceiling((double)self[i]));

            return result;
        }

        public static void CeilingFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Ceiling((double)self[i]));
        }

        public static IList<double> Cos(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Cos((double)self[i]));

            return result;
        }

        public static void CosFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Cos((double)self[i]));
        }

        public static IList<double> Exp(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Exp((double)self[i]));

            return result;
        }

        public static void ExpFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Exp((double)self[i]));
        }

        public static IList<double> Floor(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Floor((double)self[i]));

            return result;
        }

        public static void FloorFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Floor((double)self[i]));
        }

        public static IList<double> Log(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log((double)self[i]));

            return result;
        }

        public static void LogFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Log((double)self[i]));
        }

        public static IList<double> Log10(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log10((double)self[i]));

            return result;
        }

        public static void Log10Fill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Log10((double)self[i]));
        }

        public static IList<byte> Negate(this IList<byte> self)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)-self[i]);

            return result;
        }

        public static void NegateFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((byte)-self[i]);
        }

        public static IList<double> Pow(this IList<byte> self, double exp)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Pow((double)self[i], exp));

            return result;
        }

        public static void PowFill(this IList<byte> self, double exp)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Pow((double)self[i], exp));
        }

        public static IList<double> Round(this IList<byte> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Round((double)self[i], digits, mode));

            return result;
        }

        public static void RoundFill(this IList<byte> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Round((double)self[i], digits, mode));
        }

        public static IList<double> Sign(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sign((double)self[i]));

            return result;
        }

        public static void SignFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Sign((double)self[i]));
        }

        public static IList<double> Sin(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sin((double)self[i]));

            return result;
        }

        public static void SinFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Sin((double)self[i]));
        }

        public static IList<double> Sinh(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sinh((double)self[i]));

            return result;
        }

        public static void SinhFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Sinh((double)self[i]));
        }

        public static IList<double> Sqrt(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sqrt((double)self[i]));

            return result;
        }

        public static void SqrtFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Sqrt((double)self[i]));
        }

        public static IList<double> Tan(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tan((double)self[i]));

            return result;
        }

        public static void TanFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Tan((double)self[i]));
        }

        public static IList<double> Tanh(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tanh((double)self[i]));

            return result;
        }

        public static void TanhFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Tanh((double)self[i]));
        }

        public static IList<double> Truncate(this IList<byte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Truncate((double)self[i]));

            return result;
        }

        public static void TruncateFill(this IList<byte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)((double)Math.Truncate((double)self[i]));
        }

        public static IList<sbyte> Abs(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)Math.Abs((double)self[i]));

            return result;
        }

        public static void AbsFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((sbyte)Math.Abs((double)self[i]));
        }

        public static IList<sbyte> Acos(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)Math.Acos((double)self[i]));

            return result;
        }

        public static void AcosFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((sbyte)Math.Acos((double)self[i]));
        }

        public static IList<sbyte> Asin(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)Math.Asin((double)self[i]));

            return result;
        }

        public static void AsinFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((sbyte)Math.Asin((double)self[i]));
        }

        public static IList<sbyte> Atan(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)Math.Atan((double)self[i]));

            return result;
        }

        public static void AtanFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((sbyte)Math.Atan((double)self[i]));
        }

        public static IList<sbyte> Atan2(this IList<sbyte> self, double x)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)Math.Atan2((double)self[i], x));

            return result;
        }

        public static void Atan2Fill(this IList<sbyte> self, double x)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((sbyte)Math.Atan2((double)self[i], x));
        }

        public static IList<double> Ceiling(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Ceiling((double)self[i]));

            return result;
        }

        public static void CeilingFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Ceiling((double)self[i]));
        }

        public static IList<double> Cos(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Cos((double)self[i]));

            return result;
        }

        public static void CosFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Cos((double)self[i]));
        }

        public static IList<double> Exp(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Exp((double)self[i]));

            return result;
        }

        public static void ExpFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Exp((double)self[i]));
        }

        public static IList<double> Floor(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Floor((double)self[i]));

            return result;
        }

        public static void FloorFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Floor((double)self[i]));
        }

        public static IList<double> Log(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log((double)self[i]));

            return result;
        }

        public static void LogFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Log((double)self[i]));
        }

        public static IList<double> Log10(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log10((double)self[i]));

            return result;
        }

        public static void Log10Fill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Log10((double)self[i]));
        }

        public static IList<sbyte> Negate(this IList<sbyte> self)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)-self[i]);

            return result;
        }

        public static void NegateFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((sbyte)-self[i]);
        }

        public static IList<double> Pow(this IList<sbyte> self, double exp)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Pow((double)self[i], exp));

            return result;
        }

        public static void PowFill(this IList<sbyte> self, double exp)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Pow((double)self[i], exp));
        }

        public static IList<double> Round(this IList<sbyte> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Round((double)self[i], digits, mode));

            return result;
        }

        public static void RoundFill(this IList<sbyte> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Round((double)self[i], digits, mode));
        }

        public static IList<double> Sign(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sign((double)self[i]));

            return result;
        }

        public static void SignFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Sign((double)self[i]));
        }

        public static IList<double> Sin(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sin((double)self[i]));

            return result;
        }

        public static void SinFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Sin((double)self[i]));
        }

        public static IList<double> Sinh(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sinh((double)self[i]));

            return result;
        }

        public static void SinhFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Sinh((double)self[i]));
        }

        public static IList<double> Sqrt(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sqrt((double)self[i]));

            return result;
        }

        public static void SqrtFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Sqrt((double)self[i]));
        }

        public static IList<double> Tan(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tan((double)self[i]));

            return result;
        }

        public static void TanFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Tan((double)self[i]));
        }

        public static IList<double> Tanh(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tanh((double)self[i]));

            return result;
        }

        public static void TanhFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Tanh((double)self[i]));
        }

        public static IList<double> Truncate(this IList<sbyte> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Truncate((double)self[i]));

            return result;
        }

        public static void TruncateFill(this IList<sbyte> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)((double)Math.Truncate((double)self[i]));
        }

        public static IList<decimal> Abs(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)Math.Abs((double)self[i]));

            return result;
        }

        public static void AbsFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((decimal)Math.Abs((double)self[i]));
        }

        public static IList<decimal> Acos(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)Math.Acos((double)self[i]));

            return result;
        }

        public static void AcosFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((decimal)Math.Acos((double)self[i]));
        }

        public static IList<decimal> Asin(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)Math.Asin((double)self[i]));

            return result;
        }

        public static void AsinFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((decimal)Math.Asin((double)self[i]));
        }

        public static IList<decimal> Atan(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)Math.Atan((double)self[i]));

            return result;
        }

        public static void AtanFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((decimal)Math.Atan((double)self[i]));
        }

        public static IList<decimal> Atan2(this IList<decimal> self, double x)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)Math.Atan2((double)self[i], x));

            return result;
        }

        public static void Atan2Fill(this IList<decimal> self, double x)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((decimal)Math.Atan2((double)self[i], x));
        }

        public static IList<double> Ceiling(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Ceiling((double)self[i]));

            return result;
        }

        public static void CeilingFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Ceiling((double)self[i]));
        }

        public static IList<double> Cos(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Cos((double)self[i]));

            return result;
        }

        public static void CosFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Cos((double)self[i]));
        }

        public static IList<double> Exp(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Exp((double)self[i]));

            return result;
        }

        public static void ExpFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Exp((double)self[i]));
        }

        public static IList<double> Floor(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Floor((double)self[i]));

            return result;
        }

        public static void FloorFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Floor((double)self[i]));
        }

        public static IList<double> Log(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log((double)self[i]));

            return result;
        }

        public static void LogFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Log((double)self[i]));
        }

        public static IList<double> Log10(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Log10((double)self[i]));

            return result;
        }

        public static void Log10Fill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Log10((double)self[i]));
        }

        public static IList<decimal> Negate(this IList<decimal> self)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)-self[i]);

            return result;
        }

        public static void NegateFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((decimal)-self[i]);
        }

        public static IList<double> Pow(this IList<decimal> self, double exp)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Pow((double)self[i], exp));

            return result;
        }

        public static void PowFill(this IList<decimal> self, double exp)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Pow((double)self[i], exp));
        }

        public static IList<double> Round(this IList<decimal> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Round((double)self[i], digits, mode));

            return result;
        }

        public static void RoundFill(this IList<decimal> self, int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Round((double)self[i], digits, mode));
        }

        public static IList<double> Sign(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sign((double)self[i]));

            return result;
        }

        public static void SignFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Sign((double)self[i]));
        }

        public static IList<double> Sin(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sin((double)self[i]));

            return result;
        }

        public static void SinFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Sin((double)self[i]));
        }

        public static IList<double> Sinh(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sinh((double)self[i]));

            return result;
        }

        public static void SinhFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Sinh((double)self[i]));
        }

        public static IList<double> Sqrt(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Sqrt((double)self[i]));

            return result;
        }

        public static void SqrtFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Sqrt((double)self[i]));
        }

        public static IList<double> Tan(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tan((double)self[i]));

            return result;
        }

        public static void TanFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Tan((double)self[i]));
        }

        public static IList<double> Tanh(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Tanh((double)self[i]));

            return result;
        }

        public static void TanhFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Tanh((double)self[i]));
        }

        public static IList<double> Truncate(this IList<decimal> self)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)Math.Truncate((double)self[i]));

            return result;
        }

        public static void TruncateFill(this IList<decimal> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)((double)Math.Truncate((double)self[i]));
        }

        public static IList<double> ElementAdd(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(self[i] + other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementAdd(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(self[i] + value));

            return result;
        }

        public static void ElementAddFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(self[i] + other[i]);
        }

        public static void ElementAddFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(self[i] + value);
        }

        public static IList<double> ElementSubtract(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(self[i] - other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementSubtract(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(self[i] - value));

            return result;
        }

        public static void ElementSubtractFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(self[i] - other[i]);
        }

        public static void ElementSubtractFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(self[i] - value);
        }

        public static IList<double> ElementMultiply(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(self[i] * other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementMultiply(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(self[i] * value));

            return result;
        }

        public static void ElementMultiplyFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(self[i] * other[i]);
        }

        public static void ElementMultiplyFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(self[i] * value);
        }

        public static IList<double> ElementDivide(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(self[i] / other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementDivide(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(self[i] / value));

            return result;
        }

        public static void ElementDivideFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(self[i] / other[i]);
        }

        public static void ElementDivideFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(self[i] / value);
        }

        public static IList<double> ElementMod(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(self[i] % other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementMod(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(self[i] % value));

            return result;
        }

        public static void ElementModFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(self[i] % other[i]);
        }

        public static void ElementModFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(self[i] % value);
        }

        public static IList<double> ElementAddR(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(other[i] + self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementAddR(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(value - self[i]));

            return result;
        }

        public static void ElementAddRFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(other[i] + self[i]);
        }

        public static void ElementAddRFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(value - self[i]);
        }

        public static IList<double> ElementSubtractR(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(other[i] - self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementSubtractR(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(value - self[i]));

            return result;
        }

        public static void ElementSubtractRFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(other[i] - self[i]);
        }

        public static void ElementSubtractRFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(value - self[i]);
        }

        public static IList<double> ElementMultiplyR(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(other[i] * self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementMultiplyR(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(value * self[i]));

            return result;
        }

        public static void ElementMultiplyRFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(other[i] * self[i]);
        }

        public static void ElementMultiplyRFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(value * self[i]);
        }

        public static IList<double> ElementDivideR(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(other[i] / self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementDivideR(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(value / self[i]));

            return result;
        }

        public static void ElementDivideRFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(other[i] / self[i]);
        }

        public static void ElementDivideRFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(value / self[i]);
        }

        public static IList<double> ElementModR(this IList<double> self, IList<double> other)
        {
            var result = new List<double>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((double)(other[i] % self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<double> ElementModR(this IList<double> self, double value)
        {
            var result = new List<double>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((double)(value % self[i]));

            return result;
        }

        public static void ElementModRFill(this IList<double> self, IList<double> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (double)(other[i] % self[i]);
        }

        public static void ElementModRFill(this IList<double> self, double value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (double)(value % self[i]);
        }

        public static IList<float> ElementAdd(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(self[i] + other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementAdd(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(self[i] + value));

            return result;
        }

        public static void ElementAddFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(self[i] + other[i]);
        }

        public static void ElementAddFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(self[i] + value);
        }

        public static IList<float> ElementSubtract(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(self[i] - other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementSubtract(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(self[i] - value));

            return result;
        }

        public static void ElementSubtractFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(self[i] - other[i]);
        }

        public static void ElementSubtractFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(self[i] - value);
        }

        public static IList<float> ElementMultiply(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(self[i] * other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementMultiply(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(self[i] * value));

            return result;
        }

        public static void ElementMultiplyFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(self[i] * other[i]);
        }

        public static void ElementMultiplyFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(self[i] * value);
        }

        public static IList<float> ElementDivide(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(self[i] / other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementDivide(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(self[i] / value));

            return result;
        }

        public static void ElementDivideFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(self[i] / other[i]);
        }

        public static void ElementDivideFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(self[i] / value);
        }

        public static IList<float> ElementMod(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(self[i] % other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementMod(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(self[i] % value));

            return result;
        }

        public static void ElementModFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(self[i] % other[i]);
        }

        public static void ElementModFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(self[i] % value);
        }

        public static IList<float> ElementAddR(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(other[i] + self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementAddR(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(value - self[i]));

            return result;
        }

        public static void ElementAddRFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(other[i] + self[i]);
        }

        public static void ElementAddRFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(value - self[i]);
        }

        public static IList<float> ElementSubtractR(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(other[i] - self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementSubtractR(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(value - self[i]));

            return result;
        }

        public static void ElementSubtractRFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(other[i] - self[i]);
        }

        public static void ElementSubtractRFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(value - self[i]);
        }

        public static IList<float> ElementMultiplyR(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(other[i] * self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementMultiplyR(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(value * self[i]));

            return result;
        }

        public static void ElementMultiplyRFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(other[i] * self[i]);
        }

        public static void ElementMultiplyRFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(value * self[i]);
        }

        public static IList<float> ElementDivideR(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(other[i] / self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementDivideR(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(value / self[i]));

            return result;
        }

        public static void ElementDivideRFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(other[i] / self[i]);
        }

        public static void ElementDivideRFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(value / self[i]);
        }

        public static IList<float> ElementModR(this IList<float> self, IList<float> other)
        {
            var result = new List<float>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((float)(other[i] % self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<float> ElementModR(this IList<float> self, float value)
        {
            var result = new List<float>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((float)(value % self[i]));

            return result;
        }

        public static void ElementModRFill(this IList<float> self, IList<float> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (float)(other[i] % self[i]);
        }

        public static void ElementModRFill(this IList<float> self, float value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (float)(value % self[i]);
        }

        public static IList<long> ElementAdd(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(self[i] + other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementAdd(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(self[i] + value));

            return result;
        }

        public static void ElementAddFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(self[i] + other[i]);
        }

        public static void ElementAddFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(self[i] + value);
        }

        public static IList<long> ElementSubtract(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(self[i] - other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementSubtract(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(self[i] - value));

            return result;
        }

        public static void ElementSubtractFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(self[i] - other[i]);
        }

        public static void ElementSubtractFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(self[i] - value);
        }

        public static IList<long> ElementMultiply(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(self[i] * other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementMultiply(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(self[i] * value));

            return result;
        }

        public static void ElementMultiplyFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(self[i] * other[i]);
        }

        public static void ElementMultiplyFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(self[i] * value);
        }

        public static IList<long> ElementDivide(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(self[i] / other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementDivide(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(self[i] / value));

            return result;
        }

        public static void ElementDivideFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(self[i] / other[i]);
        }

        public static void ElementDivideFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(self[i] / value);
        }

        public static IList<long> ElementMod(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(self[i] % other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementMod(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(self[i] % value));

            return result;
        }

        public static void ElementModFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(self[i] % other[i]);
        }

        public static void ElementModFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(self[i] % value);
        }

        public static IList<long> ElementAddR(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(other[i] + self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementAddR(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(value - self[i]));

            return result;
        }

        public static void ElementAddRFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(other[i] + self[i]);
        }

        public static void ElementAddRFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(value - self[i]);
        }

        public static IList<long> ElementSubtractR(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(other[i] - self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementSubtractR(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(value - self[i]));

            return result;
        }

        public static void ElementSubtractRFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(other[i] - self[i]);
        }

        public static void ElementSubtractRFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(value - self[i]);
        }

        public static IList<long> ElementMultiplyR(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(other[i] * self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementMultiplyR(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(value * self[i]));

            return result;
        }

        public static void ElementMultiplyRFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(other[i] * self[i]);
        }

        public static void ElementMultiplyRFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(value * self[i]);
        }

        public static IList<long> ElementDivideR(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(other[i] / self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementDivideR(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(value / self[i]));

            return result;
        }

        public static void ElementDivideRFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(other[i] / self[i]);
        }

        public static void ElementDivideRFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(value / self[i]);
        }

        public static IList<long> ElementModR(this IList<long> self, IList<long> other)
        {
            var result = new List<long>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((long)(other[i] % self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<long> ElementModR(this IList<long> self, long value)
        {
            var result = new List<long>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((long)(value % self[i]));

            return result;
        }

        public static void ElementModRFill(this IList<long> self, IList<long> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (long)(other[i] % self[i]);
        }

        public static void ElementModRFill(this IList<long> self, long value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (long)(value % self[i]);
        }

        public static IList<int> ElementAdd(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(self[i] + other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementAdd(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(self[i] + value));

            return result;
        }

        public static void ElementAddFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(self[i] + other[i]);
        }

        public static void ElementAddFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(self[i] + value);
        }

        public static IList<int> ElementSubtract(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(self[i] - other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementSubtract(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(self[i] - value));

            return result;
        }

        public static void ElementSubtractFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(self[i] - other[i]);
        }

        public static void ElementSubtractFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(self[i] - value);
        }

        public static IList<int> ElementMultiply(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(self[i] * other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementMultiply(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(self[i] * value));

            return result;
        }

        public static void ElementMultiplyFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(self[i] * other[i]);
        }

        public static void ElementMultiplyFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(self[i] * value);
        }

        public static IList<int> ElementDivide(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(self[i] / other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementDivide(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(self[i] / value));

            return result;
        }

        public static void ElementDivideFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(self[i] / other[i]);
        }

        public static void ElementDivideFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(self[i] / value);
        }

        public static IList<int> ElementMod(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(self[i] % other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementMod(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(self[i] % value));

            return result;
        }

        public static void ElementModFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(self[i] % other[i]);
        }

        public static void ElementModFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(self[i] % value);
        }

        public static IList<int> ElementAddR(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(other[i] + self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementAddR(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(value - self[i]));

            return result;
        }

        public static void ElementAddRFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(other[i] + self[i]);
        }

        public static void ElementAddRFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(value - self[i]);
        }

        public static IList<int> ElementSubtractR(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(other[i] - self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementSubtractR(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(value - self[i]));

            return result;
        }

        public static void ElementSubtractRFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(other[i] - self[i]);
        }

        public static void ElementSubtractRFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(value - self[i]);
        }

        public static IList<int> ElementMultiplyR(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(other[i] * self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementMultiplyR(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(value * self[i]));

            return result;
        }

        public static void ElementMultiplyRFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(other[i] * self[i]);
        }

        public static void ElementMultiplyRFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(value * self[i]);
        }

        public static IList<int> ElementDivideR(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(other[i] / self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementDivideR(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(value / self[i]));

            return result;
        }

        public static void ElementDivideRFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(other[i] / self[i]);
        }

        public static void ElementDivideRFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(value / self[i]);
        }

        public static IList<int> ElementModR(this IList<int> self, IList<int> other)
        {
            var result = new List<int>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((int)(other[i] % self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<int> ElementModR(this IList<int> self, int value)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((int)(value % self[i]));

            return result;
        }

        public static void ElementModRFill(this IList<int> self, IList<int> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (int)(other[i] % self[i]);
        }

        public static void ElementModRFill(this IList<int> self, int value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (int)(value % self[i]);
        }

        public static IList<short> ElementAdd(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(self[i] + other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementAdd(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(self[i] + value));

            return result;
        }

        public static void ElementAddFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(self[i] + other[i]);
        }

        public static void ElementAddFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(self[i] + value);
        }

        public static IList<short> ElementSubtract(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(self[i] - other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementSubtract(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(self[i] - value));

            return result;
        }

        public static void ElementSubtractFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(self[i] - other[i]);
        }

        public static void ElementSubtractFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(self[i] - value);
        }

        public static IList<short> ElementMultiply(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(self[i] * other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementMultiply(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(self[i] * value));

            return result;
        }

        public static void ElementMultiplyFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(self[i] * other[i]);
        }

        public static void ElementMultiplyFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(self[i] * value);
        }

        public static IList<short> ElementDivide(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(self[i] / other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementDivide(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(self[i] / value));

            return result;
        }

        public static void ElementDivideFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(self[i] / other[i]);
        }

        public static void ElementDivideFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(self[i] / value);
        }

        public static IList<short> ElementMod(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(self[i] % other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementMod(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(self[i] % value));

            return result;
        }

        public static void ElementModFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(self[i] % other[i]);
        }

        public static void ElementModFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(self[i] % value);
        }

        public static IList<short> ElementAddR(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(other[i] + self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementAddR(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(value - self[i]));

            return result;
        }

        public static void ElementAddRFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(other[i] + self[i]);
        }

        public static void ElementAddRFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(value - self[i]);
        }

        public static IList<short> ElementSubtractR(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(other[i] - self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementSubtractR(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(value - self[i]));

            return result;
        }

        public static void ElementSubtractRFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(other[i] - self[i]);
        }

        public static void ElementSubtractRFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(value - self[i]);
        }

        public static IList<short> ElementMultiplyR(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(other[i] * self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementMultiplyR(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(value * self[i]));

            return result;
        }

        public static void ElementMultiplyRFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(other[i] * self[i]);
        }

        public static void ElementMultiplyRFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(value * self[i]);
        }

        public static IList<short> ElementDivideR(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(other[i] / self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementDivideR(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(value / self[i]));

            return result;
        }

        public static void ElementDivideRFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(other[i] / self[i]);
        }

        public static void ElementDivideRFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(value / self[i]);
        }

        public static IList<short> ElementModR(this IList<short> self, IList<short> other)
        {
            var result = new List<short>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((short)(other[i] % self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<short> ElementModR(this IList<short> self, short value)
        {
            var result = new List<short>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((short)(value % self[i]));

            return result;
        }

        public static void ElementModRFill(this IList<short> self, IList<short> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (short)(other[i] % self[i]);
        }

        public static void ElementModRFill(this IList<short> self, short value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (short)(value % self[i]);
        }

        public static IList<byte> ElementAdd(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(self[i] + other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementAdd(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(self[i] + value));

            return result;
        }

        public static void ElementAddFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(self[i] + other[i]);
        }

        public static void ElementAddFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(self[i] + value);
        }

        public static IList<byte> ElementSubtract(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(self[i] - other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementSubtract(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(self[i] - value));

            return result;
        }

        public static void ElementSubtractFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(self[i] - other[i]);
        }

        public static void ElementSubtractFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(self[i] - value);
        }

        public static IList<byte> ElementMultiply(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(self[i] * other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementMultiply(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(self[i] * value));

            return result;
        }

        public static void ElementMultiplyFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(self[i] * other[i]);
        }

        public static void ElementMultiplyFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(self[i] * value);
        }

        public static IList<byte> ElementDivide(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(self[i] / other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementDivide(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(self[i] / value));

            return result;
        }

        public static void ElementDivideFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(self[i] / other[i]);
        }

        public static void ElementDivideFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(self[i] / value);
        }

        public static IList<byte> ElementMod(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(self[i] % other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementMod(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(self[i] % value));

            return result;
        }

        public static void ElementModFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(self[i] % other[i]);
        }

        public static void ElementModFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(self[i] % value);
        }

        public static IList<byte> ElementAddR(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(other[i] + self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementAddR(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(value - self[i]));

            return result;
        }

        public static void ElementAddRFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(other[i] + self[i]);
        }

        public static void ElementAddRFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(value - self[i]);
        }

        public static IList<byte> ElementSubtractR(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(other[i] - self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementSubtractR(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(value - self[i]));

            return result;
        }

        public static void ElementSubtractRFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(other[i] - self[i]);
        }

        public static void ElementSubtractRFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(value - self[i]);
        }

        public static IList<byte> ElementMultiplyR(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(other[i] * self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementMultiplyR(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(value * self[i]));

            return result;
        }

        public static void ElementMultiplyRFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(other[i] * self[i]);
        }

        public static void ElementMultiplyRFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(value * self[i]);
        }

        public static IList<byte> ElementDivideR(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(other[i] / self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementDivideR(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(value / self[i]));

            return result;
        }

        public static void ElementDivideRFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(other[i] / self[i]);
        }

        public static void ElementDivideRFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(value / self[i]);
        }

        public static IList<byte> ElementModR(this IList<byte> self, IList<byte> other)
        {
            var result = new List<byte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((byte)(other[i] % self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<byte> ElementModR(this IList<byte> self, byte value)
        {
            var result = new List<byte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((byte)(value % self[i]));

            return result;
        }

        public static void ElementModRFill(this IList<byte> self, IList<byte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (byte)(other[i] % self[i]);
        }

        public static void ElementModRFill(this IList<byte> self, byte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (byte)(value % self[i]);
        }

        public static IList<sbyte> ElementAdd(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(self[i] + other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementAdd(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(self[i] + value));

            return result;
        }

        public static void ElementAddFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(self[i] + other[i]);
        }

        public static void ElementAddFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(self[i] + value);
        }

        public static IList<sbyte> ElementSubtract(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(self[i] - other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementSubtract(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(self[i] - value));

            return result;
        }

        public static void ElementSubtractFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(self[i] - other[i]);
        }

        public static void ElementSubtractFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(self[i] - value);
        }

        public static IList<sbyte> ElementMultiply(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(self[i] * other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementMultiply(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(self[i] * value));

            return result;
        }

        public static void ElementMultiplyFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(self[i] * other[i]);
        }

        public static void ElementMultiplyFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(self[i] * value);
        }

        public static IList<sbyte> ElementDivide(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(self[i] / other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementDivide(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(self[i] / value));

            return result;
        }

        public static void ElementDivideFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(self[i] / other[i]);
        }

        public static void ElementDivideFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(self[i] / value);
        }

        public static IList<sbyte> ElementMod(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(self[i] % other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementMod(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(self[i] % value));

            return result;
        }

        public static void ElementModFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(self[i] % other[i]);
        }

        public static void ElementModFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(self[i] % value);
        }

        public static IList<sbyte> ElementAddR(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(other[i] + self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementAddR(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(value - self[i]));

            return result;
        }

        public static void ElementAddRFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(other[i] + self[i]);
        }

        public static void ElementAddRFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(value - self[i]);
        }

        public static IList<sbyte> ElementSubtractR(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(other[i] - self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementSubtractR(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(value - self[i]));

            return result;
        }

        public static void ElementSubtractRFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(other[i] - self[i]);
        }

        public static void ElementSubtractRFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(value - self[i]);
        }

        public static IList<sbyte> ElementMultiplyR(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(other[i] * self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementMultiplyR(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(value * self[i]));

            return result;
        }

        public static void ElementMultiplyRFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(other[i] * self[i]);
        }

        public static void ElementMultiplyRFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(value * self[i]);
        }

        public static IList<sbyte> ElementDivideR(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(other[i] / self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementDivideR(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(value / self[i]));

            return result;
        }

        public static void ElementDivideRFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(other[i] / self[i]);
        }

        public static void ElementDivideRFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(value / self[i]);
        }

        public static IList<sbyte> ElementModR(this IList<sbyte> self, IList<sbyte> other)
        {
            var result = new List<sbyte>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((sbyte)(other[i] % self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<sbyte> ElementModR(this IList<sbyte> self, sbyte value)
        {
            var result = new List<sbyte>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((sbyte)(value % self[i]));

            return result;
        }

        public static void ElementModRFill(this IList<sbyte> self, IList<sbyte> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (sbyte)(other[i] % self[i]);
        }

        public static void ElementModRFill(this IList<sbyte> self, sbyte value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (sbyte)(value % self[i]);
        }

        public static IList<decimal> ElementAdd(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(self[i] + other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementAdd(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(self[i] + value));

            return result;
        }

        public static void ElementAddFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(self[i] + other[i]);
        }

        public static void ElementAddFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(self[i] + value);
        }

        public static IList<decimal> ElementSubtract(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(self[i] - other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementSubtract(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(self[i] - value));

            return result;
        }

        public static void ElementSubtractFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(self[i] - other[i]);
        }

        public static void ElementSubtractFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(self[i] - value);
        }

        public static IList<decimal> ElementMultiply(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(self[i] * other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementMultiply(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(self[i] * value));

            return result;
        }

        public static void ElementMultiplyFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(self[i] * other[i]);
        }

        public static void ElementMultiplyFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(self[i] * value);
        }

        public static IList<decimal> ElementDivide(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(self[i] / other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementDivide(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(self[i] / value));

            return result;
        }

        public static void ElementDivideFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(self[i] / other[i]);
        }

        public static void ElementDivideFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(self[i] / value);
        }

        public static IList<decimal> ElementMod(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(self[i] % other[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementMod(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(self[i] % value));

            return result;
        }

        public static void ElementModFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(self[i] % other[i]);
        }

        public static void ElementModFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(self[i] % value);
        }

        public static IList<decimal> ElementAddR(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(other[i] + self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementAddR(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(value - self[i]));

            return result;
        }

        public static void ElementAddRFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(other[i] + self[i]);
        }

        public static void ElementAddRFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(value - self[i]);
        }

        public static IList<decimal> ElementSubtractR(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(other[i] - self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementSubtractR(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(value - self[i]));

            return result;
        }

        public static void ElementSubtractRFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(other[i] - self[i]);
        }

        public static void ElementSubtractRFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(value - self[i]);
        }

        public static IList<decimal> ElementMultiplyR(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(other[i] * self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementMultiplyR(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(value * self[i]));

            return result;
        }

        public static void ElementMultiplyRFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(other[i] * self[i]);
        }

        public static void ElementMultiplyRFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(value * self[i]);
        }

        public static IList<decimal> ElementDivideR(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(other[i] / self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementDivideR(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(value / self[i]));

            return result;
        }

        public static void ElementDivideRFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(other[i] / self[i]);
        }

        public static void ElementDivideRFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(value / self[i]);
        }

        public static IList<decimal> ElementModR(this IList<decimal> self, IList<decimal> other)
        {
            var result = new List<decimal>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add((decimal)(other[i] % self[i]));
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<decimal> ElementModR(this IList<decimal> self, decimal value)
        {
            var result = new List<decimal>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add((decimal)(value % self[i]));

            return result;
        }

        public static void ElementModRFill(this IList<decimal> self, IList<decimal> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = (decimal)(other[i] % self[i]);
        }

        public static void ElementModRFill(this IList<decimal> self, decimal value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (decimal)(value % self[i]);
        }
    }
}
