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
    [Cmdlet("Get", "MxCustomFunctionNDArray")]
    [Alias("mx.nd.CustomFunction")]
    [OutputType(typeof(NDArray))]
    public class GetMxCustomFunctionNDArray : PSCmdlet
    {


        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.CustomFunction());
        }
    }

    [Cmdlet("Get", "MxCachedOpNDArray")]
    [Alias("mx.nd.CachedOp")]
    [OutputType(typeof(NDArray))]
    public class GetMxCachedOpNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.CachedOp(Data));
        }
    }

    [Cmdlet("Get", "MxCvimdecodeNDArray")]
    [Alias("mx.nd.Cvimdecode")]
    [OutputType(typeof(NDArray))]
    public class GetMxCvimdecodeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public byte[] Buf { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Flag { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool ToRgb { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Cvimdecode(Buf, Flag, ToRgb));
        }
    }

    [Cmdlet("Get", "MxCvimreadNDArray")]
    [Alias("mx.nd.Cvimread")]
    [OutputType(typeof(NDArray))]
    public class GetMxCvimreadNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Filename { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Flag { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool ToRgb { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Cvimread(Filename, Flag, ToRgb));
        }
    }

    [Cmdlet("Get", "MxCvimresizeNDArray")]
    [Alias("mx.nd.Cvimresize")]
    [OutputType(typeof(NDArray))]
    public class GetMxCvimresizeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int W { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int H { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public int Interp { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Cvimresize(Data, W, H, Interp));
        }
    }

    [Cmdlet("Get", "MxCvcopyMakeBorderNDArray")]
    [Alias("mx.nd.CvcopyMakeBorder")]
    [OutputType(typeof(NDArray))]
    public class GetMxCvcopyMakeBorderNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Top { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int Bot { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int Left { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int Right { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public int Type { get; set; } = 0;

        [Parameter(Position = 6, Mandatory = false)]
        public Tuple<double> Values { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.CvcopyMakeBorder(Data, Top, Bot, Left, Right, Type, Values));
        }
    }

    [Cmdlet("Get", "MxCopyToNDArray")]
    [Alias("mx.nd.CopyTo")]
    [OutputType(typeof(NDArray))]
    public class GetMxCopyToNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.CopyTo(Data));
        }
    }

    [Cmdlet("Get", "MxArrayNDArray")]
    [Alias("mx.nd.Array")]
    [OutputType(typeof(NDArray))]
    public class GetMxArrayNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public System.Array Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Array(Data, Ctx));
        }
    }

    [Cmdlet("Get", "MxArray2NDArray")]
    [Alias("mx.nd.Array2")]
    [OutputType(typeof(NDArray))]
    public class GetMxArray2NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public ndarray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Array(Data, Ctx));
        }
    }

    [Cmdlet("Get", "MxNoGradientNDArray")]
    [Alias("mx.nd.NoGradient")]
    [OutputType(typeof(NDArray))]
    public class GetMxNoGradientNDArray : PSCmdlet
    {


        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.NoGradient());
        }
    }

    [Cmdlet("Get", "MxBatchNormV1NDArray")]
    [Alias("mx.nd.BatchNormV1")]
    [OutputType(typeof(NDArray))]
    public class GetMxBatchNormV1NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Gamma { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Beta { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Eps { get; set; } = 0.001f;

        [Parameter(Position = 4, Mandatory = false)]
        public float Momentum { get; set; } = 0.9f;

        [Parameter(Position = 5, Mandatory = false)]
        public bool FixGamma { get; set; } = true;

        [Parameter(Position = 6, Mandatory = false)]
        public bool UseGlobalStats { get; set; } = false;

        [Parameter(Position = 7, Mandatory = false)]
        public bool OutputMeanVar { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BatchNormV1(Data, Gamma, Beta, Eps, Momentum, FixGamma, UseGlobalStats, OutputMeanVar));
        }
    }

    [Cmdlet("Get", "MxMpAdamwUpdateNDArray")]
    [Alias("mx.nd.MpAdamwUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxMpAdamwUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Mean { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray Var { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public NDArray Weight32 { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public NDArray RescaleGrad { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 7, Mandatory = true)]
        public float Eta { get; set; }

        [Parameter(Position = 8, Mandatory = false)]
        public float Beta1 { get; set; } = 0.9f;

        [Parameter(Position = 9, Mandatory = false)]
        public float Beta2 { get; set; } = 0.999f;

        [Parameter(Position = 10, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-08f;

        [Parameter(Position = 11, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 12, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MpAdamwUpdate(Weight, Grad, Mean, Var, Weight32, RescaleGrad, Lr, Eta, Beta1, Beta2, Epsilon, Wd, ClipGradient));
        }
    }

    [Cmdlet("Get", "MxAdamwUpdateNDArray")]
    [Alias("mx.nd.AdamwUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxAdamwUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Mean { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray Var { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public NDArray RescaleGrad { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public float Eta { get; set; }

        [Parameter(Position = 7, Mandatory = false)]
        public float Beta1 { get; set; } = 0.9f;

        [Parameter(Position = 8, Mandatory = false)]
        public float Beta2 { get; set; } = 0.999f;

        [Parameter(Position = 9, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-08f;

        [Parameter(Position = 10, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 11, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.AdamwUpdate(Weight, Grad, Mean, Var, RescaleGrad, Lr, Eta, Beta1, Beta2, Epsilon, Wd, ClipGradient));
        }
    }

    [Cmdlet("Get", "MxKhatriRaoNDArray")]
    [Alias("mx.nd.KhatriRao")]
    [OutputType(typeof(NDArray))]
    public class GetMxKhatriRaoNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Args { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.KhatriRao(Args));
        }
    }

    [Cmdlet("Get", "MxForeachNDArray")]
    [Alias("mx.nd.Foreach")]
    [OutputType(typeof(NDArray))]
    public class GetMxForeachNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Fn { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int NumOutputs { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int NumOutData { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public Tuple<double> InStateLocs { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public Tuple<double> InDataLocs { get; set; }

        [Parameter(Position = 7, Mandatory = true)]
        public Tuple<double> RemainLocs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Foreach(Fn, Data, NumArgs, NumOutputs, NumOutData, InStateLocs, InDataLocs, RemainLocs));
        }
    }

    [Cmdlet("Get", "MxWhileLoopNDArray")]
    [Alias("mx.nd.WhileLoop")]
    [OutputType(typeof(NDArray))]
    public class GetMxWhileLoopNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Cond { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Func { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int NumOutputs { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public int NumOutData { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public int MaxIterations { get; set; }

        [Parameter(Position = 7, Mandatory = true)]
        public Tuple<double> CondInputLocs { get; set; }

        [Parameter(Position = 8, Mandatory = true)]
        public Tuple<double> FuncInputLocs { get; set; }

        [Parameter(Position = 9, Mandatory = true)]
        public Tuple<double> FuncVarLocs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.WhileLoop(Cond, Func, Data, NumArgs, NumOutputs, NumOutData, MaxIterations, CondInputLocs, FuncInputLocs, FuncVarLocs));
        }
    }

    [Cmdlet("Get", "MxCondNDArray")]
    [Alias("mx.nd.Cond")]
    [OutputType(typeof(NDArray))]
    public class GetMxCondNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Cond { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray ThenBranch { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray ElseBranch { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public int NumOutputs { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public Tuple<double> CondInputLocs { get; set; }

        [Parameter(Position = 7, Mandatory = true)]
        public Tuple<double> ThenInputLocs { get; set; }

        [Parameter(Position = 8, Mandatory = true)]
        public Tuple<double> ElseInputLocs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Cond(Cond, ThenBranch, ElseBranch, Data, NumArgs, NumOutputs, CondInputLocs, ThenInputLocs, ElseInputLocs));
        }
    }

    [Cmdlet("Get", "MxCustomNDArray")]
    [Alias("mx.nd.Custom")]
    [OutputType(typeof(NDArray))]
    public class GetMxCustomNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public string OpType { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Custom(Data, OpType));
        }
    }

    [Cmdlet("Get", "MxIdentityAttachKLSparseRegNDArray")]
    [Alias("mx.nd.IdentityAttachKLSparseReg")]
    [OutputType(typeof(NDArray))]
    public class GetMxIdentityAttachKLSparseRegNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float SparsenessTarget { get; set; } = 0.1f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Penalty { get; set; } = 0.001f;

        [Parameter(Position = 3, Mandatory = false)]
        public float Momentum { get; set; } = 0.9f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.IdentityAttachKLSparseReg(Data, SparsenessTarget, Penalty, Momentum));
        }
    }

    [Cmdlet("Get", "MxLeakyReLUNDArray")]
    [Alias("mx.nd.LeakyReLU")]
    [OutputType(typeof(NDArray))]
    public class GetMxLeakyReLUNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public NDArray Gamma { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public ReluActType ActType { get; set; } = ReluActType.Leaky;

        [Parameter(Position = 3, Mandatory = false)]
        public float Slope { get; set; } = 0.25f;

        [Parameter(Position = 4, Mandatory = false)]
        public float LowerBound { get; set; } = 0.125f;

        [Parameter(Position = 5, Mandatory = false)]
        public float UpperBound { get; set; } = 0.334f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LeakyReLU(Data, Gamma, ActType, Slope, LowerBound, UpperBound));
        }
    }

    [Cmdlet("Get", "MxSoftmaxCrossEntropyNDArray")]
    [Alias("mx.nd.SoftmaxCrossEntropy")]
    [OutputType(typeof(NDArray))]
    public class GetMxSoftmaxCrossEntropyNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Label { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SoftmaxCrossEntropy(Data, Label));
        }
    }

    [Cmdlet("Get", "MxActivationNDArray")]
    [Alias("mx.nd.Activation")]
    [OutputType(typeof(NDArray))]
    public class GetMxActivationNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public ActivationType ActType { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Activation(Data, ActType));
        }
    }

    [Cmdlet("Get", "MxBatchNormNDArray")]
    [Alias("mx.nd.BatchNorm")]
    [OutputType(typeof(NDArray))]
    public class GetMxBatchNormNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Gamma { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Beta { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray MovingMean { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public NDArray MovingVar { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public double Eps { get; set; } = 0.001;

        [Parameter(Position = 6, Mandatory = false)]
        public float Momentum { get; set; } = 0.9f;

        [Parameter(Position = 7, Mandatory = false)]
        public bool FixGamma { get; set; } = true;

        [Parameter(Position = 8, Mandatory = false)]
        public bool UseGlobalStats { get; set; } = false;

        [Parameter(Position = 9, Mandatory = false)]
        public bool OutputMeanVar { get; set; } = false;

        [Parameter(Position = 10, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 11, Mandatory = false)]
        public bool CudnnOff { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BatchNorm(Data, Gamma, Beta, MovingMean, MovingVar, Eps, Momentum, FixGamma, UseGlobalStats, OutputMeanVar, Axis, CudnnOff));
        }
    }

    [Cmdlet("Get", "MxConcatNDArray")]
    [Alias("mx.nd.Concat")]
    [OutputType(typeof(NDArray))]
    public class GetMxConcatNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Dim { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Concat(Data, Dim));
        }
    }

    [Cmdlet("Get", "MxRnnParamConcatNDArray")]
    [Alias("mx.nd.RnnParamConcat")]
    [OutputType(typeof(NDArray))]
    public class GetMxRnnParamConcatNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Dim { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.RnnParamConcat(Data, NumArgs, Dim));
        }
    }

    [Cmdlet("Get", "MxConvolutionNDArray")]
    [Alias("mx.nd.Convolution")]
    [OutputType(typeof(NDArray))]
    public class GetMxConvolutionNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Bias { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Shape Kernel { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int NumFilter { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public Shape Stride { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public Shape Dilate { get; set; } = null;

        [Parameter(Position = 7, Mandatory = false)]
        public Shape Pad { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public int NumGroup { get; set; } = 1;

        [Parameter(Position = 9, Mandatory = false)]
        public ulong Workspace { get; set; } = 1024;

        [Parameter(Position = 10, Mandatory = false)]
        public bool NoBias { get; set; } = false;

        [Parameter(Position = 11, Mandatory = false)]
        public ConvolutionCudnnTune? CudnnTune { get; set; } = null;

        [Parameter(Position = 12, Mandatory = false)]
        public bool CudnnOff { get; set; } = false;

        [Parameter(Position = 13, Mandatory = false)]
        public ConvolutionLayout? Layout { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Convolution(Data, Weight, Bias, Kernel, NumFilter, Stride, Dilate, Pad, NumGroup, Workspace, NoBias, CudnnTune, CudnnOff, Layout));
        }
    }

    [Cmdlet("Get", "MxCTCLossNDArray")]
    [Alias("mx.nd.CTCLoss")]
    [OutputType(typeof(NDArray))]
    public class GetMxCTCLossNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Label { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray DataLengths { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray LabelLengths { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public bool UseDataLengths { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public bool UseLabelLengths { get; set; } = false;

        [Parameter(Position = 6, Mandatory = false)]
        public CtclossBlankLabel BlankLabel { get; set; } = CtclossBlankLabel.First;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.CTCLoss(Data, Label, DataLengths, LabelLengths, UseDataLengths, UseLabelLengths, BlankLabel));
        }
    }

    [Cmdlet("Get", "MxDeconvolutionNDArray")]
    [Alias("mx.nd.Deconvolution")]
    [OutputType(typeof(NDArray))]
    public class GetMxDeconvolutionNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Bias { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Shape Kernel { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public uint NumFilter { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public Shape Stride { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public Shape Dilate { get; set; } = null;

        [Parameter(Position = 7, Mandatory = false)]
        public Shape Pad { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public Shape Adj { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public Shape TargetShape { get; set; } = null;

        [Parameter(Position = 10, Mandatory = false)]
        public uint NumGroup { get; set; } = 1;

        [Parameter(Position = 11, Mandatory = false)]
        public ulong Workspace { get; set; } = 512;

        [Parameter(Position = 12, Mandatory = false)]
        public bool NoBias { get; set; } = true;

        [Parameter(Position = 13, Mandatory = false)]
        public DeconvolutionCudnnTune? CudnnTune { get; set; } = null;

        [Parameter(Position = 14, Mandatory = false)]
        public bool CudnnOff { get; set; } = false;

        [Parameter(Position = 15, Mandatory = false)]
        public DeconvolutionLayout? Layout { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Deconvolution(Data, Weight, Bias, Kernel, NumFilter, Stride, Dilate, Pad, Adj, TargetShape, NumGroup, Workspace, NoBias, CudnnTune, CudnnOff, Layout));
        }
    }

    [Cmdlet("Get", "MxDropoutNDArray")]
    [Alias("mx.nd.Dropout")]
    [OutputType(typeof(NDArray))]
    public class GetMxDropoutNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float P { get; set; } = 0.5f;

        [Parameter(Position = 2, Mandatory = false)]
        public DropoutMode Mode { get; set; } = DropoutMode.Training;

        [Parameter(Position = 3, Mandatory = false)]
        public Shape Axes { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public bool? CudnnOff { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Dropout(Data, P, Mode, Axes, CudnnOff));
        }
    }

    [Cmdlet("Get", "MxFullyConnectedNDArray")]
    [Alias("mx.nd.FullyConnected")]
    [OutputType(typeof(NDArray))]
    public class GetMxFullyConnectedNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Bias { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int NumHidden { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public bool NoBias { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public bool Flatten { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.FullyConnected(Data, Weight, Bias, NumHidden, NoBias, Flatten));
        }
    }

    [Cmdlet("Get", "MxLayerNormNDArray")]
    [Alias("mx.nd.LayerNorm")]
    [OutputType(typeof(NDArray))]
    public class GetMxLayerNormNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Gamma { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Beta { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 4, Mandatory = false)]
        public float Eps { get; set; } = 1e-05f;

        [Parameter(Position = 5, Mandatory = false)]
        public bool OutputMeanVar { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LayerNorm(Data, Gamma, Beta, Axis, Eps, OutputMeanVar));
        }
    }

    [Cmdlet("Get", "MxLRNNDArray")]
    [Alias("mx.nd.LRN")]
    [OutputType(typeof(NDArray))]
    public class GetMxLRNNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public uint Nsize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float Alpha { get; set; } = 0.0001f;

        [Parameter(Position = 3, Mandatory = false)]
        public float Beta { get; set; } = 0.75f;

        [Parameter(Position = 4, Mandatory = false)]
        public float Knorm { get; set; } = 2f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LRN(Data, Nsize, Alpha, Beta, Knorm));
        }
    }

    [Cmdlet("Get", "MxPoolingNDArray")]
    [Alias("mx.nd.Pooling")]
    [OutputType(typeof(NDArray))]
    public class GetMxPoolingNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Kernel { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public PoolingType PoolType { get; set; } = PoolingType.Max;

        [Parameter(Position = 3, Mandatory = false)]
        public bool GlobalPool { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public bool CudnnOff { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public PoolingConvention PoolingConvention { get; set; } = PoolingConvention.Valid;

        [Parameter(Position = 6, Mandatory = false)]
        public Shape Stride { get; set; } = null;

        [Parameter(Position = 7, Mandatory = false)]
        public Shape Pad { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public int? PValue { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public bool? CountIncludePad { get; set; } = null;

        [Parameter(Position = 10, Mandatory = false)]
        public string Layout { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Pooling(Data, Kernel, PoolType, GlobalPool, CudnnOff, PoolingConvention, Stride, Pad, PValue, CountIncludePad, Layout));
        }
    }

    [Cmdlet("Get", "MxSoftmaxNDArray")]
    [Alias("mx.nd.Softmax")]
    [OutputType(typeof(NDArray))]
    public class GetMxSoftmaxNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public double? Temperature { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Softmax(Data, Axis, Temperature, Dtype));
        }
    }

    [Cmdlet("Get", "MxSoftminNDArray")]
    [Alias("mx.nd.Softmin")]
    [OutputType(typeof(NDArray))]
    public class GetMxSoftminNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public double? Temperature { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Softmin(Data, Axis, Temperature, Dtype));
        }
    }

    [Cmdlet("Get", "MxLogSoftmaxNDArray")]
    [Alias("mx.nd.LogSoftmax")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogSoftmaxNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public double? Temperature { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LogSoftmax(Data, Axis, Temperature, Dtype));
        }
    }

    [Cmdlet("Get", "MxSoftmaxActivationNDArray")]
    [Alias("mx.nd.SoftmaxActivation")]
    [OutputType(typeof(NDArray))]
    public class GetMxSoftmaxActivationNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public SoftmaxMode Mode { get; set; } = SoftmaxMode.Instance;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SoftmaxActivation(Data, Mode));
        }
    }

    [Cmdlet("Get", "MxUpSamplingNDArray")]
    [Alias("mx.nd.UpSampling")]
    [OutputType(typeof(NDArray))]
    public class GetMxUpSamplingNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Scale { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public UpsamplingSampleType SampleType { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public int NumFilter { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public UpsamplingMultiInputMode MultiInputMode { get; set; } = UpsamplingMultiInputMode.Concat;

        [Parameter(Position = 6, Mandatory = false)]
        public ulong Workspace { get; set; } = 512;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.UpSampling(Data, Scale, SampleType, NumArgs, NumFilter, MultiInputMode, Workspace));
        }
    }

    [Cmdlet("Get", "MxSignsgdUpdateNDArray")]
    [Alias("mx.nd.SignsgdUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxSignsgdUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SignsgdUpdate(Weight, Grad, Lr, Wd, RescaleGrad, ClipGradient));
        }
    }

    [Cmdlet("Get", "MxSignumUpdateNDArray")]
    [Alias("mx.nd.SignumUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxSignumUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Mom { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public float Momentum { get; set; } = 0f;

        [Parameter(Position = 5, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 6, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 7, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 8, Mandatory = false)]
        public float WdLh { get; set; } = 0f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SignumUpdate(Weight, Grad, Mom, Lr, Momentum, Wd, RescaleGrad, ClipGradient, WdLh));
        }
    }

    [Cmdlet("Get", "MxMultiSgdUpdateNDArray")]
    [Alias("mx.nd.MultiSgdUpdate")]
    [OutputType(typeof(NDArrayList))]
    public class GetMxMultiSgdUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float[] Lrs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float[] Wds { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 4, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 5, Mandatory = false)]
        public int NumWeights { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public NDArrayList Outputs { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MultiSgdUpdate(Data, Lrs, Wds, RescaleGrad, ClipGradient, NumWeights, Outputs));
        }
    }

    [Cmdlet("Get", "MxMultiSgdMomUpdateNDArray")]
    [Alias("mx.nd.MultiSgdMomUpdate")]
    [OutputType(typeof(NDArrayList))]
    public class GetMxMultiSgdMomUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float[] Lrs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float[] Wds { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Momentum { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 6, Mandatory = false)]
        public int NumWeights { get; set; } = 1;

        [Parameter(Position = 7, Mandatory = false)]
        public NDArrayList Outputs { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MultiSgdMomUpdate(Data, Lrs, Wds, Momentum, RescaleGrad, ClipGradient, NumWeights, Outputs));
        }
    }

    [Cmdlet("Get", "MxMultiMpSgdUpdateNDArray")]
    [Alias("mx.nd.MultiMpSgdUpdate")]
    [OutputType(typeof(NDArrayList))]
    public class GetMxMultiMpSgdUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float[] Lrs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float[] Wds { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 4, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 5, Mandatory = false)]
        public int NumWeights { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public NDArrayList Outputs { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MultiMpSgdUpdate(Data, Lrs, Wds, RescaleGrad, ClipGradient, NumWeights, Outputs));
        }
    }

    [Cmdlet("Get", "MxMultiMpSgdMomUpdateNDArray")]
    [Alias("mx.nd.MultiMpSgdMomUpdate")]
    [OutputType(typeof(NDArrayList))]
    public class GetMxMultiMpSgdMomUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float[] Lrs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float[] Wds { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Momentum { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 6, Mandatory = false)]
        public int NumWeights { get; set; } = 1;

        [Parameter(Position = 7, Mandatory = false)]
        public NDArrayList Outputs { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MultiMpSgdMomUpdate(Data, Lrs, Wds, Momentum, RescaleGrad, ClipGradient, NumWeights, Outputs));
        }
    }

    [Cmdlet("Get", "MxSgdUpdateNDArray")]
    [Alias("mx.nd.SgdUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxSgdUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 6, Mandatory = false)]
        public bool LazyUpdate { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SgdUpdate(Weight, Grad, Lr, Wd, RescaleGrad, ClipGradient, LazyUpdate));
        }
    }

    [Cmdlet("Get", "MxSgdMomUpdateNDArray")]
    [Alias("mx.nd.SgdMomUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxSgdMomUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Mom { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public float Momentum { get; set; } = 0f;

        [Parameter(Position = 5, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 6, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 7, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 8, Mandatory = false)]
        public bool LazyUpdate { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SgdMomUpdate(Weight, Grad, Mom, Lr, Momentum, Wd, RescaleGrad, ClipGradient, LazyUpdate));
        }
    }

    [Cmdlet("Get", "MxMpSgdUpdateNDArray")]
    [Alias("mx.nd.MpSgdUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxMpSgdUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Weight32 { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 5, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 6, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 7, Mandatory = false)]
        public bool LazyUpdate { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MpSgdUpdate(Weight, Grad, Weight32, Lr, Wd, RescaleGrad, ClipGradient, LazyUpdate));
        }
    }

    [Cmdlet("Get", "MxMpSgdMomUpdateNDArray")]
    [Alias("mx.nd.MpSgdMomUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxMpSgdMomUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Mom { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray Weight32 { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public float Momentum { get; set; } = 0f;

        [Parameter(Position = 6, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 7, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 8, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 9, Mandatory = false)]
        public bool LazyUpdate { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MpSgdMomUpdate(Weight, Grad, Mom, Weight32, Lr, Momentum, Wd, RescaleGrad, ClipGradient, LazyUpdate));
        }
    }

    [Cmdlet("Get", "MxFtmlUpdateNDArray")]
    [Alias("mx.nd.FtmlUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxFtmlUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray D { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray V { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public NDArray Z { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public int T { get; set; }

        [Parameter(Position = 7, Mandatory = false)]
        public float Beta1 { get; set; } = 0.6f;

        [Parameter(Position = 8, Mandatory = false)]
        public float Beta2 { get; set; } = 0.999f;

        [Parameter(Position = 9, Mandatory = false)]
        public double Epsilon { get; set; } = 1e-08;

        [Parameter(Position = 10, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 11, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 12, Mandatory = false)]
        public float ClipGrad { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.FtmlUpdate(Weight, Grad, D, V, Z, Lr, T, Beta1, Beta2, Epsilon, Wd, RescaleGrad, ClipGrad));
        }
    }

    [Cmdlet("Get", "MxAdamUpdateNDArray")]
    [Alias("mx.nd.AdamUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxAdamUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Mean { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray Var { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public float Beta1 { get; set; } = 0.9f;

        [Parameter(Position = 6, Mandatory = false)]
        public float Beta2 { get; set; } = 0.999f;

        [Parameter(Position = 7, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-08f;

        [Parameter(Position = 8, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 9, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 10, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 11, Mandatory = false)]
        public bool LazyUpdate { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.AdamUpdate(Weight, Grad, Mean, Var, Lr, Beta1, Beta2, Epsilon, Wd, RescaleGrad, ClipGradient, LazyUpdate));
        }
    }

    [Cmdlet("Get", "MxRmspropUpdateNDArray")]
    [Alias("mx.nd.RmspropUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxRmspropUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray N { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public float Gamma1 { get; set; } = 0.95f;

        [Parameter(Position = 5, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-08f;

        [Parameter(Position = 6, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 7, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 8, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 9, Mandatory = false)]
        public float ClipWeights { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.RmspropUpdate(Weight, Grad, N, Lr, Gamma1, Epsilon, Wd, RescaleGrad, ClipGradient, ClipWeights));
        }
    }

    [Cmdlet("Get", "MxRmspropalexUpdateNDArray")]
    [Alias("mx.nd.RmspropalexUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxRmspropalexUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray N { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray G { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public NDArray Delta { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 6, Mandatory = false)]
        public float Gamma1 { get; set; } = 0.95f;

        [Parameter(Position = 7, Mandatory = false)]
        public float Gamma2 { get; set; } = 0.9f;

        [Parameter(Position = 8, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-08f;

        [Parameter(Position = 9, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 10, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 11, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 12, Mandatory = false)]
        public float ClipWeights { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.RmspropalexUpdate(Weight, Grad, N, G, Delta, Lr, Gamma1, Gamma2, Epsilon, Wd, RescaleGrad, ClipGradient, ClipWeights));
        }
    }

    [Cmdlet("Get", "MxFtrlUpdateNDArray")]
    [Alias("mx.nd.FtrlUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxFtrlUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Z { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray N { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public float Lamda1 { get; set; } = 0.01f;

        [Parameter(Position = 6, Mandatory = false)]
        public float Beta { get; set; } = 1f;

        [Parameter(Position = 7, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 8, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 9, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.FtrlUpdate(Weight, Grad, Z, N, Lr, Lamda1, Beta, Wd, RescaleGrad, ClipGradient));
        }
    }

    [Cmdlet("Get", "MxNAGMomUpdateNDArray")]
    [Alias("mx.nd.NAGMomUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxNAGMomUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Mom { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public float Momentum { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 6, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 7, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.NAGMomUpdate(Weight, Grad, Mom, Lr, Momentum, Wd, RescaleGrad, ClipGradient));
        }
    }

    [Cmdlet("Get", "MxMPNAGMomUpdateNDArray")]
    [Alias("mx.nd.MPNAGMomUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxMPNAGMomUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Mom { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray Weight32 { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public float Momentum { get; set; } = 0;

        [Parameter(Position = 6, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 7, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 8, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MPNAGMomUpdate(Weight, Grad, Mom, Weight32, Lr, Momentum, Wd, RescaleGrad, ClipGradient));
        }
    }

    [Cmdlet("Get", "MxSparseAdagradUpdateNDArray")]
    [Alias("mx.nd.SparseAdagradUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxSparseAdagradUpdateNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray History { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-07f;

        [Parameter(Position = 5, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 6, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 7, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SparseAdagradUpdate(Weight, Grad, History, Lr, Epsilon, Wd, RescaleGrad, ClipGradient));
        }
    }

    [Cmdlet("Get", "MxPadNDArray")]
    [Alias("mx.nd.Pad")]
    [OutputType(typeof(NDArray))]
    public class GetMxPadNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public PadMode Mode { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape PadWidth { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public double ConstantValue { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Pad(Data, Mode, PadWidth, ConstantValue));
        }
    }

    [Cmdlet("Get", "MxFlattenNDArray")]
    [Alias("mx.nd.Flatten")]
    [OutputType(typeof(NDArray))]
    public class GetMxFlattenNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Flatten(Data));
        }
    }

    [Cmdlet("Get", "MxSampleUniformNDArray")]
    [Alias("mx.nd.SampleUniform")]
    [OutputType(typeof(NDArray))]
    public class GetMxSampleUniformNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Low { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray High { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SampleUniform(Low, High, Shape, Dtype));
        }
    }

    [Cmdlet("Get", "MxSampleNormalNDArray")]
    [Alias("mx.nd.SampleNormal")]
    [OutputType(typeof(NDArray))]
    public class GetMxSampleNormalNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Mu { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Sigma { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SampleNormal(Mu, Sigma, Shape, Dtype));
        }
    }

    [Cmdlet("Get", "MxSampleGammaNDArray")]
    [Alias("mx.nd.SampleGamma")]
    [OutputType(typeof(NDArray))]
    public class GetMxSampleGammaNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Alpha { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Beta { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SampleGamma(Alpha, Beta, Shape, Dtype));
        }
    }

    [Cmdlet("Get", "MxSampleExponentialNDArray")]
    [Alias("mx.nd.SampleExponential")]
    [OutputType(typeof(NDArray))]
    public class GetMxSampleExponentialNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lam { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SampleExponential(Lam, Shape, Dtype));
        }
    }

    [Cmdlet("Get", "MxSamplePoissonNDArray")]
    [Alias("mx.nd.SamplePoisson")]
    [OutputType(typeof(NDArray))]
    public class GetMxSamplePoissonNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lam { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SamplePoisson(Lam, Shape, Dtype));
        }
    }

    [Cmdlet("Get", "MxSampleNegativeBinomialNDArray")]
    [Alias("mx.nd.SampleNegativeBinomial")]
    [OutputType(typeof(NDArray))]
    public class GetMxSampleNegativeBinomialNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray K { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray P { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SampleNegativeBinomial(K, P, Shape, Dtype));
        }
    }

    [Cmdlet("Get", "MxSampleGeneralizedNegativeBinomialNDArray")]
    [Alias("mx.nd.SampleGeneralizedNegativeBinomial")]
    [OutputType(typeof(NDArray))]
    public class GetMxSampleGeneralizedNegativeBinomialNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Mu { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Alpha { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SampleGeneralizedNegativeBinomial(Mu, Alpha, Shape, Dtype));
        }
    }

    [Cmdlet("Get", "MxSampleMultinomialNDArray")]
    [Alias("mx.nd.SampleMultinomial")]
    [OutputType(typeof(NDArray))]
    public class GetMxSampleMultinomialNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool GetProb { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SampleMultinomial(Data, Shape, GetProb, Dtype));
        }
    }

    [Cmdlet("Get", "MxShuffleNDArray")]
    [Alias("mx.nd.Shuffle")]
    [OutputType(typeof(NDArray))]
    public class GetMxShuffleNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Shuffle(Data));
        }
    }

    [Cmdlet("Get", "MxSampleUniqueZipfianNDArray")]
    [Alias("mx.nd.SampleUniqueZipfian")]
    [OutputType(typeof(NDArray))]
    public class GetMxSampleUniqueZipfianNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int RangeMax { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SampleUniqueZipfian(RangeMax, Shape));
        }
    }

    [Cmdlet("Get", "MxLinearRegressionOutputNDArray")]
    [Alias("mx.nd.LinearRegressionOutput")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinearRegressionOutputNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float GradScale { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinearRegressionOutput(Data, Label, GradScale));
        }
    }

    [Cmdlet("Get", "MxMAERegressionOutputNDArray")]
    [Alias("mx.nd.MAERegressionOutput")]
    [OutputType(typeof(NDArray))]
    public class GetMxMAERegressionOutputNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float GradScale { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MAERegressionOutput(Data, Label, GradScale));
        }
    }

    [Cmdlet("Get", "MxLogisticRegressionOutputNDArray")]
    [Alias("mx.nd.LogisticRegressionOutput")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogisticRegressionOutputNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float GradScale { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LogisticRegressionOutput(Data, Label, GradScale));
        }
    }

    [Cmdlet("Get", "MxRNNNDArray")]
    [Alias("mx.nd.RNN")]
    [OutputType(typeof(NDArray))]
    public class GetMxRNNNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Parameters { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray State { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray StateCell { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public uint StateSize { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public uint NumLayers { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public RNNMode Mode { get; set; }

        [Parameter(Position = 7, Mandatory = false)]
        public bool Bidirectional { get; set; } = false;

        [Parameter(Position = 8, Mandatory = false)]
        public float P { get; set; } = 0f;

        [Parameter(Position = 9, Mandatory = false)]
        public bool StateOutputs { get; set; } = false;

        [Parameter(Position = 10, Mandatory = false)]
        public int? ProjectionSize { get; set; } = null;

        [Parameter(Position = 11, Mandatory = false)]
        public double? LstmStateClipMin { get; set; } = null;

        [Parameter(Position = 12, Mandatory = false)]
        public double? LstmStateClipMax { get; set; } = null;

        [Parameter(Position = 13, Mandatory = false)]
        public bool LstmStateClipNan { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.RNN(Data, Parameters, State, StateCell, StateSize, NumLayers, Mode, Bidirectional, P, StateOutputs, ProjectionSize, LstmStateClipMin, LstmStateClipMax, LstmStateClipNan));
        }
    }

    [Cmdlet("Get", "MxSliceChannelNDArray")]
    [Alias("mx.nd.SliceChannel")]
    [OutputType(typeof(NDArray))]
    public class GetMxSliceChannelNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumOutputs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public bool SqueezeAxis { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SliceChannel(Data, NumOutputs, Axis, SqueezeAxis));
        }
    }

    [Cmdlet("Get", "MxSoftmaxOutputNDArray")]
    [Alias("mx.nd.SoftmaxOutput")]
    [OutputType(typeof(NDArray))]
    public class GetMxSoftmaxOutputNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float GradScale { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public float IgnoreLabel { get; set; } = -1f;

        [Parameter(Position = 4, Mandatory = false)]
        public bool MultiOutput { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public bool UseIgnore { get; set; } = false;

        [Parameter(Position = 6, Mandatory = false)]
        public bool PreserveShape { get; set; } = false;

        [Parameter(Position = 7, Mandatory = false)]
        public SoftmaxoutputNormalization Normalization { get; set; } = SoftmaxoutputNormalization.Null;

        [Parameter(Position = 8, Mandatory = false)]
        public bool OutGrad { get; set; } = false;

        [Parameter(Position = 9, Mandatory = false)]
        public float SmoothAlpha { get; set; } = 0f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SoftmaxOutput(Data, Label, GradScale, IgnoreLabel, MultiOutput, UseIgnore, PreserveShape, Normalization, OutGrad, SmoothAlpha));
        }
    }

    [Cmdlet("Get", "MxSwapAxisNDArray")]
    [Alias("mx.nd.SwapAxis")]
    [OutputType(typeof(NDArray))]
    public class GetMxSwapAxisNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public uint Dim1 { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public uint Dim2 { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SwapAxis(Data, Dim1, Dim2));
        }
    }

    [Cmdlet("Get", "MxArgmaxNDArray")]
    [Alias("mx.nd.Argmax")]
    [OutputType(typeof(NDArray))]
    public class GetMxArgmaxNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Argmax(Data, Axis, Keepdims));
        }
    }

    [Cmdlet("Get", "MxArgminNDArray")]
    [Alias("mx.nd.Argmin")]
    [OutputType(typeof(NDArray))]
    public class GetMxArgminNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Argmin(Data, Axis, Keepdims));
        }
    }

    [Cmdlet("Get", "MxArgmaxChannelNDArray")]
    [Alias("mx.nd.ArgmaxChannel")]
    [OutputType(typeof(NDArray))]
    public class GetMxArgmaxChannelNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ArgmaxChannel(Data));
        }
    }

    [Cmdlet("Get", "MxPickNDArray")]
    [Alias("mx.nd.Pick")]
    [OutputType(typeof(NDArray))]
    public class GetMxPickNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Index { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int? Axis { get; set; } = -1;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public PickMode Mode { get; set; } = PickMode.Clip;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Pick(Data, Index, Axis, Keepdims, Mode));
        }
    }

    [Cmdlet("Get", "MxSumNDArray")]
    [Alias("mx.nd.Sum")]
    [OutputType(typeof(NDArray))]
    public class GetMxSumNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Sum(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxSum2NDArray")]
    [Alias("mx.nd.Sum2")]
    [OutputType(typeof(NDArray))]
    public class GetMxSum2NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Axis { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Sum(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxMeanNDArray")]
    [Alias("mx.nd.Mean")]
    [OutputType(typeof(NDArray))]
    public class GetMxMeanNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Mean(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxMean2NDArray")]
    [Alias("mx.nd.Mean2")]
    [OutputType(typeof(NDArray))]
    public class GetMxMean2NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Axis { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Mean(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxProdNDArray")]
    [Alias("mx.nd.Prod")]
    [OutputType(typeof(NDArray))]
    public class GetMxProdNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Prod(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxNansumNDArray")]
    [Alias("mx.nd.Nansum")]
    [OutputType(typeof(NDArray))]
    public class GetMxNansumNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Nansum(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxNanprodNDArray")]
    [Alias("mx.nd.Nanprod")]
    [OutputType(typeof(NDArray))]
    public class GetMxNanprodNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Nanprod(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxMaxNDArray")]
    [Alias("mx.nd.Max")]
    [OutputType(typeof(NDArray))]
    public class GetMxMaxNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Max(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxMinNDArray")]
    [Alias("mx.nd.Min")]
    [OutputType(typeof(NDArray))]
    public class GetMxMinNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Min(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxBroadcastAxisNDArray")]
    [Alias("mx.nd.BroadcastAxis")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastAxisNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Size { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastAxis(Data, Axis, Size));
        }
    }

    [Cmdlet("Get", "MxBroadcastToNDArray")]
    [Alias("mx.nd.BroadcastTo")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastToNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastTo(Data, Shape));
        }
    }

    [Cmdlet("Get", "MxBroadcastLikeNDArray")]
    [Alias("mx.nd.BroadcastLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape LhsAxes { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Shape RhsAxes { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastLike(Lhs, Rhs, LhsAxes, RhsAxes));
        }
    }

    [Cmdlet("Get", "MxNormNDArray")]
    [Alias("mx.nd.Norm")]
    [OutputType(typeof(NDArray))]
    public class GetMxNormNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Ord { get; set; } = 2;

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public NormOutDtype? OutDtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Norm(Data, Ord, Axis, OutDtype, Keepdims));
        }
    }

    [Cmdlet("Get", "MxCastStorageNDArray")]
    [Alias("mx.nd.CastStorage")]
    [OutputType(typeof(NDArray))]
    public class GetMxCastStorageNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public StorageStype Stype { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.CastStorage(Data, Stype));
        }
    }

    [Cmdlet("Get", "MxWhereNDArray")]
    [Alias("mx.nd.Where")]
    [OutputType(typeof(NDArray))]
    public class GetMxWhereNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Condition { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public NDArray X { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public NDArray Y { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Where(Condition, X, Y));
        }
    }

    [Cmdlet("Get", "MxDiagNDArray")]
    [Alias("mx.nd.Diag")]
    [OutputType(typeof(NDArray))]
    public class GetMxDiagNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int K { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis1 { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public int Axis2 { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Diag(Data, K, Axis1, Axis2));
        }
    }

    [Cmdlet("Get", "MxDotNDArray")]
    [Alias("mx.nd.Dot")]
    [OutputType(typeof(NDArray))]
    public class GetMxDotNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool TransposeA { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool TransposeB { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public DotForwardStype? ForwardStype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Dot(Lhs, Rhs, TransposeA, TransposeB, ForwardStype));
        }
    }

    [Cmdlet("Get", "MxBatchDotNDArray")]
    [Alias("mx.nd.BatchDot")]
    [OutputType(typeof(NDArray))]
    public class GetMxBatchDotNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool TransposeA { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool TransposeB { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public BatchDotForwardStype? ForwardStype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BatchDot(Lhs, Rhs, TransposeA, TransposeB, ForwardStype));
        }
    }

    [Cmdlet("Get", "MxBroadcastAddNDArray")]
    [Alias("mx.nd.BroadcastAdd")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastAddNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastAdd(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastSubNDArray")]
    [Alias("mx.nd.BroadcastSub")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastSubNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastSub(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastMulNDArray")]
    [Alias("mx.nd.BroadcastMul")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastMulNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastMul(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastDivNDArray")]
    [Alias("mx.nd.BroadcastDiv")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastDivNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastDiv(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastModNDArray")]
    [Alias("mx.nd.BroadcastMod")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastModNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastMod(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastPowerNDArray")]
    [Alias("mx.nd.BroadcastPower")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastPowerNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastPower(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastMaximumNDArray")]
    [Alias("mx.nd.BroadcastMaximum")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastMaximumNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastMaximum(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastMinimumNDArray")]
    [Alias("mx.nd.BroadcastMinimum")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastMinimumNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastMinimum(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastHypotNDArray")]
    [Alias("mx.nd.BroadcastHypot")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastHypotNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastHypot(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastEqualNDArray")]
    [Alias("mx.nd.BroadcastEqual")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastEqualNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastEqual(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastNotEqualNDArray")]
    [Alias("mx.nd.BroadcastNotEqual")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastNotEqualNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastNotEqual(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastGreaterNDArray")]
    [Alias("mx.nd.BroadcastGreater")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastGreaterNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastGreater(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastGreaterEqualNDArray")]
    [Alias("mx.nd.BroadcastGreaterEqual")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastGreaterEqualNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastGreaterEqual(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastLesserNDArray")]
    [Alias("mx.nd.BroadcastLesser")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastLesserNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastLesser(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastLesserEqualNDArray")]
    [Alias("mx.nd.BroadcastLesserEqual")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastLesserEqualNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastLesserEqual(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastLogicalAndNDArray")]
    [Alias("mx.nd.BroadcastLogicalAnd")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastLogicalAndNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastLogicalAnd(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastLogicalOrNDArray")]
    [Alias("mx.nd.BroadcastLogicalOr")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastLogicalOrNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastLogicalOr(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxBroadcastLogicalXorNDArray")]
    [Alias("mx.nd.BroadcastLogicalXor")]
    [OutputType(typeof(NDArray))]
    public class GetMxBroadcastLogicalXorNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BroadcastLogicalXor(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxElemwiseAddNDArray")]
    [Alias("mx.nd.ElemwiseAdd")]
    [OutputType(typeof(NDArray))]
    public class GetMxElemwiseAddNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ElemwiseAdd(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxGradAddNDArray")]
    [Alias("mx.nd.GradAdd")]
    [OutputType(typeof(NDArray))]
    public class GetMxGradAddNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.GradAdd(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxElemwiseSubNDArray")]
    [Alias("mx.nd.ElemwiseSub")]
    [OutputType(typeof(NDArray))]
    public class GetMxElemwiseSubNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ElemwiseSub(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxElemwiseMulNDArray")]
    [Alias("mx.nd.ElemwiseMul")]
    [OutputType(typeof(NDArray))]
    public class GetMxElemwiseMulNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ElemwiseMul(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxElemwiseDivNDArray")]
    [Alias("mx.nd.ElemwiseDiv")]
    [OutputType(typeof(NDArray))]
    public class GetMxElemwiseDivNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ElemwiseDiv(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxModNDArray")]
    [Alias("mx.nd.Mod")]
    [OutputType(typeof(NDArray))]
    public class GetMxModNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Mod(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxPowerNDArray")]
    [Alias("mx.nd.Power")]
    [OutputType(typeof(NDArray))]
    public class GetMxPowerNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Power(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxMaximumNDArray")]
    [Alias("mx.nd.Maximum")]
    [OutputType(typeof(NDArray))]
    public class GetMxMaximumNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Maximum(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxMinimumNDArray")]
    [Alias("mx.nd.Minimum")]
    [OutputType(typeof(NDArray))]
    public class GetMxMinimumNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Minimum(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxHypotNDArray")]
    [Alias("mx.nd.Hypot")]
    [OutputType(typeof(NDArray))]
    public class GetMxHypotNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Hypot(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxEqualNDArray")]
    [Alias("mx.nd.Equal")]
    [OutputType(typeof(NDArray))]
    public class GetMxEqualNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Equal(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxNotEqualNDArray")]
    [Alias("mx.nd.NotEqual")]
    [OutputType(typeof(NDArray))]
    public class GetMxNotEqualNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.NotEqual(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxGreaterNDArray")]
    [Alias("mx.nd.Greater")]
    [OutputType(typeof(NDArray))]
    public class GetMxGreaterNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Greater(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxGreaterEqualNDArray")]
    [Alias("mx.nd.GreaterEqual")]
    [OutputType(typeof(NDArray))]
    public class GetMxGreaterEqualNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.GreaterEqual(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxLesserNDArray")]
    [Alias("mx.nd.Lesser")]
    [OutputType(typeof(NDArray))]
    public class GetMxLesserNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Lesser(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxLesserEqualNDArray")]
    [Alias("mx.nd.LesserEqual")]
    [OutputType(typeof(NDArray))]
    public class GetMxLesserEqualNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LesserEqual(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxLogicalAndNDArray")]
    [Alias("mx.nd.LogicalAnd")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogicalAndNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LogicalAnd(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxLogicalOrNDArray")]
    [Alias("mx.nd.LogicalOr")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogicalOrNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LogicalOr(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxLogicalXorNDArray")]
    [Alias("mx.nd.LogicalXor")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogicalXorNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LogicalXor(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxPlusScalarNDArray")]
    [Alias("mx.nd.PlusScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxPlusScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.PlusScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxMinusScalarNDArray")]
    [Alias("mx.nd.MinusScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxMinusScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MinusScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxRminusScalarNDArray")]
    [Alias("mx.nd.RminusScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxRminusScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.RminusScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxMulScalarNDArray")]
    [Alias("mx.nd.MulScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxMulScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MulScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxDivScalarNDArray")]
    [Alias("mx.nd.DivScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxDivScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.DivScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxRdivScalarNDArray")]
    [Alias("mx.nd.RdivScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxRdivScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.RdivScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxModScalarNDArray")]
    [Alias("mx.nd.ModScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxModScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ModScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxRmodScalarNDArray")]
    [Alias("mx.nd.RmodScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxRmodScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.RmodScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxMaximumScalarNDArray")]
    [Alias("mx.nd.MaximumScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxMaximumScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MaximumScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxMinimumScalarNDArray")]
    [Alias("mx.nd.MinimumScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxMinimumScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MinimumScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxPowerScalarNDArray")]
    [Alias("mx.nd.PowerScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxPowerScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.PowerScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxRpowerScalarNDArray")]
    [Alias("mx.nd.RpowerScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxRpowerScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.RpowerScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxHypotScalarNDArray")]
    [Alias("mx.nd.HypotScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxHypotScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.HypotScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxSmoothL1NDArray")]
    [Alias("mx.nd.SmoothL1")]
    [OutputType(typeof(NDArray))]
    public class GetMxSmoothL1NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SmoothL1(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxEqualScalarNDArray")]
    [Alias("mx.nd.EqualScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxEqualScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.EqualScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxNotEqualScalarNDArray")]
    [Alias("mx.nd.NotEqualScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxNotEqualScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.NotEqualScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxGreaterScalarNDArray")]
    [Alias("mx.nd.GreaterScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxGreaterScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.GreaterScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxGreaterEqualScalarNDArray")]
    [Alias("mx.nd.GreaterEqualScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxGreaterEqualScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.GreaterEqualScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxLesserScalarNDArray")]
    [Alias("mx.nd.LesserScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxLesserScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LesserScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxLesserEqualScalarNDArray")]
    [Alias("mx.nd.LesserEqualScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxLesserEqualScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LesserEqualScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxLogicalAndScalarNDArray")]
    [Alias("mx.nd.LogicalAndScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogicalAndScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LogicalAndScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxLogicalOrScalarNDArray")]
    [Alias("mx.nd.LogicalOrScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogicalOrScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LogicalOrScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxLogicalXorScalarNDArray")]
    [Alias("mx.nd.LogicalXorScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogicalXorScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LogicalXorScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxScatterElemwiseDivNDArray")]
    [Alias("mx.nd.ScatterElemwiseDiv")]
    [OutputType(typeof(NDArray))]
    public class GetMxScatterElemwiseDivNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ScatterElemwiseDiv(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxScatterPlusScalarNDArray")]
    [Alias("mx.nd.ScatterPlusScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxScatterPlusScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ScatterPlusScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxScatterMinusScalarNDArray")]
    [Alias("mx.nd.ScatterMinusScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxScatterMinusScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ScatterMinusScalar(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxAddNNDArray")]
    [Alias("mx.nd.AddN")]
    [OutputType(typeof(NDArray))]
    public class GetMxAddNNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Args { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.AddN(Args));
        }
    }

    [Cmdlet("Get", "MxReluNDArray")]
    [Alias("mx.nd.Relu")]
    [OutputType(typeof(NDArray))]
    public class GetMxReluNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Relu(Data));
        }
    }

    [Cmdlet("Get", "MxSigmoidNDArray")]
    [Alias("mx.nd.Sigmoid")]
    [OutputType(typeof(NDArray))]
    public class GetMxSigmoidNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Sigmoid(Data));
        }
    }

    [Cmdlet("Get", "MxHardSigmoidNDArray")]
    [Alias("mx.nd.HardSigmoid")]
    [OutputType(typeof(NDArray))]
    public class GetMxHardSigmoidNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Alpha { get; set; } = 0.2f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Beta { get; set; } = 0.5f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.HardSigmoid(Data, Alpha, Beta));
        }
    }

    [Cmdlet("Get", "MxSoftsignNDArray")]
    [Alias("mx.nd.Softsign")]
    [OutputType(typeof(NDArray))]
    public class GetMxSoftsignNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Softsign(Data));
        }
    }

    [Cmdlet("Get", "MxCopyNDArray")]
    [Alias("mx.nd.Copy")]
    [OutputType(typeof(NDArray))]
    public class GetMxCopyNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Copy(Data));
        }
    }

    [Cmdlet("Get", "MxBlockGradNDArray")]
    [Alias("mx.nd.BlockGrad")]
    [OutputType(typeof(NDArray))]
    public class GetMxBlockGradNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BlockGrad(Data));
        }
    }

    [Cmdlet("Get", "MxMakeLossNDArray")]
    [Alias("mx.nd.MakeLoss")]
    [OutputType(typeof(NDArray))]
    public class GetMxMakeLossNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MakeLoss(Data));
        }
    }

    [Cmdlet("Get", "MxIdentityWithAttrLikeRhsNDArray")]
    [Alias("mx.nd.IdentityWithAttrLikeRhs")]
    [OutputType(typeof(NDArray))]
    public class GetMxIdentityWithAttrLikeRhsNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.IdentityWithAttrLikeRhs(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxReshapeLikeNDArray")]
    [Alias("mx.nd.ReshapeLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxReshapeLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ReshapeLike(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxShapeArrayNDArray")]
    [Alias("mx.nd.ShapeArray")]
    [OutputType(typeof(NDArray))]
    public class GetMxShapeArrayNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? LhsBegin { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int? LhsEnd { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public int? RhsBegin { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public int? RhsEnd { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ShapeArray(Data, LhsBegin, LhsEnd, RhsBegin, RhsEnd));
        }
    }

    [Cmdlet("Get", "MxSizeArrayNDArray")]
    [Alias("mx.nd.SizeArray")]
    [OutputType(typeof(NDArray))]
    public class GetMxSizeArrayNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SizeArray(Data));
        }
    }

    [Cmdlet("Get", "MxCastNDArray")]
    [Alias("mx.nd.Cast")]
    [OutputType(typeof(NDArray))]
    public class GetMxCastNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public DType Dtype { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Cast(Data, Dtype));
        }
    }

    [Cmdlet("Get", "MxNegativeNDArray")]
    [Alias("mx.nd.Negative")]
    [OutputType(typeof(NDArray))]
    public class GetMxNegativeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Negative(Data));
        }
    }

    [Cmdlet("Get", "MxReciprocalNDArray")]
    [Alias("mx.nd.Reciprocal")]
    [OutputType(typeof(NDArray))]
    public class GetMxReciprocalNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Reciprocal(Data));
        }
    }

    [Cmdlet("Get", "MxAbsNDArray")]
    [Alias("mx.nd.Abs")]
    [OutputType(typeof(NDArray))]
    public class GetMxAbsNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Abs(Data));
        }
    }

    [Cmdlet("Get", "MxSignNDArray")]
    [Alias("mx.nd.Sign")]
    [OutputType(typeof(NDArray))]
    public class GetMxSignNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Sign(Data));
        }
    }

    [Cmdlet("Get", "MxRoundNDArray")]
    [Alias("mx.nd.Round")]
    [OutputType(typeof(NDArray))]
    public class GetMxRoundNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Round(Data));
        }
    }

    [Cmdlet("Get", "MxRintNDArray")]
    [Alias("mx.nd.Rint")]
    [OutputType(typeof(NDArray))]
    public class GetMxRintNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Rint(Data));
        }
    }

    [Cmdlet("Get", "MxCeilNDArray")]
    [Alias("mx.nd.Ceil")]
    [OutputType(typeof(NDArray))]
    public class GetMxCeilNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Ceil(Data));
        }
    }

    [Cmdlet("Get", "MxFloorNDArray")]
    [Alias("mx.nd.Floor")]
    [OutputType(typeof(NDArray))]
    public class GetMxFloorNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Floor(Data));
        }
    }

    [Cmdlet("Get", "MxTruncNDArray")]
    [Alias("mx.nd.Trunc")]
    [OutputType(typeof(NDArray))]
    public class GetMxTruncNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Trunc(Data));
        }
    }

    [Cmdlet("Get", "MxFixNDArray")]
    [Alias("mx.nd.Fix")]
    [OutputType(typeof(NDArray))]
    public class GetMxFixNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Fix(Data));
        }
    }

    [Cmdlet("Get", "MxSquareNDArray")]
    [Alias("mx.nd.Square")]
    [OutputType(typeof(NDArray))]
    public class GetMxSquareNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Square(Data));
        }
    }

    [Cmdlet("Get", "MxSqrtNDArray")]
    [Alias("mx.nd.Sqrt")]
    [OutputType(typeof(NDArray))]
    public class GetMxSqrtNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Sqrt(Data));
        }
    }

    [Cmdlet("Get", "MxRsqrtNDArray")]
    [Alias("mx.nd.Rsqrt")]
    [OutputType(typeof(NDArray))]
    public class GetMxRsqrtNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Rsqrt(Data));
        }
    }

    [Cmdlet("Get", "MxCbrtNDArray")]
    [Alias("mx.nd.Cbrt")]
    [OutputType(typeof(NDArray))]
    public class GetMxCbrtNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Cbrt(Data));
        }
    }

    [Cmdlet("Get", "MxErfNDArray")]
    [Alias("mx.nd.Erf")]
    [OutputType(typeof(NDArray))]
    public class GetMxErfNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Erf(Data));
        }
    }

    [Cmdlet("Get", "MxErfinvNDArray")]
    [Alias("mx.nd.Erfinv")]
    [OutputType(typeof(NDArray))]
    public class GetMxErfinvNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Erfinv(Data));
        }
    }

    [Cmdlet("Get", "MxRcbrtNDArray")]
    [Alias("mx.nd.Rcbrt")]
    [OutputType(typeof(NDArray))]
    public class GetMxRcbrtNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Rcbrt(Data));
        }
    }

    [Cmdlet("Get", "MxExpNDArray")]
    [Alias("mx.nd.Exp")]
    [OutputType(typeof(NDArray))]
    public class GetMxExpNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Exp(Data));
        }
    }

    [Cmdlet("Get", "MxLogNDArray")]
    [Alias("mx.nd.Log")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Log(Data));
        }
    }

    [Cmdlet("Get", "MxLog10NDArray")]
    [Alias("mx.nd.Log10")]
    [OutputType(typeof(NDArray))]
    public class GetMxLog10NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Log10(Data));
        }
    }

    [Cmdlet("Get", "MxLog2NDArray")]
    [Alias("mx.nd.Log2")]
    [OutputType(typeof(NDArray))]
    public class GetMxLog2NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Log2(Data));
        }
    }

    [Cmdlet("Get", "MxLog1PNDArray")]
    [Alias("mx.nd.Log1P")]
    [OutputType(typeof(NDArray))]
    public class GetMxLog1PNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Log1P(Data));
        }
    }

    [Cmdlet("Get", "MxExpm1NDArray")]
    [Alias("mx.nd.Expm1")]
    [OutputType(typeof(NDArray))]
    public class GetMxExpm1NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Expm1(Data));
        }
    }

    [Cmdlet("Get", "MxGammaNDArray")]
    [Alias("mx.nd.Gamma")]
    [OutputType(typeof(NDArray))]
    public class GetMxGammaNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Gamma(Data));
        }
    }

    [Cmdlet("Get", "MxGammalnNDArray")]
    [Alias("mx.nd.Gammaln")]
    [OutputType(typeof(NDArray))]
    public class GetMxGammalnNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Gammaln(Data));
        }
    }

    [Cmdlet("Get", "MxLogicalNotNDArray")]
    [Alias("mx.nd.LogicalNot")]
    [OutputType(typeof(NDArray))]
    public class GetMxLogicalNotNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LogicalNot(Data));
        }
    }

    [Cmdlet("Get", "MxSinNDArray")]
    [Alias("mx.nd.Sin")]
    [OutputType(typeof(NDArray))]
    public class GetMxSinNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Sin(Data));
        }
    }

    [Cmdlet("Get", "MxCosNDArray")]
    [Alias("mx.nd.Cos")]
    [OutputType(typeof(NDArray))]
    public class GetMxCosNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Cos(Data));
        }
    }

    [Cmdlet("Get", "MxTanNDArray")]
    [Alias("mx.nd.Tan")]
    [OutputType(typeof(NDArray))]
    public class GetMxTanNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Tan(Data));
        }
    }

    [Cmdlet("Get", "MxArcsinNDArray")]
    [Alias("mx.nd.Arcsin")]
    [OutputType(typeof(NDArray))]
    public class GetMxArcsinNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Arcsin(Data));
        }
    }

    [Cmdlet("Get", "MxArccosNDArray")]
    [Alias("mx.nd.Arccos")]
    [OutputType(typeof(NDArray))]
    public class GetMxArccosNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Arccos(Data));
        }
    }

    [Cmdlet("Get", "MxArctanNDArray")]
    [Alias("mx.nd.Arctan")]
    [OutputType(typeof(NDArray))]
    public class GetMxArctanNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Arctan(Data));
        }
    }

    [Cmdlet("Get", "MxDegreesNDArray")]
    [Alias("mx.nd.Degrees")]
    [OutputType(typeof(NDArray))]
    public class GetMxDegreesNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Degrees(Data));
        }
    }

    [Cmdlet("Get", "MxRadiansNDArray")]
    [Alias("mx.nd.Radians")]
    [OutputType(typeof(NDArray))]
    public class GetMxRadiansNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Radians(Data));
        }
    }

    [Cmdlet("Get", "MxSinhNDArray")]
    [Alias("mx.nd.Sinh")]
    [OutputType(typeof(NDArray))]
    public class GetMxSinhNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Sinh(Data));
        }
    }

    [Cmdlet("Get", "MxCoshNDArray")]
    [Alias("mx.nd.Cosh")]
    [OutputType(typeof(NDArray))]
    public class GetMxCoshNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Cosh(Data));
        }
    }

    [Cmdlet("Get", "MxTanhNDArray")]
    [Alias("mx.nd.Tanh")]
    [OutputType(typeof(NDArray))]
    public class GetMxTanhNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Tanh(Data));
        }
    }

    [Cmdlet("Get", "MxArcsinhNDArray")]
    [Alias("mx.nd.Arcsinh")]
    [OutputType(typeof(NDArray))]
    public class GetMxArcsinhNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Arcsinh(Data));
        }
    }

    [Cmdlet("Get", "MxArccoshNDArray")]
    [Alias("mx.nd.Arccosh")]
    [OutputType(typeof(NDArray))]
    public class GetMxArccoshNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Arccosh(Data));
        }
    }

    [Cmdlet("Get", "MxArctanhNDArray")]
    [Alias("mx.nd.Arctanh")]
    [OutputType(typeof(NDArray))]
    public class GetMxArctanhNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Arctanh(Data));
        }
    }

    [Cmdlet("Get", "MxHistogramNDArray")]
    [Alias("mx.nd.Histogram")]
    [OutputType(typeof(NDArray))]
    public class GetMxHistogramNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Bins { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int? BinCnt { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Tuple<double> Range { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Histogram(Data, Bins, BinCnt, Range));
        }
    }

    [Cmdlet("Get", "MxEmbeddingNDArray")]
    [Alias("mx.nd.Embedding")]
    [OutputType(typeof(NDArray))]
    public class GetMxEmbeddingNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int InputDim { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int OutputDim { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public bool SparseGrad { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Embedding(Data, Weight, InputDim, OutputDim, Dtype, SparseGrad));
        }
    }

    [Cmdlet("Get", "MxTakeNDArray")]
    [Alias("mx.nd.Take")]
    [OutputType(typeof(NDArray))]
    public class GetMxTakeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Indices { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public TakeMode Mode { get; set; } = TakeMode.Clip;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Take(A, Indices, Axis, Mode));
        }
    }

    [Cmdlet("Get", "MxBatchTakeNDArray")]
    [Alias("mx.nd.BatchTake")]
    [OutputType(typeof(NDArray))]
    public class GetMxBatchTakeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Indices { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BatchTake(A, Indices));
        }
    }

    [Cmdlet("Get", "MxOneHotNDArray")]
    [Alias("mx.nd.OneHot")]
    [OutputType(typeof(NDArray))]
    public class GetMxOneHotNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Indices { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Depth { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public double OnValue { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public double OffValue { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.OneHot(Indices, Depth, OnValue, OffValue, Dtype));
        }
    }

    [Cmdlet("Get", "MxGatherNdNDArray")]
    [Alias("mx.nd.GatherNd")]
    [OutputType(typeof(NDArray))]
    public class GetMxGatherNdNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Indices { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.GatherNd(Data, Indices));
        }
    }

    [Cmdlet("Get", "MxScatterNdNDArray")]
    [Alias("mx.nd.ScatterNd")]
    [OutputType(typeof(NDArray))]
    public class GetMxScatterNdNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Indices { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape Shape { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ScatterNd(Data, Indices, Shape));
        }
    }

    [Cmdlet("Get", "MxScatterSetNdNDArray")]
    [Alias("mx.nd.ScatterSetNd")]
    [OutputType(typeof(NDArray))]
    public class GetMxScatterSetNdNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Indices { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Shape Shape { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ScatterSetNd(Lhs, Rhs, Indices, Shape));
        }
    }

    [Cmdlet("Get", "MxZerosWithoutDtypeNDArray")]
    [Alias("mx.nd.ZerosWithoutDtype")]
    [OutputType(typeof(NDArray))]
    public class GetMxZerosWithoutDtypeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ZerosWithoutDtype(Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxZerosNDArray")]
    [Alias("mx.nd.Zeros")]
    [OutputType(typeof(NDArray))]
    public class GetMxZerosNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Zeros(Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxEyeNDArray")]
    [Alias("mx.nd.Eye")]
    [OutputType(typeof(NDArray))]
    public class GetMxEyeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Tuple<double> N { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int M { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public int K { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Eye(N, M, K, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxOnesNDArray")]
    [Alias("mx.nd.Ones")]
    [OutputType(typeof(NDArray))]
    public class GetMxOnesNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Ones(Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxEmptyNDArray")]
    [Alias("mx.nd.Empty")]
    [OutputType(typeof(NDArray))]
    public class GetMxEmptyNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Empty(Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxFullNDArray")]
    [Alias("mx.nd.Full")]
    [OutputType(typeof(NDArray))]
    public class GetMxFullNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public double Value { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Full(Value, Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxArangeNDArray")]
    [Alias("mx.nd.Arange")]
    [OutputType(typeof(NDArray))]
    public class GetMxArangeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Start { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Stop { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int Step { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public int Repeat { get; set; } = 1;

        [Parameter(Position = 4, Mandatory = false)]
        public bool InferRange { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Arange(Start, Stop, Step, Repeat, InferRange, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxZerosLikeNDArray")]
    [Alias("mx.nd.ZerosLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxZerosLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ZerosLike(Data));
        }
    }

    [Cmdlet("Get", "MxOnesLikeNDArray")]
    [Alias("mx.nd.OnesLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxOnesLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.OnesLike(Data));
        }
    }

    [Cmdlet("Get", "MxLinalgGemmNDArray")]
    [Alias("mx.nd.LinalgGemm")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinalgGemmNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray B { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray C { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public bool TransposeA { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public bool TransposeB { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public double Alpha { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public double Beta { get; set; } = 1;

        [Parameter(Position = 7, Mandatory = false)]
        public int Axis { get; set; } = -2;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgGemm(A, B, C, TransposeA, TransposeB, Alpha, Beta, Axis));
        }
    }

    [Cmdlet("Get", "MxLinalgGemm2NDArray")]
    [Alias("mx.nd.LinalgGemm2")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinalgGemm2NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray B { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool TransposeA { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool TransposeB { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public double Alpha { get; set; } = 1;

        [Parameter(Position = 5, Mandatory = false)]
        public int Axis { get; set; } = -2;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgGemm2(A, B, TransposeA, TransposeB, Alpha, Axis));
        }
    }

    [Cmdlet("Get", "MxLinalgPotrfNDArray")]
    [Alias("mx.nd.LinalgPotrf")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinalgPotrfNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgPotrf(A));
        }
    }

    [Cmdlet("Get", "MxLinalgPotriNDArray")]
    [Alias("mx.nd.LinalgPotri")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinalgPotriNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgPotri(A));
        }
    }

    [Cmdlet("Get", "MxLinalgTrmmNDArray")]
    [Alias("mx.nd.LinalgTrmm")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinalgTrmmNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray B { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool Transpose { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Rightside { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public bool Lower { get; set; } = true;

        [Parameter(Position = 5, Mandatory = false)]
        public double Alpha { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgTrmm(A, B, Transpose, Rightside, Lower, Alpha));
        }
    }

    [Cmdlet("Get", "MxLinalgTrsmNDArray")]
    [Alias("mx.nd.LinalgTrsm")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinalgTrsmNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray B { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool Transpose { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Rightside { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public bool Lower { get; set; } = true;

        [Parameter(Position = 5, Mandatory = false)]
        public double Alpha { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgTrsm(A, B, Transpose, Rightside, Lower, Alpha));
        }
    }

    [Cmdlet("Get", "MxLinalgSumlogdiagNDArray")]
    [Alias("mx.nd.LinalgSumlogdiag")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinalgSumlogdiagNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgSumlogdiag(A));
        }
    }

    [Cmdlet("Get", "MxLinalgSyrkNDArray")]
    [Alias("mx.nd.LinalgSyrk")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinalgSyrkNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public bool Transpose { get; set; } = false;

        [Parameter(Position = 2, Mandatory = false)]
        public double Alpha { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgSyrk(A, Transpose, Alpha));
        }
    }

    [Cmdlet("Get", "MxLinalgGelqfNDArray")]
    [Alias("mx.nd.LinalgGelqf")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinalgGelqfNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgGelqf(A));
        }
    }

    [Cmdlet("Get", "MxLinalgSyevdNDArray")]
    [Alias("mx.nd.LinalgSyevd")]
    [OutputType(typeof((NDArray, NDArray)))]
    public class GetMxLinalgSyevdNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray A { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.LinalgSyevd(A));
        }
    }

    [Cmdlet("Get", "MxReshapeNDArray")]
    [Alias("mx.nd.Reshape")]
    [OutputType(typeof(NDArray))]
    public class GetMxReshapeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Reverse { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Reshape(Data, Shape, Reverse));
        }
    }

    [Cmdlet("Get", "MxTransposeNDArray")]
    [Alias("mx.nd.Transpose")]
    [OutputType(typeof(NDArray))]
    public class GetMxTransposeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axes { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Transpose(Data, Axes));
        }
    }

    [Cmdlet("Get", "MxExpandDimsNDArray")]
    [Alias("mx.nd.ExpandDims")]
    [OutputType(typeof(NDArray))]
    public class GetMxExpandDimsNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Axis { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ExpandDims(Data, Axis));
        }
    }

    [Cmdlet("Get", "MxSliceNDArray")]
    [Alias("mx.nd.Slice")]
    [OutputType(typeof(NDArray))]
    public class GetMxSliceNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Begin { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape End { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public Shape Step { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Slice(Data, Begin, End, Step));
        }
    }

    [Cmdlet("Get", "MxSliceAssignNDArray")]
    [Alias("mx.nd.SliceAssign")]
    [OutputType(typeof(NDArray))]
    public class GetMxSliceAssignNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape Begin { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Shape End { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public Shape Step { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SliceAssign(Lhs, Rhs, Begin, End, Step));
        }
    }

    [Cmdlet("Get", "MxSliceAssignScalarNDArray")]
    [Alias("mx.nd.SliceAssignScalar")]
    [OutputType(typeof(NDArray))]
    public class GetMxSliceAssignScalarNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Begin { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape End { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public double Scalar { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public Shape Step { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SliceAssignScalar(Data, Begin, End, Scalar, Step));
        }
    }

    [Cmdlet("Get", "MxSliceAxisNDArray")]
    [Alias("mx.nd.SliceAxis")]
    [OutputType(typeof(NDArray))]
    public class GetMxSliceAxisNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Axis { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int Begin { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int? End { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SliceAxis(Data, Axis, Begin, End));
        }
    }

    [Cmdlet("Get", "MxSliceLikeNDArray")]
    [Alias("mx.nd.SliceLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxSliceLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray ShapeLike { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Axes { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SliceLike(Data, ShapeLike, Axes));
        }
    }

    [Cmdlet("Get", "MxClipNDArray")]
    [Alias("mx.nd.Clip")]
    [OutputType(typeof(NDArray))]
    public class GetMxClipNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float AMin { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float AMax { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Clip(Data, AMin, AMax));
        }
    }

    [Cmdlet("Get", "MxRepeatNDArray")]
    [Alias("mx.nd.Repeat")]
    [OutputType(typeof(NDArray))]
    public class GetMxRepeatNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Repeats { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int? Axis { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Repeat(Data, Repeats, Axis));
        }
    }

    [Cmdlet("Get", "MxTileNDArray")]
    [Alias("mx.nd.Tile")]
    [OutputType(typeof(NDArray))]
    public class GetMxTileNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Reps { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Tile(Data, Reps));
        }
    }

    [Cmdlet("Get", "MxReverseNDArray")]
    [Alias("mx.nd.Reverse")]
    [OutputType(typeof(NDArray))]
    public class GetMxReverseNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Axis { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Reverse(Data, Axis));
        }
    }

    [Cmdlet("Get", "MxFlipNDArray")]
    [Alias("mx.nd.Flip")]
    [OutputType(typeof(NDArray))]
    public class GetMxFlipNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Axis { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Flip(Data, Axis));
        }
    }

    [Cmdlet("Get", "MxStackNDArray")]
    [Alias("mx.nd.Stack")]
    [OutputType(typeof(NDArray))]
    public class GetMxStackNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Stack(Data, NumArgs, Axis));
        }
    }

    [Cmdlet("Get", "MxSqueezeNDArray")]
    [Alias("mx.nd.Squeeze")]
    [OutputType(typeof(NDArray))]
    public class GetMxSqueezeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Squeeze(Data, Axis));
        }
    }

    [Cmdlet("Get", "MxDepthToSpaceNDArray")]
    [Alias("mx.nd.DepthToSpace")]
    [OutputType(typeof(NDArray))]
    public class GetMxDepthToSpaceNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int BlockSize { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.DepthToSpace(Data, BlockSize));
        }
    }

    [Cmdlet("Get", "MxSpaceToDepthNDArray")]
    [Alias("mx.nd.SpaceToDepth")]
    [OutputType(typeof(NDArray))]
    public class GetMxSpaceToDepthNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int BlockSize { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SpaceToDepth(Data, BlockSize));
        }
    }

    [Cmdlet("Get", "MxSplitV2NDArray")]
    [Alias("mx.nd.SplitV2")]
    [OutputType(typeof(NDArrayList))]
    public class GetMxSplitV2NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Indices { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public bool SqueezeAxis { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public int Sections { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SplitV2(Data, Indices, Axis, SqueezeAxis, Sections));
        }
    }

    [Cmdlet("Get", "MxSplitNDArray")]
    [Alias("mx.nd.Split")]
    [OutputType(typeof(NDArrayList))]
    public class GetMxSplitNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumOutputs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public bool SqueezeAxis { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Split(Data, NumOutputs, Axis, SqueezeAxis));
        }
    }

    [Cmdlet("Get", "MxTopkNDArray")]
    [Alias("mx.nd.Topk")]
    [OutputType(typeof(NDArray))]
    public class GetMxTopkNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public int K { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public TopkRetTyp RetTyp { get; set; } = TopkRetTyp.Indices;

        [Parameter(Position = 4, Mandatory = false)]
        public bool IsAscend { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Topk(Data, Axis, K, RetTyp, IsAscend, Dtype));
        }
    }

    [Cmdlet("Get", "MxSortNDArray")]
    [Alias("mx.nd.Sort")]
    [OutputType(typeof(NDArray))]
    public class GetMxSortNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool IsAscend { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Sort(Data, Axis, IsAscend));
        }
    }

    [Cmdlet("Get", "MxArgsortNDArray")]
    [Alias("mx.nd.Argsort")]
    [OutputType(typeof(NDArray))]
    public class GetMxArgsortNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool IsAscend { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Argsort(Data, Axis, IsAscend, Dtype));
        }
    }

    [Cmdlet("Get", "MxRavelMultiIndexNDArray")]
    [Alias("mx.nd.RavelMultiIndex")]
    [OutputType(typeof(NDArray))]
    public class GetMxRavelMultiIndexNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.RavelMultiIndex(Data, Shape));
        }
    }

    [Cmdlet("Get", "MxUnravelIndexNDArray")]
    [Alias("mx.nd.UnravelIndex")]
    [OutputType(typeof(NDArray))]
    public class GetMxUnravelIndexNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.UnravelIndex(Data, Shape));
        }
    }

    [Cmdlet("Get", "MxSparseRetainNDArray")]
    [Alias("mx.nd.SparseRetain")]
    [OutputType(typeof(NDArray))]
    public class GetMxSparseRetainNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Indices { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SparseRetain(Data, Indices));
        }
    }

    [Cmdlet("Get", "MxSquareSumNDArray")]
    [Alias("mx.nd.SquareSum")]
    [OutputType(typeof(NDArray))]
    public class GetMxSquareSumNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SquareSum(Data, Axis, Keepdims, Exclude));
        }
    }

    [Cmdlet("Get", "MxBilinearSamplerNDArray")]
    [Alias("mx.nd.BilinearSampler")]
    [OutputType(typeof(NDArray))]
    public class GetMxBilinearSamplerNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Grid { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool? CudnnOff { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.BilinearSampler(Data, Grid, CudnnOff));
        }
    }

    [Cmdlet("Get", "MxConvolutionV1NDArray")]
    [Alias("mx.nd.ConvolutionV1")]
    [OutputType(typeof(NDArray))]
    public class GetMxConvolutionV1NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Bias { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Shape Kernel { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public uint NumFilter { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public Shape Stride { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public Shape Dilate { get; set; } = null;

        [Parameter(Position = 7, Mandatory = false)]
        public Shape Pad { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public uint NumGroup { get; set; } = 1;

        [Parameter(Position = 9, Mandatory = false)]
        public ulong Workspace { get; set; } = 1024;

        [Parameter(Position = 10, Mandatory = false)]
        public bool NoBias { get; set; } = false;

        [Parameter(Position = 11, Mandatory = false)]
        public ConvolutionV1CudnnTune? CudnnTune { get; set; } = null;

        [Parameter(Position = 12, Mandatory = false)]
        public bool CudnnOff { get; set; } = false;

        [Parameter(Position = 13, Mandatory = false)]
        public ConvolutionV1Layout? Layout { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ConvolutionV1(Data, Weight, Bias, Kernel, NumFilter, Stride, Dilate, Pad, NumGroup, Workspace, NoBias, CudnnTune, CudnnOff, Layout));
        }
    }

    [Cmdlet("Get", "MxCorrelationNDArray")]
    [Alias("mx.nd.Correlation")]
    [OutputType(typeof(NDArray))]
    public class GetMxCorrelationNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data1 { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Data2 { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public uint KernelSize { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public uint MaxDisplacement { get; set; } = 1;

        [Parameter(Position = 4, Mandatory = false)]
        public uint Stride1 { get; set; } = 1;

        [Parameter(Position = 5, Mandatory = false)]
        public uint Stride2 { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public uint PadSize { get; set; } = 0;

        [Parameter(Position = 7, Mandatory = false)]
        public bool IsMultiply { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Correlation(Data1, Data2, KernelSize, MaxDisplacement, Stride1, Stride2, PadSize, IsMultiply));
        }
    }

    [Cmdlet("Get", "MxCropNDArray")]
    [Alias("mx.nd.Crop")]
    [OutputType(typeof(NDArray))]
    public class GetMxCropNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Offset { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Shape HW { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public bool CenterCrop { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Crop(Data, NumArgs, Offset, HW, CenterCrop));
        }
    }

    [Cmdlet("Get", "MxCrossDeviceCopyNDArray")]
    [Alias("mx.nd.CrossDeviceCopy")]
    [OutputType(typeof(NDArray))]
    public class GetMxCrossDeviceCopyNDArray : PSCmdlet
    {


        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.CrossDeviceCopy());
        }
    }

    [Cmdlet("Get", "MxNativeNDArray")]
    [Alias("mx.nd.Native")]
    [OutputType(typeof(NDArray))]
    public class GetMxNativeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public System.IntPtr Info { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool NeedTopGrad { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Native(Data, Info, NeedTopGrad));
        }
    }

    [Cmdlet("Get", "MxGridGeneratorNDArray")]
    [Alias("mx.nd.GridGenerator")]
    [OutputType(typeof(NDArray))]
    public class GetMxGridGeneratorNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public GridgeneratorTransformType TransformType { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape TargetShape { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.GridGenerator(Data, TransformType, TargetShape));
        }
    }

    [Cmdlet("Get", "MxInstanceNormNDArray")]
    [Alias("mx.nd.InstanceNorm")]
    [OutputType(typeof(NDArray))]
    public class GetMxInstanceNormNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Gamma { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Beta { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Eps { get; set; } = 0.001f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.InstanceNorm(Data, Gamma, Beta, Eps));
        }
    }

    [Cmdlet("Get", "MxL2NormalizationNDArray")]
    [Alias("mx.nd.L2Normalization")]
    [OutputType(typeof(NDArray))]
    public class GetMxL2NormalizationNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Eps { get; set; } = 1e-10f;

        [Parameter(Position = 2, Mandatory = false)]
        public L2normalizationMode Mode { get; set; } = L2normalizationMode.Instance;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.L2Normalization(Data, Eps, Mode));
        }
    }

    [Cmdlet("Get", "MxMakeLoss2NDArray")]
    [Alias("mx.nd.MakeLoss2")]
    [OutputType(typeof(NDArray))]
    public class GetMxMakeLoss2NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float GradScale { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public float ValidThresh { get; set; } = 0f;

        [Parameter(Position = 3, Mandatory = false)]
        public MakelossNormalization Normalization { get; set; } = MakelossNormalization.Null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.MakeLoss(Data, GradScale, ValidThresh, Normalization));
        }
    }

    [Cmdlet("Get", "MxPoolingV1NDArray")]
    [Alias("mx.nd.PoolingV1")]
    [OutputType(typeof(NDArray))]
    public class GetMxPoolingV1NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Kernel { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public PoolingV1PoolType PoolType { get; set; } = PoolingV1PoolType.Max;

        [Parameter(Position = 3, Mandatory = false)]
        public bool GlobalPool { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public PoolingV1PoolingConvention PoolingConvention { get; set; } = PoolingV1PoolingConvention.Valid;

        [Parameter(Position = 5, Mandatory = false)]
        public Shape Stride { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public Shape Pad { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.PoolingV1(Data, Kernel, PoolType, GlobalPool, PoolingConvention, Stride, Pad));
        }
    }

    [Cmdlet("Get", "MxROIPoolingNDArray")]
    [Alias("mx.nd.ROIPooling")]
    [OutputType(typeof(NDArray))]
    public class GetMxROIPoolingNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rois { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape PooledSize { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float SpatialScale { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.ROIPooling(Data, Rois, PooledSize, SpatialScale));
        }
    }

    [Cmdlet("Get", "MxSequenceLastNDArray")]
    [Alias("mx.nd.SequenceLast")]
    [OutputType(typeof(NDArray))]
    public class GetMxSequenceLastNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray SequenceLength { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool UseSequenceLength { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public int Axis { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SequenceLast(Data, SequenceLength, UseSequenceLength, Axis));
        }
    }

    [Cmdlet("Get", "MxSequenceMaskNDArray")]
    [Alias("mx.nd.SequenceMask")]
    [OutputType(typeof(NDArray))]
    public class GetMxSequenceMaskNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray SequenceLength { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool UseSequenceLength { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public float Value { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public int Axis { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SequenceMask(Data, SequenceLength, UseSequenceLength, Value, Axis));
        }
    }

    [Cmdlet("Get", "MxSequenceReverseNDArray")]
    [Alias("mx.nd.SequenceReverse")]
    [OutputType(typeof(NDArray))]
    public class GetMxSequenceReverseNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray SequenceLength { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool UseSequenceLength { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public int Axis { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SequenceReverse(Data, SequenceLength, UseSequenceLength, Axis));
        }
    }

    [Cmdlet("Get", "MxSpatialTransformerNDArray")]
    [Alias("mx.nd.SpatialTransformer")]
    [OutputType(typeof(NDArray))]
    public class GetMxSpatialTransformerNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Loc { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public SpatialtransformerTransformType TransformType { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public SpatialtransformerSamplerType SamplerType { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public Shape TargetShape { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public bool? CudnnOff { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SpatialTransformer(Data, Loc, TransformType, SamplerType, TargetShape, CudnnOff));
        }
    }

    [Cmdlet("Get", "MxSVMOutputNDArray")]
    [Alias("mx.nd.SVMOutput")]
    [OutputType(typeof(NDArray))]
    public class GetMxSVMOutputNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float Margin { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public float RegularizationCoefficient { get; set; } = 1f;

        [Parameter(Position = 4, Mandatory = false)]
        public bool UseLinear { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.SVMOutput(Data, Label, Margin, RegularizationCoefficient, UseLinear));
        }
    }

    [Cmdlet("Get", "MxOnehotEncodeNDArray")]
    [Alias("mx.nd.OnehotEncode")]
    [OutputType(typeof(NDArray))]
    public class GetMxOnehotEncodeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.OnehotEncode(Lhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxFillElement0IndexNDArray")]
    [Alias("mx.nd.FillElement0Index")]
    [OutputType(typeof(NDArray))]
    public class GetMxFillElement0IndexNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Mhs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Rhs { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.FillElement0Index(Lhs, Mhs, Rhs));
        }
    }

    [Cmdlet("Get", "MxImdecodeNDArray")]
    [Alias("mx.nd.Imdecode")]
    [OutputType(typeof(NDArray))]
    public class GetMxImdecodeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Mean { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Index { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int X0 { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int Y0 { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int X1 { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public int Y1 { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public int C { get; set; }

        [Parameter(Position = 7, Mandatory = true)]
        public int Size { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Imdecode(Mean, Index, X0, Y0, X1, Y1, C, Size));
        }
    }

    [Cmdlet("Get", "MxLinspaceNDArray")]
    [Alias("mx.nd.Linspace")]
    [OutputType(typeof(NDArray))]
    public class GetMxLinspaceNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public float Start { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Stop { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int Num { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public bool Endpoint { get; set; } = true;

        [Parameter(Position = 4, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Linspace(Start, Stop, Num, Endpoint, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxStopGradientNDArray")]
    [Alias("mx.nd.StopGradient")]
    [OutputType(typeof(NDArray))]
    public class GetMxStopGradientNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.StopGradient(Data));
        }
    }
}
