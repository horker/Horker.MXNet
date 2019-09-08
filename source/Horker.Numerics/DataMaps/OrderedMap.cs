using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Horker.Numerics.DataMaps
{
    // Note:
    // It is arguable whether OrderedMap should implement the IDictionary interface or not.
    // First, PowerShell treats objects that implement IDictionary as if they have properties of hash keys as their names,
    // so that the user can access their elements directly by property access syntax rather than the ["element"] accessor.
    // On the other hand, PowerShell suspends method completion for such objects so that
    // the user needs to type full method names such as 'Head()'.
    // Currently we don't implement the IDictionary interface because the latter behavior seems to be somewhat surprising.

    public class OrderedMap : IDictionary<string, Column> /* , IDictionary */
    {
        protected IEqualityComparer<string> _keyComparer;
        protected LinkedList<Column> _columns;
        protected Dictionary<string, LinkedListNode<Column>> _nameMap;

        public OrderedMap(IEqualityComparer<string> keyComparaer = null)
        {
            _keyComparer = keyComparaer ?? StringComparer.InvariantCultureIgnoreCase;

            _columns = new LinkedList<Column>();
            _nameMap = new Dictionary<string, LinkedListNode<Column>>(keyComparaer);
        }

        // Object methods

        protected virtual bool TestEquality(OrderedMap other)
        {
            if (_columns.Count != other._columns.Count)
                return false;

            var c1 = _columns.GetEnumerator();
            var c2 = other._columns.GetEnumerator();
            while (c1.MoveNext())
            {
                c2.MoveNext();
                if (!c1.Current.Equals(c2.Current))
                    return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(OrderedMap))
                return false;

            return TestEquality(obj as OrderedMap);
        }

        public override int GetHashCode()
        {
            return _columns.GetHashCode() * 17 + _nameMap.GetHashCode();
        }

        // IDictionary<string, Column> implementations

        public int Count => _columns.Count;

        public bool IsReadOnly => false;

        public ICollection<string> Keys => _columns.Select(x => x.Name).ToArray();

        public IEnumerable Values => _columns.Select(x => x.Data);

        public Column this[string name]
        {
            get => _nameMap[name].Value;
            set
            {
                AddLast(name, value);
            }
        }

        ICollection<Column> IDictionary<string, Column>.Values => _columns;

        public void Add(string key, Column value)
        {
            if (key != value.Name)
                throw new ArgumentException("key and column name is different");

            AddLast(value.Name, value.Data);
        }

        public void Add(KeyValuePair<string, Column> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _columns.Clear();
            _nameMap.Clear();
        }

        public bool Contains(KeyValuePair<string, Column> item)
        {
            return TryGetValue(item.Key, out var v) && v == item.Value;
        }

        public bool ContainsKey(string key)
        {
            return Contains(key);
        }

        public void CopyTo(KeyValuePair<string, Column>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, Column>> GetEnumerator()
        {
            return new OrderedMapEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new OrderedMapEnumerator(this);
        }

        public bool Remove(KeyValuePair<string, Column> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(string name, out Column value)
        {
            if (_nameMap.TryGetValue(name, out var column))
            {
                value = column.Value;
                return true;
            }
            value = null;
            return false;
        }

        // Additional properties and methods

        public int ColumnCount => _columns.Count;

        public IEnumerable<Column> Columns => _columns;

        public IEnumerable<string> ColumnNames => _columns.Select(x => x.Name);

        public void Add(string name, IList value)
        {
            AddLast(name, value);
        }

        public bool Contains(string name)
        {
            return _nameMap.ContainsKey(name);
        }

        public bool Remove(string name)
        {
            LinkedListNode<Column> column = null;
            if (_nameMap.TryGetValue(name, out column))
            {
                _nameMap.Remove(name);
                _columns.Remove(column);
                return true;
            }
            return false;
        }

        public virtual void AddFirst(string name, IList value)
        {
            if (value is Column c)
            {
                if (name != c.Name)
                    throw new ArgumentException("name and column name is different");
                value = c.Data;
            }

            if (_nameMap.ContainsKey(name))
                Remove(name);

            var column = new LinkedListNode<Column>(new Column(name, value));
            _columns.AddFirst(column);
            _nameMap.Add(name, column);
        }

        public virtual void AddLast(string name, IList value)
        {
            if (value is Column c)
            {
                if (name != c.Name)
                    throw new ArgumentException("name and column name is different");
                value = c.Data;
            }

            if (_nameMap.ContainsKey(name))
                Remove(name);

            var column = new LinkedListNode<Column>(new Column(name, value));
            _columns.AddLast(column);
            _nameMap.Add(name, column);
        }

        public void MoveToFirst(string name)
        {
            var column = _nameMap[name];
            _columns.Remove(column);
            _columns.AddFirst(column);
        }

        public void MoveToLast(string name)
        {
            var column = _nameMap[name];
            _columns.Remove(column);
            _columns.AddLast(column);
        }

        public void SetOrder(params string[] columnNames)
        {
            foreach (var name in columnNames.Reverse())
                MoveToFirst(name);
        }

    }
}
