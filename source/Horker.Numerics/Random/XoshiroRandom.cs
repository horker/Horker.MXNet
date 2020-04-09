using Xoshiro.PRNG64;

namespace Horker.Numerics.Random
{
    public class XoshiroRandom : IRandom
    {
        private XoShiRo256starstar _random;

        public XoshiroRandom()
        {
            _random = new XoShiRo256starstar();
        }

        public XoshiroRandom(long seed)
        {
            _random = new XoShiRo256starstar(seed);
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
            return _random.NextFloat();
        }
    }
}
