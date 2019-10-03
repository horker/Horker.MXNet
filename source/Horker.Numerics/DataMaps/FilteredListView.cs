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
        private List<int> _index;

        public List<int> IndexList => _index;

        public FilteredListView(IList underlying, IList<bool> filter)
        {
            if (underlying.Count != filter.Count)
                throw new ArgumentException("underlying and filter should have the same length");

            _underlying = underlying;

            _index = new List<int>();

            for (var i = 0; i < underlying.Count; ++i)
            {
                if (filter[i])
                    _index.Add(i);
            }
        }

        public object this[int index]
        {
            get => _underlying[_index[index]];
            set => _underlying[_index[index]] = value;
        }

        public bool IsReadOnly => _underlying.IsReadOnly;

        public bool IsFixedSize => true;

        public int Count => _index.Count;

        public object SyncRoot => _underlying.SyncRoot;

        public bool IsSynchronized => _underlying.IsSynchronized;

        public int Add(object value)
        {
            throw new InvalidOperationException("This object is fixed size");
        }

        public void Clear()
        {
            throw new InvalidOperationException("This object is fixed size");
        }

        public bool Contains(object value)
        {
            for (var i = 0; i < _index.Count; ++i)
            {
                if (_underlying[_index[i]] == value)
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
            for (var i = 0; i < _index.Count; ++i)
            {
                if (_underlying[_index[i]] == value)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, object value)
        {
            throw new InvalidOperationException("This object is fixed size");
        }

        public void Remove(object value)
        {
            throw new InvalidOperationException("This object is fixed size");
        }

        public void RemoveAt(int index)
        {
            throw new InvalidOperationException("This object is fixed size");
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
