using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace Horker.Numerics.DataMaps.Extensions
{
    public static partial class IListExtensions
    {
        public static Type GetDataType(this IList value)
        {
            // Types and type conversions

            if (value is Array)
                return value.GetType().GetElementType();

            if (value is SeriesBase s)
                value = s.UnderlyingList;

            // TODO: check if it implements IList<>.
            var ga = value.GetType().GetGenericArguments();

            if (ga == null || ga.Length == 0)
                return typeof(object);

            return ga[0];
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
                var m = typeof(IListExtensions).GetMethod("AsArray").MakeGenericMethod(new Type[] { firstType });
                try
                {
                    return (IList)m.Invoke(null, new object[] { self });
                }
                catch (InvalidCastException)
                {
                    return self;
                }
            }

            return self;
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

        public static IList<U> Apply<T, U>(this IList<T> self, Func<T, int, U> func)
        {
            List<U> result = result = new List<U>(self.Count);
            int i = 0;
            foreach (var e in self)
            {
                var value = func.Invoke(e, i++);
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

        public static IList<T> RemoveIf<T>(this IList<T> self, Func<T, int, bool> func)
        {
            var result = new List<T>();
            for (int i = 0; i < self.Count; ++i)
            {
                if (!func.Invoke(self[i], i))
                    result.Add(self[i]);
            }
            return result;
        }

        public static IList<U> RollingApply<T, U>(this IList<T> self, Func<T[], int, U> func, int window)
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

        private static object InvokeFuncString(string funcString, Type[] funcTypes, string funcName, Type[] methodGenericTypes, bool hasReturnValue, object[] arguments)
        {
            var func = FunctionCompiler.Compile(funcString, funcTypes, hasReturnValue);
            arguments[1] = func;

            var m = typeof(IListExtensions).GetMethod(funcName, BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);

            var gm = m.MakeGenericMethod(methodGenericTypes);

            return gm.Invoke(null, arguments);
        }

        public static IList<U> ApplyFuncString<T, U>(this IList<T> self, string funcString)
        {
            return (IList<U>)InvokeFuncString(funcString,
                new Type[] { typeof(T), typeof(int), typeof(U) },
                "Apply", new Type[] { typeof(T), typeof(U) }, true,
                new object[] { self, null });
        }

        public static void ApplyFillFuncString<T>(this IList<T> self, string funcString)
        {
            InvokeFuncString(funcString,
                new Type[] { typeof(T), typeof(int), typeof(T) },
                "ApplyFill", new Type[] { typeof(T) }, true,
                new object[] { self, null });
        }

        public static void ForEachFuncString<T>(this IList<T> self, string funcString)
        {
            InvokeFuncString(funcString,
                new Type[] { typeof(T), typeof(int) },
                "ForEach", new Type[] { typeof(T) }, true,
                new object[] { self, null });
        }

        public static U ReduceFuncString<T, U>(this IList<T> self, string funcString, U initialValue)
        {
            return (U)InvokeFuncString(funcString,
                new Type[] { typeof(T), typeof(int), typeof(U), typeof(U) },
                "Reduce", new Type[] { typeof(T), typeof(U) }, true,
                new object[] { self, null, initialValue });
        }

        public static int CountIfFuncString<T>(this IList<T> self, string funcString)
        {
            return (int)InvokeFuncString(funcString,
                new Type[] { typeof(T), typeof(int), typeof(bool) },
                "CountIf", new Type[] { typeof(T) }, true,
                new object[] { self, null });
        }

        public static IList<T> RemoveIfFuncString<T>(this IList<T> self, string funcString)
        {
            return (IList<T>)InvokeFuncString(funcString,
                new Type[] { typeof(T), typeof(int), typeof(bool) },
                "RemoveIf", new Type[] { typeof(T) }, true,
                new object[] { self, null });
        }

        public static IList<U> RollingApplyFuncString<T, U>(this IList<T> self, string funcString, int window)
        {
            return (IList<U>)InvokeFuncString(funcString,
                new Type[] { typeof(T[]), typeof(int), typeof(U) },
                "RollingApply", new Type[] { typeof(T), typeof(U) }, true,
                new object[] { self, null, window });
        }

        public static void RollingApplyFillFuncString<T>(this IList<T> self, string funcString, int window)
        {
            InvokeFuncString(funcString,
                new Type[] { typeof(T[]), typeof(int) },
                "RollingApply", new Type[] { typeof(T) }, true,
                new object[] { self, null, window });
        }

        public static IList<U> ApplyScriptBlock<T, U>(this IList<T> self, ScriptBlock scriptBlock)
        {
            var result = new List<U>(self.Count);

            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            var i = 0;
            foreach (var e in self)
            {
                parameters[0].Value = e;
                parameters[1].Value = i++;
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

            var i = 0;
            foreach (var e in self)
            {
                parameters[0].Value = e;
                parameters[1].Value = i++;
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

            var i = 0;
            foreach (var e in self)
            {
                parameters[0].Value = e;
                parameters[1].Value = i++;
                scriptBlock.InvokeWithContext(null, parameters, null);
            }
        }

        public static object ReduceScriptBlock<T, U>(this IList<T> self, ScriptBlock scriptBlock, object initialValue)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));
            parameters.Add(new PSVariable("result"));

            var i = 0;
            object result = initialValue;
            foreach (var e in self)
            {
                parameters[0].Value = e;
                parameters[1].Value = i++;
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

            var i = 0;
            var count = 0;
            foreach (var e in self)
            {
                parameters[0].Value = e;
                parameters[1].Value = i++;
                if ((bool)scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject)
                    ++count;
            }

            return count;
        }

        public static IList<T> RemoveIfScriptBlock<T>(this IList<T> self, ScriptBlock scriptBlock)
        {
            var parameters = new List<PSVariable>();
            parameters.Add(new PSVariable("value"));
            parameters.Add(new PSVariable("index"));

            var i = 0;
            var result = new List<T>();
            foreach (var e in self)
            {
                parameters[0].Value = e;
                parameters[1].Value = i++;
                if (!(bool)scriptBlock.InvokeWithContext(null, parameters, null)[0].BaseObject)
                    result.Add(e);
            }

            return result;
        }

        public static IList<U> RollingApplyScriptBlock<T, U>(this IList<T> self, ScriptBlock scriptBlock, int window)
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

        public static void RollingApplyFill<T>(this IList<T> self, ScriptBlock scriptBlock, int window)
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

        // Comparison operators

        private static IList<bool> CompareIList(this IList self, IList other, Func<int, bool> cond)
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

        private static IList<bool> CompareScalar(this IList self, object value, Func<int, bool> cond)
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

        public static IList<bool> Eq(this IList self, IList other)
        {
            return CompareIList(self, other, x => x == 0);
        }

        public static IList<bool> Eq(this IList self, object value)
        {
            return CompareScalar(self, value, x => x == 0);
        }

        public static IList<bool> Ne(this IList self, IList other)
        {
            return CompareIList(self, other, x => x != 0);
        }

        public static IList<bool> Ne(this IList self, object value)
        {
            return CompareScalar(self, value, x => x != 0);
        }

        public static IList<bool> Lt(this IList self, IList other)
        {
            return CompareIList(self, other, x => x < 0);
        }

        public static IList<bool> Lt(this IList self, object value)
        {
            return CompareScalar(self, value, x => x < 0);
        }

        public static IList<bool> Le(this IList self, IList other)
        {
            return CompareIList(self, other, x => x <= 0);
        }

        public static IList<bool> Le(this IList self, object value)
        {
            return CompareScalar(self, value, x => x <= 0);
        }

        public static IList<bool> Gt(this IList self, IList other)
        {
            return CompareIList(self, other, x => x > 0);
        }

        public static IList<bool> Gt(this IList self, object value)
        {
            return CompareScalar(self, value, x => x > 0);
        }

        public static IList<bool> Ge(this IList self, IList other)
        {
            return CompareIList(self, other, x => x >= 0);
        }

        public static IList<bool> Ge(this IList self, object value)
        {
            return CompareScalar(self, value, x => x >= 0);
        }

        // Other operations

        public static IList<T> Map<T>(this IList<T> self, IDictionary map)
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

    }
}
