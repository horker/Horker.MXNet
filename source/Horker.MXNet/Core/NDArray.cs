using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Operators;

namespace Horker.MXNet.Core
{
    /// <summary>
    /// This class is equivalent to the python API's NDArray.
    /// </summary>
    public partial class NDArray : NDArrayOrSymbol
    {
        private NDShape _shape = null;
        private long _size = -1;
        private DType _dtype = null;
        private Context _context = null;

        // Constructors and factory methods

        public NDArray(IntPtr handle)
        {
            Handle = handle;
        }

        public NDArray(IntPtr handle, int[] shape)
        {
            Handle = handle;
            _shape = shape;
        }

        public static NDArray CreateNone()
        {
            CApi.MXNDArrayCreateNone(out var result);
            return new NDArray(result);
        }

        public static NDArray Create(int[] shape, Context ctx = null, DType type = null, bool delayAlloc = true)
        {
            ctx = ctx ?? Context.DefaultContext;
            type = type ?? DType.DefaultDType;

            CApi.MXNDArrayCreateEx(shape, shape.Length, (int)ctx.DeviceType, ctx.DeviceId, delayAlloc ? 1 : 0, type, out var handle);

            return new NDArray(handle, shape);
        }

        public static NDArray FromArray<T>(T[] data, int[] shape = null, Context ctx = null)
        {
            shape = shape ?? new int[] { data.Length };
            ctx = ctx ?? Context.DefaultContext;
            var dtype = DType.FromType(typeof(T));

            CApi.MXNDArrayCreateEx(shape, shape.Length, (int)ctx.DeviceType, ctx.DeviceId, 1, dtype, out var handle);

            using (var dataPin = new ObjectPin(data))
            {
                CApi.MXNDArraySyncCopyFromCPU(handle, dataPin.Address, data.Length);
#if DEBUG
                GC.Collect();
#endif
            }

            return new NDArray(handle, shape);
        }

        public static NDArray Zeros(NDShape shape, Context ctx = null, DType type = null)
        {
            ctx = ctx ?? Context.DefaultContext;
            type = type ?? DType.DefaultDType;

            var results = Operator.Invoke("_zeros",
                new string[] { "shape", "ctx", "dtype" },
                new string[] { shape, ctx, type });

            return results[0];
        }

        public static NDArray ZerosLike(NDArray array, Context ctx = null)
        {
            ctx = ctx ?? Context.DefaultContext;

            var results = Operator.Invoke("_zeros",
                new string[] { "shape", "ctx", "dtype" },
                new string[] { array.Shape, ctx, array.DType });

            return results[0];
        }

        public static NDArray Ones(NDShape shape, Context ctx = null, DType type = null)
        {
            ctx = ctx ?? Context.DefaultContext;
            type = type ?? DType.DefaultDType;

            var results = Operator.Invoke("_ones",
                new string[] { "shape", "ctx", "dtype" },
                new string[] { shape, ctx, type });

            return results[0];
        }

        public static NDArray OnesLike(NDArray array, Context ctx = null)
        {
            ctx = ctx ?? Context.DefaultContext;

            var results = Operator.Invoke("_ones",
                new string[] { "shape", "ctx", "dtype" },
                new string[] { array.Shape, ctx, array.DType });

            return results[0];
        }

        // Properties

        public NDShape Shape
        {
            get
            {
                if (_shape == null)
                {
                    CApi.MXNDArrayGetShapeEx(Handle, out var dim, out var data);
                    _shape = new NDShape(data, dim);
                }

                return _shape;
            }
        }

        public long Size
        {
            get
            {
                if (_size < 0)
                {
                    var shape = Shape;
                    _size = 1;
                    foreach (var s in shape.Dimensions)
                        _size *= s;
                }

                return _size;
            }
        }

        public DType DType
        {
            get
            {
                if (_dtype == null)
                {
                    CApi.MXNDArrayGetDType(Handle, out var dtype);
                    _dtype = new DType((DTypeEnum)dtype);
                }

                return _dtype;
            }
        }

        public Context Context
        {
            get
            {
                if (_context == null)
                {
                    CApi.MXNDArrayGetContext(Handle, out var deviceType, out var deviceId);
                    _context = new Context((DeviceType)deviceType, deviceId);
                }

                return _context;
            }
        }

        public double this[params int[] shape]
        {
            get
            {
                return Op.Slice(this, shape, ShapeHelpers.GetAdjacent(shape)).Cast(DType.Float64).ToArray<double>()[0];
            }
        }

        public NDArray this[int[] begin, int[] end, int[] step = null]
        {
            get
            {
                return Op.Slice(this, begin, end);
            }
        }

        // Override methods

        public override string ToString()
        {
            return NDArrayExtensions.ToStringInShortFormat(this, true);
        }

        public override int GetHashCode()
        {
            return (int)Handle;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        // Other methods

        public T[] ToArray<T>()
        {
            if (typeof(T) != DType.RuntimeType)
                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            var size = Size;
            var result = new T[size];
            using (var pin = new ObjectPin(result))
            {
                CApi.MXNDArraySyncCopyToCPU(Handle, pin.Address, size);
#if DEBUG
                GC.Collect();
#endif
            }

            return result;
        }

        public T[,] To2DArray<T>()
        {
            if (typeof(T) != DType.RuntimeType)
                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (Shape.NDimensions != 2)
                throw new InvalidOperationException("NDArray is not 2-dimensional");

            var size = Size;
            var result = new T[Shape[0], Shape[1]];
            using (var pin = new ObjectPin(result))
            {
                CApi.MXNDArraySyncCopyToCPU(Handle, pin.Address, size);
            }

            return result;
        }

        public T[,,] To3DArray<T>()
        {
            if (typeof(T) != DType.RuntimeType)
                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (Shape.NDimensions != 3)
                throw new InvalidOperationException("NDArray is not 2-dimensional");

            var size = Size;
            var result = new T[Shape[0], Shape[1], Shape[2]];
            using (var pin = new ObjectPin(result))
            {
                CApi.MXNDArraySyncCopyToCPU(Handle, pin.Address, size);
            }

            return result;
        }

        public T[,,,] To4DArray<T>()
        {
            if (typeof(T) != DType.RuntimeType)
                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (Shape.NDimensions != 4)
                throw new InvalidOperationException("NDArray is not 2-dimensional");

            var size = Size;
            var result = new T[Shape[0], Shape[1], Shape[2], Shape[4]];
            using (var pin = new ObjectPin(result))
            {
                CApi.MXNDArraySyncCopyToCPU(Handle, pin.Address, size);
            }

            return result;
        }

        public T[][] To2DJagged<T>()
        {
            if (typeof(T) != DType.RuntimeType)
                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (Shape.NDimensions != 2)
                throw new InvalidOperationException("NDArray is not 2-dimensional");

            var size = Size;
            var s0 = Shape[0];
            var s1 = Shape[1];

            var data = To2DArray<T>();
            var result = new T[s0][];

            for (var i0 = 0; i0 < result.Length; ++i0)
            {
                result[i0] = new T[s1];
                for (var i1 = 0; i1 < result[i0].Length; ++i1)
                {
                    result[i0][i1] = data[i0, i1];
                }
            }

            return result;
        }

        public T[][][] To3DJagged<T>()
        {
            if (typeof(T) != DType.RuntimeType)
                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (Shape.NDimensions != 3)
                throw new InvalidOperationException("NDArray is not 3-dimensional");

            var size = Size;
            var s0 = Shape[0];
            var s1 = Shape[1];
            var s2 = Shape[2];

            var data = To3DArray<T>();
            var result = new T[s0][][];

            for (var i0 = 0; i0 < result.Length; ++i0)
            {
                result[i0] = new T[s1][];
                for (var i1 = 0; i1 < result[i0].Length; ++i1)
                {
                    result[i0][i1] = new T[s2];
                    for (var i2 = 0; i2 < result[i0][i1].Length; ++i1)
                    {
                        result[i0][i1][i2] = data[i0, i1, i2];
                    }
                }
            }

            return result;
        }

        public T[][][][] To4DJagged<T>()
        {
            if (typeof(T) != DType.RuntimeType)
                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (Shape.NDimensions != 4)
                throw new InvalidOperationException("NDArray is not 4-dimensional");

            var size = Size;
            var s0 = Shape[0];
            var s1 = Shape[1];
            var s2 = Shape[2];
            var s3 = Shape[3];

            var data = To4DArray<T>();
            var result = new T[s0][][][];

            for (var i0 = 0; i0 < result.Length; ++i0)
            {
                result[i0] = new T[s1][][];
                for (var i1 = 0; i1 < result[i0].Length; ++i1)
                {
                    result[i0][i1] = new T[s2][];
                    for (var i2 = 0; i2 < result[i0][i1].Length; ++i1)
                    {
                        result[i0][i1][i2] = new T[s3];
                        for (var i3 = 0; i3 < result[i0][i1][i2].Length; ++i1)
                        {
                            result[i0][i1][i2][i3] = data[i0, i1, i2, i3];
                        }
                    }
                }
            }

            return result;
        }
    }
}
