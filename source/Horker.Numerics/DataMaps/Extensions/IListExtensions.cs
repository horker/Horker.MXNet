﻿using Horker.Numerics.DataMaps.Utilities;
using Horker.Numerics.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace Horker.Numerics.DataMaps.Extensions
{
    public static partial class GenericIListExtensions
    {
        // Types and type conversions

        public static Type GetDataType(this IList value)
        {
            while (value is SeriesBase s)
                value = s.UnderlyingList;

            var t = value.GetType();

            var e = t.GetElementType();
            if (e != null)
                return e;

            if (!t.IsGenericType)
                return typeof(object);

            if (t.GetGenericTypeDefinition() == typeof(List<>))
                return t.GetGenericArguments()[0];

            foreach (var itf in t.GetTypeInfo().ImplementedInterfaces)
            {
                if (itf.IsGenericType && itf.GetGenericTypeDefinition() == typeof(IList<>))
                    return itf.GetGenericArguments()[0];
            }

            return typeof(object);
        }

        public static T[] ToArray<T>(this IList value)
        {
            if (GetDataType(value) == typeof(T))
                ((IList<T>)value).ToArray();

            var result = new T[value.Count];
            for (var i = 0; i < result.Length; ++i)
                result[i] = SmartConverter.ConvertTo<T>(value[i]);

            return result;
        }

        public static List<T> ToList<T>(this IList value)
        {
            if (value is IList<T> l)
                return new List<T>(l);

            var result = new List<T>(value.Count);
            for (var i = 0; i < value.Count; ++i)
                result.Add(SmartConverter.ConvertTo<T>(value[i]));

            return result;
        }

        public static T[] AsArray<T>(this IList value)
        {
            var t = GetDataType(value);
            if (value is Array && t == typeof(T))
                return (T[])value;

            if (t == typeof(T))
                ((IList<T>)value).ToArray();

            var result = new T[value.Count];
            for (var i = 0; i < result.Length; ++i)
                result[i] = SmartConverter.ConvertTo<T>(value[i]);

            return result;
        }

        public static IList<T> AsList<T>(this IList value)
        {
            if (value is IList<T> l)
                return l;

            var result = new List<T>(value.Count);
            for (var i = 0; i < value.Count; ++i)
                result.Add(SmartConverter.ConvertTo<T>(value[i]));

            return result;
        }

        public static IList CastDown(this IList self)
        {
            Type firstType = null;
            foreach (var value in self)
            {
                if (value != null)
                {
                    firstType = value.GetType();
                    break;
                }
            }

            if (firstType != null)
            {
                try
                {
                    if (self is Array)
                    {
                        var m = typeof(GenericIListExtensions).GetMethod("AsArray").MakeGenericMethod(new Type[] { firstType });
                        return (IList)m.Invoke(null, new object[] { self });
                    }
                    else
                    {
                        var m = typeof(GenericIListExtensions).GetMethod("AsList").MakeGenericMethod(new Type[] { firstType });
                        return (IList)m.Invoke(null, new object[] { self });
                    }
                }
                catch (InvalidCastException)
                {
                    // fall through
                }
            }

            var result = new List<object>(self.Count);
            foreach (var value in self)
                result.Add(value);
            return result;
        }

        public static IList TryConversion(this IList self, Type[] possibleTypes = null, bool raiseError = false)
        {
            possibleTypes = possibleTypes ?? DataMap.ConversionTypes;

            var type = GetDataType(self);

            ArgumentException cause = null;
            foreach (var t in possibleTypes)
            {
                if (t == type)
                    return self;

                try
                {
                    return SmartConverter.ConvertTo(t, self);
                }
                catch (ArgumentException ex)
                {
                    cause = ex;
                }
            }

            // If any possible types are not adequate, try to cast down the type of the first non-null element.

            return CastDown(self);
        }

        public static IList<bool> IsNaN<T>(this IList<T> self)
        {
            var result = new List<bool>(self.Count);

            foreach (var item in self)
                result.Add(TypeTrait<T>.IsNaN(item));

            return result;
        }

        // Element-wise operations of string

        public static IList<string> ElementAdd(this IList<string> self, IList<string> other)
        {
            var result = new List<string>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add(self[i] + other[i]);
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<string> ElementAdd(this IList<string> self, string value)
        {
            var result = new List<string>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add(self[i] + value);

            return result;
        }

        public static IList<string> ElementAddR(this IList<string> self, string value)
        {
            var result = new List<string>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add(value + self[i]);

            return result;
        }

        // Element-wise operations of boolean

        public static IList<bool> And(this IList<bool> self, IList<bool> other)
        {
            var result = new List<bool>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add(self[i] && other[i]);
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<bool> Or(this IList<bool> self, IList<bool> other)
        {
            var result = new List<bool>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add(self[i] || other[i]);
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<bool> Xor(this IList<bool> self, IList<bool> other)
        {
            var result = new List<bool>(Math.Max(self.Count, other.Count));

            var i = 0;
            for (; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(self[i]);
                else
                    result.Add(self[i] ^ other[i]);
            }
            for (; i < other.Count; ++i)
                result.Add(other[i]);

            return result;
        }

        public static IList<bool> Not(this IList<bool> self)
        {
            var result = new List<bool>(self.Count);

            for (var i = 0; i < self.Count; ++i)
                result.Add(!self[i]);

            return result;
        }

        // Apply and friends

        public static List<U> Apply<T, U>(this IList<T> self, Func<T, int, U> func)
        {
            List<U> result = result = new List<U>(self.Count);

            for (var i = 0; i < self.Count; ++i)
            {
                var value = func.Invoke(self[i], i);
                result.Add(value);
            }
            return result;
        }

        public static void ApplyFill<T>(this IList<T> self, Func<T, int, T> func)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                var value = func.Invoke(self[i], i);
                self[i] = value;
            }
        }

        public static void ForEach<T>(this IList<T> self, Action<T, int> func)
        {
            for (int i = 0; i < self.Count; ++i)
                func.Invoke(self[i], i);
        }

        public static U Reduce<T, U>(this IList<T> self, Func<T, int, U, U> func, U initialValue)
        {
            U result = initialValue;
            for (int i = 0; i < self.Count; ++i)
                result = func.Invoke(self[i], i, result);
            return result;
        }

        public static int CountIf<T>(this IList<T> self, Func<T, int, bool> func)
        {
            int count = 0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (func.Invoke(self[i], i))
                    ++count;
            }
            return count;
        }

        public static List<T> Fill<T>(this IList<T> self, T value)
        {
            var result = new List<T>(self.Count);
            for (int i = 0; i < self.Count; ++i)
                result.Add(value);
            return result;
        }

        public static void FillFill<T>(this IList<T> self, T value)
        {
            for (int i = 0; i < self.Count; ++i)
                self[i] = value;
        }

        public static List<T> FillIf<T>(this IList<T> self, Func<T, int, bool> func, T value)
        {
            var result = new List<T>(self.Count);
            for (int i = 0; i < self.Count; ++i)
            {
                if (func.Invoke(self[i], i))
                    result.Add(value);
                else
                    result.Add(self[i]);
            }
            return result;
        }

        public static void FillIfFill<T>(this IList<T> self, Func<T, int, bool> func, T value)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (func.Invoke(self[i], i))
                    self[i] = value;
            }
        }

        public static List<T> Filter<T>(this IList<T> self, Func<T, int, bool> func)
        {
            var result = new List<T>();
            for (int i = 0; i < self.Count; ++i)
            {
                if (func.Invoke(self[i], i))
                    result.Add(self[i]);
            }
            return result;
        }

        public static List<T> RemoveIf<T>(this IList<T> self, Func<T, int, bool> func)
        {
            var result = new List<T>();
            for (int i = 0; i < self.Count; ++i)
            {
                if (!func.Invoke(self[i], i))
                    result.Add(self[i]);
            }
            return result;
        }

        public static List<U> RollingApply<T, U>(this IList<T> self, Func<T[], int, U> func, int window)
        {
            var result = new List<U>();
            T[] slice;

            var i = 0;
            for (;  i < window - 1; ++i)
            {
                slice = new T[i + 1];
                for (var j = 0; j <= i; ++j)
                    slice[j] = self[j];

                var value = func.Invoke(slice, i);
                result.Add(value);
            }

            slice = new T[window];
            for (; i < self.Count; ++i)
            {
                for (var j = 0; j < window; ++j)
                    slice[j] = self[i - window + 1 + j];

                var value = func.Invoke(slice, i);
                result.Add(value);
            }

            return result;
        }

        public static void RollingApplyFill<T>(this IList<T> self, Func<T[], int, T> func, int window)
        {
            T[] slice;

            var i = 0;
            for (;  i < window - 1; ++i)
            {
                slice = new T[i + 1];
                for (var j = 0; j <= i; ++j)
                    slice[j] = self[j];

                var value = func.Invoke(slice, i);
                self[i] = value;
            }

            slice = new T[window];
            for (; i < self.Count; ++i)
            {
                for (var j = 0; j < window; ++j)
                    slice[j] = self[i - window + 1 + j];

                var value = func.Invoke(slice, i);
                self[i] = value;
            }
        }

        public static bool All<T>(this IList<T> self, Func<T, int, bool> func)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (!func.Invoke(self[i], i))
                    return false;
            }
            return true;
        }

        public static bool All<T>(this IList<T> self, T value)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (!self[i].Equals(value))
                    return false;
            }

            return true;
        }

        public static bool Any<T>(this IList<T> self, Func<T, int, bool> func)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (func.Invoke(self[i], i))
                    return true;
            }
            return false;
        }

        public static bool Any<T>(this IList<T> self, T value)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (self[i].Equals(value))
                    return true;
            }

            return false;
        }

        public static IList<U> ApplyScriptBlock<T, U>(this IList<T> self, ScriptBlock scriptBlock)
        {
            var result = new List<U>(self.Count);

            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                var rawValue = scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject;
                var value = (U)(object)rawValue;
                result.Add(value);
            }

            return result;
        }

        public static void ApplyFillScriptBlock<T>(this IList<T> self, ScriptBlock scriptBlock)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                var rawValue = scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject;
                var value = (T)(object)rawValue;
                self[i] = value;
            }
        }

        public static void ForEachScriptBlock<T>(this IList<T> self, ScriptBlock scriptBlock)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                scriptBlock.InvokeWithContext(null, parameters, null);
            }
        }

        public static object ReduceScriptBlock<T, U>(this IList<T> self, ScriptBlock scriptBlock, object initialValue)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));
            parameters.Add(new PSVariable("result"));

            object result = initialValue;
            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                parameters[2].Value = result;
                result = scriptBlock.InvokeWithContext(null, parameters, null)[0];
            }

            return (result as PSObject).BaseObject;
        }

        public static object CountIfScriptBlock<T>(this IList<T> self, ScriptBlock scriptBlock)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            var count = 0;
            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                if ((bool)scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject)
                    ++count;
            }

            return count;
        }

        public static void FillIfScriptBlock<T>(this IList<T> self, ScriptBlock scriptBlock, T value)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                if ((bool)scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject)
                    self[i] = value;
            }
        }

        public static List<T> FillIfFillScriptBlock<T>(this IList<T> self, ScriptBlock scriptBlock, T value)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            var result = new List<T>(self.Count);

            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                if ((bool)scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject)
                    result.Add(value);
                else
                    result.Add(self[i]);
            }

            return result;
        }

        public static List<T> RemoveIfScriptBlock<T>(this IList<T> self, ScriptBlock scriptBlock)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            var result = new List<T>();
            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                if (!(bool)scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject)
                    result.Add(self[i]);
            }

            return result;
        }

        public static List<U> RollingApplyScriptBlock<T, U>(this IList<T> self, ScriptBlock scriptBlock, int window)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("values"));
            parameters.Add(new PSVariable("index"));

            var result = new List<U>();
            T[] slice;

            var i = 0;
            for (;  i < window - 1; ++i)
            {
                slice = new T[i + 1];
                for (var j = 0; j <= i; ++j)
                    slice[j] = self[j];

                parameters[0].Value = slice;
                parameters[1].Value = i;
                var value = scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject;
                result.Add((U)value);
            }

            slice = new T[window];
            for (; i < self.Count; ++i)
            {
                for (var j = 0; j < window; ++j)
                    slice[j] = self[i - window + 1 + j];

                parameters[0].Value = slice;
                parameters[1].Value = i;
                var value = scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject;
                result.Add((U)value);
            }

            return result;
        }

        public static void RollingApplyFill<T>(this IList<T> self, ScriptBlock scriptBlock, int window, DataMap dataMap)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("values"));
            parameters.Add(new PSVariable("index"));

            T[] slice;

            var i = 0;
            for (;  i < window - 1; ++i)
            {
                slice = new T[i + 1];
                for (var j = 0; j <= i; ++j)
                    slice[j] = self[j];

                parameters[0].Value = slice;
                parameters[1].Value = i;
                var value = scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject;
                self[i] = (T)value;
            }

            slice = new T[window];
            for (; i < self.Count; ++i)
            {
                for (var j = 0; j < window; ++j)
                    slice[j] = self[i - window + 1 + j];

                parameters[0].Value = slice;
                parameters[1].Value = i;
                var value = scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject;
                self[i] = (T)value;
            }
        }

        public static bool AllScriptBlock<T>(this IList<T> self, ScriptBlock scriptBlock)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                if (!(bool)scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject)
                    return false;
            }

            return true;
        }

        public static bool AnyScriptBlock<T>(this IList<T> self, ScriptBlock scriptBlock)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            for (var i = 0; i < self.Count; ++i)
            {
                parameters[0].Value = self[i];
                parameters[1].Value = i;
                if ((bool)scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject)
                    return true;
            }

            return false;
        }

        // Comparison operators

        private static List<bool> CompareIList<T>(this IList<T> self, IList<T> other, Func<int, bool> cond)
        {
            var result = new List<bool>(self.Count);
            var comparer = Comparer.Default;

            for (var i = 0; i < self.Count; ++i)
            {
                if (i > other.Count - 1)
                    result.Add(false);
                else
                {
                    var c = cond.Invoke(comparer.Compare(self[i], other[i]));
                    result.Add(c);
                }
            }

            return result;
        }

        private static List<bool> CompareScalar<T>(this IList<T> self, T value, Func<int, bool> cond)
        {
            var result = new List<bool>(self.Count);
            var comparer = Comparer.Default;

            for (var i = 0; i < self.Count; ++i)
            {
                var c = cond.Invoke(comparer.Compare(self[i], value));
                result.Add(c);
            }

            return result;
        }

        public static List<bool> Eq<T>(this IList<T> self, IList<T> other)
        {
            return CompareIList(self, other, x => x == 0);
        }

        public static List<bool> Eq<T>(this IList<T> self, T value)
        {
            return CompareScalar(self, value, x => x == 0);
        }

        public static List<bool> Ne<T>(this IList<T> self, IList<T> other)
        {
            return CompareIList(self, other, x => x != 0);
        }

        public static List<bool> Ne<T>(this IList<T> self, T value)
        {
            return CompareScalar(self, value, x => x != 0);
        }

        public static List<bool> Lt<T>(this IList<T> self, IList<T> other)
        {
            return CompareIList(self, other, x => x < 0);
        }

        public static List<bool> Lt<T>(this IList<T> self, T value)
        {
            return CompareScalar(self, value, x => x < 0);
        }

        public static List<bool> Le<T>(this IList<T> self, IList<T> other)
        {
            return CompareIList(self, other, x => x <= 0);
        }

        public static List<bool> Le<T>(this IList<T> self, T value)
        {
            return CompareScalar(self, value, x => x <= 0);
        }

        public static List<bool> Gt<T>(this IList<T> self, IList<T> other)
        {
            return CompareIList(self, other, x => x > 0);
        }

        public static List<bool> Gt<T>(this IList<T> self, T value)
        {
            return CompareScalar(self, value, x => x > 0);
        }

        public static List<bool> Ge<T>(this IList<T> self, IList<T> other)
        {
            return CompareIList(self, other, x => x >= 0);
        }

        public static List<bool> Ge<T>(this IList<T> self, T value)
        {
            return CompareScalar(self, value, x => x >= 0);
        }

        public static List<bool> Between<T>(this IList<T> self, T left, T right, bool inclusive = true)
        {
            var result = new List<bool>(self.Count);
            var comparer = Comparer.Default;

            if (inclusive)
            {
                for (var i = 0; i < self.Count; ++i)
                {
                    var r = comparer.Compare(left, self[i]) <= 0;
                    var l = comparer.Compare(self[i], right) <= 0;
                    result.Add(r && l);
                }
            }
            else
            {
                for (var i = 0; i < self.Count; ++i)
                {
                    var r = comparer.Compare(left, self[i]) < 0;
                    var l = comparer.Compare(self[i], right) < 0;
                    result.Add(r && l);
                }
            }

            return result;
        }

        public static List<bool> In<T>(this IList<T> self, params T[] values)
        {
            var result = new List<bool>(self.Count);
            var comparer = Comparer.Default;

            for (var i = 0; i < self.Count; ++i)
            {
                var c = false;
                for (var j = 0; j < values.Length; ++j)
                {
                    if (comparer.Compare(self[i], values[j]) == 0)
                    {
                        c = true;
                        break;
                    }
                }

                result.Add(c);
            }

            return result;
        }

        // Other operations

        public static IList<T> Copy<T>(this IList<T> self)
        {
            return new List<T>(self);
        }

        public static int CountNaN<T>(this IList<T> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (TypeTrait<T>.IsNaN(value))
                    ++count;
            }

            return count;
        }

        public static int CountUnique<T>(this IList<T> self)
        {
            return self.Unique(false).Count;
        }

        public static ValueBin[] CountValues<T>(this IList<T> self, bool sort = true)
        {
            var counter = new Dictionary<T, int>();

            foreach (var value in self)
            {
                if (counter.TryGetValue(value, out var count))
                    counter[value] = count + 1;
                else
                    counter[value] = 1;
            }

            var values = counter.Keys.ToArray();
            if (sort)
                Array.Sort(values);

            var bins = new ValueBin[values.Length];
            for (var i = 0; i < values.Length; ++i)
            {
                var count = counter[values[i]];
                var bin = new ValueBin()
                {
                    Index = i,
                    Value = values[i],
                    Count = count,
                    Ratio = (double)count / self.Count
                };

                bins[i] = bin;
            }

            return bins;
        }

        public static IList<T> CreateLike<T>(this IList<T> self)
            where T: new()
        {
            var result = new List<T>(self.Count);
            for (var i = 0; i < self.Count; ++i)
                result.Add(new T());

            return result;
        }

        public static IList<string> CreateLike(this IList<string> self)
        {
            var result = new List<string>(self.Count);
            for (var i = 0; i < self.Count; ++i)
                result.Add(string.Empty);

            return result;
        }

        public static Summary Describe<T>(this IList<T> self)
        {
            var summary = new Summary();
            summary.Count = self.Count;
            summary.NaN = CountNaN(self);
            summary.Unique = CountUnique(self);
            return summary;
        }

        public static List<T> FillNaN<T>(this IList<T> self, T fillValue)
        {
            var result = new List<T>(self.Count);
            foreach (var value in self)
            {
                if (TypeTrait<T>.IsNaN(value))
                    result.Add(fillValue);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void FillNaNFill<T>(this IList<T> self, T fillValue)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (TypeTrait<T>.IsNaN(self[i]))
                    self[i] = fillValue;
            }
        }

        public static List<T> Map<T>(this IList<T> self, IDictionary<T, T> map)
        {
            var result = new List<T>();
            for (var i = 0; i < self.Count; ++i)
            {
                if (map.TryGetValue(self[i], out var value))
                    result.Add(value);
                else
                    result.Add(self[i]);
            }

            return result;
        }

        public static List<U> Map<T, U>(this IList<T> self, IDictionary<T, U> map, U fallback)
        {
            var result = new List<U>();
            for (var i = 0; i < self.Count; ++i)
            {
                if (map.TryGetValue(self[i], out var value))
                    result.Add(value);
                else
                    result.Add(fallback);
            }

            return result;
        }

        public static void MapFill<T>(this IList<T> self, IDictionary<T, T> map)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (map.TryGetValue(self[i], out var value))
                    self[i] = value;
            }
        }

        public static List<T> RemoveNaN<T>(this IList<T> self)
        {
            var result = new List<T>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<T>.IsNaN(value))
                    result.Add(value);

            return result;
        }

        public static List<T> Sample<T>(this IList<T> self, bool replacement = false, int sampleSize = -1, IRandom random = null)
        {
            if (sampleSize == -1)
                sampleSize = self.Count;

            if (replacement)
            {
                random ??= RandomInstance.Get();
                var result = new List<T>(sampleSize);
                for (var i = 0; i < sampleSize; ++i)
                {
                    var j = random.Next(self.Count);
                    result.Add(self[j]);
                }

                return result;
            }
            else
            {
                if (sampleSize > self.Count)
                    throw new ArgumentOutOfRangeException("sampleSize", "Sample size must not be larger than the size of the population without replacement.");

                var result = Shuffle(self, random);
                if (sampleSize == self.Count)
                    return result;
                return result.Take(sampleSize).ToList();
            }
        }

        public static List<T> Shuffle<T>(this IList<T> self, IRandom random = null)
        {
            // ref: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle 

            var result = new List<T>(self.Count);
            random ??= RandomInstance.Get();
            for (var i = 0; i < self.Count; ++i)
            {
                var j = random.Next(i + 1);
                if (j == i)
                    result.Add(self[i]);
                else
                {
                    result.Add(result[j]);
                    result[j] = self[i];
                }
            }

            return result;
        }

        public static void ShuffleFill<T>(this IList<T> self, IRandom random = null)
        {
            random ??= RandomInstance.Get();
            for (var i = 0; i < self.Count; ++i)
            {
                var j = random.Next(i + 1);
                var temp = self[i];
                self[i] = self[j];
                self[j] = temp;
            }
        }

        public static List<T> SortedCopy<T>(this IList<T> self)
        {
            var result = new List<T>(self);
            result.Sort();
            return result;
        }

        public static T[] SortBy<T, S>(this IList<T> self, IList<S> by)
        {
            var result = self.ToArray();
            var s = by.ToArray();
            Array.Sort(s, result);

            return result;
        }

        public static void SortFill<T>(this IList<T> self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            try
            {
                ((dynamic)self).Sort();
            }
            catch (Exception)
            {
                throw new InvalidOperationException("This object does not support inplace Sort() operation");
            }
        }

        public static void SortByFill<T, S>(this IList<T> self, IList<S> by)
        {
            var result = SortBy(self, by);

            for (var i = 0; i < self.Count; ++i)
                self[i] = result[i];
        }

        public static int[] ArgSort<T>(this IList<T> self)
        {
            var interm = new int[self.Count];
            for (var i = 0; i < self.Count; ++i)
                interm[i] = i;

            var s = self.ToArray();
            Array.Sort(s, interm);

            var result = new int[self.Count];

            for (var i = 0; i < self.Count; ++i)
                result[interm[i]] = i;

            return result;
        }

        public static List<T> Unique<T>(this IList<T> self, bool sort = true)
        {
            if (sort)
                return new SortedSet<T>(self).ToList();
            else
                return new HashSet<T>(self).ToList();
        }
    }
}
