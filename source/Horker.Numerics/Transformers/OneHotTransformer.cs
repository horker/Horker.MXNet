using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps;

namespace Horker.Numerics.Transformers
{
    public enum OneHotType
    {
        OneHot,
        DropFirst,
        EffectCoding
    }

    public class OneHotTransformer<T> : SeriesTransformer
    {
        private OneHotType _oneHotType;
        private OrderedDictionary _encoding;
        private string _columnNameFormat;

        public OneHotTransformer(OneHotType oneHotType, string columnNameFormat = "{0}")
        {
            _oneHotType = oneHotType;
            _columnNameFormat = columnNameFormat ?? "{0}";
        }

        public override void Fit(SeriesBase data)
        {
            _encoding = new OrderedDictionary();

            var count = 0;
            foreach (var e in data.UnderlyingList)
            {
                var s = e.ToString();
                if (!_encoding.Contains(s))
                    _encoding.Add(s, count++);
            }
        }

        public override DataMap TransformToDataMap(SeriesBase data)
        {
            var listMap = new Dictionary<string, int[]>();
            var size = data.Count;

            string firstKey = null;
            foreach (DictionaryEntry entry in _encoding)
            {
                if (firstKey == null && _oneHotType != OneHotType.OneHot)
                {
                    firstKey = entry.Key as string;
                    continue;
                }

                listMap[entry.Key as string] = new int[size];
            }

            int i = 0;
            switch (_oneHotType)
            {
                case OneHotType.OneHot:
                    foreach (var e in data)
                    {
                        var name = e.ToString();
                        if (listMap.TryGetValue(name, out var list))
                            list[i] = 1;
                        ++i;
                    }
                    break;

                case OneHotType.DropFirst:
                    foreach (var e in data)
                    {
                        var name = e.ToString();
                        if (name != firstKey && listMap.TryGetValue(name, out var list))
                            list[i] = 1;
                        ++i;
                    }
                    break;

                case OneHotType.EffectCoding:
                    foreach (var e in data)
                    {
                        var name = e.ToString();
                        if (name != firstKey)
                        {
                            if (listMap.TryGetValue(name, out var list))
                                list[i] = 1;
                        }
                        else
                        {
                            foreach (var entry in listMap)
                                entry.Value[i] = -1;
                        }
                        ++i;
                    }
                    break;
            }

            var result = new DataMap();
            var keys = listMap.Keys.ToArray();
            Array.Sort(keys);
            foreach (var k in keys)
            {
                var columnName = string.Format(_columnNameFormat, k);
                result[columnName] = listMap[k];
            }

            return result;
        }
    }
}
