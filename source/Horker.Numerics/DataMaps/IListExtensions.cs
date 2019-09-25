using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Horker.Numerics.DataMaps.Extensions
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

            ArgumentException cause = null;
            foreach (var t in possibleTypes)
            {
                if (t == type)
                    return value;

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

        // IList versions of GenericIListExtensions

        public static IList Sort(this IList self)
        {
            var result = new List<object>(self.ToArray<object>());
            result.Sort();
            return result;
        }

        public static void SortFill(this IList self)
        {
            if (self is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = self.GetType();
            if (t.GetGenericTypeDefinition() == typeof(List<>))
            {
                var l = self as List<MetaNum>;
                l.Sort();
                return;
            }

            var m = t.GetMethod("Sort", new Type[0]);
            if (m == null)
                throw new InvalidOperationException("This object does not support inplace Sort() operation");

            m.Invoke(self, new object[0]);
        }

        public static int CountNaN(this IList self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (value == null)
                    ++count;
            }

            return count;
        }

        public static IList GetUnique(this IList self)
        {
            var unique = new HashSet<object>();
            foreach (var value in self)
                unique.Add(value);

            return unique.ToList();
        }

        public static int CountUnique(this IList self)
        {
            return GetUnique(self).Count;
        }

        public static Summary Describe(this IList self)
        {
            var summary = new Summary()
            {
                Count = self.Count,
                NaN = CountNaN(self),
                Unique = CountUnique(self)
            };

            return summary;
        }

        public static IList RemoveNaN(this IList self)
        {
            var result = new List<object>(self.Count);
            foreach (var value in self)
                if (value != null)
                    result.Add(value);

            return result;
        }
    }
}
