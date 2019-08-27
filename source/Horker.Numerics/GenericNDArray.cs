using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics
{
    public class GenericNDArray<T> : NDArray<T>
    {
        private T[] _data;
        private int[] _shape;
        private long _size = -1;

        public override int[] Shape => _shape;
        public override long Size
        {
            get
            {
                if (_size < 0)
                    _size = _shape.Aggregate((x, sum) => x * sum);
                return _size;
            }
        }

        // This method does not copy data and shape.
        private GenericNDArray(T[] data, int[] shape)
        {
            _data = data;
            _shape = shape;
        }

        public override T[] ToArray()
        {
            return _data;
        }

        public static GenericNDArray<T> Create(T[] data, int[] shape)
        {
            if (shape == null)
                shape = new int[1] { data.Length };

            return new GenericNDArray<T>(data.ToArray(), shape.ToArray());
        }

    }
}
