using System;
using System.Collections.Generic;
using System.IO;

namespace MxNetLib.OpGenerator
{
    public class Program
    {
        static public void Main(string[] args)
        {
            OpWrapperGenerator opWrapperGenerator = new OpWrapperGenerator();
            opWrapperGenerator.LoadMxOps();

            opWrapperGenerator.SaveJson("op.json");
        }
    }
}
