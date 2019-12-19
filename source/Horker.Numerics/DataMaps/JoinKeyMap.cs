using Horker.Numerics.DataMaps.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class JoinKeyMap
    {
        private DataMap _dataMap;
        private string[] _keyColumns;
        private IDictionary _indexMap;

        public DataMap DataMap => _dataMap;
        public string[] KeyColumns => _keyColumns;
        public IDictionary IndexMap => _indexMap;

        public JoinKeyMap(DataMap dataMap, string[] keyColumns)
        {
            if (keyColumns.Length > 5)
                throw new ArgumentException("More than five key columns are not supported");

            _dataMap = dataMap;
            _keyColumns = keyColumns;
            _indexMap = GetJoinKeyMap();
        }

        private object InvokeIndirect(string baseMethodName, object[] arguments)
        {
            var dataTypes = new Type[_keyColumns.Length];

            for (var i = 0; i < _keyColumns.Length; ++i)
                dataTypes[i] = _dataMap[_keyColumns[i]].DataType;

            var methodName = baseMethodName + _keyColumns.Length;

            var m = typeof(JoinKeyMap).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            var gm = m.MakeGenericMethod(dataTypes);

            return gm.Invoke(this, arguments);
        }

        public IDictionary GetJoinKeyMap()
        {
            return (IDictionary)InvokeIndirect("CollectKeys", new object[0]);
        }

        private Dictionary<T1, int> CollectKeys1<T1>()
        {
            Debug.Assert(_keyColumns.Length == 1);

            var column1 = _dataMap[_keyColumns[0]].UnderlyingList;
            var rowCount = _dataMap.MaxRowCount;
            var result = new Dictionary<T1, int>(rowCount);
            for (var i = 0; i < rowCount; ++i)
            {
                T1 k1 = i >= column1.Count ? TypeTrait<T1>.GetNaN() : (T1)column1[i];

                result.Add(k1, i);
            }

            return result;
        }

        private Dictionary<Tuple<T1, T2>, int> CollectKeys2<T1, T2>()
        {
            Debug.Assert(_keyColumns.Length == 2);

            var column1 = _dataMap[_keyColumns[0]].UnderlyingList;
            var column2 = _dataMap[_keyColumns[1]].UnderlyingList;
            var rowCount = _dataMap.MaxRowCount;
            var result = new Dictionary<Tuple<T1, T2>, int>(rowCount);
            for (var i = 0; i < rowCount; ++i)
            {
                T1 k1 = i >= column1.Count ? TypeTrait<T1>.GetNaN() : (T1)column1[i];
                T2 k2 = i >= column2.Count ? TypeTrait<T2>.GetNaN() : (T2)column2[i];

                var key = Tuple.Create(k1, k2);
                try
                {
                    result.Add(key, i);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Duplicate key values: {key}", ex);
                }
            }

            return result;
        }

        private Dictionary<Tuple<T1, T2, T3>, int> CollectKeys3<T1, T2, T3>()
        {
            Debug.Assert(_keyColumns.Length == 3);

            var column1 = _dataMap[_keyColumns[0]].UnderlyingList;
            var column2 = _dataMap[_keyColumns[1]].UnderlyingList;
            var column3 = _dataMap[_keyColumns[2]].UnderlyingList;
            var rowCount = _dataMap.MaxRowCount;
            var result = new Dictionary<Tuple<T1, T2, T3>, int>(rowCount);
            for (var i = 0; i < rowCount; ++i)
            {
                T1 k1 = i >= column1.Count ? TypeTrait<T1>.GetNaN() : (T1)column1[i];
                T2 k2 = i >= column2.Count ? TypeTrait<T2>.GetNaN() : (T2)column2[i];
                T3 k3 = i >= column3.Count ? TypeTrait<T3>.GetNaN() : (T3)column3[i];

                var key = Tuple.Create(k1, k2, k3);
                try
                {
                    result.Add(key, i);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Duplicate key values: {key}", ex);
                }
            }

            return result;
        }

        private Dictionary<Tuple<T1, T2, T3, T4>, int> CollectKeys4<T1, T2, T3, T4>()
        {
            Debug.Assert(_keyColumns.Length == 4);

            var column1 = _dataMap[_keyColumns[0]].UnderlyingList;
            var column2 = _dataMap[_keyColumns[1]].UnderlyingList;
            var column3 = _dataMap[_keyColumns[2]].UnderlyingList;
            var column4 = _dataMap[_keyColumns[3]].UnderlyingList;
            var rowCount = _dataMap.MaxRowCount;
            var result = new Dictionary<Tuple<T1, T2, T3, T4>, int>(rowCount);
            for (var i = 0; i < rowCount; ++i)
            {
                T1 k1 = i >= column1.Count ? TypeTrait<T1>.GetNaN() : (T1)column1[i];
                T2 k2 = i >= column2.Count ? TypeTrait<T2>.GetNaN() : (T2)column2[i];
                T3 k3 = i >= column3.Count ? TypeTrait<T3>.GetNaN() : (T3)column3[i];
                T4 k4 = i >= column4.Count ? TypeTrait<T4>.GetNaN() : (T4)column4[i];

                var key = Tuple.Create(k1, k2, k3, k4);
                try
                {
                    result.Add(key, i);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Duplicate key values: {key}", ex);
                }
            }

            return result;
        }

        private Dictionary<Tuple<T1, T2, T3, T4, T5>, int> CollectKeys5<T1, T2, T3, T4, T5>()
        {
            Debug.Assert(_keyColumns.Length == 5);

            var column1 = _dataMap[_keyColumns[0]].UnderlyingList;
            var column2 = _dataMap[_keyColumns[1]].UnderlyingList;
            var column3 = _dataMap[_keyColumns[2]].UnderlyingList;
            var column4 = _dataMap[_keyColumns[3]].UnderlyingList;
            var column5 = _dataMap[_keyColumns[4]].UnderlyingList;
            var rowCount = _dataMap.MaxRowCount;
            var result = new Dictionary<Tuple<T1, T2, T3, T4, T5>, int>(rowCount);
            for (var i = 0; i < rowCount; ++i)
            {
                T1 k1 = i >= column1.Count ? TypeTrait<T1>.GetNaN() : (T1)column1[i];
                T2 k2 = i >= column2.Count ? TypeTrait<T2>.GetNaN() : (T2)column2[i];
                T3 k3 = i >= column3.Count ? TypeTrait<T3>.GetNaN() : (T3)column3[i];
                T4 k4 = i >= column4.Count ? TypeTrait<T4>.GetNaN() : (T4)column4[i];
                T5 k5 = i >= column5.Count ? TypeTrait<T5>.GetNaN() : (T5)column5[i];

                var key = Tuple.Create(k1, k2, k3, k4, k5);
                try
                {
                    result.Add(key, i);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Duplicate key values: {key}", ex);
                }
            }

            return result;
        }

        public int[] GetMatchingIndexes(DataMap left, string[] leftKeyColumns)
        {
            if (leftKeyColumns.Length != _keyColumns.Length)
                throw new ArgumentException("Invalid number of key columns");

            return (int[])InvokeIndirect("GetMatchingIndexes", new object[] { left, leftKeyColumns });
        }

        private int[] GetMatchingIndexes1<T1>(DataMap left, string[] leftKeyColumns)
        {
            if (leftKeyColumns == null)
                leftKeyColumns = _keyColumns;

            var result = new int[left.MaxRowCount];
            for (var i = 0; i < result.Length; ++i)
                result[i] = -1;

            var indexMap = (Dictionary<T1, int>)_indexMap;
            var column1 = (IList<T1>)left[leftKeyColumns[0]].UnderlyingList;

            for (var i = 0; i < result.Length; ++i)
            {
                if (indexMap.TryGetValue(column1[i], out var index))
                    result[i] = index;
            }

            return result;
        }

        private int[] GetMatchingIndexes2<T1, T2>(DataMap left, string[] leftKeyColumns)
        {
            if (leftKeyColumns == null)
                leftKeyColumns = _keyColumns;

            var result = new int[left.MaxRowCount];
            for (var i = 0; i < result.Length; ++i)
                result[i] = -1;

            var indexMap = (Dictionary<Tuple<T1, T2>, int>)_indexMap;
            var column1 = (IList<T1>)left[leftKeyColumns[0]].UnderlyingList;
            var column2 = (IList<T2>)left[leftKeyColumns[1]].UnderlyingList;

            for (var i = 0; i < result.Length; ++i)
            {
                var key = Tuple.Create(column1[i], column2[i]);
                if (indexMap.TryGetValue(key, out var index))
                    result[i] = index;
            }

            return result;
        }

        private int[] GetMatchingIndexes3<T1, T2, T3>(DataMap left, string[] leftKeyColumns)
        {
            if (leftKeyColumns == null)
                leftKeyColumns = _keyColumns;

            var result = new int[left.MaxRowCount];
            for (var i = 0; i < result.Length; ++i)
                result[i] = -1;

            var indexMap = (Dictionary<Tuple<T1, T2, T3>, int>)_indexMap;
            var column1 = (IList<T1>)left[leftKeyColumns[0]].UnderlyingList;
            var column2 = (IList<T2>)left[leftKeyColumns[1]].UnderlyingList;
            var column3 = (IList<T3>)left[leftKeyColumns[2]].UnderlyingList;

            for (var i = 0; i < result.Length; ++i)
            {
                var key = Tuple.Create(column1[i], column2[i], column3[i]);
                if (indexMap.TryGetValue(key, out var index))
                    result[i] = index;
            }

            return result;
        }

        private int[] GetMatchingIndexes4<T1, T2, T3, T4>(DataMap left, string[] leftKeyColumns)
        {
            if (leftKeyColumns == null)
                leftKeyColumns = _keyColumns;

            var result = new int[left.MaxRowCount];
            for (var i = 0; i < result.Length; ++i)
                result[i] = -1;

            var indexMap = (Dictionary<Tuple<T1, T2, T3, T4>, int>)_indexMap;
            var column1 = (IList<T1>)left[leftKeyColumns[0]].UnderlyingList;
            var column2 = (IList<T2>)left[leftKeyColumns[1]].UnderlyingList;
            var column3 = (IList<T3>)left[leftKeyColumns[2]].UnderlyingList;
            var column4 = (IList<T4>)left[leftKeyColumns[3]].UnderlyingList;

            for (var i = 0; i < result.Length; ++i)
            {
                var key = Tuple.Create(column1[i], column2[i], column3[i], column4[i]);
                if (indexMap.TryGetValue(key, out var index))
                    result[i] = index;
            }

            return result;
        }

        private int[] GetMatchingIndexes4<T1, T2, T3, T4, T5>(DataMap left, string[] leftKeyColumns)
        {
            if (leftKeyColumns == null)
                leftKeyColumns = _keyColumns;

            var result = new int[left.MaxRowCount];
            for (var i = 0; i < result.Length; ++i)
                result[i] = -1;

            var indexMap = (Dictionary<Tuple<T1, T2, T3, T4, T5>, int>)_indexMap;
            var column1 = (IList<T1>)left[leftKeyColumns[0]].UnderlyingList;
            var column2 = (IList<T2>)left[leftKeyColumns[1]].UnderlyingList;
            var column3 = (IList<T3>)left[leftKeyColumns[2]].UnderlyingList;
            var column4 = (IList<T4>)left[leftKeyColumns[3]].UnderlyingList;
            var column5 = (IList<T5>)left[leftKeyColumns[4]].UnderlyingList;

            for (var i = 0; i < result.Length; ++i)
            {
                var key = Tuple.Create(column1[i], column2[i], column3[i], column4[i], column5[i]);
                if (indexMap.TryGetValue(key, out var index))
                    result[i] = index;
            }

            return result;
        }
    }
}
