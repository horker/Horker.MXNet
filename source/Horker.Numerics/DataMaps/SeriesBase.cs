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

        public IList<T> ToList<T>()
        {
            return IListExtensions.ToList<T>(UnderlyingList);
        }

        public T[] AsArray<T>()
        {
            return IListExtensions.AsArray<T>(UnderlyingList);
        }

        public IList<T> AsList<T>()
        {
            return IListExtensions.AsList<T>(UnderlyingList);
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
            var comparer = new Comparer(CultureInfo.CurrentCulture);

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
