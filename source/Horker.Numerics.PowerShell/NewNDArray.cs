using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.PowerShell
{
    [Cmdlet("New", "NDArray")]
    [OutputType(typeof(NDArrayObject))]
    public class NewNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "double")]
        public double[] DoubleValues;

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "float")]
        public float[] FloatValues;

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "int")]
        public int[] IntValues;

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "object")]
        public object[] ObjectValues;

        [Parameter(Position = 1, Mandatory = false)]
        public int[] Shape = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Type Type = null;

        protected override void BeginProcessing()
        {
            var setName = ParameterSetName;
            NDArrayObject result = null;

            if (Type != null)
            {
                // TODO
            }

            if (setName == "double")
                result = NDArray<double>.Create(DoubleValues, Shape);
            else if (setName == "float")
                result = NDArray<float>.Create(FloatValues, Shape);
            else if (setName == "int")
                result = NDArray<int>.Create(IntValues, Shape);
            else if (setName == "object")
                result = NDArray<object>.Create(ObjectValues, Shape);

            WriteObject(result);
        }
    }
}
