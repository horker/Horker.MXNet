using System;
using System.Collections.Generic;
using System.IO;

namespace MxNetLib.OpGenerator
{
    public class Program
    {
        static List<MxOp> Load()
        {
            OpWrapperGenerator opWrapperGenerator = new OpWrapperGenerator();
            opWrapperGenerator.LoadMxOps();

            return opWrapperGenerator.Operators;
        }
    }
}
