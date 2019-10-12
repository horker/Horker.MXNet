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
                result[i] = (T)value[i];

            return result;
        }

        public static IList<T> ToList<T>(this IList value)
        {
            if (value is IList<T> l)
                return new List<T>(l);

            var result = new List<T>(value.Count);
            for (var i = 0; i < value.Count; ++i)
                result.Add((T)value[i]);

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
                result[i] = (T)value[i];

            return result;
        }

        public static IList<T> AsList<T>(this IList value)
        {
            if (value is IList<T> l)
                return l;

            var result = new List<T>(value.Count);
            for (var i = 0; i < value.Count; ++i)
                result.Add((T)value[i]);

            return result;
        }

        public static List<T> Convert<T>(this IList value)
        {
            var result = new List<T>(value.Count);
            for (var i = 0; i < value.Count; ++i)
                result.Add(SmartConverter.ConvertTo<T>(value[i]));

            return result;
        }

        public static IList Convert(this IList value, Type type)
        {
            return SmartConverter.ConvertTo(type, value);
        }

        public static IList CastDownToFirstElementType(this IList self)
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

        public static IList Convert(this IList self, Type[] possibleTypes = null, bool raiseError = false)
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

            return CastDownToFirstElementType(self);
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

        public static void Fill<T>(this IList<T> self, T value)
        {
            for (int i = 0; i < self.Count; ++i)
                self[i] = value;
        }

        public static void FillIf<T>(this IList<T> self, Func<T, int, bool> func, T value)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (func.Invoke(self[i], i))
                    self[i] = value;
            }
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

        public static bool Any<T>(this IList<T> self, Func<T, int, bool> func)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (func.Invoke(self[i], i))
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

        private static List<bool> CompareIList(this IList self, IList other, Func<int, bool> cond)
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

        private static List<bool> CompareScalar(this IList self, object value, Func<int, bool> cond)
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

        public static List<bool> Eq(this IList self, IList other)
        {
            return CompareIList(self, other, x => x == 0);
        }

        public static List<bool> Eq(this IList self, object value)
        {
            return CompareScalar(self, value, x => x == 0);
        }

        public static List<bool> Ne(this IList self, IList other)
        {
            return CompareIList(self, other, x => x != 0);
        }

        public static List<bool> Ne(this IList self, object value)
        {
            return CompareScalar(self, value, x => x != 0);
        }

        public static List<bool> Lt(this IList self, IList other)
        {
            return CompareIList(self, other, x => x < 0);
        }

        public static List<bool> Lt(this IList self, object value)
        {
            return CompareScalar(self, value, x => x < 0);
        }

        public static List<bool> Le(this IList self, IList other)
        {
            return CompareIList(self, other, x => x <= 0);
        }

        public static List<bool> Le(this IList self, object value)
        {
            return CompareScalar(self, value, x => x <= 0);
        }

        public static List<bool> Gt(this IList self, IList other)
        {
            return CompareIList(self, other, x => x > 0);
        }

        public static List<bool> Gt(this IList self, object value)
        {
            return CompareScalar(self, value, x => x > 0);
        }

        public static List<bool> Ge(this IList self, IList other)
        {
            return CompareIList(self, other, x => x >= 0);
        }

        public static List<bool> Ge(this IList self, object value)
        {
            return CompareScalar(self, value, x => x >= 0);
        }

        public static List<bool> Between(this IList self, object left, object right, bool inclusive = true)
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
            return self.Unique().Count;
        }

        public static ValueBin[] CountValues<T>(this IList<T> self)
        {
            var counter = new Dictionary<T, int>();

            foreach (var value in self)
            {
                if (counter.TryGetValue(value, out var count))
                    counter[value] = count + 1;
                else
                    counter[value] = 1;
            }

            var bins = new ValueBin[counter.Count];
            var i = 0;
            foreach (var entry in counter)
            {
                var bin = new ValueBin()
                {
                    Index = i,
                    Value = entry.Key,
                    Count = entry.Value,
                    Ratio = (double)entry.Value / self.Count
                };
                bins[i] = bin;
                ++i;
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

        public static List<T> RemoveNaN<T>(this IList<T> self)
        {
            var result = new List<T>(self.Count);
            foreach (var value in self)
                if (!TypeTrait<T>.IsNaN(value))
                    result.Add(value);

            return result;
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

        public static List<T> SortedCopy<T>(this IList<T> self)
        {
            var result = new List<T>(self);
            result.Sort();
            return result;
        }

        public static List<T> Map<T>(this IList<T> self, IDictionary map)
        {
            var result = new List<T>();
            foreach (var value in self)
            {
                if (map.Contains(value))
                    result.Add((T)map[value]);
                else
                    result.Add(value);
            }

            return result;
        }

        public static void MapFill<T>(this IList<T> self, IDictionary map)
        {
            for (var i = 0; i < self.Count; ++i)
            {
                if (map.Contains(self[i]))
                    self[i] = (T)map[self[i]];
            }
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

        public static List<T> Unique<T>(this IList<T> self)
        {
            return new HashSet<T>(self).ToList();
        }
    }
}
