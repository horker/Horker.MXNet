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
    public class NDArray : NDArrayOrSymbol
    {
        private NDShape _shape = null;
        private long _size = -1;
        private DType _dtype = null;
        private Context _context = null;

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

        public T[] ToArray<T>()
            where T: new()
        {
            if (typeof(T) != DType.RuntimeType)
                throw new ArgumentException("T must be {_dtyhpe.Runtime.FullName} for this NDArray");

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
    }
}
