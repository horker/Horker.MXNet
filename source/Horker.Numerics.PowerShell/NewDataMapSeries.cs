using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps;

namespace Horker.Numerics.PowerShell
{
    [Cmdlet("New", "DataMapSeries")]
    [OutputType(typeof(Series))]
    public class NewDataMapSeries : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "New")]
        public Type DataType;

        [Parameter(Position = 1, Mandatory = true, ParameterSetName = "New")]
        public int Count;

        [Parameter(Position = 2, Mandatory = false, ParameterSetName = "New")]
        public object Value = null;

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "FromArray")]
        public IList Data;

        protected override void BeginProcessing()
        {
            if (ParameterSetName == "New")
                WriteObject(new Series(DataType, Count, Value));
            else
                WriteObject(new Series(Data));
        }
    }
}
