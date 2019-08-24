using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    public class UnmanagedDllLoader
    {
        // The DLLs will be loaded in this order.
        private static readonly string[] Files = new string[] {
            // MKL
            "libiomp5md.dll",
            "mklml.dll",
            "mkldnn.dll",

            // OpenBLAS
            "libgcc_s_seh-1.dll",
            "libgfortran-3.dll",
            "libopenblas.dll",
            "libquadmath-0.dll",

            // CuDNN (no dep.)
            "cudnn64_7.dll",

            // MXNet
            "libmxnet.dll"
        };

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        private static bool loaded = false;

        public static void Load(string dllPath)
        {
            if (loaded)
                return;

            foreach (var file in Files)
            {
                var path = Path.Combine(dllPath, file);
                var result = LoadLibrary(path);
                if (result == IntPtr.Zero)
                    throw new InvalidOperationException($"Failed to load: {path}");
            }

            loaded = true;
        }
    }
}