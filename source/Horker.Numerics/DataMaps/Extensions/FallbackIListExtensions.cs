using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
/*
namespace Horker.Numerics.DataMaps.Extensions
{
    public static partial class IListExtensions
    {
        // IList versions of GenericIListExtensions

        public static IList Sort(this IList self)
        {
            var result = new List<object>(self.ToArray<object>());
            result.Sort();
            return result;
        }

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
                var l = self as List<MetaNum>;
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
                if (value == null)
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
            var summary = new Summary()
            {
                Count = self.Count,
                NaN = CountNaN(self),
                Unique = CountUnique(self)
            };

            return summary;
        }

        public static IList RemoveNaN(this IList self)
        {
            var result = new List<object>(self.Count);
            foreach (var value in self)
                if (value != null)
                    result.Add(value);

            return result;
        }

        public static IList FillNaN(this IList self, object fillValue)
        {
            var result = new List<object>(self.Count);
            foreach (var value in self)
            {
                if (value != null)
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
                if (self[i] == null)
                    self[i] = fillValue;
            }
        }

    }
}
*/
