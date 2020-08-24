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
    [Cmdlet("New", "MxMetricAccuracy")]
    [Alias("mx.metric.Accuracy")]
    [OutputType(typeof(global::MxNet.Metrics.Accuracy))]
    public class NewMxMetricAccuracy : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.Accuracy(Axis, OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricBinaryAccuracy")]
    [Alias("mx.metric.BinaryAccuracy")]
    [OutputType(typeof(global::MxNet.Metrics.BinaryAccuracy))]
    public class NewMxMetricBinaryAccuracy : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Threshold { get; set; } = 0.5f;

        [Parameter(Position = 1, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.BinaryAccuracy(Threshold, OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricCrossEntropy")]
    [Alias("mx.metric.CrossEntropy")]
    [OutputType(typeof(global::MxNet.Metrics.CrossEntropy))]
    public class NewMxMetricCrossEntropy : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Eps { get; set; } = 1e-12f;

        [Parameter(Position = 1, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.CrossEntropy(Eps, OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricF1")]
    [Alias("mx.metric.F1")]
    [OutputType(typeof(global::MxNet.Metrics.F1))]
    public class NewMxMetricF1 : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public string LabelName { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string Average { get; set; } = "macro";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.F1(OutputName, LabelName, Average));
        }
    }

    [Cmdlet("New", "MxMetricMAE")]
    [Alias("mx.metric.MAE")]
    [OutputType(typeof(global::MxNet.Metrics.MAE))]
    public class NewMxMetricMAE : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.MAE(OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricMCC")]
    [Alias("mx.metric.MCC")]
    [OutputType(typeof(global::MxNet.Metrics.MCC))]
    public class NewMxMetricMCC : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public string LabelName { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string Average { get; set; } = "macro";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.MCC(OutputName, LabelName, Average));
        }
    }

    [Cmdlet("New", "MxMetricMSE")]
    [Alias("mx.metric.MSE")]
    [OutputType(typeof(global::MxNet.Metrics.MSE))]
    public class NewMxMetricMSE : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.MSE(OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricNegativeLogLikelihood")]
    [Alias("mx.metric.NegativeLogLikelihood")]
    [OutputType(typeof(global::MxNet.Metrics.NegativeLogLikelihood))]
    public class NewMxMetricNegativeLogLikelihood : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Eps { get; set; } = 1e-12f;

        [Parameter(Position = 1, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.NegativeLogLikelihood(Eps, OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricPCC")]
    [Alias("mx.metric.PCC")]
    [OutputType(typeof(global::MxNet.Metrics.PCC))]
    public class NewMxMetricPCC : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.PCC(OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricPearsonCorrelation")]
    [Alias("mx.metric.PearsonCorrelation")]
    [OutputType(typeof(global::MxNet.Metrics.PearsonCorrelation))]
    public class NewMxMetricPearsonCorrelation : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.PearsonCorrelation(OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricPerplexity")]
    [Alias("mx.metric.Perplexity")]
    [OutputType(typeof(global::MxNet.Metrics.Perplexity))]
    public class NewMxMetricPerplexity : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int? IgnoreLabel { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.Perplexity(IgnoreLabel, Axis, OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricRMSE")]
    [Alias("mx.metric.RMSE")]
    [OutputType(typeof(global::MxNet.Metrics.RMSE))]
    public class NewMxMetricRMSE : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.RMSE(OutputName, LabelName));
        }
    }

    [Cmdlet("New", "MxMetricTopKAccuracy")]
    [Alias("mx.metric.TopKAccuracy")]
    [OutputType(typeof(global::MxNet.Metrics.TopKAccuracy))]
    public class NewMxMetricTopKAccuracy : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int TopK { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public string OutputName { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string LabelName { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Metrics.TopKAccuracy(TopK, OutputName, LabelName));
        }
    }
}
