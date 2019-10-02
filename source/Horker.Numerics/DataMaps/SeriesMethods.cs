using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps.Extensions;

namespace Horker.Numerics.DataMaps
{
    public partial class SeriesBase : IList
    {
        public SeriesBase CumulativeMax()
        {
			var result = GenericIListExtensions.CumulativeMax((dynamic)UnderlyingList);
            return new Series((IList)result);
        }

        public SeriesBase CumulativeMin()
        {
			var result = GenericIListExtensions.CumulativeMin((dynamic)UnderlyingList);
            return new Series((IList)result);
        }

        public SeriesBase CumulativeProduct()
        {
			var result = GenericIListExtensions.CumulativeProduct((dynamic)UnderlyingList);
            return new Series((IList)result);
        }

        public SeriesBase CumulativeSum()
        {
			var result = GenericIListExtensions.CumulativeSum((dynamic)UnderlyingList);
            return new Series((IList)result);
        }

        public SeriesBase FillNaN(object fillValue)
        {
			var result = GenericIListExtensions.FillNaN((dynamic)UnderlyingList, (dynamic)fillValue);
            return new Series((IList)result);
        }

        public SeriesBase GetUnique()
        {
			var result = GenericIListExtensions.GetUnique((dynamic)UnderlyingList);
            return new Series((IList)result);
        }

        public SeriesBase RemoveNaN()
        {
			var result = GenericIListExtensions.RemoveNaN((dynamic)UnderlyingList);
            return new Series((IList)result);
        }

        public object Correlation(SeriesBase other, bool skipNaN = true)
        {
			return (object)GenericIListExtensions.Correlation((dynamic)UnderlyingList, (dynamic)other.UnderlyingList, skipNaN);
        }

        public object Cor(SeriesBase other, bool skipNaN = true)
        {
			return (object)GenericIListExtensions.Cor((dynamic)UnderlyingList, (dynamic)other.UnderlyingList, skipNaN);
        }

        public object Covariance(SeriesBase other, bool unbiased = true, bool skipNaN = true)
        {
			return (object)GenericIListExtensions.Covariance((dynamic)UnderlyingList, (dynamic)other.UnderlyingList, unbiased, skipNaN);
        }

        public object Cov(SeriesBase other, bool unbiased = true, bool skipNaN = true)
        {
			return (object)GenericIListExtensions.Cov((dynamic)UnderlyingList, (dynamic)other.UnderlyingList, unbiased, skipNaN);
        }

        public void CumulativeMaxFill()
        {
			GenericIListExtensions.CumulativeMaxFill((dynamic)UnderlyingList);
        }

        public void CumulativeMinFill()
        {
			GenericIListExtensions.CumulativeMinFill((dynamic)UnderlyingList);
        }

        public void CumulativeProductFill()
        {
			GenericIListExtensions.CumulativeProductFill((dynamic)UnderlyingList);
        }

        public void CumulativeSumFill()
        {
			GenericIListExtensions.CumulativeSumFill((dynamic)UnderlyingList);
        }

        public int CountNaN()
        {
			return (int)GenericIListExtensions.CountNaN((dynamic)UnderlyingList);
        }

        public int CountUnique()
        {
			return (int)GenericIListExtensions.CountUnique((dynamic)UnderlyingList);
        }

        public Summary Describe()
        {
			return (Summary)GenericIListExtensions.Describe((dynamic)UnderlyingList);
        }

        public void FillNaNFill(object fillValue)
        {
			GenericIListExtensions.FillNaNFill((dynamic)UnderlyingList, (dynamic)fillValue);
        }

        public object Kurtosis(bool unbiased = true)
        {
			return (object)GenericIListExtensions.Kurtosis((dynamic)UnderlyingList, unbiased);
        }

        public object Max()
        {
			return (object)GenericIListExtensions.Max((dynamic)UnderlyingList);
        }

        public object Min()
        {
			return (object)GenericIListExtensions.Min((dynamic)UnderlyingList);
        }

        public object Mean(bool skipNaN = true)
        {
			return (object)GenericIListExtensions.Mean((dynamic)UnderlyingList, skipNaN);
        }

        public object Median(bool skipNaN = true, bool isSorted = false)
        {
			return (object)GenericIListExtensions.Median((dynamic)UnderlyingList, skipNaN, isSorted);
        }

        public object Mode(bool skipNaN = true)
        {
			return (object)GenericIListExtensions.Mode((dynamic)UnderlyingList, skipNaN);
        }

        public object Quantile(double p, bool skipNaN = true, bool isSorted = false)
        {
			return (object)GenericIListExtensions.Quantile((dynamic)UnderlyingList, p, skipNaN, isSorted);
        }

        public object Skewness(bool unbiased = true)
        {
			return (object)GenericIListExtensions.Skewness((dynamic)UnderlyingList, unbiased);
        }

        public object StandardDeviation(bool unbiased = true, bool skipNaN = true)
        {
			return (object)GenericIListExtensions.StandardDeviation((dynamic)UnderlyingList, unbiased, skipNaN);
        }

        public object Std(bool unbiased = true, bool skipNaN = true)
        {
			return (object)GenericIListExtensions.Std((dynamic)UnderlyingList, unbiased, skipNaN);
        }

        public object Variance(bool unbiased = true, bool skipNaN = true)
        {
			return (object)GenericIListExtensions.Variance((dynamic)UnderlyingList, unbiased, skipNaN);
        }

        public object Var(bool unbiased = true, bool skipNaN = true)
        {
			return (object)GenericIListExtensions.Var((dynamic)UnderlyingList, unbiased, skipNaN);
        }

        public void SortFill()
        {
			GenericIListExtensions.SortFill((dynamic)UnderlyingList);
        }

	}
}
