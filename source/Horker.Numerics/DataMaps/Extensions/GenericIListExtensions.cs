using System;
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
                    if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
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
                if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
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
            summary.Std = StandardDeviation(self);

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

        public static double Kurtosis(this IList<double> self, bool unbiased = true)
        {
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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

        public static double Median(this IList<double> self, bool skipNaN = true)
        {
            // TODO: Accord.NET uses partial sort for efficiency.

            var values = self.ToArray();
            Array.Sort(values);

            var offset = 0;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait.IsNaN(values[offset]))
                    ++offset;
            }

            var actualCount = self.Count - offset;
            var half = offset + actualCount / 2;

            if (actualCount % 2 == 0)
                return ((double)values[half - 1] + (double)values[half]) / 2;
            else
                return (double)values[half];
        }

        public static double Mode(this IList<double> self, bool skipNaN = true)
        {
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
                while (TypeTrait.IsNaN(values[i]))
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
            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait.IsNaN(v))
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
                    if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
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
                if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
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
            summary.Std = StandardDeviation(self);

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

        public static float Kurtosis(this IList<float> self, bool unbiased = true)
        {
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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

        public static float Median(this IList<float> self, bool skipNaN = true)
        {
            // TODO: Accord.NET uses partial sort for efficiency.

            var values = self.ToArray();
            Array.Sort(values);

            var offset = 0;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait.IsNaN(values[offset]))
                    ++offset;
            }

            var actualCount = self.Count - offset;
            var half = offset + actualCount / 2;

            if (actualCount % 2 == 0)
                return ((float)values[half - 1] + (float)values[half]) / 2;
            else
                return (float)values[half];
        }

        public static float Mode(this IList<float> self, bool skipNaN = true)
        {
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
                while (TypeTrait.IsNaN(values[i]))
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
            float mean = Mean(self, skipNaN);
            if (float.IsNaN(mean))
                return float.NaN;

            float variance = (float)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                float v = (float)value;
                if (TypeTrait.IsNaN(v))
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
                    if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
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
                if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
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
            summary.Std = StandardDeviation(self);

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

        public static double Kurtosis(this IList<long> self, bool unbiased = true)
        {
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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

        public static double Median(this IList<long> self, bool skipNaN = true)
        {
            // TODO: Accord.NET uses partial sort for efficiency.

            var values = self.ToArray();
            Array.Sort(values);

            var offset = 0;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait.IsNaN(values[offset]))
                    ++offset;
            }

            var actualCount = self.Count - offset;
            var half = offset + actualCount / 2;

            if (actualCount % 2 == 0)
                return ((double)values[half - 1] + (double)values[half]) / 2;
            else
                return (double)values[half];
        }

        public static long Mode(this IList<long> self, bool skipNaN = true)
        {
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
                while (TypeTrait.IsNaN(values[i]))
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
            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait.IsNaN(v))
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
                    if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
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
                if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
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
            summary.Std = StandardDeviation(self);

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

        public static double Kurtosis(this IList<int> self, bool unbiased = true)
        {
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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

        public static double Median(this IList<int> self, bool skipNaN = true)
        {
            // TODO: Accord.NET uses partial sort for efficiency.

            var values = self.ToArray();
            Array.Sort(values);

            var offset = 0;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait.IsNaN(values[offset]))
                    ++offset;
            }

            var actualCount = self.Count - offset;
            var half = offset + actualCount / 2;

            if (actualCount % 2 == 0)
                return ((double)values[half - 1] + (double)values[half]) / 2;
            else
                return (double)values[half];
        }

        public static int Mode(this IList<int> self, bool skipNaN = true)
        {
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
                while (TypeTrait.IsNaN(values[i]))
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
            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait.IsNaN(v))
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
                    if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
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
                if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
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
            summary.Std = StandardDeviation(self);

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

        public static double Kurtosis(this IList<short> self, bool unbiased = true)
        {
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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

        public static double Median(this IList<short> self, bool skipNaN = true)
        {
            // TODO: Accord.NET uses partial sort for efficiency.

            var values = self.ToArray();
            Array.Sort(values);

            var offset = 0;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait.IsNaN(values[offset]))
                    ++offset;
            }

            var actualCount = self.Count - offset;
            var half = offset + actualCount / 2;

            if (actualCount % 2 == 0)
                return ((double)values[half - 1] + (double)values[half]) / 2;
            else
                return (double)values[half];
        }

        public static short Mode(this IList<short> self, bool skipNaN = true)
        {
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
                while (TypeTrait.IsNaN(values[i]))
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
            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait.IsNaN(v))
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
                    if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
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
                if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
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
            summary.Std = StandardDeviation(self);

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

        public static double Kurtosis(this IList<byte> self, bool unbiased = true)
        {
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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

        public static double Median(this IList<byte> self, bool skipNaN = true)
        {
            // TODO: Accord.NET uses partial sort for efficiency.

            var values = self.ToArray();
            Array.Sort(values);

            var offset = 0;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait.IsNaN(values[offset]))
                    ++offset;
            }

            var actualCount = self.Count - offset;
            var half = offset + actualCount / 2;

            if (actualCount % 2 == 0)
                return ((double)values[half - 1] + (double)values[half]) / 2;
            else
                return (double)values[half];
        }

        public static byte Mode(this IList<byte> self, bool skipNaN = true)
        {
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
                while (TypeTrait.IsNaN(values[i]))
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
            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait.IsNaN(v))
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
                    if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
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
                if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
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
            summary.Std = StandardDeviation(self);

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

        public static double Kurtosis(this IList<sbyte> self, bool unbiased = true)
        {
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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

        public static double Median(this IList<sbyte> self, bool skipNaN = true)
        {
            // TODO: Accord.NET uses partial sort for efficiency.

            var values = self.ToArray();
            Array.Sort(values);

            var offset = 0;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait.IsNaN(values[offset]))
                    ++offset;
            }

            var actualCount = self.Count - offset;
            var half = offset + actualCount / 2;

            if (actualCount % 2 == 0)
                return ((double)values[half - 1] + (double)values[half]) / 2;
            else
                return (double)values[half];
        }

        public static sbyte Mode(this IList<sbyte> self, bool skipNaN = true)
        {
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
                while (TypeTrait.IsNaN(values[i]))
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
            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait.IsNaN(v))
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
                    if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
                        continue;

                    s1.Add(self[i]);
                    s2.Add(other[i]);
                }
                return Covariance(s1, s2) / s1.StandardDeviation() / s2.StandardDeviation();
            }

            return Covariance(self, other) / self.StandardDeviation() / other.StandardDeviation();
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
                if (TypeTrait.IsNaN(self[i]) || TypeTrait.IsNaN(other[i]))
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
            summary.Std = StandardDeviation(self);

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

        public static double Kurtosis(this IList<decimal> self, bool unbiased = true)
        {
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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
            var i = 0;
            while (TypeTrait.IsNaN(self[0]) && i < self.Count - 1)
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

        public static double Median(this IList<decimal> self, bool skipNaN = true)
        {
            // TODO: Accord.NET uses partial sort for efficiency.

            var values = self.ToArray();
            Array.Sort(values);

            var offset = 0;

            if (skipNaN)
            {
                // After sort, NaNs should be collected to the first location of the sequence.
                while (TypeTrait.IsNaN(values[offset]))
                    ++offset;
            }

            var actualCount = self.Count - offset;
            var half = offset + actualCount / 2;

            if (actualCount % 2 == 0)
                return ((double)values[half - 1] + (double)values[half]) / 2;
            else
                return (double)values[half];
        }

        public static decimal Mode(this IList<decimal> self, bool skipNaN = true)
        {
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
                while (TypeTrait.IsNaN(values[i]))
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
            double mean = Mean(self, skipNaN);
            if (double.IsNaN(mean))
                return double.NaN;

            double variance = (double)0.0;
            int actualCount = self.Count;

            foreach (var value in self)
            {
                double v = (double)value;
                if (TypeTrait.IsNaN(v))
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
