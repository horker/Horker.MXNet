using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.LightGBM
{
    public class UnmanagedDllLoader
    {
        private static readonly string[] Files = new string[] {
            @"x64\lib_lightgbm.dll"
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