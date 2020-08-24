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
    [Cmdlet("Get", "MxImgCropSymbol")]
    [Alias("mx.sym.img.Crop")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgCropSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int X { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int Y { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int Width { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int Height { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().Crop(Data, X, Y, Width, Height, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgToTensorSymbol")]
    [Alias("mx.sym.img.ToTensor")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgToTensorSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().ToTensor(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgNormalizeSymbol")]
    [Alias("mx.sym.img.Normalize")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgNormalizeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Tuple<float> Mean { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Tuple<float> Std { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().Normalize(Data, Mean, Std, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgFlipLeftRightSymbol")]
    [Alias("mx.sym.img.FlipLeftRight")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgFlipLeftRightSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().FlipLeftRight(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgRandomFlipLeftRightSymbol")]
    [Alias("mx.sym.img.RandomFlipLeftRight")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgRandomFlipLeftRightSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().RandomFlipLeftRight(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgFlipTopBottomSymbol")]
    [Alias("mx.sym.img.FlipTopBottom")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgFlipTopBottomSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().FlipTopBottom(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgRandomFlipTopBottomSymbol")]
    [Alias("mx.sym.img.RandomFlipTopBottom")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgRandomFlipTopBottomSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().RandomFlipTopBottom(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgRandomBrightnessSymbol")]
    [Alias("mx.sym.img.RandomBrightness")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgRandomBrightnessSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float MinFactor { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float MaxFactor { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().RandomBrightness(Data, MinFactor, MaxFactor, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgRandomContrastSymbol")]
    [Alias("mx.sym.img.RandomContrast")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgRandomContrastSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float MinFactor { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float MaxFactor { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().RandomContrast(Data, MinFactor, MaxFactor, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgRandomSaturationSymbol")]
    [Alias("mx.sym.img.RandomSaturation")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgRandomSaturationSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float MinFactor { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float MaxFactor { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().RandomSaturation(Data, MinFactor, MaxFactor, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgRandomHueSymbol")]
    [Alias("mx.sym.img.RandomHue")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgRandomHueSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float MinFactor { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float MaxFactor { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().RandomHue(Data, MinFactor, MaxFactor, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgRandomColorJitterSymbol")]
    [Alias("mx.sym.img.RandomColorJitter")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgRandomColorJitterSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Brightness { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float Contrast { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float Saturation { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public float Hue { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().RandomColorJitter(Data, Brightness, Contrast, Saturation, Hue, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgAdjustLightingSymbol")]
    [Alias("mx.sym.img.AdjustLighting")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgAdjustLightingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Tuple<double> Alpha { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().AdjustLighting(Data, Alpha, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgRandomLightingSymbol")]
    [Alias("mx.sym.img.RandomLighting")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgRandomLightingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float AlphaStd { get; set; } = 0.05f;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().RandomLighting(Data, AlphaStd, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImgResizeSymbol")]
    [Alias("mx.sym.img.Resize")]
    [OutputType(typeof(Symbol))]
    public class GetMxImgResizeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Size { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool KeepRatio { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public int Interp { get; set; } = 1;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymImgApi().Resize(Data, Size, KeepRatio, Interp, SymbolName));
        }
    }
}
