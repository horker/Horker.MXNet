using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class FilteredListView<T> : IList<T>, IList
    {
        private IList<T> _underlying;
        private List<int> _link;

        public FilteredListView(IList<T> underlying, IList<bool> filter)
        {
            if (underlying.Count > filter.Count)
                throw new ArgumentException("Underlying list should be longer than filter");

            _underlying = underlying;

            _link = new List<int>();

            for (var i = 0; i < underlying.Count; ++i)
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
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return new FilteredListViewEnumerator<T>(this);
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

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class FilteredListViewEnumerator<T> : IEnumerator
    {
        private FilteredListView<T> _list;
        private int _index;

        public FilteredListViewEnumerator(FilteredListView<T> list)
        {
            _list = list;
            _index = -1;
        }

        public object Current => _list[_index];

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
}
