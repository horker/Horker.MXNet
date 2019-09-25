using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions
{
    internal static class MetaStructIListExtensions
    {
        public static bool IsNaN(MetaStruct self)
        {
            return false;
        }

        // CUT ABOVE
        public static List<MetaStruct> Sort(this IList<MetaStruct> self)
        {
            var result = new List<MetaStruct>(self);
            result.Sort();
            return result;
        }

        public static void SortFill(this IList<MetaStruct> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.Name.StartsWith("List`"))
            {
                var l = self as List<MetaStruct>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static int CountNaN(this IList<MetaStruct> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static IList<MetaStruct> GetUnique(this IList<MetaStruct> self)
        {
            var unique = new HashSet<MetaStruct>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList<MetaStruct> self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList<MetaStruct> self)
        {
            var summary = new Summary();
            summary.Count = self.Count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            return summary;
        }

        public static IList<MetaStruct> RemoveNaN(this IList<MetaStruct> self)
        {
            var result = new List<MetaStruct>(self.Count);
            foreach (var value in self)
                if (!IsNaN(value))
                    result.Add(value);

            return result;
        }

        // CUT BELOW
    }
}
