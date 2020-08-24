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
    [Cmdlet("Get", "MxAdaptiveAvgPooling2DNDArray")]
    [Alias("mx.nd.contrib.AdaptiveAvgPooling2D")]
    [OutputType(typeof(NDArray))]
    public class GetMxAdaptiveAvgPooling2DNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Shape OutputSize { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().AdaptiveAvgPooling2D(Data, OutputSize));
        }
    }

    [Cmdlet("Get", "MxBilinearResize2DNDArray")]
    [Alias("mx.nd.contrib.BilinearResize2D")]
    [OutputType(typeof(NDArray))]
    public class GetMxBilinearResize2DNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int Height { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public int Width { get; set; } = 1;

        [Parameter(Position = 3, Mandatory = false)]
        public float? ScaleHeight { get; set; } = null;

        [Parameter(Position = 4, Mandatory = false)]
        public float? ScaleWidth { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().BilinearResize2D(Data, Height, Width, ScaleHeight, ScaleWidth));
        }
    }

    [Cmdlet("Get", "MxBooleanMaskNDArray")]
    [Alias("mx.nd.contrib.BooleanMask")]
    [OutputType(typeof(NDArray))]
    public class GetMxBooleanMaskNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Index { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Axis { get; set; } = 0;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().BooleanMask(Data, Index, Axis));
        }
    }

    [Cmdlet("Get", "MxBoxNmsNDArray")]
    [Alias("mx.nd.contrib.BoxNms")]
    [OutputType(typeof(NDArray))]
    public class GetMxBoxNmsNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().BoxNms(Data, OverlapThresh, ValidThresh, Topk, CoordStart, ScoreIndex, IdIndex, BackgroundId, ForceSuppress, InFormat, OutFormat));
        }
    }

    [Cmdlet("Get", "MxBoxNonMaximumSupressionNDArray")]
    [Alias("mx.nd.contrib.BoxNonMaximumSupression")]
    [OutputType(typeof(NDArray))]
    public class GetMxBoxNonMaximumSupressionNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().BoxNonMaximumSupression(Data, OverlapThresh, ValidThresh, Topk, CoordStart, ScoreIndex, IdIndex, BackgroundId, ForceSuppress, InFormat, OutFormat));
        }
    }

    [Cmdlet("Get", "MxBoxIouNDArray")]
    [Alias("mx.nd.contrib.BoxIou")]
    [OutputType(typeof(NDArray))]
    public class GetMxBoxIouNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Lhs { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rhs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public ContribBoxIouFormat Format { get; set; } = ContribBoxIouFormat.Corner;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().BoxIou(Lhs, Rhs, Format));
        }
    }

    [Cmdlet("Get", "MxBipartiteMatchingNDArray")]
    [Alias("mx.nd.contrib.BipartiteMatching")]
    [OutputType(typeof(NDArray))]
    public class GetMxBipartiteMatchingNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Threshold { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public bool IsAscend { get; set; } = false;

        [Parameter(Position = 3, Mandatory = false)]
        public int Topk { get; set; } = -1;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().BipartiteMatching(Data, Threshold, IsAscend, Topk));
        }
    }

    [Cmdlet("Get", "MxDglCsrNeighborUniformSampleNDArray")]
    [Alias("mx.nd.contrib.DglCsrNeighborUniformSample")]
    [OutputType(typeof(NDArray))]
    public class GetMxDglCsrNeighborUniformSampleNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray CsrMatrix { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArrayList SeedArrays { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public int NumHops { get; set; } = 1;

        [Parameter(Position = 4, Mandatory = false)]
        public int NumNeighbor { get; set; } = 2;

        [Parameter(Position = 5, Mandatory = false)]
        public int MaxNumVertices { get; set; } = 100;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().DglCsrNeighborUniformSample(CsrMatrix, SeedArrays, NumArgs, NumHops, NumNeighbor, MaxNumVertices));
        }
    }

    [Cmdlet("Get", "MxDglCsrNeighborNonUniformSampleNDArray")]
    [Alias("mx.nd.contrib.DglCsrNeighborNonUniformSample")]
    [OutputType(typeof(NDArray))]
    public class GetMxDglCsrNeighborNonUniformSampleNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray CsrMatrix { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Probability { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArrayList SeedArrays { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public int NumHops { get; set; } = 1;

        [Parameter(Position = 5, Mandatory = false)]
        public int NumNeighbor { get; set; } = 2;

        [Parameter(Position = 6, Mandatory = false)]
        public int MaxNumVertices { get; set; } = 100;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().DglCsrNeighborNonUniformSample(CsrMatrix, Probability, SeedArrays, NumArgs, NumHops, NumNeighbor, MaxNumVertices));
        }
    }

    [Cmdlet("Get", "MxDglSubgraphNDArray")]
    [Alias("mx.nd.contrib.DglSubgraph")]
    [OutputType(typeof(NDArray))]
    public class GetMxDglSubgraphNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Graph { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public bool ReturnMapping { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().DglSubgraph(Graph, Data, NumArgs, ReturnMapping));
        }
    }

    [Cmdlet("Get", "MxEdgeIdNDArray")]
    [Alias("mx.nd.contrib.EdgeId")]
    [OutputType(typeof(NDArray))]
    public class GetMxEdgeIdNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray U { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray V { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().EdgeId(Data, U, V));
        }
    }

    [Cmdlet("Get", "MxDglAdjacencyNDArray")]
    [Alias("mx.nd.contrib.DglAdjacency")]
    [OutputType(typeof(NDArray))]
    public class GetMxDglAdjacencyNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().DglAdjacency(Data));
        }
    }

    [Cmdlet("Get", "MxDglGraphCompactNDArray")]
    [Alias("mx.nd.contrib.DglGraphCompact")]
    [OutputType(typeof(NDArray))]
    public class GetMxDglGraphCompactNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList GraphData { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public bool ReturnMapping { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public Tuple<double> GraphSizes { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().DglGraphCompact(GraphData, NumArgs, ReturnMapping, GraphSizes));
        }
    }

    [Cmdlet("Get", "MxGradientmultiplierNDArray")]
    [Alias("mx.nd.contrib.Gradientmultiplier")]
    [OutputType(typeof(NDArray))]
    public class GetMxGradientmultiplierNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public float Scalar { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().Gradientmultiplier(Data, Scalar));
        }
    }

    [Cmdlet("Get", "MxIndexCopyNDArray")]
    [Alias("mx.nd.contrib.IndexCopy")]
    [OutputType(typeof(NDArray))]
    public class GetMxIndexCopyNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray OldTensor { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray IndexVector { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray NewTensor { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().IndexCopy(OldTensor, IndexVector, NewTensor));
        }
    }

    [Cmdlet("Get", "MxGetnnzNDArray")]
    [Alias("mx.nd.contrib.Getnnz")]
    [OutputType(typeof(NDArray))]
    public class GetMxGetnnzNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int? Axis { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().Getnnz(Data, Axis));
        }
    }

    [Cmdlet("Get", "MxGroupAdagradUpdateNDArray")]
    [Alias("mx.nd.contrib.GroupAdagradUpdate")]
    [OutputType(typeof(NDArray))]
    public class GetMxGroupAdagradUpdateNDArray : PSCmdlet
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
        public float RescaleGrad { get; set; } = 1f;

        [Parameter(Position = 5, Mandatory = false)]
        public float ClipGradient { get; set; } = -1f;

        [Parameter(Position = 6, Mandatory = false)]
        public float Epsilon { get; set; } = 1e-05f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().GroupAdagradUpdate(Weight, Grad, History, Lr, RescaleGrad, ClipGradient, Epsilon));
        }
    }

    [Cmdlet("Get", "MxQuadraticNDArray")]
    [Alias("mx.nd.contrib.Quadratic")]
    [OutputType(typeof(NDArray))]
    public class GetMxQuadraticNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float A { get; set; } = 0f;

        [Parameter(Position = 2, Mandatory = false)]
        public float B { get; set; } = 0f;

        [Parameter(Position = 3, Mandatory = false)]
        public float C { get; set; } = 0f;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().Quadratic(Data, A, B, C));
        }
    }

    [Cmdlet("Get", "MxROIAlignNDArray")]
    [Alias("mx.nd.contrib.ROIAlign")]
    [OutputType(typeof(NDArray))]
    public class GetMxROIAlignNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Rois { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public Shape PooledSize { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public float SpatialScale { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public int SampleRatio { get; set; } = -1;

        [Parameter(Position = 5, Mandatory = false)]
        public bool PositionSensitive { get; set; } = false;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().ROIAlign(Data, Rois, PooledSize, SpatialScale, SampleRatio, PositionSensitive));
        }
    }

    [Cmdlet("Get", "MxSyncBatchNormNDArray")]
    [Alias("mx.nd.contrib.SyncBatchNorm")]
    [OutputType(typeof(NDArray))]
    public class GetMxSyncBatchNormNDArray : PSCmdlet
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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().SyncBatchNorm(Data, Gamma, Beta, MovingMean, MovingVar, Key, Eps, Momentum, FixGamma, UseGlobalStats, OutputMeanVar, Ndev));
        }
    }

    [Cmdlet("Get", "MxDivSqrtDimNDArray")]
    [Alias("mx.nd.contrib.DivSqrtDim")]
    [OutputType(typeof(NDArray))]
    public class GetMxDivSqrtDimNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().DivSqrtDim(Data));
        }
    }

    [Cmdlet("Get", "MxDequantizeNDArray")]
    [Alias("mx.nd.contrib.Dequantize")]
    [OutputType(typeof(NDArray))]
    public class GetMxDequantizeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray MinRange { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray MaxRange { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public ContribDequantizeOutType OutType { get; set; } = ContribDequantizeOutType.Float32;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().Dequantize(Data, MinRange, MaxRange, OutType));
        }
    }

    [Cmdlet("Get", "MxQuantizeNDArray")]
    [Alias("mx.nd.contrib.Quantize")]
    [OutputType(typeof(NDArray))]
    public class GetMxQuantizeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray MinRange { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray MaxRange { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public ContribQuantizeOutType OutType { get; set; } = ContribQuantizeOutType.Uint8;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().Quantize(Data, MinRange, MaxRange, OutType));
        }
    }

    [Cmdlet("Get", "MxQuantizeV2NDArray")]
    [Alias("mx.nd.contrib.QuantizeV2")]
    [OutputType(typeof(NDArray))]
    public class GetMxQuantizeV2NDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public ContribQuantizeV2OutType OutType { get; set; } = ContribQuantizeV2OutType.Int8;

        [Parameter(Position = 2, Mandatory = false)]
        public float? MinCalibRange { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public float? MaxCalibRange { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().QuantizeV2(Data, OutType, MinCalibRange, MaxCalibRange));
        }
    }

    [Cmdlet("Get", "MxQuantizedActNDArray")]
    [Alias("mx.nd.contrib.QuantizedAct")]
    [OutputType(typeof(NDArray))]
    public class GetMxQuantizedActNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray MinData { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray MaxData { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public ContribQuantizedActActType ActType { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().QuantizedAct(Data, MinData, MaxData, ActType));
        }
    }

    [Cmdlet("Get", "MxQuantizedConcatNDArray")]
    [Alias("mx.nd.contrib.QuantizedConcat")]
    [OutputType(typeof(NDArray))]
    public class GetMxQuantizedConcatNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArrayList Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public int NumArgs { get; set; }

        [Parameter(Position = 2, Mandatory = false)]
        public int Dim { get; set; } = 1;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().QuantizedConcat(Data, NumArgs, Dim));
        }
    }

    [Cmdlet("Get", "MxQuantizedConvNDArray")]
    [Alias("mx.nd.contrib.QuantizedConv")]
    [OutputType(typeof(NDArray))]
    public class GetMxQuantizedConvNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Bias { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray MinData { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public NDArray MaxData { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public NDArray MinWeight { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public NDArray MaxWeight { get; set; }

        [Parameter(Position = 7, Mandatory = true)]
        public NDArray MinBias { get; set; }

        [Parameter(Position = 8, Mandatory = true)]
        public NDArray MaxBias { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().QuantizedConv(Data, Weight, Bias, MinData, MaxData, MinWeight, MaxWeight, MinBias, MaxBias, Kernel, NumFilter, Stride, Dilate, Pad, NumGroup, Workspace, NoBias, CudnnTune, CudnnOff, Layout));
        }
    }

    [Cmdlet("Get", "MxQuantizedFlattenNDArray")]
    [Alias("mx.nd.contrib.QuantizedFlatten")]
    [OutputType(typeof(NDArray))]
    public class GetMxQuantizedFlattenNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray MinData { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray MaxData { get; set; }

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().QuantizedFlatten(Data, MinData, MaxData));
        }
    }

    [Cmdlet("Get", "MxQuantizedFullyConnectedNDArray")]
    [Alias("mx.nd.contrib.QuantizedFullyConnected")]
    [OutputType(typeof(NDArray))]
    public class GetMxQuantizedFullyConnectedNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Bias { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray MinData { get; set; }

        [Parameter(Position = 4, Mandatory = true)]
        public NDArray MaxData { get; set; }

        [Parameter(Position = 5, Mandatory = true)]
        public NDArray MinWeight { get; set; }

        [Parameter(Position = 6, Mandatory = true)]
        public NDArray MaxWeight { get; set; }

        [Parameter(Position = 7, Mandatory = true)]
        public NDArray MinBias { get; set; }

        [Parameter(Position = 8, Mandatory = true)]
        public NDArray MaxBias { get; set; }

        [Parameter(Position = 9, Mandatory = true)]
        public int NumHidden { get; set; }

        [Parameter(Position = 10, Mandatory = false)]
        public bool NoBias { get; set; } = false;

        [Parameter(Position = 11, Mandatory = false)]
        public bool Flatten { get; set; } = true;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().QuantizedFullyConnected(Data, Weight, Bias, MinData, MaxData, MinWeight, MaxWeight, MinBias, MaxBias, NumHidden, NoBias, Flatten));
        }
    }

    [Cmdlet("Get", "MxQuantizedPoolingNDArray")]
    [Alias("mx.nd.contrib.QuantizedPooling")]
    [OutputType(typeof(NDArray))]
    public class GetMxQuantizedPoolingNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray MinData { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray MaxData { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().QuantizedPooling(Data, MinData, MaxData, Kernel, PoolType, GlobalPool, CudnnOff, PoolingConvention, Stride, Pad, PValue, CountIncludePad, Layout));
        }
    }

    [Cmdlet("Get", "MxRequantizeNDArray")]
    [Alias("mx.nd.contrib.Requantize")]
    [OutputType(typeof(NDArray))]
    public class GetMxRequantizeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray MinRange { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray MaxRange { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public ContribRequantizeOutType OutType { get; set; } = ContribRequantizeOutType.Int8;

        [Parameter(Position = 4, Mandatory = false)]
        public float? MinCalibRange { get; set; } = null;

        [Parameter(Position = 5, Mandatory = false)]
        public float? MaxCalibRange { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().Requantize(Data, MinRange, MaxRange, OutType, MinCalibRange, MaxCalibRange));
        }
    }

    [Cmdlet("Get", "MxSparseEmbeddingNDArray")]
    [Alias("mx.nd.contrib.SparseEmbedding")]
    [OutputType(typeof(NDArray))]
    public class GetMxSparseEmbeddingNDArray : PSCmdlet
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
            WriteObject(new global::MxNet.NDContribApi().SparseEmbedding(Data, Weight, InputDim, OutputDim, Dtype, SparseGrad));
        }
    }

    [Cmdlet("Get", "MxCountSketchNDArray")]
    [Alias("mx.nd.contrib.CountSketch")]
    [OutputType(typeof(NDArray))]
    public class GetMxCountSketchNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray H { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray S { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public int OutDim { get; set; }

        [Parameter(Position = 4, Mandatory = false)]
        public int ProcessingBatchSize { get; set; } = 32;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().CountSketch(Data, H, S, OutDim, ProcessingBatchSize));
        }
    }

    [Cmdlet("Get", "MxDeformableConvolutionNDArray")]
    [Alias("mx.nd.contrib.DeformableConvolution")]
    [OutputType(typeof(NDArray))]
    public class GetMxDeformableConvolutionNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Offset { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Weight { get; set; }

        [Parameter(Position = 3, Mandatory = true)]
        public NDArray Bias { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().DeformableConvolution(Data, Offset, Weight, Bias, Kernel, NumFilter, Stride, Dilate, Pad, NumGroup, NumDeformableGroup, Workspace, NoBias, Layout));
        }
    }

    [Cmdlet("Get", "MxDeformablePSROIPoolingNDArray")]
    [Alias("mx.nd.contrib.DeformablePSROIPooling")]
    [OutputType(typeof(NDArray))]
    public class GetMxDeformablePSROIPoolingNDArray : PSCmdlet
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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().DeformablePSROIPooling(Data, Rois, Trans, SpatialScale, OutputDim, GroupSize, PooledSize, PartSize, SamplePerPart, TransStd, NoTrans));
        }
    }

    [Cmdlet("Get", "MxFftNDArray")]
    [Alias("mx.nd.contrib.Fft")]
    [OutputType(typeof(NDArray))]
    public class GetMxFftNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int ComputeSize { get; set; } = 128;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().Fft(Data, ComputeSize));
        }
    }

    [Cmdlet("Get", "MxIfftNDArray")]
    [Alias("mx.nd.contrib.Ifft")]
    [OutputType(typeof(NDArray))]
    public class GetMxIfftNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int ComputeSize { get; set; } = 128;

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().Ifft(Data, ComputeSize));
        }
    }

    [Cmdlet("Get", "MxMultiProposalNDArray")]
    [Alias("mx.nd.contrib.MultiProposal")]
    [OutputType(typeof(NDArray))]
    public class GetMxMultiProposalNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray ClsProb { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray BboxPred { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray ImInfo { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().MultiProposal(ClsProb, BboxPred, ImInfo, RpnPreNmsTopN, RpnPostNmsTopN, Threshold, RpnMinSize, Scales, Ratios, FeatureStride, OutputScore, IouLoss));
        }
    }

    [Cmdlet("Get", "MxMultiBoxDetectionNDArray")]
    [Alias("mx.nd.contrib.MultiBoxDetection")]
    [OutputType(typeof(NDArray))]
    public class GetMxMultiBoxDetectionNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray ClsProb { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray LocPred { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray Anchor { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().MultiBoxDetection(ClsProb, LocPred, Anchor, Clip, Threshold, BackgroundId, NmsThreshold, ForceSuppress, Variances, NmsTopk));
        }
    }

    [Cmdlet("Get", "MxMultiBoxPriorNDArray")]
    [Alias("mx.nd.contrib.MultiBoxPrior")]
    [OutputType(typeof(NDArray))]
    public class GetMxMultiBoxPriorNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().MultiBoxPrior(Data, Sizes, Ratios, Clip, Steps, Offsets));
        }
    }

    [Cmdlet("Get", "MxMultiBoxTargetNDArray")]
    [Alias("mx.nd.contrib.MultiBoxTarget")]
    [OutputType(typeof(NDArray))]
    public class GetMxMultiBoxTargetNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Anchor { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray Label { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray ClsPred { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().MultiBoxTarget(Anchor, Label, ClsPred, OverlapThreshold, IgnoreLabel, NegativeMiningRatio, NegativeMiningThresh, MinimumNegativeSamples, Variances));
        }
    }

    [Cmdlet("Get", "MxProposalNDArray")]
    [Alias("mx.nd.contrib.Proposal")]
    [OutputType(typeof(NDArray))]
    public class GetMxProposalNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray ClsProb { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public NDArray BboxPred { get; set; }

        [Parameter(Position = 2, Mandatory = true)]
        public NDArray ImInfo { get; set; }

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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().Proposal(ClsProb, BboxPred, ImInfo, RpnPreNmsTopN, RpnPostNmsTopN, Threshold, RpnMinSize, Scales, Ratios, FeatureStride, OutputScore, IouLoss));
        }
    }

    [Cmdlet("Get", "MxPSROIPoolingNDArray")]
    [Alias("mx.nd.contrib.PSROIPooling")]
    [OutputType(typeof(NDArray))]
    public class GetMxPSROIPoolingNDArray : PSCmdlet
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

        protected override void BeginProcessing()
        {
            WriteObject(new global::MxNet.NDContribApi().PSROIPooling(Data, Rois, SpatialScale, OutputDim, PooledSize, GroupSize));
        }
    }
}
