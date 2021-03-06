﻿using System;
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
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Horker.Numerics.DataMaps
{
    [Serializable]
    public partial class SeriesBase : ISerializable
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

        public object First => this[0];

        public object Last => this[Count - 1];

        // ISerializable implementation

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void Save(Stream stream)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }

        public void Save(string path)
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                Save(stream);
            }
        }

        public static SeriesBase Load(Stream stream)
        {
            var formatter = new BinaryFormatter();
            return (SeriesBase)formatter.Deserialize(stream);
        }

        public static SeriesBase Load(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return Load(stream);
            }
        }

        // IList implementation

        public IList Values => UnderlyingList;

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

        private T InvokeConversionMethod<T>(string methodName, Type type)
        {
            // TODO: use cache
            type = type ?? DataType;
            var m = typeof(GenericIListExtensions).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            var gm = m.MakeGenericMethod(new[] { type });
            return (T)gm.Invoke(null, new[] { UnderlyingList });
        }

        public T[] ToArray<T>()
        {
            return GenericIListExtensions.ToArray<T>(UnderlyingList);
        }

        public Array ToArray(Type type = null)
        {
            return InvokeConversionMethod<Array>("ToArray", type);
        }

        public List<T> ToList<T>()
        {
            return GenericIListExtensions.ToList<T>(UnderlyingList);
        }

        public IList ToList(Type type = null)
        {
            return InvokeConversionMethod<IList>("ToList", type);
        }

        public T[] AsArray<T>()
        {
            return GenericIListExtensions.AsArray<T>(UnderlyingList);
        }

        public Array AsArray(Type type = null)
        {
            return InvokeConversionMethod<Array>("AsArray", type);
        }

        public IList<T> AsList<T>()
        {
            return GenericIListExtensions.AsList<T>(UnderlyingList);
        }

        public IList AsList(Type type = null)
        {
            return InvokeConversionMethod<IList>("AsList", type);
        }

        public SeriesBase Cast<T>()
        {
            return new Series(ToList<T>());
        }

        public SeriesBase Cast(Type type)
        {
            return new Series(ToList(type));
        }

        public SeriesBase CastDown()
        {
            return new Series(GenericIListExtensions.CastDown(UnderlyingList));
        }

        public IList TryConversion(Type[] possibleTypes = null, bool raiseError = false)
        {
            return GenericIListExtensions.TryConversion(UnderlyingList, possibleTypes, raiseError);
        }

        public SortedListIndexSeries ToSortedListIndexSeries()
        {
            return new SortedListIndexSeries(UnderlyingList);
        }

        public void EnsureDataType(Type type)
        {
            if (type == DataType)
                return;

            UnderlyingList = ToList(type);
        }

        public void EnsureSizeable()
        {
            if (!UnderlyingList.IsFixedSize)
                return;

            var listType = typeof(List<>).MakeGenericType(DataType);
            UnderlyingList = (IList)Activator.CreateInstance(listType, UnderlyingList);
        }

        public void EnsureWritable()
        {
            if (!UnderlyingList.IsReadOnly)
                return;

            var listType = typeof(List<>).MakeGenericType(DataType);
            UnderlyingList = (IList)Activator.CreateInstance(listType, UnderlyingList);
        }

        // Arithmetic operators

        public static SeriesBase operator -(SeriesBase self) { return self.Negate(); }

        public static SeriesBase operator +(SeriesBase lhs, SeriesBase rhs) { return lhs.ElementAdd(rhs);  }
        public static SeriesBase operator -(SeriesBase lhs, SeriesBase rhs) { return lhs.ElementSubtract(rhs);  }
        public static SeriesBase operator *(SeriesBase lhs, SeriesBase rhs) { return lhs.ElementMultiply(rhs);  }
        public static SeriesBase operator /(SeriesBase lhs, SeriesBase rhs) { return lhs.ElementDivide(rhs);  }
        public static SeriesBase operator %(SeriesBase lhs, SeriesBase rhs) { return lhs.ElementMod(rhs);  }

        public static SeriesBase operator +(SeriesBase lhs, object rhs) { return lhs.ElementAdd(rhs);  }
        public static SeriesBase operator -(SeriesBase lhs, object rhs) { return lhs.ElementSubtract(rhs);  }
        public static SeriesBase operator *(SeriesBase lhs, object rhs) { return lhs.ElementMultiply(rhs);  }
        public static SeriesBase operator /(SeriesBase lhs, object rhs) { return lhs.ElementDivide(rhs);  }
        public static SeriesBase operator %(SeriesBase lhs, object rhs) { return lhs.ElementMod(rhs);  }

        public static SeriesBase operator +(object lhs, SeriesBase rhs) { return rhs.ElementAddR(lhs);  }
        public static SeriesBase operator -(object lhs, SeriesBase rhs) { return rhs.ElementSubtractR(lhs);  }
        public static SeriesBase operator *(object lhs, SeriesBase rhs) { return rhs.ElementMultiplyR(lhs);  }
        public static SeriesBase operator /(object lhs, SeriesBase rhs) { return rhs.ElementDivideR(lhs);  }
        public static SeriesBase operator %(object lhs, SeriesBase rhs) { return rhs.ElementModR(lhs);  }

        // Implicit conversion operators to allow to assign arrays/lists to Series objects

        public static implicit operator SeriesBase(Array value) { return new Series(value); }
        public static implicit operator SeriesBase(List<double> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<float> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<long> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<int> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<short> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<byte> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<sbyte> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<decimal> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<string> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<bool> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<DateTime> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<DateTimeOffset> value) { return new Series(value); }
        public static implicit operator SeriesBase(List<object> value) { return new Series(value); }

        // Explicit conversion operators from Series objects to arrays

        public static explicit operator double[](SeriesBase value) { return value.UnderlyingList.AsArray<double>(); }
        public static explicit operator float[](SeriesBase value) { return value.UnderlyingList.AsArray<float>(); }
        public static explicit operator long[](SeriesBase value) { return value.UnderlyingList.AsArray<long>(); }
        public static explicit operator int[](SeriesBase value) { return value.UnderlyingList.AsArray<int>(); }
        public static explicit operator short[](SeriesBase value) { return value.UnderlyingList.AsArray<short>(); }
        public static explicit operator byte[](SeriesBase value) { return value.UnderlyingList.AsArray<byte>(); }
        public static explicit operator sbyte[](SeriesBase value) { return value.UnderlyingList.AsArray<sbyte>(); }
        public static explicit operator decimal[](SeriesBase value) { return value.UnderlyingList.AsArray<decimal>(); }
        public static explicit operator string[](SeriesBase value) { return value.UnderlyingList.AsArray<string>(); }
        public static explicit operator bool[](SeriesBase value) { return value.UnderlyingList.AsArray<bool>(); }
        public static explicit operator DateTime[](SeriesBase value) { return value.UnderlyingList.AsArray<DateTime>(); }
        public static explicit operator DateTimeOffset[](SeriesBase value) { return value.UnderlyingList.AsArray<DateTimeOffset>(); }
        public static explicit operator object[](SeriesBase value) { return value.UnderlyingList.AsArray<object>(); }

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

        public SeriesBase FillIf(ScriptBlock scriptBlock, object value)
        {
            return new Series(GenericIListExtensions.FillIfScriptBlock((dynamic)UnderlyingList, scriptBlock, value));
        }

        public SeriesBase FillIf<T>(Func<T, int, bool> func, T value)
        {
            return new Series(((IList<T>)UnderlyingList).FillIf(func, value));
        }

        public SeriesBase FillIf(string funcString, object value)
        {
            var dataType = DataType;
            return new Series((IList)InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), typeof(bool) },
                "FillIf", new Type[] { dataType }, true,
                new object[] { UnderlyingList, null, value }));
        }

        public void FillIfFill(ScriptBlock scriptBlock, object value)
        {
            GenericIListExtensions.FillIfFillScriptBlock((dynamic)UnderlyingList, scriptBlock, value);
        }

        public void FillIfFill<T>(Func<T, int, bool> func, T value)
        {
            ((IList<T>)UnderlyingList).FillIfFill(func, value);
        }

        public void FillIfFill(string funcString, object value)
        {
            var dataType = DataType;
            InvokeFuncString(funcString,
                new Type[] { dataType, typeof(int), typeof(bool) },
                "FillIfFill", new Type[] { dataType }, true,
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

        public bool All(dynamic value)
        {
            return ((dynamic)UnderlyingList).All(value);
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

        public bool Any(dynamic value)
        {
            return ((dynamic)UnderlyingList).Any(value);
        }

        // Comparison operators

        public SeriesBase Eq(dynamic value)
        {
            if (value is SeriesBase s)
                return new Series(GenericIListExtensions.Eq((dynamic)UnderlyingList, (dynamic)s.UnderlyingList));
            return new Series(GenericIListExtensions.Eq((dynamic)UnderlyingList, value));
        }

        public SeriesBase Ne(dynamic value)
        {
            if (value is SeriesBase s)
                return new Series(GenericIListExtensions.Ne((dynamic)UnderlyingList, (dynamic)s.UnderlyingList));
            return new Series(GenericIListExtensions.Ne((dynamic)UnderlyingList, value));
        }

        public SeriesBase Lt(dynamic value)
        {
            if (value is SeriesBase s)
                return new Series(GenericIListExtensions.Lt((dynamic)UnderlyingList, (dynamic)s.UnderlyingList));
            return new Series(GenericIListExtensions.Lt((dynamic)UnderlyingList, value));
        }

        public SeriesBase Le(dynamic value)
        {
            if (value is SeriesBase s)
                return new Series(GenericIListExtensions.Le((dynamic)UnderlyingList, (dynamic)s.UnderlyingList));
            return new Series(GenericIListExtensions.Le((dynamic)UnderlyingList, value));
        }

        public SeriesBase Gt(dynamic value)
        {
            if (value is SeriesBase s)
                return new Series(GenericIListExtensions.Gt((dynamic)UnderlyingList, (dynamic)s.UnderlyingList));
            return new Series(GenericIListExtensions.Gt((dynamic)UnderlyingList, value));
        }

        public SeriesBase Ge(dynamic value)
        {
            if (value is SeriesBase s)
                return new Series(GenericIListExtensions.Ge((dynamic)UnderlyingList, (dynamic)s.UnderlyingList));
            return new Series(GenericIListExtensions.Ge((dynamic)UnderlyingList, value));
        }

        public SeriesBase Between(dynamic left, dynamic right, bool inclusive = true)
        {
            return new Series(GenericIListExtensions.Between((dynamic)UnderlyingList, left, right, inclusive));
        }

        public SeriesBase In(dynamic values)
        {
            if (values is SeriesBase s)
                return new Series(GenericIListExtensions.In((dynamic)UnderlyingList, (dynamic)s.UnderlyingList));
            return new Series(GenericIListExtensions.In((dynamic)UnderlyingList, values));
        }

        public SeriesBase IsNaN()
        {
            return new Series(GenericIListExtensions.IsNaN((dynamic)UnderlyingList));
        }

        // Other methods

        public SeriesBase Filter(bool[] filter, bool copy = false)
        {
            var filtered = FilteredListView.Create(UnderlyingList, filter);
            if (copy)
                return new Series(filtered).Copy();
            return new Series(filtered);
        }

        public SeriesBase Filter(object[] filter, bool copy = false)
        {
            return Filter(Utils.StripOffPSObjects<bool>(filter).ToArray(), copy);
        }

        public SeriesBase Filter(SeriesBase filter, bool copy = false)
        {
            return Filter(filter.UnderlyingList.ToArray<bool>(), copy);
        }

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

        public static SeriesBase MapTyped<T, U>(IList<T> list, IDictionary map, U fallback)
        {
            if (!(map is IDictionary<T, U> typedMap))
            {
                typedMap = new Dictionary<T, U>();
                foreach (DictionaryEntry entry in map)
                {
                    var key = SmartConverter.ConvertTo<T>(entry.Key);
                    var value = SmartConverter.ConvertTo<U>(entry.Value);
                    typedMap.Add(key, value);
                }
            }

            return new Series(list.Map(typedMap, fallback));
        }

        public SeriesBase Map(IDictionary map)
        {
            return MapTyped((dynamic)UnderlyingList, map);
        }

        public SeriesBase Map(IDictionary map, object fallback)
        {
            return MapTyped((dynamic)UnderlyingList, map, (dynamic)fallback);
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

        public SeriesBase Slice(int start, int count = -1)
        {
            return new Series(SlicedListView.Create(UnderlyingList, start, count, false));
        }

        // Transformers

        public DataMap OneHotEncoding(OneHotType oneHotType = OneHotType.OneHot, IList mapping = null, string columnNameFormat = "{0}")
        {
            var trans = new OneHotTransformer<double>(oneHotType, columnNameFormat);

            if (mapping != null)
            {
                trans.Fit(mapping);
                return trans.TransformToDataMap(this);
            }

            return trans.FitTransformToDataMap(this);
        }

        public SeriesBase LabelEncoding()
        {
            var trans = new LabelEncodingTransformer<double>();
            return trans.FitTransform(this);
        }
    }
}
