using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class Series : SeriesBase
    {
        private IList _underlying;

        public override IList UnderlyingList => _underlying;

        public Series(IList underlying)
        {
            while (underlying is SeriesBase s)
                underlying = s.UnderlyingList;

            _underlying = underlying;
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
    }
}
