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
            get => IListExtensions.GetDataType(UnderlyingList);
        }

        // Method cache

        private static Dictionary<Type, MethodInfo[]> _methodCache;
        private static MethodInfo[] _fallbackMethodCache;

        private static readonly Type[] _types = new Type[]
        {
            typeof(double), typeof(float), typeof(long), typeof(int), typeof(short),
            typeof(byte), typeof(sbyte), typeof(decimal),
            typeof(bool), typeof(string), typeof(DateTime), typeof(DateTimeOffset)
        };

        static SeriesBase()
        {
            var names = typeof(MethodIndex).GetEnumNames();

            // Set up the fallback method cache.

            _fallbackMethodCache = new MethodInfo[names.Length];

            for (var i = 0; i < names.Length; ++i)
            {
                var m = typeof(Extensions.IListExtensions).GetMethod(names[i], BindingFlags.Static | BindingFlags.Public);
                _fallbackMethodCache[i] = m;
            }

            // Set up the method cache.

            var methodNameIndexMap = new Dictionary<string, int>();
            for (var i = 0; i < names.Length; ++i)
                methodNameIndexMap.Add(names[i], i);

            _methodCache = new Dictionary<Type, MethodInfo[]>();

            foreach (var t in _types)
            {
                var mi = new MethodInfo[names.Length];
                Array.Copy(_fallbackMethodCache, mi, names.Length);
                _methodCache.Add(t, mi);
            }

            foreach (var m in typeof(Extensions.GenericIListExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public))
            {
                if (!methodNameIndexMap.TryGetValue(m.Name, out var index))
                    continue;
                var listType = m.GetParameters()[0].ParameterType.GetGenericArguments()[0];
                if (_methodCache.TryGetValue(listType, out var cache))
                    cache[index] = m;
            }
        }

        private MethodInfo GetMethodInfo(MethodIndex index)
        {
            MethodInfo m;
            if (!_methodCache.TryGetValue(DataType, out var methodTable))
                m = _fallbackMethodCache[(int)index];
            else
                m = methodTable[(int)index];

            if (m == null)
            {
                var methodName = typeof(MethodIndex).GetEnumName(index);
                throw new InvalidOperationException($"Operation {methodName}() is not supported for type {DataType.Name}");
            }

            return m;
        }

        // IList implementation

        public virtual object this[int index] { get => UnderlyingList[index]; set => UnderlyingList[index] = value; }

        public bool IsReadOnly => UnderlyingList.IsReadOnly;

        public bool IsFixedSize => UnderlyingList.IsFixedSize;

        public int Count => UnderlyingList.Count;

        public object SyncRoot => UnderlyingList.SyncRoot;

        public bool IsSynchronized => UnderlyingList.IsSynchronized;

        public int Add(object value)
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

        // Conversion methods

        public T[] ToArray<T>()
        {
            return IListExtensions.ToArray<T>(UnderlyingList);
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
            return IListExtensions.ToList<T>(UnderlyingList);
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
            return IListExtensions.AsArray<T>(UnderlyingList);
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
            return IListExtensions.AsList<T>(UnderlyingList);
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
            return IListExtensions.Convert<T>(UnderlyingList);
        }

        public IList Convert(Type type)
        {
            return IListExtensions.Convert(UnderlyingList, type);
        }

        public IList Convert(Type[] possibleTypes = null, bool raiseError = false)
        {
            return IListExtensions.Convert(UnderlyingList, possibleTypes, raiseError);
        }

        public SortedListIndexSeries ToSortedListIndexSeries()
        {
            return new SortedListIndexSeries(UnderlyingList);
        }

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

        // LINQ-like methods

        private SeriesBase SelectByFunc<S, T>(Func<S, int, T> func)
        {
            var result = new List<T>();

            var i = 0;
            foreach (var e in UnderlyingList.AsList<S>())
            {
                var value = func.Invoke(e, i++);
                result.Add(value);
            }

            return new Series(result);
        }

        private SeriesBase SelectByScriptBlock<T>(ScriptBlock scriptBlock)
        {
            var result = new List<T>();

            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("x"));
            parameters.Add(new PSVariable("i"));

            var i = 0;
            foreach (var e in UnderlyingList)
            {
                parameters[0].Value = e;
                parameters[1].Value = i++;
                var rawValue = scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject;
                var value = OperatorFuncs<T>.GetConvertOperatorFunc(rawValue.GetType()).Invoke(rawValue);
                result.Add(value);
            }

            return new Series(result);
        }

        public SeriesBase Select(string funcString, Type returnType = null)
        {
            returnType = returnType ?? DataType;
            var func = FunctionCompiler.Compile(funcString, new Type[] { DataType, typeof(int), returnType });

            var m = typeof(SeriesBase).GetMethod("SelectByFunc", BindingFlags.NonPublic | BindingFlags.Instance);
            var gm = m.MakeGenericMethod(new Type[] { DataType, returnType });
            return (SeriesBase)gm.Invoke(this, new object[] { func });
        }

        public SeriesBase Select<S, T>(Func<S, int, T> func)
        {
            return SelectByFunc(func);
        }

        public SeriesBase Select(ScriptBlock scriptBlock, Type returnType = null)
        {
            returnType = returnType ?? DataType;
            var m = typeof(SeriesBase).GetMethod("SelectByScriptBlock", BindingFlags.NonPublic | BindingFlags.Instance);
            var gm = m.MakeGenericMethod(new Type[] { returnType });
            return (SeriesBase)gm.Invoke(this, new object[] { scriptBlock });
        }

        // Transformers

        public DataMap OneHot(OneHotType oneHotType = OneHotType.OneHot, string columnNameFormat = "{0}")
        {
            var trans = new OneHotSeriesTransformer(oneHotType, columnNameFormat);
            return trans.FitTransformToDataMap(this);
        }
    }
}
