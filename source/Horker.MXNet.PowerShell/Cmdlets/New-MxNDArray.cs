using MxNet;
using System;
using System.Management.Automation;

namespace Horker.MxNet.PowerShell
{
    [Cmdlet("New", "MxNDArray")]
    [Alias("mx.NDArray")]
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
        public Context Context = null;

        protected override void BeginProcessing()
        {
            var setName = ParameterSetName;
            NDArray result = null;

            if (setName == "double")
                result = new NDArray(Double, Context);
            else if (setName == "float")
                result = new NDArray(Float, Context);
            else if (setName == "int")
                result = new NDArray(Int, Context);
            else
            {
                WriteError(new ErrorRecord(new ArgumentException("Unsupported type"), "", ErrorCategory.InvalidType, null));
                return;
            }

            if (Shape != null)
                result = result.Reshape(Shape);

            WriteObject(result);
        }
    }
}
