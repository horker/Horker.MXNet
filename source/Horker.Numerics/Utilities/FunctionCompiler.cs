using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Horker.Numerics
{
    public class FunctionCompiler
    {
        private static readonly Dictionary<string, Delegate> CodeCache = new Dictionary<string, Delegate>();

        private static int classNameSuffix = 0;

        private static readonly string Namespace = "Horker.Numerics.DynamicallyGeneratedClasses";

        private static readonly string FuncSourceCode = @"
using System;
namespace {0} {{
    public static class {1}
    {{
        public static Func<{2}> f()
        {{
            return new Func<{3}>({4});
        }}
    }}
}}
";

        private static readonly string ActionSourceCode = @"
using System;
namespace {0} {{
    public static class {1}
    {{
        public static Action<{2}> f()
        {{
            return new Action<{3}>({4});
        }}
    }}
}}
";

        private static Assembly CompileString(string sourceString, string compilerVersion)
        {
            var provider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", compilerVersion } });

            var param = new CompilerParameters()
            {
                GenerateExecutable = false,
                CompilerOptions = "/optimize"
            };

            param.ReferencedAssemblies.Add("System.dll");
            param.ReferencedAssemblies.Add("System.Core.dll");

            var cr = provider.CompileAssemblyFromSource(param, sourceString);
            
            if(cr.Errors.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("Error on compiling: {0}", sourceString);
                sb.AppendLine();
                foreach (var ce in cr.Errors)
                {
                    sb.Append("  ");
                    sb.Append(ce.ToString());
                    sb.AppendLine();
                }

                throw new ArgumentException(sb.ToString());
            }

            return cr.CompiledAssembly;
        }

        public static Delegate Compile(string funcString, Type[] parameterTypes, bool func, string compilerVersion = "v4.0")
        {
            if (CodeCache.TryGetValue(funcString, out var f))
                return f;

            string sourceCode;
            if (func)
                sourceCode = FuncSourceCode;
            else
                sourceCode = ActionSourceCode;

            var className = "Func" + classNameSuffix++;
            var typeString = string.Join(", ", parameterTypes.Select(x => x.FullName));
            var sourceString = string.Format(sourceCode, Namespace, className, typeString, typeString, funcString);

            var assembly = CompileString(sourceString, "v3.5");

            var t = assembly.GetTypes().Where(c => c.Name == className).First();
            var m = t.GetMethod("f", BindingFlags.Static | BindingFlags.Public);

            return (Delegate)m.Invoke(null, new object[0]);
        }
    }
}
