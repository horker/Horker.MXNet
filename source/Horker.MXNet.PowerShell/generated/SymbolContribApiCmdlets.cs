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
    [Cmdlet("Get", "MxAdaptiveAvgPooling2DSymbol")]
    [Alias("mx.sym.contrib.AdaptiveAvgPooling2D")]
    [OutputType(typeof(Symbol))]
    public class GetMxAdaptiveAvgPooling2DSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape OutputSize { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().AdaptiveAvgPooling2D(Data, OutputSize, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBilinearResize2DSymbol")]
    [Alias("mx.sym.contrib.BilinearResize2D")]
    [OutputType(typeof(Symbol))]
    public class GetMxBilinearResize2DSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Height { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public int Width { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public float? ScaleHeight { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public float? ScaleWidth { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().BilinearResize2D(Data, Height, Width, ScaleHeight, ScaleWidth, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBooleanMaskSymbol")]
    [Alias("mx.sym.contrib.BooleanMask")]
    [OutputType(typeof(Symbol))]
    public class GetMxBooleanMaskSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Index { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 0;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().BooleanMask(Data, Index, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBoxNmsSymbol")]
    [Alias("mx.sym.contrib.BoxNms")]
    [OutputType(typeof(Symbol))]
    public class GetMxBoxNmsSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float OverlapThresh { get; set; } = 0.5f;

        [Parameter(Position = 2, Mandatory = false)]
        public float ValidThresh { get; set; } = 0f;

        [Parameter(Position = 3, Mandatory = false)]
        public int Topk { get; set; } = -1;

        [Parameter(Position = 4, Mandatory = false)]
        public int CoordStart { get; set; } = 2;

        [Parameter(Position = 5, Mandatory = false)]
        public int ScoreIndex { get; set; } = 1;

        [Parameter(Position = 6, Mandatory = false)]
        public int IdIndex { get; set; } = -1;

        [Parameter(Position = 7, Mandatory = false)]
        public int BackgroundId { get; set; } = -1;

        [Parameter(Position = 8, Mandatory = false)]
        public bool ForceSuppress { get; set; } = false;

        [Parameter(Position = 9, Mandatory = false)]
        public ContribBoxNmsInFormat InFormat { get; set; } = ContribBoxNmsInFormat.Corner;

        [Parameter(Position = 10, Mandatory = false)]
        public ContribBoxNmsOutFormat OutFormat { get; set; } = ContribBoxNmsOutFormat.Corner;

        [Parameter(Position = 11, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().BoxNms(Data, OverlapThresh, ValidThresh, Topk, CoordStart, ScoreIndex, IdIndex, BackgroundId, ForceSuppress, InFormat, OutFormat, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBoxIouSymbol")]
    [Alias("mx.sym.contrib.BoxIou")]
    [OutputType(typeof(Symbol))]
    public class GetMxBoxIouSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public ContribBoxIouFormat Format { get; set; } = ContribBoxIouFormat.Corner;

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().BoxIou(Lhs, Rhs, Format, SymbolName));
        }
    }

    [Cmdlet("Get", "MxBipartiteMatchingSymbol")]
    [Alias("mx.sym.contrib.BipartiteMatching")]
    [OutputType(typeof(Symbol))]
    public class GetMxBipartiteMatchingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Threshold { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool IsAscend { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public int Topk { get; set; } = -1;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().BipartiteMatching(Data, Threshold, IsAscend, Topk, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDglCsrNeighborUniformSampleSymbol")]
    [Alias("mx.sym.contrib.DglCsrNeighborUniformSample")]
    [OutputType(typeof(Symbol))]
    public class GetMxDglCsrNeighborUniformSampleSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol CsrMatrix { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public SymbolList SeedArrays { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public int NumHops { get; set; } = 1;

        [Parameter(Position = 4, Mandatory = false)]
        public int NumNeighbor { get; set; } = 2;

        [Parameter(Position = 5, Mandatory = false)]
        public int MaxNumVertices { get; set; } = 100;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().DglCsrNeighborUniformSample(CsrMatrix, SeedArrays, NumArgs, NumHops, NumNeighbor, MaxNumVertices, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDglCsrNeighborNonUniformSampleSymbol")]
    [Alias("mx.sym.contrib.DglCsrNeighborNonUniformSample")]
    [OutputType(typeof(Symbol))]
    public class GetMxDglCsrNeighborNonUniformSampleSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol CsrMatrix { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Probability { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public SymbolList SeedArrays { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public int NumHops { get; set; } = 1;

        [Parameter(Position = 5, Mandatory = false)]
        public int NumNeighbor { get; set; } = 2;

        [Parameter(Position = 6, Mandatory = false)]
        public int MaxNumVertices { get; set; } = 100;

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().DglCsrNeighborNonUniformSample(CsrMatrix, Probability, SeedArrays, NumArgs, NumHops, NumNeighbor, MaxNumVertices, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDglSubgraphSymbol")]
    [Alias("mx.sym.contrib.DglSubgraph")]
    [OutputType(typeof(Symbol))]
    public class GetMxDglSubgraphSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Graph { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public SymbolList Data { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public bool ReturnMapping { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().DglSubgraph(Graph, Data, NumArgs, ReturnMapping, SymbolName));
        }
    }

    [Cmdlet("Get", "MxEdgeIdSymbol")]
    [Alias("mx.sym.contrib.EdgeId")]
    [OutputType(typeof(Symbol))]
    public class GetMxEdgeIdSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol U { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol V { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().EdgeId(Data, U, V, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDglAdjacencySymbol")]
    [Alias("mx.sym.contrib.DglAdjacency")]
    [OutputType(typeof(Symbol))]
    public class GetMxDglAdjacencySymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().DglAdjacency(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDglGraphCompactSymbol")]
    [Alias("mx.sym.contrib.DglGraphCompact")]
    [OutputType(typeof(Symbol))]
    public class GetMxDglGraphCompactSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SymbolList GraphData { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public bool ReturnMapping { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Tuple<double> GraphSizes { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().DglGraphCompact(GraphData, NumArgs, ReturnMapping, GraphSizes, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGradientmultiplierSymbol")]
    [Alias("mx.sym.contrib.Gradientmultiplier")]
    [OutputType(typeof(Symbol))]
    public class GetMxGradientmultiplierSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().Gradientmultiplier(Data, Scalar, SymbolName));
        }
    }

    [Cmdlet("Get", "MxIndexCopySymbol")]
    [Alias("mx.sym.contrib.IndexCopy")]
    [OutputType(typeof(Symbol))]
    public class GetMxIndexCopySymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol OldTensor { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol IndexVector { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol NewTensor { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().IndexCopy(OldTensor, IndexVector, NewTensor, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGetnnzSymbol")]
    [Alias("mx.sym.contrib.Getnnz")]
    [OutputType(typeof(Symbol))]
    public class GetMxGetnnzSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().Getnnz(Data, Axis, SymbolName));
        }
    }

    [Cmdlet("Get", "MxGroupAdagradUpdateSymbol")]
    [Alias("mx.sym.contrib.GroupAdagradUpdate")]
    [OutputType(typeof(Symbol))]
    public class GetMxGroupAdagradUpdateSymbol : PSCmdlet
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
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 6, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-05f;

        [Parameter(Position = 7, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().GroupAdagradUpdate(Weight, Grad, History, Lr, RescaleGrad, ClipGradient, Epsilon, SymbolName));
        }
    }

    [Cmdlet("Get", "MxQuadraticSymbol")]
    [Alias("mx.sym.contrib.Quadratic")]
    [OutputType(typeof(Symbol))]
    public class GetMxQuadraticSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float A { get; set; } = 0f;

        [Parameter(Position = 2, Mandatory = false)]
        public float B { get; set; } = 0f;

        [Parameter(Position = 3, Mandatory = false)]
        public float C { get; set; } = 0f;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().Quadratic(Data, A, B, C, SymbolName));
        }
    }

    [Cmdlet("Get", "MxROIAlignSymbol")]
    [Alias("mx.sym.contrib.ROIAlign")]
    [OutputType(typeof(Symbol))]
    public class GetMxROIAlignSymbol : PSCmdlet
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
        public int SampleRatio { get; set; } = -1;

        [Parameter(Position = 5, Mandatory = false)]
        public bool PositionSensitive { get; set; } = false;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().ROIAlign(Data, Rois, PooledSize, SpatialScale, SampleRatio, PositionSensitive, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSyncBatchNormSymbol")]
    [Alias("mx.sym.contrib.SyncBatchNorm")]
    [OutputType(typeof(Symbol))]
    public class GetMxSyncBatchNormSymbol : PSCmdlet
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

        [Parameter(Position = 5, Mandatory = true)]
        public string Key { get; set; }

        [Parameter(Position = 6, Mandatory = false)]
        public float Eps { get; set; } = 0.001f;

        [Parameter(Position = 7, Mandatory = false)]
        public float Momentum { get; set; } = 0.9f;

        [Parameter(Position = 8, Mandatory = false)]
        public bool FixGamma { get; set; } = true;

        [Parameter(Position = 9, Mandatory = false)]
        public bool UseGlobalStats { get; set; } = false;

        [Parameter(Position = 10, Mandatory = false)]
        public bool OutputMeanVar { get; set; } = false;

        [Parameter(Position = 11, Mandatory = false)]
        public int Ndev { get; set; } = 1;

        [Parameter(Position = 12, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().SyncBatchNorm(Data, Gamma, Beta, MovingMean, MovingVar, Key, Eps, Momentum, FixGamma, UseGlobalStats, OutputMeanVar, Ndev, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDivSqrtDimSymbol")]
    [Alias("mx.sym.contrib.DivSqrtDim")]
    [OutputType(typeof(Symbol))]
    public class GetMxDivSqrtDimSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().DivSqrtDim(Data, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDequantizeSymbol")]
    [Alias("mx.sym.contrib.Dequantize")]
    [OutputType(typeof(Symbol))]
    public class GetMxDequantizeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol MinRange { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol MaxRange { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public ContribDequantizeOutType OutType { get; set; } = ContribDequantizeOutType.Float32;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().Dequantize(Data, MinRange, MaxRange, OutType, SymbolName));
        }
    }

    [Cmdlet("Get", "MxQuantizeSymbol")]
    [Alias("mx.sym.contrib.Quantize")]
    [OutputType(typeof(Symbol))]
    public class GetMxQuantizeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol MinRange { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol MaxRange { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public ContribQuantizeOutType OutType { get; set; } = ContribQuantizeOutType.Uint8;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().Quantize(Data, MinRange, MaxRange, OutType, SymbolName));
        }
    }

    [Cmdlet("Get", "MxQuantizeV2Symbol")]
    [Alias("mx.sym.contrib.QuantizeV2")]
    [OutputType(typeof(Symbol))]
    public class GetMxQuantizeV2Symbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public ContribQuantizeV2OutType OutType { get; set; } = ContribQuantizeV2OutType.Int8;

        [Parameter(Position = 2, Mandatory = false)]
        public float? MinCalibRange { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public float? MaxCalibRange { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().QuantizeV2(Data, OutType, MinCalibRange, MaxCalibRange, SymbolName));
        }
    }

    [Cmdlet("Get", "MxQuantizedActSymbol")]
    [Alias("mx.sym.contrib.QuantizedAct")]
    [OutputType(typeof(Symbol))]
    public class GetMxQuantizedActSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol MinData { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol MaxData { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public ContribQuantizedActActType ActType { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().QuantizedAct(Data, MinData, MaxData, ActType, SymbolName));
        }
    }

    [Cmdlet("Get", "MxQuantizedConcatSymbol")]
    [Alias("mx.sym.contrib.QuantizedConcat")]
    [OutputType(typeof(Symbol))]
    public class GetMxQuantizedConcatSymbol : PSCmdlet
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
            WriteObject(new global::MxNet.SymContribApi().QuantizedConcat(Data, NumArgs, Dim, SymbolName));
        }
    }

    [Cmdlet("Get", "MxQuantizedConvSymbol")]
    [Alias("mx.sym.contrib.QuantizedConv")]
    [OutputType(typeof(Symbol))]
    public class GetMxQuantizedConvSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Bias { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol MinData { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public Symbol MaxData { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public Symbol MinWeight { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public Symbol MaxWeight { get; set; }

        [Parameter(Position = 7, Mandatory = true)]
        public Symbol MinBias { get; set; }

        [Parameter(Position = 8, Mandatory = true)]
        public Symbol MaxBias { get; set; }

        [Parameter(Position = 9, Mandatory = true)]
        public Shape Kernel { get; set; }

        [Parameter(Position = 10, Mandatory = true)]
        public uint NumFilter { get; set; }

        [Parameter(Position = 11, Mandatory = false)]
        public Shape Stride { get; set; } = null;

        [Parameter(Position = 12, Mandatory = false)]
        public Shape Dilate { get; set; } = null;

        [Parameter(Position = 13, Mandatory = false)]
        public Shape Pad { get; set; } = null;

        [Parameter(Position = 14, Mandatory = false)]
        public uint NumGroup { get; set; } = 1;

        [Parameter(Position = 15, Mandatory = false)]
        public ulong Workspace { get; set; } = 1024;

        [Parameter(Position = 16, Mandatory = false)]
        public bool NoBias { get; set; } = false;

        [Parameter(Position = 17, Mandatory = false)]
        public ContribQuantizedConvCudnnTune? CudnnTune { get; set; } = null;

        [Parameter(Position = 18, Mandatory = false)]
        public bool CudnnOff { get; set; } = false;

        [Parameter(Position = 19, Mandatory = false)]
        public ContribQuantizedConvLayout? Layout { get; set; } = null;

        [Parameter(Position = 20, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().QuantizedConv(Data, Weight, Bias, MinData, MaxData, MinWeight, MaxWeight, MinBias, MaxBias, Kernel, NumFilter, Stride, Dilate, Pad, NumGroup, Workspace, NoBias, CudnnTune, CudnnOff, Layout, SymbolName));
        }
    }

    [Cmdlet("Get", "MxQuantizedFlattenSymbol")]
    [Alias("mx.sym.contrib.QuantizedFlatten")]
    [OutputType(typeof(Symbol))]
    public class GetMxQuantizedFlattenSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol MinData { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol MaxData { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().QuantizedFlatten(Data, MinData, MaxData, SymbolName));
        }
    }

    [Cmdlet("Get", "MxQuantizedFullyConnectedSymbol")]
    [Alias("mx.sym.contrib.QuantizedFullyConnected")]
    [OutputType(typeof(Symbol))]
    public class GetMxQuantizedFullyConnectedSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Bias { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol MinData { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public Symbol MaxData { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public Symbol MinWeight { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public Symbol MaxWeight { get; set; }

        [Parameter(Position = 7, Mandatory = true)]
        public Symbol MinBias { get; set; }

        [Parameter(Position = 8, Mandatory = true)]
        public Symbol MaxBias { get; set; }

        [Parameter(Position = 9, Mandatory = true)]
        public int NumHidden { get; set; }

        [Parameter(Position = 10, Mandatory = false)]
        public bool NoBias { get; set; } = false;

        [Parameter(Position = 11, Mandatory = false)]
        public bool Flatten { get; set; } = true;

        [Parameter(Position = 12, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().QuantizedFullyConnected(Data, Weight, Bias, MinData, MaxData, MinWeight, MaxWeight, MinBias, MaxBias, NumHidden, NoBias, Flatten, SymbolName));
        }
    }

    [Cmdlet("Get", "MxQuantizedPoolingSymbol")]
    [Alias("mx.sym.contrib.QuantizedPooling")]
    [OutputType(typeof(Symbol))]
    public class GetMxQuantizedPoolingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol MinData { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol MaxData { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public Shape Kernel { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public ContribQuantizedPoolingPoolType PoolType { get; set; } = ContribQuantizedPoolingPoolType.Max;

        [Parameter(Position = 5, Mandatory = false)]
        public bool GlobalPool { get; set; } = false;

        [Parameter(Position = 6, Mandatory = false)]
        public bool CudnnOff { get; set; } = false;

        [Parameter(Position = 7, Mandatory = false)]
        public ContribQuantizedPoolingPoolingConvention PoolingConvention { get; set; } = ContribQuantizedPoolingPoolingConvention.Valid;

        [Parameter(Position = 8, Mandatory = false)]
        public Shape Stride { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public Shape Pad { get; set; } = null;

        [Parameter(Position = 10, Mandatory = false)]
        public int? PValue { get; set; } = null;

        [Parameter(Position = 11, Mandatory = false)]
        public bool? CountIncludePad { get; set; } = null;

        [Parameter(Position = 12, Mandatory = false)]
        public ContribQuantizedPoolingLayout? Layout { get; set; } = null;

        [Parameter(Position = 13, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().QuantizedPooling(Data, MinData, MaxData, Kernel, PoolType, GlobalPool, CudnnOff, PoolingConvention, Stride, Pad, PValue, CountIncludePad, Layout, SymbolName));
        }
    }

    [Cmdlet("Get", "MxRequantizeSymbol")]
    [Alias("mx.sym.contrib.Requantize")]
    [OutputType(typeof(Symbol))]
    public class GetMxRequantizeSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol MinRange { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol MaxRange { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public ContribRequantizeOutType OutType { get; set; } = ContribRequantizeOutType.Int8;

        [Parameter(Position = 4, Mandatory = false)]
        public float? MinCalibRange { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public float? MaxCalibRange { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().Requantize(Data, MinRange, MaxRange, OutType, MinCalibRange, MaxCalibRange, SymbolName));
        }
    }

    [Cmdlet("Get", "MxSparseEmbeddingSymbol")]
    [Alias("mx.sym.contrib.SparseEmbedding")]
    [OutputType(typeof(Symbol))]
    public class GetMxSparseEmbeddingSymbol : PSCmdlet
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
            WriteObject(new global::MxNet.SymContribApi().SparseEmbedding(Data, Weight, InputDim, OutputDim, Dtype, SparseGrad, SymbolName));
        }
    }

    [Cmdlet("Get", "MxCountSketchSymbol")]
    [Alias("mx.sym.contrib.CountSketch")]
    [OutputType(typeof(Symbol))]
    public class GetMxCountSketchSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol H { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol S { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int OutDim { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public int ProcessingBatchSize { get; set; } = 32;

        [Parameter(Position = 5, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().CountSketch(Data, H, S, OutDim, ProcessingBatchSize, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDeformableConvolutionSymbol")]
    [Alias("mx.sym.contrib.DeformableConvolution")]
    [OutputType(typeof(Symbol))]
    public class GetMxDeformableConvolutionSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Offset { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Weight { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Symbol Bias { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public Shape Kernel { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public uint NumFilter { get; set; }

        [Parameter(Position = 6, Mandatory = false)]
        public Shape Stride { get; set; } = null;

        [Parameter(Position = 7, Mandatory = false)]
        public Shape Dilate { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public Shape Pad { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public uint NumGroup { get; set; } = 1;

        [Parameter(Position = 10, Mandatory = false)]
        public uint NumDeformableGroup { get; set; } = 1;

        [Parameter(Position = 11, Mandatory = false)]
        public ulong Workspace { get; set; } = 1024;

        [Parameter(Position = 12, Mandatory = false)]
        public bool NoBias { get; set; } = false;

        [Parameter(Position = 13, Mandatory = false)]
        public ContribDeformableconvolutionLayout? Layout { get; set; } = null;

        [Parameter(Position = 14, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().DeformableConvolution(Data, Offset, Weight, Bias, Kernel, NumFilter, Stride, Dilate, Pad, NumGroup, NumDeformableGroup, Workspace, NoBias, Layout, SymbolName));
        }
    }

    [Cmdlet("Get", "MxDeformablePSROIPoolingSymbol")]
    [Alias("mx.sym.contrib.DeformablePSROIPooling")]
    [OutputType(typeof(Symbol))]
    public class GetMxDeformablePSROIPoolingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rois { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Trans { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float SpatialScale { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int OutputDim { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public int GroupSize { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public int PooledSize { get; set; }

        [Parameter(Position = 7, Mandatory = false)]
        public int PartSize { get; set; } = 0;

        [Parameter(Position = 8, Mandatory = false)]
        public int SamplePerPart { get; set; } = 1;

        [Parameter(Position = 9, Mandatory = false)]
        public float TransStd { get; set; } = 0f;

        [Parameter(Position = 10, Mandatory = false)]
        public bool NoTrans { get; set; } = false;

        [Parameter(Position = 11, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().DeformablePSROIPooling(Data, Rois, Trans, SpatialScale, OutputDim, GroupSize, PooledSize, PartSize, SamplePerPart, TransStd, NoTrans, SymbolName));
        }
    }

    [Cmdlet("Get", "MxFftSymbol")]
    [Alias("mx.sym.contrib.Fft")]
    [OutputType(typeof(Symbol))]
    public class GetMxFftSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int ComputeSize { get; set; } = 128;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().Fft(Data, ComputeSize, SymbolName));
        }
    }

    [Cmdlet("Get", "MxIfftSymbol")]
    [Alias("mx.sym.contrib.Ifft")]
    [OutputType(typeof(Symbol))]
    public class GetMxIfftSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int ComputeSize { get; set; } = 128;

        [Parameter(Position = 2, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().Ifft(Data, ComputeSize, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMultiProposalSymbol")]
    [Alias("mx.sym.contrib.MultiProposal")]
    [OutputType(typeof(Symbol))]
    public class GetMxMultiProposalSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol ClsProb { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol BboxPred { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol ImInfo { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public int RpnPreNmsTopN { get; set; } = 6000;

        [Parameter(Position = 4, Mandatory = false)]
        public int RpnPostNmsTopN { get; set; } = 300;

        [Parameter(Position = 5, Mandatory = false)]
        public float Threshold { get; set; } = 0.7f;

        [Parameter(Position = 6, Mandatory = false)]
        public int RpnMinSize { get; set; } = 16;

        [Parameter(Position = 7, Mandatory = false)]
        public Tuple<double> Scales { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public Tuple<double> Ratios { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public int FeatureStride { get; set; } = 16;

        [Parameter(Position = 10, Mandatory = false)]
        public bool OutputScore { get; set; } = false;

        [Parameter(Position = 11, Mandatory = false)]
        public bool IouLoss { get; set; } = false;

        [Parameter(Position = 12, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().MultiProposal(ClsProb, BboxPred, ImInfo, RpnPreNmsTopN, RpnPostNmsTopN, Threshold, RpnMinSize, Scales, Ratios, FeatureStride, OutputScore, IouLoss, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMultiBoxDetectionSymbol")]
    [Alias("mx.sym.contrib.MultiBoxDetection")]
    [OutputType(typeof(Symbol))]
    public class GetMxMultiBoxDetectionSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol ClsProb { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol LocPred { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol Anchor { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public bool Clip { get; set; } = true;

        [Parameter(Position = 4, Mandatory = false)]
        public float Threshold { get; set; } = 0.01f;

        [Parameter(Position = 5, Mandatory = false)]
        public int BackgroundId { get; set; } = 0;

        [Parameter(Position = 6, Mandatory = false)]
        public float NmsThreshold { get; set; } = 0.5f;

        [Parameter(Position = 7, Mandatory = false)]
        public bool ForceSuppress { get; set; } = false;

        [Parameter(Position = 8, Mandatory = false)]
        public Tuple<double> Variances { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public int NmsTopk { get; set; } = -1;

        [Parameter(Position = 10, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().MultiBoxDetection(ClsProb, LocPred, Anchor, Clip, Threshold, BackgroundId, NmsThreshold, ForceSuppress, Variances, NmsTopk, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMultiBoxPriorSymbol")]
    [Alias("mx.sym.contrib.MultiBoxPrior")]
    [OutputType(typeof(Symbol))]
    public class GetMxMultiBoxPriorSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Tuple<double> Sizes { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Tuple<double> Ratios { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public bool Clip { get; set; } = false;

        [Parameter(Position = 4, Mandatory = false)]
        public Tuple<double> Steps { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public Tuple<double> Offsets { get; set; } = null;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().MultiBoxPrior(Data, Sizes, Ratios, Clip, Steps, Offsets, SymbolName));
        }
    }

    [Cmdlet("Get", "MxMultiBoxTargetSymbol")]
    [Alias("mx.sym.contrib.MultiBoxTarget")]
    [OutputType(typeof(Symbol))]
    public class GetMxMultiBoxTargetSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Anchor { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Label { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol ClsPred { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public float OverlapThreshold { get; set; } = 0.5f;

        [Parameter(Position = 4, Mandatory = false)]
        public float IgnoreLabel { get; set; } = -1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float NegativeMiningRatio { get; set; } = -1f;

        [Parameter(Position = 6, Mandatory = false)]
        public float NegativeMiningThresh { get; set; } = 0.5f;

        [Parameter(Position = 7, Mandatory = false)]
        public int MinimumNegativeSamples { get; set; } = 0;

        [Parameter(Position = 8, Mandatory = false)]
        public Tuple<double> Variances { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().MultiBoxTarget(Anchor, Label, ClsPred, OverlapThreshold, IgnoreLabel, NegativeMiningRatio, NegativeMiningThresh, MinimumNegativeSamples, Variances, SymbolName));
        }
    }

    [Cmdlet("Get", "MxProposalSymbol")]
    [Alias("mx.sym.contrib.Proposal")]
    [OutputType(typeof(Symbol))]
    public class GetMxProposalSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol ClsProb { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol BboxPred { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Symbol ImInfo { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public int RpnPreNmsTopN { get; set; } = 6000;

        [Parameter(Position = 4, Mandatory = false)]
        public int RpnPostNmsTopN { get; set; } = 300;

        [Parameter(Position = 5, Mandatory = false)]
        public float Threshold { get; set; } = 0.7f;

        [Parameter(Position = 6, Mandatory = false)]
        public int RpnMinSize { get; set; } = 16;

        [Parameter(Position = 7, Mandatory = false)]
        public Tuple<double> Scales { get; set; } = null;

        [Parameter(Position = 8, Mandatory = false)]
        public Tuple<double> Ratios { get; set; } = null;

        [Parameter(Position = 9, Mandatory = false)]
        public int FeatureStride { get; set; } = 16;

        [Parameter(Position = 10, Mandatory = false)]
        public bool OutputScore { get; set; } = false;

        [Parameter(Position = 11, Mandatory = false)]
        public bool IouLoss { get; set; } = false;

        [Parameter(Position = 12, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().Proposal(ClsProb, BboxPred, ImInfo, RpnPreNmsTopN, RpnPostNmsTopN, Threshold, RpnMinSize, Scales, Ratios, FeatureStride, OutputScore, IouLoss, SymbolName));
        }
    }

    [Cmdlet("Get", "MxPSROIPoolingSymbol")]
    [Alias("mx.sym.contrib.PSROIPooling")]
    [OutputType(typeof(Symbol))]
    public class GetMxPSROIPoolingSymbol : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Symbol Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Symbol Rois { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public float SpatialScale { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int OutputDim { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public int PooledSize { get; set; }

        [Parameter(Position = 5, Mandatory = false)]
        public int GroupSize { get; set; } = 0;

        [Parameter(Position = 6, Mandatory = false)]
        public string SymbolName { get; set; } = "";

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.SymContribApi().PSROIPooling(Data, Rois, SpatialScale, OutputDim, PooledSize, GroupSize, SymbolName));
        }
    }
}
