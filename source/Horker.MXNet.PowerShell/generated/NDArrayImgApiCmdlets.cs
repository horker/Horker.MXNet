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
    [Cmdlet("Get", "MxImgCropNDArray")]
    [Alias("mx.nd.img.Crop")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgCropNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int X { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int Y { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int Width { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int Height { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().Crop(Data, X, Y, Width, Height));
        }
    }

    [Cmdlet("Get", "MxImgToTensorNDArray")]
    [Alias("mx.nd.img.ToTensor")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgToTensorNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().ToTensor(Data));
        }
    }

    [Cmdlet("Get", "MxImgNormalizeNDArray")]
    [Alias("mx.nd.img.Normalize")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgNormalizeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Tuple<float> Mean { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Tuple<float> Std { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().Normalize(Data, Mean, Std));
        }
    }

    [Cmdlet("Get", "MxImgFlipLeftRightNDArray")]
    [Alias("mx.nd.img.FlipLeftRight")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgFlipLeftRightNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().FlipLeftRight(Data));
        }
    }

    [Cmdlet("Get", "MxImgRandomFlipLeftRightNDArray")]
    [Alias("mx.nd.img.RandomFlipLeftRight")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgRandomFlipLeftRightNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().RandomFlipLeftRight(Data));
        }
    }

    [Cmdlet("Get", "MxImgFlipTopBottomNDArray")]
    [Alias("mx.nd.img.FlipTopBottom")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgFlipTopBottomNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().FlipTopBottom(Data));
        }
    }

    [Cmdlet("Get", "MxImgRandomFlipTopBottomNDArray")]
    [Alias("mx.nd.img.RandomFlipTopBottom")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgRandomFlipTopBottomNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().RandomFlipTopBottom(Data));
        }
    }

    [Cmdlet("Get", "MxImgRandomBrightnessNDArray")]
    [Alias("mx.nd.img.RandomBrightness")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgRandomBrightnessNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float MinFactor { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float MaxFactor { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().RandomBrightness(Data, MinFactor, MaxFactor));
        }
    }

    [Cmdlet("Get", "MxImgRandomContrastNDArray")]
    [Alias("mx.nd.img.RandomContrast")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgRandomContrastNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float MinFactor { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float MaxFactor { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().RandomContrast(Data, MinFactor, MaxFactor));
        }
    }

    [Cmdlet("Get", "MxImgRandomSaturationNDArray")]
    [Alias("mx.nd.img.RandomSaturation")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgRandomSaturationNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float MinFactor { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float MaxFactor { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().RandomSaturation(Data, MinFactor, MaxFactor));
        }
    }

    [Cmdlet("Get", "MxImgRandomHueNDArray")]
    [Alias("mx.nd.img.RandomHue")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgRandomHueNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float MinFactor { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float MaxFactor { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().RandomHue(Data, MinFactor, MaxFactor));
        }
    }

    [Cmdlet("Get", "MxImgRandomColorJitterNDArray")]
    [Alias("mx.nd.img.RandomColorJitter")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgRandomColorJitterNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Brightness { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float Contrast { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float Saturation { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public float Hue { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().RandomColorJitter(Data, Brightness, Contrast, Saturation, Hue));
        }
    }

    [Cmdlet("Get", "MxImgAdjustLightingNDArray")]
    [Alias("mx.nd.img.AdjustLighting")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgAdjustLightingNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Tuple<double> Alpha { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().AdjustLighting(Data, Alpha));
        }
    }

    [Cmdlet("Get", "MxImgRandomLightingNDArray")]
    [Alias("mx.nd.img.RandomLighting")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgRandomLightingNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float AlphaStd { get; set; } = 0.05f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().RandomLighting(Data, AlphaStd));
        }
    }

    [Cmdlet("Get", "MxImgResizeNDArray")]
    [Alias("mx.nd.img.Resize")]
    [OutputType(typeof(NDArray))]
    public class GetMxImgResizeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Size { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool KeepRatio { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public int Interp { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDImgApi().Resize(Data, Size, KeepRatio, Interp));
        }
    }
}
