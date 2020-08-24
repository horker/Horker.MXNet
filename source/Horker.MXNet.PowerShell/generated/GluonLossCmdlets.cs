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
    [Cmdlet("New", "MxGluonCosineEmbeddingLoss")]
    [Alias("mx.gluon.loss.CosineEmbeddingLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.CosineEmbeddingLoss))]
    public class NewMxGluonCosineEmbeddingLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public int? BatchAxis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public float Margin { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.CosineEmbeddingLoss(Weight, BatchAxis, Margin, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonCTCLoss")]
    [Alias("mx.gluon.loss.CTCLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.CTCLoss))]
    public class NewMxGluonCTCLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Layout { get; set; } = "NTC";

        [Parameter(Position = 1, Mandatory = false)]
        public string LabelLayout { get; set; } = "NT";

        [Parameter(Position = 2, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 5, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.CTCLoss(Layout, LabelLayout, Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonHingeLoss")]
    [Alias("mx.gluon.loss.HingeLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.HingeLoss))]
    public class NewMxGluonHingeLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Margin { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.HingeLoss(Margin, Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonHuberLoss")]
    [Alias("mx.gluon.loss.HuberLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.HuberLoss))]
    public class NewMxGluonHuberLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Rho { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.HuberLoss(Rho, Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonKLDivLoss")]
    [Alias("mx.gluon.loss.KLDivLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.KLDivLoss))]
    public class NewMxGluonKLDivLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public bool FromLogits { get; set; } = true;

        [Parameter(Position = 1, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 5, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.KLDivLoss(FromLogits, Axis, Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonL1Loss")]
    [Alias("mx.gluon.loss.L1Loss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.L1Loss))]
    public class NewMxGluonL1Loss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 3, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.L1Loss(Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonL2Loss")]
    [Alias("mx.gluon.loss.L2Loss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.L2Loss))]
    public class NewMxGluonL2Loss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float? Weight { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 3, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.L2Loss(Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonLogisticLoss")]
    [Alias("mx.gluon.loss.LogisticLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.LogisticLoss))]
    public class NewMxGluonLogisticLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public string LabelFormat { get; set; } = "signed";

        [Parameter(Position = 3, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 4, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.LogisticLoss(Weight, BatchAxis, LabelFormat, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonPoissonNLLLoss")]
    [Alias("mx.gluon.loss.PoissonNLLLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.PoissonNLLLoss))]
    public class NewMxGluonPoissonNLLLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public bool FromLogits { get; set; } = false;

        [Parameter(Position = 1, Mandatory = false)]
        public bool ComputeFull { get; set; } = false;

        [Parameter(Position = 2, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 5, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.PoissonNLLLoss(FromLogits, ComputeFull, Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonSigmoidBinaryCrossEntropyLoss")]
    [Alias("mx.gluon.loss.SigmoidBinaryCrossEntropyLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.SigmoidBinaryCrossEntropyLoss))]
    public class NewMxGluonSigmoidBinaryCrossEntropyLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public bool FromSigmoid { get; set; } = false;

        [Parameter(Position = 1, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 4, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.SigmoidBinaryCrossEntropyLoss(FromSigmoid, Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonSoftmaxCrossEntropyLoss")]
    [Alias("mx.gluon.loss.SoftmaxCrossEntropyLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.SoftmaxCrossEntropyLoss))]
    public class NewMxGluonSoftmaxCrossEntropyLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 1, Mandatory = false)]
        public bool SparseLabel { get; set; } = true;

        [Parameter(Position = 2, Mandatory = false)]
        public bool FromLogits { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 6, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.SoftmaxCrossEntropyLoss(Axis, SparseLabel, FromLogits, Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonSquaredHingeLoss")]
    [Alias("mx.gluon.loss.SquaredHingeLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.SquaredHingeLoss))]
    public class NewMxGluonSquaredHingeLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Margin { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.SquaredHingeLoss(Margin, Weight, BatchAxis, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonTripletLoss")]
    [Alias("mx.gluon.loss.TripletLoss")]
    [OutputType(typeof(global::MxNet.Gluon.Losses.TripletLoss))]
    public class NewMxGluonTripletLoss : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Margin { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public float? Weight { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int? BatchAxis { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.Losses.TripletLoss(Margin, Weight, BatchAxis, Prefix, Params));
        }
    }
}
