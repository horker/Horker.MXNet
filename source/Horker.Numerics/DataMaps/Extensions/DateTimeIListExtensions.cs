using Horker.Numerics.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions.Internal
{
    static partial class DateTimeIListExtensions
    {
        // CUT ABOVE
        public static List<int> Year(this IList<DateTime> self)
        {
            var result = new List<int>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add(self[i].Year);

            return result;
        }

        // CUT BELOW
    }
}
