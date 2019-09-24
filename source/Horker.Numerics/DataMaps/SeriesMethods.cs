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
			Sort,
			CumulativeSumFill,
			SortFill,
			CountNaN,
			Describe,
        }

        public SeriesBase CumulativeSum()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeSum);
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public SeriesBase Sort()
        {
			var m = GetMethodInfo(MethodIndex.Sort);
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public void CumulativeSumFill()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeSumFill);

            m.Invoke(null, new object[] { UnderlyingList });
        }

        public void SortFill()
        {
			var m = GetMethodInfo(MethodIndex.SortFill);

            m.Invoke(null, new object[] { UnderlyingList });
        }

        public int CountNaN()
        {
			var m = GetMethodInfo(MethodIndex.CountNaN);

            return (int)m.Invoke(null, new object[] { UnderlyingList });
        }

        public ISummary Describe()
        {
			var m = GetMethodInfo(MethodIndex.Describe);

            return (ISummary)m.Invoke(null, new object[] { UnderlyingList });
        }

	}
}
