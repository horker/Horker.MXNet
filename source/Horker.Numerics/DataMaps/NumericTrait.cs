using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    public class NumericTrait<T>
    {
        public static readonly T NaN = (T)GetNaN();

        private static object GetNaN()
        {
            if (typeof(T) == typeof(double))
                return double.NaN;

            if (typeof(T) == typeof(float))
                return float.NaN;

            return 0;
        }
    }
}
