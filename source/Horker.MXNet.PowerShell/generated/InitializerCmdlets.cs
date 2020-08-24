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
    [Cmdlet("New", "MxConstantInitializer")]
    [Alias("mx.initializer.Constant")]
    [OutputType(typeof(global::MxNet.Initializers.Constant))]
    public class NewMxConstantInitializer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Value { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Initializers.Constant(Value));
        }
    }

    [Cmdlet("New", "MxLSTMBiasInitializer")]
    [Alias("mx.initializer.LSTMBias")]
    [OutputType(typeof(global::MxNet.Initializers.LSTMBias))]
    public class NewMxLSTMBiasInitializer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float ForgetBias { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Initializers.LSTMBias(ForgetBias));
        }
    }

    [Cmdlet("New", "MxMixedInitializer")]
    [Alias("mx.initializer.Mixed")]
    [OutputType(typeof(global::MxNet.Initializers.Mixed))]
    public class NewMxMixedInitializer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string[] Patterns { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Initializer[] Initializers { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Initializers.Mixed(Patterns, Initializers));
        }
    }

    [Cmdlet("New", "MxMSRAPreluInitializer")]
    [Alias("mx.initializer.MSRAPrelu")]
    [OutputType(typeof(global::MxNet.Initializers.MSRAPrelu))]
    public class NewMxMSRAPreluInitializer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string FactorType { get; set; } = "avg";

        [Parameter(Position = 1, Mandatory = false)]
        public float Slope { get; set; } = 0.25f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Initializers.MSRAPrelu(FactorType, Slope));
        }
    }

    [Cmdlet("New", "MxNormalInitializer")]
    [Alias("mx.initializer.Normal")]
    [OutputType(typeof(global::MxNet.Initializers.Normal))]
    public class NewMxNormalInitializer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Sigma { get; set; } = 0.01f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Initializers.Normal(Sigma));
        }
    }

    [Cmdlet("New", "MxOrthogonalInitializer")]
    [Alias("mx.initializer.Orthogonal")]
    [OutputType(typeof(global::MxNet.Initializers.Orthogonal))]
    public class NewMxOrthogonalInitializer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Scale { get; set; } = 1.414f;

        [Parameter(Position = 1, Mandatory = false)]
        public string RandType { get; set; } = "uniform";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Initializers.Orthogonal(Scale, RandType));
        }
    }

    [Cmdlet("New", "MxUniformInitializer")]
    [Alias("mx.initializer.Uniform")]
    [OutputType(typeof(global::MxNet.Initializers.Uniform))]
    public class NewMxUniformInitializer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Scale { get; set; } = 0.07f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Initializers.Uniform(Scale));
        }
    }

    [Cmdlet("New", "MxXavierInitializer")]
    [Alias("mx.initializer.Xavier")]
    [OutputType(typeof(global::MxNet.Initializers.Xavier))]
    public class NewMxXavierInitializer : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string RndType { get; set; } = "uniform";

        [Parameter(Position = 1, Mandatory = false)]
        public string FactorType { get; set; } = "avg";

        [Parameter(Position = 2, Mandatory = false)]
        public float Magnitude { get; set; } = 3;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Initializers.Xavier(RndType, FactorType, Magnitude));
        }
    }
}
