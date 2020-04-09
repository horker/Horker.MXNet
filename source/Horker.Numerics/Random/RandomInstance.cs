using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Horker.Numerics.Random
{
    public static class RandomInstance
    {
        static ThreadLocal<IRandom> _instance = new ThreadLocal<IRandom>(() => new XoshiroRandom());

        public static IRandom Get() => _instance.Value;
        public static void Set(IRandom random) => _instance.Value = random;

        public static void SetSeed(int seed)
        {
            _instance.Value = new XoshiroRandom(seed);
        }
    }
}
