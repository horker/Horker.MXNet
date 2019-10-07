using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions.Internal
{
    public static partial class MetaNumIListExtensions
    {
        // CUT ABOVE
        public static IList<MetaNum> ElementAdd(this IList<MetaNum> self, IList<MetaNum> other)
        {
            var result = new List<MetaNum>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add(self[i] + other[i]);
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<MetaNum> ElementAdd(this IList<MetaNum> self, MetaNum value)
        {
            var result = new List<MetaNum>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add(self[i] + value);

            return result;
        }

        public static void ElementAddFill(this IList<MetaNum> self, IList<MetaNum> other)
        {
            var i = 0;
            for (; i < Math.Min(self.Count, other.Count); ++i)
                self[i] = self[i] + other[i];
        }

        public static void ElementAddFill(this IList<MetaNum> self, MetaNum value)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = self[i] + value;
        }

        // CUT BELOW
    }
}
