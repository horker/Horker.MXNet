using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Horker.Numerics.DataMaps
{
    public class DataMap : OrderedMap
    {
        public IEqualityComparer<string> ColumnNameComparer => _keyComparer;

        public DataMap(IEqualityComparer<string> columnNameComparer = null)
            : base(columnNameComparer)
        {
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

        public static DataMap FromDictionary(IDictionary<string, IList> source)
        {
            var result = new DataMap();

            foreach (var entry in source)
                result.AddLast(entry.Key, entry.Value);

            return result;
        }

        // Object methods

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(DataMap))
                return false;

            return TestEquality(obj as DataMap);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // Other properties and methods

        public Column First => _columns.First.Value;
        public Column Last => _columns.Last.Value;

        public int RowCount
        {
            get
            {
                if (_columns.Count > 0)
                    return _columns.First.Value.Data.Count;
                return 0;
            }
        }

        public IList<T> GetAs<T>(string name)
        {
            return _nameMap[name].Value.AsList<T>();
        }

        public IList<T> FirstAs<T>() => First.AsList<T>();
        public IList<T> LastAs<T>() => Last.AsList<T>();

        public DataMap Select(params string[] selected)
        {
            var result = new DataMap(ColumnNameComparer);

            foreach (var n in selected)
            {
                var c = this[n];
                result.AddLast(n, c.Data);
            }

            return result;
        }

        protected DataMap Unselec(params string[] unselected)
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

        public DataMap FilterRow(Func<int, bool> filterFunc)
        {
            var filtered = DataMap.CreateLike(this);

            var newRowCount = 0;
            for (var i = 0; i < RowCount; ++i)
            {
                if (filterFunc(i))
                {
                    var source = Columns.GetEnumerator();
                    var dest = filtered.Columns.GetEnumerator();
                    while (source.MoveNext())
                    {
                        dest.MoveNext();
                        var s = source.Current;
                        var d = dest.Current;
                        d.Data.Add(s.Data[i]);
                    }
                    ++newRowCount;
                }
            }

            return filtered;
        }

        public static DataMap Concatenate(params DataMap[] dfs)
        {
            var rowCount = dfs.Max(df => df.RowCount);

            var result = new DataMap();

            foreach (var df in dfs)
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

        // Conversions

        public Dictionary<string, IList> ToDictionary()
        {
            var result = new Dictionary<string, IList>();

            foreach (var column in Columns)
                result[column.Name] = column.Data;

            return result;
        }

        public DataMap TryTypeConversion(Type[] possibleTypes)
        {
            var d = new DataMap(ColumnNameComparer);

            foreach (var column in Columns)
                d.Add(column.Name, column.ConvertTo(possibleTypes));

            return d;
        }
    }
}
