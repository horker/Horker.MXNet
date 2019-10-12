using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.Utilities;

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
                index = Utils.StripOffPSObject(index);
                return _sorted[index];
            }

            set
            {
                index = Utils.StripOffPSObject(index);
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
