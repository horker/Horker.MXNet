using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions
{
    internal static class MetaNumIListExtensions
    {
        // CUT ABOVE
        public static List<MetaNum> Sort(this IList<MetaNum> self)
        {
            var result = new List<MetaNum>(self);
            result.Sort();
            return result;
        }

        public static void FillSort(this IList<MetaNum> self)
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

        public static void FillCumulativeSum(this IList<MetaNum> self)
        {
            MetaNum sum = (MetaNum)0;

            var i = 0;
            foreach (var value in self)
            {
                sum += value;
                self[i++] = sum;
            }
        }

        // CUT BELOW
    }
}
