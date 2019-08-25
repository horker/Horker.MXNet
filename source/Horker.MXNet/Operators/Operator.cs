﻿using System;
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
            if (_creators != null)
                return;

            CApi.MXSymbolListAtomicSymbolCreators(out var size, out var array);
            var creators = IntPtrConverter.ToArray<IntPtr>(array, size);

            _creators = new Dictionary<string, IntPtr>();

            foreach (var c in creators)
            {
                // You must call this method for all symbols before use.
                CApi.MXSymbolGetAtomicSymbolInfo(
                    c,
                    out IntPtr namePtr,                 // const char **
                    out IntPtr description,          // const char **
                    out int num_args,             // mx_uint *
                    out IntPtr arg_names,            // const char ***
                    out IntPtr arg_type_infos,       // const char ***
                    out IntPtr arg_descriptions,     // const char ***
                    out IntPtr key_var_num_args,     // const char **
                    out IntPtr return_type           // const char **
                );
                string name = Marshal.PtrToStringAnsi(namePtr);
//                CApi.MXSymbolGetAtomicSymbolName(c, out var name);
                _creators.Add(name, c);
            }
        }

        private static IntPtr[] _zeroInputHandles = new IntPtr[0];

        public static NDArray[] Invoke(string name, string[] paramKeys, string[] paramValues, NDArrayOrSymbol[] inputs = null, int outputCount = 1)
        {
            Debug.Assert(paramKeys.Length == paramValues.Length);

            var handle = _creators[name];

            // Prepare input handles.

            IntPtr[] inputHandles;
            if (inputs == null)
                inputHandles = _zeroInputHandles;
            else
            {
                inputHandles = new IntPtr[inputs.Length];
                for (var i = 0; i < inputs.Length; ++i)
                    inputHandles[i] = inputs[i].Handle;
            }

            // Prepare output handles.
            // To avoid memeory allocation in MXNet's unmanaged code, prepare the buffer of the output values in our code.
            // MXNet thoughtfully uses this buffer.

            var outputs = new NDArray[outputCount];
            for (var i = 0; i < outputCount; ++i)
                outputs[i] = NDArray.CreateNone();

            var outputHandles = new IntPtr[outputCount];
            for (var i = 0; i < outputCount; ++i)
                outputHandles[i] = outputs[i].Handle;

            // Invoke the operator.
            // (Not sure that inputsPin is necessary, but behave defensive.)

            using (var outputsPin = new ObjectPin(outputHandles))
            {
                var outputCount2 = outputCount;
                var outputAddress = outputsPin.Address;

                CApi.MXImperativeInvoke(
                    handle, inputHandles.Length, inputHandles, ref outputCount2, ref outputAddress,
                    paramKeys.Length, paramKeys, paramValues);

#if DEBUG
                GC.Collect();
#endif

                Debug.Assert(outputCount2 == outputCount);
                Debug.Assert(outputAddress == outputsPin.Address);
            }

            return outputs;
        }
    }
}
