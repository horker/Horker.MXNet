using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class SortedListIndexSeries : SeriesBase
    {
        private SortedList _sorted;

        public override IList UnderlyingList => _sorted.GetValueList();

        public override object this[object index]
        {
            get
            {
                if (index is PSObject pso)
                    index = pso.BaseObject;
                
                return _sorted[index];
            }

            set
            {
                if (index is PSObject pso)
                    index = pso.BaseObject;
                
                _sorted[index] = value;
            }
        }

        public SortedListIndexSeries(IList l)
        {
            _sorted = new SortedList();

            foreach (var e in l)
                _sorted.Add(e, e);
        }
    }
}
