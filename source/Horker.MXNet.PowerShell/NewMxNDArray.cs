using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Core;

namespace Horker.MXNet.PowerShell
{
    [Cmdlet("New", "MxNDArray")]
    [Alias("mx.ndarray")]
    [OutputType(typeof(NDArray))]
    public class NewMxNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "double")]
        public double[] Double;

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "float")]
        public float[] Float;

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "int")]
        public int[] Int;

        [Parameter(Position = 1, Mandatory = false)]
        public int[] Shape = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Type Type = null;

        protected override void BeginProcessing()
        {
            var setName = ParameterSetName;
            NDArray result = null;

            if (Type != null)
            {
                // TODO
            }

            if (setName == "double")
                result = NDArray.FromArray(Double, Shape);
            else if (setName == "float")
                result = NDArray.FromArray(Float, Shape);
            else if (setName == "int")
                result = NDArray.FromArray(Int, Shape);
            else
            {
                WriteError(new ErrorRecord(new ArgumentException("Unsupported type"), "", ErrorCategory.InvalidType, null));
                return;
            }

            WriteObject(result);
        }
    }
}
