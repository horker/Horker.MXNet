﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps.Extensions;
using Horker.Numerics.Utilities;

namespace Horker.Numerics.DataMaps
{
    public class GroupBy : IEnumerable<DataMap>
    {
        private class GroupingColumn
        {
            public string Name;
            public IList CategoryValues;
            public Dictionary<object, bool[]> FilterSet;

            public GroupingColumn(string name, IList categoryValues)
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
                    _keys[i] = Utils.StripOffPSObject(keys[i]);
            }

            public override bool Equals(object obj)
            {
                if (!(obj is CacheKey other))
                    return false;

                if (_keys.Length != other._keys.Length)
                    return false;

                for (var i = 0; i < _keys.Length; ++i)
                {
                    var value = Utils.StripOffPSObject(other._keys[i]);
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
        private IList<string> _groupingColumnNames;
        private IList<string> _selectColumnNames;
        private List<GroupingColumn> _groupingColumns;
        private int _maxRowCount;
        private Dictionary<CacheKey, DataMap> _cache;

        public DataMap DataMap => _dataMap;
        public IList<string> GroupingColumnNames => _groupingColumnNames;
        public IList<string> SelectColumnNames => _selectColumnNames;

        public GroupBy(DataMap dataMap, IList<string> groupingColumnNames, IList<string> selectColumnNames = null)
        {
            _dataMap = dataMap;
            _groupingColumnNames = groupingColumnNames;
            _selectColumnNames = selectColumnNames;

            _groupingColumns = new List<GroupingColumn>();
            _maxRowCount = dataMap.MaxRowCount;
            _cache = new Dictionary<CacheKey, DataMap>();

            foreach (var c in groupingColumnNames)
            {
                var keyColumn = new GroupingColumn(c, dataMap[c].Unique().UnderlyingList);
                _groupingColumns.Add(keyColumn);

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
                var keyColumn = _groupingColumns[i];
                if (!keyColumn.FilterSet.TryGetValue(categories[i], out var f))
                    Array.Clear(filter, 0, filter.Length);

                for (var j = 0; j < f.Length; ++j)
                    filter[j] &= f[j];
            }

            var dataMap = new DataMap();

            if (_selectColumnNames != null)
            {
                foreach (var c in _selectColumnNames)
                {
                    var filtered = FilteredListView.Create(_dataMap[c].UnderlyingList, filter);
                    dataMap.Add(c, filtered);
                }
            }
            else
            {
                foreach (var column in _dataMap)
                {
                    var filtered = FilteredListView.Create(column.Data.UnderlyingList, filter);
                    dataMap.Add(column.Name, filtered);
                }
            }

            _cache.Add(cacheKey, dataMap);

            return dataMap;
        }

        public IEnumerable<DataMap> Groups()
        {
            var counts = new int[_groupingColumns.Count];
            var categories = new object[_groupingColumns.Count];

            while (counts[0] < _groupingColumns[0].CategoryValues.Count)
            {
                for (var i = 0; i < categories.Length; ++i)
                    categories[i] = _groupingColumns[i].CategoryValues[counts[i]];

                yield return GetSubset(categories);

                for (var i = _groupingColumns.Count - 1; i >= 0; --i)
                {
                    ++counts[i];
                    if (counts[i] < _groupingColumns[i].CategoryValues.Count)
                        break;
                }
            }
        }

        public IEnumerator<DataMap> GetEnumerator()
        {
            return Groups().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public SeriesBase Summarize(ScriptBlock aggregator)
        {
            var result = new List<object>();
            var arguments = new object[1];

            foreach (var g in Groups())
            {
                arguments[0] = g;
                var value = aggregator.Invoke(arguments)[0];
                result.Add(value.BaseObject ?? value);
            }

            return new Series(result);
        }

        public DataMap Summarize<T>(string[] aggregationNames, ScriptBlock[] aggregators)
        {
            var result = new DataMap();

            foreach (var c in _groupingColumnNames)
                result.Add(c, new List<T>());

            foreach (var c in aggregationNames)
                result.Add(c, new List<T>());

            var variables = new List<PSVariable>();
            variables.Add(new PSVariable("group", null));
            var arguments = new object[1];
            foreach (var g in Groups())
            {
                foreach (var c in _groupingColumnNames)
                    result[c].Add(g[c][0]);

                for (var i = 0; i < aggregationNames.Length; ++i)
                {
                    var column = result[aggregationNames[i]];
                    variables[0].Value = g;
                    arguments[0] = g;
                    var values = aggregators[i].InvokeWithContext(null, variables, arguments);
                    if (values == null || values.Count == 0)
                        column.Add(null);
                    else
                        column.Add(values[0].BaseObject ?? values[0]);
                }
            }

            return result;
        }

        private DataMap SummarizeInternal<T>(
            string[] columnNames, string[] aggregationNames, object[] aggregators)
        {
            if (columnNames == null || columnNames.Length == 0)
                columnNames = _dataMap.ColumnNames.Except(_groupingColumnNames).ToArray();

            // Set up aggregators

            var re = new Regex(@"^\w+(\([^)]*\))?$");
            var funcs = new object[aggregationNames.Length];
            var scriptBlockGiven = false;
            for (var i = 0; i < aggregators.Length; ++i)
            {
                var agg = aggregators[i];
                if (agg is ScriptBlock sb)
                {
                    funcs[i] = agg;
                    scriptBlockGiven = true;
                }
                else if (agg is string st)
                {
                    var match = re.Match(st);
                    if (match.Success)
                    {
                        if (!match.Groups[1].Success)
                        {
                            // A bare word is given -- treats as a method name.
                            funcs[i] = st;
                            continue;
                        }
                        else
                        {
                            // We accept method name plus arguments for convinience.
                            st = "column => column." + st;
                        }
                    }

                    funcs[i] = (Func<SeriesBase, object>)FunctionCompiler.Compile(
                        st, new[] { typeof(SeriesBase), typeof(object) }, true, _dataMap, null);
                }
                else
                {
                    throw new ArgumentException("Aggregators should be a ScriptBlock or string");
                }
            }

            var result = new DataMap();

            foreach (var c in _groupingColumnNames)
                result.Add(c, new List<object>());

            foreach (var c in columnNames)
            {
                foreach (var a in aggregationNames)
                {
                    result.Add(a + "/" + c, new List<T>());
                }
            }

            List<PSVariable> variables = null;
            object[] arguments  = null;
            if (scriptBlockGiven)
            {
                variables = new List<PSVariable>();
                variables.Add(new PSVariable("column", null));
                arguments = new object[1];
            }

            foreach (var g in Groups())
            {
                foreach (var c in _groupingColumnNames)
                    result[c].Add(g[c][0]);

                foreach (var c in columnNames)
                {
                    for (var i = 0; i < aggregationNames.Length; ++i)
                    {
                        var column = g[c];
                        object value = null;
                        if (funcs[i] is ScriptBlock sb)
                        {
                            variables[0].Value = column;
                            arguments[0] = column;
                            var values = sb.InvokeWithContext(null, variables, arguments);
                            value = (values == null || values.Count == 0) ? null : (values[0].BaseObject ?? values[0]);
                        }
                        else if (funcs[i] is Func<SeriesBase, object> f)
                        {
                            value = f.Invoke(column);
                        }
                        else if (funcs[i] is string st)
                        {
                            var m = column.GetType().GetMethod(st, BindingFlags.Public | BindingFlags.Instance);
                            value = m.Invoke(column, new object[0]);
                        }

                        var name =  aggregationNames[i] + "/" + c;
                        result[name].Add(value);
                    }
                }
            }

            return result;
        }

        private Tuple<string[], object[]> ConvertToArrayPair(IDictionary aggregators)
        {
            var names = new string[aggregators.Count];
            var funcs = new object[aggregators.Count];

            int i = 0;
            foreach (DictionaryEntry entry in aggregators)
            {
                names[i] = (string)entry.Key;
                funcs[i] = entry.Value;
                ++i;
            }

            return Tuple.Create(names, funcs);
        }
/*
        public DataMap Summarize(IDictionary<string, object> aggregators)
        {
            var names = aggregators.Keys.ToArray();
            var funcs = aggregators.Values.ToArray();
            return Summarize<object>(names, funcs);
        }

        public DataMap Summarize(IDictionary aggregators)
        {
            var args = ConvertToArrayPair(aggregators);
            return Summarize<object>(args.Item1, args.Item2);
        }
*/

        public DataMap Summarize(string[] columnNames, IDictionary<string, object> aggregators)
        {
            var names = aggregators.Keys.ToArray();
            var scriptBlocks = aggregators.Values.ToArray();
            return SummarizeInternal<object>(columnNames, names, scriptBlocks);
        }

        public DataMap Summarize(string[] columnNames, IDictionary aggregators)
        {
            var args = ConvertToArrayPair(aggregators);
            return SummarizeInternal<object>(columnNames, args.Item1, args.Item2);
        }

        public DataMap Summarize(string[] columnNames, object[] aggregators)
        {
            var names = new string[aggregators.Length];
            var count = 1;
            for (var i = 0; i < aggregators.Length; ++i)
            {
                var agg = aggregators[i];
                if (agg is string st)
                    names[i] = st;
                else
                    names[i] = "expr" + count++;
            }
            return SummarizeInternal<object>(columnNames, names, aggregators);
        }
    }
}
