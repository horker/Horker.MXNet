using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            _underlying = underlying;
        }
    }
}
