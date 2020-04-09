using Horker.Numerics.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    [Serializable]
    public class Series : SeriesBase
    {
        private IList _underlying;

        public override IList UnderlyingList
        {
            get => _underlying;
            set => _underlying = value;
        }

        public Series(IList underlying)
        {
            while (underlying is SeriesBase s)
                underlying = s.UnderlyingList;

            _underlying = underlying;
        }

        public Series(Type dataType, int size, object value = null)
        {
            if (value == null)
            {
                var m = typeof(Series).GetMethod("CreateList", BindingFlags.NonPublic | BindingFlags.Static);
                var gm = m.MakeGenericMethod(new Type[] { dataType });
                _underlying = (IList)gm.Invoke(null, new object[] { size });

            }
            else
            {
                var m = typeof(Series).GetMethod("CreateListWithValue", BindingFlags.NonPublic | BindingFlags.Static);
                var gm = m.MakeGenericMethod(new Type[] { dataType });
                _underlying = (IList)gm.Invoke(null, new object[] { size, value });
            }
        }

        private static IList CreateList<T>(int size)
        {
            var list = new List<T>(size);
            for (var i = 0; i < size; ++i)
                list.Add(default(T));

            return list;
        }

        private static IList CreateListWithValue<T>(int size, T value)
        {
            var list = new List<T>(size);
            for (var i = 0; i < size; ++i)
                list.Add(value);

            return list;
        }

        public static Series CreateRandom<T>(int size, IRandom random = null)
        {
            var list = new List<T>(size);
            random ??= RandomInstance.Get();

            for (var i = 0; i < size; ++i)
            {
                var value = random.NextDouble();
                list.Add(SmartConverter.ConvertTo<T>(value));
            }

            return new Series(list);
        }

        // ISerializable implementation

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("underlying", _underlying);
        }

        public Series(SerializationInfo info, StreamingContext context)
        {
            _underlying = (IList)info.GetValue("underlying", typeof(IList));
        }
    }
}
