using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Horker.Numerics.Transformers;
using Horker.Numerics.DataMaps.Extensions;
using System.Management.Automation;
using System.Diagnostics;

namespace Horker.Numerics.DataMaps
{
    public partial class SeriesBase : IList
    {
        // Methods to be overrriden by subclasses

        public virtual IList UnderlyingList
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public virtual object this[object index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        // Properties

        public Type DataType
        {
            get => GenericIListExtensions.GetDataType(UnderlyingList);
        }

        // IList implementation

        public virtual object this[int index] { get => UnderlyingList[index]; set => UnderlyingList[index] = value; }

        public bool IsReadOnly => UnderlyingList.IsReadOnly;

        public bool IsFixedSize => UnderlyingList.IsFixedSize;

        public int Count => UnderlyingList.Count;

        public object SyncRoot => UnderlyingList.SyncRoot;

        public bool IsSynchronized => UnderlyingList.IsSynchronized;

        public void Add(object value)
        {
            UnderlyingList.Add(value);
        }

        int IList.Add(object value)
        {
            return UnderlyingList.Add(value);
        }

        public void Clear()
        {
            UnderlyingList.Clear();
        }

        public bool Contains(object value)
        {
            return Contains(value);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return UnderlyingList.GetEnumerator();
        }

        public int IndexOf(object value)
        {
            return UnderlyingList.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            UnderlyingList.Insert(index, value);
        }

        public void Remove(object value)
        {
            UnderlyingList.Remove(value);
        }

        public void RemoveAt(int index)
        {
            UnderlyingList.RemoveAt(index);
        }

        public void AddRange(IList other)
        {
            foreach (var value in other)
                UnderlyingList.Add(value);
        }

        // Conversion methods

        public T[] ToArray<T>()
        {
            return GenericIListExtensions.ToArray<T>(UnderlyingList);
        }

        public Array ToArray(Type type = null)
        {
            type = type ?? DataType;

            if (type == typeof(double)) return ToArray<double>();
            if (type == typeof(float)) return ToArray<float>();
            if (type == typeof(long)) return ToArray<long>();
            if (type == typeof(int)) return ToArray<int>();
            if (type == typeof(short)) return ToArray<short>();
            if (type == typeof(byte)) return ToArray<byte>();
            if (type == typeof(sbyte)) return ToArray<sbyte>();
            if (type == typeof(decimal)) return ToArray<decimal>();
            if (type == typeof(bool)) return ToArray<bool>();
            if (type == typeof(string)) return ToArray<string>();
            if (type == typeof(DateTime)) return ToArray<DateTime>();
            if (type == typeof(DateTimeOffset)) return ToArray<DateTimeOffset>();

            return ToArray<object>();
        }

        public IList<T> ToList<T>()
        {
            return GenericIListExtensions.ToList<T>(UnderlyingList);
        }

        public object ToList(Type type = null)
        {
            type = type ?? DataType;

            if (type == typeof(double)) return ToList<double>();
            if (type == typeof(float)) return ToList<float>();
            if (type == typeof(long)) return ToList<long>();
            if (type == typeof(int)) return ToList<int>();
            if (type == typeof(short)) return ToList<short>();
            if (type == typeof(byte)) return ToList<byte>();
            if (type == typeof(sbyte)) return ToList<sbyte>();
            if (type == typeof(decimal)) return ToList<decimal>();
            if (type == typeof(bool)) return ToList<bool>();
            if (type == typeof(string)) return ToList<string>();
            if (type == typeof(DateTime)) return ToList<DateTime>();
            if (type == typeof(DateTimeOffset)) return ToList<DateTimeOffset>();

            return ToList<object>();
        }

        public T[] AsArray<T>()
        {
            return GenericIListExtensions.AsArray<T>(UnderlyingList);
        }

        public Array AsArray(Type type = null)
        {
            type = type ?? DataType;

            if (type == typeof(double)) return AsArray<double>();
            if (type == typeof(float)) return AsArray<float>();
            if (type == typeof(long)) return AsArray<long>();
            if (type == typeof(int)) return AsArray<int>();
            if (type == typeof(short)) return AsArray<short>();
            if (type == typeof(byte)) return AsArray<byte>();
            if (type == typeof(sbyte)) return AsArray<sbyte>();
            if (type == typeof(decimal)) return AsArray<decimal>();
            if (type == typeof(bool)) return AsArray<bool>();
            if (type == typeof(string)) return AsArray<string>();
            if (type == typeof(DateTime)) return AsArray<DateTime>();
            if (type == typeof(DateTimeOffset)) return AsArray<DateTimeOffset>();

            return AsArray<object>();
        }

        public IList<T> AsList<T>()
        {
            return GenericIListExtensions.AsList<T>(UnderlyingList);
        }

        public object AsList(Type type = null)
        {
            type = type ?? DataType;

            if (type == typeof(double)) return AsList<double>();
            if (type == typeof(float)) return AsList<float>();
            if (type == typeof(long)) return AsList<long>();
            if (type == typeof(int)) return AsList<int>();
            if (type == typeof(short)) return AsList<short>();
            if (type == typeof(byte)) return AsList<byte>();
            if (type == typeof(sbyte)) return AsList<sbyte>();
            if (type == typeof(decimal)) return AsList<decimal>();
            if (type == typeof(bool)) return AsList<bool>();
            if (type == typeof(string)) return AsList<string>();
            if (type == typeof(DateTime)) return AsList<DateTime>();
            if (type == typeof(DateTimeOffset)) return AsList<DateTimeOffset>();

            return AsList<object>();
        }

        public List<T> Convert<T>()
        {
            return GenericIListExtensions.Convert<T>(UnderlyingList);
        }

        public IList Convert(Type type)
        {
            return GenericIListExtensions.Convert(UnderlyingList, type);
        }

        public IList Convert(Type[] possibleTypes = null, bool raiseError = false)
        {
            return GenericIListExtensions.Convert(UnderlyingList, possibleTypes, raiseError);
        }

        public SortedListIndexSeries ToSortedListIndexSeries()
        {
            return new SortedListIndexSeries(UnderlyingList);
        }

        // Arithmetic operators

        public static SeriesBase operator -(SeriesBase self) { return self.Negate(); }

        public static SeriesBase operator +(SeriesBase lhs, SeriesBase rhs) { return lhs.ElementAdd(rhs);  }
        public static SeriesBase operator -(SeriesBase lhs, SeriesBase rhs) { return lhs.ElementSubtract(rhs);  }
        public static SeriesBase operator *(SeriesBase lhs, SeriesBase rhs) { return lhs.ElementMultiply(rhs);  }
        public static SeriesBase operator /(SeriesBase lhs, SeriesBase rhs) { return lhs.ElementDivide(rhs);  }

        public static SeriesBase operator +(SeriesBase lhs, object rhs) { return lhs.ElementAdd(rhs);  }
        public static SeriesBase operator -(SeriesBase lhs, object rhs) { return lhs.ElementSubtract(rhs);  }
        public static SeriesBase operator *(SeriesBase lhs, object rhs) { return lhs.ElementMultiply(rhs);  }
        public static SeriesBase operator /(SeriesBase lhs, object rhs) { return lhs.ElementDivide(rhs);  }

        // Implicit conversion operators

        public static implicit operator SeriesBase(Array value) { return new Series(value); }
        public static implicit operator SeriesBase(List<double> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<float> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<long> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<int> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<short> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<byte> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<sbyte> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<string> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<bool> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<DateTime> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<DateTimeOffset> value) { return new Series(value); }

        // Apply methods

        private string ChooseApplyMethodName(object lambda, string stringName, string scriptBlockName)
        {
            Debug.Assert(stringName.Replace("FuncString", "") == scriptBlockName.Replace("ScriptBlock", ""));

            string methodName;

            if (lambda is string)
                methodName = stringName;
            else if (lambda is ScriptBlock)
                methodName = scriptBlockName;
            else
                throw new ArgumentException("Invalid script block");

            return methodName;
        }

        public SeriesBase Apply(object lambda, Type returnType = null)
        {
            var dataType = DataType;
            returnType = returnType ?? dataType;

            var methodName = ChooseApplyMethodName(lambda, "ApplyFuncString", "ApplyScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType, returnType });

            var result = (IList)gm.Invoke(null, new object[] { UnderlyingList, lambda, null, this });
            return new Series(result);
        }

        public void ApplyFill(object lambda)
        {
            var dataType = DataType;

            var methodName = ChooseApplyMethodName(lambda, "ApplyFillFuncString", "ApplyFillScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType });
            gm.Invoke(null, new object[] { UnderlyingList, lambda, null, null });
        }

        public void ForEach(object lambda)
        {
            var dataType = DataType;

            var methodName = ChooseApplyMethodName(lambda, "ForEachFuncString", "ForEachScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType });

            gm.Invoke(null, new object[] { UnderlyingList, lambda, null, null });
        }

        public object Reduce(object lambda, object initialValue, Type returnType = null)
        {
            var dataType = DataType;

            if (returnType == null)
            {
                if (initialValue is PSObject pso)
                    returnType = pso.BaseObject.GetType();
                else
                    returnType = initialValue.GetType();
            }

            var methodName = ChooseApplyMethodName(lambda, "ReduceFuncString", "ReduceScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType, returnType });

            return gm.Invoke(null, new object[] { UnderlyingList, lambda, initialValue, null, null });
        }

        public int CountIf(object lambda)
        {
            var dataType = DataType;

            var methodName = ChooseApplyMethodName(lambda, "CountIfFuncString", "CountIfScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType });

            return (int)gm.Invoke(null, new object[] { UnderlyingList, lambda, null, null });
        }

        public SeriesBase RemoveIf(object lambda)
        {
            var dataType = DataType;

            var methodName = ChooseApplyMethodName(lambda, "RemoveIfFuncString", "RemoveIfScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType });

            var result = (IList)gm.Invoke(null, new object[] { UnderlyingList, lambda, null, null });
            return new Series(result);
        }

        public SeriesBase RollingApply(object lambda)
        {
            var dataType = DataType;

            var methodName = ChooseApplyMethodName(lambda, "RollingApplyFuncString", "RollingApplyScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType });

            var result = (IList)gm.Invoke(null, new object[] { UnderlyingList, lambda, null, null });
            return new Series(result);
        }

        public void RollingApplyFill(object lambda)
        {
            var dataType = DataType;

            var methodName = ChooseApplyMethodName(lambda, "RollingApplyFillFuncString", "RollingApplyFillScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType });

            gm.Invoke(null, new object[] { UnderlyingList, lambda, null, null });
        }

        public bool All(object lambda)
        {
            var dataType = DataType;

            var methodName = ChooseApplyMethodName(lambda, "AllFuncString", "AllScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType });

            var result = (bool)gm.Invoke(null, new object[] { UnderlyingList, lambda, null, null });
            return result;
        }

        public bool Any(object lambda)
        {
            var dataType = DataType;

            var methodName = ChooseApplyMethodName(lambda, "AnyFuncString", "AnyScriptBlock");

            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { dataType });

            var result = (bool)gm.Invoke(null, new object[] { UnderlyingList, lambda, null, null });
            return result;
        }

        // Comparison operators

        public SeriesBase Eq(IList other)
        {
            return new Series((IList)UnderlyingList.Eq(other));
        }

        public SeriesBase Eq(object value)
        {
            return new Series((IList)UnderlyingList.Eq(value));
        }

        public SeriesBase Ne(IList other)
        {
            return new Series((IList)UnderlyingList.Ne(other));
        }

        public SeriesBase Ne(object value)
        {
            return new Series((IList)UnderlyingList.Ne(value));
        }

        public SeriesBase Lt(IList other)
        {
            return new Series((IList)UnderlyingList.Lt(other));
        }

        public SeriesBase Lt(object value)
        {
            return new Series((IList)UnderlyingList.Lt(value));
        }

        public SeriesBase Le(IList other)
        {
            return new Series((IList)UnderlyingList.Le(other));
        }

        public SeriesBase Le(object value)
        {
            return new Series((IList)UnderlyingList.Le(value));
        }

        public SeriesBase Gt(IList other)
        {
            return new Series((IList)UnderlyingList.Gt(other));
        }

        public SeriesBase Gt(object value)
        {
            return new Series((IList)UnderlyingList.Gt(value));
        }

        public SeriesBase Ge(IList other)
        {
            return new Series((IList)UnderlyingList.Ge(other));
        }

        public SeriesBase Ge(object value)
        {
            return new Series((IList)UnderlyingList.Ge(value));
        }

        public SeriesBase Between(object left, object right, bool inclusive = true)
        {
            return new Series((IList)UnderlyingList.Between(left, right, inclusive));
        }

        // Transformers

        public DataMap OneHot(OneHotType oneHotType = OneHotType.OneHot, string columnNameFormat = "{0}")
        {
            var trans = new OneHotSeriesTransformer(oneHotType, columnNameFormat);
            return trans.FitTransformToDataMap(this);
        }
    }
}
