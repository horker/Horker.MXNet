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

        // Apply

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

        public static IList<U> ApplyFuncString<T, U>(this IList<T> self, string funcString)
        {
            var result = new List<U>(self.Count);

            var func = FunctionCompiler.Compile(funcString, new Type[] { typeof(T), typeof(int), typeof(U) }, true);

            var m = typeof(IListExtensions).GetMethod("Apply", BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { typeof(T), typeof(U) });
            return (IList<U>)gm.Invoke(null, new object[] { self, func });
        }

        public static void ApplyFillFuncString<T>(this IList<T> self, string funcString)
        {
            var func = FunctionCompiler.Compile(funcString, new Type[] { typeof(T), typeof(int), typeof(T) }, true);

            var m = typeof(IListExtensions).GetMethod("ApplyFill", BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { typeof(T) });
            gm.Invoke(null, new object[] { self, func });
        }

        public static void ForEachFuncString<T>(this IList<T> self, string funcString)
        {
            var func = FunctionCompiler.Compile(funcString, new Type[] { typeof(T), typeof(int) }, false);

            var m = typeof(IListExtensions).GetMethod("ForEach", BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { typeof(T) });
            gm.Invoke(null, new object[] { self, func });
        }

        public static U ReduceFuncString<T, U>(this IList<T> self, string funcString, U initialValue)
        {
            var func = FunctionCompiler.Compile(funcString, new Type[] { typeof(T), typeof(int), typeof(U), typeof(U) }, true);

            var m = typeof(IListExtensions).GetMethod("Reduce", BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { typeof(T), typeof(U) });
            return (U)gm.Invoke(null, new object[] { self, func, initialValue });
        }

        public static int CountIfFuncString<T>(this IList<T> self, string funcString)
        {
            var func = FunctionCompiler.Compile(funcString, new Type[] { typeof(T), typeof(int), typeof(bool) }, true);

            var m = typeof(IListExtensions).GetMethod("CountIf", BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { typeof(T) });
            return (int)gm.Invoke(null, new object[] { self, func });
        }

        public static IList<T> RemoveIfFuncString<T>(this IList<T> self, string funcString)
        {
            var func = FunctionCompiler.Compile(funcString, new Type[] { typeof(T), typeof(int), typeof(bool) }, true);

            var m = typeof(IListExtensions).GetMethod("RemoveIf", BindingFlags.Public | BindingFlags.Static);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(new Type[] { typeof(T) });
            return (IList<T>)gm.Invoke(null, new object[] { self, func });
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
    }
}
