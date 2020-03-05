using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using Horker.Numerics.DataMaps;
using System.Management.Automation;

namespace Horker.Numerics
{
    public static class FunctionCompilerCodeDom
    {
        private static readonly Dictionary<string, MethodInfo> _codeCache = new Dictionary<string, MethodInfo>();

        private static int classNameSuffix = 0;

        private static readonly string Namespace = "Horker.Numerics.DynamicallyGeneratedClasses";

        private static readonly string FuncSourceCode = @"
using System;
using System.Linq;
using Horker.Numerics;
using Horker.Numerics.DataMaps;
using Horker.Numerics.DataMaps.Extensions;
namespace {0} {{
    public static class {1}
    {{
        public static Func<{2}> f(DataMap dataMap, SeriesBase column)
        {{
            return new Func<{3}>({4});
        }}
    }}
}}
";

        private static readonly string ActionSourceCode = @"
using System;
using System.Linq;
using Horker.Numerics;
using Horker.Numerics.DataMaps;
using Horker.Numerics.DataMaps.Extensions;
namespace {0} {{
    public static class {1}
    {{
        public static Action<{2}> f(DataMap dataMap, SeriesBase column)
        {{
            return new Action<{3}>({4});
        }}
    }}
}}
";

        private static Assembly CompileString(string sourceString, string compilerVersion)
        {
            var provider = new CSharpCodeProvider( new Dictionary<string, string> { { "CompilerVersion", compilerVersion } });

            var param = new CompilerParameters()
            {
                GenerateExecutable = false,
                CompilerOptions = "/optimize"
            };

            param.ReferencedAssemblies.Add("System.dll");
            param.ReferencedAssemblies.Add("System.Core.dll");
            param.ReferencedAssemblies.Add("mscorlib.dll");
            param.ReferencedAssemblies.Add(typeof(PowerShell).Assembly.Location);
            param.ReferencedAssemblies.Add(typeof(FunctionCompiler).Assembly.Location);

            var cr = provider.CompileAssemblyFromSource(param, sourceString);
            
            if(cr.Errors.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("Error when compiling: {0}", sourceString);
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

        public static Delegate Compile(string funcString, Type[] parameterTypes, bool func, DataMap dataMap = null, SeriesBase column = null, string compilerVersion = "v4.0")
        {
            var typeString = string.Join(", ", parameterTypes.Select(x => x.FullName));

            lock (_codeCache)
            {
                if (!_codeCache.TryGetValue(funcString + "$" + typeString, out var m))
                {
                    string sourceCode;
                    if (func)
                        sourceCode = FuncSourceCode;
                    else
                        sourceCode = ActionSourceCode;

                    var className = "Class" + classNameSuffix++;
                    var sourceString = string.Format(sourceCode, Namespace, className, typeString, typeString, funcString);

                    var assembly = CompileString(sourceString, "v4.0");

                    var t = assembly.GetTypes().Where(c => c.Name == className).First();
                    m = t.GetMethod("f", BindingFlags.Static | BindingFlags.Public);

                    _codeCache.Add(funcString + "$" + typeString, m);
                }

                return (Delegate)m.Invoke(null, new object[] { dataMap, column });
            }
        }
    }
}
