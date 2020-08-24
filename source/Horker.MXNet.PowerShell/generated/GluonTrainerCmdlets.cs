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
    [Cmdlet("New", "MxTrainer")]
    [Alias("mx.gluon.Trainer")]
    [OutputType(typeof(global::MxNet.Gluon.Trainer))]
    public class NewMxTrainer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public ParameterDict Params { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Optimizer Optimizer { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string Kvstore { get; set; } = "device";

        [Parameter(Position = 3, Mandatory = false)]
        public Dictionary<string, object> CompressionParams { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public bool? UpdateOnKvstore { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Trainer(Params, Optimizer, Kvstore, CompressionParams, UpdateOnKvstore));
        }
    }
}
