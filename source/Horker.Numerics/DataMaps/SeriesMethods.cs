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
			CumulativeMax,
			CumulativeMin,
			CumulativeProduct,
			CumulativeSum,
			FillNaN,
			GetUnique,
			RemoveNaN,
			CumulativeMaxFill,
			CumulativeMinFill,
			CumulativeProductFill,
			CumulativeSumFill,
			CountNaN,
			CountUnique,
			Describe,
			FillNaNFill,
			SortFill,
        }

        public SeriesBase CumulativeMax()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeMax);
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public SeriesBase CumulativeMin()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeMin);
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public SeriesBase CumulativeProduct()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeProduct);
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public SeriesBase CumulativeSum()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeSum);
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public SeriesBase FillNaN(object fillValue)
        {
			var m = GetMethodInfo(MethodIndex.FillNaN);
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList, fillValue }));
        }

        public SeriesBase GetUnique()
        {
			var m = GetMethodInfo(MethodIndex.GetUnique);
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public SeriesBase RemoveNaN()
        {
			var m = GetMethodInfo(MethodIndex.RemoveNaN);
            return new Series((IList)m.Invoke(null, new object[] { UnderlyingList }));
        }

        public void CumulativeMaxFill()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeMaxFill);

            m.Invoke(null, new object[] { UnderlyingList });
        }

        public void CumulativeMinFill()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeMinFill);

            m.Invoke(null, new object[] { UnderlyingList });
        }

        public void CumulativeProductFill()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeProductFill);

            m.Invoke(null, new object[] { UnderlyingList });
        }

        public void CumulativeSumFill()
        {
			var m = GetMethodInfo(MethodIndex.CumulativeSumFill);

            m.Invoke(null, new object[] { UnderlyingList });
        }

        public int CountNaN()
        {
			var m = GetMethodInfo(MethodIndex.CountNaN);

            return (int)m.Invoke(null, new object[] { UnderlyingList });
        }

        public int CountUnique()
        {
			var m = GetMethodInfo(MethodIndex.CountUnique);

            return (int)m.Invoke(null, new object[] { UnderlyingList });
        }

        public Summary Describe()
        {
			var m = GetMethodInfo(MethodIndex.Describe);

            return (Summary)m.Invoke(null, new object[] { UnderlyingList });
        }

        public void FillNaNFill(object fillValue)
        {
			var m = GetMethodInfo(MethodIndex.FillNaNFill);

            m.Invoke(null, new object[] { UnderlyingList, fillValue });
        }

        public void SortFill()
        {
			var m = GetMethodInfo(MethodIndex.SortFill);

            m.Invoke(null, new object[] { UnderlyingList });
        }

	}
}
