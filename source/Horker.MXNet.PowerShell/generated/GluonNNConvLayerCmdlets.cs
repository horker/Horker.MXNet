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
    [Cmdlet("New", "MxGluonAvgPool1D")]
    [Alias("mx.gluon.nn.AvgPool1D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.AvgPool1D))]
    public class NewMxGluonAvgPool1D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int PoolSize { get; set; } = 2;

        [Parameter(Position = 1, Mandatory = false)]
        public int? Strides { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int Padding { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string Layout { get; set; } = "NCW";

        [Parameter(Position = 4, Mandatory = false)]
        public bool CeilMode { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.AvgPool1D(PoolSize, Strides, Padding, Layout, CeilMode, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonAvgPool2D")]
    [Alias("mx.gluon.nn.AvgPool2D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.AvgPool2D))]
    public class NewMxGluonAvgPool2D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public (int, int)? PoolSize { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public (int, int)? Strides { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public (int, int)? Padding { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string Layout { get; set; } = "NCHW";

        [Parameter(Position = 4, Mandatory = false)]
        public bool CeilMode { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.AvgPool2D(PoolSize, Strides, Padding, Layout, CeilMode, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonAvgPool3D")]
    [Alias("mx.gluon.nn.AvgPool3D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.AvgPool3D))]
    public class NewMxGluonAvgPool3D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public (int, int, int)? PoolSize { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public (int, int, int)? Strides { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public (int, int, int)? Padding { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string Layout { get; set; } = "NCDHW";

        [Parameter(Position = 4, Mandatory = false)]
        public bool CeilMode { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.AvgPool3D(PoolSize, Strides, Padding, Layout, CeilMode, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonConv1D")]
    [Alias("mx.gluon.nn.Conv1D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Conv1D))]
    public class NewMxGluonConv1D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Channels { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int KernelSize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Strides { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public int Padding { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public int Dilation { get; set; } = 1;

        [Parameter(Position = 5, Mandatory = false)]
        public int Groups { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public string Layout { get; set; } = "NCW";

        [Parameter(Position = 7, Mandatory = false)]
        public int InChannels { get; set; } = 0;

        [Parameter(Position = 8, Mandatory = false)]
        public ActivationType? Activation { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public bool UseBias { get; set; } = true;

        [Parameter(Position = 10, Mandatory = false)]
        public Initializer WeightInitializer { get; set; } = null;

        [Parameter(Position = 11, Mandatory = false)]
        public string BiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 12, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 13, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Conv1D(Channels, KernelSize, Strides, Padding, Dilation, Groups, Layout, InChannels, Activation, UseBias, WeightInitializer, BiasInitializer, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonConv1DTranspose")]
    [Alias("mx.gluon.nn.Conv1DTranspose")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Conv1DTranspose))]
    public class NewMxGluonConv1DTranspose : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Channels { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int KernelSize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Strides { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public int Padding { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public int OutputPadding { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public int Dilation { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public int Groups { get; set; } = 1;

        [Parameter(Position = 7, Mandatory = false)]
        public string Layout { get; set; } = "NCW";

        [Parameter(Position = 8, Mandatory = false)]
        public ActivationType? Activation { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public bool UseBias { get; set; } = true;

        [Parameter(Position = 10, Mandatory = false)]
        public Initializer WeightInitializer { get; set; } = null;

        [Parameter(Position = 11, Mandatory = false)]
        public string BiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 12, Mandatory = false)]
        public int InChannels { get; set; } = 0;

        [Parameter(Position = 13, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 14, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Conv1DTranspose(Channels, KernelSize, Strides, Padding, OutputPadding, Dilation, Groups, Layout, Activation, UseBias, WeightInitializer, BiasInitializer, InChannels, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonConv2D")]
    [Alias("mx.gluon.nn.Conv2D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Conv2D))]
    public class NewMxGluonConv2D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Channels { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public (int, int) KernelSize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public (int, int)? Strides { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public (int, int)? Padding { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public (int, int)? Dilation { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public int Groups { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public string Layout { get; set; } = "NCHW";

        [Parameter(Position = 7, Mandatory = false)]
        public int InChannels { get; set; } = 0;

        [Parameter(Position = 8, Mandatory = false)]
        public ActivationType? Activation { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public bool UseBias { get; set; } = true;

        [Parameter(Position = 10, Mandatory = false)]
        public Initializer WeightInitializer { get; set; } = null;

        [Parameter(Position = 11, Mandatory = false)]
        public string BiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 12, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 13, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Conv2D(Channels, KernelSize, Strides, Padding, Dilation, Groups, Layout, InChannels, Activation, UseBias, WeightInitializer, BiasInitializer, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonConv2DTranspose")]
    [Alias("mx.gluon.nn.Conv2DTranspose")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Conv2DTranspose))]
    public class NewMxGluonConv2DTranspose : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Channels { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public (int, int) KernelSize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public (int, int) Strides { get; set; } = default;

        [Parameter(Position = 3, Mandatory = false)]
        public (int, int) Padding { get; set; } = default;

        [Parameter(Position = 4, Mandatory = false)]
        public (int, int) OutputPadding { get; set; } = default;

        [Parameter(Position = 5, Mandatory = false)]
        public (int, int) Dilation { get; set; } = default;

        [Parameter(Position = 6, Mandatory = false)]
        public int Groups { get; set; } = 1;

        [Parameter(Position = 7, Mandatory = false)]
        public string Layout { get; set; } = "NCHW";

        [Parameter(Position = 8, Mandatory = false)]
        public ActivationType? Activation { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public bool UseBias { get; set; } = true;

        [Parameter(Position = 10, Mandatory = false)]
        public Initializer WeightInitializer { get; set; } = null;

        [Parameter(Position = 11, Mandatory = false)]
        public string BiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 12, Mandatory = false)]
        public int InChannels { get; set; } = 0;

        [Parameter(Position = 13, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 14, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Conv2DTranspose(Channels, KernelSize, Strides, Padding, OutputPadding, Dilation, Groups, Layout, Activation, UseBias, WeightInitializer, BiasInitializer, InChannels, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonConv3D")]
    [Alias("mx.gluon.nn.Conv3D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Conv3D))]
    public class NewMxGluonConv3D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Channels { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public (int, int, int) KernelSize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public (int, int, int)? Strides { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public (int, int, int)? Padding { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public (int, int, int)? Dilation { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public int Groups { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public string Layout { get; set; } = "NCDHW";

        [Parameter(Position = 7, Mandatory = false)]
        public int InChannels { get; set; } = 0;

        [Parameter(Position = 8, Mandatory = false)]
        public ActivationType? Activation { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public bool UseBias { get; set; } = true;

        [Parameter(Position = 10, Mandatory = false)]
        public string WeightInitializer { get; set; } = null;

        [Parameter(Position = 11, Mandatory = false)]
        public string BiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 12, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 13, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Conv3D(Channels, KernelSize, Strides, Padding, Dilation, Groups, Layout, InChannels, Activation, UseBias, WeightInitializer, BiasInitializer, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonConv3DTranspose")]
    [Alias("mx.gluon.nn.Conv3DTranspose")]
    [OutputType(typeof(global::MxNet.Gluon.NN.Conv3DTranspose))]
    public class NewMxGluonConv3DTranspose : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Channels { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public (int, int, int) KernelSize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public (int, int, int) Strides { get; set; } = default;

        [Parameter(Position = 3, Mandatory = false)]
        public (int, int, int) Padding { get; set; } = default;

        [Parameter(Position = 4, Mandatory = false)]
        public (int, int, int) OutputPadding { get; set; } = default;

        [Parameter(Position = 5, Mandatory = false)]
        public (int, int, int) Dilation { get; set; } = default;

        [Parameter(Position = 6, Mandatory = false)]
        public int Groups { get; set; } = 1;

        [Parameter(Position = 7, Mandatory = false)]
        public string Layout { get; set; } = "NCDHW";

        [Parameter(Position = 8, Mandatory = false)]
        public ActivationType? Activation { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public bool UseBias { get; set; } = true;

        [Parameter(Position = 10, Mandatory = false)]
        public Initializer WeightInitializer { get; set; } = null;

        [Parameter(Position = 11, Mandatory = false)]
        public string BiasInitializer { get; set; } = "zeros";

        [Parameter(Position = 12, Mandatory = false)]
        public int InChannels { get; set; } = 0;

        [Parameter(Position = 13, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 14, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.Conv3DTranspose(Channels, KernelSize, Strides, Padding, OutputPadding, Dilation, Groups, Layout, Activation, UseBias, WeightInitializer, BiasInitializer, InChannels, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonGlobalAvgPool1D")]
    [Alias("mx.gluon.nn.GlobalAvgPool1D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.GlobalAvgPool1D))]
    public class NewMxGluonGlobalAvgPool1D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Layout { get; set; } = "NCW";

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.GlobalAvgPool1D(Layout, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonGlobalAvgPool2D")]
    [Alias("mx.gluon.nn.GlobalAvgPool2D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.GlobalAvgPool2D))]
    public class NewMxGluonGlobalAvgPool2D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Layout { get; set; } = "NCHW";

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.GlobalAvgPool2D(Layout, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonGlobalAvgPool3D")]
    [Alias("mx.gluon.nn.GlobalAvgPool3D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.GlobalAvgPool3D))]
    public class NewMxGluonGlobalAvgPool3D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Layout { get; set; } = "NCDHW";

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.GlobalAvgPool3D(Layout, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonGlobalMaxPool1D")]
    [Alias("mx.gluon.nn.GlobalMaxPool1D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.GlobalMaxPool1D))]
    public class NewMxGluonGlobalMaxPool1D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Layout { get; set; } = "NCW";

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.GlobalMaxPool1D(Layout, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonGlobalMaxPool2D")]
    [Alias("mx.gluon.nn.GlobalMaxPool2D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.GlobalMaxPool2D))]
    public class NewMxGluonGlobalMaxPool2D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Layout { get; set; } = "NCHW";

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.GlobalMaxPool2D(Layout, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonGlobalMaxPool3D")]
    [Alias("mx.gluon.nn.GlobalMaxPool3D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.GlobalMaxPool3D))]
    public class NewMxGluonGlobalMaxPool3D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string Layout { get; set; } = "NCDHW";

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.GlobalMaxPool3D(Layout, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonMaxPool1D")]
    [Alias("mx.gluon.nn.MaxPool1D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.MaxPool1D))]
    public class NewMxGluonMaxPool1D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int PoolSize { get; set; } = 2;

        [Parameter(Position = 1, Mandatory = false)]
        public int? Strides { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int Padding { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string Layout { get; set; } = "NCW";

        [Parameter(Position = 4, Mandatory = false)]
        public bool CeilMode { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.MaxPool1D(PoolSize, Strides, Padding, Layout, CeilMode, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonMaxPool2D")]
    [Alias("mx.gluon.nn.MaxPool2D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.MaxPool2D))]
    public class NewMxGluonMaxPool2D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public (int, int)? PoolSize { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public (int, int)? Strides { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public (int, int)? Padding { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string Layout { get; set; } = "NCHW";

        [Parameter(Position = 4, Mandatory = false)]
        public bool CeilMode { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.MaxPool2D(PoolSize, Strides, Padding, Layout, CeilMode, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonMaxPool3D")]
    [Alias("mx.gluon.nn.MaxPool3D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.MaxPool3D))]
    public class NewMxGluonMaxPool3D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public (int, int, int)? PoolSize { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public (int, int, int)? Strides { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public (int, int, int)? Padding { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string Layout { get; set; } = "NCDHW";

        [Parameter(Position = 4, Mandatory = false)]
        public bool CeilMode { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.MaxPool3D(PoolSize, Strides, Padding, Layout, CeilMode, Prefix, Params));
        }
    }

    [Cmdlet("New", "MxGluonReflectionPad2D")]
    [Alias("mx.gluon.nn.ReflectionPad2D")]
    [OutputType(typeof(global::MxNet.Gluon.NN.ReflectionPad2D))]
    public class NewMxGluonReflectionPad2D : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int Padding { get; set; } = 0;

        [Parameter(Position = 1, Mandatory = false)]
        public string Prefix { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ParameterDict Params { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Gluon.NN.ReflectionPad2D(Padding, Prefix, Params));
        }
    }
}
