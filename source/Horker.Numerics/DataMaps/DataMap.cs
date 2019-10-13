using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.Transformers;

namespace Horker.Numerics.DataMaps
{
    public class DataMap : IList<Column>
    {
        // Private fields

        protected LinkedList<Column> _columns;
        protected Dictionary<string, LinkedListNode<Column>> _nameMap;
        protected IEqualityComparer<string> _keyComparer;

        // Properties

        public static Type[] ConversionTypes { get; set; } = new Type[] {
            typeof(double), typeof(bool), typeof(DateTime), typeof(DateTimeOffset)
        };

        public IEnumerable<Column> Columns => _columns;

        public int ColumnCount => _columns.Count;

        public IEnumerable<string> ColumnNames => _columns.Select(x => x.Name);

        public IEqualityComparer<string> ColumnNameComparer => _keyComparer;

        public Column First => _columns.First.Value;
        public Column Last => _columns.Last.Value;

        public int RowCount => MaxRowCount;

        public int MaxRowCount
        {
            get
            {
                var count = 0;
                foreach (var column in _columns)
                    count = Math.Max(column.Data.Count, count);

                return count;
            }
        }

        public int MinRowCount
        {
            get
            {
                var count = int.MaxValue;
                foreach (var column in _columns)
                    count = Math.Min(column.Data.Count, count);

                return count;
            }
        }

        public bool HasSameRowCounts()
        {
            var first = RowCount;
            foreach (var column in _columns)
                if (column.Data.Count != first)
                    return false;

            return true;
        }

        // Constructors and factory methods

        public DataMap(IEqualityComparer<string> keyComparer = null)
        {
            _keyComparer = keyComparer ?? StringComparer.CurrentCultureIgnoreCase;

            _columns = new LinkedList<Column>();
            _nameMap = new Dictionary<string, LinkedListNode<Column>>(_keyComparer);
        }

        public static DataMap CreateLike(DataMap source)
        {
            var result = new DataMap(source.ColumnNameComparer);

            foreach (var column in source.Columns)
            {
                var v = column.Data;
                var t = column.DataType;
                var data = (IList)typeof(List<>).MakeGenericType(t).GetConstructor(new Type[0]).Invoke(new object[0]);

                result.AddLast(column.Name, data);
            }

            return result;
        }

        /*
                public static DataMap FromDictionary(IDictionary<string, IList> source)
                {
                    var result = new DataMap();

                    foreach (var entry in source)
                        result.AddLast(entry.Key, entry.Value);

                    return result;
                }
        */

        public static DataMap FromDictionary(IDictionary source, IEqualityComparer<string> keyComparaer = null)
        {
            var result = new DataMap(keyComparaer);

            foreach (DictionaryEntry entry in source)
                result.AddLast((string)entry.Key, (IList)entry.Value);

            return result;
        }

        // Object methods

        public override bool Equals(object obj)
        {
            var other = obj as DataMap;
            if (obj == null)
                return false;

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

        public override int GetHashCode()
        {
            return _columns.GetHashCode() * 17 + _nameMap.GetHashCode();
        }

        // IList implementation

        public Column this[int index]
        {
            get
            {
                var i = 0;
                foreach (var c in _columns)
                {
                    if (i == index)
                        return c;
                }
                throw new ArgumentOutOfRangeException($"Index out of range ({index} for collection size {_columns.Count}");
            }
            set => throw new NotImplementedException();
        }

        public int Count => _columns.Count;

        public bool IsReadOnly => true;

        public void Add(Column item)
        {
            AddLast(item.Name, item.Data);
        }

        public void Clear()
        {
            _columns.Clear();
            _nameMap.Clear();
        }

        public bool Contains(Column item)
        {
            return Contains(item.Name);
        }

        public void CopyTo(Column[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Column> GetEnumerator()
        {
            return new DataMapEnumerator(this);
        }

        public int IndexOf(Column item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Column item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Column item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Other methods

        public virtual SeriesBase this[string name]
        {
            get => _nameMap[name].Value.Data;
            set
            {
                if (_nameMap.TryGetValue(name, out var node))
                {
                    node.Value.Data = value;
                    value.DataMap = this;
                }
                else
                    AddLast(name, value);
            }
        }

        public IList<T> GetAs<T>(string name)
        {
            return _nameMap[name].Value.Data.AsList<T>();
        }

        public IList<T> FirstAs<T>() => First.Data.AsList<T>();
        public IList<T> LastAs<T>() => Last.Data.AsList<T>();

        public void Add(string name, IList value)
        {
            AddLast(name, value);
        }

        public void Add(string name, Series value)
        {
            AddLast(name, value);
        }

        public void Add(params DataMap[] maps)
        {
            var rowCount = Math.Max(RowCount, maps.Max(df => df.RowCount));

            foreach (var df in maps)
            {
                foreach (var column in df.Columns)
                {
                    var name = column.Name;

                    var i = 1;
                    while (Contains(name))
                        name = column.Name + "_" + i++;

                    AddLast(name, column.Data);
                }
            }
        }

        public void AddFirst(string name, SeriesBase value)
        {
            if (_nameMap.ContainsKey(name))
                throw new ArgumentException($"Column '{name}' already exits");

            var node = new LinkedListNode<Column>(new Column(name, value));
            _columns.AddFirst(node);
            _nameMap.Add(name, node);
        }

        public void AddFirst(string name, IList value)
        {
            AddFirst(name, new Series(value));
        }

        public virtual void AddLast(string name, SeriesBase value)
        {
            if (_nameMap.ContainsKey(name))
                throw new ArgumentException($"Column '{name}' already exits");

            var node = new LinkedListNode<Column>(new Column(name, value));
            _columns.AddLast(node);
            _nameMap.Add(name, node);
            value.DataMap = this;
        }

        public void AddLast(string name, IList value)
        {
            AddLast(name, new Series(value));
        }

        public void AddBefore(string before, string name, SeriesBase value)
        {
            if (_nameMap.ContainsKey(name))
                throw new ArgumentException($"Column '{name}' already exits");

            if (!_nameMap.TryGetValue(before, out var beforeNode))
                throw new ArgumentException($"Column '{before}' does not exist");

            var node = new LinkedListNode<Column>(new Column(name, value));
            _columns.AddBefore(beforeNode, node);
            _nameMap.Add(name, node);
        }

        public void AddBefore(string before, string name, IList value)
        {
            AddBefore(before, name, new Series(value));
        }

        public void AddAfter(string after, string name, SeriesBase value)
        {
            if (_nameMap.ContainsKey(name))
                throw new ArgumentException($"Column '{name}' already exits");

            if (!_nameMap.TryGetValue(after, out var afterNode))
                throw new ArgumentException($"Column '{after}' does not exist");

            var node = new LinkedListNode<Column>(new Column(name, value));
            _columns.AddAfter(afterNode, node);
            _nameMap.Add(name, node);
        }

        public void AddAfter(string before, string name, IList value)
        {
            AddAfter(before, name, new Series(value));
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

        public DataMap SelectColumns(params string[] selected)
        {
            var result = new DataMap(ColumnNameComparer);

            foreach (var n in selected)
            {
                var c = this[n];
                result.AddLast(n, c);
            }

            return result;
        }

        public DataMap UnselectColumns(params string[] unselected)
        {
            var result = new DataMap(ColumnNameComparer);
            var unselectedMap = new HashSet<string>(unselected);

            foreach (var column in Columns)
            {
                if (unselectedMap.Contains(column.Name))
                    continue;
                result.AddLast(column.Name, column.Data);
            }

            return result;
        }

        public DataMap FilterRows(bool[] filter)
        {
            var dataMap = new DataMap(ColumnNameComparer);
            foreach (var c in Columns)
            {
                var filtered = FilteredListView.Create(c.Data.UnderlyingList, filter);
                dataMap.Add(c.Name, filtered);
            }

            return dataMap;
        }

        public DataMap FilterRows(SeriesBase filter)
        {
            return FilterRows(filter.AsArray<bool>());
        }

        public DataMap TopRows(int rowCount)
        {
            var dataMap = new DataMap(ColumnNameComparer);

            var filter = new bool[rowCount];
            for (var i = 0; i < rowCount; ++i)
                filter[i] = true;

            foreach (var c in Columns)
                dataMap.Add(c.Name, FilteredListView.Create(c.Data.UnderlyingList, filter));

            return dataMap;
        }

        public DataMap BottomRows(int rowCount)
        {
            var dataMap = new DataMap(ColumnNameComparer);

            var filter = new bool[MaxRowCount];
            for (var i = 0; i < rowCount; ++i)
                filter[filter.Length - 1 - i] = true;

            foreach (var c in Columns)
                dataMap.Add(c.Name, FilteredListView.Create(c.Data.UnderlyingList, filter));

            return dataMap;
        }

        public static DataMap Concatenate(params DataMap[] maps)
        {
            var rowCount = maps.Max(df => df.RowCount);

            var result = new DataMap();

            foreach (var df in maps)
            {
                foreach (var column in df.Columns)
                {
                    var name = column.Name;

                    var i = 1;
                    while (result.Contains(name))
                        name = column.Name + "_" + i++;

                    result.Add(name, column.Data);
                }
            }

            return result;
        }

        public GroupBy GroupBy(string[] groupingColumnNames, string[] selectColumns = null)
        {
            return new GroupBy(this, groupingColumnNames, selectColumns);
        }

        public IEnumerable<Summary> Describe()
        {
            foreach (var column in Columns)
            {
                Summary result = null;
                try
                {
                    result = column.Data.Describe();
                }
                catch (InvalidOperationException)
                {
                    result = new Summary()
                    {
                        Count = column.Data.Count,
                        NaN = column.Data.CountNaN(),
                        Unique = column.Data.CountUnique()
                    };
                }
                result.Name = column.Name;
                yield return result;
            }
        }

        // Conversions

        public Dictionary<string, IList> ToDictionary()
        {
            var result = new Dictionary<string, IList>(ColumnNameComparer);

            foreach (var column in Columns)
                result[column.Name] = column.Data;

            return result;
        }

        public DataMap TryTypeConversion(Type[] possibleTypes = null)
        {
            var d = new DataMap(ColumnNameComparer);

            foreach (var column in Columns)
                d.Add(column.Name, column.Data.Convert(possibleTypes));

            return d;
        }

        public T[,] To2DArray<T>()
        {
            var columnCount = ColumnCount;
            var maxRowCount = MaxRowCount;

            var result = new T[columnCount, maxRowCount];

            var c = 0;
            foreach (var column in _columns)
            {
                var r = 0;
                foreach (var e in column.Data.UnderlyingList)
                    result[c, r++] = (T)e;
                ++c;
            }

            return result;
        }

        public Array To2DArray(Type type = null)
        {
            type = type ?? First.DataType;

            if (type == typeof(double)) return To2DArray<double>();
            if (type == typeof(float)) return To2DArray<float>();
            if (type == typeof(long)) return To2DArray<long>();
            if (type == typeof(int)) return To2DArray<int>();
            if (type == typeof(short)) return To2DArray<short>();
            if (type == typeof(byte)) return To2DArray<byte>();
            if (type == typeof(sbyte)) return To2DArray<sbyte>();
            if (type == typeof(decimal)) return To2DArray<decimal>();
            if (type == typeof(bool)) return To2DArray<bool>();
            if (type == typeof(string)) return To2DArray<string>();
            if (type == typeof(DateTime)) return To2DArray<DateTime>();
            if (type == typeof(DateTimeOffset)) return To2DArray<DateTimeOffset>();

            return To2DArray<object>();
        }

        public IEnumerable<PSObject> ToPSObject(int maxCount = int.MaxValue, int skip = 0)
        {
            int count = RowCount;

            var columnNames = ColumnNames;

            for (var i = skip; i < skip + maxCount; ++i)
            {
                var hasElement = false;
                var pso = new PSObject();

                foreach (var column in Columns)
                {
                    if (i > column.Data.Count - 1)
                        continue;

                    pso.Properties.Add(new PSNoteProperty(column.Name, column.Data[i]));
                    hasElement = true;
                }

                if (!hasElement)
                    break;

                yield return pso;
            }
        }

        public IEnumerable<PSObject> Head(int maxCount = 10)
        {
            return ToPSObject(maxCount);
        }

        public IEnumerable<PSObject> Tail(int count = 10)
        {
            var total = Columns.Select(x => x.Data.Count).Max();
            return ToPSObject(count, total - count);
        }

        // Transformers

        public void OneHot(string columnName, OneHotType oneHotType = OneHotType.OneHot, string columnNameFormat = "{1}_{0}")
        {
            var format = string.Format(columnNameFormat, "{0}", columnName);
            var oneHot = this[columnName].OneHot(oneHotType, format);

            foreach (var column in oneHot)
                AddBefore(columnName, column.Name, column.Data);

            Remove(columnName);
        }
    }
}
