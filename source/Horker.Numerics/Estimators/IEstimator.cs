using Horker.Numerics.DataMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.Estimators
{
    public interface IEstimator
    {
        DataMap Parameters { get; set; }

        void Fit(DataMap x, DataMap y);
        DataMap Predict(DataMap x);
        double Score(DataMap x, DataMap y);
    }
}
