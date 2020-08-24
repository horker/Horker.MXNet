using System.Collections.Generic;
using System.Management.Automation;
using MxNet;
using MxNet.Initializers;
using MxNet.Optimizers;
using MxNet.Image;
using MxNet.Gluon;
using MxNet.Gluon.RNN;
using OpenCvSharp;
using NumpyDotNet;

namespace Horker.MxNet.PowerShell
{
    [Cmdlet("New", "MxNDArrayIter")]
    [Alias("mx.io.NDArrayIter")]
    [OutputType(typeof(global::MxNet.IO.NDArrayIter))]
    public class NewMxNDArrayIter : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public NDArrayList Label { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int BatchSize { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Shuffle { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string LastBatchHandle { get; set; } = "pad";

        [Parameter(Position = 5, Mandatory = false)]
        public string DataName { get; set; } = "data";

        [Parameter(Position = 6, Mandatory = false)]
        public string LabelName { get; set; } = "softmax_label";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.IO.NDArrayIter(Data, Label, BatchSize, Shuffle, LastBatchHandle, DataName, LabelName));
        }
    }
}
