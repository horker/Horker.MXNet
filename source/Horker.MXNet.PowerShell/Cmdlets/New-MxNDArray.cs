using MxNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http.Headers;

namespace Horker.MxNet.PowerShell
{
    [Cmdlet("New", "MxNDArray")]
    [Alias("mx.NDArray")]
    [OutputType(typeof(NDArray))]
    public class NewMxNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Array Values = new float[0];

        [Parameter(Position = 1, Mandatory = false)]
        public DType DType = DType.Float32;

        [Parameter(Position = 2, Mandatory = false)]
        public int[] Shape = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Context Context = null;

        protected override void BeginProcessing()
        {
            // Find data shape

            var actualShape = new List<int>();
            Array v = Values;
            while (true)
            {
                actualShape.Add(v.Length);
                var vv = v.GetValue(0);
                if (!(vv is Array a) || a.Length == 0)
                    break;
                v = a;
            }

            var size = actualShape.Aggregate(1, (x, y) => x * y);

            if (Shape != null)
            {
                var reshapeSize = Shape.Aggregate(1, (x, y) => x * y);
                if (size != reshapeSize)
                {
                    WriteError(new ErrorRecord(new ArgumentException($"Invalid shape specified; actual data size is {size} ({string.Join("x", actualShape)})"), "", ErrorCategory.InvalidArgument, null));
                    return;
                }
            }

            Array data = null;
            if (DType == DType.Float64)
                data = CopyJaggedArray<double>(Values, size);
            else if (DType == DType.Float32)
                data = CopyJaggedArray<float>(Values, size);
            else if (DType == DType.Int64)
                data = CopyJaggedArray<long>(Values, size);
            else if (DType == DType.Int32)
                data = CopyJaggedArray<int>(Values, size);
            else if (DType == DType.Int8)
                data = CopyJaggedArray<byte>(Values, size);
            else if (DType == DType.UInt8)
                data = CopyJaggedArray<sbyte>(Values, size);
            else
            {
                WriteError(new ErrorRecord(new ArgumentException($"Unsupported type: {DType}"), "", ErrorCategory.InvalidType, null));
                return;
            }

            NDArray result = null;
            if (Shape != null)
                result = new NDArray(data, new Shape(Shape), Context, DType);
            else
                result = new NDArray(data, new Shape(actualShape.ToArray()), Context, DType);

            WriteObject(result);
        }

        private Array CopyJaggedArray<T>(Array data, int size)
        {
            var result = new T[size];

            CopyJaggedArrayInternal((dynamic)data, result, 0);

            return result;
        }

        private int CopyJaggedArrayInternal<T, U>(T[] source, U[] dest, int offset)
        {
            var firstItem = source[0];
            if (firstItem is Array fa)
            {
                var length = fa.Length;
                foreach (var value in source)
                {
                    if (!(value is Array a) || a.Length != length)
                        throw new ApplicationException("Invalid shape of array");

                    offset = CopyJaggedArrayInternal((dynamic)a, dest, offset);
                }
                return offset;
            }
            else
            {
                if (firstItem.GetType() == typeof(T))
                {
                    Array.Copy(source, 0, dest, offset, source.Length);
                }
                else
                {
                    for (var i = 0; i < source.Length; ++i)
                        dest[i + offset] = (U)Convert.ChangeType(source[i], typeof(U));
                }
                return offset + source.Length;
            }
        }
    }
}
