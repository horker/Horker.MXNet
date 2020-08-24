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
    [Cmdlet("Get", "MxCustomFunctionSymbol")]
    [Alias("mx.sym.CustomFunction")]
    [OutputType(typeof(Symbol))]
    public class GetMxCustomFunctionSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.CustomFunction(SymbolName));
        }
    }

    [Cmdlet("Get", "MxCachedOpSymbol")]
    [Alias("mx.sym.CachedOp")]
    [OutputType(typeof(Symbol))]
    public class GetMxCachedOpSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.CachedOp(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCvimdecodeSymbol")]
    [Alias("mx.sym.Cvimdecode")]
    [OutputType(typeof(Symbol))]
    public class GetMxCvimdecodeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Buf { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Flag { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool ToRgb { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Cvimdecode(Buf, Flag, ToRgb, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCvimreadSymbol")]
    [Alias("mx.sym.Cvimread")]
    [OutputType(typeof(Symbol))]
    public class GetMxCvimreadSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Filename { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Flag { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool ToRgb { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Cvimread(Filename, Flag, ToRgb, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCvimresizeSymbol")]
    [Alias("mx.sym.Cvimresize")]
    [OutputType(typeof(Symbol))]
    public class GetMxCvimresizeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int W { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int H { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public int Interp { get; set; } = 1;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Cvimresize(Data, W, H, Interp, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCvcopyMakeBorderSymbol")]
    [Alias("mx.sym.CvcopyMakeBorder")]
    [OutputType(typeof(Symbol))]
    public class GetMxCvcopyMakeBorderSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

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

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.CvcopyMakeBorder(Data, Top, Bot, Left, Right, Type, Values, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCopytoSymbol")]
    [Alias("mx.sym.Copyto")]
    [OutputType(typeof(Symbol))]
    public class GetMxCopytoSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Copyto(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxNoGradientSymbol")]
    [Alias("mx.sym.NoGradient")]
    [OutputType(typeof(Symbol))]
    public class GetMxNoGradientSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.NoGradient(SymbolName));
        }
    }

    [Cmdlet("Get", "MxBatchNormV1Symbol")]
    [Alias("mx.sym.BatchNormV1")]
    [OutputType(typeof(Symbol))]
    public class GetMxBatchNormV1Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Gamma { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Beta { get; set; }

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

        [Parameter(Position = 8, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BatchNormV1(Data, Gamma, Beta, Eps, Momentum, FixGamma, UseGlobalStats, OutputMeanVar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMpAdamwUpdateSymbol")]
    [Alias("mx.sym.MpAdamwUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxMpAdamwUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Mean { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol Var { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public Symbol Weight32 { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public Symbol RescaleGrad { get; set; }

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

        [Parameter(Position = 13, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MpAdamwUpdate(Weight, Grad, Mean, Var, Weight32, RescaleGrad, Lr, Eta, Beta1, Beta2, Epsilon, Wd, ClipGradient, SymbolName));
        }
    }

    [Cmdlet("Get", "MxAdamwUpdateSymbol")]
    [Alias("mx.sym.AdamwUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxAdamwUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Mean { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol Var { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public Symbol RescaleGrad { get; set; }

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

        [Parameter(Position = 12, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.AdamwUpdate(Weight, Grad, Mean, Var, RescaleGrad, Lr, Eta, Beta1, Beta2, Epsilon, Wd, ClipGradient, SymbolName));
        }
    }

    [Cmdlet("Get", "MxKhatriRaoSymbol")]
    [Alias("mx.sym.KhatriRao")]
    [OutputType(typeof(Symbol))]
    public class GetMxKhatriRaoSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Args { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.KhatriRao(Args, SymbolName));
        }
    }

    [Cmdlet("Get", "MxForeachSymbol")]
    [Alias("mx.sym.Foreach")]
    [OutputType(typeof(Symbol))]
    public class GetMxForeachSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Fn { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public SymbolList Data { get; set; }

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

        [Parameter(Position = 8, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Foreach(Fn, Data, NumArgs, NumOutputs, NumOutData, InStateLocs, InDataLocs, RemainLocs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxWhileLoopSymbol")]
    [Alias("mx.sym.WhileLoop")]
    [OutputType(typeof(Symbol))]
    public class GetMxWhileLoopSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Cond { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Func { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public SymbolList Data { get; set; }

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

        [Parameter(Position = 10, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.WhileLoop(Cond, Func, Data, NumArgs, NumOutputs, NumOutData, MaxIterations, CondInputLocs, FuncInputLocs, FuncVarLocs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCondSymbol")]
    [Alias("mx.sym.Cond")]
    [OutputType(typeof(Symbol))]
    public class GetMxCondSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Cond { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol ThenBranch { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol ElseBranch { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public SymbolList Data { get; set; }

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

        [Parameter(Position = 9, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Cond(Cond, ThenBranch, ElseBranch, Data, NumArgs, NumOutputs, CondInputLocs, ThenInputLocs, ElseInputLocs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCustomSymbol")]
    [Alias("mx.sym.Custom")]
    [OutputType(typeof(Symbol))]
    public class GetMxCustomSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public string OpType { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Custom(Data, OpType, SymbolName));
        }
    }

    [Cmdlet("Get", "MxIdentityAttachKLSparseRegSymbol")]
    [Alias("mx.sym.IdentityAttachKLSparseReg")]
    [OutputType(typeof(Symbol))]
    public class GetMxIdentityAttachKLSparseRegSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float SparsenessTarget { get; set; } = 0.1f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Penalty { get; set; } = 0.001f;

        [Parameter(Position = 3, Mandatory = false)]
        public float Momentum { get; set; } = 0.9f;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.IdentityAttachKLSparseReg(Data, SparsenessTarget, Penalty, Momentum, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLeakyReLUSymbol")]
    [Alias("mx.sym.LeakyReLU")]
    [OutputType(typeof(Symbol))]
    public class GetMxLeakyReLUSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

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

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LeakyReLU(Data, Gamma, ActType, Slope, LowerBound, UpperBound, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSoftmaxCrossEntropySymbol")]
    [Alias("mx.sym.SoftmaxCrossEntropy")]
    [OutputType(typeof(Symbol))]
    public class GetMxSoftmaxCrossEntropySymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SoftmaxCrossEntropy(Data, Label, SymbolName));
        }
    }

    [Cmdlet("Get", "MxActivationSymbol")]
    [Alias("mx.sym.Activation")]
    [OutputType(typeof(Symbol))]
    public class GetMxActivationSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public ActivationType ActType { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Activation(Data, ActType, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBatchNormSymbol")]
    [Alias("mx.sym.BatchNorm")]
    [OutputType(typeof(Symbol))]
    public class GetMxBatchNormSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Gamma { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Beta { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol MovingMean { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public Symbol MovingVar { get; set; }

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

        [Parameter(Position = 12, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BatchNorm(Data, Gamma, Beta, MovingMean, MovingVar, Eps, Momentum, FixGamma, UseGlobalStats, OutputMeanVar, Axis, CudnnOff, SymbolName));
        }
    }

    [Cmdlet("Get", "MxConcatSymbol")]
    [Alias("mx.sym.Concat")]
    [OutputType(typeof(Symbol))]
    public class GetMxConcatSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Dim { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Concat(Data, Dim, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRnnParamConcatSymbol")]
    [Alias("mx.sym.RnnParamConcat")]
    [OutputType(typeof(Symbol))]
    public class GetMxRnnParamConcatSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Dim { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RnnParamConcat(Data, NumArgs, Dim, SymbolName));
        }
    }

    [Cmdlet("Get", "MxConvolutionSymbol")]
    [Alias("mx.sym.Convolution")]
    [OutputType(typeof(Symbol))]
    public class GetMxConvolutionSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Bias { get; set; }

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
        public bool NoBias { get; set; } = true;

        [Parameter(Position = 11, Mandatory = false)]
        public ConvolutionCudnnTune? CudnnTune { get; set; } = null;

        [Parameter(Position = 12, Mandatory = false)]
        public bool CudnnOff { get; set; } = false;

        [Parameter(Position = 13, Mandatory = false)]
        public ConvolutionLayout? Layout { get; set; } = null;

        [Parameter(Position = 14, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Convolution(Data, Weight, Bias, Kernel, NumFilter, Stride, Dilate, Pad, NumGroup, Workspace, NoBias, CudnnTune, CudnnOff, Layout, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCTCLossSymbol")]
    [Alias("mx.sym.CTCLoss")]
    [OutputType(typeof(Symbol))]
    public class GetMxCTCLossSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Label { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol DataLengths { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol LabelLengths { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public bool UseDataLengths { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public bool UseLabelLengths { get; set; } = false;

        [Parameter(Position = 6, Mandatory = false)]
        public CtclossBlankLabel BlankLabel { get; set; } = CtclossBlankLabel.First;

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.CTCLoss(Data, Label, DataLengths, LabelLengths, UseDataLengths, UseLabelLengths, BlankLabel, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDeconvolutionSymbol")]
    [Alias("mx.sym.Deconvolution")]
    [OutputType(typeof(Symbol))]
    public class GetMxDeconvolutionSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape Kernel { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int NumFilter { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public Shape Stride { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public Shape Dilate { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public Shape Pad { get; set; } = null;

        [Parameter(Position = 7, Mandatory = false)]
        public Shape Adj { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public Shape TargetShape { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public Symbol Bias { get; set; } = null;

        [Parameter(Position = 10, Mandatory = false)]
        public bool NoBias { get; set; } = true;

        [Parameter(Position = 11, Mandatory = false)]
        public int NumGroup { get; set; } = 1;

        [Parameter(Position = 12, Mandatory = false)]
        public ulong Workspace { get; set; } = 512;

        [Parameter(Position = 13, Mandatory = false)]
        public DeconvolutionCudnnTune? CudnnTune { get; set; } = null;

        [Parameter(Position = 14, Mandatory = false)]
        public bool CudnnOff { get; set; } = false;

        [Parameter(Position = 15, Mandatory = false)]
        public DeconvolutionLayout? Layout { get; set; } = null;

        [Parameter(Position = 16, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Deconvolution(Data, Weight, Kernel, NumFilter, Stride, Dilate, Pad, Adj, TargetShape, Bias, NoBias, NumGroup, Workspace, CudnnTune, CudnnOff, Layout, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDropoutSymbol")]
    [Alias("mx.sym.Dropout")]
    [OutputType(typeof(Symbol))]
    public class GetMxDropoutSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float P { get; set; } = 0.5f;

        [Parameter(Position = 2, Mandatory = false)]
        public DropoutMode Mode { get; set; } = DropoutMode.Training;

        [Parameter(Position = 3, Mandatory = false)]
        public Shape Axes { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public bool? CudnnOff { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Dropout(Data, P, Mode, Axes, CudnnOff, SymbolName));
        }
    }

    [Cmdlet("Get", "MxFullyConnectedSymbol")]
    [Alias("mx.sym.FullyConnected")]
    [OutputType(typeof(Symbol))]
    public class GetMxFullyConnectedSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Symbol Bias { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public int NumHidden { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public bool NoBias { get; set; } = true;

        [Parameter(Position = 5, Mandatory = false)]
        public bool Flatten { get; set; } = true;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.FullyConnected(Data, Weight, Bias, NumHidden, NoBias, Flatten, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLayerNormSymbol")]
    [Alias("mx.sym.LayerNorm")]
    [OutputType(typeof(Symbol))]
    public class GetMxLayerNormSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Gamma { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Beta { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 4, Mandatory = false)]
        public float Eps { get; set; } = 1e-05f;

        [Parameter(Position = 5, Mandatory = false)]
        public bool OutputMeanVar { get; set; } = false;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LayerNorm(Data, Gamma, Beta, Axis, Eps, OutputMeanVar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLRNSymbol")]
    [Alias("mx.sym.LRN")]
    [OutputType(typeof(Symbol))]
    public class GetMxLRNSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public uint Nsize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float Alpha { get; set; } = 0.0001f;

        [Parameter(Position = 3, Mandatory = false)]
        public float Beta { get; set; } = 0.75f;

        [Parameter(Position = 4, Mandatory = false)]
        public float Knorm { get; set; } = 2f;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LRN(Data, Nsize, Alpha, Beta, Knorm, SymbolName));
        }
    }

    [Cmdlet("Get", "MxPoolingSymbol")]
    [Alias("mx.sym.Pooling")]
    [OutputType(typeof(Symbol))]
    public class GetMxPoolingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

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

        [Parameter(Position = 11, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Pooling(Data, Kernel, PoolType, GlobalPool, CudnnOff, PoolingConvention, Stride, Pad, PValue, CountIncludePad, Layout, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSoftmaxSymbol")]
    [Alias("mx.sym.Softmax")]
    [OutputType(typeof(Symbol))]
    public class GetMxSoftmaxSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public double? Temperature { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Softmax(Data, Axis, Temperature, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSoftminSymbol")]
    [Alias("mx.sym.Softmin")]
    [OutputType(typeof(Symbol))]
    public class GetMxSoftminSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public double? Temperature { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Softmin(Data, Axis, Temperature, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogSoftmaxSymbol")]
    [Alias("mx.sym.LogSoftmax")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogSoftmaxSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public double? Temperature { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LogSoftmax(Data, Axis, Temperature, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSoftmaxActivationSymbol")]
    [Alias("mx.sym.SoftmaxActivation")]
    [OutputType(typeof(Symbol))]
    public class GetMxSoftmaxActivationSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public SoftmaxMode Mode { get; set; } = SoftmaxMode.Instance;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SoftmaxActivation(Data, Mode, SymbolName));
        }
    }

    [Cmdlet("Get", "MxUpSamplingSymbol")]
    [Alias("mx.sym.UpSampling")]
    [OutputType(typeof(Symbol))]
    public class GetMxUpSamplingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

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

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.UpSampling(Data, Scale, SampleType, NumArgs, NumFilter, MultiInputMode, Workspace, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSignsgdUpdateSymbol")]
    [Alias("mx.sym.SignsgdUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxSignsgdUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float Lr { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Wd { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SignsgdUpdate(Weight, Grad, Lr, Wd, RescaleGrad, ClipGradient, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSignumUpdateSymbol")]
    [Alias("mx.sym.SignumUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxSignumUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Mom { get; set; }

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

        [Parameter(Position = 9, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SignumUpdate(Weight, Grad, Mom, Lr, Momentum, Wd, RescaleGrad, ClipGradient, WdLh, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMultiSgdUpdateSymbol")]
    [Alias("mx.sym.MultiSgdUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxMultiSgdUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Tuple<double> Lrs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Tuple<double> Wds { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 4, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 5, Mandatory = false)]
        public int NumWeights { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MultiSgdUpdate(Data, Lrs, Wds, RescaleGrad, ClipGradient, NumWeights, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMultiSgdMomUpdateSymbol")]
    [Alias("mx.sym.MultiSgdMomUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxMultiSgdMomUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Tuple<double> Lrs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Tuple<double> Wds { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Momentum { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 6, Mandatory = false)]
        public int NumWeights { get; set; } = 1;

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MultiSgdMomUpdate(Data, Lrs, Wds, Momentum, RescaleGrad, ClipGradient, NumWeights, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMultiMpSgdUpdateSymbol")]
    [Alias("mx.sym.MultiMpSgdUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxMultiMpSgdUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Tuple<double> Lrs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Tuple<double> Wds { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 4, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 5, Mandatory = false)]
        public int NumWeights { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MultiMpSgdUpdate(Data, Lrs, Wds, RescaleGrad, ClipGradient, NumWeights, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMultiMpSgdMomUpdateSymbol")]
    [Alias("mx.sym.MultiMpSgdMomUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxMultiMpSgdMomUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Tuple<double> Lrs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Tuple<double> Wds { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Momentum { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 6, Mandatory = false)]
        public int NumWeights { get; set; } = 1;

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MultiMpSgdMomUpdate(Data, Lrs, Wds, Momentum, RescaleGrad, ClipGradient, NumWeights, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSgdUpdateSymbol")]
    [Alias("mx.sym.SgdUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxSgdUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

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

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SgdUpdate(Weight, Grad, Lr, Wd, RescaleGrad, ClipGradient, LazyUpdate, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSgdMomUpdateSymbol")]
    [Alias("mx.sym.SgdMomUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxSgdMomUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Mom { get; set; }

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

        [Parameter(Position = 9, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SgdMomUpdate(Weight, Grad, Mom, Lr, Momentum, Wd, RescaleGrad, ClipGradient, LazyUpdate, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMpSgdUpdateSymbol")]
    [Alias("mx.sym.MpSgdUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxMpSgdUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Weight32 { get; set; }

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

        [Parameter(Position = 8, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MpSgdUpdate(Weight, Grad, Weight32, Lr, Wd, RescaleGrad, ClipGradient, LazyUpdate, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMpSgdMomUpdateSymbol")]
    [Alias("mx.sym.MpSgdMomUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxMpSgdMomUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Mom { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol Weight32 { get; set; }

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

        [Parameter(Position = 10, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MpSgdMomUpdate(Weight, Grad, Mom, Weight32, Lr, Momentum, Wd, RescaleGrad, ClipGradient, LazyUpdate, SymbolName));
        }
    }

    [Cmdlet("Get", "MxFtmlUpdateSymbol")]
    [Alias("mx.sym.FtmlUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxFtmlUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol D { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol V { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public Symbol Z { get; set; }

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

        [Parameter(Position = 13, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.FtmlUpdate(Weight, Grad, D, V, Z, Lr, T, Beta1, Beta2, Epsilon, Wd, RescaleGrad, ClipGrad, SymbolName));
        }
    }

    [Cmdlet("Get", "MxAdamUpdateSymbol")]
    [Alias("mx.sym.AdamUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxAdamUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Mean { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol Var { get; set; }

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

        [Parameter(Position = 12, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.AdamUpdate(Weight, Grad, Mean, Var, Lr, Beta1, Beta2, Epsilon, Wd, RescaleGrad, ClipGradient, LazyUpdate, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRmspropUpdateSymbol")]
    [Alias("mx.sym.RmspropUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxRmspropUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol N { get; set; }

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

        [Parameter(Position = 10, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RmspropUpdate(Weight, Grad, N, Lr, Gamma1, Epsilon, Wd, RescaleGrad, ClipGradient, ClipWeights, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRmspropalexUpdateSymbol")]
    [Alias("mx.sym.RmspropalexUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxRmspropalexUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol N { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol G { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public Symbol Delta { get; set; }

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

        [Parameter(Position = 13, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RmspropalexUpdate(Weight, Grad, N, G, Delta, Lr, Gamma1, Gamma2, Epsilon, Wd, RescaleGrad, ClipGradient, ClipWeights, SymbolName));
        }
    }

    [Cmdlet("Get", "MxFtrlUpdateSymbol")]
    [Alias("mx.sym.FtrlUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxFtrlUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Z { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol N { get; set; }

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

        [Parameter(Position = 10, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.FtrlUpdate(Weight, Grad, Z, N, Lr, Lamda1, Beta, Wd, RescaleGrad, ClipGradient, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSparseAdagradUpdateSymbol")]
    [Alias("mx.sym.SparseAdagradUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxSparseAdagradUpdateSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grad { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol History { get; set; }

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

        [Parameter(Position = 8, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SparseAdagradUpdate(Weight, Grad, History, Lr, Epsilon, Wd, RescaleGrad, ClipGradient, SymbolName));
        }
    }

    [Cmdlet("Get", "MxPadSymbol")]
    [Alias("mx.sym.Pad")]
    [OutputType(typeof(Symbol))]
    public class GetMxPadSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public PadMode Mode { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape PadWidth { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public double ConstantValue { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Pad(Data, Mode, PadWidth, ConstantValue, SymbolName));
        }
    }

    [Cmdlet("Get", "MxFlattenSymbol")]
    [Alias("mx.sym.Flatten")]
    [OutputType(typeof(Symbol))]
    public class GetMxFlattenSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Flatten(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSampleUniformSymbol")]
    [Alias("mx.sym.SampleUniform")]
    [OutputType(typeof(Symbol))]
    public class GetMxSampleUniformSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Low { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol High { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SampleUniform(Low, High, Shape, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSampleNormalSymbol")]
    [Alias("mx.sym.SampleNormal")]
    [OutputType(typeof(Symbol))]
    public class GetMxSampleNormalSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Mu { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Sigma { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SampleNormal(Mu, Sigma, Shape, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSampleGammaSymbol")]
    [Alias("mx.sym.SampleGamma")]
    [OutputType(typeof(Symbol))]
    public class GetMxSampleGammaSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Alpha { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Beta { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SampleGamma(Alpha, Beta, Shape, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSampleExponentialSymbol")]
    [Alias("mx.sym.SampleExponential")]
    [OutputType(typeof(Symbol))]
    public class GetMxSampleExponentialSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lam { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SampleExponential(Lam, Shape, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSamplePoissonSymbol")]
    [Alias("mx.sym.SamplePoisson")]
    [OutputType(typeof(Symbol))]
    public class GetMxSamplePoissonSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lam { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SamplePoisson(Lam, Shape, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSampleNegativeBinomialSymbol")]
    [Alias("mx.sym.SampleNegativeBinomial")]
    [OutputType(typeof(Symbol))]
    public class GetMxSampleNegativeBinomialSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol K { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol P { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SampleNegativeBinomial(K, P, Shape, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSampleGeneralizedNegativeBinomialSymbol")]
    [Alias("mx.sym.SampleGeneralizedNegativeBinomial")]
    [OutputType(typeof(Symbol))]
    public class GetMxSampleGeneralizedNegativeBinomialSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Mu { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Alpha { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SampleGeneralizedNegativeBinomial(Mu, Alpha, Shape, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSampleMultinomialSymbol")]
    [Alias("mx.sym.SampleMultinomial")]
    [OutputType(typeof(Symbol))]
    public class GetMxSampleMultinomialSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool GetProb { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SampleMultinomial(Data, Shape, GetProb, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomUniformSymbol")]
    [Alias("mx.sym.RandomUniform")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomUniformSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Low { get; set; } = 0f;

        [Parameter(Position = 1, Mandatory = false)]
        public float High { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomUniform(Low, High, Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomNormalSymbol")]
    [Alias("mx.sym.RandomNormal")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomNormalSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Loc { get; set; } = 0f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Scale { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomNormal(Loc, Scale, Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomGammaSymbol")]
    [Alias("mx.sym.RandomGamma")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomGammaSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Alpha { get; set; } = 1f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Beta { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomGamma(Alpha, Beta, Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomExponentialSymbol")]
    [Alias("mx.sym.RandomExponential")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomExponentialSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Lam { get; set; } = 1f;

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomExponential(Lam, Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomPoissonSymbol")]
    [Alias("mx.sym.RandomPoisson")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomPoissonSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Lam { get; set; } = 1f;

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomPoisson(Lam, Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomNegativeBinomialSymbol")]
    [Alias("mx.sym.RandomNegativeBinomial")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomNegativeBinomialSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int K { get; set; } = 1;

        [Parameter(Position = 1, Mandatory = false)]
        public float P { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomNegativeBinomial(K, P, Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomGeneralizedNegativeBinomialSymbol")]
    [Alias("mx.sym.RandomGeneralizedNegativeBinomial")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomGeneralizedNegativeBinomialSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Mu { get; set; } = 1f;

        [Parameter(Position = 1, Mandatory = false)]
        public float Alpha { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomGeneralizedNegativeBinomial(Mu, Alpha, Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomRandintSymbol")]
    [Alias("mx.sym.RandomRandint")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomRandintSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Tuple<double> Low { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Tuple<double> High { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomRandint(Low, High, Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomUniformLikeSymbol")]
    [Alias("mx.sym.RandomUniformLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomUniformLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Low { get; set; } = 0f;

        [Parameter(Position = 2, Mandatory = false)]
        public float High { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomUniformLike(Data, Low, High, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomNormalLikeSymbol")]
    [Alias("mx.sym.RandomNormalLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomNormalLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Loc { get; set; } = 0f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Scale { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomNormalLike(Data, Loc, Scale, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomGammaLikeSymbol")]
    [Alias("mx.sym.RandomGammaLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomGammaLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Alpha { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Beta { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomGammaLike(Data, Alpha, Beta, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomExponentialLikeSymbol")]
    [Alias("mx.sym.RandomExponentialLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomExponentialLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Lam { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomExponentialLike(Data, Lam, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomPoissonLikeSymbol")]
    [Alias("mx.sym.RandomPoissonLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomPoissonLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Lam { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomPoissonLike(Data, Lam, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomNegativeBinomialLikeSymbol")]
    [Alias("mx.sym.RandomNegativeBinomialLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomNegativeBinomialLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int K { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public float P { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomNegativeBinomialLike(Data, K, P, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRandomGeneralizedNegativeBinomialLikeSymbol")]
    [Alias("mx.sym.RandomGeneralizedNegativeBinomialLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxRandomGeneralizedNegativeBinomialLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Mu { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Alpha { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RandomGeneralizedNegativeBinomialLike(Data, Mu, Alpha, SymbolName));
        }
    }

    [Cmdlet("Get", "MxShuffleSymbol")]
    [Alias("mx.sym.Shuffle")]
    [OutputType(typeof(Symbol))]
    public class GetMxShuffleSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Shuffle(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSampleUniqueZipfianSymbol")]
    [Alias("mx.sym.SampleUniqueZipfian")]
    [OutputType(typeof(Symbol))]
    public class GetMxSampleUniqueZipfianSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int RangeMax { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SampleUniqueZipfian(RangeMax, Shape, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinearRegressionOutputSymbol")]
    [Alias("mx.sym.LinearRegressionOutput")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinearRegressionOutputSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float GradScale { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinearRegressionOutput(Data, Label, GradScale, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMAERegressionOutputSymbol")]
    [Alias("mx.sym.MAERegressionOutput")]
    [OutputType(typeof(Symbol))]
    public class GetMxMAERegressionOutputSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float GradScale { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MAERegressionOutput(Data, Label, GradScale, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogisticRegressionOutputSymbol")]
    [Alias("mx.sym.LogisticRegressionOutput")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogisticRegressionOutputSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float GradScale { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LogisticRegressionOutput(Data, Label, GradScale, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRNNSymbol")]
    [Alias("mx.sym.RNN")]
    [OutputType(typeof(Symbol))]
    public class GetMxRNNSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Parameters { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol State { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol StateCell { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int StateSize { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public int NumLayers { get; set; }

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

        [Parameter(Position = 14, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RNN(Data, Parameters, State, StateCell, StateSize, NumLayers, Mode, Bidirectional, P, StateOutputs, ProjectionSize, LstmStateClipMin, LstmStateClipMax, LstmStateClipNan, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSplitSymbol")]
    [Alias("mx.sym.Split")]
    [OutputType(typeof(Symbol))]
    public class GetMxSplitSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumOutputs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public bool SqueezeAxis { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Split(Data, NumOutputs, Axis, SqueezeAxis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSoftmaxOutputSymbol")]
    [Alias("mx.sym.SoftmaxOutput")]
    [OutputType(typeof(Symbol))]
    public class GetMxSoftmaxOutputSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Label { get; set; }

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
        public SoftmaxoutputNormalization Normalization { get; set; } = SoftmaxoutputNormalization.Valid;

        [Parameter(Position = 8, Mandatory = false)]
        public bool OutGrad { get; set; } = false;

        [Parameter(Position = 9, Mandatory = false)]
        public float SmoothAlpha { get; set; } = 0f;

        [Parameter(Position = 10, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SoftmaxOutput(Data, Label, GradScale, IgnoreLabel, MultiOutput, UseIgnore, PreserveShape, Normalization, OutGrad, SmoothAlpha, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSwapAxisSymbol")]
    [Alias("mx.sym.SwapAxis")]
    [OutputType(typeof(Symbol))]
    public class GetMxSwapAxisSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Dim1 { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public int Dim2 { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SwapAxis(Data, Dim1, Dim2, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArgmaxSymbol")]
    [Alias("mx.sym.Argmax")]
    [OutputType(typeof(Symbol))]
    public class GetMxArgmaxSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Argmax(Data, Axis, Keepdims, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArgminSymbol")]
    [Alias("mx.sym.Argmin")]
    [OutputType(typeof(Symbol))]
    public class GetMxArgminSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Argmin(Data, Axis, Keepdims, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArgmaxChannelSymbol")]
    [Alias("mx.sym.ArgmaxChannel")]
    [OutputType(typeof(Symbol))]
    public class GetMxArgmaxChannelSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ArgmaxChannel(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxPickSymbol")]
    [Alias("mx.sym.Pick")]
    [OutputType(typeof(Symbol))]
    public class GetMxPickSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Index { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int? Axis { get; set; } = -1;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public PickMode Mode { get; set; } = PickMode.Clip;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Pick(Data, Index, Axis, Keepdims, Mode, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSumSymbol")]
    [Alias("mx.sym.Sum")]
    [OutputType(typeof(Symbol))]
    public class GetMxSumSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Sum(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSum2Symbol")]
    [Alias("mx.sym.Sum2")]
    [OutputType(typeof(Symbol))]
    public class GetMxSum2Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Axis { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Sum(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMeanSymbol")]
    [Alias("mx.sym.Mean")]
    [OutputType(typeof(Symbol))]
    public class GetMxMeanSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Mean(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMean2Symbol")]
    [Alias("mx.sym.Mean2")]
    [OutputType(typeof(Symbol))]
    public class GetMxMean2Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Axis { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Mean(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxProdSymbol")]
    [Alias("mx.sym.Prod")]
    [OutputType(typeof(Symbol))]
    public class GetMxProdSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Prod(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxNansumSymbol")]
    [Alias("mx.sym.Nansum")]
    [OutputType(typeof(Symbol))]
    public class GetMxNansumSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Nansum(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxNanprodSymbol")]
    [Alias("mx.sym.Nanprod")]
    [OutputType(typeof(Symbol))]
    public class GetMxNanprodSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Nanprod(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMaxSymbol")]
    [Alias("mx.sym.Max")]
    [OutputType(typeof(Symbol))]
    public class GetMxMaxSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Max(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMinSymbol")]
    [Alias("mx.sym.Min")]
    [OutputType(typeof(Symbol))]
    public class GetMxMinSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Min(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastAxisSymbol")]
    [Alias("mx.sym.BroadcastAxis")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastAxisSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Size { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastAxis(Data, Axis, Size, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastToSymbol")]
    [Alias("mx.sym.BroadcastTo")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastToSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastTo(Data, Shape, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastLikeSymbol")]
    [Alias("mx.sym.BroadcastLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape LhsAxes { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Shape RhsAxes { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastLike(Lhs, Rhs, LhsAxes, RhsAxes, SymbolName));
        }
    }

    [Cmdlet("Get", "MxNormSymbol")]
    [Alias("mx.sym.Norm")]
    [OutputType(typeof(Symbol))]
    public class GetMxNormSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Ord { get; set; } = 2;

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public NormOutDtype? OutDtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Norm(Data, Ord, Axis, OutDtype, Keepdims, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCastStorageSymbol")]
    [Alias("mx.sym.CastStorage")]
    [OutputType(typeof(Symbol))]
    public class GetMxCastStorageSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public StorageStype Stype { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.CastStorage(Data, Stype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxWhereSymbol")]
    [Alias("mx.sym.Where")]
    [OutputType(typeof(Symbol))]
    public class GetMxWhereSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Condition { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol X { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Y { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Where(Condition, X, Y, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDiagSymbol")]
    [Alias("mx.sym.Diag")]
    [OutputType(typeof(Symbol))]
    public class GetMxDiagSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int K { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis1 { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public int Axis2 { get; set; } = 1;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Diag(Data, K, Axis1, Axis2, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDotSymbol")]
    [Alias("mx.sym.Dot")]
    [OutputType(typeof(Symbol))]
    public class GetMxDotSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool TransposeA { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool TransposeB { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public DotForwardStype? ForwardStype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Dot(Lhs, Rhs, TransposeA, TransposeB, ForwardStype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBatchDotSymbol")]
    [Alias("mx.sym.BatchDot")]
    [OutputType(typeof(Symbol))]
    public class GetMxBatchDotSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool TransposeA { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool TransposeB { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public BatchDotForwardStype? ForwardStype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BatchDot(Lhs, Rhs, TransposeA, TransposeB, ForwardStype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastAddSymbol")]
    [Alias("mx.sym.BroadcastAdd")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastAddSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastAdd(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastSubSymbol")]
    [Alias("mx.sym.BroadcastSub")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastSubSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastSub(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastMulSymbol")]
    [Alias("mx.sym.BroadcastMul")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastMulSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastMul(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastDivSymbol")]
    [Alias("mx.sym.BroadcastDiv")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastDivSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastDiv(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastModSymbol")]
    [Alias("mx.sym.BroadcastMod")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastModSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastMod(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastPowerSymbol")]
    [Alias("mx.sym.BroadcastPower")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastPowerSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastPower(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastMaximumSymbol")]
    [Alias("mx.sym.BroadcastMaximum")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastMaximumSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastMaximum(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastMinimumSymbol")]
    [Alias("mx.sym.BroadcastMinimum")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastMinimumSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastMinimum(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastHypotSymbol")]
    [Alias("mx.sym.BroadcastHypot")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastHypotSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastHypot(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastEqualSymbol")]
    [Alias("mx.sym.BroadcastEqual")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastEqualSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastEqual(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastNotEqualSymbol")]
    [Alias("mx.sym.BroadcastNotEqual")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastNotEqualSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastNotEqual(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastGreaterSymbol")]
    [Alias("mx.sym.BroadcastGreater")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastGreaterSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastGreater(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastGreaterEqualSymbol")]
    [Alias("mx.sym.BroadcastGreaterEqual")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastGreaterEqualSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastGreaterEqual(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastLesserSymbol")]
    [Alias("mx.sym.BroadcastLesser")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastLesserSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastLesser(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastLesserEqualSymbol")]
    [Alias("mx.sym.BroadcastLesserEqual")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastLesserEqualSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastLesserEqual(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastLogicalAndSymbol")]
    [Alias("mx.sym.BroadcastLogicalAnd")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastLogicalAndSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastLogicalAnd(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastLogicalOrSymbol")]
    [Alias("mx.sym.BroadcastLogicalOr")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastLogicalOrSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastLogicalOr(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBroadcastLogicalXorSymbol")]
    [Alias("mx.sym.BroadcastLogicalXor")]
    [OutputType(typeof(Symbol))]
    public class GetMxBroadcastLogicalXorSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BroadcastLogicalXor(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxElemwiseAddSymbol")]
    [Alias("mx.sym.ElemwiseAdd")]
    [OutputType(typeof(Symbol))]
    public class GetMxElemwiseAddSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ElemwiseAdd(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGradAddSymbol")]
    [Alias("mx.sym.GradAdd")]
    [OutputType(typeof(Symbol))]
    public class GetMxGradAddSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.GradAdd(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxElemwiseSubSymbol")]
    [Alias("mx.sym.ElemwiseSub")]
    [OutputType(typeof(Symbol))]
    public class GetMxElemwiseSubSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ElemwiseSub(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxElemwiseMulSymbol")]
    [Alias("mx.sym.ElemwiseMul")]
    [OutputType(typeof(Symbol))]
    public class GetMxElemwiseMulSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ElemwiseMul(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxElemwiseDivSymbol")]
    [Alias("mx.sym.ElemwiseDiv")]
    [OutputType(typeof(Symbol))]
    public class GetMxElemwiseDivSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ElemwiseDiv(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxModSymbol")]
    [Alias("mx.sym.Mod")]
    [OutputType(typeof(Symbol))]
    public class GetMxModSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Mod(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxPowerSymbol")]
    [Alias("mx.sym.Power")]
    [OutputType(typeof(Symbol))]
    public class GetMxPowerSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Power(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMaximumSymbol")]
    [Alias("mx.sym.Maximum")]
    [OutputType(typeof(Symbol))]
    public class GetMxMaximumSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Maximum(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMinimumSymbol")]
    [Alias("mx.sym.Minimum")]
    [OutputType(typeof(Symbol))]
    public class GetMxMinimumSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Minimum(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxHypotSymbol")]
    [Alias("mx.sym.Hypot")]
    [OutputType(typeof(Symbol))]
    public class GetMxHypotSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Hypot(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxEqualSymbol")]
    [Alias("mx.sym.Equal")]
    [OutputType(typeof(Symbol))]
    public class GetMxEqualSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Equal(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxNotEqualSymbol")]
    [Alias("mx.sym.NotEqual")]
    [OutputType(typeof(Symbol))]
    public class GetMxNotEqualSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.NotEqual(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGreaterSymbol")]
    [Alias("mx.sym.Greater")]
    [OutputType(typeof(Symbol))]
    public class GetMxGreaterSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Greater(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGreaterEqualSymbol")]
    [Alias("mx.sym.GreaterEqual")]
    [OutputType(typeof(Symbol))]
    public class GetMxGreaterEqualSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.GreaterEqual(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLesserSymbol")]
    [Alias("mx.sym.Lesser")]
    [OutputType(typeof(Symbol))]
    public class GetMxLesserSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Lesser(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLesserEqualSymbol")]
    [Alias("mx.sym.LesserEqual")]
    [OutputType(typeof(Symbol))]
    public class GetMxLesserEqualSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LesserEqual(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogicalAndSymbol")]
    [Alias("mx.sym.LogicalAnd")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogicalAndSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LogicalAnd(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogicalOrSymbol")]
    [Alias("mx.sym.LogicalOr")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogicalOrSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LogicalOr(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogicalXorSymbol")]
    [Alias("mx.sym.LogicalXor")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogicalXorSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LogicalXor(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxPlusScalarSymbol")]
    [Alias("mx.sym.PlusScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxPlusScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.PlusScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMinusScalarSymbol")]
    [Alias("mx.sym.MinusScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxMinusScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MinusScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRminusScalarSymbol")]
    [Alias("mx.sym.RminusScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxRminusScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RminusScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMulScalarSymbol")]
    [Alias("mx.sym.MulScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxMulScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MulScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDivScalarSymbol")]
    [Alias("mx.sym.DivScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxDivScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.DivScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRdivScalarSymbol")]
    [Alias("mx.sym.RdivScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxRdivScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RdivScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxModScalarSymbol")]
    [Alias("mx.sym.ModScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxModScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ModScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRmodScalarSymbol")]
    [Alias("mx.sym.RmodScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxRmodScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RmodScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMaximumScalarSymbol")]
    [Alias("mx.sym.MaximumScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxMaximumScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MaximumScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMinimumScalarSymbol")]
    [Alias("mx.sym.MinimumScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxMinimumScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MinimumScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxPowerScalarSymbol")]
    [Alias("mx.sym.PowerScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxPowerScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.PowerScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRpowerScalarSymbol")]
    [Alias("mx.sym.RpowerScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxRpowerScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RpowerScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxHypotScalarSymbol")]
    [Alias("mx.sym.HypotScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxHypotScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.HypotScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSmoothL1Symbol")]
    [Alias("mx.sym.SmoothL1")]
    [OutputType(typeof(Symbol))]
    public class GetMxSmoothL1Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SmoothL1(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxEqualScalarSymbol")]
    [Alias("mx.sym.EqualScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxEqualScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.EqualScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxNotEqualScalarSymbol")]
    [Alias("mx.sym.NotEqualScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxNotEqualScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.NotEqualScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGreaterScalarSymbol")]
    [Alias("mx.sym.GreaterScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxGreaterScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.GreaterScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGreaterEqualScalarSymbol")]
    [Alias("mx.sym.GreaterEqualScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxGreaterEqualScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.GreaterEqualScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLesserScalarSymbol")]
    [Alias("mx.sym.LesserScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxLesserScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LesserScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLesserEqualScalarSymbol")]
    [Alias("mx.sym.LesserEqualScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxLesserEqualScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LesserEqualScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogicalAndScalarSymbol")]
    [Alias("mx.sym.LogicalAndScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogicalAndScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LogicalAndScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogicalOrScalarSymbol")]
    [Alias("mx.sym.LogicalOrScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogicalOrScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LogicalOrScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogicalXorScalarSymbol")]
    [Alias("mx.sym.LogicalXorScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogicalXorScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LogicalXorScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxScatterElemwiseDivSymbol")]
    [Alias("mx.sym.ScatterElemwiseDiv")]
    [OutputType(typeof(Symbol))]
    public class GetMxScatterElemwiseDivSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ScatterElemwiseDiv(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxScatterPlusScalarSymbol")]
    [Alias("mx.sym.ScatterPlusScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxScatterPlusScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ScatterPlusScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxScatterMinusScalarSymbol")]
    [Alias("mx.sym.ScatterMinusScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxScatterMinusScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ScatterMinusScalar(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxAddNSymbol")]
    [Alias("mx.sym.AddN")]
    [OutputType(typeof(Symbol))]
    public class GetMxAddNSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Args { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.AddN(Args, SymbolName));
        }
    }

    [Cmdlet("Get", "MxReluSymbol")]
    [Alias("mx.sym.Relu")]
    [OutputType(typeof(Symbol))]
    public class GetMxReluSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Relu(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSigmoidSymbol")]
    [Alias("mx.sym.Sigmoid")]
    [OutputType(typeof(Symbol))]
    public class GetMxSigmoidSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Sigmoid(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxHardSigmoidSymbol")]
    [Alias("mx.sym.HardSigmoid")]
    [OutputType(typeof(Symbol))]
    public class GetMxHardSigmoidSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Alpha { get; set; } = 0.2f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Beta { get; set; } = 0.5f;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.HardSigmoid(Data, Alpha, Beta, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSoftsignSymbol")]
    [Alias("mx.sym.Softsign")]
    [OutputType(typeof(Symbol))]
    public class GetMxSoftsignSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Softsign(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCopySymbol")]
    [Alias("mx.sym.Copy")]
    [OutputType(typeof(Symbol))]
    public class GetMxCopySymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Copy(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBlockGradSymbol")]
    [Alias("mx.sym.BlockGrad")]
    [OutputType(typeof(Symbol))]
    public class GetMxBlockGradSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BlockGrad(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMakeLossSymbol")]
    [Alias("mx.sym.MakeLoss")]
    [OutputType(typeof(Symbol))]
    public class GetMxMakeLossSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MakeLoss(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxIdentityWithAttrLikeRhsSymbol")]
    [Alias("mx.sym.IdentityWithAttrLikeRhs")]
    [OutputType(typeof(Symbol))]
    public class GetMxIdentityWithAttrLikeRhsSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.IdentityWithAttrLikeRhs(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxReshapeLikeSymbol")]
    [Alias("mx.sym.ReshapeLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxReshapeLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ReshapeLike(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxShapeArraySymbol")]
    [Alias("mx.sym.ShapeArray")]
    [OutputType(typeof(Symbol))]
    public class GetMxShapeArraySymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? LhsBegin { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public int? LhsEnd { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public int? RhsBegin { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public int? RhsEnd { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ShapeArray(Data, LhsBegin, LhsEnd, RhsBegin, RhsEnd, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSizeArraySymbol")]
    [Alias("mx.sym.SizeArray")]
    [OutputType(typeof(Symbol))]
    public class GetMxSizeArraySymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SizeArray(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCastSymbol")]
    [Alias("mx.sym.Cast")]
    [OutputType(typeof(Symbol))]
    public class GetMxCastSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public DType Dtype { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Cast(Data, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxNegativeSymbol")]
    [Alias("mx.sym.Negative")]
    [OutputType(typeof(Symbol))]
    public class GetMxNegativeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Negative(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxReciprocalSymbol")]
    [Alias("mx.sym.Reciprocal")]
    [OutputType(typeof(Symbol))]
    public class GetMxReciprocalSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Reciprocal(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxAbsSymbol")]
    [Alias("mx.sym.Abs")]
    [OutputType(typeof(Symbol))]
    public class GetMxAbsSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Abs(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSignSymbol")]
    [Alias("mx.sym.Sign")]
    [OutputType(typeof(Symbol))]
    public class GetMxSignSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Sign(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRoundSymbol")]
    [Alias("mx.sym.Round")]
    [OutputType(typeof(Symbol))]
    public class GetMxRoundSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Round(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRintSymbol")]
    [Alias("mx.sym.Rint")]
    [OutputType(typeof(Symbol))]
    public class GetMxRintSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Rint(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCeilSymbol")]
    [Alias("mx.sym.Ceil")]
    [OutputType(typeof(Symbol))]
    public class GetMxCeilSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Ceil(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxFloorSymbol")]
    [Alias("mx.sym.Floor")]
    [OutputType(typeof(Symbol))]
    public class GetMxFloorSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Floor(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxTruncSymbol")]
    [Alias("mx.sym.Trunc")]
    [OutputType(typeof(Symbol))]
    public class GetMxTruncSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Trunc(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxFixSymbol")]
    [Alias("mx.sym.Fix")]
    [OutputType(typeof(Symbol))]
    public class GetMxFixSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Fix(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSquareSymbol")]
    [Alias("mx.sym.Square")]
    [OutputType(typeof(Symbol))]
    public class GetMxSquareSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Square(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSqrtSymbol")]
    [Alias("mx.sym.Sqrt")]
    [OutputType(typeof(Symbol))]
    public class GetMxSqrtSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Sqrt(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRsqrtSymbol")]
    [Alias("mx.sym.Rsqrt")]
    [OutputType(typeof(Symbol))]
    public class GetMxRsqrtSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Rsqrt(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCbrtSymbol")]
    [Alias("mx.sym.Cbrt")]
    [OutputType(typeof(Symbol))]
    public class GetMxCbrtSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Cbrt(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxErfSymbol")]
    [Alias("mx.sym.Erf")]
    [OutputType(typeof(Symbol))]
    public class GetMxErfSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Erf(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxErfinvSymbol")]
    [Alias("mx.sym.Erfinv")]
    [OutputType(typeof(Symbol))]
    public class GetMxErfinvSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Erfinv(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRcbrtSymbol")]
    [Alias("mx.sym.Rcbrt")]
    [OutputType(typeof(Symbol))]
    public class GetMxRcbrtSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Rcbrt(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxExpSymbol")]
    [Alias("mx.sym.Exp")]
    [OutputType(typeof(Symbol))]
    public class GetMxExpSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Exp(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogSymbol")]
    [Alias("mx.sym.Log")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Log(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLog10Symbol")]
    [Alias("mx.sym.Log10")]
    [OutputType(typeof(Symbol))]
    public class GetMxLog10Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Log10(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLog2Symbol")]
    [Alias("mx.sym.Log2")]
    [OutputType(typeof(Symbol))]
    public class GetMxLog2Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Log2(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLog1PSymbol")]
    [Alias("mx.sym.Log1P")]
    [OutputType(typeof(Symbol))]
    public class GetMxLog1PSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Log1P(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxExpm1Symbol")]
    [Alias("mx.sym.Expm1")]
    [OutputType(typeof(Symbol))]
    public class GetMxExpm1Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Expm1(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGammaSymbol")]
    [Alias("mx.sym.Gamma")]
    [OutputType(typeof(Symbol))]
    public class GetMxGammaSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Gamma(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGammalnSymbol")]
    [Alias("mx.sym.Gammaln")]
    [OutputType(typeof(Symbol))]
    public class GetMxGammalnSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Gammaln(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLogicalNotSymbol")]
    [Alias("mx.sym.LogicalNot")]
    [OutputType(typeof(Symbol))]
    public class GetMxLogicalNotSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LogicalNot(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSinSymbol")]
    [Alias("mx.sym.Sin")]
    [OutputType(typeof(Symbol))]
    public class GetMxSinSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Sin(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCosSymbol")]
    [Alias("mx.sym.Cos")]
    [OutputType(typeof(Symbol))]
    public class GetMxCosSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Cos(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxTanSymbol")]
    [Alias("mx.sym.Tan")]
    [OutputType(typeof(Symbol))]
    public class GetMxTanSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Tan(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArcsinSymbol")]
    [Alias("mx.sym.Arcsin")]
    [OutputType(typeof(Symbol))]
    public class GetMxArcsinSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Arcsin(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArccosSymbol")]
    [Alias("mx.sym.Arccos")]
    [OutputType(typeof(Symbol))]
    public class GetMxArccosSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Arccos(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArctanSymbol")]
    [Alias("mx.sym.Arctan")]
    [OutputType(typeof(Symbol))]
    public class GetMxArctanSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Arctan(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDegreesSymbol")]
    [Alias("mx.sym.Degrees")]
    [OutputType(typeof(Symbol))]
    public class GetMxDegreesSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Degrees(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRadiansSymbol")]
    [Alias("mx.sym.Radians")]
    [OutputType(typeof(Symbol))]
    public class GetMxRadiansSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Radians(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSinhSymbol")]
    [Alias("mx.sym.Sinh")]
    [OutputType(typeof(Symbol))]
    public class GetMxSinhSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Sinh(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCoshSymbol")]
    [Alias("mx.sym.Cosh")]
    [OutputType(typeof(Symbol))]
    public class GetMxCoshSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Cosh(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxTanhSymbol")]
    [Alias("mx.sym.Tanh")]
    [OutputType(typeof(Symbol))]
    public class GetMxTanhSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Tanh(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArcsinhSymbol")]
    [Alias("mx.sym.Arcsinh")]
    [OutputType(typeof(Symbol))]
    public class GetMxArcsinhSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Arcsinh(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArccoshSymbol")]
    [Alias("mx.sym.Arccosh")]
    [OutputType(typeof(Symbol))]
    public class GetMxArccoshSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Arccosh(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArctanhSymbol")]
    [Alias("mx.sym.Arctanh")]
    [OutputType(typeof(Symbol))]
    public class GetMxArctanhSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Arctanh(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxHistogramSymbol")]
    [Alias("mx.sym.Histogram")]
    [OutputType(typeof(Symbol))]
    public class GetMxHistogramSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Bins { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int? BinCnt { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Tuple<double> Range { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Histogram(Data, Bins, BinCnt, Range, SymbolName));
        }
    }

    [Cmdlet("Get", "MxEmbeddingSymbol")]
    [Alias("mx.sym.Embedding")]
    [OutputType(typeof(Symbol))]
    public class GetMxEmbeddingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int InputDim { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int OutputDim { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public bool SparseGrad { get; set; } = false;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Embedding(Data, Weight, InputDim, OutputDim, Dtype, SparseGrad, SymbolName));
        }
    }

    [Cmdlet("Get", "MxTakeSymbol")]
    [Alias("mx.sym.Take")]
    [OutputType(typeof(Symbol))]
    public class GetMxTakeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Indices { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public TakeMode Mode { get; set; } = TakeMode.Clip;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Take(A, Indices, Axis, Mode, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBatchTakeSymbol")]
    [Alias("mx.sym.BatchTake")]
    [OutputType(typeof(Symbol))]
    public class GetMxBatchTakeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Indices { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BatchTake(A, Indices, SymbolName));
        }
    }

    [Cmdlet("Get", "MxOneHotSymbol")]
    [Alias("mx.sym.OneHot")]
    [OutputType(typeof(Symbol))]
    public class GetMxOneHotSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Indices { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Depth { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public double OnValue { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public double OffValue { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.OneHot(Indices, Depth, OnValue, OffValue, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGatherNdSymbol")]
    [Alias("mx.sym.GatherNd")]
    [OutputType(typeof(Symbol))]
    public class GetMxGatherNdSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Indices { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.GatherNd(Data, Indices, SymbolName));
        }
    }

    [Cmdlet("Get", "MxScatterNdSymbol")]
    [Alias("mx.sym.ScatterNd")]
    [OutputType(typeof(Symbol))]
    public class GetMxScatterNdSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Indices { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape Shape { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ScatterNd(Data, Indices, Shape, SymbolName));
        }
    }

    [Cmdlet("Get", "MxScatterSetNdSymbol")]
    [Alias("mx.sym.ScatterSetNd")]
    [OutputType(typeof(Symbol))]
    public class GetMxScatterSetNdSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Indices { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Shape Shape { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ScatterSetNd(Lhs, Rhs, Indices, Shape, SymbolName));
        }
    }

    [Cmdlet("Get", "MxZerosWithoutDtypeSymbol")]
    [Alias("mx.sym.ZerosWithoutDtype")]
    [OutputType(typeof(Symbol))]
    public class GetMxZerosWithoutDtypeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ZerosWithoutDtype(Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxZerosSymbol")]
    [Alias("mx.sym.Zeros")]
    [OutputType(typeof(Symbol))]
    public class GetMxZerosSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Zeros(Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxEyeSymbol")]
    [Alias("mx.sym.Eye")]
    [OutputType(typeof(Symbol))]
    public class GetMxEyeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int N { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int M { get; set; } = 0;

        [Parameter(Position = 2, Mandatory = false)]
        public int K { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Eye(N, M, K, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxOnesSymbol")]
    [Alias("mx.sym.Ones")]
    [OutputType(typeof(Symbol))]
    public class GetMxOnesSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Ones(Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxEmptySymbol")]
    [Alias("mx.sym.Empty")]
    [OutputType(typeof(Symbol))]
    public class GetMxEmptySymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Empty(Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxFullSymbol")]
    [Alias("mx.sym.Full")]
    [OutputType(typeof(Symbol))]
    public class GetMxFullSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public double Value { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Full(Value, Shape, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArangeSymbol")]
    [Alias("mx.sym.Arange")]
    [OutputType(typeof(Symbol))]
    public class GetMxArangeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public double Start { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public double? Stop { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public double Step { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public int Repeat { get; set; } = 1;

        [Parameter(Position = 4, Mandatory = false)]
        public bool InferRange { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Arange(Start, Stop, Step, Repeat, InferRange, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxZerosLikeSymbol")]
    [Alias("mx.sym.ZerosLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxZerosLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ZerosLike(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxOnesLikeSymbol")]
    [Alias("mx.sym.OnesLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxOnesLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.OnesLike(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgGemmSymbol")]
    [Alias("mx.sym.LinalgGemm")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgGemmSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol B { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol C { get; set; }

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

        [Parameter(Position = 8, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgGemm(A, B, C, TransposeA, TransposeB, Alpha, Beta, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgGemm2Symbol")]
    [Alias("mx.sym.LinalgGemm2")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgGemm2Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol B { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool TransposeA { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool TransposeB { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public double Alpha { get; set; } = 1;

        [Parameter(Position = 5, Mandatory = false)]
        public int Axis { get; set; } = -2;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgGemm2(A, B, TransposeA, TransposeB, Alpha, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgPotrfSymbol")]
    [Alias("mx.sym.LinalgPotrf")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgPotrfSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgPotrf(A, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgPotriSymbol")]
    [Alias("mx.sym.LinalgPotri")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgPotriSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgPotri(A, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgTrmmSymbol")]
    [Alias("mx.sym.LinalgTrmm")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgTrmmSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol B { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool Transpose { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Rightside { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public bool Lower { get; set; } = true;

        [Parameter(Position = 5, Mandatory = false)]
        public double Alpha { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgTrmm(A, B, Transpose, Rightside, Lower, Alpha, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgTrsmSymbol")]
    [Alias("mx.sym.LinalgTrsm")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgTrsmSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol B { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool Transpose { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Rightside { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public bool Lower { get; set; } = true;

        [Parameter(Position = 5, Mandatory = false)]
        public double Alpha { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgTrsm(A, B, Transpose, Rightside, Lower, Alpha, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgSumlogdiagSymbol")]
    [Alias("mx.sym.LinalgSumlogdiag")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgSumlogdiagSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgSumlogdiag(A, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgSyrkSymbol")]
    [Alias("mx.sym.LinalgSyrk")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgSyrkSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public bool Transpose { get; set; } = false;

        [Parameter(Position = 2, Mandatory = false)]
        public double Alpha { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgSyrk(A, Transpose, Alpha, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgGelqfSymbol")]
    [Alias("mx.sym.LinalgGelqf")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgGelqfSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgGelqf(A, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinalgSyevdSymbol")]
    [Alias("mx.sym.LinalgSyevd")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinalgSyevdSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol A { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.LinalgSyevd(A, SymbolName));
        }
    }

    [Cmdlet("Get", "MxReshapeSymbol")]
    [Alias("mx.sym.Reshape")]
    [OutputType(typeof(Symbol))]
    public class GetMxReshapeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Reverse { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Reshape(Data, Shape, Reverse, SymbolName));
        }
    }

    [Cmdlet("Get", "MxTransposeSymbol")]
    [Alias("mx.sym.Transpose")]
    [OutputType(typeof(Symbol))]
    public class GetMxTransposeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axes { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Transpose(Data, Axes, SymbolName));
        }
    }

    [Cmdlet("Get", "MxExpandDimsSymbol")]
    [Alias("mx.sym.ExpandDims")]
    [OutputType(typeof(Symbol))]
    public class GetMxExpandDimsSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Axis { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ExpandDims(Data, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSliceSymbol")]
    [Alias("mx.sym.Slice")]
    [OutputType(typeof(Symbol))]
    public class GetMxSliceSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Begin { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape End { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public Shape Step { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Slice(Data, Begin, End, Step, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSliceChannelSymbol")]
    [Alias("mx.sym.SliceChannel")]
    [OutputType(typeof(Symbol))]
    public class GetMxSliceChannelSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumOutputs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public bool SqueezeAxis { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SliceChannel(Data, NumOutputs, Axis, SqueezeAxis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSliceAssignSymbol")]
    [Alias("mx.sym.SliceAssign")]
    [OutputType(typeof(Symbol))]
    public class GetMxSliceAssignSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape Begin { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Shape End { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public Shape Step { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SliceAssign(Lhs, Rhs, Begin, End, Step, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSliceAssignScalarSymbol")]
    [Alias("mx.sym.SliceAssignScalar")]
    [OutputType(typeof(Symbol))]
    public class GetMxSliceAssignScalarSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Begin { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape End { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public double Scalar { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public Shape Step { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SliceAssignScalar(Data, Begin, End, Scalar, Step, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSliceAxisSymbol")]
    [Alias("mx.sym.SliceAxis")]
    [OutputType(typeof(Symbol))]
    public class GetMxSliceAxisSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Axis { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int Begin { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int? End { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SliceAxis(Data, Axis, Begin, End, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSliceLikeSymbol")]
    [Alias("mx.sym.SliceLike")]
    [OutputType(typeof(Symbol))]
    public class GetMxSliceLikeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol ShapeLike { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Axes { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SliceLike(Data, ShapeLike, Axes, SymbolName));
        }
    }

    [Cmdlet("Get", "MxClipSymbol")]
    [Alias("mx.sym.Clip")]
    [OutputType(typeof(Symbol))]
    public class GetMxClipSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float AMin { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float AMax { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Clip(Data, AMin, AMax, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRepeatSymbol")]
    [Alias("mx.sym.Repeat")]
    [OutputType(typeof(Symbol))]
    public class GetMxRepeatSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int Repeats { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int? Axis { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Repeat(Data, Repeats, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxTileSymbol")]
    [Alias("mx.sym.Tile")]
    [OutputType(typeof(Symbol))]
    public class GetMxTileSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Reps { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Tile(Data, Reps, SymbolName));
        }
    }

    [Cmdlet("Get", "MxReverseSymbol")]
    [Alias("mx.sym.Reverse")]
    [OutputType(typeof(Symbol))]
    public class GetMxReverseSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Axis { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Reverse(Data, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxStackSymbol")]
    [Alias("mx.sym.Stack")]
    [OutputType(typeof(Symbol))]
    public class GetMxStackSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Stack(Data, NumArgs, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSqueezeSymbol")]
    [Alias("mx.sym.Squeeze")]
    [OutputType(typeof(Symbol))]
    public class GetMxSqueezeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Squeeze(Data, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDepthToSpaceSymbol")]
    [Alias("mx.sym.DepthToSpace")]
    [OutputType(typeof(Symbol))]
    public class GetMxDepthToSpaceSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int BlockSize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.DepthToSpace(Data, BlockSize, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSpaceToDepthSymbol")]
    [Alias("mx.sym.SpaceToDepth")]
    [OutputType(typeof(Symbol))]
    public class GetMxSpaceToDepthSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int BlockSize { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SpaceToDepth(Data, BlockSize, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSplitV2Symbol")]
    [Alias("mx.sym.SplitV2")]
    [OutputType(typeof(Symbol))]
    public class GetMxSplitV2Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Shape Indices { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public bool SqueezeAxis { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public int Sections { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SplitV2(Data, Indices, Axis, SqueezeAxis, Sections, SymbolName));
        }
    }

    [Cmdlet("Get", "MxTopkSymbol")]
    [Alias("mx.sym.Topk")]
    [OutputType(typeof(Symbol))]
    public class GetMxTopkSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

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

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Topk(Data, Axis, K, RetTyp, IsAscend, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSortSymbol")]
    [Alias("mx.sym.Sort")]
    [OutputType(typeof(Symbol))]
    public class GetMxSortSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool IsAscend { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Sort(Data, Axis, IsAscend, SymbolName));
        }
    }

    [Cmdlet("Get", "MxArgsortSymbol")]
    [Alias("mx.sym.Argsort")]
    [OutputType(typeof(Symbol))]
    public class GetMxArgsortSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = -1;

        [Parameter(Position = 2, Mandatory = false)]
        public bool IsAscend { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Argsort(Data, Axis, IsAscend, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRavelMultiIndexSymbol")]
    [Alias("mx.sym.RavelMultiIndex")]
    [OutputType(typeof(Symbol))]
    public class GetMxRavelMultiIndexSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.RavelMultiIndex(Data, Shape, SymbolName));
        }
    }

    [Cmdlet("Get", "MxUnravelIndexSymbol")]
    [Alias("mx.sym.UnravelIndex")]
    [OutputType(typeof(Symbol))]
    public class GetMxUnravelIndexSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.UnravelIndex(Data, Shape, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSparseRetainSymbol")]
    [Alias("mx.sym.SparseRetain")]
    [OutputType(typeof(Symbol))]
    public class GetMxSparseRetainSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Indices { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SparseRetain(Data, Indices, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSquareSumSymbol")]
    [Alias("mx.sym.SquareSum")]
    [OutputType(typeof(Symbol))]
    public class GetMxSquareSumSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public bool Keepdims { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Exclude { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SquareSum(Data, Axis, Keepdims, Exclude, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBilinearSamplerSymbol")]
    [Alias("mx.sym.BilinearSampler")]
    [OutputType(typeof(Symbol))]
    public class GetMxBilinearSamplerSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Grid { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool? CudnnOff { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.BilinearSampler(Data, Grid, CudnnOff, SymbolName));
        }
    }

    [Cmdlet("Get", "MxConvolutionV1Symbol")]
    [Alias("mx.sym.ConvolutionV1")]
    [OutputType(typeof(Symbol))]
    public class GetMxConvolutionV1Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Bias { get; set; }

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

        [Parameter(Position = 14, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ConvolutionV1(Data, Weight, Bias, Kernel, NumFilter, Stride, Dilate, Pad, NumGroup, Workspace, NoBias, CudnnTune, CudnnOff, Layout, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCorrelationSymbol")]
    [Alias("mx.sym.Correlation")]
    [OutputType(typeof(Symbol))]
    public class GetMxCorrelationSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data1 { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Data2 { get; set; }

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

        [Parameter(Position = 8, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Correlation(Data1, Data2, KernelSize, MaxDisplacement, Stride1, Stride2, PadSize, IsMultiply, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCropSymbol")]
    [Alias("mx.sym.Crop")]
    [OutputType(typeof(Symbol))]
    public class GetMxCropSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape Offset { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public Shape HW { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public bool CenterCrop { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Crop(Data, NumArgs, Offset, HW, CenterCrop, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCrossDeviceCopySymbol")]
    [Alias("mx.sym.CrossDeviceCopy")]
    [OutputType(typeof(Symbol))]
    public class GetMxCrossDeviceCopySymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.CrossDeviceCopy(SymbolName));
        }
    }

    [Cmdlet("Get", "MxNativeSymbol")]
    [Alias("mx.sym.Native")]
    [OutputType(typeof(Symbol))]
    public class GetMxNativeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public System.IntPtr Info { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool NeedTopGrad { get; set; } = true;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Native(Data, Info, NeedTopGrad, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGridGeneratorSymbol")]
    [Alias("mx.sym.GridGenerator")]
    [OutputType(typeof(Symbol))]
    public class GetMxGridGeneratorSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public GridgeneratorTransformType TransformType { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public Shape TargetShape { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.GridGenerator(Data, TransformType, TargetShape, SymbolName));
        }
    }

    [Cmdlet("Get", "MxInstanceNormSymbol")]
    [Alias("mx.sym.InstanceNorm")]
    [OutputType(typeof(Symbol))]
    public class GetMxInstanceNormSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Gamma { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Beta { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float Eps { get; set; } = 0.001f;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.InstanceNorm(Data, Gamma, Beta, Eps, SymbolName));
        }
    }

    [Cmdlet("Get", "MxL2NormalizationSymbol")]
    [Alias("mx.sym.L2Normalization")]
    [OutputType(typeof(Symbol))]
    public class GetMxL2NormalizationSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Eps { get; set; } = 1e-10f;

        [Parameter(Position = 2, Mandatory = false)]
        public L2normalizationMode Mode { get; set; } = L2normalizationMode.Instance;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.L2Normalization(Data, Eps, Mode, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMakeLoss2Symbol")]
    [Alias("mx.sym.MakeLoss2")]
    [OutputType(typeof(Symbol))]
    public class GetMxMakeLoss2Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float GradScale { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public float ValidThresh { get; set; } = 0f;

        [Parameter(Position = 3, Mandatory = false)]
        public MakelossNormalization Normalization { get; set; } = MakelossNormalization.Null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.MakeLoss(Data, GradScale, ValidThresh, Normalization, SymbolName));
        }
    }

    [Cmdlet("Get", "MxPoolingV1Symbol")]
    [Alias("mx.sym.PoolingV1")]
    [OutputType(typeof(Symbol))]
    public class GetMxPoolingV1Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

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

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.PoolingV1(Data, Kernel, PoolType, GlobalPool, PoolingConvention, Stride, Pad, SymbolName));
        }
    }

    [Cmdlet("Get", "MxROIPoolingSymbol")]
    [Alias("mx.sym.ROIPooling")]
    [OutputType(typeof(Symbol))]
    public class GetMxROIPoolingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rois { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape PooledSize { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float SpatialScale { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.ROIPooling(Data, Rois, PooledSize, SpatialScale, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSequenceLastSymbol")]
    [Alias("mx.sym.SequenceLast")]
    [OutputType(typeof(Symbol))]
    public class GetMxSequenceLastSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol SequenceLength { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool UseSequenceLength { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public int Axis { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SequenceLast(Data, SequenceLength, UseSequenceLength, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSequenceMaskSymbol")]
    [Alias("mx.sym.SequenceMask")]
    [OutputType(typeof(Symbol))]
    public class GetMxSequenceMaskSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol SequenceLength { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool UseSequenceLength { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public float Value { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public int Axis { get; set; } = 0;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SequenceMask(Data, SequenceLength, UseSequenceLength, Value, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSequenceReverseSymbol")]
    [Alias("mx.sym.SequenceReverse")]
    [OutputType(typeof(Symbol))]
    public class GetMxSequenceReverseSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol SequenceLength { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool UseSequenceLength { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public int Axis { get; set; } = 0;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SequenceReverse(Data, SequenceLength, UseSequenceLength, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSpatialTransformerSymbol")]
    [Alias("mx.sym.SpatialTransformer")]
    [OutputType(typeof(Symbol))]
    public class GetMxSpatialTransformerSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Loc { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public SpatialtransformerTransformType TransformType { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public SpatialtransformerSamplerType SamplerType { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public Shape TargetShape { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public bool? CudnnOff { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SpatialTransformer(Data, Loc, TransformType, SamplerType, TargetShape, CudnnOff, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSVMOutputSymbol")]
    [Alias("mx.sym.SVMOutput")]
    [OutputType(typeof(Symbol))]
    public class GetMxSVMOutputSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Label { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public float Margin { get; set; } = 1f;

        [Parameter(Position = 3, Mandatory = false)]
        public float RegularizationCoefficient { get; set; } = 1f;

        [Parameter(Position = 4, Mandatory = false)]
        public bool UseLinear { get; set; } = false;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.SVMOutput(Data, Label, Margin, RegularizationCoefficient, UseLinear, SymbolName));
        }
    }

    [Cmdlet("Get", "MxOnehotEncodeSymbol")]
    [Alias("mx.sym.OnehotEncode")]
    [OutputType(typeof(Symbol))]
    public class GetMxOnehotEncodeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.OnehotEncode(Lhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxFillElement0IndexSymbol")]
    [Alias("mx.sym.FillElement0Index")]
    [OutputType(typeof(Symbol))]
    public class GetMxFillElement0IndexSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Mhs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.FillElement0Index(Lhs, Mhs, Rhs, SymbolName));
        }
    }

    [Cmdlet("Get", "MxImdecodeSymbol")]
    [Alias("mx.sym.Imdecode")]
    [OutputType(typeof(Symbol))]
    public class GetMxImdecodeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Mean { get; set; }

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

        [Parameter(Position = 8, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Imdecode(Mean, Index, X0, Y0, X1, Y1, C, Size, SymbolName));
        }
    }

    [Cmdlet("Get", "MxLinspaceSymbol")]
    [Alias("mx.sym.Linspace")]
    [OutputType(typeof(Symbol))]
    public class GetMxLinspaceSymbol : PSCmdlet
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

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.Linspace(Start, Stop, Num, Endpoint, Ctx, Dtype, SymbolName));
        }
    }

    [Cmdlet("Get", "MxStopGradientSymbol")]
    [Alias("mx.sym.StopGradient")]
    [OutputType(typeof(Symbol))]
    public class GetMxStopGradientSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.sym.StopGradient(Data, SymbolName));
        }
    }
}
