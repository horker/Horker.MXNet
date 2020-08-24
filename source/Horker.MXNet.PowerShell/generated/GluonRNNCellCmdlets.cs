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
    [Cmdlet("New", "MxGluonRNNBidirectionalCell")]
    [Alias("mx.gluon.rnn.BidirectionalCell")]
    [OutputType(typeof(global::MxNet.Gluon.RNN.BidirectionalCell))]
    public class NewMxGluonRNNBidirectionalCell : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public RecurrentCell LCell { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public RecurrentCell RCell { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string OutputPrefix { get; set; } = "bi_";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.RNN.BidirectionalCell(LCell, RCell, OutputPrefix));
        }
    }

    [Cmdlet("New", "MxGluonRNNDropoutCell")]
    [Alias("mx.gluon.rnn.DropoutCell")]
    [OutputType(typeof(global::MxNet.Gluon.RNN.DropoutCell))]
    public class NewMxGluonRNNDropoutCell : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Rate { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axes { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 3, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.RNN.DropoutCell(Rate, Axes, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonGRUCell")]
    [Alias("mx.gluon.rnn.GRUCell")]
    [OutputType(typeof(global::MxNet.Gluon.RNN.GRUCell))]
    public class NewMxGluonGRUCell : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int HiddenSize { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string I2hWeightInitializer { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string H2hWeightInitializer { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string I2hBiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 4, Mandatory = false)]
        public string H2hBiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 5, Mandatory = false)]
        public int InputSize { get; set; } = 0;

        [Parameter(Position = 6, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 7, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.RNN.GRUCell(HiddenSize, I2hWeightInitializer, H2hWeightInitializer, I2hBiasInitializer, H2hBiasInitializer, InputSize, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonHybridSequentialRNNCell")]
    [Alias("mx.gluon.rnn.HybridSequentialRNNCell")]
    [OutputType(typeof(global::MxNet.Gluon.RNN.HybridSequentialRNNCell))]
    public class NewMxGluonHybridSequentialRNNCell : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Prefix { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public ParameterDict Params { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.RNN.HybridSequentialRNNCell(Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonLSTMCell")]
    [Alias("mx.gluon.rnn.LSTMCell")]
    [OutputType(typeof(global::MxNet.Gluon.RNN.LSTMCell))]
    public class NewMxGluonLSTMCell : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int HiddenSize { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string Activation { get; set; } = "tanh";

        [Parameter(Position = 2, Mandatory = false)]
        public string RecurrentActivation { get; set; } = "sigmoid";

        [Parameter(Position = 3, Mandatory = false)]
        public string I2hWeightInitializer { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string H2hWeightInitializer { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string I2hBiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 6, Mandatory = false)]
        public string H2hBiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 7, Mandatory = false)]
        public int InputSize { get; set; } = 0;

        [Parameter(Position = 8, Mandatory = false)]
        public string Prefix { get; set; } = "";

        [Parameter(Position = 9, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.RNN.LSTMCell(HiddenSize, Activation, RecurrentActivation, I2hWeightInitializer, H2hWeightInitializer, I2hBiasInitializer, H2hBiasInitializer, InputSize, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonResidualCell")]
    [Alias("mx.gluon.rnn.ResidualCell")]
    [OutputType(typeof(global::MxNet.Gluon.RNN.ResidualCell))]
    public class NewMxGluonResidualCell : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public RecurrentCell BaseCell { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.RNN.ResidualCell(BaseCell));
        }
    }

    [Cmdlet("New", "MxGluonSequentialRNNCell")]
    [Alias("mx.gluon.rnn.SequentialRNNCell")]
    [OutputType(typeof(global::MxNet.Gluon.RNN.SequentialRNNCell))]
    public class NewMxGluonSequentialRNNCell : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Prefix { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public ParameterDict Params { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.RNN.SequentialRNNCell(Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonZoneoutCell")]
    [Alias("mx.gluon.rnn.ZoneoutCell")]
    [OutputType(typeof(global::MxNet.Gluon.RNN.ZoneoutCell))]
    public class NewMxGluonZoneoutCell : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public RecurrentCell BaseCell { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float ZoneoutOutputs { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public float ZoneoutStates { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.RNN.ZoneoutCell(BaseCell, ZoneoutOutputs, ZoneoutStates));
        }
    }
}
