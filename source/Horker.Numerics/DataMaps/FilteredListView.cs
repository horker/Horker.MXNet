using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class FilteredListView : IList
    {
        private IList _underlying;
        private List<int> _link;

        public FilteredListView(IList underlying, IList<bool> filter)
        {
            if (underlying.Count != filter.Count)
                throw new ArgumentException("Underlying and filter should have the same length");

            _underlying = underlying;

            _link = new List<int>();

            for (var i = 0; i < underlying.Count; ++i)
            {
                if (filter[i])
                    _link.Add(i);
            }
        }

        public object this[int index]
        {
            get => _underlying[_link[index]];
            set => _underlying[_link[index]] = value;
        }

        public bool IsReadOnly => _underlying.IsReadOnly;

        public bool IsFixedSize => _underlying.IsFixedSize;

        public int Count => _link.Count;

        public object SyncRoot => _underlying.SyncRoot;

        public bool IsSynchronized => false;

        public int Add(object value)
        {
            throw new InvalidOperationException("This list doens not support the Add() operation");
        }

        public void Clear()
        {
            for (var i = _link.Count - 1; i >= 0; --i)
                _underlying.RemoveAt(_link[i]);

            _link.Clear();
        }

        public bool Contains(object value)
        {
            for (var i = 0; i < _link.Count; ++i)
            {
                if (_underlying[_link[i]] == value)
                    return true;
            }
            return false;
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return new FilteredListViewEnumerator(this);
        }

        public int IndexOf(object value)
        {
            for (var i = 0; i < _link.Count; ++i)
            {
                if (_underlying[_link[i]] == value)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, object value)
        {
            throw new InvalidOperationException("This list does not support Insert() operation");
        }

        public void Remove(object value)
        {
            var i = IndexOf(value);
            if (i != -1)
                RemoveAt(i);
        }

        public void RemoveAt(int index)
        {
            _underlying.RemoveAt(_link[index]);
            _link.RemoveAt(index);

            for (; index < _link.Count; ++index)
                --_link[index];
        }
    }

    public class FilteredListViewEnumerator : IEnumerator
    {
        private FilteredListView _list;
        private int _index;

        public FilteredListViewEnumerator(FilteredListView list)
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
