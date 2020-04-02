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

        [Parameter(Position = 3, Mandatory = false, ParameterSetName = "New")]
        public SwitchParameter Random = false;

        [Parameter(Position = 4, Mandatory = false, ParameterSetName = "New")]
        public int Seed = -1;

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "FromArray")]
        public IList Data;

        protected override void BeginProcessing()
        {
            SeriesBase result = null;
            if (ParameterSetName == "New")
            {
                if (Random)
                {
                    var m = typeof(Series).GetMethod("CreateRandom");
                    var gm = m.MakeGenericMethod(DataType);
                    result = gm.Invoke(null, new object [] { Count, Seed }) as SeriesBase;
                }
                else
                    result = new Series(DataType, Count, Value);
            }
            else
                result = new Series(Data);

            WriteObject(result);
        }
    }
}
