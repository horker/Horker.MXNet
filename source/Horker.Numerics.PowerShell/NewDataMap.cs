using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps;

namespace Horker.Numerics.PowerShell
{
    [Cmdlet("New", "DataMap")]
    [OutputType(typeof(DataMap))]
    public class NewDataMap : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            var map = new DataMap();
            WriteObject(map);
        }
    }
}
