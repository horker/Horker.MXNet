using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps;

namespace Horker.Numerics.Transformers
{
    public abstract class SeriesTransformer
    {
        public virtual DataMap Parameters { get => null; set { } }

        public abstract void Fit(SeriesBase data);
        public abstract Series Transform(SeriesBase data);
        public abstract DataMap TransformToDataMap(SeriesBase data);

        public virtual Series FitTransform(SeriesBase data)
        {
            Fit(data);
            return Transform(data);
        }

        public virtual DataMap FitTransformToDataMap(SeriesBase data)
        {
            Fit(data);
            return TransformToDataMap(data);
        }
    }
}
