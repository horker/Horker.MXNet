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
    [Cmdlet("Get", "MxImageImRead")]
    [Alias("mx.image.ImRead")]
    [OutputType(typeof(NDArray))]
    public class GetMxImageImRead : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Filename { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Flag { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool ToRgb { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.ImRead(Filename, Flag, ToRgb));
        }
    }

    [Cmdlet("Get", "MxImageImResize")]
    [Alias("mx.image.ImResize")]
    [OutputType(typeof(NDArray))]
    public class GetMxImageImResize : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Src { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int W { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int H { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Linear;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.ImResize(Src, W, H, Interp));
        }
    }

    [Cmdlet("Set", "MxImageImShow")]
    [Alias("mx.image.ImShow")]
    [OutputType(typeof(void))]
    public class SetMxImageImShow : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray X { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string Winname { get; set; } = "";

        [Parameter(Position = 2, Mandatory = false)]
        public bool Wait { get; set; } = true;

        protected override void BeginProcessing()
        {
            global::MxNet.Image.Img.ImShow(X, Winname, Wait);
        }
    }

    [Cmdlet("Set", "MxImageImDecode")]
    [Alias("mx.image.ImDecode")]
    [OutputType(typeof(NDArray))]
    public class SetMxImageImDecode : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public byte[] Buf { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Flag { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool ToRgb { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.ImDecode(Buf, Flag, ToRgb));
        }
    }

    [Cmdlet("Set", "MxImageScaleDown")]
    [Alias("mx.image.ScaleDown")]
    [OutputType(typeof((int, int)))]
    public class SetMxImageScaleDown : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public (int, int) SrcSize { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public (int, int) Size { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.ScaleDown(SrcSize, Size));
        }
    }

    [Cmdlet("Set", "MxImageCopyMakeBorder")]
    [Alias("mx.image.CopyMakeBorder")]
    [OutputType(typeof(NDArray))]
    public class SetMxImageCopyMakeBorder : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Src { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Top { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int Bot { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int Left { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int Right { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public BorderTypes Type { get; set; } = BorderTypes.Constant;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.CopyMakeBorder(Src, Top, Bot, Left, Right, Type));
        }
    }

    [Cmdlet("Set", "MxImageGetInterp")]
    [Alias("mx.image.GetInterp")]
    [OutputType(typeof(InterpolationFlags))]
    public class SetMxImageGetInterp : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public InterpolationFlags Interp { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public (int, int, int, int)? Sizes { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.GetInterp(Interp, Sizes));
        }
    }

    [Cmdlet("Set", "MxImageResizeShort")]
    [Alias("mx.image.ResizeShort")]
    [OutputType(typeof(NDArray))]
    public class SetMxImageResizeShort : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Src { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Size { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Linear;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.ResizeShort(Src, Size, Interp));
        }
    }

    [Cmdlet("Set", "MxImageFixedCrop")]
    [Alias("mx.image.FixedCrop")]
    [OutputType(typeof(NDArray))]
    public class SetMxImageFixedCrop : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Src { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int X0 { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int Y0 { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int W { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int H { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public (int, int)? Size { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Area;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.FixedCrop(Src, X0, Y0, W, H, Size, Interp));
        }
    }

    [Cmdlet("Set", "MxImageRandomCrop")]
    [Alias("mx.image.RandomCrop")]
    [OutputType(typeof((NDArray, (int, int, int, int))))]
    public class SetMxImageRandomCrop : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Src { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public (int, int) Size { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Area;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.RandomCrop(Src, Size, Interp));
        }
    }

    [Cmdlet("Set", "MxImageCenterCrop")]
    [Alias("mx.image.CenterCrop")]
    [OutputType(typeof((NDArray, (int, int, int, int))))]
    public class SetMxImageCenterCrop : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Src { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public (int, int) Size { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Area;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.CenterCrop(Src, Size, Interp));
        }
    }

    [Cmdlet("Set", "MxImageColorNormalize")]
    [Alias("mx.image.ColorNormalize")]
    [OutputType(typeof(NDArray))]
    public class SetMxImageColorNormalize : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Src { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Mean { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public NDArray Std { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.ColorNormalize(Src, Mean, Std));
        }
    }

    [Cmdlet("Set", "MxImageRandomSizeCrop")]
    [Alias("mx.image.RandomSizeCrop")]
    [OutputType(typeof((NDArray, (int, int, int, int))))]
    public class SetMxImageRandomSizeCrop : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Src { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public (int, int) Size { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public (float, float) Area { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public (float, float) Ratio { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Area;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.RandomSizeCrop(Src, Size, Area, Ratio, Interp));
        }
    }

    [Cmdlet("Set", "MxImageCreateAugmenter")]
    [Alias("mx.image.CreateAugmenter")]
    [OutputType(typeof(Augmenter))]
    public class SetMxImageCreateAugmenter : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Shape DataShape { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Resize { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public bool RandCrop { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool RandResize { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public bool RandMirror { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public NDArray Mean { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public NDArray Std { get; set; } = null;

        [Parameter(Position = 7, Mandatory = false)]
        public float Brightness { get; set; } = 0;

        [Parameter(Position = 8, Mandatory = false)]
        public float Contrast { get; set; } = 0;

        [Parameter(Position = 9, Mandatory = false)]
        public float Saturation { get; set; } = 0;

        [Parameter(Position = 10, Mandatory = false)]
        public float Hue { get; set; } = 0;

        [Parameter(Position = 11, Mandatory = false)]
        public float PcaNoise { get; set; } = 0;

        [Parameter(Position = 12, Mandatory = false)]
        public float RandGray { get; set; } = 0;

        [Parameter(Position = 13, Mandatory = false)]
        public ImgInterp InterMethod { get; set; } = ImgInterp.Area_Based;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.Image.Img.CreateAugmenter(DataShape, Resize, RandCrop, RandResize, RandMirror, Mean, Std, Brightness, Contrast, Saturation, Hue, PcaNoise, RandGray, InterMethod));
        }
    }
}
