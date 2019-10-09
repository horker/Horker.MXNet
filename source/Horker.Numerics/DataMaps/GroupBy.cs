using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps.Extensions;

namespace Horker.Numerics.DataMaps
{
    public class GroupBy
    {
        private class KeyColumn
        {
            public string Name;
            public IList CategoryValues;
            public Dictionary<object, bool[]> FilterSet;

            public KeyColumn(string name, IList categoryValues)
            {
                Name = name;
                CategoryValues = categoryValues;
                FilterSet = new Dictionary<object, bool[]>();
            }
        }

        private class CacheKey
        {
            private object[] _keys;

            public CacheKey(object[] keys)
            {
                _keys = new object[keys.Length];

                for (var i = 0; i < keys.Length; ++i)
                {
                    var value = keys[i];
                    if (value is PSObject psobj)
                        value = psobj.BaseObject;

                    _keys[i] = value;
                }
            }

            public override bool Equals(object obj)
            {
                if (!(obj is CacheKey other))
                    return false;

                if (_keys.Length != other._keys.Length)
                    return false;

                for (var i = 0; i < _keys.Length; ++i)
                {
                    var value = other._keys[i];
                    if (value is PSObject psobj)
                        value = psobj.BaseObject;

                    if (_keys[i] != value)
                        return false;
                }

                return true;
            }

            public override int GetHashCode()
            {
                var hash = 0;
                for (var i = 0; i < _keys.Length; ++i)
                    hash = hash * 13 + _keys[i].GetHashCode();

                return hash;
            }
        }

        private DataMap _dataMap;
        private IList<string> _selectColumns;
        private List<KeyColumn> _keyColumns;
        private int _maxRowCount;
        private Dictionary<CacheKey, DataMap> _cache;

        public GroupBy(DataMap dataMap, IList<string> keyColumnNmaes, IList<string> selectColumns = null)
        {
            _dataMap = dataMap;
            _selectColumns = selectColumns;
            _keyColumns = new List<KeyColumn>();
            _maxRowCount = dataMap.MaxRowCount;
            _cache = new Dictionary<CacheKey, DataMap>();

            foreach (var c in keyColumnNmaes)
            {
                var keyColumn = new KeyColumn(c, dataMap[c].Unique().UnderlyingList);
                _keyColumns.Add(keyColumn);

                foreach (var categoryValue in keyColumn.CategoryValues)
                {
                    var filter = new bool[_maxRowCount];

                    var data = dataMap[keyColumn.Name].UnderlyingList;
                    for (var i = 0; i <  data.Count; ++i)
                    {
                        if (data[i].Equals(categoryValue))
                            filter[i] = true;
                    }

                    keyColumn.FilterSet.Add(categoryValue, filter);
                }
            }
        }

        private IList CreateFilteredListView(IList list, bool[] filter)
        {
            var type = list.GetDataType();
            var listType = typeof(FilteredListView<>).MakeGenericType(new[] { type });
            var constructor = listType.GetConstructor(new[] { typeof(IList<>).MakeGenericType(new Type[] { type }), typeof(IList<bool>) });
            return (IList)constructor.Invoke(new[] { list, filter });
        }

        public DataMap GetSubset(params object[] categories)
        {
            var cacheKey = new CacheKey(categories);
            if (_cache.TryGetValue(cacheKey, out var cached))
                return cached;

            var filter = new bool[_maxRowCount];
            for (var i = 0; i < filter.Length; ++i)
                filter[i] = true;

            for (var i = 0; i < categories.Length; ++i)
            {
                var keyColumn = _keyColumns[i];
                if (!keyColumn.FilterSet.TryGetValue(categories[i], out var f))
                    Array.Clear(filter, 0, filter.Length);

                for (var j = 0; j < f.Length; ++j)
                    filter[j] &= f[j];
            }

            var dataMap = new DataMap();

            if (_selectColumns != null)
            {
                foreach (var c in _selectColumns)
                {
                    var filtered = CreateFilteredListView(_dataMap[c].UnderlyingList, filter);
                    dataMap.Add(c, filtered);
                }
            }
            else
            {
                foreach (var column in _dataMap)
                {
                    var filtered = CreateFilteredListView(column.Data.UnderlyingList, filter);
                    dataMap.Add(column.Name, filtered);
                }
            }

            _cache.Add(cacheKey, dataMap);

            return dataMap;
        }

        public IEnumerable<DataMap> Groups()
        {
            var counts = new int[_keyColumns.Count];
            var categories = new object[_keyColumns.Count];

            while (counts[0] < _keyColumns[0].CategoryValues.Count)
            {
                for (var i = 0; i < categories.Length; ++i)
                    categories[i] = _keyColumns[i].CategoryValues[i];

                yield return GetSubset(categories);

                for (var i = _keyColumns.Count - 1; i >= 0; --i)
                {
                    ++counts[i];
                    if (counts[i] < _keyColumns[i].CategoryValues.Count)
                        break;
                }
            }
        }
    }
}
