using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps.Utilities;
using Horker.Numerics.Transformers;
using Horker.Numerics.Utilities;

namespace Horker.Numerics.DataMaps
{
    [Serializable]
    public class DataMap : ISerializable
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

        public SeriesBase First => _columns.First.Value.Data;
        public SeriesBase Last => _columns.Last.Value.Data;

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

        public static DataMap FromJagged<T>(T[][] source, string[] columnNames = null, IEqualityComparer<string> keyComparaer = null)
        {
            if (columnNames == null)
            {
                var c = source[0].Length;
                columnNames = new string[c];
                for (var i = 0; i < c; ++i)
                    columnNames[i] = "Column" + i;
            }

            var result = new DataMap(keyComparaer);

            for (var i = 0; i < columnNames.Length; ++i)
            {
                var data = new List<T>(source.Length);
                result.Add(columnNames[i], data);
                for (var j = 0; j < source.Length; ++j)
                {
                    var row = source[j];
                    if (i < row.Length)
                        data.Add(row[i]);
                    else
                        data.Add(TypeTrait<T>.GetNaN());
                }
            }

            return result;
        }

        public static DataMap From2DArray<T>(T[,] source, string[] columnNames = null, IEqualityComparer<string> keyComparaer = null)
        {
            if (columnNames == null)
            {
                var c = source.GetLength(1);
                columnNames = new string[c];
                for (var i = 0; i < c; ++i)
                    columnNames[i] = "Column" + i;
            }

            var result = new DataMap(keyComparaer);

            for (var i = 0; i < source.GetLength(1); ++i)
            {
                var data = new List<T>(source.Length);
                result.Add(columnNames[i], data);
                for (var j = 0; j < source.GetLength(0); ++j)
                    data.Add(source[j, i]);
            }

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

        // ISerializable implementation

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("names", ColumnNames.ToArray());
            info.AddValue("values", _columns.Select(c => c.Data.UnderlyingList).ToArray());

            // TODO: StringComparer is not serializable
            // info.AddValue("comparer", _keyComparer);
        }

        protected DataMap(SerializationInfo info, StreamingContext context)
        {
            var names = (string[])info.GetValue("names", typeof(string[]));
            var values = (IList[])info.GetValue("values", typeof(IList[]));

            _columns = new LinkedList<Column>();
            _nameMap = new Dictionary<string, LinkedListNode<Column>>();

            // TODO: StringComparer is not serializable
            // _keyComparer = (IEqualityComparer<string>)info.GetValue("comparer", typeof(IEqualityComparer<string>));
            _keyComparer = StringComparer.CurrentCultureIgnoreCase;

            for (var i = 0; i < names.Length; ++i)
                AddLast(names[i], values[i]);
        }

        public void Save(Stream stream)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }

        public void Save(string path)
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                Save(stream);
            }
        }

        public static DataMap Load(Stream stream)
        {
            var formatter = new BinaryFormatter();
            return (DataMap)formatter.Deserialize(stream);
        }

        public static DataMap Load(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return Load(stream);
            }
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

        // Other methods

        public virtual SeriesBase this[string name]
        {
            get
            {
                if (_nameMap.TryGetValue(name, out var node))
                    return node.Value.Data;

                throw new KeyNotFoundException($"Column '{name}' is not found");
            }

            set
            {
                if (_nameMap.TryGetValue(name, out var node))
                {
                    node.Value.Data = value;
                    value.DataMap = this;
                }
                else
                {
                    AddLast(name, value);
                }
            }
        }

        public IList<T> GetAs<T>(string name)
        {
            return _nameMap[name].Value.Data.AsList<T>();
        }

        public IList<T> FirstAs<T>() => First.AsList<T>();
        public IList<T> LastAs<T>() => Last.AsList<T>();

        public void Add(string name, SeriesBase value)
        {
            AddLast(name, value);
        }

        public void Add(string name, Array value)
        {
            AddLast(name, value);
        }

        public void Add(string name, IList value)
        {
            AddLast(name, value);
        }

        public void Add<T>(string name, List<T> value)
        {
            AddLast(name, value);
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

        public void AddLast(string name, Array value)
        {
            AddLast(name, new Series(value));
        }

        public void AddLast(string name, IList value)
        {
            AddLast(name, new Series(value));
        }

        public void AddLast<T>(string name, List<T> value)
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

        // Copy methods

        public DataMap ShallowCopy()
        {
            var result = new DataMap(_keyComparer);

            foreach (var column in _columns)
                result.Add(column.Name, column.Data);

            return result;
        }

        public DataMap DeepCopy()
        {
            var result = new DataMap(_keyComparer);

            foreach (var column in _columns)
            {
                var l = column.Data.Copy();
                result.Add(column.Name, l);
            }

            return result;
        }

        public DataMap CreateLike(int capacity = 0, int count = 0)
        {
            var result = new DataMap(_keyComparer);

            foreach (var column in Columns)
            {
                var v = column.Data;
                var t = column.DataType;
                var data = Utils.CreateList(t, capacity, count);

                result.AddLast(column.Name, data);
            }

            return result;
        }

        // Other manipulation

        public int GetColumnIndex(string name)
        {
            var i = 0;
            foreach (var c in _columns)
            {
                if (c.Name == name)
                    return i;
                ++i;
            }

            return -1;
        }

        public DataMap Filter(bool[] filter)
        {
            var dataMap = new DataMap(ColumnNameComparer);
            foreach (var c in Columns)
            {
                var filtered = FilteredListView.Create(c.Data.UnderlyingList, filter);
                dataMap.Add(c.Name, filtered);
            }

            return dataMap;
        }

        public DataMap Filter(SeriesBase filter)
        {
            return Filter(filter.AsArray<bool>());
        }

        public DataMap LeftJoin(DataMap other, JoinKeyMap joinKeyMap, string[] rightKeyColumns = null)
        {
            var result = ShallowCopy();
            result.LeftJoinFill(other, joinKeyMap, rightKeyColumns);
            return result;
        }

        public DataMap LeftJoin(DataMap other, string[] leftKeyColumns, string[] rightKeyColumns = null)
        {
            if (rightKeyColumns == null)
                rightKeyColumns = leftKeyColumns;

            return LeftJoin(other, new JoinKeyMap(this, leftKeyColumns), rightKeyColumns);
        }

        public void LeftJoinFill(DataMap other, JoinKeyMap joinKeyMap, string[] rightKeyColumns = null)
        {
            var rowCount = MaxRowCount;
            var indexes = joinKeyMap.GetMatchingIndexes(other, rightKeyColumns);

            foreach (var column in other.Columns)
            {
                var l = column.Data.UnderlyingList;
                var newColumn = Utils.CreateList(column.DataType, rowCount, 0);

                var i = 0;
                var count = Math.Min(rowCount, l.Count);
                for (i = 0; i < count; ++i)
                {
                    var j = indexes[i];
                    if (j == -1)
                        newColumn.Add(TypeTrait.GetNaN(column.DataType));
                    else
                        newColumn.Add(l[j]);
                }

                var name = GetUniqueColumnName(column.Name);
                Add(name, newColumn);
            }
        }

        public void LeftJoinFill(DataMap other, string[] leftKeyColumns, string[] rightKeyColumns = null)
        {
            if (rightKeyColumns == null)
                rightKeyColumns = leftKeyColumns;

            LeftJoinFill(other, new JoinKeyMap(this, leftKeyColumns), rightKeyColumns);
        }

        public DataMap Slice(int start, int count = -1)
        {
            var dataMap = new DataMap(ColumnNameComparer);
            foreach (var c in Columns)
            {
                var slice = SlicedListView.Create(c.Data.UnderlyingList, start, count, false);
                dataMap.Add(c.Name, slice);
            }

            return dataMap;
        }

        public DataMap[] Split(params int[] counts)
        {
            var results = new DataMap[counts.Length];

            int start = 0;
            for (var i = 0; i < counts.Length; ++i)
            {
                var dataMap = new DataMap(ColumnNameComparer);
                foreach (var c in Columns)
                {
                    var slice = SlicedListView.Create(c.Data.UnderlyingList, start, counts[i], false);
                    dataMap.Add(c.Name, slice);
                }

                results[i] = dataMap;
                start += counts[i];
            }

            return results;
        }

        public DataMap[] Split(params double[] props)
        {
            if (Math.Abs(props.Sum() - 1.0) > 1e-5)
                throw new ArgumentException("Sum of proportions should be equal to 1.0");

            var counts = new int[props.Length];

            int start = 0;
            int maxRowCount = MaxRowCount;
            for (var i = 0; i < props.Length; ++i)
            {
                if (i == props.Length - 1)
                    counts[i] = maxRowCount - start;
                else
                    counts[i] = (int)(props[i] * maxRowCount);
            }

            return Split(counts);
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

        public void Concatenate(params DataMap[] maps)
        {
            foreach (var m in maps)
            {
                foreach (var column in m.Columns)
                {
                    var name = column.Name;

                    var i = 1;
                    while (Contains(name))
                        name = column.Name + "_" + i++;

                    Add(name, column.Data);
                }
            }
        }

        public static DataMap ConcatenateAll(params DataMap[] maps)
        {
            var result = new DataMap(maps[0].ColumnNameComparer);
            result.Concatenate(maps);
            return result;
        }

        public void Pile(params DataMap[] maps)
        {
            var rowCount = maps.Select(x => x.MaxRowCount).Sum();

            foreach (var m in maps)
            {
                foreach (var c in m.Columns)
                {
                    if (!Contains(c.Name))
                        Add(c.Name, Utils.CreateList(c.DataType, rowCount, rowCount));
                }
            }

            var offset = 0;
            foreach (var m in maps)
            {
                foreach (var c in m.Columns)
                {
                    var series = this[c.Name];
                    if (series.DataType == c.DataType)
                    {
                        for (var i = 0; i < c.Data.Count; ++i)
                            series[i + offset] = c.Data[i];
                    }
                    else
                    {
                        var l = SmartConverter.ConvertTo(series.DataType, c.Data.UnderlyingList);
                        for (var i = 0; i < l.Count; ++i)
                            series[i + offset] = l[i];
                    }
                }
                offset += m.MaxRowCount;
            }
        }

        public static DataMap PileAll(params DataMap[] maps)
        {
            var result = new DataMap(maps[0].ColumnNameComparer);
            result.Pile(maps);
            return result;
        }

        private string GetUniqueColumnName(string baseName)
        {
            var name = baseName;
            int i = 1;
            while (_nameMap.ContainsKey(name))
                name = baseName + "_" + i++;

            return name;
        }

        public DataMap Unstack(string columnToUnstack,
            string[] keyColumns = null,
            string[] selectColumnNames = null,
            int minColumnCount = 0,
            int maxColumnCount = int.MaxValue)
        {
            if (selectColumnNames == null)
                selectColumnNames = ColumnNames.ToArray();

            var result = new DataMap(ColumnNameComparer);

            // Prepare groups that are grouped by key column values.

            DataMap[] groups;
            if (keyColumns == null)
                groups = new[] { this };
            else
            {
                var columns = new List<string>(keyColumns);
                columns.Add(columnToUnstack);
                columns.AddRange(selectColumnNames);
                groups = new GroupBy(this, keyColumns, columns).Groups().ToArray();
            }

            var groupCount = groups.Length;

            // Insert key column values.

            foreach (var k in keyColumns)
            {
                var list = Utils.CreateList(this[k].DataType, groupCount, 0);
                foreach (var g in groups)
                    list.Add(g[k][0]);
                result.Add(k, list);
            }

            // Prepare columnHash -- the mapping from values to be unstacked to lists.

            var toUnstack = this[columnToUnstack];
            var columnHash = new Dictionary<object, Dictionary<string, IList>>();
            var columnCount = 0;
            for (var i = 0; i < toUnstack.Count; ++i)
            {
                if (!columnHash.ContainsKey(toUnstack[i]))
                {
                    var columnToListMap = new Dictionary<string, IList>();
                    foreach (var s in selectColumnNames)
                    {
                        var name = result.GetUniqueColumnName(toUnstack[i].ToString() + "_" + s);
                        var list = Utils.CreateList(this[s].DataType, groupCount, 0);
                        result.Add(name, list);
                        columnToListMap.Add(s, list);
                    }
                    columnHash.Add(toUnstack[i], columnToListMap);
                    ++columnCount;
                    if (columnCount >= maxColumnCount)
                        break;
                }
            }

            // Add columns to satisfy minimum column count requirement.

            for (; columnCount < minColumnCount; ++columnCount)
            {
                foreach (var s in selectColumnNames)
                {
                    var name = result.GetUniqueColumnName("na_" + s);
                    var list = Utils.CreateList(this[s].DataType, groupCount, groupCount);
                    result.Add(name, list);
                }
            }

            // Insert unstacked values.

            for (var i = 0; i < groups.Length; ++i)
            {
                var w = groups[i][columnToUnstack];
                for (var j = 0; j < w.Count; ++j)
                {
                    if (!columnHash.TryGetValue(w[j], out var columnToListMap))
                        continue;

                    foreach (var s in selectColumnNames)
                    {
                        var from = groups[i][s];
                        var to = columnToListMap[s];

                        if (to.Count > i)
                            throw new ArgumentException($"In the row {i}, the value '{w[j]}' appears more than once");

                        if (to.Count < i)
                        {
                            var nan = TypeTrait.GetNaN(from.DataType);
                            while (to.Count < i)
                                to.Add(nan);
                        }

                        to.Add(from[j]);
                    }
                }
            }

            // Fill the last rows with NaN values.

            foreach (var c in result.Columns)
            {
                if (c.Data.Count < groupCount)
                {
                    var nan = TypeTrait.GetNaN(c.DataType);
                    while (c.Data.Count < groupCount)
                        c.Data.Add(nan);
                }
            }

            return result;
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

        public GroupBy GroupBy(string[] groupingColumnNames, string[] selectColumnNames = null)
        {
            return new GroupBy(this, groupingColumnNames, selectColumnNames);
        }

        private T[] JoinArrays<T>(params T[][] arrays)
        {
            var total = arrays.Sum(x => x == null ? 0 : x.Length);
            var result = new T[total];

            var position = 0;
            for (var i = 0; i < arrays.Length; ++i)
            {
                var a = arrays[i];
                if (a == null)
                    continue;
                Array.Copy(a, 0, result, position, a.Length);
                position += a.Length;
            }

            return result;
        }

        public DataMap Summarize(string[] groupingColumnNames, IDictionary aggregators)
        {
            return new GroupBy(this, groupingColumnNames).Summarize( aggregators);
        }

        public DataMap Summarize(string[] groupingColumnNames, object[] aggregators)
        {
            return new GroupBy(this, groupingColumnNames).Summarize(aggregators);
        }

        public DataMap Summarize(string[] groupingColumnNames, string[] aggregateColumnNames, IDictionary aggregators)
        {
            var columns = JoinArrays(groupingColumnNames, aggregateColumnNames);

            return new GroupBy(this, groupingColumnNames, columns).
                Summarize(aggregateColumnNames, aggregators);
        }

        public DataMap Summarize(string[] groupingColumnNames, string[] aggregateColumnNames, object[] aggregators)
        {
            var columns = JoinArrays(groupingColumnNames, aggregateColumnNames);

            return new GroupBy(this, groupingColumnNames, columns).
                Summarize(aggregateColumnNames, aggregators);
        }

        public IEnumerable<KFold> KFold(int k, bool shuffle = false, int seed = -1)
        {
            var splitter = new KFoldSplitter(this, k, shuffle, seed);
            return splitter.EnumerateFolds();
        }

        private int[] GetOrder<T>(IList<T> list)
        {
            var order = new int[list.Count];
            for (var i = 0; i < order.Length; ++i)
                order[i] = i;

            Array.Sort(list.ToArray(), order, 0, order.Length);
            return order;
        }

        private IList GetReorderedList<T>(IList<T> list, int[] order)
        {
            var result = new List<T>(list.Count);
            for (var i = 0; i < list.Count; ++i)
                result.Add(list[order[i]]);

            return result;
        }

        public DataMap Sort(string keyColumn)
        {
            var order = GetOrder((dynamic)this[keyColumn].UnderlyingList);

            var result = new DataMap(ColumnNameComparer);
            foreach (var column in Columns)
            {
                var reordered = GetReorderedList((dynamic)column.Data.UnderlyingList, order);
                result.Add(column.Name, new Series(reordered));
            }

            return result;
        }

        // Conversions

        public static explicit operator double[,](DataMap value) { return value.To2DArray<double>(); }
        public static explicit operator float[,](DataMap value) { return value.To2DArray<float>(); }
        public static explicit operator long[,](DataMap value) { return value.To2DArray<long>(); }
        public static explicit operator int[,](DataMap value) { return value.To2DArray<int>(); }
        public static explicit operator short[,](DataMap value) { return value.To2DArray<short>(); }
        public static explicit operator byte[,](DataMap value) { return value.To2DArray<byte>(); }
        public static explicit operator sbyte[,](DataMap value) { return value.To2DArray<sbyte>(); }
        public static explicit operator decimal[,](DataMap value) { return value.To2DArray<decimal>(); }
        public static explicit operator string[,](DataMap value) { return value.To2DArray<string>(); }
        public static explicit operator bool[,](DataMap value) { return value.To2DArray<bool>(); }
        public static explicit operator DateTime[,](DataMap value) { return value.To2DArray<DateTime>(); }
        public static explicit operator DateTimeOffset[,](DataMap value) { return value.To2DArray<DateTimeOffset>(); }
        public static explicit operator object[,](DataMap value) { return value.To2DArray<object>(); }

        public static explicit operator double[][](DataMap value) { return value.ToJagged<double>(); }
        public static explicit operator float[][](DataMap value) { return value.ToJagged<float>(); }
        public static explicit operator long[][](DataMap value) { return value.ToJagged<long>(); }
        public static explicit operator int[][](DataMap value) { return value.ToJagged<int>(); }
        public static explicit operator short[][](DataMap value) { return value.ToJagged<short>(); }
        public static explicit operator byte[][](DataMap value) { return value.ToJagged<byte>(); }
        public static explicit operator sbyte[][](DataMap value) { return value.ToJagged<sbyte>(); }
        public static explicit operator decimal[][](DataMap value) { return value.ToJagged<decimal>(); }
        public static explicit operator string[][](DataMap value) { return value.ToJagged<string>(); }
        public static explicit operator bool[][](DataMap value) { return value.ToJagged<bool>(); }
        public static explicit operator DateTime[][](DataMap value) { return value.ToJagged<DateTime>(); }
        public static explicit operator DateTimeOffset[][](DataMap value) { return value.ToJagged<DateTimeOffset>(); }
        public static explicit operator object[][](DataMap value) { return value.ToJagged<object>(); }

        public Dictionary<string, IList> ToDictionary()
        {
            var result = new Dictionary<string, IList>(ColumnNameComparer);

            foreach (var column in Columns)
                result[column.Name] = column.Data.UnderlyingList;

            return result;
        }

        public DataMap TryConversion(Type[] possibleTypes = null)
        {
            var d = new DataMap(ColumnNameComparer);

            foreach (var column in Columns)
                d.Add(column.Name, column.Data.TryConversion(possibleTypes));

            return d;
        }

        public DataMap CastDown()
        {
            var d = new DataMap(ColumnNameComparer);

            foreach (var column in Columns)
                d.Add(column.Name, column.Data.CastDown());

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
                    result[c, r++] = SmartConverter.ConvertTo<T>(e);
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

        public T[][] ToJagged<T>()
        {
            var columnCount = ColumnCount;
            var maxRowCount = MaxRowCount;

            var result = new T[maxRowCount][];

            for (var i = 0; i < maxRowCount; ++i)
            {
                string columnName = ""; // for better exception message
                try
                {
                    var row = new T[columnCount];
                    result[i] = row;
                    var c = 0;
                    foreach (var column in _columns)
                    {
                        columnName = column.Name;
                        var list = column.Data.UnderlyingList;
                        if (i < list.Count)
                            row[c] = SmartConverter.ConvertTo<T>(list[i]);
                        else
                            row[c] = TypeTrait<T>.GetNaN();
                        ++c;
                    }
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Error occurred in column '{columnName}' at row {i}; See inner exception for details", ex);
                }
            }

            return result;
        }

        public Array ToJagged(Type type = null)
        {
            type = type ?? First.DataType;

            var m = typeof(DataMap).GetMethod("ToJagged",
                BindingFlags.Public | BindingFlags.Instance,
                null, CallingConventions.Any, Type.EmptyTypes, null);
            Debug.Assert(m != null);
            var gm = m.MakeGenericMethod(type);

            return (Array)gm.Invoke(this, new object[0]);
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

        public IEnumerable<PSObject> Rows => ToPSObject();

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

        public void OneHotEncoding(string columnName, OneHotType oneHotType = OneHotType.OneHot, string columnNameFormat = "{1}_{0}")
        {
            var format = string.Format(columnNameFormat, "{0}", columnName);
            var oneHot = this[columnName].OneHotEncoding(oneHotType, format);

            foreach (var column in oneHot)
                AddBefore(columnName, column.Name, column.Data);

            Remove(columnName);
        }
    }
}
