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
    [Cmdlet("New", "MxAdaDelta")]
    [Alias("mx.optimizer.AdaDelta")]
    [OutputType(typeof(global::MxNet.Optimizers.AdaDelta))]
    public class NewMxAdaDelta : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Lr { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public float Rho { get; set; } = 0.95f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Decayrate { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-07f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.AdaDelta(Lr, Rho, Decayrate, Epsilon));
        }
    }

    [Cmdlet("New", "MxAdaGrad")]
    [Alias("mx.optimizer.AdaGrad")]
    [OutputType(typeof(global::MxNet.Optimizers.AdaGrad))]
    public class NewMxAdaGrad : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-07f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.AdaGrad(Lr, Epsilon));
        }
    }

    [Cmdlet("New", "MxAdam")]
    [Alias("mx.optimizer.Adam")]
    [OutputType(typeof(global::MxNet.Optimizers.Adam))]
    public class NewMxAdam : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float LearningRate { get; set; } = 0.001f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Beta1 { get; set; } = 0.9f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Beta2 { get; set; } = 0.999f;

        [Parameter(Position = 3, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-8f;

        [Parameter(Position = 4, Mandatory = false)]
        public bool LazyUpdate { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.Adam(LearningRate, Beta1, Beta2, Epsilon, LazyUpdate));
        }
    }

    [Cmdlet("New", "MxAdamax")]
    [Alias("mx.optimizer.Adamax")]
    [OutputType(typeof(global::MxNet.Optimizers.Adamax))]
    public class NewMxAdamax : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float LearningRate { get; set; } = 0.00f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Beta1 { get; set; } = 0.9f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Beta2 { get; set; } = 0.999f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.Adamax(LearningRate, Beta1, Beta2));
        }
    }

    [Cmdlet("New", "MxDCASGD")]
    [Alias("mx.optimizer.DCASGD")]
    [OutputType(typeof(global::MxNet.Optimizers.DCASGD))]
    public class NewMxDCASGD : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Momentum { get; set; } = 0;

        [Parameter(Position = 1, Mandatory = false)]
        public float Lamda { get; set; } = 0.04f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.DCASGD(Momentum, Lamda));
        }
    }

    [Cmdlet("New", "MxFtlr")]
    [Alias("mx.optimizer.Ftlr")]
    [OutputType(typeof(global::MxNet.Optimizers.Ftlr))]
    public class NewMxFtlr : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Lamda1 { get; set; } = 0.1f;

        [Parameter(Position = 1, Mandatory = false)]
        public float LearningRate { get; set; } = 0.1f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Beta { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.Ftlr(Lamda1, LearningRate, Beta));
        }
    }

    [Cmdlet("New", "MxFTML")]
    [Alias("mx.optimizer.FTML")]
    [OutputType(typeof(global::MxNet.Optimizers.FTML))]
    public class NewMxFTML : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Beta1 { get; set; } = 0.6f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Beta2 { get; set; } = 0.999f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-8f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.FTML(Beta1, Beta2, Epsilon));
        }
    }

    [Cmdlet("New", "MxLBSGD")]
    [Alias("mx.optimizer.LBSGD")]
    [OutputType(typeof(global::MxNet.Optimizers.LBSGD))]
    public class NewMxLBSGD : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Momentum { get; set; } = 0;

        [Parameter(Position = 1, Mandatory = false)]
        public bool MultiPrecision { get; set; } = false;

        [Parameter(Position = 2, Mandatory = false)]
        public string WarmupStrategy { get; set; } = "linear'";

        [Parameter(Position = 3, Mandatory = false)]
        public int WarmupEpochs { get; set; } = 5;

        [Parameter(Position = 4, Mandatory = false)]
        public int BatchScale { get; set; } = 1;

        [Parameter(Position = 5, Mandatory = false)]
        public int UpdatesPerEpoch { get; set; } = 32;

        [Parameter(Position = 6, Mandatory = false)]
        public int BeginEpoch { get; set; } = 0;

        [Parameter(Position = 7, Mandatory = false)]
        public int NumEpochs { get; set; } = 60;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.LBSGD(Momentum, MultiPrecision, WarmupStrategy, WarmupEpochs, BatchScale, UpdatesPerEpoch, BeginEpoch, NumEpochs));
        }
    }

    [Cmdlet("New", "MxNadam")]
    [Alias("mx.optimizer.Nadam")]
    [OutputType(typeof(global::MxNet.Optimizers.Nadam))]
    public class NewMxNadam : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float LearningRate { get; set; } = 0.001f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Beta1 { get; set; } = 0.9f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Beta2 { get; set; } = 0.999f;

        [Parameter(Position = 3, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-8f;

        [Parameter(Position = 4, Mandatory = false)]
        public float ScheduleDecay { get; set; } = 0.004f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.Nadam(LearningRate, Beta1, Beta2, Epsilon, ScheduleDecay));
        }
    }

    [Cmdlet("New", "MxNAG")]
    [Alias("mx.optimizer.NAG")]
    [OutputType(typeof(global::MxNet.Optimizers.NAG))]
    public class NewMxNAG : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Momentum { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.NAG(Momentum));
        }
    }

    [Cmdlet("New", "MxRMSProp")]
    [Alias("mx.optimizer.RMSProp")]
    [OutputType(typeof(global::MxNet.Optimizers.RMSProp))]
    public class NewMxRMSProp : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float LearningRate { get; set; } = 0.001f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Gamma1 { get; set; } = 0.9f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Gamma2 { get; set; } = 0.9f;

        [Parameter(Position = 3, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-8f;

        [Parameter(Position = 4, Mandatory = false)]
        public bool Centered { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public float? ClipWeights { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.RMSProp(LearningRate, Gamma1, Gamma2, Epsilon, Centered, ClipWeights));
        }
    }

    [Cmdlet("New", "MxSGD")]
    [Alias("mx.optimizer.SGD")]
    [OutputType(typeof(global::MxNet.Optimizers.SGD))]
    public class NewMxSGD : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float LearningRate { get; set; } = 0.01f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Momentum { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public bool LazyUpdate { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public bool MultiPrecision { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.SGD(LearningRate, Momentum, LazyUpdate, MultiPrecision));
        }
    }

    [Cmdlet("New", "MxSignum")]
    [Alias("mx.optimizer.Signum")]
    [OutputType(typeof(global::MxNet.Optimizers.Signum))]
    public class NewMxSignum : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float LearningRate { get; set; } = 0.01f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Momentum { get; set; } = 0.9f;

        [Parameter(Position = 2, Mandatory = false)]
        public float WdLh { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Optimizers.Signum(LearningRate, Momentum, WdLh));
        }
    }
}
