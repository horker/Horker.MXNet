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
    [Cmdlet("New", "MxGluonELU")]
    [Alias("mx.gluon.nn.ELU")]
    [OutputType(typeof(global::MxNet.Gluon.NN.ELU))]
    public class NewMxGluonELU : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Alpha { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.ELU(Alpha, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonGELU")]
    [Alias("mx.gluon.nn.GELU")]
    [OutputType(typeof(global::MxNet.Gluon.NN.GELU))]
    public class NewMxGluonGELU : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.GELU(Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonLeakyReLU")]
    [Alias("mx.gluon.nn.LeakyReLU")]
    [OutputType(typeof(global::MxNet.Gluon.NN.LeakyReLU))]
    public class NewMxGluonLeakyReLU : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Alpha { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.LeakyReLU(Alpha, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonPReLU")]
    [Alias("mx.gluon.nn.PReLU")]
    [OutputType(typeof(global::MxNet.Gluon.NN.PReLU))]
    public class NewMxGluonPReLU : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Initializer AlphaInitializer { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.PReLU(AlphaInitializer, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonSELU")]
    [Alias("mx.gluon.nn.SELU")]
    [OutputType(typeof(global::MxNet.Gluon.NN.SELU))]
    public class NewMxGluonSELU : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.SELU(Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonSwish")]
    [Alias("mx.gluon.nn.Swish")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Swish))]
    public class NewMxGluonSwish : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Beta { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Swish(Beta, Prefix, Params));
        }
    }
}
