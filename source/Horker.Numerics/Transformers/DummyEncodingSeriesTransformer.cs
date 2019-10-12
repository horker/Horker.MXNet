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
    public class DummyEncodingSeriesTransformer<T> : SeriesTransformer
    {
        private bool _useFallback;
        private T _fallbackValue;

        private Dictionary<object, T> _encoding;

        public DummyEncodingSeriesTransformer(bool useFallback = false, T fallbackValue = default(T))
        {
            _useFallback = useFallback;
            _fallbackValue = fallbackValue;
        }

        public override void Fit(SeriesBase data)
        {
            _encoding = new Dictionary<object, T>();

            var count = 0;
            foreach (var value in data.UnderlyingList)
            {
                if (!_encoding.ContainsKey(value))
                    _encoding.Add(value, SmartConverter.ConvertTo<T>(count));
                ++count;
            }
        }

        public override SeriesBase Transform(SeriesBase data)
        {
            var result = new Series(typeof(double), data.Count, _fallbackValue);

            for (var i = 0; i < result.Count; ++i)
            {
                if (_encoding.TryGetValue(data[i], out var value))
                    result[i] = value;
                else
                {
                    if (!_useFallback)
                        throw new ArgumentException($"Unknown value: {data[i]}");
                }
            }

            return result;
        }
    }
}
