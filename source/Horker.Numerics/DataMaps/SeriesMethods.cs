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
    public partial class SeriesBase
    {
        public SeriesBase Abs()
        {
			try
			{
				var result = GenericIListExtensions.Abs((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Abs() does not support data type {DataType}");
			}
        }

        public SeriesBase Acos()
        {
			try
			{
				var result = GenericIListExtensions.Acos((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Acos() does not support data type {DataType}");
			}
        }

        public SeriesBase Asin()
        {
			try
			{
				var result = GenericIListExtensions.Asin((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Asin() does not support data type {DataType}");
			}
        }

        public SeriesBase Atan()
        {
			try
			{
				var result = GenericIListExtensions.Atan((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Atan() does not support data type {DataType}");
			}
        }

        public SeriesBase Atan2(double x)
        {
			try
			{
				var result = GenericIListExtensions.Atan2((dynamic)UnderlyingList, x);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Atan2() does not support data type {DataType}");
			}
        }

        public SeriesBase Ceiling()
        {
			try
			{
				var result = GenericIListExtensions.Ceiling((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Ceiling() does not support data type {DataType}");
			}
        }

        public SeriesBase Cos()
        {
			try
			{
				var result = GenericIListExtensions.Cos((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Cos() does not support data type {DataType}");
			}
        }

        public SeriesBase Exp()
        {
			try
			{
				var result = GenericIListExtensions.Exp((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Exp() does not support data type {DataType}");
			}
        }

        public SeriesBase Floor()
        {
			try
			{
				var result = GenericIListExtensions.Floor((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Floor() does not support data type {DataType}");
			}
        }

        public SeriesBase Log()
        {
			try
			{
				var result = GenericIListExtensions.Log((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Log() does not support data type {DataType}");
			}
        }

        public SeriesBase Log10()
        {
			try
			{
				var result = GenericIListExtensions.Log10((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Log10() does not support data type {DataType}");
			}
        }

        public SeriesBase Negate()
        {
			try
			{
				var result = GenericIListExtensions.Negate((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Negate() does not support data type {DataType}");
			}
        }

        public SeriesBase Pow(double exp)
        {
			try
			{
				var result = GenericIListExtensions.Pow((dynamic)UnderlyingList, exp);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Pow() does not support data type {DataType}");
			}
        }

        public SeriesBase Round(int digits = 0, MidpointRounding mode = MidpointRounding.ToEven)
        {
			try
			{
				var result = GenericIListExtensions.Round((dynamic)UnderlyingList, digits, mode);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Round() does not support data type {DataType}");
			}
        }

        public SeriesBase Sign()
        {
			try
			{
				var result = GenericIListExtensions.Sign((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Sign() does not support data type {DataType}");
			}
        }

        public SeriesBase Sin()
        {
			try
			{
				var result = GenericIListExtensions.Sin((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Sin() does not support data type {DataType}");
			}
        }

        public SeriesBase Sinh()
        {
			try
			{
				var result = GenericIListExtensions.Sinh((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Sinh() does not support data type {DataType}");
			}
        }

        public SeriesBase Sqrt()
        {
			try
			{
				var result = GenericIListExtensions.Sqrt((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Sqrt() does not support data type {DataType}");
			}
        }

        public SeriesBase Tan()
        {
			try
			{
				var result = GenericIListExtensions.Tan((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Tan() does not support data type {DataType}");
			}
        }

        public SeriesBase Tanh()
        {
			try
			{
				var result = GenericIListExtensions.Tanh((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Tanh() does not support data type {DataType}");
			}
        }

        public SeriesBase Truncate()
        {
			try
			{
				var result = GenericIListExtensions.Truncate((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Truncate() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementAdd(SeriesBase other)
        {
			try
			{
				var result = GenericIListExtensions.ElementAdd((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementAdd() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementAdd(object other)
        {
			try
			{
				var result = GenericIListExtensions.ElementAdd((dynamic)UnderlyingList, (dynamic)other);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementAdd() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementSubtract(SeriesBase other)
        {
			try
			{
				var result = GenericIListExtensions.ElementSubtract((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementSubtract() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementSubtract(object other)
        {
			try
			{
				var result = GenericIListExtensions.ElementSubtract((dynamic)UnderlyingList, (dynamic)other);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementSubtract() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementMultiply(SeriesBase other)
        {
			try
			{
				var result = GenericIListExtensions.ElementMultiply((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementMultiply() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementMultiply(object other)
        {
			try
			{
				var result = GenericIListExtensions.ElementMultiply((dynamic)UnderlyingList, (dynamic)other);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementMultiply() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementDivide(SeriesBase other)
        {
			try
			{
				var result = GenericIListExtensions.ElementDivide((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementDivide() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementDivide(object other)
        {
			try
			{
				var result = GenericIListExtensions.ElementDivide((dynamic)UnderlyingList, (dynamic)other);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementDivide() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementAddR(SeriesBase other)
        {
			try
			{
				var result = GenericIListExtensions.ElementAddR((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementAddR() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementAddR(object other)
        {
			try
			{
				var result = GenericIListExtensions.ElementAddR((dynamic)UnderlyingList, (dynamic)other);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementAddR() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementSubtractR(SeriesBase other)
        {
			try
			{
				var result = GenericIListExtensions.ElementSubtractR((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementSubtractR() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementSubtractR(object other)
        {
			try
			{
				var result = GenericIListExtensions.ElementSubtractR((dynamic)UnderlyingList, (dynamic)other);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementSubtractR() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementMultiplyR(SeriesBase other)
        {
			try
			{
				var result = GenericIListExtensions.ElementMultiplyR((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementMultiplyR() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementMultiplyR(object other)
        {
			try
			{
				var result = GenericIListExtensions.ElementMultiplyR((dynamic)UnderlyingList, (dynamic)other);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementMultiplyR() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementDivideR(SeriesBase other)
        {
			try
			{
				var result = GenericIListExtensions.ElementDivideR((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementDivideR() does not support data type {DataType}");
			}
        }

        public SeriesBase ElementDivideR(object other)
        {
			try
			{
				var result = GenericIListExtensions.ElementDivideR((dynamic)UnderlyingList, (dynamic)other);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementDivideR() does not support data type {DataType}");
			}
        }

        public SeriesBase Copy()
        {
			try
			{
				var result = GenericIListExtensions.Copy((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Copy() does not support data type {DataType}");
			}
        }

        public SeriesBase CreateLike()
        {
			try
			{
				var result = GenericIListExtensions.CreateLike((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CreateLike() does not support data type {DataType}");
			}
        }

        public SeriesBase CumulativeMax()
        {
			try
			{
				var result = GenericIListExtensions.CumulativeMax((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CumulativeMax() does not support data type {DataType}");
			}
        }

        public SeriesBase CumulativeMin()
        {
			try
			{
				var result = GenericIListExtensions.CumulativeMin((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CumulativeMin() does not support data type {DataType}");
			}
        }

        public SeriesBase CumulativeProduct()
        {
			try
			{
				var result = GenericIListExtensions.CumulativeProduct((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CumulativeProduct() does not support data type {DataType}");
			}
        }

        public SeriesBase CumulativeSum()
        {
			try
			{
				var result = GenericIListExtensions.CumulativeSum((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CumulativeSum() does not support data type {DataType}");
			}
        }

        public SeriesBase Fill(object value)
        {
			try
			{
				var result = GenericIListExtensions.Fill((dynamic)UnderlyingList, (dynamic)value);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Fill() does not support data type {DataType}");
			}
        }

        public SeriesBase Shuffle(int seed = -1)
        {
			try
			{
				var result = GenericIListExtensions.Shuffle((dynamic)UnderlyingList, seed);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Shuffle() does not support data type {DataType}");
			}
        }

        public SeriesBase SortedCopy()
        {
			try
			{
				var result = GenericIListExtensions.SortedCopy((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"SortedCopy() does not support data type {DataType}");
			}
        }

        public SeriesBase FillNaN(object fillValue)
        {
			try
			{
				var result = GenericIListExtensions.FillNaN((dynamic)UnderlyingList, (dynamic)fillValue);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"FillNaN() does not support data type {DataType}");
			}
        }

        public SeriesBase RemoveNaN()
        {
			try
			{
				var result = GenericIListExtensions.RemoveNaN((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"RemoveNaN() does not support data type {DataType}");
			}
        }

        public SeriesBase Unique()
        {
			try
			{
				var result = GenericIListExtensions.Unique((dynamic)UnderlyingList);
				return new Series((IList)result);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Unique() does not support data type {DataType}");
			}
        }

        public void AbsFill()
        {
			try
			{
				GenericIListExtensions.AbsFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"AbsFill() does not support data type {DataType}");
			}
        }

        public void AcosFill()
        {
			try
			{
				GenericIListExtensions.AcosFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"AcosFill() does not support data type {DataType}");
			}
        }

        public void AsinFill()
        {
			try
			{
				GenericIListExtensions.AsinFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"AsinFill() does not support data type {DataType}");
			}
        }

        public void AtanFill()
        {
			try
			{
				GenericIListExtensions.AtanFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"AtanFill() does not support data type {DataType}");
			}
        }

        public void Atan2Fill(double x)
        {
			try
			{
				GenericIListExtensions.Atan2Fill((dynamic)UnderlyingList, x);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Atan2Fill() does not support data type {DataType}");
			}
        }

        public void CeilingFill()
        {
			try
			{
				GenericIListExtensions.CeilingFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CeilingFill() does not support data type {DataType}");
			}
        }

        public void CosFill()
        {
			try
			{
				GenericIListExtensions.CosFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CosFill() does not support data type {DataType}");
			}
        }

        public void ExpFill()
        {
			try
			{
				GenericIListExtensions.ExpFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ExpFill() does not support data type {DataType}");
			}
        }

        public void LogFill()
        {
			try
			{
				GenericIListExtensions.LogFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"LogFill() does not support data type {DataType}");
			}
        }

        public void Log10Fill()
        {
			try
			{
				GenericIListExtensions.Log10Fill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Log10Fill() does not support data type {DataType}");
			}
        }

        public void NegateFill()
        {
			try
			{
				GenericIListExtensions.NegateFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"NegateFill() does not support data type {DataType}");
			}
        }

        public void PowFill(double exp)
        {
			try
			{
				GenericIListExtensions.PowFill((dynamic)UnderlyingList, exp);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"PowFill() does not support data type {DataType}");
			}
        }

        public void SignFill()
        {
			try
			{
				GenericIListExtensions.SignFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"SignFill() does not support data type {DataType}");
			}
        }

        public void SinFill()
        {
			try
			{
				GenericIListExtensions.SinFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"SinFill() does not support data type {DataType}");
			}
        }

        public void SinhFill()
        {
			try
			{
				GenericIListExtensions.SinhFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"SinhFill() does not support data type {DataType}");
			}
        }

        public void SqrtFill()
        {
			try
			{
				GenericIListExtensions.SqrtFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"SqrtFill() does not support data type {DataType}");
			}
        }

        public void TanFill()
        {
			try
			{
				GenericIListExtensions.TanFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"TanFill() does not support data type {DataType}");
			}
        }

        public void TanhFill()
        {
			try
			{
				GenericIListExtensions.TanhFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"TanhFill() does not support data type {DataType}");
			}
        }

        public void TruncateFill()
        {
			try
			{
				GenericIListExtensions.TruncateFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"TruncateFill() does not support data type {DataType}");
			}
        }

        public void ElementAddFill(SeriesBase other)
        {
			try
			{
				GenericIListExtensions.ElementAddFill((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementAddFill() does not support data type {DataType}");
			}
        }

        public void ElementAddFill(object other)
        {
			try
			{
				GenericIListExtensions.ElementAddFill((dynamic)UnderlyingList, (dynamic)other);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementAddFill() does not support data type {DataType}");
			}
        }

        public void ElementSubtractFill(SeriesBase other)
        {
			try
			{
				GenericIListExtensions.ElementSubtractFill((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementSubtractFill() does not support data type {DataType}");
			}
        }

        public void ElementSubtractFill(object other)
        {
			try
			{
				GenericIListExtensions.ElementSubtractFill((dynamic)UnderlyingList, (dynamic)other);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementSubtractFill() does not support data type {DataType}");
			}
        }

        public void ElementMultiplyFill(SeriesBase other)
        {
			try
			{
				GenericIListExtensions.ElementMultiplyFill((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementMultiplyFill() does not support data type {DataType}");
			}
        }

        public void ElementMultiplyFill(object other)
        {
			try
			{
				GenericIListExtensions.ElementMultiplyFill((dynamic)UnderlyingList, (dynamic)other);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementMultiplyFill() does not support data type {DataType}");
			}
        }

        public void ElementDivideFill(SeriesBase other)
        {
			try
			{
				GenericIListExtensions.ElementDivideFill((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementDivideFill() does not support data type {DataType}");
			}
        }

        public void ElementDivideFill(object other)
        {
			try
			{
				GenericIListExtensions.ElementDivideFill((dynamic)UnderlyingList, (dynamic)other);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementDivideFill() does not support data type {DataType}");
			}
        }

        public void ElementAddRFill(SeriesBase other)
        {
			try
			{
				GenericIListExtensions.ElementAddRFill((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementAddRFill() does not support data type {DataType}");
			}
        }

        public void ElementAddRFill(object other)
        {
			try
			{
				GenericIListExtensions.ElementAddRFill((dynamic)UnderlyingList, (dynamic)other);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementAddRFill() does not support data type {DataType}");
			}
        }

        public void ElementSubtractRFill(SeriesBase other)
        {
			try
			{
				GenericIListExtensions.ElementSubtractRFill((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementSubtractRFill() does not support data type {DataType}");
			}
        }

        public void ElementSubtractRFill(object other)
        {
			try
			{
				GenericIListExtensions.ElementSubtractRFill((dynamic)UnderlyingList, (dynamic)other);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementSubtractRFill() does not support data type {DataType}");
			}
        }

        public void ElementMultiplyRFill(SeriesBase other)
        {
			try
			{
				GenericIListExtensions.ElementMultiplyRFill((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementMultiplyRFill() does not support data type {DataType}");
			}
        }

        public void ElementMultiplyRFill(object other)
        {
			try
			{
				GenericIListExtensions.ElementMultiplyRFill((dynamic)UnderlyingList, (dynamic)other);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementMultiplyRFill() does not support data type {DataType}");
			}
        }

        public void ElementDivideRFill(SeriesBase other)
        {
			try
			{
				GenericIListExtensions.ElementDivideRFill((dynamic)UnderlyingList, (dynamic)other.UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementDivideRFill() does not support data type {DataType}");
			}
        }

        public void ElementDivideRFill(object other)
        {
			try
			{
				GenericIListExtensions.ElementDivideRFill((dynamic)UnderlyingList, (dynamic)other);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ElementDivideRFill() does not support data type {DataType}");
			}
        }

        public object Correlation(SeriesBase other, bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.Correlation((dynamic)UnderlyingList, (dynamic)other.UnderlyingList, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Correlation() does not support data type {DataType}");
			}
        }

        public object Cor(SeriesBase other, bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.Cor((dynamic)UnderlyingList, (dynamic)other.UnderlyingList, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Cor() does not support data type {DataType}");
			}
        }

        public object Covariance(SeriesBase other, bool unbiased = true, bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.Covariance((dynamic)UnderlyingList, (dynamic)other.UnderlyingList, unbiased, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Covariance() does not support data type {DataType}");
			}
        }

        public object Cov(SeriesBase other, bool unbiased = true, bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.Cov((dynamic)UnderlyingList, (dynamic)other.UnderlyingList, unbiased, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Cov() does not support data type {DataType}");
			}
        }

        public void CumulativeMaxFill()
        {
			try
			{
				GenericIListExtensions.CumulativeMaxFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CumulativeMaxFill() does not support data type {DataType}");
			}
        }

        public void CumulativeMinFill()
        {
			try
			{
				GenericIListExtensions.CumulativeMinFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CumulativeMinFill() does not support data type {DataType}");
			}
        }

        public void CumulativeProductFill()
        {
			try
			{
				GenericIListExtensions.CumulativeProductFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CumulativeProductFill() does not support data type {DataType}");
			}
        }

        public void CumulativeSumFill()
        {
			try
			{
				GenericIListExtensions.CumulativeSumFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CumulativeSumFill() does not support data type {DataType}");
			}
        }

        public int CountNaN()
        {
			try
			{
				return (int)GenericIListExtensions.CountNaN((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CountNaN() does not support data type {DataType}");
			}
        }

        public int CountUnique()
        {
			try
			{
				return (int)GenericIListExtensions.CountUnique((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CountUnique() does not support data type {DataType}");
			}
        }

        public object CountValues()
        {
			try
			{
				return (object)GenericIListExtensions.CountValues((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"CountValues() does not support data type {DataType}");
			}
        }

        public Summary Describe()
        {
			try
			{
				return (Summary)GenericIListExtensions.Describe((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Describe() does not support data type {DataType}");
			}
        }

        public void FillFill(object value)
        {
			try
			{
				GenericIListExtensions.FillFill((dynamic)UnderlyingList, (dynamic)value);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"FillFill() does not support data type {DataType}");
			}
        }

        public void FillNaNFill(object fillValue)
        {
			try
			{
				GenericIListExtensions.FillNaNFill((dynamic)UnderlyingList, (dynamic)fillValue);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"FillNaNFill() does not support data type {DataType}");
			}
        }

        public HistogramBin[] Histogram(int binCount = -1, double binWidth = double.NaN)
        {
			try
			{
				return (HistogramBin[])GenericIListExtensions.Histogram((dynamic)UnderlyingList, binCount, binWidth);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Histogram() does not support data type {DataType}");
			}
        }

        public object Kurtosis(bool unbiased = true)
        {
			try
			{
				return (object)GenericIListExtensions.Kurtosis((dynamic)UnderlyingList, unbiased);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Kurtosis() does not support data type {DataType}");
			}
        }

        public object Max()
        {
			try
			{
				return (object)GenericIListExtensions.Max((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Max() does not support data type {DataType}");
			}
        }

        public object Min()
        {
			try
			{
				return (object)GenericIListExtensions.Min((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Min() does not support data type {DataType}");
			}
        }

        public object Mean(bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.Mean((dynamic)UnderlyingList, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Mean() does not support data type {DataType}");
			}
        }

        public object Median(bool skipNaN = true, bool isSorted = false)
        {
			try
			{
				return (object)GenericIListExtensions.Median((dynamic)UnderlyingList, skipNaN, isSorted);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Median() does not support data type {DataType}");
			}
        }

        public object Mode(bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.Mode((dynamic)UnderlyingList, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Mode() does not support data type {DataType}");
			}
        }

        public object Quantile(double p, bool skipNaN = true, bool isSorted = false)
        {
			try
			{
				return (object)GenericIListExtensions.Quantile((dynamic)UnderlyingList, p, skipNaN, isSorted);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Quantile() does not support data type {DataType}");
			}
        }

        public void ShuffleFill(int seed = -1)
        {
			try
			{
				GenericIListExtensions.ShuffleFill((dynamic)UnderlyingList, seed);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"ShuffleFill() does not support data type {DataType}");
			}
        }

        public object Skewness(bool unbiased = true)
        {
			try
			{
				return (object)GenericIListExtensions.Skewness((dynamic)UnderlyingList, unbiased);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Skewness() does not support data type {DataType}");
			}
        }

        public object StandardDeviation(bool unbiased = true, bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.StandardDeviation((dynamic)UnderlyingList, unbiased, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"StandardDeviation() does not support data type {DataType}");
			}
        }

        public object Std(bool unbiased = true, bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.Std((dynamic)UnderlyingList, unbiased, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Std() does not support data type {DataType}");
			}
        }

        public object Variance(bool unbiased = true, bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.Variance((dynamic)UnderlyingList, unbiased, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Variance() does not support data type {DataType}");
			}
        }

        public object Var(bool unbiased = true, bool skipNaN = true)
        {
			try
			{
				return (object)GenericIListExtensions.Var((dynamic)UnderlyingList, unbiased, skipNaN);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"Var() does not support data type {DataType}");
			}
        }

        public void SortFill()
        {
			try
			{
				GenericIListExtensions.SortFill((dynamic)UnderlyingList);
			}
			catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
			{
				throw new InvalidOperationException($"SortFill() does not support data type {DataType}");
			}
        }

	}
}
