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
			Correlation,
			Cor,
			Covariance,
			CumulativeMaxFill,
			CumulativeMinFill,
			CumulativeProductFill,
			CumulativeSumFill,
			CountNaN,
			CountUnique,
			Describe,
			FillNaNFill,
			Kurtosis,
			Max,
			Min,
			Mean,
			Median,
			Mode,
			Quantile,
			Skewness,
			StandardDeviation,
			Std,
			Variance,
			Var,
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

        public object Correlation(SeriesBase other, bool skipNaN = true)
        {
			var m = GetMethodInfo(MethodIndex.Correlation);

            return (object)m.Invoke(null, new object[] { UnderlyingList, other.UnderlyingList, skipNaN });
        }

        public object Cor(SeriesBase other, bool skipNaN = true)
        {
			var m = GetMethodInfo(MethodIndex.Cor);

            return (object)m.Invoke(null, new object[] { UnderlyingList, other.UnderlyingList, skipNaN });
        }

        public object Covariance(SeriesBase other)
        {
			var m = GetMethodInfo(MethodIndex.Covariance);

            return (object)m.Invoke(null, new object[] { UnderlyingList, other.UnderlyingList });
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

        public object Kurtosis(bool unbiased = true)
        {
			var m = GetMethodInfo(MethodIndex.Kurtosis);

            return (object)m.Invoke(null, new object[] { UnderlyingList, unbiased });
        }

        public object Max()
        {
			var m = GetMethodInfo(MethodIndex.Max);

            return (object)m.Invoke(null, new object[] { UnderlyingList });
        }

        public object Min()
        {
			var m = GetMethodInfo(MethodIndex.Min);

            return (object)m.Invoke(null, new object[] { UnderlyingList });
        }

        public object Mean(bool skipNaN = true)
        {
			var m = GetMethodInfo(MethodIndex.Mean);

            return (object)m.Invoke(null, new object[] { UnderlyingList, skipNaN });
        }

        public object Median(bool skipNaN = true, bool isSorted = false)
        {
			var m = GetMethodInfo(MethodIndex.Median);

            return (object)m.Invoke(null, new object[] { UnderlyingList, skipNaN, isSorted });
        }

        public object Mode(bool skipNaN = true)
        {
			var m = GetMethodInfo(MethodIndex.Mode);

            return (object)m.Invoke(null, new object[] { UnderlyingList, skipNaN });
        }

        public object Quantile(bool skipNaN = true, bool isSorted = false)
        {
			var m = GetMethodInfo(MethodIndex.Quantile);

            return (object)m.Invoke(null, new object[] { UnderlyingList, skipNaN, isSorted });
        }

        public object Skewness(bool unbiased = true)
        {
			var m = GetMethodInfo(MethodIndex.Skewness);

            return (object)m.Invoke(null, new object[] { UnderlyingList, unbiased });
        }

        public object StandardDeviation(bool unbiased = true, bool skipNaN = true)
        {
			var m = GetMethodInfo(MethodIndex.StandardDeviation);

            return (object)m.Invoke(null, new object[] { UnderlyingList, unbiased, skipNaN });
        }

        public object Std(bool unbiased = true, bool skipNaN = true)
        {
			var m = GetMethodInfo(MethodIndex.Std);

            return (object)m.Invoke(null, new object[] { UnderlyingList, unbiased, skipNaN });
        }

        public object Variance(bool unbiased = true, bool skipNaN = true)
        {
			var m = GetMethodInfo(MethodIndex.Variance);

            return (object)m.Invoke(null, new object[] { UnderlyingList, unbiased, skipNaN });
        }

        public object Var(bool unbiased = true, bool skipNaN = true)
        {
			var m = GetMethodInfo(MethodIndex.Var);

            return (object)m.Invoke(null, new object[] { UnderlyingList, unbiased, skipNaN });
        }

        public void SortFill()
        {
			var m = GetMethodInfo(MethodIndex.SortFill);

            m.Invoke(null, new object[] { UnderlyingList });
        }

	}
}
