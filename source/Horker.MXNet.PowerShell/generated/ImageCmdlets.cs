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
    [Cmdlet("New", "MxImageBrightnessJitterAug")]
    [Alias("mx.image.BrightnessJitterAug")]
    [OutputType(typeof(global::MxNet.Image.BrightnessJitterAug))]
    public class NewMxImageBrightnessJitterAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Brightness { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.BrightnessJitterAug(Brightness));
        }
    }

    [Cmdlet("New", "MxCastAug")]
    [Alias("mx.image.CastAug")]
    [OutputType(typeof(global::MxNet.Image.CastAug))]
    public class NewMxCastAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public DType Dtype { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.CastAug(Dtype));
        }
    }

    [Cmdlet("New", "MxColorJitterAug")]
    [Alias("mx.image.ColorJitterAug")]
    [OutputType(typeof(global::MxNet.Image.ColorJitterAug))]
    public class NewMxColorJitterAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Brightness { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Contrast { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float Saturation { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.ColorJitterAug(Brightness, Contrast, Saturation));
        }
    }

    [Cmdlet("New", "MxColorNormalizeAug")]
    [Alias("mx.image.ColorNormalizeAug")]
    [OutputType(typeof(global::MxNet.Image.ColorNormalizeAug))]
    public class NewMxColorNormalizeAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Mean { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Std { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.ColorNormalizeAug(Mean, Std));
        }
    }

    [Cmdlet("New", "MxContrastJitterAug")]
    [Alias("mx.image.ContrastJitterAug")]
    [OutputType(typeof(global::MxNet.Image.ContrastJitterAug))]
    public class NewMxContrastJitterAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Contrast { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.ContrastJitterAug(Contrast));
        }
    }

    [Cmdlet("New", "MxForceResizeAug")]
    [Alias("mx.image.ForceResizeAug")]
    [OutputType(typeof(global::MxNet.Image.ForceResizeAug))]
    public class NewMxForceResizeAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public (int, int) Size { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Area;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.ForceResizeAug(Size, Interp));
        }
    }

    [Cmdlet("New", "MxHorizontalFlipAug")]
    [Alias("mx.image.HorizontalFlipAug")]
    [OutputType(typeof(global::MxNet.Image.HorizontalFlipAug))]
    public class NewMxHorizontalFlipAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float P { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.HorizontalFlipAug(P));
        }
    }

    [Cmdlet("New", "MxHueJitterAug")]
    [Alias("mx.image.HueJitterAug")]
    [OutputType(typeof(global::MxNet.Image.HueJitterAug))]
    public class NewMxHueJitterAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Hue { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.HueJitterAug(Hue));
        }
    }

    [Cmdlet("New", "MxImageIter")]
    [Alias("mx.image.ImageIter")]
    [OutputType(typeof(global::MxNet.Image.ImageIter))]
    public class NewMxImageIter : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int BatchSize { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape DataShape { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int LabelWidth { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public string PathImgrec { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string PathImglist { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string PathRoot { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public string PathImgidx { get; set; } = null;

        [Parameter(Position = 7, Mandatory = false)]
        public bool Shuffle { get; set; } = false;

        [Parameter(Position = 8, Mandatory = false)]
        public int PartIndex { get; set; } = 0;

        [Parameter(Position = 9, Mandatory = false)]
        public int NumParts { get; set; } = 1;

        [Parameter(Position = 10, Mandatory = false)]
        public Augmenter[] AugList { get; set; } = null;

        [Parameter(Position = 11, Mandatory = false)]
        public (float[], string)[] Imglist { get; set; } = null;

        [Parameter(Position = 12, Mandatory = false)]
        public string DataName { get; set; } = "data";

        [Parameter(Position = 13, Mandatory = false)]
        public string LabelName { get; set; } = "softmax_label";

        [Parameter(Position = 14, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 15, Mandatory = false)]
        public string LastBatchHandle { get; set; } = "pad";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.ImageIter(BatchSize, DataShape, LabelWidth, PathImgrec, PathImglist, PathRoot, PathImgidx, Shuffle, PartIndex, NumParts, AugList, Imglist, DataName, LabelName, Dtype, LastBatchHandle));
        }
    }

    [Cmdlet("New", "MxLightingAug")]
    [Alias("mx.image.LightingAug")]
    [OutputType(typeof(global::MxNet.Image.LightingAug))]
    public class NewMxLightingAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Alphastd { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Eigval { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Eigvec { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.LightingAug(Alphastd, Eigval, Eigvec));
        }
    }

    [Cmdlet("New", "MxRandomCropAug")]
    [Alias("mx.image.RandomCropAug")]
    [OutputType(typeof(global::MxNet.Image.RandomCropAug))]
    public class NewMxRandomCropAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public (int, int) Size { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Area;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.RandomCropAug(Size, Interp));
        }
    }

    [Cmdlet("New", "MxRandomGrayAug")]
    [Alias("mx.image.RandomGrayAug")]
    [OutputType(typeof(global::MxNet.Image.RandomGrayAug))]
    public class NewMxRandomGrayAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float P { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.RandomGrayAug(P));
        }
    }

    [Cmdlet("New", "MxRandomOrderAug")]
    [Alias("mx.image.RandomOrderAug")]
    [OutputType(typeof(global::MxNet.Image.RandomOrderAug))]
    public class NewMxRandomOrderAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Augmenter[] Ts { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.RandomOrderAug(Ts));
        }
    }

    [Cmdlet("New", "MxRandomSizedCropAug")]
    [Alias("mx.image.RandomSizedCropAug")]
    [OutputType(typeof(global::MxNet.Image.RandomSizedCropAug))]
    public class NewMxRandomSizedCropAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public (int, int) Size { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public (float, float) Area { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public (float, float) Ratio { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Area;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.RandomSizedCropAug(Size, Area, Ratio, Interp));
        }
    }

    [Cmdlet("New", "MxResizeAug")]
    [Alias("mx.image.ResizeAug")]
    [OutputType(typeof(global::MxNet.Image.ResizeAug))]
    public class NewMxResizeAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Size { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public InterpolationFlags Interp { get; set; } = InterpolationFlags.Area;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.ResizeAug(Size, Interp));
        }
    }

    [Cmdlet("New", "MxSaturationJitterAug")]
    [Alias("mx.image.SaturationJitterAug")]
    [OutputType(typeof(global::MxNet.Image.SaturationJitterAug))]
    public class NewMxSaturationJitterAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Saturation { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.SaturationJitterAug(Saturation));
        }
    }

    [Cmdlet("New", "MxSequentialAug")]
    [Alias("mx.image.SequentialAug")]
    [OutputType(typeof(global::MxNet.Image.SequentialAug))]
    public class NewMxSequentialAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Augmenter[] Ts { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.SequentialAug(Ts));
        }
    }

    [Cmdlet("New", "MxDetBorrowAug")]
    [Alias("mx.image.DetBorrowAug")]
    [OutputType(typeof(global::MxNet.Image.DetBorrowAug))]
    public class NewMxDetBorrowAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Augmenter Augmenter { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.DetBorrowAug(Augmenter));
        }
    }

    [Cmdlet("New", "MxDetHorizontalFlipAug")]
    [Alias("mx.image.DetHorizontalFlipAug")]
    [OutputType(typeof(global::MxNet.Image.DetHorizontalFlipAug))]
    public class NewMxDetHorizontalFlipAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float P { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.DetHorizontalFlipAug(P));
        }
    }

    [Cmdlet("New", "MxDetRandomCropAug")]
    [Alias("mx.image.DetRandomCropAug")]
    [OutputType(typeof(global::MxNet.Image.DetRandomCropAug))]
    public class NewMxDetRandomCropAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float MinObjectCovered { get; set; } = 0.1f;

        [Parameter(Position = 1, Mandatory = false)]
        public float MinEjectCoverage { get; set; } = 0.3f;

        [Parameter(Position = 2, Mandatory = false)]
        public (float, float)? AspectRatioRange { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public (float, float)? AreaRange { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public int MaxAttempts { get; set; } = 50;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.DetRandomCropAug(MinObjectCovered, MinEjectCoverage, AspectRatioRange, AreaRange, MaxAttempts));
        }
    }

    [Cmdlet("New", "MxDetRandomPadAug")]
    [Alias("mx.image.DetRandomPadAug")]
    [OutputType(typeof(global::MxNet.Image.DetRandomPadAug))]
    public class NewMxDetRandomPadAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public (float, float)? AspectRatioRange { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public (float, float)? AreaRange { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int MaxAttempts { get; set; } = 50;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.DetRandomPadAug(AspectRatioRange, AreaRange, MaxAttempts));
        }
    }

    [Cmdlet("New", "MxDetRandomSelectAug")]
    [Alias("mx.image.DetRandomSelectAug")]
    [OutputType(typeof(global::MxNet.Image.DetRandomSelectAug))]
    public class NewMxDetRandomSelectAug : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public DetAugmenter[] AugList { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float SkipProb { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.Image.DetRandomSelectAug(AugList, SkipProb));
        }
    }
}
