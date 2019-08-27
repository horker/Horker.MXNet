using System;
using System.Collections.Generic;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;
using Horker.Numerics;

namespace Horker.Numerics
{
    public partial class NumericNDArray<T> : NDArray<T>
        where T: struct
    {

        /// func
        public NumericNDArray<T> BatchNormV1(NumericNDArray<T> gamma, NumericNDArray<T> beta, float eps = 0.00100000005f, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false)
        {
            var impl = Op.BatchNormV1(_impl, gamma._impl, beta._impl, eps, momentum, fixGamma, useGlobalStats, outputMeanVar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MpAdamwUpdate(NumericNDArray<T> grad, NumericNDArray<T> mean, NumericNDArray<T> var, NumericNDArray<T> weight32, NumericNDArray<T> rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f)
        {
            var impl = Op.MpAdamwUpdate(_impl, grad._impl, mean._impl, var._impl, weight32._impl, rescaleGrad._impl, lr, eta, beta1, beta2, epsilon, wd, clipGradient);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> AdamwUpdate(NumericNDArray<T> grad, NumericNDArray<T> mean, NumericNDArray<T> var, NumericNDArray<T> rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f)
        {
            var impl = Op.AdamwUpdate(_impl, grad._impl, mean._impl, var._impl, rescaleGrad._impl, lr, eta, beta1, beta2, epsilon, wd, clipGradient);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> IdentityAttachKLSparseReg(float sparsenessTarget = 0.100000001f, float penalty = 0.00100000005f, float momentum = 0.899999976f)
        {
            var impl = Op.IdentityAttachKLSparseReg(_impl, sparsenessTarget, penalty, momentum);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LeakyReLU(NumericNDArray<T> gamma, LeakyreluActType actType = LeakyreluActType.Leaky, float slope = 0.25f, float lowerBound = 0.125f, float upperBound = 0.333999991f)
        {
            var impl = Op.LeakyReLU(_impl, gamma._impl, actType, slope, lowerBound, upperBound);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SoftmaxCrossEntropy(NumericNDArray<T> label)
        {
            var impl = Op.SoftmaxCrossEntropy(_impl, label._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Activation(ActivationActType actType)
        {
            var impl = Op.Activation(_impl, actType);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BatchNorm(NumericNDArray<T> gamma, NumericNDArray<T> beta, NumericNDArray<T> movingMean, NumericNDArray<T> movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false)
        {
            var impl = Op.BatchNorm(_impl, gamma._impl, beta._impl, movingMean._impl, movingVar._impl, eps, momentum, fixGamma, useGlobalStats, outputMeanVar, axis, cudnnOff);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Convolution(NumericNDArray<T> weight, NumericNDArray<T> bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionLayout? layout = null)
        {
            var impl = Op.Convolution(_impl, weight._impl, bias._impl, kernel, numFilter, stride, dilate, pad, numGroup, workspace, noBias, cudnnTune, cudnnOff, layout);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> CTCLoss(NumericNDArray<T> label, NumericNDArray<T> dataLengths, NumericNDArray<T> labelLengths, bool useDataLengths = false, bool useLabelLengths = false, CtclossBlankLabel blankLabel = CtclossBlankLabel.First)
        {
            var impl = Op.CTCLoss(_impl, label._impl, dataLengths._impl, labelLengths._impl, useDataLengths, useLabelLengths, blankLabel);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> CuDNNBatchNorm(NumericNDArray<T> gamma, NumericNDArray<T> beta, NumericNDArray<T> movingMean, NumericNDArray<T> movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false)
        {
            var impl = Op.CuDNNBatchNorm(_impl, gamma._impl, beta._impl, movingMean._impl, movingVar._impl, eps, momentum, fixGamma, useGlobalStats, outputMeanVar, axis, cudnnOff);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Deconvolution(NumericNDArray<T> weight, NumericNDArray<T> bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, NDShape adj = null, NDShape targetShape = null, uint numGroup = 1, ulong workspace = 512, bool noBias = true, DeconvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, DeconvolutionLayout? layout = null)
        {
            var impl = Op.Deconvolution(_impl, weight._impl, bias._impl, kernel, numFilter, stride, dilate, pad, adj, targetShape, numGroup, workspace, noBias, cudnnTune, cudnnOff, layout);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Dropout(float p = 0.5f, DropoutMode mode = DropoutMode.Training, NDShape axes = null, bool? cudnnOff = false)
        {
            var impl = Op.Dropout(_impl, p, mode, axes, cudnnOff);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> FullyConnected(NumericNDArray<T> weight, NumericNDArray<T> bias, int numHidden, bool noBias = false, bool flatten = true)
        {
            var impl = Op.FullyConnected(_impl, weight._impl, bias._impl, numHidden, noBias, flatten);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LayerNorm(NumericNDArray<T> gamma, NumericNDArray<T> beta, int axis = -1, float eps = 9.99999975e-06f, bool outputMeanVar = false)
        {
            var impl = Op.LayerNorm(_impl, gamma._impl, beta._impl, axis, eps, outputMeanVar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LRN(uint nsize, float alpha = 9.99999975e-05f, float beta = 0.75f, float knorm = 2f)
        {
            var impl = Op.LRN(_impl, nsize, alpha, beta, knorm);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Moments(NDShape axes = null, bool keepdims = false)
        {
            var impl = Op.Moments(_impl, axes, keepdims);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Pooling(NDShape kernel = null, PoolingPoolType poolType = PoolingPoolType.Max, bool globalPool = false, bool cudnnOff = false, PoolingPoolingConvention poolingConvention = PoolingPoolingConvention.Valid, NDShape stride = null, NDShape pad = null, int? pValue = null, bool? countIncludePad = null, PoolingLayout? layout = null)
        {
            var impl = Op.Pooling(_impl, kernel, poolType, globalPool, cudnnOff, poolingConvention, stride, pad, pValue, countIncludePad, layout);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Softmax(int axis = -1, double? temperature = null, DType dtype = null)
        {
            var impl = Op.Softmax(_impl, axis, temperature, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Softmin(int axis = -1, double? temperature = null, DType dtype = null)
        {
            var impl = Op.Softmin(_impl, axis, temperature, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LogSoftmax(int axis = -1, double? temperature = null, DType dtype = null)
        {
            var impl = Op.LogSoftmax(_impl, axis, temperature, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SoftmaxActivation(SoftmaxactivationMode mode = SoftmaxactivationMode.Instance)
        {
            var impl = Op.SoftmaxActivation(_impl, mode);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SignsgdUpdate(NumericNDArray<T> grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var impl = Op.SignsgdUpdate(_impl, grad._impl, lr, wd, rescaleGrad, clipGradient);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SignumUpdate(NumericNDArray<T> grad, NumericNDArray<T> mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float wdLh = 0f)
        {
            var impl = Op.SignumUpdate(_impl, grad._impl, mom._impl, lr, momentum, wd, rescaleGrad, clipGradient, wdLh);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SgdUpdate(NumericNDArray<T> grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var impl = Op.SgdUpdate(_impl, grad._impl, lr, wd, rescaleGrad, clipGradient, lazyUpdate);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SgdMomUpdate(NumericNDArray<T> grad, NumericNDArray<T> mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var impl = Op.SgdMomUpdate(_impl, grad._impl, mom._impl, lr, momentum, wd, rescaleGrad, clipGradient, lazyUpdate);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MpSgdUpdate(NumericNDArray<T> grad, NumericNDArray<T> weight32, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var impl = Op.MpSgdUpdate(_impl, grad._impl, weight32._impl, lr, wd, rescaleGrad, clipGradient, lazyUpdate);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MpSgdMomUpdate(NumericNDArray<T> grad, NumericNDArray<T> mom, NumericNDArray<T> weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var impl = Op.MpSgdMomUpdate(_impl, grad._impl, mom._impl, weight32._impl, lr, momentum, wd, rescaleGrad, clipGradient, lazyUpdate);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> FtmlUpdate(NumericNDArray<T> grad, NumericNDArray<T> d, NumericNDArray<T> v, NumericNDArray<T> z, float lr, int t, float beta1 = 0.600000024f, float beta2 = 0.999000013f, double epsilon = 9.9999999392252903e-09, float wd = 0f, float rescaleGrad = 1f, float clipGrad = -1f)
        {
            var impl = Op.FtmlUpdate(_impl, grad._impl, d._impl, v._impl, z._impl, lr, t, beta1, beta2, epsilon, wd, rescaleGrad, clipGrad);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> AdamUpdate(NumericNDArray<T> grad, NumericNDArray<T> mean, NumericNDArray<T> var, float lr, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var impl = Op.AdamUpdate(_impl, grad._impl, mean._impl, var._impl, lr, beta1, beta2, epsilon, wd, rescaleGrad, clipGradient, lazyUpdate);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> NagMomUpdate(NumericNDArray<T> grad, NumericNDArray<T> mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var impl = Op.NagMomUpdate(_impl, grad._impl, mom._impl, lr, momentum, wd, rescaleGrad, clipGradient);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MpNagMomUpdate(NumericNDArray<T> grad, NumericNDArray<T> mom, NumericNDArray<T> weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var impl = Op.MpNagMomUpdate(_impl, grad._impl, mom._impl, weight32._impl, lr, momentum, wd, rescaleGrad, clipGradient);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RmspropUpdate(NumericNDArray<T> grad, NumericNDArray<T> n, float lr, float gamma1 = 0.949999988f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f)
        {
            var impl = Op.RmspropUpdate(_impl, grad._impl, n._impl, lr, gamma1, epsilon, wd, rescaleGrad, clipGradient, clipWeights);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RmspropalexUpdate(NumericNDArray<T> grad, NumericNDArray<T> n, NumericNDArray<T> g, NumericNDArray<T> delta, float lr, float gamma1 = 0.949999988f, float gamma2 = 0.899999976f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f)
        {
            var impl = Op.RmspropalexUpdate(_impl, grad._impl, n._impl, g._impl, delta._impl, lr, gamma1, gamma2, epsilon, wd, rescaleGrad, clipGradient, clipWeights);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> FtrlUpdate(NumericNDArray<T> grad, NumericNDArray<T> z, NumericNDArray<T> n, float lr, float lamda1 = 0.00999999978f, float beta = 1f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var impl = Op.FtrlUpdate(_impl, grad._impl, z._impl, n._impl, lr, lamda1, beta, wd, rescaleGrad, clipGradient);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SparseAdagradUpdate(NumericNDArray<T> grad, NumericNDArray<T> history, float lr, float epsilon = 1.00000001e-07f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var impl = Op.SparseAdagradUpdate(_impl, grad._impl, history._impl, lr, epsilon, wd, rescaleGrad, clipGradient);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Pad(PadMode mode, NDShape padWidth, double constantValue = 0)
        {
            var impl = Op.Pad(_impl, mode, padWidth, constantValue);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Flatten()
        {
            var impl = Op.Flatten(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SampleUniform(NumericNDArray<T> high, NDShape shape = null, DType dtype = null)
        {
            var impl = Op.SampleUniform(_impl, high._impl, shape, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SampleNormal(NumericNDArray<T> sigma, NDShape shape = null, DType dtype = null)
        {
            var impl = Op.SampleNormal(_impl, sigma._impl, shape, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SampleGamma(NumericNDArray<T> beta, NDShape shape = null, DType dtype = null)
        {
            var impl = Op.SampleGamma(_impl, beta._impl, shape, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SampleExponential(NDShape shape = null, DType dtype = null)
        {
            var impl = Op.SampleExponential(_impl, shape, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SamplePoisson(NDShape shape = null, DType dtype = null)
        {
            var impl = Op.SamplePoisson(_impl, shape, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SampleNegativeBinomial(NumericNDArray<T> p, NDShape shape = null, DType dtype = null)
        {
            var impl = Op.SampleNegativeBinomial(_impl, p._impl, shape, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SampleGeneralizedNegativeBinomial(NumericNDArray<T> alpha, NDShape shape = null, DType dtype = null)
        {
            var impl = Op.SampleGeneralizedNegativeBinomial(_impl, alpha._impl, shape, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SampleMultinomial(NDShape shape = null, bool getProb = false, DType dtype = null)
        {
            var impl = Op.SampleMultinomial(_impl, shape, getProb, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RandomUniformLike(float low = 0f, float high = 1f)
        {
            var impl = Op.RandomUniformLike(_impl, low, high);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RandomNormalLike(float loc = 0f, float scale = 1f)
        {
            var impl = Op.RandomNormalLike(_impl, loc, scale);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RandomGammaLike(float alpha = 1f, float beta = 1f)
        {
            var impl = Op.RandomGammaLike(_impl, alpha, beta);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RandomExponentialLike(float lam = 1f)
        {
            var impl = Op.RandomExponentialLike(_impl, lam);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RandomPoissonLike(float lam = 1f)
        {
            var impl = Op.RandomPoissonLike(_impl, lam);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RandomNegativeBinomialLike(int k = 1, float p = 1f)
        {
            var impl = Op.RandomNegativeBinomialLike(_impl, k, p);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RandomGeneralizedNegativeBinomialLike(float mu = 1f, float alpha = 1f)
        {
            var impl = Op.RandomGeneralizedNegativeBinomialLike(_impl, mu, alpha);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Shuffle()
        {
            var impl = Op.Shuffle(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinearRegressionOutput(NumericNDArray<T> label, float gradScale = 1f)
        {
            var impl = Op.LinearRegressionOutput(_impl, label._impl, gradScale);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MAERegressionOutput(NumericNDArray<T> label, float gradScale = 1f)
        {
            var impl = Op.MAERegressionOutput(_impl, label._impl, gradScale);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LogisticRegressionOutput(NumericNDArray<T> label, float gradScale = 1f)
        {
            var impl = Op.LogisticRegressionOutput(_impl, label._impl, gradScale);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RNN(NumericNDArray<T> parameters, NumericNDArray<T> state, NumericNDArray<T> stateCell, NumericNDArray<T> sequenceLength, uint stateSize, uint numLayers, RNNMode mode, bool bidirectional = false, float p = 0f, bool stateOutputs = false, int? projectionSize = null, double? lstmStateClipMin = null, double? lstmStateClipMax = null, bool lstmStateClipNan = false, bool useSequenceLength = false)
        {
            var impl = Op.RNN(_impl, parameters._impl, state._impl, stateCell._impl, sequenceLength._impl, stateSize, numLayers, mode, bidirectional, p, stateOutputs, projectionSize, lstmStateClipMin, lstmStateClipMax, lstmStateClipNan, useSequenceLength);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SliceChannel(int numOutputs, int axis = 1, bool squeezeAxis = false)
        {
            var impl = Op.SliceChannel(_impl, numOutputs, axis, squeezeAxis);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SoftmaxOutput(NumericNDArray<T> label, float gradScale = 1f, float ignoreLabel = -1f, bool multiOutput = false, bool useIgnore = false, bool preserveShape = false, SoftmaxoutputNormalization normalization = SoftmaxoutputNormalization.Null, bool outGrad = false, float smoothAlpha = 0f)
        {
            var impl = Op.SoftmaxOutput(_impl, label._impl, gradScale, ignoreLabel, multiOutput, useIgnore, preserveShape, normalization, outGrad, smoothAlpha);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SwapAxis(uint dim1 = 0, uint dim2 = 0)
        {
            var impl = Op.SwapAxis(_impl, dim1, dim2);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> AmpCast(DType dtype)
        {
            var impl = Op.AmpCast(_impl, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Argmax(int? axis = null, bool keepdims = false)
        {
            var impl = Op.Argmax(_impl, axis, keepdims);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Argmin(int? axis = null, bool keepdims = false)
        {
            var impl = Op.Argmin(_impl, axis, keepdims);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ArgmaxChannel()
        {
            var impl = Op.ArgmaxChannel(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Pick(NumericNDArray<T> index, int? axis = -1, bool keepdims = false, PickMode mode = PickMode.Clip)
        {
            var impl = Op.Pick(_impl, index._impl, axis, keepdims, mode);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Sum(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var impl = Op.Sum(_impl, axis, keepdims, exclude);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Mean(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var impl = Op.Mean(_impl, axis, keepdims, exclude);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Prod(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var impl = Op.Prod(_impl, axis, keepdims, exclude);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Nansum(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var impl = Op.Nansum(_impl, axis, keepdims, exclude);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Nanprod(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var impl = Op.Nanprod(_impl, axis, keepdims, exclude);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Max(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var impl = Op.Max(_impl, axis, keepdims, exclude);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Min(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var impl = Op.Min(_impl, axis, keepdims, exclude);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastAxis(NDShape axis = null, NDShape size = null)
        {
            var impl = Op.BroadcastAxis(_impl, axis, size);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastTo(NDShape shape = null)
        {
            var impl = Op.BroadcastTo(_impl, shape);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastLike(NumericNDArray<T> rhs, NDShape lhsAxes = null, NDShape rhsAxes = null)
        {
            var impl = Op.BroadcastLike(_impl, rhs._impl, lhsAxes, rhsAxes);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Norm(int ord = 2, NDShape axis = null, NormOutDtype? outDtype = null, bool keepdims = false)
        {
            var impl = Op.Norm(_impl, ord, axis, outDtype, keepdims);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> CastStorage(CastStorageStype stype)
        {
            var impl = Op.CastStorage(_impl, stype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Where(NumericNDArray<T> x, NumericNDArray<T> y)
        {
            var impl = Op.Where(_impl, x._impl, y._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Diag(int k = 0, int axis1 = 0, int axis2 = 1)
        {
            var impl = Op.Diag(_impl, k, axis1, axis2);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Dot(NumericNDArray<T> rhs, bool transposeA = false, bool transposeB = false, DotForwardStype? forwardStype = null)
        {
            var impl = Op.Dot(_impl, rhs._impl, transposeA, transposeB, forwardStype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BatchDot(NumericNDArray<T> rhs, bool transposeA = false, bool transposeB = false, BatchDotForwardStype? forwardStype = null)
        {
            var impl = Op.BatchDot(_impl, rhs._impl, transposeA, transposeB, forwardStype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastAdd(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastAdd(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastSub(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastSub(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastMul(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastMul(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastDiv(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastDiv(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastMod(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastMod(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastPower(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastPower(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastMaximum(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastMaximum(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastMinimum(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastMinimum(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastHypot(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastHypot(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastEqual(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastEqual(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastNotEqual(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastNotEqual(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastGreater(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastGreater(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastGreaterEqual(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastGreaterEqual(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastLesser(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastLesser(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastLesserEqual(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastLesserEqual(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastLogicalAnd(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastLogicalAnd(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastLogicalOr(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastLogicalOr(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BroadcastLogicalXor(NumericNDArray<T> rhs)
        {
            var impl = Op.BroadcastLogicalXor(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ElemwiseAdd(NumericNDArray<T> rhs)
        {
            var impl = Op.ElemwiseAdd(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> GradAdd(NumericNDArray<T> rhs)
        {
            var impl = Op.GradAdd(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ElemwiseSub(NumericNDArray<T> rhs)
        {
            var impl = Op.ElemwiseSub(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ElemwiseMul(NumericNDArray<T> rhs)
        {
            var impl = Op.ElemwiseMul(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ElemwiseDiv(NumericNDArray<T> rhs)
        {
            var impl = Op.ElemwiseDiv(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Mod(NumericNDArray<T> rhs)
        {
            var impl = Op.Mod(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Power(NumericNDArray<T> rhs)
        {
            var impl = Op.Power(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Maximum(NumericNDArray<T> rhs)
        {
            var impl = Op.Maximum(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Minimum(NumericNDArray<T> rhs)
        {
            var impl = Op.Minimum(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Hypot(NumericNDArray<T> rhs)
        {
            var impl = Op.Hypot(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Equal(NumericNDArray<T> rhs)
        {
            var impl = Op.Equal(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> NotEqual(NumericNDArray<T> rhs)
        {
            var impl = Op.NotEqual(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Greater(NumericNDArray<T> rhs)
        {
            var impl = Op.Greater(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> GreaterEqual(NumericNDArray<T> rhs)
        {
            var impl = Op.GreaterEqual(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Lesser(NumericNDArray<T> rhs)
        {
            var impl = Op.Lesser(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LesserEqual(NumericNDArray<T> rhs)
        {
            var impl = Op.LesserEqual(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LogicalAnd(NumericNDArray<T> rhs)
        {
            var impl = Op.LogicalAnd(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LogicalOr(NumericNDArray<T> rhs)
        {
            var impl = Op.LogicalOr(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LogicalXor(NumericNDArray<T> rhs)
        {
            var impl = Op.LogicalXor(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> PlusScalar(float scalar)
        {
            var impl = Op.PlusScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MinusScalar(float scalar)
        {
            var impl = Op.MinusScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RminusScalar(float scalar)
        {
            var impl = Op.RminusScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MulScalar(float scalar)
        {
            var impl = Op.MulScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> DivScalar(float scalar)
        {
            var impl = Op.DivScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RdivScalar(float scalar)
        {
            var impl = Op.RdivScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ModScalar(float scalar)
        {
            var impl = Op.ModScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RmodScalar(float scalar)
        {
            var impl = Op.RmodScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MaximumScalar(float scalar)
        {
            var impl = Op.MaximumScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MinimumScalar(float scalar)
        {
            var impl = Op.MinimumScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> PowerScalar(float scalar)
        {
            var impl = Op.PowerScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RpowerScalar(float scalar)
        {
            var impl = Op.RpowerScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> HypotScalar(float scalar)
        {
            var impl = Op.HypotScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SmoothL1(float scalar)
        {
            var impl = Op.SmoothL1(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> EqualScalar(float scalar)
        {
            var impl = Op.EqualScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> NotEqualScalar(float scalar)
        {
            var impl = Op.NotEqualScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> GreaterScalar(float scalar)
        {
            var impl = Op.GreaterScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> GreaterEqualScalar(float scalar)
        {
            var impl = Op.GreaterEqualScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LesserScalar(float scalar)
        {
            var impl = Op.LesserScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LesserEqualScalar(float scalar)
        {
            var impl = Op.LesserEqualScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LogicalAndScalar(float scalar)
        {
            var impl = Op.LogicalAndScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LogicalOrScalar(float scalar)
        {
            var impl = Op.LogicalOrScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LogicalXorScalar(float scalar)
        {
            var impl = Op.LogicalXorScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ScatterElemwiseDiv(NumericNDArray<T> rhs)
        {
            var impl = Op.ScatterElemwiseDiv(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ScatterPlusScalar(float scalar)
        {
            var impl = Op.ScatterPlusScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ScatterMinusScalar(float scalar)
        {
            var impl = Op.ScatterMinusScalar(_impl, scalar);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Relu()
        {
            var impl = Op.Relu(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Sigmoid()
        {
            var impl = Op.Sigmoid(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> HardSigmoid(float alpha = 0.200000003f, float beta = 0.5f)
        {
            var impl = Op.HardSigmoid(_impl, alpha, beta);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Softsign()
        {
            var impl = Op.Softsign(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Copy()
        {
            var impl = Op.Copy(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BlockGrad()
        {
            var impl = Op.BlockGrad(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MakeLoss()
        {
            var impl = Op.MakeLoss(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> IdentityWithAttrLikeRhs(NumericNDArray<T> rhs)
        {
            var impl = Op.IdentityWithAttrLikeRhs(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ReshapeLike(NumericNDArray<T> rhs)
        {
            var impl = Op.ReshapeLike(_impl, rhs._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ShapeArray(int? lhsBegin = null, int? lhsEnd = null, int? rhsBegin = null, int? rhsEnd = null)
        {
            var impl = Op.ShapeArray(_impl, lhsBegin, lhsEnd, rhsBegin, rhsEnd);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SizeArray()
        {
            var impl = Op.SizeArray(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Cast(DType dtype)
        {
            var impl = Op.Cast(_impl, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Negative()
        {
            var impl = Op.Negative(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Reciprocal()
        {
            var impl = Op.Reciprocal(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Abs()
        {
            var impl = Op.Abs(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Sign()
        {
            var impl = Op.Sign(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Round()
        {
            var impl = Op.Round(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Rint()
        {
            var impl = Op.Rint(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Ceil()
        {
            var impl = Op.Ceil(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Floor()
        {
            var impl = Op.Floor(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Trunc()
        {
            var impl = Op.Trunc(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Fix()
        {
            var impl = Op.Fix(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Square()
        {
            var impl = Op.Square(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Sqrt()
        {
            var impl = Op.Sqrt(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Rsqrt()
        {
            var impl = Op.Rsqrt(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Cbrt()
        {
            var impl = Op.Cbrt(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Erf()
        {
            var impl = Op.Erf(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Erfinv()
        {
            var impl = Op.Erfinv(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Rcbrt()
        {
            var impl = Op.Rcbrt(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Exp()
        {
            var impl = Op.Exp(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Log()
        {
            var impl = Op.Log(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Log10()
        {
            var impl = Op.Log10(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Log2()
        {
            var impl = Op.Log2(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Log1p()
        {
            var impl = Op.Log1p(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Expm1()
        {
            var impl = Op.Expm1(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Gamma()
        {
            var impl = Op.Gamma(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Gammaln()
        {
            var impl = Op.Gammaln(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LogicalNot()
        {
            var impl = Op.LogicalNot(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Sin()
        {
            var impl = Op.Sin(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Cos()
        {
            var impl = Op.Cos(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Tan()
        {
            var impl = Op.Tan(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Arcsin()
        {
            var impl = Op.Arcsin(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Arccos()
        {
            var impl = Op.Arccos(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Arctan()
        {
            var impl = Op.Arctan(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Degrees()
        {
            var impl = Op.Degrees(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Radians()
        {
            var impl = Op.Radians(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Sinh()
        {
            var impl = Op.Sinh(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Cosh()
        {
            var impl = Op.Cosh(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Tanh()
        {
            var impl = Op.Tanh(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Arcsinh()
        {
            var impl = Op.Arcsinh(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Arccosh()
        {
            var impl = Op.Arccosh(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Arctanh()
        {
            var impl = Op.Arctanh(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Histogram(NumericNDArray<T> bins, int? binCnt = null, Tuple<double> range = null)
        {
            var impl = Op.Histogram(_impl, bins._impl, binCnt, range);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Embedding(NumericNDArray<T> weight, int inputDim, int outputDim, DType dtype = null, bool sparseGrad = false)
        {
            var impl = Op.Embedding(_impl, weight._impl, inputDim, outputDim, dtype, sparseGrad);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Take(NumericNDArray<T> indices, int axis = 0, TakeMode mode = TakeMode.Clip)
        {
            var impl = Op.Take(_impl, indices._impl, axis, mode);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BatchTake(NumericNDArray<T> indices)
        {
            var impl = Op.BatchTake(_impl, indices._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> OneHot(int depth, double onValue = 1, double offValue = 0, DType dtype = null)
        {
            var impl = Op.OneHot(_impl, depth, onValue, offValue, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> GatherNd(NumericNDArray<T> indices)
        {
            var impl = Op.GatherNd(_impl, indices._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ScatterNd(NumericNDArray<T> indices, NDShape shape)
        {
            var impl = Op.ScatterNd(_impl, indices._impl, shape);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ScatterSetNd(NumericNDArray<T> rhs, NumericNDArray<T> indices, NDShape shape)
        {
            var impl = Op.ScatterSetNd(_impl, rhs._impl, indices._impl, shape);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ZerosLike()
        {
            var impl = Op.ZerosLike(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> OnesLike()
        {
            var impl = Op.OnesLike(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgGemm(NumericNDArray<T> B, NumericNDArray<T> C, bool transposeA = false, bool transposeB = false, double alpha = 1, double beta = 1, int axis = -2)
        {
            var impl = Op.LinalgGemm(_impl, B._impl, C._impl, transposeA, transposeB, alpha, beta, axis);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgGemm2(NumericNDArray<T> B, bool transposeA = false, bool transposeB = false, double alpha = 1, int axis = -2)
        {
            var impl = Op.LinalgGemm2(_impl, B._impl, transposeA, transposeB, alpha, axis);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgPotrf()
        {
            var impl = Op.LinalgPotrf(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgPotri()
        {
            var impl = Op.LinalgPotri(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgTrmm(NumericNDArray<T> B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1)
        {
            var impl = Op.LinalgTrmm(_impl, B._impl, transpose, rightside, lower, alpha);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgTrsm(NumericNDArray<T> B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1)
        {
            var impl = Op.LinalgTrsm(_impl, B._impl, transpose, rightside, lower, alpha);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgSumlogdiag()
        {
            var impl = Op.LinalgSumlogdiag(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgExtractdiag(int offset = 0)
        {
            var impl = Op.LinalgExtractdiag(_impl, offset);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgMakediag(int offset = 0)
        {
            var impl = Op.LinalgMakediag(_impl, offset);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgExtracttrian(int offset = 0, bool lower = true)
        {
            var impl = Op.LinalgExtracttrian(_impl, offset, lower);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgMaketrian(int offset = 0, bool lower = true)
        {
            var impl = Op.LinalgMaketrian(_impl, offset, lower);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgSyrk(bool transpose = false, double alpha = 1)
        {
            var impl = Op.LinalgSyrk(_impl, transpose, alpha);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgGelqf()
        {
            var impl = Op.LinalgGelqf(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgSyevd()
        {
            var impl = Op.LinalgSyevd(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> LinalgInverse()
        {
            var impl = Op.LinalgInverse(_impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Reshape(NDShape shape = null, bool reverse = false)
        {
            var impl = Op.Reshape(_impl, shape, reverse);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Transpose(NDShape axes = null)
        {
            var impl = Op.Transpose(_impl, axes);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ExpandDims(int axis)
        {
            var impl = Op.ExpandDims(_impl, axis);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Slice(NDShape begin, NDShape end, NDShape step = null)
        {
            var impl = Op.Slice(_impl, begin, end, step);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SliceAssign(NumericNDArray<T> rhs, NDShape begin, NDShape end, NDShape step = null)
        {
            var impl = Op.SliceAssign(_impl, rhs._impl, begin, end, step);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SliceAssignScalar(NDShape begin, NDShape end, double scalar = 0, NDShape step = null)
        {
            var impl = Op.SliceAssignScalar(_impl, begin, end, scalar, step);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SliceAxis(int axis, int begin, int? end)
        {
            var impl = Op.SliceAxis(_impl, axis, begin, end);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SliceLike(NumericNDArray<T> shapeLike, NDShape axes = null)
        {
            var impl = Op.SliceLike(_impl, shapeLike._impl, axes);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Clip(float aMin, float aMax)
        {
            var impl = Op.Clip(_impl, aMin, aMax);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Repeat(int repeats, int? axis = null)
        {
            var impl = Op.Repeat(_impl, repeats, axis);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Tile(NDShape reps)
        {
            var impl = Op.Tile(_impl, reps);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Reverse(NDShape axis)
        {
            var impl = Op.Reverse(_impl, axis);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> DepthToSpace(int blockSize)
        {
            var impl = Op.DepthToSpace(_impl, blockSize);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SpaceToDepth(int blockSize)
        {
            var impl = Op.SpaceToDepth(_impl, blockSize);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SplitV2(NDShape indices, int axis = 1, bool squeezeAxis = false, int sections = 0)
        {
            var impl = Op.SplitV2(_impl, indices, axis, squeezeAxis, sections);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Topk(int? axis = -1, int k = 1, TopkRetTyp retTyp = TopkRetTyp.Indices, bool isAscend = false, DType dtype = null)
        {
            var impl = Op.Topk(_impl, axis, k, retTyp, isAscend, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Sort(int? axis = -1, bool isAscend = true)
        {
            var impl = Op.Sort(_impl, axis, isAscend);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Argsort(int? axis = -1, bool isAscend = true, DType dtype = null)
        {
            var impl = Op.Argsort(_impl, axis, isAscend, dtype);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> RavelMultiIndex(NDShape shape = null)
        {
            var impl = Op.RavelMultiIndex(_impl, shape);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> UnravelIndex(NDShape shape = null)
        {
            var impl = Op.UnravelIndex(_impl, shape);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SparseRetain(NumericNDArray<T> indices)
        {
            var impl = Op.SparseRetain(_impl, indices._impl);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SquareSum(NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var impl = Op.SquareSum(_impl, axis, keepdims, exclude);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> BilinearSampler(NumericNDArray<T> grid, bool? cudnnOff = null)
        {
            var impl = Op.BilinearSampler(_impl, grid._impl, cudnnOff);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ConvolutionV1(NumericNDArray<T> weight, NumericNDArray<T> bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionV1CudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionV1Layout? layout = null)
        {
            var impl = Op.ConvolutionV1(_impl, weight._impl, bias._impl, kernel, numFilter, stride, dilate, pad, numGroup, workspace, noBias, cudnnTune, cudnnOff, layout);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Correlation(NumericNDArray<T> data2, uint kernelSize = 1, uint maxDisplacement = 1, uint stride1 = 1, uint stride2 = 1, uint padSize = 0, bool isMultiply = true)
        {
            var impl = Op.Correlation(_impl, data2._impl, kernelSize, maxDisplacement, stride1, stride2, padSize, isMultiply);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> GridGenerator(GridgeneratorTransformType transformType, NDShape targetShape = null)
        {
            var impl = Op.GridGenerator(_impl, transformType, targetShape);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> InstanceNorm(NumericNDArray<T> gamma, NumericNDArray<T> beta, float eps = 0.00100000005f)
        {
            var impl = Op.InstanceNorm(_impl, gamma._impl, beta._impl, eps);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> L2Normalization(float eps = 1.00000001e-10f, L2normalizationMode mode = L2normalizationMode.Instance)
        {
            var impl = Op.L2Normalization(_impl, eps, mode);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> MakeLoss(float gradScale = 1f, float validThresh = 0f, MakelossNormalization normalization = MakelossNormalization.Null)
        {
            var impl = Op.MakeLoss(_impl, gradScale, validThresh, normalization);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> PoolingV1(NDShape kernel = null, PoolingV1PoolType poolType = PoolingV1PoolType.Max, bool globalPool = false, PoolingV1PoolingConvention poolingConvention = PoolingV1PoolingConvention.Valid, NDShape stride = null, NDShape pad = null)
        {
            var impl = Op.PoolingV1(_impl, kernel, poolType, globalPool, poolingConvention, stride, pad);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> ROIPooling(NumericNDArray<T> rois, NDShape pooledSize, float spatialScale)
        {
            var impl = Op.ROIPooling(_impl, rois._impl, pooledSize, spatialScale);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SequenceLast(NumericNDArray<T> sequenceLength, bool useSequenceLength = false, int axis = 0)
        {
            var impl = Op.SequenceLast(_impl, sequenceLength._impl, useSequenceLength, axis);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SequenceMask(NumericNDArray<T> sequenceLength, bool useSequenceLength = false, float value = 0f, int axis = 0)
        {
            var impl = Op.SequenceMask(_impl, sequenceLength._impl, useSequenceLength, value, axis);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SequenceReverse(NumericNDArray<T> sequenceLength, bool useSequenceLength = false, int axis = 0)
        {
            var impl = Op.SequenceReverse(_impl, sequenceLength._impl, useSequenceLength, axis);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SpatialTransformer(NumericNDArray<T> loc, SpatialtransformerTransformType transformType, SpatialtransformerSamplerType samplerType, NDShape targetShape = null, bool? cudnnOff = null)
        {
            var impl = Op.SpatialTransformer(_impl, loc._impl, transformType, samplerType, targetShape, cudnnOff);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> SVMOutput(NumericNDArray<T> label, float margin = 1f, float regularizationCoefficient = 1f, bool useLinear = false)
        {
            var impl = Op.SVMOutput(_impl, label._impl, margin, regularizationCoefficient, useLinear);
            return new NumericNDArray<T>(impl);
        }

        /// func
        public NumericNDArray<T> Imdecode(int index, int x0, int y0, int x1, int y1, int c, int size)
        {
            var impl = Op.Imdecode(_impl, index, x0, y0, x1, y1, c, size);
            return new NumericNDArray<T>(impl);
        }
    }
}
