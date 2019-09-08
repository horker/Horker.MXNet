using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class DataMapEnumerator : IEnumerator<Column>
    {
        private IEnumerator<Column> _e;

        public DataMapEnumerator(DataMap map)
        {
            _e = map.Columns.GetEnumerator();
        }

        public Column Current => _e.Current;

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
