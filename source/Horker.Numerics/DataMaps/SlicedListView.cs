using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps.Extensions;

namespace Horker.Numerics.DataMaps
{
    public class SlicedListView<T> : IList<T>, IList
    {
        private IList<T> _underlying;
        private int _start;
        private int _count;

        public SlicedListView(IList<T> underlying, int start, int count, bool strict = true)
        {
            _underlying = underlying;
            _start = start;
            _count = count;

            if (start < 0)
                throw new ArgumentOutOfRangeException("start");

            if (count < 0)
            {
                if (strict)
                    throw new ArgumentOutOfRangeException("count");
                else
                    _count = Math.Max(underlying.Count - start, 0);
            }

            if (start > underlying.Count - 1)
            {
                if (strict)
                    throw new ArgumentOutOfRangeException("start");
                else
                    _count = 0;
            }
            else
            {
                if (start + count > underlying.Count)
                {
                    if (strict)
                        throw new ArgumentOutOfRangeException("start");
                    else
                        _count = underlying.Count - start;
                }
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || _count <= index)
                    throw new ArgumentOutOfRangeException("index");
                return _underlying[_start + index];
            }

            set
            {
                if (index < 0 || _count <= index)
                    throw new ArgumentOutOfRangeException("index");
                _underlying[_start + index] = value;
            }
        }

        object IList.this[int index] { get => this[index]; set => this[index] = (T)value; }

        public bool IsReadOnly => _underlying.IsReadOnly;

        public int Count => _count;

        public bool IsSynchronized => false;

        public bool IsFixedSize => false;

        public object SyncRoot => null;

        public void Add(T item)
        {
            throw new InvalidOperationException("This list does not support Insert() operation");
        }

        public int Add(object value)
        {
            throw new InvalidOperationException("This list does not support Insert() operation");
        }

        public void Clear()
        {
            _underlying.Clear();
            _start = 0;
            _count = 0;
        }

        public bool Contains(T value)
        {
            var count = _count;
            for (var i = 0; i < count; ++i)
            {
                if (_underlying[_start + i].Equals(value))
                    return true;
            }
            return false;
        }

        public bool Contains(object value)
        {
            return Contains((T)value);
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex < _count)
                throw new ArgumentOutOfRangeException("Destination array too short");

            var count = _count;
            for (var i = 0; i < count; ++i)
                array[arrayIndex + i] = _underlying[_start + i];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SlicedListViewEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            var count = _count;
            for (var i = 0; i < _count; ++i)
            {
                if (_underlying[_start + i].Equals(item))
                    return i;
            }
            return -1;
        }

        public int IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        public void Insert(int index, T item)
        {
            throw new InvalidOperationException("This list does not support Insert() operation");
        }

        public void Insert(int index, object value)
        {
            throw new InvalidOperationException("This list does not support Insert() operation");
        }

        public bool Remove(T item)
        {
            var index = _underlying.IndexOf(item);
            if (index < _start || _start + _count <= index)
                return false;

            _underlying.RemoveAt(_start + index);
            return true;
        }

        public void Remove(object value)
        {
            Remove((T)value);
        }

        public void RemoveAt(int index)
        {
            if (index < _start || _start + _count <= index)
                throw new ArgumentOutOfRangeException("index");

            _underlying.RemoveAt(_start + index);
            --_count;
        }
    }

    public class SlicedListViewEnumerator<T> : IEnumerator<T>
    {
        private SlicedListView<T> _list;
        private int _index;

        public SlicedListViewEnumerator(SlicedListView<T> list)
        {
            _list = list;
            _index = -1;
        }

        public T Current => _list[_index];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            ++_index;
            return _index < _list.Count;
        }

        public void Reset()
        {
            _index = 0;
        }
    }

    public static class SlicedListView
    {
        public static IList Create(IList list, int start, int count, bool strict)
        {
            if (count == -1)
                count = list.Count - start;

            var type = list.GetDataType();
            var listType = typeof(SlicedListView<>).MakeGenericType(new[] { type });
            var constructor = listType.GetConstructor(
                new[] { typeof(IList<>).MakeGenericType(new Type[] { type }), typeof(int), typeof(int), typeof(bool) });
            return (IList)constructor.Invoke(new object[] { list, start, count, strict });
        }
    }
}
