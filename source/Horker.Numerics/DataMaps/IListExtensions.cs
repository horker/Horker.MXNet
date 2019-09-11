using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Horker.Numerics.DataMaps
{
    public static class IListExtensions
    {
        public static Type GetDataType(this IList value)
        {
            if (value is Array)
                return value.GetType().GetElementType();

            if (value is SeriesBase s)
                value = s.UnderlyingList;

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

        public static IList Convert(this IList value, Type[] possibleTypes = null, bool raiseError = false)
        {
            possibleTypes = possibleTypes ?? DataMap.ConversionTypes;

            var type = GetDataType(value);
            foreach (var t in possibleTypes)
            {
                if (t == type)
                    return value;
            }

            ArgumentException cause = null;
            foreach (var t in possibleTypes)
            {
                try
                {
                    return SmartConverter.ConvertTo(t, value);
                }
                catch (ArgumentException ex)
                {
                    cause = ex;
                }
            }

            if (raiseError)
                throw new ArgumentException("Failed to convert to any possible types", cause);

            return value;
        }
    }
}
