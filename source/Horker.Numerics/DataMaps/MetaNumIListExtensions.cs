using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions
{
    public static partial class MetaNumIListExtensions
    {
        public static bool IsNaN(MetaNum self)
        {
            return false;
        }

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
                if (IsNaN(value))
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
            summary.Min = sorted[0];
            summary.Q1 = (MetaNum)(q ? sorted[count / 4] : (sorted[count / 4] + sorted[count / 4 + 1]) / 2);
            summary.Mean = (MetaNum)(sum / count);
            summary.Median = (MetaNum)(even ? sorted[count / 2] : (sorted[count / 2] + sorted[count / 2 + 1]) / 2);
            summary.Q3 = (MetaNum)(q ? sorted[count / 4 * 3] : (sorted[count / 4 * 3] + sorted[count / 4 * 3 + 1]) / 2);
            summary.Max = sorted[count - 1];

            return summary;
        }

        // CUT BELOW
    }
}
