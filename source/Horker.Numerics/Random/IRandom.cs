using System;
using System.Collections.Generic;
using System.Text;

namespace Horker.Numerics.Random
{
    public interface IRandom
    {
        int Next();
        int Next(int maximum);
        double NextDouble();
        float NextFloat();
    }
}
