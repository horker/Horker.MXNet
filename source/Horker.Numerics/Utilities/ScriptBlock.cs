using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.ScriptBlocks
{
    public class ScriptBlock
    {
        private Delegate _func;

        private System.Management.Automation.ScriptBlock _scriptBlock;
        private List<PSVariable> _params;

        private string _toString;

        public ScriptBlock(string functionString, Type[] parameterTypes)
        {
            _func = FunctionCompiler.Compile(functionString, parameterTypes);
            _toString = functionString;
        }

        public ScriptBlock(System.Management.Automation.ScriptBlock scriptBlock)
        {
            _scriptBlock = scriptBlock;
            _params = new List<PSVariable>();
            _toString = _scriptBlock.ToString();
        }

        public void SetParameterNames(string[] names)
        {
            foreach (var n in names)
            {
                var p = new PSVariable(n);
                _params.Add(p);
            }
        }

        public object Invoke(object[] args)
        {
            if (_func != null)
                return _func.DynamicInvoke(args);

            for (var i = 0; i < args.Length; ++i)
                _params[i].Value = args[i];

            var result = _scriptBlock.InvokeWithContext(null, _params, null)[0];

            return result.BaseObject ?? result;
        }

        public override string ToString()
        {
            return _toString;
        }
    }
}
