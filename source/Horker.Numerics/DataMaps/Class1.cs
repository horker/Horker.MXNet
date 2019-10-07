using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class GroupBy
    {
        private Dictionary<List<string>, SeriesBase> _seriesSet;

        private IList<string> _keys;
        private IList<string> _columns;

        public GroupBy(DataMap map, IList<string> keys, IList<string> columns)
        {
            _keys = keys;
            _columns = columns;

            

        }
    }
}
