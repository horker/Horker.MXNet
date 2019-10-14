using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Horker.Numerics.DataMaps;
using Horker.Numerics.Utilities;

namespace Horker.Numerics.PowerShell
{
    [Cmdlet("ConvertTo", "DataMap")]
    public class ConvertToDataMap : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public PSObject InputObject;

        [Parameter(Position = 1, Mandatory = false)]
        public Type[] PossibleTypes = null;

        [Parameter(Position = 2, Mandatory = false)]
        public SwitchParameter ConvertTypes;

        private Dictionary<string, List<object>> _data;
        private int _recordCount;

        protected override void BeginProcessing()
        {
            _data = new Dictionary<string, List<object>>();
            _recordCount = 0;
        }

        protected override void ProcessRecord()
        {
            foreach (var prop in InputObject.Properties)
            {
                List<object> column;
                if (!_data.TryGetValue(prop.Name, out column))
                {
                    column = new List<object>();
                    _data.Add(prop.Name, column);
                    for (var i = 0; i < _recordCount; ++i)
                        column.Add(null);
                }

                try
                {
                    var value = Utils.StripOffPSObject(prop.Value);
                    column.Add(value);
                }
                catch (Exception)
                {
                    // An exception can occur when you try to get the property value. It will be ignored.
                    // e.g. the ExitCode property of System.Diagnostics.Process
                    column.Add(null);
                }
            }

            ++_recordCount;
        }

        protected override void EndProcessing()
        {
            var d = new DataMap();

            foreach (var entry in _data)
                d.AddLast(entry.Key, entry.Value);

           if (ConvertTypes)
                d = d.TypeConversion(PossibleTypes);

            WriteObject(d);
        }
    }
}
