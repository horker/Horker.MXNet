using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps;
using Horker.Numerics.DataMaps.Extensions;

namespace Horker.Numerics.PowerShell
{
    [CmdletBinding()]
    [Cmdlet("Convert", "ElementType")]
    [OutputType(typeof(IList))]
    public class ConvertType : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public IList Data;

        [Parameter(Position = 1, Mandatory = false)]
        public Type[] Types = null;

        protected override void BeginProcessing()
        {
            var result = Data.Convert(Types);
            WriteObject(result);
        }


    }
}
