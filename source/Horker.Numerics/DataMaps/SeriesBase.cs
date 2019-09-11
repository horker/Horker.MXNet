using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class SeriesBase : IList
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

        // Manipulators

        public void Sort()
        {
            var l = UnderlyingList;

            if (l is Array a)
            {
                Array.Sort(a);
                return;
            }

            var t = l.GetType();
            if (t.IsGenericType)
            {
                if (l is List<double> ld) { ld.Sort(); return; }
                if (l is List<float> lf) { lf.Sort(); return; }
                if (l is List<long> ll) { ll.Sort(); return; }
                if (l is List<int> li) { li.Sort(); return; }
                if (l is List<short> ls) { ls.Sort(); return; }
                if (l is List<byte> lb) { lb.Sort(); return; }
                if (l is List<sbyte> lsb) { lsb.Sort(); return; }
                if (l is List<bool> lbo) { lbo.Sort(); return; }
                if (l is List<string> lstr) { lstr.Sort(); return; }
                if (l is List<DateTime> ldt) { ldt.Sort(); return; }
                if (l is List<DateTimeOffset> ldto) { ldto.Sort(); return; }
            }

            var result = new List<object>(l.Count);
            foreach (var e in l)
                result.Add(e);
            result.Sort();

            UnderlyingList = result;
        }
    }
}
