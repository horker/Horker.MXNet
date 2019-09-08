using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class OrderedMapEnumerator : IEnumerator<KeyValuePair<string, Column>>
    {
        private OrderedMap _map;
        private IEnumerator<Column> _e;

        public OrderedMapEnumerator(OrderedMap map)
        {
            _map = map;
            _e = map.Columns.GetEnumerator();
        }

        public KeyValuePair<string, Column> Current => new KeyValuePair<string, Column>(_e.Current.Name, _e.Current);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _e.Dispose();
        }

        public bool MoveNext()
        {
            return _e.MoveNext();
        }

        public void Reset()
        {
            _e.Reset();
        }
    }
}
