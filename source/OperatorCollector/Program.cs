using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args.Length > 0 ? args[0] : "ops.json";

            var ops = OperatorDefinition.Load().ToArray();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(ops);
            File.WriteAllText(file, json);
        }
    }
}
