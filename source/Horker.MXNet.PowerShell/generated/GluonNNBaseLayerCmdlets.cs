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
    [Cmdlet("New", "MxGluonBatchNorm")]
    [Alias("mx.gluon.nn.BatchNorm")]
    [OutputType(typeof(global::MxNet.Gluon.NN.BatchNorm))]
    public class NewMxGluonBatchNorm : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public float Momentum { get; set; } = 0.9f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-5f;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Center { get; set; } = true;

        [Parameter(Position = 4, Mandatory = false)]
        public bool Scale { get; set; } = true;

        [Parameter(Position = 5, Mandatory = false)]
        public bool UseGlobalStats { get; set; } = false;

        [Parameter(Position = 6, Mandatory = false)]
        public string BetaInitializer { get; set; } = "zeros";

        [Parameter(Position = 7, Mandatory = false)]
        public string GammaInitializer { get; set; } = "ones";

        [Parameter(Position = 8, Mandatory = false)]
        public string RunningMeanInitializer { get; set; } = "zeros";

        [Parameter(Position = 9, Mandatory = false)]
        public string RunningVarianceInitializer { get; set; } = "ones";

        [Parameter(Position = 10, Mandatory = false)]
        public int InChannels { get; set; } = 0;

        [Parameter(Position = 11, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 12, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.BatchNorm(Axis, Momentum, Epsilon, Center, Scale, UseGlobalStats, BetaInitializer, GammaInitializer, RunningMeanInitializer, RunningVarianceInitializer, InChannels, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonDense")]
    [Alias("mx.gluon.nn.Dense")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Dense))]
    public class NewMxGluonDense : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Units { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public ActivationType? Activation { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool UseBias { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Flatten { get; set; } = true;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public Initializer WeightInitializer { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public string BiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 7, Mandatory = false)]
        public int InUnits { get; set; } = 0;

        [Parameter(Position = 8, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Dense(Units, Activation, UseBias, Flatten, Dtype, WeightInitializer, BiasInitializer, InUnits, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonDropout")]
    [Alias("mx.gluon.nn.Dropout")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Dropout))]
    public class NewMxGluonDropout : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Rate { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axes { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Dropout(Rate, Axes, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonEmbedding")]
    [Alias("mx.gluon.nn.Embedding")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Embedding))]
    public class NewMxGluonEmbedding : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int InputDim { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int OutputDim { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string WeightInitializer { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public bool SparseGrad { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Embedding(InputDim, OutputDim, Dtype, WeightInitializer, SparseGrad, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonFlatten")]
    [Alias("mx.gluon.nn.Flatten")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Flatten))]
    public class NewMxGluonFlatten : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Flatten(Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonHybridSequential")]
    [Alias("mx.gluon.nn.HybridSequential")]
    [OutputType(typeof(global::MxNet.Gluon.NN.HybridSequential))]
    public class NewMxGluonHybridSequential : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.HybridSequential(Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonHybridSequential2")]
    [Alias("mx.gluon.nn.HybridSequential2")]
    [OutputType(typeof(global::MxNet.Gluon.NN.HybridSequential))]
    public class NewMxGluonHybridSequential2 : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Dictionary<string, Block> Blocks { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public bool Loadkeys { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.HybridSequential(Blocks, Loadkeys));
        }
    }

    [Cmdlet("New", "MxGluonInstanceNorm")]
    [Alias("mx.gluon.nn.InstanceNorm")]
    [OutputType(typeof(global::MxNet.Gluon.NN.InstanceNorm))]
    public class NewMxGluonInstanceNorm : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-5f;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Center { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Scale { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string BetaInitializer { get; set; } = "zeros";

        [Parameter(Position = 5, Mandatory = false)]
        public string GammaInitializer { get; set; } = "ones";

        [Parameter(Position = 6, Mandatory = false)]
        public int InChannels { get; set; } = 0;

        [Parameter(Position = 7, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.InstanceNorm(Axis, Epsilon, Center, Scale, BetaInitializer, GammaInitializer, InChannels, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonLayerNorm")]
    [Alias("mx.gluon.nn.LayerNorm")]
    [OutputType(typeof(global::MxNet.Gluon.NN.LayerNorm))]
    public class NewMxGluonLayerNorm : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-5f;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Center { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Scale { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string BetaInitializer { get; set; } = "zeros";

        [Parameter(Position = 5, Mandatory = false)]
        public string GammaInitializer { get; set; } = "ones";

        [Parameter(Position = 6, Mandatory = false)]
        public int InChannels { get; set; } = 0;

        [Parameter(Position = 7, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.LayerNorm(Axis, Epsilon, Center, Scale, BetaInitializer, GammaInitializer, InChannels, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonSequential")]
    [Alias("mx.gluon.nn.Sequential")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Sequential))]
    public class NewMxGluonSequential : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Sequential(Prefix, Params));
        }
    }
}
