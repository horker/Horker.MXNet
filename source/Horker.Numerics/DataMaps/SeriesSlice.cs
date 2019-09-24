using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class SeriesSlice : SeriesBase
    {
        private IList _underlying;
        private int _start;
        private int _end;

        public int Start => _start;
        public int End => _end;

        public SeriesSlice(IList underlying, int start, int end)
        {
            _underlying = underlying;
            _start = start;
            _end = end;
        }
    }
}
