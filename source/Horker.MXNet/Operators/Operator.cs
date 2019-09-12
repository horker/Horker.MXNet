using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Horker.MXNet.Core;

namespace Horker.MXNet.Operators
{
    public class Operator
    {
        private static Dictionary<string, IntPtr> _creators;

        public static void LoadSymbolCreators()
        {
            lock (typeof(Operator))
            {
                if (_creators != null)
                    return;

                CApi.MXSymbolListAtomicSymbolCreators(out var size, out var array);
                var creators = IntPtrConverter.ToArray<IntPtr>(array, size);

                _creators = new Dictionary<string, IntPtr>();

                foreach (var c in creators)
                {
                    // You must call this method for all symbols before use.
                    // Otherwise, the process will crash in an unexpected manner.
                    CApi.MXSymbolGetAtomicSymbolInfo(
                        c,
                        out IntPtr namePtr,              // const char **
                        out IntPtr description,          // const char **
                        out int num_args,                // mx_uint *
                        out IntPtr arg_names,            // const char ***
                        out IntPtr arg_type_infos,       // const char ***
                        out IntPtr arg_descriptions,     // const char ***
                        out IntPtr key_var_num_args,     // const char **
                        out IntPtr return_type           // const char **
                    );
                    string name = Marshal.PtrToStringAnsi(namePtr);
                    _creators.Add(name, c);
                }
            }
        }

        private static IntPtr[] _zeroInputHandles = new IntPtr[0];

        public static NDArray Invoke(string name, string[] paramKeys, string[] paramValues, IntPtr[] inputHandles, NDArray output = null)
        {
            Debug.Assert(paramKeys.Length == paramValues.Length);

            var creatorHandle = _creators[name];

            // Prepare input handles.

            if (inputHandles == null)
                inputHandles = _zeroInputHandles;

            // Prepare output handles.
            // To avoid memeory allocation in MXNet's unmanaged code, prepare the buffer of the output values in our code.
            // MXNet thoughtfully uses this buffer.

            var outputHandles = new IntPtr[1];
            if (output == null)
            {
                CApi.MXNDArrayCreateNone(out var handle);
                outputHandles[0] = handle;
            }
            else
            {
                outputHandles[0] = output.Handle;
            }

            // Invoke the operator.

            using (var outputsPin = new ObjectPin(outputHandles))
            {
                var outputCount = outputHandles.Length;
                var outputAddress = outputsPin.Address;

                CApi.MXImperativeInvoke(
                    creatorHandle, inputHandles.Length, inputHandles, ref outputCount, ref outputAddress,
                    paramKeys.Length, paramKeys, paramValues);

#if DEBUG
                GC.Collect();
#endif

                Debug.Assert(outputAddress == outputsPin.Address);
            }

            return new NDArray(outputHandles[0]);
        }
    }
}
