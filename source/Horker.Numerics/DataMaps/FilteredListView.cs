﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps.Extensions;

namespace Horker.Numerics.DataMaps
{
    public class FilteredListView<T> : IList<T>, IList
    {
        private IList<T> _underlying;
        private List<int> _link;

        public FilteredListView(IList<T> underlying, IList<bool> filter)
        {
            _underlying = underlying;

            _link = new List<int>();

            for (var i = 0; i < Math.Min(filter.Count, underlying.Count); ++i)
            {
                if (filter[i])
                    _link.Add(i);
            }
        }

        public T this[int index]
        {
            get => _underlying[_link[index]];
            set => _underlying[_link[index]] = value;
        }
        object IList.this[int index] { get => this[index]; set => this[index] = (T)value; }

        public bool IsReadOnly => _underlying.IsReadOnly;

        public int Count => _link.Count;

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
            for (var i = _link.Count - 1; i >= 0; --i)
                _underlying.RemoveAt(_link[i]);

            _link.Clear();
        }

        public bool Contains(T value)
        {
            for (var i = 0; i < _link.Count; ++i)
            {
                if (_underlying[_link[i]].Equals(value))
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
            var buffer = new T[_link.Count];
            for (var i = 0; i < _link.Count; ++i)
                buffer[i + index] = _underlying[_link[i]];

            Array.Copy(buffer, 0, array, index, _link.Count);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (var i = 0; i < _link.Count; ++i)
                array[i + arrayIndex] = _underlying[_link[i]];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FilteredListViewEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < _link.Count; ++i)
            {
                if (_underlying[_link[i]].Equals(item))
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
            var i = IndexOf(item);
            if (i != -1)
            {
                RemoveAt(i);
                return true;
            }

            return false;
        }

        public void Remove(object value)
        {
            Remove((T)value);
        }

        public void RemoveAt(int index)
        {
            _underlying.RemoveAt(_link[index]);
            _link.RemoveAt(index);

            for (; index < _link.Count; ++index)
                --_link[index];
        }
    }

    public class FilteredListViewEnumerator<T> : IEnumerator<T>
    {
        private FilteredListView<T> _list;
        private int _index;

        public FilteredListViewEnumerator(FilteredListView<T> list)
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

    public static class FilteredListView
    {
        public static IList Create(IList list, bool[] filter)
        {
            var type = list.GetDataType();
            var listType = typeof(FilteredListView<>).MakeGenericType(new[] { type });
            var constructor = listType.GetConstructor(new[] { typeof(IList<>).MakeGenericType(new Type[] { type }), typeof(IList<bool>) });
            return (IList)constructor.Invoke(new[] { list, filter });
        }
    }
}
