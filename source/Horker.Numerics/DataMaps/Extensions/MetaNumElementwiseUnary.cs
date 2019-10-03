using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions
{
    using ReturnType = MetaNum;

    public partial class GenericIListExtensions
    {
        // CUT ABOVE
        public static IList<ReturnType> Negate(this IList<MetaNum> self)
        {
            var result = new List<ReturnType>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add(-self[i]);

            return result;
        }

        public static void NegateFill(this IList<MetaNum> self)
        {
            for (var i = 0; i < self.Count; ++i)
                self[i] = (MetaNum)(-self[i]);
        }

        // CUT BELOW
    }
}
