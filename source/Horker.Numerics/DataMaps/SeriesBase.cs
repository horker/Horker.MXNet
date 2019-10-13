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
using Horker.Numerics.Utilities;

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

        private DataMap _dataMap;

        public DataMap DataMap
        {
            get => _dataMap;
            set => _dataMap = value;
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
            value = Utils.StripOffPSObject(value);

            UnderlyingList.Add(value);
        }

        int IList.Add(object value)
        {
            value = Utils.StripOffPSObject(value);

            return UnderlyingList.Add(value);
        }

        public void Clear()
        {
            UnderlyingList.Clear();
        }

        private static bool ContainsTyped<T>(IList<T> list, T value)
        {
            return list.Contains(value);
        }

        public bool Contains(object value)
        {
            value = Utils.StripOffPSObject(value);

            return ContainsTyped((dynamic)UnderlyingList, (dynamic)value);
        }

        public void CopyTo(Array array, int index)
        {
            UnderlyingList.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return UnderlyingList.GetEnumerator();
        }

        private static int IndexOfTyped<T>(IList<T> list, T value)
        {
            return list.IndexOf(value);
        }

        public int IndexOf(object value)
        {
            value = Utils.StripOffPSObject(value);

            return IndexOfTyped((dynamic)UnderlyingList, (dynamic)value);
        }

        public void Insert(int index, object value)
        {
            value = Utils.StripOffPSObject(value);

            UnderlyingList.Insert(index, value);
        }

        private static void RemoveTyped<T>(IList<T> list, T value)
        {
            list.Remove(value);
        }

        public void Remove(object value)
        {
            value = Utils.StripOffPSObject(value);

            RemoveTyped((dynamic)UnderlyingList, (dynamic)value);
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

        // Apply and friends

        private object InvokeFuncString(string funcString, Type[] funcTypes, string methodName, Type[] methodGenericTypes,
            bool hasReturnValue, object[] arguments)
        {
            var func = FunctionCompiler.Compile(funcString, funcTypes, hasReturnValue, _dataMap, this);
            arguments[1] = func;

            // TODO: Use a cache.
            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);

            var gm = m.MakeGenericMethod(methodGenericTypes);

            return gm.Invoke(null, arguments);
        }

        private object InvokeScriptBlock(ScriptBlock scriptBlock, string methodName, Type[] methodGenericTypes, object[] arguments)
        {
            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);

            var gm = m.MakeGenericMethod(methodGenericTypes);

            arguments[0] = UnderlyingList;
            arguments[1] = scriptBlock;

            return gm.Invoke(null, arguments);
        }

        public SeriesBase Apply<T, U>(Func<T, int, U> func)
        {
            return new Series(((IList<T>)UnderlyingList).Apply(func));
        }

        public SeriesBase Apply(string funcString, Type returnType = null)
        {
            var dataType = DataType;
            returnType = returnType ?? dataType;
            return new Series((IList)InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), returnType },
                "Apply", new Type[] { dataType, returnType }, true,
                new object[] { UnderlyingList, null }));
        }

        public SeriesBase Apply(ScriptBlock scriptBlock, Type returnType = null)
        {
            var dataType = DataType;
            returnType = returnType ?? dataType;
            return new Series((IList)InvokeScriptBlock(scriptBlock,
                "ApplyScriptBlock", new Type[] { dataType, returnType },
                new object[2]));
        }

        public void ApplyFill<T>(Func<T, int, T> func)
        {
            ((IList<T>)UnderlyingList).ApplyFill(func);
        }

        public void ApplyFill(string funcString)
        {
            var dataType = DataType;
            InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), dataType },
                "ApplyFill", new Type[] { dataType }, true,
                new object[] { UnderlyingList, null });
        }

        public void ApplyFill(ScriptBlock scriptBlock)
        {
            var dataType = DataType;
            InvokeScriptBlock(scriptBlock,
                "ApplyFillScriptBlock", new Type[] { dataType },
                new object[2]);
        }

        public void ForEach<T>(Action<T, int> func)
        {
            ((IList<T>)UnderlyingList).ForEach(func);
        }

        public void ForEach(string funcString)
        {
            var dataType = DataType;
            InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int) },
                "ForEach", new Type[] { dataType }, true,
                new object[] { UnderlyingList, null });
        }

        public void ForEach(ScriptBlock scriptBlock)
        {
            GenericIListExtensions.ForEachScriptBlock((dynamic)UnderlyingList, scriptBlock);
        }

        public U Reduce<T, U>(Func<T, int, U, U> func, U initialValue)
        {
            return ((IList<T>)UnderlyingList).Reduce(func, initialValue);
        }

        public object Reduce(string funcString, object initialValue, Type returnType = null)
        {
            var dataType = DataType;
            returnType = returnType ?? dataType;
            return InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), returnType, returnType },
                "Reduce", new Type[] { dataType, returnType }, true,
                new object[] { UnderlyingList, null, initialValue });
        }

        public object Reduce(ScriptBlock scriptBlock, object initialValue, Type returnType = null)
        {
            return InvokeScriptBlock(scriptBlock,
                "ReduceScriptBlock", new Type[] { DataType },
                new object[3] { null, null, initialValue });
        }

        public int CountIf<T>(Func<T, int, bool> func)
        {
            return ((IList<T>)UnderlyingList).CountIf(func);
        }

        public int CountIf(string funcString)
        {
            var dataType = DataType;
            return (int)InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), typeof(bool) },
                "CountIf", new Type[] { dataType }, true,
                new object[] { UnderlyingList, null });
        }

        public void FillIf(ScriptBlock scriptBlock, object value)
        {
            GenericIListExtensions.FillIfScriptBlock((dynamic)UnderlyingList, scriptBlock, value);
        }

        public void FillIf<T>(Func<T, int, bool> func, T value)
        {
            ((IList<T>)UnderlyingList).FillIf(func, value);
        }

        public void FillIf(string funcString, object value)
        {
            var dataType = DataType;
            InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), typeof(bool) },
                "FillIf", new Type[] { dataType }, true,
                new object[] { UnderlyingList, null, value });
        }

        public int CountIf(ScriptBlock scriptpBlock)
        {
            return GenericIListExtensions.CountIfScriptBlock((dynamic)UnderlyingList, scriptpBlock);
        }

        public SeriesBase Filter<T>(Func<T, int, bool> func)
        {
            return new Series(((IList<T>)UnderlyingList).Filter(func));
        }

        public SeriesBase RemoveIf<T>(Func<T, int, bool> func)
        {
            return new Series(((IList<T>)UnderlyingList).RemoveIf(func));
        }

        public SeriesBase RemoveIf(string funcString)
        {
            var dataType = DataType;
            return new Series((IList)InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), typeof(bool) },
                "RemoveIf", new Type[] { dataType }, true,
                new object[] { UnderlyingList, null }));
        }

        public SeriesBase RemoveIf(ScriptBlock scriptBlock)
        {
            return new Series((IList)GenericIListExtensions.RemoveIfScriptBlock((dynamic)UnderlyingList, scriptBlock));
        }

        public SeriesBase RollingApply<T, U>(Func<T[], int, U> func, int window)
        {
            return new Series(((IList<T>)UnderlyingList).RollingApply(func, window));
        }

        public SeriesBase RollingApply(string funcString, int window, Type returnType = null)
        {
            var dataType = DataType;
            returnType = returnType ?? dataType;
            return new Series((IList)InvokeFuncString(funcString,
                new Type[] { dataType.MakeArrayType(), typeof(int), returnType },
                "RollingApply", new Type[] { dataType, returnType }, true,
                new object[] { UnderlyingList, null, window }));
        }

        public SeriesBase RollingApply(ScriptBlock scriptBlock, int window)
        {
            return new Series((IList)InvokeScriptBlock(scriptBlock,
                "RollingApplyScriptBlock", new Type[] { DataType },
                new object[3] { null, null, window }));
        }

        public void RollingApplyFill<T, U>(Func<T[], int, U> func, int window)
        {
            ((IList<T>)UnderlyingList).RollingApply(func, window);
        }

        public void RollingApplyFill(string funcString, int window)
        {
            var dataType = DataType;
            InvokeFuncString(funcString,
                new Type[] { dataType.MakeArrayType(), typeof(int) },
                "RollingApply", new Type[] { dataType }, true,
                new object[] { null, null, window });
        }

        public void RollingApplyFill(ScriptBlock scriptBlock, int window)
        {
            GenericIListExtensions.RollingApplyScriptBlock((dynamic)UnderlyingList, scriptBlock, window);
        }

        public bool All<T>(Func<T, int, bool> func)
        {
            return ((IList<T>)UnderlyingList).All(func);
        }

        public bool All(string funcString)
        {
            var dataType = DataType;
            return (bool)InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), typeof(bool) },
                "All", new Type[] { dataType }, true,
                new object[] { UnderlyingList, null });
        }

        public bool All(ScriptBlock scriptBlock)
        {
            return (bool)GenericIListExtensions.AllScriptBlock((dynamic)UnderlyingList, scriptBlock);
        }

        public bool Any<T>(Func<T, int, bool> func)
        {
            return ((IList<T>)UnderlyingList).Any(func);
        }

        public bool Any(string funcString)
        {
            var dataType = DataType;
            return (bool)InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), typeof(bool) },
                "Any", new Type[] { dataType }, true,
                new object[] { UnderlyingList, null });
        }

        public bool Any(ScriptBlock scriptBlock)
        {
            return (bool)GenericIListExtensions.AnyScriptBlock((dynamic)UnderlyingList, scriptBlock);
        }

        // Comparison operators

        public SeriesBase Eq(IList other)
        {
            return new Series(UnderlyingList.Eq(other));
        }

        public SeriesBase Eq(object value)
        {
            return new Series(UnderlyingList.Eq(value));
        }

        public SeriesBase Ne(IList other)
        {
            return new Series(UnderlyingList.Ne(other));
        }

        public SeriesBase Ne(object value)
        {
            return new Series(UnderlyingList.Ne(value));
        }

        public SeriesBase Lt(IList other)
        {
            return new Series(UnderlyingList.Lt(other));
        }

        public SeriesBase Lt(object value)
        {
            return new Series(UnderlyingList.Lt(value));
        }

        public SeriesBase Le(IList other)
        {
            return new Series(UnderlyingList.Le(other));
        }

        public SeriesBase Le(object value)
        {
            return new Series(UnderlyingList.Le(value));
        }

        public SeriesBase Gt(IList other)
        {
            return new Series(UnderlyingList.Gt(other));
        }

        public SeriesBase Gt(object value)
        {
            return new Series(UnderlyingList.Gt(value));
        }

        public SeriesBase Ge(IList other)
        {
            return new Series(UnderlyingList.Ge(other));
        }

        public SeriesBase Ge(object value)
        {
            return new Series(UnderlyingList.Ge(value));
        }

        public SeriesBase Between(object left, object right, bool inclusive = true)
        {
            return new Series(UnderlyingList.Between(left, right, inclusive));
        }

        // Other methods

        public static SeriesBase MapTyped<T>(IList<T> list, IDictionary map)
        {
            if (!(map is IDictionary<T, T> typedMap))
            {
                typedMap = new Dictionary<T, T>();
                foreach (DictionaryEntry entry in map)
                {
                    var key = SmartConverter.ConvertTo<T>(entry.Key);
                    var value = SmartConverter.ConvertTo<T>(entry.Value);
                    typedMap.Add(key, value);
                }
            }

            return new Series(list.Map(typedMap));
        }

        public SeriesBase Map(IDictionary map)
        {
            return MapTyped((dynamic)UnderlyingList, map);
        }

        public static void MapFillTyped<T>(IList<T> list, IDictionary map)
        {
            if (!(map is IDictionary<T, T> typedMap))
            {
                typedMap = new Dictionary<T, T>();
                foreach (DictionaryEntry entry in map)
                {
                    var key = SmartConverter.ConvertTo<T>(entry.Key);
                    var value = SmartConverter.ConvertTo<T>(entry.Value);
                    typedMap.Add(key, value);
                }
            }

            list.MapFill(typedMap);
        }

        public void MapFill(IDictionary map)
        {
            MapFillTyped((dynamic)UnderlyingList, map);
        }

        // Transformers

        public DataMap OneHot(OneHotType oneHotType = OneHotType.OneHot, string columnNameFormat = "{1}_{0}")
        {
            var trans = new OneHotTransformer<double>(oneHotType, columnNameFormat);
            return trans.FitTransformToDataMap(this);
        }

        public SeriesBase DummyEncoding()
        {
            var trans = new DummyEncodingTransformer<double>();
            return trans.FitTransform(this);
        }
    }
}
