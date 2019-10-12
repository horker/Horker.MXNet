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

        public virtual SeriesBase Transform(SeriesBase data)
        {
            throw new NotImplementedException();
        }

        public virtual DataMap TransformToDataMap(SeriesBase data)
        {
            throw new NotImplementedException();
        }

        public virtual SeriesBase FitTransform(SeriesBase data)
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
