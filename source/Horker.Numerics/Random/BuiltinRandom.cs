using System;

namespace Horker.Numerics.Random
{
    public class BuiltinRandom : IRandom
    {
        private System.Random _random;

        public BuiltinRandom()
        {
            _random = new System.Random();
        }

        public BuiltinRandom(long seed)
        {
            _random = new System.Random((int)seed);
        }

        public int Next()
        {
            return _random.Next();
        }

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public double NextDouble()
        {
            return _random.NextDouble();
        }

        public float NextFloat()
        {
            return (float)_random.NextDouble();
        }
    }
}
