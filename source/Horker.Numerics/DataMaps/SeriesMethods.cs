using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public partial class SeriesBase : IList
    {
        enum MethodIndex
        {
			CumulativeSum,
			FillCumulativeSum,
			Sort,
			FillSort,
        }

        public SeriesBase CumulativeSum()
        {
            if (!_methodCache.TryGetValue(DataType, out var methodTable))
                throw new InvalidOperationException($"CumulativeSum() is not supported for type {DataType}");

            var m = methodTable[(int)MethodIndex.CumulativeSum];
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public SeriesBase FillCumulativeSum()
        {
            if (!_methodCache.TryGetValue(DataType, out var methodTable))
                throw new InvalidOperationException($"FillCumulativeSum() is not supported for type {DataType}");

            var m = methodTable[(int)MethodIndex.FillCumulativeSum];
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public SeriesBase Sort()
        {
            if (!_methodCache.TryGetValue(DataType, out var methodTable))
                throw new InvalidOperationException($"Sort() is not supported for type {DataType}");

            var m = methodTable[(int)MethodIndex.Sort];
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public SeriesBase FillSort()
        {
            if (!_methodCache.TryGetValue(DataType, out var methodTable))
                throw new InvalidOperationException($"FillSort() is not supported for type {DataType}");

            var m = methodTable[(int)MethodIndex.FillSort];
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

	}
}
