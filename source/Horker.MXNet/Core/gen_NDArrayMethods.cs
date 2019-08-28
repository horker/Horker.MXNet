using System;
using System.Collections.Generic;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.MXNet.Core
{
    public partial class NDArray : NDArrayOrSymbol
    {

        /// doc
        public NDArray BatchNormV1(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, float eps = 0.00100000005f, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false)
        {
            return Op.BatchNormV1(this, gamma, beta, eps, momentum, fixGamma, useGlobalStats, outputMeanVar);
        }

        /// doc
        public NDArray MpAdamwUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol weight32, NDArrayOrSymbol rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f)
        {
            return Op.MpAdamwUpdate(this, grad, mean, var, weight32, rescaleGrad, lr, eta, beta1, beta2, epsilon, wd, clipGradient);
        }

        /// doc
        public NDArray AdamwUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f)
        {
            return Op.AdamwUpdate(this, grad, mean, var, rescaleGrad, lr, eta, beta1, beta2, epsilon, wd, clipGradient);
        }

        /// doc
        public NDArray IdentityAttachKLSparseReg(float sparsenessTarget = 0.100000001f, float penalty = 0.00100000005f, float momentum = 0.899999976f)
        {
            return Op.IdentityAttachKLSparseReg(this, sparsenessTarget, penalty, momentum);
        }

        /// doc
        public NDArray LeakyReLU(NDArrayOrSymbol gamma, LeakyreluActType actType = LeakyreluActType.Leaky, float slope = 0.25f, float lowerBound = 0.125f, float upperBound = 0.333999991f)
        {
            return Op.LeakyReLU(this, gamma, actType, slope, lowerBound, upperBound);
        }

        /// doc
        public NDArray SoftmaxCrossEntropy(NDArrayOrSymbol label)
        {
            return Op.SoftmaxCrossEntropy(this, label);
        }

        /// doc
        public NDArray Activation(ActivationActType actType)
        {
            return Op.Activation(this, actType);
        }

        /// doc
        public NDArray BatchNorm(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false)
        {
            return Op.BatchNorm(this, gamma, beta, movingMean, movingVar, eps, momentum, fixGamma, useGlobalStats, outputMeanVar, axis, cudnnOff);
        }

        /// doc
        public NDArray Convolution(NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionLayout? layout = null)
        {
            return Op.Convolution(this, weight, bias, kernel, numFilter, stride, dilate, pad, numGroup, workspace, noBias, cudnnTune, cudnnOff, layout);
        }

        /// doc
        public NDArray CTCLoss(NDArrayOrSymbol label, NDArrayOrSymbol dataLengths, NDArrayOrSymbol labelLengths, bool useDataLengths = false, bool useLabelLengths = false, CtclossBlankLabel blankLabel = CtclossBlankLabel.First)
        {
            return Op.CTCLoss(this, label, dataLengths, labelLengths, useDataLengths, useLabelLengths, blankLabel);
        }

        /// doc
        public NDArray CuDNNBatchNorm(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false)
        {
            return Op.CuDNNBatchNorm(this, gamma, beta, movingMean, movingVar, eps, momentum, fixGamma, useGlobalStats, outputMeanVar, axis, cudnnOff);
        }

        /// doc
        public NDArray Deconvolution(NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, NDShape adj = null, NDShape targetShape = null, uint numGroup = 1, ulong workspace = 512, bool noBias = true, DeconvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, DeconvolutionLayout? layout = null)
        {
            return Op.Deconvolution(this, weight, bias, kernel, numFilter, stride, dilate, pad, adj, targetShape, numGroup, workspace, noBias, cudnnTune, cudnnOff, layout);
        }

        /// doc
        public NDArray Dropout(float p = 0.5f, DropoutMode mode = DropoutMode.Training, NDShape axes = null, bool? cudnnOff = false)
        {
            return Op.Dropout(this, p, mode, axes, cudnnOff);
        }

        /// doc
        public NDArray FullyConnected(NDArrayOrSymbol weight, NDArrayOrSymbol bias, int numHidden, bool noBias = false, bool flatten = true)
        {
            return Op.FullyConnected(this, weight, bias, numHidden, noBias, flatten);
        }

        /// doc
        public NDArray LayerNorm(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, int axis = -1, float eps = 9.99999975e-06f, bool outputMeanVar = false)
        {
            return Op.LayerNorm(this, gamma, beta, axis, eps, outputMeanVar);
        }

        /// doc
        public NDArray LRN(uint nsize, float alpha = 9.99999975e-05f, float beta = 0.75f, float knorm = 2f)
        {
            return Op.LRN(this, nsize, alpha, beta, knorm);
        }

        /// doc
        public NDArray Moments(NDShape axes = null, bool keepdims = false)
        {
            return Op.Moments(this, axes, keepdims);
        }

        /// doc
        public NDArray Pooling(NDShape kernel = null, PoolingPoolType poolType = PoolingPoolType.Max, bool globalPool = false, bool cudnnOff = false, PoolingPoolingConvention poolingConvention = PoolingPoolingConvention.Valid, NDShape stride = null, NDShape pad = null, int? pValue = null, bool? countIncludePad = null, PoolingLayout? layout = null)
        {
            return Op.Pooling(this, kernel, poolType, globalPool, cudnnOff, poolingConvention, stride, pad, pValue, countIncludePad, layout);
        }

        /// doc
        public NDArray Softmax(int axis = -1, double? temperature = null, DType dtype = null)
        {
            return Op.Softmax(this, axis, temperature, dtype);
        }

        /// doc
        public NDArray Softmin(int axis = -1, double? temperature = null, DType dtype = null)
        {
            return Op.Softmin(this, axis, temperature, dtype);
        }

        /// doc
        public NDArray LogSoftmax(int axis = -1, double? temperature = null, DType dtype = null)
        {
            return Op.LogSoftmax(this, axis, temperature, dtype);
        }

        /// doc
        public NDArray SoftmaxActivation(SoftmaxactivationMode mode = SoftmaxactivationMode.Instance)
        {
            return Op.SoftmaxActivation(this, mode);
        }

        /// doc
        public NDArray SignsgdUpdate(NDArrayOrSymbol grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            return Op.SignsgdUpdate(this, grad, lr, wd, rescaleGrad, clipGradient);
        }

        /// doc
        public NDArray SignumUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float wdLh = 0f)
        {
            return Op.SignumUpdate(this, grad, mom, lr, momentum, wd, rescaleGrad, clipGradient, wdLh);
        }

        /// doc
        public NDArray SgdUpdate(NDArrayOrSymbol grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            return Op.SgdUpdate(this, grad, lr, wd, rescaleGrad, clipGradient, lazyUpdate);
        }

        /// doc
        public NDArray SgdMomUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            return Op.SgdMomUpdate(this, grad, mom, lr, momentum, wd, rescaleGrad, clipGradient, lazyUpdate);
        }

        /// doc
        public NDArray MpSgdUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol weight32, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            return Op.MpSgdUpdate(this, grad, weight32, lr, wd, rescaleGrad, clipGradient, lazyUpdate);
        }

        /// doc
        public NDArray MpSgdMomUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            return Op.MpSgdMomUpdate(this, grad, mom, weight32, lr, momentum, wd, rescaleGrad, clipGradient, lazyUpdate);
        }

        /// doc
        public NDArray FtmlUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol d, NDArrayOrSymbol v, NDArrayOrSymbol z, float lr, int t, float beta1 = 0.600000024f, float beta2 = 0.999000013f, double epsilon = 9.9999999392252903e-09, float wd = 0f, float rescaleGrad = 1f, float clipGrad = -1f)
        {
            return Op.FtmlUpdate(this, grad, d, v, z, lr, t, beta1, beta2, epsilon, wd, rescaleGrad, clipGrad);
        }

        /// doc
        public NDArray AdamUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, float lr, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            return Op.AdamUpdate(this, grad, mean, var, lr, beta1, beta2, epsilon, wd, rescaleGrad, clipGradient, lazyUpdate);
        }

        /// doc
        public NDArray NagMomUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            return Op.NagMomUpdate(this, grad, mom, lr, momentum, wd, rescaleGrad, clipGradient);
        }

        /// doc
        public NDArray MpNagMomUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            return Op.MpNagMomUpdate(this, grad, mom, weight32, lr, momentum, wd, rescaleGrad, clipGradient);
        }

        /// doc
        public NDArray RmspropUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol n, float lr, float gamma1 = 0.949999988f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f)
        {
            return Op.RmspropUpdate(this, grad, n, lr, gamma1, epsilon, wd, rescaleGrad, clipGradient, clipWeights);
        }

        /// doc
        public NDArray RmspropalexUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol n, NDArrayOrSymbol g, NDArrayOrSymbol delta, float lr, float gamma1 = 0.949999988f, float gamma2 = 0.899999976f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f)
        {
            return Op.RmspropalexUpdate(this, grad, n, g, delta, lr, gamma1, gamma2, epsilon, wd, rescaleGrad, clipGradient, clipWeights);
        }

        /// doc
        public NDArray FtrlUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol z, NDArrayOrSymbol n, float lr, float lamda1 = 0.00999999978f, float beta = 1f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            return Op.FtrlUpdate(this, grad, z, n, lr, lamda1, beta, wd, rescaleGrad, clipGradient);
        }

        /// doc
        public NDArray SparseAdagradUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol history, float lr, float epsilon = 1.00000001e-07f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            return Op.SparseAdagradUpdate(this, grad, history, lr, epsilon, wd, rescaleGrad, clipGradient);
        }

        /// doc
        public NDArray Pad(PadMode mode, NDShape padWidth, double constantValue = 0)
        {
            return Op.Pad(this, mode, padWidth, constantValue);
        }

        /// doc
        public NDArray Flatten()
        {
            return Op.Flatten(this);
        }

        /// doc
        public NDArray SampleUniform(NDArrayOrSymbol high, NDShape shape = null, DType dtype = null)
        {
            return Op.SampleUniform(this, high, shape, dtype);
        }

        /// doc
        public NDArray SampleNormal(NDArrayOrSymbol sigma, NDShape shape = null, DType dtype = null)
        {
            return Op.SampleNormal(this, sigma, shape, dtype);
        }

        /// doc
        public NDArray SampleGamma(NDArrayOrSymbol beta, NDShape shape = null, DType dtype = null)
        {
            return Op.SampleGamma(this, beta, shape, dtype);
        }

        /// doc
        public NDArray SampleExponential(NDShape shape = null, DType dtype = null)
        {
            return Op.SampleExponential(this, shape, dtype);
        }

        /// doc
        public NDArray SamplePoisson(NDShape shape = null, DType dtype = null)
        {
            return Op.SamplePoisson(this, shape, dtype);
        }

        /// doc
        public NDArray SampleNegativeBinomial(NDArrayOrSymbol p, NDShape shape = null, DType dtype = null)
        {
            return Op.SampleNegativeBinomial(this, p, shape, dtype);
        }

        /// doc
        public NDArray SampleGeneralizedNegativeBinomial(NDArrayOrSymbol alpha, NDShape shape = null, DType dtype = null)
        {
            return Op.SampleGeneralizedNegativeBinomial(this, alpha, shape, dtype);
        }

        /// doc
        public NDArray SampleMultinomial(NDShape shape = null, bool getProb = false, DType dtype = null)
        {
            return Op.SampleMultinomial(this, shape, getProb, dtype);
        }

        /// doc
        public NDArray RandomUniformLike(float low = 0f, float high = 1f)
        {
            return Op.RandomUniformLike(this, low, high);
        }

        /// doc
        public NDArray RandomNormalLike(float loc = 0f, float scale = 1f)
        {
            return Op.RandomNormalLike(this, loc, scale);
        }

        /// doc
        public NDArray RandomGammaLike(float alpha = 1f, float beta = 1f)
        {
            return Op.RandomGammaLike(this, alpha, beta);
        }

        /// doc
        public NDArray RandomExponentialLike(float lam = 1f)
        {
            return Op.RandomExponentialLike(this, lam);
        }

        /// doc
        public NDArray RandomPoissonLike(float lam = 1f)
        {
            return Op.RandomPoissonLike(this, lam);
        }

        /// doc
        public NDArray RandomNegativeBinomialLike(int k = 1, float p = 1f)
        {
            return Op.RandomNegativeBinomialLike(this, k, p);
        }

        /// doc
        public NDArray RandomGeneralizedNegativeBinomialLike(float mu = 1f, float alpha = 1f)
        {
            return Op.RandomGeneralizedNegativeBinomialLike(this, mu, alpha);
        }

        /// doc
        public NDArray Shuffle()
        {
            return Op.Shuffle(this);
        }

        /// doc
        public NDArray LinearRegressionOutput(NDArrayOrSymbol label, float gradScale = 1f)
        {
            return Op.LinearRegressionOutput(this, label, gradScale);
        }

        /// doc
        public NDArray MAERegressionOutput(NDArrayOrSymbol label, float gradScale = 1f)
        {
            return Op.MAERegressionOutput(this, label, gradScale);
        }

        /// doc
        public NDArray LogisticRegressionOutput(NDArrayOrSymbol label, float gradScale = 1f)
        {
            return Op.LogisticRegressionOutput(this, label, gradScale);
        }

        /// doc
        public NDArray RNN(NDArrayOrSymbol parameters, NDArrayOrSymbol state, NDArrayOrSymbol stateCell, NDArrayOrSymbol sequenceLength, uint stateSize, uint numLayers, RNNMode mode, bool bidirectional = false, float p = 0f, bool stateOutputs = false, int? projectionSize = null, double? lstmStateClipMin = null, double? lstmStateClipMax = null, bool lstmStateClipNan = false, bool useSequenceLength = false)
        {
            return Op.RNN(this, parameters, state, stateCell, sequenceLength, stateSize, numLayers, mode, bidirectional, p, stateOutputs, projectionSize, lstmStateClipMin, lstmStateClipMax, lstmStateClipNan, useSequenceLength);
        }

        /// doc
        public NDArray SliceChannel(int numOutputs, int axis = 1, bool squeezeAxis = false)
        {
            return Op.SliceChannel(this, numOutputs, axis, squeezeAxis);
        }

        /// doc
        public NDArray SoftmaxOutput(NDArrayOrSymbol label, float gradScale = 1f, float ignoreLabel = -1f, bool multiOutput = false, bool useIgnore = false, bool preserveShape = false, SoftmaxoutputNormalization normalization = SoftmaxoutputNormalization.Null, bool outGrad = false, float smoothAlpha = 0f)
        {
            return Op.SoftmaxOutput(this, label, gradScale, ignoreLabel, multiOutput, useIgnore, preserveShape, normalization, outGrad, smoothAlpha);
        }

        /// doc
        public NDArray SwapAxis(uint dim1 = 0, uint dim2 = 0)
        {
            return Op.SwapAxis(this, dim1, dim2);
        }

        /// doc
        public NDArray AmpCast(DType dtype)
        {
            return Op.AmpCast(this, dtype);
        }

        /// doc
        public NDArray Argmax(int? axis = null, bool keepdims = false)
        {
            return Op.Argmax(this, axis, keepdims);
        }

        /// doc
        public NDArray Argmin(int? axis = null, bool keepdims = false)
        {
            return Op.Argmin(this, axis, keepdims);
        }

        /// doc
        public NDArray ArgmaxChannel()
        {
            return Op.ArgmaxChannel(this);
        }

        /// doc
        public NDArray Pick(NDArrayOrSymbol index, int? axis = -1, bool keepdims = false, PickMode mode = PickMode.Clip)
        {
            return Op.Pick(this, index, axis, keepdims, mode);
        }

        /// doc
        public NDArray Sum(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            return Op.Sum(this, axis, keepdims, exclude);
        }

        /// doc
        public NDArray Mean(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            return Op.Mean(this, axis, keepdims, exclude);
        }

        /// doc
        public NDArray Prod(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            return Op.Prod(this, axis, keepdims, exclude);
        }

        /// doc
        public NDArray Nansum(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            return Op.Nansum(this, axis, keepdims, exclude);
        }

        /// doc
        public NDArray Nanprod(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            return Op.Nanprod(this, axis, keepdims, exclude);
        }

        /// doc
        public NDArray Max(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            return Op.Max(this, axis, keepdims, exclude);
        }

        /// doc
        public NDArray Min(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            return Op.Min(this, axis, keepdims, exclude);
        }

        /// doc
        public NDArray BroadcastAxis(NDShape axis = null, NDShape size = null)
        {
            return Op.BroadcastAxis(this, axis, size);
        }

        /// doc
        public NDArray BroadcastTo(NDShape shape = null)
        {
            return Op.BroadcastTo(this, shape);
        }

        /// doc
        public NDArray BroadcastLike(NDArrayOrSymbol rhs, NDShape lhsAxes = null, NDShape rhsAxes = null)
        {
            return Op.BroadcastLike(this, rhs, lhsAxes, rhsAxes);
        }

        /// doc
        public NDArray Norm(int ord = 2, NDShape axis = null, NormOutDtype? outDtype = null, bool keepdims = false)
        {
            return Op.Norm(this, ord, axis, outDtype, keepdims);
        }

        /// doc
        public NDArray CastStorage(CastStorageStype stype)
        {
            return Op.CastStorage(this, stype);
        }

        /// doc
        public NDArray Where(NDArrayOrSymbol x, NDArrayOrSymbol y)
        {
            return Op.Where(this, x, y);
        }

        /// doc
        public NDArray Diag(int k = 0, int axis1 = 0, int axis2 = 1)
        {
            return Op.Diag(this, k, axis1, axis2);
        }

        /// doc
        public NDArray Dot(NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, DotForwardStype? forwardStype = null)
        {
            return Op.Dot(this, rhs, transposeA, transposeB, forwardStype);
        }

        /// doc
        public NDArray BatchDot(NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, BatchDotForwardStype? forwardStype = null)
        {
            return Op.BatchDot(this, rhs, transposeA, transposeB, forwardStype);
        }

        /// doc
        public NDArray BroadcastAdd(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastAdd(this, rhs);
        }

        /// doc
        public NDArray BroadcastSub(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastSub(this, rhs);
        }

        /// doc
        public NDArray BroadcastMul(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastMul(this, rhs);
        }

        /// doc
        public NDArray BroadcastDiv(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastDiv(this, rhs);
        }

        /// doc
        public NDArray BroadcastMod(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastMod(this, rhs);
        }

        /// doc
        public NDArray BroadcastPower(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastPower(this, rhs);
        }

        /// doc
        public NDArray BroadcastMaximum(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastMaximum(this, rhs);
        }

        /// doc
        public NDArray BroadcastMinimum(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastMinimum(this, rhs);
        }

        /// doc
        public NDArray BroadcastHypot(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastHypot(this, rhs);
        }

        /// doc
        public NDArray BroadcastEqual(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastEqual(this, rhs);
        }

        /// doc
        public NDArray BroadcastNotEqual(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastNotEqual(this, rhs);
        }

        /// doc
        public NDArray BroadcastGreater(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastGreater(this, rhs);
        }

        /// doc
        public NDArray BroadcastGreaterEqual(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastGreaterEqual(this, rhs);
        }

        /// doc
        public NDArray BroadcastLesser(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastLesser(this, rhs);
        }

        /// doc
        public NDArray BroadcastLesserEqual(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastLesserEqual(this, rhs);
        }

        /// doc
        public NDArray BroadcastLogicalAnd(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastLogicalAnd(this, rhs);
        }

        /// doc
        public NDArray BroadcastLogicalOr(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastLogicalOr(this, rhs);
        }

        /// doc
        public NDArray BroadcastLogicalXor(NDArrayOrSymbol rhs)
        {
            return Op.BroadcastLogicalXor(this, rhs);
        }

        /// doc
        public NDArray ElemwiseAdd(NDArrayOrSymbol rhs)
        {
            return Op.ElemwiseAdd(this, rhs);
        }

        /// doc
        public NDArray GradAdd(NDArrayOrSymbol rhs)
        {
            return Op.GradAdd(this, rhs);
        }

        /// doc
        public NDArray ElemwiseSub(NDArrayOrSymbol rhs)
        {
            return Op.ElemwiseSub(this, rhs);
        }

        /// doc
        public NDArray ElemwiseMul(NDArrayOrSymbol rhs)
        {
            return Op.ElemwiseMul(this, rhs);
        }

        /// doc
        public NDArray ElemwiseDiv(NDArrayOrSymbol rhs)
        {
            return Op.ElemwiseDiv(this, rhs);
        }

        /// doc
        public NDArray Mod(NDArrayOrSymbol rhs)
        {
            return Op.Mod(this, rhs);
        }

        /// doc
        public NDArray Power(NDArrayOrSymbol rhs)
        {
            return Op.Power(this, rhs);
        }

        /// doc
        public NDArray Maximum(NDArrayOrSymbol rhs)
        {
            return Op.Maximum(this, rhs);
        }

        /// doc
        public NDArray Minimum(NDArrayOrSymbol rhs)
        {
            return Op.Minimum(this, rhs);
        }

        /// doc
        public NDArray Hypot(NDArrayOrSymbol rhs)
        {
            return Op.Hypot(this, rhs);
        }

        /// doc
        public NDArray Equal(NDArrayOrSymbol rhs)
        {
            return Op.Equal(this, rhs);
        }

        /// doc
        public NDArray NotEqual(NDArrayOrSymbol rhs)
        {
            return Op.NotEqual(this, rhs);
        }

        /// doc
        public NDArray Greater(NDArrayOrSymbol rhs)
        {
            return Op.Greater(this, rhs);
        }

        /// doc
        public NDArray GreaterEqual(NDArrayOrSymbol rhs)
        {
            return Op.GreaterEqual(this, rhs);
        }

        /// doc
        public NDArray Lesser(NDArrayOrSymbol rhs)
        {
            return Op.Lesser(this, rhs);
        }

        /// doc
        public NDArray LesserEqual(NDArrayOrSymbol rhs)
        {
            return Op.LesserEqual(this, rhs);
        }

        /// doc
        public NDArray LogicalAnd(NDArrayOrSymbol rhs)
        {
            return Op.LogicalAnd(this, rhs);
        }

        /// doc
        public NDArray LogicalOr(NDArrayOrSymbol rhs)
        {
            return Op.LogicalOr(this, rhs);
        }

        /// doc
        public NDArray LogicalXor(NDArrayOrSymbol rhs)
        {
            return Op.LogicalXor(this, rhs);
        }

        /// doc
        public NDArray PlusScalar(float scalar)
        {
            return Op.PlusScalar(this, scalar);
        }

        /// doc
        public NDArray MinusScalar(float scalar)
        {
            return Op.MinusScalar(this, scalar);
        }

        /// doc
        public NDArray RminusScalar(float scalar)
        {
            return Op.RminusScalar(this, scalar);
        }

        /// doc
        public NDArray MulScalar(float scalar)
        {
            return Op.MulScalar(this, scalar);
        }

        /// doc
        public NDArray DivScalar(float scalar)
        {
            return Op.DivScalar(this, scalar);
        }

        /// doc
        public NDArray RdivScalar(float scalar)
        {
            return Op.RdivScalar(this, scalar);
        }

        /// doc
        public NDArray ModScalar(float scalar)
        {
            return Op.ModScalar(this, scalar);
        }

        /// doc
        public NDArray RmodScalar(float scalar)
        {
            return Op.RmodScalar(this, scalar);
        }

        /// doc
        public NDArray MaximumScalar(float scalar)
        {
            return Op.MaximumScalar(this, scalar);
        }

        /// doc
        public NDArray MinimumScalar(float scalar)
        {
            return Op.MinimumScalar(this, scalar);
        }

        /// doc
        public NDArray PowerScalar(float scalar)
        {
            return Op.PowerScalar(this, scalar);
        }

        /// doc
        public NDArray RpowerScalar(float scalar)
        {
            return Op.RpowerScalar(this, scalar);
        }

        /// doc
        public NDArray HypotScalar(float scalar)
        {
            return Op.HypotScalar(this, scalar);
        }

        /// doc
        public NDArray SmoothL1(float scalar)
        {
            return Op.SmoothL1(this, scalar);
        }

        /// doc
        public NDArray EqualScalar(float scalar)
        {
            return Op.EqualScalar(this, scalar);
        }

        /// doc
        public NDArray NotEqualScalar(float scalar)
        {
            return Op.NotEqualScalar(this, scalar);
        }

        /// doc
        public NDArray GreaterScalar(float scalar)
        {
            return Op.GreaterScalar(this, scalar);
        }

        /// doc
        public NDArray GreaterEqualScalar(float scalar)
        {
            return Op.GreaterEqualScalar(this, scalar);
        }

        /// doc
        public NDArray LesserScalar(float scalar)
        {
            return Op.LesserScalar(this, scalar);
        }

        /// doc
        public NDArray LesserEqualScalar(float scalar)
        {
            return Op.LesserEqualScalar(this, scalar);
        }

        /// doc
        public NDArray LogicalAndScalar(float scalar)
        {
            return Op.LogicalAndScalar(this, scalar);
        }

        /// doc
        public NDArray LogicalOrScalar(float scalar)
        {
            return Op.LogicalOrScalar(this, scalar);
        }

        /// doc
        public NDArray LogicalXorScalar(float scalar)
        {
            return Op.LogicalXorScalar(this, scalar);
        }

        /// doc
        public NDArray ScatterElemwiseDiv(NDArrayOrSymbol rhs)
        {
            return Op.ScatterElemwiseDiv(this, rhs);
        }

        /// doc
        public NDArray ScatterPlusScalar(float scalar)
        {
            return Op.ScatterPlusScalar(this, scalar);
        }

        /// doc
        public NDArray ScatterMinusScalar(float scalar)
        {
            return Op.ScatterMinusScalar(this, scalar);
        }

        /// doc
        public NDArray Relu()
        {
            return Op.Relu(this);
        }

        /// doc
        public NDArray Sigmoid()
        {
            return Op.Sigmoid(this);
        }

        /// doc
        public NDArray HardSigmoid(float alpha = 0.200000003f, float beta = 0.5f)
        {
            return Op.HardSigmoid(this, alpha, beta);
        }

        /// doc
        public NDArray Softsign()
        {
            return Op.Softsign(this);
        }

        /// doc
        public NDArray Copy()
        {
            return Op.Copy(this);
        }

        /// doc
        public NDArray BlockGrad()
        {
            return Op.BlockGrad(this);
        }

        /// doc
        public NDArray MakeLoss()
        {
            return Op.MakeLoss(this);
        }

        /// doc
        public NDArray IdentityWithAttrLikeRhs(NDArrayOrSymbol rhs)
        {
            return Op.IdentityWithAttrLikeRhs(this, rhs);
        }

        /// doc
        public NDArray ReshapeLike(NDArrayOrSymbol rhs)
        {
            return Op.ReshapeLike(this, rhs);
        }

        /// doc
        public NDArray ShapeArray(int? lhsBegin = null, int? lhsEnd = null, int? rhsBegin = null, int? rhsEnd = null)
        {
            return Op.ShapeArray(this, lhsBegin, lhsEnd, rhsBegin, rhsEnd);
        }

        /// doc
        public NDArray SizeArray()
        {
            return Op.SizeArray(this);
        }

        /// doc
        public NDArray Cast(DType dtype)
        {
            return Op.Cast(this, dtype);
        }

        /// doc
        public NDArray Negative()
        {
            return Op.Negative(this);
        }

        /// doc
        public NDArray Reciprocal()
        {
            return Op.Reciprocal(this);
        }

        /// doc
        public NDArray Abs()
        {
            return Op.Abs(this);
        }

        /// doc
        public NDArray Sign()
        {
            return Op.Sign(this);
        }

        /// doc
        public NDArray Round()
        {
            return Op.Round(this);
        }

        /// doc
        public NDArray Rint()
        {
            return Op.Rint(this);
        }

        /// doc
        public NDArray Ceil()
        {
            return Op.Ceil(this);
        }

        /// doc
        public NDArray Floor()
        {
            return Op.Floor(this);
        }

        /// doc
        public NDArray Trunc()
        {
            return Op.Trunc(this);
        }

        /// doc
        public NDArray Fix()
        {
            return Op.Fix(this);
        }

        /// doc
        public NDArray Square()
        {
            return Op.Square(this);
        }

        /// doc
        public NDArray Sqrt()
        {
            return Op.Sqrt(this);
        }

        /// doc
        public NDArray Rsqrt()
        {
            return Op.Rsqrt(this);
        }

        /// doc
        public NDArray Cbrt()
        {
            return Op.Cbrt(this);
        }

        /// doc
        public NDArray Erf()
        {
            return Op.Erf(this);
        }

        /// doc
        public NDArray Erfinv()
        {
            return Op.Erfinv(this);
        }

        /// doc
        public NDArray Rcbrt()
        {
            return Op.Rcbrt(this);
        }

        /// doc
        public NDArray Exp()
        {
            return Op.Exp(this);
        }

        /// doc
        public NDArray Log()
        {
            return Op.Log(this);
        }

        /// doc
        public NDArray Log10()
        {
            return Op.Log10(this);
        }

        /// doc
        public NDArray Log2()
        {
            return Op.Log2(this);
        }

        /// doc
        public NDArray Log1p()
        {
            return Op.Log1p(this);
        }

        /// doc
        public NDArray Expm1()
        {
            return Op.Expm1(this);
        }

        /// doc
        public NDArray Gamma()
        {
            return Op.Gamma(this);
        }

        /// doc
        public NDArray Gammaln()
        {
            return Op.Gammaln(this);
        }

        /// doc
        public NDArray LogicalNot()
        {
            return Op.LogicalNot(this);
        }

        /// doc
        public NDArray Sin()
        {
            return Op.Sin(this);
        }

        /// doc
        public NDArray Cos()
        {
            return Op.Cos(this);
        }

        /// doc
        public NDArray Tan()
        {
            return Op.Tan(this);
        }

        /// doc
        public NDArray Arcsin()
        {
            return Op.Arcsin(this);
        }

        /// doc
        public NDArray Arccos()
        {
            return Op.Arccos(this);
        }

        /// doc
        public NDArray Arctan()
        {
            return Op.Arctan(this);
        }

        /// doc
        public NDArray Degrees()
        {
            return Op.Degrees(this);
        }

        /// doc
        public NDArray Radians()
        {
            return Op.Radians(this);
        }

        /// doc
        public NDArray Sinh()
        {
            return Op.Sinh(this);
        }

        /// doc
        public NDArray Cosh()
        {
            return Op.Cosh(this);
        }

        /// doc
        public NDArray Tanh()
        {
            return Op.Tanh(this);
        }

        /// doc
        public NDArray Arcsinh()
        {
            return Op.Arcsinh(this);
        }

        /// doc
        public NDArray Arccosh()
        {
            return Op.Arccosh(this);
        }

        /// doc
        public NDArray Arctanh()
        {
            return Op.Arctanh(this);
        }

        /// doc
        public NDArray Histogram(NDArrayOrSymbol bins, int? binCnt = null, Tuple<double> range = null)
        {
            return Op.Histogram(this, bins, binCnt, range);
        }

        /// doc
        public NDArray Embedding(NDArrayOrSymbol weight, int inputDim, int outputDim, DType dtype = null, bool sparseGrad = false)
        {
            return Op.Embedding(this, weight, inputDim, outputDim, dtype, sparseGrad);
        }

        /// doc
        public NDArray Take(NDArrayOrSymbol indices, int axis = 0, TakeMode mode = TakeMode.Clip)
        {
            return Op.Take(this, indices, axis, mode);
        }

        /// doc
        public NDArray BatchTake(NDArrayOrSymbol indices)
        {
            return Op.BatchTake(this, indices);
        }

        /// doc
        public NDArray OneHot(int depth, double onValue = 1, double offValue = 0, DType dtype = null)
        {
            return Op.OneHot(this, depth, onValue, offValue, dtype);
        }

        /// doc
        public NDArray GatherNd(NDArrayOrSymbol indices)
        {
            return Op.GatherNd(this, indices);
        }

        /// doc
        public NDArray ScatterNd(NDArrayOrSymbol indices, NDShape shape)
        {
            return Op.ScatterNd(this, indices, shape);
        }

        /// doc
        public NDArray ScatterSetNd(NDArrayOrSymbol rhs, NDArrayOrSymbol indices, NDShape shape)
        {
            return Op.ScatterSetNd(this, rhs, indices, shape);
        }

        /// doc
        public NDArray ZerosLike()
        {
            return Op.ZerosLike(this);
        }

        /// doc
        public NDArray OnesLike()
        {
            return Op.OnesLike(this);
        }

        /// doc
        public NDArray LinalgGemm(NDArrayOrSymbol B, NDArrayOrSymbol C, bool transposeA = false, bool transposeB = false, double alpha = 1, double beta = 1, int axis = -2)
        {
            return Op.LinalgGemm(this, B, C, transposeA, transposeB, alpha, beta, axis);
        }

        /// doc
        public NDArray LinalgGemm2(NDArrayOrSymbol B, bool transposeA = false, bool transposeB = false, double alpha = 1, int axis = -2)
        {
            return Op.LinalgGemm2(this, B, transposeA, transposeB, alpha, axis);
        }

        /// doc
        public NDArray LinalgPotrf()
        {
            return Op.LinalgPotrf(this);
        }

        /// doc
        public NDArray LinalgPotri()
        {
            return Op.LinalgPotri(this);
        }

        /// doc
        public NDArray LinalgTrmm(NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1)
        {
            return Op.LinalgTrmm(this, B, transpose, rightside, lower, alpha);
        }

        /// doc
        public NDArray LinalgTrsm(NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1)
        {
            return Op.LinalgTrsm(this, B, transpose, rightside, lower, alpha);
        }

        /// doc
        public NDArray LinalgSumlogdiag()
        {
            return Op.LinalgSumlogdiag(this);
        }

        /// doc
        public NDArray LinalgExtractdiag(int offset = 0)
        {
            return Op.LinalgExtractdiag(this, offset);
        }

        /// doc
        public NDArray LinalgMakediag(int offset = 0)
        {
            return Op.LinalgMakediag(this, offset);
        }

        /// doc
        public NDArray LinalgExtracttrian(int offset = 0, bool lower = true)
        {
            return Op.LinalgExtracttrian(this, offset, lower);
        }

        /// doc
        public NDArray LinalgMaketrian(int offset = 0, bool lower = true)
        {
            return Op.LinalgMaketrian(this, offset, lower);
        }

        /// doc
        public NDArray LinalgSyrk(bool transpose = false, double alpha = 1)
        {
            return Op.LinalgSyrk(this, transpose, alpha);
        }

        /// doc
        public NDArray LinalgGelqf()
        {
            return Op.LinalgGelqf(this);
        }

        /// doc
        public NDArray LinalgSyevd()
        {
            return Op.LinalgSyevd(this);
        }

        /// doc
        public NDArray LinalgInverse()
        {
            return Op.LinalgInverse(this);
        }

        /// doc
        public NDArray Reshape(NDShape shape = null, bool reverse = false)
        {
            return Op.Reshape(this, shape, reverse);
        }

        /// doc
        public NDArray Transpose(NDShape axes = null)
        {
            return Op.Transpose(this, axes);
        }

        /// doc
        public NDArray ExpandDims(int axis)
        {
            return Op.ExpandDims(this, axis);
        }

        /// doc
        public NDArray Slice(NDShape begin, NDShape end, NDShape step = null)
        {
            return Op.Slice(this, begin, end, step);
        }

        /// doc
        public NDArray SliceAssign(NDArrayOrSymbol rhs, NDShape begin, NDShape end, NDShape step = null)
        {
            return Op.SliceAssign(this, rhs, begin, end, step);
        }

        /// doc
        public NDArray SliceAssignScalar(NDShape begin, NDShape end, double scalar = 0, NDShape step = null)
        {
            return Op.SliceAssignScalar(this, begin, end, scalar, step);
        }

        /// doc
        public NDArray SliceAxis(int axis, int begin, int? end)
        {
            return Op.SliceAxis(this, axis, begin, end);
        }

        /// doc
        public NDArray SliceLike(NDArrayOrSymbol shapeLike, NDShape axes = null)
        {
            return Op.SliceLike(this, shapeLike, axes);
        }

        /// doc
        public NDArray Clip(float aMin, float aMax)
        {
            return Op.Clip(this, aMin, aMax);
        }

        /// doc
        public NDArray Repeat(int repeats, int? axis = null)
        {
            return Op.Repeat(this, repeats, axis);
        }

        /// doc
        public NDArray Tile(NDShape reps)
        {
            return Op.Tile(this, reps);
        }

        /// doc
        public NDArray Reverse(NDShape axis)
        {
            return Op.Reverse(this, axis);
        }

        /// doc
        public NDArray DepthToSpace(int blockSize)
        {
            return Op.DepthToSpace(this, blockSize);
        }

        /// doc
        public NDArray SpaceToDepth(int blockSize)
        {
            return Op.SpaceToDepth(this, blockSize);
        }

        /// doc
        public NDArray SplitV2(NDShape indices, int axis = 1, bool squeezeAxis = false, int sections = 0)
        {
            return Op.SplitV2(this, indices, axis, squeezeAxis, sections);
        }

        /// doc
        public NDArray Topk(int? axis = -1, int k = 1, TopkRetTyp retTyp = TopkRetTyp.Indices, bool isAscend = false, DType dtype = null)
        {
            return Op.Topk(this, axis, k, retTyp, isAscend, dtype);
        }

        /// doc
        public NDArray Sort(int? axis = -1, bool isAscend = true)
        {
            return Op.Sort(this, axis, isAscend);
        }

        /// doc
        public NDArray Argsort(int? axis = -1, bool isAscend = true, DType dtype = null)
        {
            return Op.Argsort(this, axis, isAscend, dtype);
        }

        /// doc
        public NDArray RavelMultiIndex(NDShape shape = null)
        {
            return Op.RavelMultiIndex(this, shape);
        }

        /// doc
        public NDArray UnravelIndex(NDShape shape = null)
        {
            return Op.UnravelIndex(this, shape);
        }

        /// doc
        public NDArray SparseRetain(NDArrayOrSymbol indices)
        {
            return Op.SparseRetain(this, indices);
        }

        /// doc
        public NDArray SquareSum(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            return Op.SquareSum(this, axis, keepdims, exclude);
        }

        /// doc
        public NDArray BilinearSampler(NDArrayOrSymbol grid, bool? cudnnOff = null)
        {
            return Op.BilinearSampler(this, grid, cudnnOff);
        }

        /// doc
        public NDArray ConvolutionV1(NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionV1CudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionV1Layout? layout = null)
        {
            return Op.ConvolutionV1(this, weight, bias, kernel, numFilter, stride, dilate, pad, numGroup, workspace, noBias, cudnnTune, cudnnOff, layout);
        }

        /// doc
        public NDArray Correlation(NDArrayOrSymbol data2, uint kernelSize = 1, uint maxDisplacement = 1, uint stride1 = 1, uint stride2 = 1, uint padSize = 0, bool isMultiply = true)
        {
            return Op.Correlation(this, data2, kernelSize, maxDisplacement, stride1, stride2, padSize, isMultiply);
        }

        /// doc
        public NDArray GridGenerator(GridgeneratorTransformType transformType, NDShape targetShape = null)
        {
            return Op.GridGenerator(this, transformType, targetShape);
        }

        /// doc
        public NDArray InstanceNorm(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, float eps = 0.00100000005f)
        {
            return Op.InstanceNorm(this, gamma, beta, eps);
        }

        /// doc
        public NDArray L2Normalization(float eps = 1.00000001e-10f, L2normalizationMode mode = L2normalizationMode.Instance)
        {
            return Op.L2Normalization(this, eps, mode);
        }

        /// doc
        public NDArray MakeLoss(float gradScale = 1f, float validThresh = 0f, MakelossNormalization normalization = MakelossNormalization.Null)
        {
            return Op.MakeLoss(this, gradScale, validThresh, normalization);
        }

        /// doc
        public NDArray PoolingV1(NDShape kernel = null, PoolingV1PoolType poolType = PoolingV1PoolType.Max, bool globalPool = false, PoolingV1PoolingConvention poolingConvention = PoolingV1PoolingConvention.Valid, NDShape stride = null, NDShape pad = null)
        {
            return Op.PoolingV1(this, kernel, poolType, globalPool, poolingConvention, stride, pad);
        }

        /// doc
        public NDArray ROIPooling(NDArrayOrSymbol rois, NDShape pooledSize, float spatialScale)
        {
            return Op.ROIPooling(this, rois, pooledSize, spatialScale);
        }

        /// doc
        public NDArray SequenceLast(NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0)
        {
            return Op.SequenceLast(this, sequenceLength, useSequenceLength, axis);
        }

        /// doc
        public NDArray SequenceMask(NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, float value = 0f, int axis = 0)
        {
            return Op.SequenceMask(this, sequenceLength, useSequenceLength, value, axis);
        }

        /// doc
        public NDArray SequenceReverse(NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0)
        {
            return Op.SequenceReverse(this, sequenceLength, useSequenceLength, axis);
        }

        /// doc
        public NDArray SpatialTransformer(NDArrayOrSymbol loc, SpatialtransformerTransformType transformType, SpatialtransformerSamplerType samplerType, NDShape targetShape = null, bool? cudnnOff = null)
        {
            return Op.SpatialTransformer(this, loc, transformType, samplerType, targetShape, cudnnOff);
        }

        /// doc
        public NDArray SVMOutput(NDArrayOrSymbol label, float margin = 1f, float regularizationCoefficient = 1f, bool useLinear = false)
        {
            return Op.SVMOutput(this, label, margin, regularizationCoefficient, useLinear);
        }

        /// doc
        public NDArray Imdecode(int index, int x0, int y0, int x1, int y1, int c, int size)
        {
            return Op.Imdecode(this, index, x0, y0, x1, y1, c, size);
        }
    }
}
