using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Core;

namespace CodeGenerator
{
    class OperatorDefinition
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int NumArgs { get; private set; }
        public string[] ArgNames { get; private set; }
        public string[] ArgTypeInfos { get; private set; }
        public string[] ArgDescriptions { get; private set; }
        public string KeyVarNumArgs { get; private set; }
        public string ReturnType { get; private set; }

        public static IntPtr[] ConvertToIntPtrArray(IntPtr p, int size)
        {
            var result = new IntPtr[size];
            if (size == 0)
                return result;

            Marshal.Copy(p, result, 0, size);
            return result;
        }

        public static string[] ConvertToStringArray(IntPtr p, int size)
        {
            if (size == 0)
                return new string[0];

            var a = ConvertToIntPtrArray(p, size);

            var result = new string[a.Length];

            for (var i = 0; i < a.Length; ++i)
                result[i] = Marshal.PtrToStringAnsi(a[i]);

            return result;
        }

        public static IEnumerable<OperatorDefinition> Load()
        {
            CApiDeclaration.MXSymbolListAtomicSymbolCreators(out var size, out var array);
            var creators = ConvertToIntPtrArray(array, size);

            foreach (var c in creators)
            {
                CApiDeclaration.MXSymbolGetAtomicSymbolInfo(
                    c,
                    out IntPtr name,                 // const char **
                    out IntPtr description,          // const char **
                    out int num_args,                // mx_uint *
                    out IntPtr arg_names,            // const char ***
                    out IntPtr arg_type_infos,       // const char ***
                    out IntPtr arg_descriptions,     // const char ***
                    out IntPtr key_var_num_args,     // const char **
                    out IntPtr return_type           // const char **
                );

                var op = new OperatorDefinition()
                {
                    Name = Marshal.PtrToStringAnsi(name),
                    Description = Marshal.PtrToStringAnsi(description),
                    NumArgs = num_args,
                    ArgNames = ConvertToStringArray(arg_names, num_args),
                    ArgTypeInfos = ConvertToStringArray(arg_type_infos, num_args),
                    ArgDescriptions = ConvertToStringArray(arg_descriptions, num_args),
                    KeyVarNumArgs = Marshal.PtrToStringAnsi(key_var_num_args),
                    ReturnType = Marshal.PtrToStringAnsi(return_type)
                };

                yield return op;
            }
        }
    }
}
