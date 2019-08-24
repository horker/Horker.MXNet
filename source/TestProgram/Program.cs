using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
//            UnmanagedDllLoader.Load(@".\..\..\..\..\..\lib");
            Operator.LoadSymbolCreators();

            var version = Utilities.GetVersion();
            Console.WriteLine($"version: {version}");

            var a = NDArray.FromArray(new float[] { 1, 2, 3 }, new int[]{ 3, 1 });
            var b = NDArray.FromArray(new float[] { 4, 5, 6 }, new int[]{ 1, 3 });

            var c = Op.BroadcastAdd(a, b);

            var values = c.ToArray<float>();
            Console.WriteLine(string.Join(" ", values));

            Thread.Sleep(1000);

            a = NDArray.FromArray(new float[] { 1, 2, 3 }, new int[]{ 3 }, Context.Gpu(0));
            b = NDArray.FromArray(new float[] { 4, 5, 6 }, new int[]{ 3 }, Context.Gpu(0));

            c = Op.BroadcastAdd(a, b);

            values = c.ToArray<float>();
            Console.WriteLine(string.Join(" ", values));

            Console.Write("Push any key to exit");
            Console.ReadLine();
        }
    }
}
