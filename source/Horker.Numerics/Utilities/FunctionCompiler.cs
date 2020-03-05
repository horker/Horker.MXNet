using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Management.Automation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Horker.Numerics.DataMaps;
using System.IO;

namespace Horker.Numerics
{
    public static class FunctionCompiler
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
using Accord.Math;
using Accord.Statistics;
using Accord.MachineLearning;
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
using Accord.Math;
using Accord.Statistics;
using Accord.MachineLearning;
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

        private static string _assemblyBasePath = Path.GetDirectoryName(typeof(object).Assembly.Location);

        private static IEnumerable<MetadataReference> _references = new string[]
        {
            // This assembly is System.Private.CoreLib.dll in .NET Core 3.1.2.
            typeof(object).Assembly.Location,

            // Core classes
            // We are not sure what classes the above assembly contains, but we know it isn't
            // sufficient to make ordinary core classes available anyway.
            // So we will load core assemblies explicitly.
            Path.Combine(_assemblyBasePath, "mscorlib.dll"),
            Path.Combine(_assemblyBasePath, "System.dll"),
            Path.Combine(_assemblyBasePath, "System.Runtime.dll"),
            Path.Combine(_assemblyBasePath, "System.Collections.dll"),
            Path.Combine(_assemblyBasePath, "System.Linq.dll"),

            // System.Management.Automation
            typeof(PowerShell).Assembly.Location,

            // Horker.Numerics
            typeof(FunctionCompiler).Assembly.Location,

            // Accord.Math
            typeof(Accord.Math.Beta).Assembly.Location,

            // Accord.Statistics
            typeof(Accord.Statistics.Distributions.Univariate.NormalDistribution).Assembly.Location,

            // Accord.MachineLearning
            typeof(Accord.MachineLearning.KMeans).Assembly.Location
        }.Select(r => MetadataReference.CreateFromFile(r));

        private static Assembly CompileString(string sourceString, string compilerVersion)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceString);

            var assemblyName = Path.GetRandomFileName();

            var compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { syntaxTree },
                _references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);

                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    var e = new StringBuilder();
                    e.AppendFormat("Error when compiling: {0}", sourceString);
                    e.AppendLine();
                    foreach (Diagnostic diagnostic in failures)
                    {
                        e.Append("  ");
                        e.AppendFormat("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                        e.AppendLine();
                    }

                    throw new ArgumentException(e.ToString());
                }

                ms.Seek(0, SeekOrigin.Begin);
                return Assembly.Load(ms.ToArray());
            }
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
