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
    [Cmdlet("New", "MxCosineScheduler")]
    [Alias("mx.scheduler.CosineScheduler")]
    [OutputType(typeof(global::MxNet.Schedulers.CosineScheduler))]
    public class NewMxCosineScheduler : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int MaxUpdate { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float BaseLr { get; set; } = 0.01F;

        [Parameter(Position = 2, Mandatory = false)]
        public float FinalLr { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public int WarmupSteps { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public float WarmupBeginLr { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public string WarmupMode { get; set; } = "linear";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Schedulers.CosineScheduler(MaxUpdate, BaseLr, FinalLr, WarmupSteps, WarmupBeginLr, WarmupMode));
        }
    }

    [Cmdlet("New", "MxFactorScheduler")]
    [Alias("mx.scheduler.FactorScheduler")]
    [OutputType(typeof(global::MxNet.FactorScheduler))]
    public class NewMxFactorScheduler : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Step { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Factor { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public float StopFactorLr { get; set; } = 1e-8f;

        [Parameter(Position = 3, Mandatory = false)]
        public float BaseLr { get; set; } = 0.01F;

        [Parameter(Position = 4, Mandatory = false)]
        public int WarmupSteps { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public float WarmupBeginLr { get; set; } = 0;

        [Parameter(Position = 6, Mandatory = false)]
        public string WarmupMode { get; set; } = "linear";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.FactorScheduler(Step, Factor, StopFactorLr, BaseLr, WarmupSteps, WarmupBeginLr, WarmupMode));
        }
    }

    [Cmdlet("New", "MxMultiFactorScheduler")]
    [Alias("mx.scheduler.MultiFactorScheduler")]
    [OutputType(typeof(global::MxNet.Schedulers.MultiFactorScheduler))]
    public class NewMxMultiFactorScheduler : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int[] Step { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Factor { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public float BaseLr { get; set; } = 0.01F;

        [Parameter(Position = 3, Mandatory = false)]
        public int WarmupSteps { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public float WarmupBeginLr { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public string WarmupMode { get; set; } = "linear";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Schedulers.MultiFactorScheduler(Step, Factor, BaseLr, WarmupSteps, WarmupBeginLr, WarmupMode));
        }
    }

    [Cmdlet("New", "MxPolyScheduler")]
    [Alias("mx.scheduler.PolyScheduler")]
    [OutputType(typeof(global::MxNet.Schedulers.PolyScheduler))]
    public class NewMxPolyScheduler : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int MaxUpdate { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float BaseLr { get; set; } = 0.01F;

        [Parameter(Position = 2, Mandatory = false)]
        public int Pwr { get; set; } = 2;

        [Parameter(Position = 3, Mandatory = false)]
        public float FinalLr { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public int WarmupSteps { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public float WarmupBeginLr { get; set; } = 0;

        [Parameter(Position = 6, Mandatory = false)]
        public string WarmupMode { get; set; } = "linear";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Schedulers.PolyScheduler(MaxUpdate, BaseLr, Pwr, FinalLr, WarmupSteps, WarmupBeginLr, WarmupMode));
        }
    }
}
