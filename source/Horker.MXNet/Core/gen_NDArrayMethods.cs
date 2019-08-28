using System;
using System.Collections.Generic;
using Horker.MXNet.Operators;

namespace Horker.MXNet.Core
{
    public partial class NDArray : NDArrayOrSymbol
    {

        /// doc
        public NDArray BatchNormV1(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, float eps = 0.00100000005f, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, NDArray output = null)
        {
            return Op.BatchNormV1(this, gamma, beta, eps, momentum, fixGamma, useGlobalStats, outputMeanVar, output);
        }

        /// doc
        public NDArray MpAdamwUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol weight32, NDArrayOrSymbol rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f, NDArray output = null)
        {
            return Op.MpAdamwUpdate(this, grad, mean, var, weight32, rescaleGrad, lr, eta, beta1, beta2, epsilon, wd, clipGradient, output);
        }

        /// doc
        public NDArray AdamwUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f, NDArray output = null)
        {
            return Op.AdamwUpdate(this, grad, mean, var, rescaleGrad, lr, eta, beta1, beta2, epsilon, wd, clipGradient, output);
        }

        /// doc
        public NDArray IdentityAttachKLSparseReg(float sparsenessTarget = 0.100000001f, float penalty = 0.00100000005f, float momentum = 0.899999976f, NDArray output = null)
        {
            return Op.IdentityAttachKLSparseReg(this, sparsenessTarget, penalty, momentum, output);
        }

        /// doc
        public NDArray LeakyReLU(NDArrayOrSymbol gamma, LeakyreluActType actType = LeakyreluActType.Leaky, float slope = 0.25f, float lowerBound = 0.125f, float upperBound = 0.333999991f, NDArray output = null)
        {
            return Op.LeakyReLU(this, gamma, actType, slope, lowerBound, upperBound, output);
        }

        /// doc
        public NDArray SoftmaxCrossEntropy(NDArrayOrSymbol label, NDArray output = null)
        {
            return Op.SoftmaxCrossEntropy(this, label, output);
        }

        /// doc
        public NDArray Activation(ActivationActType actType, NDArray output = null)
        {
            return Op.Activation(this, actType, output);
        }

        /// doc
        public NDArray BatchNorm(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false, NDArray output = null)
        {
            return Op.BatchNorm(this, gamma, beta, movingMean, movingVar, eps, momentum, fixGamma, useGlobalStats, outputMeanVar, axis, cudnnOff, output);
        }

        /// doc
        public NDArray Convolution(NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionLayout? layout = null, NDArray output = null)
        {
            return Op.Convolution(this, weight, bias, kernel, numFilter, stride, dilate, pad, numGroup, workspace, noBias, cudnnTune, cudnnOff, layout, output);
        }

        /// doc
        public NDArray CTCLoss(NDArrayOrSymbol label, NDArrayOrSymbol dataLengths, NDArrayOrSymbol labelLengths, bool useDataLengths = false, bool useLabelLengths = false, CtclossBlankLabel blankLabel = CtclossBlankLabel.First, NDArray output = null)
        {
            return Op.CTCLoss(this, label, dataLengths, labelLengths, useDataLengths, useLabelLengths, blankLabel, output);
        }

        /// doc
        public NDArray CuDNNBatchNorm(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false, NDArray output = null)
        {
            return Op.CuDNNBatchNorm(this, gamma, beta, movingMean, movingVar, eps, momentum, fixGamma, useGlobalStats, outputMeanVar, axis, cudnnOff, output);
        }

        /// doc
        public NDArray Deconvolution(NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, NDShape adj = null, NDShape targetShape = null, uint numGroup = 1, ulong workspace = 512, bool noBias = true, DeconvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, DeconvolutionLayout? layout = null, NDArray output = null)
        {
            return Op.Deconvolution(this, weight, bias, kernel, numFilter, stride, dilate, pad, adj, targetShape, numGroup, workspace, noBias, cudnnTune, cudnnOff, layout, output);
        }

        /// doc
        public NDArray Dropout(float p = 0.5f, DropoutMode mode = DropoutMode.Training, NDShape axes = null, bool? cudnnOff = false, NDArray output = null)
        {
            return Op.Dropout(this, p, mode, axes, cudnnOff, output);
        }

        /// doc
        public NDArray FullyConnected(NDArrayOrSymbol weight, NDArrayOrSymbol bias, int numHidden, bool noBias = false, bool flatten = true, NDArray output = null)
        {
            return Op.FullyConnected(this, weight, bias, numHidden, noBias, flatten, output);
        }

        /// doc
        public NDArray LayerNorm(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, int axis = -1, float eps = 9.99999975e-06f, bool outputMeanVar = false, NDArray output = null)
        {
            return Op.LayerNorm(this, gamma, beta, axis, eps, outputMeanVar, output);
        }

        /// doc
        public NDArray LRN(uint nsize, float alpha = 9.99999975e-05f, float beta = 0.75f, float knorm = 2f, NDArray output = null)
        {
            return Op.LRN(this, nsize, alpha, beta, knorm, output);
        }

        /// doc
        public NDArray Moments(NDShape axes = null, bool keepdims = false, NDArray output = null)
        {
            return Op.Moments(this, axes, keepdims, output);
        }

        /// doc
        public NDArray Pooling(NDShape kernel = null, PoolingPoolType poolType = PoolingPoolType.Max, bool globalPool = false, bool cudnnOff = false, PoolingPoolingConvention poolingConvention = PoolingPoolingConvention.Valid, NDShape stride = null, NDShape pad = null, int? pValue = null, bool? countIncludePad = null, PoolingLayout? layout = null, NDArray output = null)
        {
            return Op.Pooling(this, kernel, poolType, globalPool, cudnnOff, poolingConvention, stride, pad, pValue, countIncludePad, layout, output);
        }

        /// doc
        public NDArray Softmax(int axis = -1, double? temperature = null, DType dtype = null, NDArray output = null)
        {
            return Op.Softmax(this, axis, temperature, dtype, output);
        }

        /// doc
        public NDArray Softmin(int axis = -1, double? temperature = null, DType dtype = null, NDArray output = null)
        {
            return Op.Softmin(this, axis, temperature, dtype, output);
        }

        /// doc
        public NDArray LogSoftmax(int axis = -1, double? temperature = null, DType dtype = null, NDArray output = null)
        {
            return Op.LogSoftmax(this, axis, temperature, dtype, output);
        }

        /// doc
        public NDArray SoftmaxActivation(SoftmaxactivationMode mode = SoftmaxactivationMode.Instance, NDArray output = null)
        {
            return Op.SoftmaxActivation(this, mode, output);
        }

        /// doc
        public NDArray SignsgdUpdate(NDArrayOrSymbol grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            return Op.SignsgdUpdate(this, grad, lr, wd, rescaleGrad, clipGradient, output);
        }

        /// doc
        public NDArray SignumUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float wdLh = 0f, NDArray output = null)
        {
            return Op.SignumUpdate(this, grad, mom, lr, momentum, wd, rescaleGrad, clipGradient, wdLh, output);
        }

        /// doc
        public NDArray SgdUpdate(NDArrayOrSymbol grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            return Op.SgdUpdate(this, grad, lr, wd, rescaleGrad, clipGradient, lazyUpdate, output);
        }

        /// doc
        public NDArray SgdMomUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            return Op.SgdMomUpdate(this, grad, mom, lr, momentum, wd, rescaleGrad, clipGradient, lazyUpdate, output);
        }

        /// doc
        public NDArray MpSgdUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol weight32, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            return Op.MpSgdUpdate(this, grad, weight32, lr, wd, rescaleGrad, clipGradient, lazyUpdate, output);
        }

        /// doc
        public NDArray MpSgdMomUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            return Op.MpSgdMomUpdate(this, grad, mom, weight32, lr, momentum, wd, rescaleGrad, clipGradient, lazyUpdate, output);
        }

        /// doc
        public NDArray FtmlUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol d, NDArrayOrSymbol v, NDArrayOrSymbol z, float lr, int t, float beta1 = 0.600000024f, float beta2 = 0.999000013f, double epsilon = 9.9999999392252903e-09, float wd = 0f, float rescaleGrad = 1f, float clipGrad = -1f, NDArray output = null)
        {
            return Op.FtmlUpdate(this, grad, d, v, z, lr, t, beta1, beta2, epsilon, wd, rescaleGrad, clipGrad, output);
        }

        /// doc
        public NDArray AdamUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, float lr, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            return Op.AdamUpdate(this, grad, mean, var, lr, beta1, beta2, epsilon, wd, rescaleGrad, clipGradient, lazyUpdate, output);
        }

        /// doc
        public NDArray NagMomUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            return Op.NagMomUpdate(this, grad, mom, lr, momentum, wd, rescaleGrad, clipGradient, output);
        }

        /// doc
        public NDArray MpNagMomUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            return Op.MpNagMomUpdate(this, grad, mom, weight32, lr, momentum, wd, rescaleGrad, clipGradient, output);
        }

        /// doc
        public NDArray RmspropUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol n, float lr, float gamma1 = 0.949999988f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f, NDArray output = null)
        {
            return Op.RmspropUpdate(this, grad, n, lr, gamma1, epsilon, wd, rescaleGrad, clipGradient, clipWeights, output);
        }

        /// doc
        public NDArray RmspropalexUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol n, NDArrayOrSymbol g, NDArrayOrSymbol delta, float lr, float gamma1 = 0.949999988f, float gamma2 = 0.899999976f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f, NDArray output = null)
        {
            return Op.RmspropalexUpdate(this, grad, n, g, delta, lr, gamma1, gamma2, epsilon, wd, rescaleGrad, clipGradient, clipWeights, output);
        }

        /// doc
        public NDArray FtrlUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol z, NDArrayOrSymbol n, float lr, float lamda1 = 0.00999999978f, float beta = 1f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            return Op.FtrlUpdate(this, grad, z, n, lr, lamda1, beta, wd, rescaleGrad, clipGradient, output);
        }

        /// doc
        public NDArray SparseAdagradUpdate(NDArrayOrSymbol grad, NDArrayOrSymbol history, float lr, float epsilon = 1.00000001e-07f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            return Op.SparseAdagradUpdate(this, grad, history, lr, epsilon, wd, rescaleGrad, clipGradient, output);
        }

        /// doc
        public NDArray Pad(PadMode mode, NDShape padWidth, double constantValue = 0, NDArray output = null)
        {
            return Op.Pad(this, mode, padWidth, constantValue, output);
        }

        /// doc
        public NDArray Flatten(NDArray output = null)
        {
            return Op.Flatten(this, output);
        }

        /// doc
        public NDArray SampleUniform(NDArrayOrSymbol high, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            return Op.SampleUniform(this, high, shape, dtype, output);
        }

        /// doc
        public NDArray SampleNormal(NDArrayOrSymbol sigma, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            return Op.SampleNormal(this, sigma, shape, dtype, output);
        }

        /// doc
        public NDArray SampleGamma(NDArrayOrSymbol beta, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            return Op.SampleGamma(this, beta, shape, dtype, output);
        }

        /// doc
        public NDArray SampleExponential(NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            return Op.SampleExponential(this, shape, dtype, output);
        }

        /// doc
        public NDArray SamplePoisson(NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            return Op.SamplePoisson(this, shape, dtype, output);
        }

        /// doc
        public NDArray SampleNegativeBinomial(NDArrayOrSymbol p, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            return Op.SampleNegativeBinomial(this, p, shape, dtype, output);
        }

        /// doc
        public NDArray SampleGeneralizedNegativeBinomial(NDArrayOrSymbol alpha, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            return Op.SampleGeneralizedNegativeBinomial(this, alpha, shape, dtype, output);
        }

        /// doc
        public NDArray SampleMultinomial(NDShape shape = null, bool getProb = false, DType dtype = null, NDArray output = null)
        {
            return Op.SampleMultinomial(this, shape, getProb, dtype, output);
        }

        /// doc
        public NDArray RandomUniformLike(float low = 0f, float high = 1f, NDArray output = null)
        {
            return Op.RandomUniformLike(this, low, high, output);
        }

        /// doc
        public NDArray RandomNormalLike(float loc = 0f, float scale = 1f, NDArray output = null)
        {
            return Op.RandomNormalLike(this, loc, scale, output);
        }

        /// doc
        public NDArray RandomGammaLike(float alpha = 1f, float beta = 1f, NDArray output = null)
        {
            return Op.RandomGammaLike(this, alpha, beta, output);
        }

        /// doc
        public NDArray RandomExponentialLike(float lam = 1f, NDArray output = null)
        {
            return Op.RandomExponentialLike(this, lam, output);
        }

        /// doc
        public NDArray RandomPoissonLike(float lam = 1f, NDArray output = null)
        {
            return Op.RandomPoissonLike(this, lam, output);
        }

        /// doc
        public NDArray RandomNegativeBinomialLike(int k = 1, float p = 1f, NDArray output = null)
        {
            return Op.RandomNegativeBinomialLike(this, k, p, output);
        }

        /// doc
        public NDArray RandomGeneralizedNegativeBinomialLike(float mu = 1f, float alpha = 1f, NDArray output = null)
        {
            return Op.RandomGeneralizedNegativeBinomialLike(this, mu, alpha, output);
        }

        /// doc
        public NDArray Shuffle(NDArray output = null)
        {
            return Op.Shuffle(this, output);
        }

        /// doc
        public NDArray LinearRegressionOutput(NDArrayOrSymbol label, float gradScale = 1f, NDArray output = null)
        {
            return Op.LinearRegressionOutput(this, label, gradScale, output);
        }

        /// doc
        public NDArray MAERegressionOutput(NDArrayOrSymbol label, float gradScale = 1f, NDArray output = null)
        {
            return Op.MAERegressionOutput(this, label, gradScale, output);
        }

        /// doc
        public NDArray LogisticRegressionOutput(NDArrayOrSymbol label, float gradScale = 1f, NDArray output = null)
        {
            return Op.LogisticRegressionOutput(this, label, gradScale, output);
        }

        /// doc
        public NDArray RNN(NDArrayOrSymbol parameters, NDArrayOrSymbol state, NDArrayOrSymbol stateCell, NDArrayOrSymbol sequenceLength, uint stateSize, uint numLayers, RNNMode mode, bool bidirectional = false, float p = 0f, bool stateOutputs = false, int? projectionSize = null, double? lstmStateClipMin = null, double? lstmStateClipMax = null, bool lstmStateClipNan = false, bool useSequenceLength = false, NDArray output = null)
        {
            return Op.RNN(this, parameters, state, stateCell, sequenceLength, stateSize, numLayers, mode, bidirectional, p, stateOutputs, projectionSize, lstmStateClipMin, lstmStateClipMax, lstmStateClipNan, useSequenceLength, output);
        }

        /// doc
        public NDArray SliceChannel(int numOutputs, int axis = 1, bool squeezeAxis = false, NDArray output = null)
        {
            return Op.SliceChannel(this, numOutputs, axis, squeezeAxis, output);
        }

        /// doc
        public NDArray SoftmaxOutput(NDArrayOrSymbol label, float gradScale = 1f, float ignoreLabel = -1f, bool multiOutput = false, bool useIgnore = false, bool preserveShape = false, SoftmaxoutputNormalization normalization = SoftmaxoutputNormalization.Null, bool outGrad = false, float smoothAlpha = 0f, NDArray output = null)
        {
            return Op.SoftmaxOutput(this, label, gradScale, ignoreLabel, multiOutput, useIgnore, preserveShape, normalization, outGrad, smoothAlpha, output);
        }

        /// doc
        public NDArray SwapAxis(uint dim1 = 0, uint dim2 = 0, NDArray output = null)
        {
            return Op.SwapAxis(this, dim1, dim2, output);
        }

        /// doc
        public NDArray AmpCast(DType dtype, NDArray output = null)
        {
            return Op.AmpCast(this, dtype, output);
        }

        /// doc
        public NDArray Argmax(int? axis = null, bool keepdims = false, NDArray output = null)
        {
            return Op.Argmax(this, axis, keepdims, output);
        }

        /// doc
        public NDArray Argmin(int? axis = null, bool keepdims = false, NDArray output = null)
        {
            return Op.Argmin(this, axis, keepdims, output);
        }

        /// doc
        public NDArray ArgmaxChannel(NDArray output = null)
        {
            return Op.ArgmaxChannel(this, output);
        }

        /// doc
        public NDArray Pick(NDArrayOrSymbol index, int? axis = -1, bool keepdims = false, PickMode mode = PickMode.Clip, NDArray output = null)
        {
            return Op.Pick(this, index, axis, keepdims, mode, output);
        }

        /// doc
        public NDArray Sum(NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            return Op.Sum(this, axis, keepdims, exclude, output);
        }

        /// doc
        public NDArray Mean(NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            return Op.Mean(this, axis, keepdims, exclude, output);
        }

        /// doc
        public NDArray Prod(NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            return Op.Prod(this, axis, keepdims, exclude, output);
        }

        /// doc
        public NDArray Nansum(NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            return Op.Nansum(this, axis, keepdims, exclude, output);
        }

        /// doc
        public NDArray Nanprod(NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            return Op.Nanprod(this, axis, keepdims, exclude, output);
        }

        /// doc
        public NDArray Max(NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            return Op.Max(this, axis, keepdims, exclude, output);
        }

        /// doc
        public NDArray Min(NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            return Op.Min(this, axis, keepdims, exclude, output);
        }

        /// doc
        public NDArray BroadcastAxis(NDShape axis = null, NDShape size = null, NDArray output = null)
        {
            return Op.BroadcastAxis(this, axis, size, output);
        }

        /// doc
        public NDArray BroadcastTo(NDShape shape = null, NDArray output = null)
        {
            return Op.BroadcastTo(this, shape, output);
        }

        /// doc
        public NDArray BroadcastLike(NDArrayOrSymbol rhs, NDShape lhsAxes = null, NDShape rhsAxes = null, NDArray output = null)
        {
            return Op.BroadcastLike(this, rhs, lhsAxes, rhsAxes, output);
        }

        /// doc
        public NDArray Norm(int ord = 2, NDShape axis = null, NormOutDtype? outDtype = null, bool keepdims = false, NDArray output = null)
        {
            return Op.Norm(this, ord, axis, outDtype, keepdims, output);
        }

        /// doc
        public NDArray CastStorage(CastStorageStype stype, NDArray output = null)
        {
            return Op.CastStorage(this, stype, output);
        }

        /// doc
        public NDArray Where(NDArrayOrSymbol x, NDArrayOrSymbol y, NDArray output = null)
        {
            return Op.Where(this, x, y, output);
        }

        /// doc
        public NDArray Diag(int k = 0, int axis1 = 0, int axis2 = 1, NDArray output = null)
        {
            return Op.Diag(this, k, axis1, axis2, output);
        }

        /// doc
        public NDArray Dot(NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, DotForwardStype? forwardStype = null, NDArray output = null)
        {
            return Op.Dot(this, rhs, transposeA, transposeB, forwardStype, output);
        }

        /// doc
        public NDArray BatchDot(NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, BatchDotForwardStype? forwardStype = null, NDArray output = null)
        {
            return Op.BatchDot(this, rhs, transposeA, transposeB, forwardStype, output);
        }

        /// doc
        public NDArray BroadcastAdd(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastAdd(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastSub(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastSub(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastMul(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastMul(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastDiv(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastDiv(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastMod(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastMod(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastPower(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastPower(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastMaximum(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastMaximum(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastMinimum(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastMinimum(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastHypot(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastHypot(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastEqual(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastEqual(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastNotEqual(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastNotEqual(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastGreater(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastGreater(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastGreaterEqual(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastGreaterEqual(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastLesser(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastLesser(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastLesserEqual(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastLesserEqual(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastLogicalAnd(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastLogicalAnd(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastLogicalOr(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastLogicalOr(this, rhs, output);
        }

        /// doc
        public NDArray BroadcastLogicalXor(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.BroadcastLogicalXor(this, rhs, output);
        }

        /// doc
        public NDArray ElemwiseAdd(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.ElemwiseAdd(this, rhs, output);
        }

        /// doc
        public NDArray GradAdd(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.GradAdd(this, rhs, output);
        }

        /// doc
        public NDArray ElemwiseSub(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.ElemwiseSub(this, rhs, output);
        }

        /// doc
        public NDArray ElemwiseMul(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.ElemwiseMul(this, rhs, output);
        }

        /// doc
        public NDArray ElemwiseDiv(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.ElemwiseDiv(this, rhs, output);
        }

        /// doc
        public NDArray Mod(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.Mod(this, rhs, output);
        }

        /// doc
        public NDArray Power(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.Power(this, rhs, output);
        }

        /// doc
        public NDArray Maximum(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.Maximum(this, rhs, output);
        }

        /// doc
        public NDArray Minimum(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.Minimum(this, rhs, output);
        }

        /// doc
        public NDArray Hypot(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.Hypot(this, rhs, output);
        }

        /// doc
        public NDArray Equal(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.Equal(this, rhs, output);
        }

        /// doc
        public NDArray NotEqual(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.NotEqual(this, rhs, output);
        }

        /// doc
        public NDArray Greater(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.Greater(this, rhs, output);
        }

        /// doc
        public NDArray GreaterEqual(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.GreaterEqual(this, rhs, output);
        }

        /// doc
        public NDArray Lesser(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.Lesser(this, rhs, output);
        }

        /// doc
        public NDArray LesserEqual(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.LesserEqual(this, rhs, output);
        }

        /// doc
        public NDArray LogicalAnd(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.LogicalAnd(this, rhs, output);
        }

        /// doc
        public NDArray LogicalOr(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.LogicalOr(this, rhs, output);
        }

        /// doc
        public NDArray LogicalXor(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.LogicalXor(this, rhs, output);
        }

        /// doc
        public NDArray PlusScalar(float scalar, NDArray output = null)
        {
            return Op.PlusScalar(this, scalar, output);
        }

        /// doc
        public NDArray MinusScalar(float scalar, NDArray output = null)
        {
            return Op.MinusScalar(this, scalar, output);
        }

        /// doc
        public NDArray RminusScalar(float scalar, NDArray output = null)
        {
            return Op.RminusScalar(this, scalar, output);
        }

        /// doc
        public NDArray MulScalar(float scalar, NDArray output = null)
        {
            return Op.MulScalar(this, scalar, output);
        }

        /// doc
        public NDArray DivScalar(float scalar, NDArray output = null)
        {
            return Op.DivScalar(this, scalar, output);
        }

        /// doc
        public NDArray RdivScalar(float scalar, NDArray output = null)
        {
            return Op.RdivScalar(this, scalar, output);
        }

        /// doc
        public NDArray ModScalar(float scalar, NDArray output = null)
        {
            return Op.ModScalar(this, scalar, output);
        }

        /// doc
        public NDArray RmodScalar(float scalar, NDArray output = null)
        {
            return Op.RmodScalar(this, scalar, output);
        }

        /// doc
        public NDArray MaximumScalar(float scalar, NDArray output = null)
        {
            return Op.MaximumScalar(this, scalar, output);
        }

        /// doc
        public NDArray MinimumScalar(float scalar, NDArray output = null)
        {
            return Op.MinimumScalar(this, scalar, output);
        }

        /// doc
        public NDArray PowerScalar(float scalar, NDArray output = null)
        {
            return Op.PowerScalar(this, scalar, output);
        }

        /// doc
        public NDArray RpowerScalar(float scalar, NDArray output = null)
        {
            return Op.RpowerScalar(this, scalar, output);
        }

        /// doc
        public NDArray HypotScalar(float scalar, NDArray output = null)
        {
            return Op.HypotScalar(this, scalar, output);
        }

        /// doc
        public NDArray SmoothL1(float scalar, NDArray output = null)
        {
            return Op.SmoothL1(this, scalar, output);
        }

        /// doc
        public NDArray EqualScalar(float scalar, NDArray output = null)
        {
            return Op.EqualScalar(this, scalar, output);
        }

        /// doc
        public NDArray NotEqualScalar(float scalar, NDArray output = null)
        {
            return Op.NotEqualScalar(this, scalar, output);
        }

        /// doc
        public NDArray GreaterScalar(float scalar, NDArray output = null)
        {
            return Op.GreaterScalar(this, scalar, output);
        }

        /// doc
        public NDArray GreaterEqualScalar(float scalar, NDArray output = null)
        {
            return Op.GreaterEqualScalar(this, scalar, output);
        }

        /// doc
        public NDArray LesserScalar(float scalar, NDArray output = null)
        {
            return Op.LesserScalar(this, scalar, output);
        }

        /// doc
        public NDArray LesserEqualScalar(float scalar, NDArray output = null)
        {
            return Op.LesserEqualScalar(this, scalar, output);
        }

        /// doc
        public NDArray LogicalAndScalar(float scalar, NDArray output = null)
        {
            return Op.LogicalAndScalar(this, scalar, output);
        }

        /// doc
        public NDArray LogicalOrScalar(float scalar, NDArray output = null)
        {
            return Op.LogicalOrScalar(this, scalar, output);
        }

        /// doc
        public NDArray LogicalXorScalar(float scalar, NDArray output = null)
        {
            return Op.LogicalXorScalar(this, scalar, output);
        }

        /// doc
        public NDArray ScatterElemwiseDiv(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.ScatterElemwiseDiv(this, rhs, output);
        }

        /// doc
        public NDArray ScatterPlusScalar(float scalar, NDArray output = null)
        {
            return Op.ScatterPlusScalar(this, scalar, output);
        }

        /// doc
        public NDArray ScatterMinusScalar(float scalar, NDArray output = null)
        {
            return Op.ScatterMinusScalar(this, scalar, output);
        }

        /// doc
        public NDArray Relu(NDArray output = null)
        {
            return Op.Relu(this, output);
        }

        /// doc
        public NDArray Sigmoid(NDArray output = null)
        {
            return Op.Sigmoid(this, output);
        }

        /// doc
        public NDArray HardSigmoid(float alpha = 0.200000003f, float beta = 0.5f, NDArray output = null)
        {
            return Op.HardSigmoid(this, alpha, beta, output);
        }

        /// doc
        public NDArray Softsign(NDArray output = null)
        {
            return Op.Softsign(this, output);
        }

        /// doc
        public NDArray Copy(NDArray output = null)
        {
            return Op.Copy(this, output);
        }

        /// doc
        public NDArray BlockGrad(NDArray output = null)
        {
            return Op.BlockGrad(this, output);
        }

        /// doc
        public NDArray MakeLoss(NDArray output = null)
        {
            return Op.MakeLoss(this, output);
        }

        /// doc
        public NDArray IdentityWithAttrLikeRhs(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.IdentityWithAttrLikeRhs(this, rhs, output);
        }

        /// doc
        public NDArray ReshapeLike(NDArrayOrSymbol rhs, NDArray output = null)
        {
            return Op.ReshapeLike(this, rhs, output);
        }

        /// doc
        public NDArray ShapeArray(int? lhsBegin = null, int? lhsEnd = null, int? rhsBegin = null, int? rhsEnd = null, NDArray output = null)
        {
            return Op.ShapeArray(this, lhsBegin, lhsEnd, rhsBegin, rhsEnd, output);
        }

        /// doc
        public NDArray SizeArray(NDArray output = null)
        {
            return Op.SizeArray(this, output);
        }

        /// doc
        public NDArray Cast(DType dtype, NDArray output = null)
        {
            return Op.Cast(this, dtype, output);
        }

        /// doc
        public NDArray Negative(NDArray output = null)
        {
            return Op.Negative(this, output);
        }

        /// doc
        public NDArray Reciprocal(NDArray output = null)
        {
            return Op.Reciprocal(this, output);
        }

        /// doc
        public NDArray Abs(NDArray output = null)
        {
            return Op.Abs(this, output);
        }

        /// doc
        public NDArray Sign(NDArray output = null)
        {
            return Op.Sign(this, output);
        }

        /// doc
        public NDArray Round(NDArray output = null)
        {
            return Op.Round(this, output);
        }

        /// doc
        public NDArray Rint(NDArray output = null)
        {
            return Op.Rint(this, output);
        }

        /// doc
        public NDArray Ceil(NDArray output = null)
        {
            return Op.Ceil(this, output);
        }

        /// doc
        public NDArray Floor(NDArray output = null)
        {
            return Op.Floor(this, output);
        }

        /// doc
        public NDArray Trunc(NDArray output = null)
        {
            return Op.Trunc(this, output);
        }

        /// doc
        public NDArray Fix(NDArray output = null)
        {
            return Op.Fix(this, output);
        }

        /// doc
        public NDArray Square(NDArray output = null)
        {
            return Op.Square(this, output);
        }

        /// doc
        public NDArray Sqrt(NDArray output = null)
        {
            return Op.Sqrt(this, output);
        }

        /// doc
        public NDArray Rsqrt(NDArray output = null)
        {
            return Op.Rsqrt(this, output);
        }

        /// doc
        public NDArray Cbrt(NDArray output = null)
        {
            return Op.Cbrt(this, output);
        }

        /// doc
        public NDArray Erf(NDArray output = null)
        {
            return Op.Erf(this, output);
        }

        /// doc
        public NDArray Erfinv(NDArray output = null)
        {
            return Op.Erfinv(this, output);
        }

        /// doc
        public NDArray Rcbrt(NDArray output = null)
        {
            return Op.Rcbrt(this, output);
        }

        /// doc
        public NDArray Exp(NDArray output = null)
        {
            return Op.Exp(this, output);
        }

        /// doc
        public NDArray Log(NDArray output = null)
        {
            return Op.Log(this, output);
        }

        /// doc
        public NDArray Log10(NDArray output = null)
        {
            return Op.Log10(this, output);
        }

        /// doc
        public NDArray Log2(NDArray output = null)
        {
            return Op.Log2(this, output);
        }

        /// doc
        public NDArray Log1p(NDArray output = null)
        {
            return Op.Log1p(this, output);
        }

        /// doc
        public NDArray Expm1(NDArray output = null)
        {
            return Op.Expm1(this, output);
        }

        /// doc
        public NDArray Gamma(NDArray output = null)
        {
            return Op.Gamma(this, output);
        }

        /// doc
        public NDArray Gammaln(NDArray output = null)
        {
            return Op.Gammaln(this, output);
        }

        /// doc
        public NDArray LogicalNot(NDArray output = null)
        {
            return Op.LogicalNot(this, output);
        }

        /// doc
        public NDArray Sin(NDArray output = null)
        {
            return Op.Sin(this, output);
        }

        /// doc
        public NDArray Cos(NDArray output = null)
        {
            return Op.Cos(this, output);
        }

        /// doc
        public NDArray Tan(NDArray output = null)
        {
            return Op.Tan(this, output);
        }

        /// doc
        public NDArray Arcsin(NDArray output = null)
        {
            return Op.Arcsin(this, output);
        }

        /// doc
        public NDArray Arccos(NDArray output = null)
        {
            return Op.Arccos(this, output);
        }

        /// doc
        public NDArray Arctan(NDArray output = null)
        {
            return Op.Arctan(this, output);
        }

        /// doc
        public NDArray Degrees(NDArray output = null)
        {
            return Op.Degrees(this, output);
        }

        /// doc
        public NDArray Radians(NDArray output = null)
        {
            return Op.Radians(this, output);
        }

        /// doc
        public NDArray Sinh(NDArray output = null)
        {
            return Op.Sinh(this, output);
        }

        /// doc
        public NDArray Cosh(NDArray output = null)
        {
            return Op.Cosh(this, output);
        }

        /// doc
        public NDArray Tanh(NDArray output = null)
        {
            return Op.Tanh(this, output);
        }

        /// doc
        public NDArray Arcsinh(NDArray output = null)
        {
            return Op.Arcsinh(this, output);
        }

        /// doc
        public NDArray Arccosh(NDArray output = null)
        {
            return Op.Arccosh(this, output);
        }

        /// doc
        public NDArray Arctanh(NDArray output = null)
        {
            return Op.Arctanh(this, output);
        }

        /// doc
        public NDArray Histogram(NDArrayOrSymbol bins, int? binCnt = null, Tuple<double> range = null, NDArray output = null)
        {
            return Op.Histogram(this, bins, binCnt, range, output);
        }

        /// doc
        public NDArray Embedding(NDArrayOrSymbol weight, int inputDim, int outputDim, DType dtype = null, bool sparseGrad = false, NDArray output = null)
        {
            return Op.Embedding(this, weight, inputDim, outputDim, dtype, sparseGrad, output);
        }

        /// doc
        public NDArray Take(NDArrayOrSymbol indices, int axis = 0, TakeMode mode = TakeMode.Clip, NDArray output = null)
        {
            return Op.Take(this, indices, axis, mode, output);
        }

        /// doc
        public NDArray BatchTake(NDArrayOrSymbol indices, NDArray output = null)
        {
            return Op.BatchTake(this, indices, output);
        }

        /// doc
        public NDArray OneHot(int depth, double onValue = 1, double offValue = 0, DType dtype = null, NDArray output = null)
        {
            return Op.OneHot(this, depth, onValue, offValue, dtype, output);
        }

        /// doc
        public NDArray GatherNd(NDArrayOrSymbol indices, NDArray output = null)
        {
            return Op.GatherNd(this, indices, output);
        }

        /// doc
        public NDArray ScatterNd(NDArrayOrSymbol indices, NDShape shape, NDArray output = null)
        {
            return Op.ScatterNd(this, indices, shape, output);
        }

        /// doc
        public NDArray ScatterSetNd(NDArrayOrSymbol rhs, NDArrayOrSymbol indices, NDShape shape, NDArray output = null)
        {
            return Op.ScatterSetNd(this, rhs, indices, shape, output);
        }

        /// doc
        public NDArray ZerosLike(NDArray output = null)
        {
            return Op.ZerosLike(this, output);
        }

        /// doc
        public NDArray OnesLike(NDArray output = null)
        {
            return Op.OnesLike(this, output);
        }

        /// doc
        public NDArray LinalgGemm(NDArrayOrSymbol B, NDArrayOrSymbol C, bool transposeA = false, bool transposeB = false, double alpha = 1, double beta = 1, int axis = -2, NDArray output = null)
        {
            return Op.LinalgGemm(this, B, C, transposeA, transposeB, alpha, beta, axis, output);
        }

        /// doc
        public NDArray LinalgGemm2(NDArrayOrSymbol B, bool transposeA = false, bool transposeB = false, double alpha = 1, int axis = -2, NDArray output = null)
        {
            return Op.LinalgGemm2(this, B, transposeA, transposeB, alpha, axis, output);
        }

        /// doc
        public NDArray LinalgPotrf(NDArray output = null)
        {
            return Op.LinalgPotrf(this, output);
        }

        /// doc
        public NDArray LinalgPotri(NDArray output = null)
        {
            return Op.LinalgPotri(this, output);
        }

        /// doc
        public NDArray LinalgTrmm(NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1, NDArray output = null)
        {
            return Op.LinalgTrmm(this, B, transpose, rightside, lower, alpha, output);
        }

        /// doc
        public NDArray LinalgTrsm(NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1, NDArray output = null)
        {
            return Op.LinalgTrsm(this, B, transpose, rightside, lower, alpha, output);
        }

        /// doc
        public NDArray LinalgSumlogdiag(NDArray output = null)
        {
            return Op.LinalgSumlogdiag(this, output);
        }

        /// doc
        public NDArray LinalgExtractdiag(int offset = 0, NDArray output = null)
        {
            return Op.LinalgExtractdiag(this, offset, output);
        }

        /// doc
        public NDArray LinalgMakediag(int offset = 0, NDArray output = null)
        {
            return Op.LinalgMakediag(this, offset, output);
        }

        /// doc
        public NDArray LinalgExtracttrian(int offset = 0, bool lower = true, NDArray output = null)
        {
            return Op.LinalgExtracttrian(this, offset, lower, output);
        }

        /// doc
        public NDArray LinalgMaketrian(int offset = 0, bool lower = true, NDArray output = null)
        {
            return Op.LinalgMaketrian(this, offset, lower, output);
        }

        /// doc
        public NDArray LinalgSyrk(bool transpose = false, double alpha = 1, NDArray output = null)
        {
            return Op.LinalgSyrk(this, transpose, alpha, output);
        }

        /// doc
        public NDArray LinalgGelqf(NDArray output = null)
        {
            return Op.LinalgGelqf(this, output);
        }

        /// doc
        public NDArray LinalgSyevd(NDArray output = null)
        {
            return Op.LinalgSyevd(this, output);
        }

        /// doc
        public NDArray LinalgInverse(NDArray output = null)
        {
            return Op.LinalgInverse(this, output);
        }

        /// doc
        public NDArray Reshape(NDShape shape = null, bool reverse = false, NDArray output = null)
        {
            return Op.Reshape(this, shape, reverse, output);
        }

        /// doc
        public NDArray Transpose(NDShape axes = null, NDArray output = null)
        {
            return Op.Transpose(this, axes, output);
        }

        /// doc
        public NDArray ExpandDims(int axis, NDArray output = null)
        {
            return Op.ExpandDims(this, axis, output);
        }

        /// doc
        public NDArray Slice(NDShape begin, NDShape end, NDShape step = null, NDArray output = null)
        {
            return Op.Slice(this, begin, end, step, output);
        }

        /// doc
        public NDArray SliceAssign(NDArrayOrSymbol rhs, NDShape begin, NDShape end, NDShape step = null, NDArray output = null)
        {
            return Op.SliceAssign(this, rhs, begin, end, step, output);
        }

        /// doc
        public NDArray SliceAssignScalar(NDShape begin, NDShape end, double scalar = 0, NDShape step = null, NDArray output = null)
        {
            return Op.SliceAssignScalar(this, begin, end, scalar, step, output);
        }

        /// doc
        public NDArray SliceAxis(int axis, int begin, int? end, NDArray output = null)
        {
            return Op.SliceAxis(this, axis, begin, end, output);
        }

        /// doc
        public NDArray SliceLike(NDArrayOrSymbol shapeLike, NDShape axes = null, NDArray output = null)
        {
            return Op.SliceLike(this, shapeLike, axes, output);
        }

        /// doc
        public NDArray Clip(float aMin, float aMax, NDArray output = null)
        {
            return Op.Clip(this, aMin, aMax, output);
        }

        /// doc
        public NDArray Repeat(int repeats, int? axis = null, NDArray output = null)
        {
            return Op.Repeat(this, repeats, axis, output);
        }

        /// doc
        public NDArray Tile(NDShape reps, NDArray output = null)
        {
            return Op.Tile(this, reps, output);
        }

        /// doc
        public NDArray Reverse(NDShape axis, NDArray output = null)
        {
            return Op.Reverse(this, axis, output);
        }

        /// doc
        public NDArray DepthToSpace(int blockSize, NDArray output = null)
        {
            return Op.DepthToSpace(this, blockSize, output);
        }

        /// doc
        public NDArray SpaceToDepth(int blockSize, NDArray output = null)
        {
            return Op.SpaceToDepth(this, blockSize, output);
        }

        /// doc
        public NDArray SplitV2(NDShape indices, int axis = 1, bool squeezeAxis = false, int sections = 0, NDArray output = null)
        {
            return Op.SplitV2(this, indices, axis, squeezeAxis, sections, output);
        }

        /// doc
        public NDArray Topk(int? axis = -1, int k = 1, TopkRetTyp retTyp = TopkRetTyp.Indices, bool isAscend = false, DType dtype = null, NDArray output = null)
        {
            return Op.Topk(this, axis, k, retTyp, isAscend, dtype, output);
        }

        /// doc
        public NDArray Sort(int? axis = -1, bool isAscend = true, NDArray output = null)
        {
            return Op.Sort(this, axis, isAscend, output);
        }

        /// doc
        public NDArray Argsort(int? axis = -1, bool isAscend = true, DType dtype = null, NDArray output = null)
        {
            return Op.Argsort(this, axis, isAscend, dtype, output);
        }

        /// doc
        public NDArray RavelMultiIndex(NDShape shape = null, NDArray output = null)
        {
            return Op.RavelMultiIndex(this, shape, output);
        }

        /// doc
        public NDArray UnravelIndex(NDShape shape = null, NDArray output = null)
        {
            return Op.UnravelIndex(this, shape, output);
        }

        /// doc
        public NDArray SparseRetain(NDArrayOrSymbol indices, NDArray output = null)
        {
            return Op.SparseRetain(this, indices, output);
        }

        /// doc
        public NDArray SquareSum(NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            return Op.SquareSum(this, axis, keepdims, exclude, output);
        }

        /// doc
        public NDArray BilinearSampler(NDArrayOrSymbol grid, bool? cudnnOff = null, NDArray output = null)
        {
            return Op.BilinearSampler(this, grid, cudnnOff, output);
        }

        /// doc
        public NDArray ConvolutionV1(NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionV1CudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionV1Layout? layout = null, NDArray output = null)
        {
            return Op.ConvolutionV1(this, weight, bias, kernel, numFilter, stride, dilate, pad, numGroup, workspace, noBias, cudnnTune, cudnnOff, layout, output);
        }

        /// doc
        public NDArray Correlation(NDArrayOrSymbol data2, uint kernelSize = 1, uint maxDisplacement = 1, uint stride1 = 1, uint stride2 = 1, uint padSize = 0, bool isMultiply = true, NDArray output = null)
        {
            return Op.Correlation(this, data2, kernelSize, maxDisplacement, stride1, stride2, padSize, isMultiply, output);
        }

        /// doc
        public NDArray GridGenerator(GridgeneratorTransformType transformType, NDShape targetShape = null, NDArray output = null)
        {
            return Op.GridGenerator(this, transformType, targetShape, output);
        }

        /// doc
        public NDArray InstanceNorm(NDArrayOrSymbol gamma, NDArrayOrSymbol beta, float eps = 0.00100000005f, NDArray output = null)
        {
            return Op.InstanceNorm(this, gamma, beta, eps, output);
        }

        /// doc
        public NDArray L2Normalization(float eps = 1.00000001e-10f, L2normalizationMode mode = L2normalizationMode.Instance, NDArray output = null)
        {
            return Op.L2Normalization(this, eps, mode, output);
        }

        /// doc
        public NDArray PoolingV1(NDShape kernel = null, PoolingV1PoolType poolType = PoolingV1PoolType.Max, bool globalPool = false, PoolingV1PoolingConvention poolingConvention = PoolingV1PoolingConvention.Valid, NDShape stride = null, NDShape pad = null, NDArray output = null)
        {
            return Op.PoolingV1(this, kernel, poolType, globalPool, poolingConvention, stride, pad, output);
        }

        /// doc
        public NDArray ROIPooling(NDArrayOrSymbol rois, NDShape pooledSize, float spatialScale, NDArray output = null)
        {
            return Op.ROIPooling(this, rois, pooledSize, spatialScale, output);
        }

        /// doc
        public NDArray SequenceLast(NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0, NDArray output = null)
        {
            return Op.SequenceLast(this, sequenceLength, useSequenceLength, axis, output);
        }

        /// doc
        public NDArray SequenceMask(NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, float value = 0f, int axis = 0, NDArray output = null)
        {
            return Op.SequenceMask(this, sequenceLength, useSequenceLength, value, axis, output);
        }

        /// doc
        public NDArray SequenceReverse(NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0, NDArray output = null)
        {
            return Op.SequenceReverse(this, sequenceLength, useSequenceLength, axis, output);
        }

        /// doc
        public NDArray SpatialTransformer(NDArrayOrSymbol loc, SpatialtransformerTransformType transformType, SpatialtransformerSamplerType samplerType, NDShape targetShape = null, bool? cudnnOff = null, NDArray output = null)
        {
            return Op.SpatialTransformer(this, loc, transformType, samplerType, targetShape, cudnnOff, output);
        }

        /// doc
        public NDArray SVMOutput(NDArrayOrSymbol label, float margin = 1f, float regularizationCoefficient = 1f, bool useLinear = false, NDArray output = null)
        {
            return Op.SVMOutput(this, label, margin, regularizationCoefficient, useLinear, output);
        }

        /// doc
        public NDArray Imdecode(int index, int x0, int y0, int x1, int y1, int c, int size, NDArray output = null)
        {
            return Op.Imdecode(this, index, x0, y0, x1, y1, c, size, output);
        }
    }
}
