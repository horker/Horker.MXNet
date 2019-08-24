using System;
using System.Collections.Generic;
using Horker.MXNet.Core;

namespace Horker.MXNet.Operators
{
    public partial class Op : OperatorsBase
    {

        private static string[] _CustomFunctionParamNames = _empty;

        public static NDArray CustomFunction()
        {
            var results = Operator.Invoke(
                "_CustomFunction",
                _CustomFunctionParamNames,
                _empty,
                _emptyInput);
            return results[0];
        }

        private static string[] _cvimdecodeParamNames = new[] { "flag", "toRgb" };

        public static NDArray Cvimdecode(Symbol buf, int flag = 1, bool toRgb = true)
        {
            var results = Operator.Invoke(
                "_cvimdecode",
                _cvimdecodeParamNames,
                new[] { Convert(flag), Convert(toRgb) },
                new[] { buf });
            return results[0];
        }

        private static string[] _cvimreadParamNames = new[] { "filename", "flag", "toRgb" };

        public static NDArray Cvimread(string filename, int flag = 1, bool toRgb = true)
        {
            var results = Operator.Invoke(
                "_cvimread",
                _cvimreadParamNames,
                new[] { Convert(filename), Convert(flag), Convert(toRgb) },
                _emptyInput);
            return results[0];
        }

        private static string[] _cvimresizeParamNames = new[] { "w", "h", "interp" };

        public static NDArray Cvimresize(Symbol data, int w, int h, int interp = 1)
        {
            var results = Operator.Invoke(
                "_cvimresize",
                _cvimresizeParamNames,
                new[] { Convert(w), Convert(h), Convert(interp) },
                new[] { data });
            return results[0];
        }

        private static string[] _cvcopyMakeBorderParamNames = new[] { "top", "bot", "left", "right", "type", "values" };

        public static NDArray CvcopyMakeBorder(Symbol data, int top, int bot, int left, int right, int type = 0, Tuple<double> values = null)
        {
            var results = Operator.Invoke(
                "_cvcopyMakeBorder",
                _cvcopyMakeBorderParamNames,
                new[] { Convert(top), Convert(bot), Convert(left), Convert(right), Convert(type), Convert(values) },
                new[] { data });
            return results[0];
        }

        private static string[] _copytoParamNames = _empty;

        public static NDArray Copyto(Symbol data)
        {
            var results = Operator.Invoke(
                "_copyto",
                _copytoParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _NoGradientParamNames = _empty;

        public static NDArray NoGradient()
        {
            var results = Operator.Invoke(
                "_NoGradient",
                _NoGradientParamNames,
                _empty,
                _emptyInput);
            return results[0];
        }

        private static string[] _BatchNormV1ParamNames = new[] { "eps", "momentum", "fixGamma", "useGlobalStats", "outputMeanVar" };

        public static NDArray BatchNormV1(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, float eps = 0.00100000005f, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false)
        {
            var results = Operator.Invoke(
                "BatchNorm_v1",
                _BatchNormV1ParamNames,
                new[] { Convert(eps), Convert(momentum), Convert(fixGamma), Convert(useGlobalStats), Convert(outputMeanVar) },
                new[] { data, gamma, beta });
            return results[0];
        }

        private static string[] _mpAdamwUpdateParamNames = new[] { "lr", "eta", "beta1", "beta2", "epsilon", "wd", "clipGradient" };

        public static NDArray MpAdamwUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol weight32, NDArrayOrSymbol rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f)
        {
            var results = Operator.Invoke(
                "_mp_adamw_update",
                _mpAdamwUpdateParamNames,
                new[] { Convert(lr), Convert(eta), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(clipGradient) },
                new[] { weight, grad, mean, var, weight32, rescaleGrad });
            return results[0];
        }

        private static string[] _adamwUpdateParamNames = new[] { "lr", "eta", "beta1", "beta2", "epsilon", "wd", "clipGradient" };

        public static NDArray AdamwUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f)
        {
            var results = Operator.Invoke(
                "_adamw_update",
                _adamwUpdateParamNames,
                new[] { Convert(lr), Convert(eta), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(clipGradient) },
                new[] { weight, grad, mean, var, rescaleGrad });
            return results[0];
        }

        private static string[] _allFiniteParamNames = new[] { "initOutput" };

        public static NDArray AllFinite(Symbol data, bool initOutput = true)
        {
            var results = Operator.Invoke(
                "all_finite",
                _allFiniteParamNames,
                new[] { Convert(initOutput) },
                new[] { data });
            return results[0];
        }

        private static string[] _IdentityAttachKLSparseRegParamNames = new[] { "sparsenessTarget", "penalty", "momentum" };

        public static NDArray IdentityAttachKLSparseReg(NDArrayOrSymbol data, float sparsenessTarget = 0.100000001f, float penalty = 0.00100000005f, float momentum = 0.899999976f)
        {
            var results = Operator.Invoke(
                "IdentityAttachKLSparseReg",
                _IdentityAttachKLSparseRegParamNames,
                new[] { Convert(sparsenessTarget), Convert(penalty), Convert(momentum) },
                new[] { data });
            return results[0];
        }

        private static string[] _LeakyReLUParamNames = new[] { "actType", "slope", "lowerBound", "upperBound" };

        public static NDArray LeakyReLU(NDArrayOrSymbol data, NDArrayOrSymbol gamma, LeakyreluActType actType = LeakyreluActType.Leaky, float slope = 0.25f, float lowerBound = 0.125f, float upperBound = 0.333999991f)
        {
            var results = Operator.Invoke(
                "LeakyReLU",
                _LeakyReLUParamNames,
                new[] { Convert((int)actType), Convert(slope), Convert(lowerBound), Convert(upperBound) },
                new[] { data, gamma });
            return results[0];
        }

        private static string[] _softmaxCrossEntropyParamNames = _empty;

        public static NDArray SoftmaxCrossEntropy(NDArrayOrSymbol data, NDArrayOrSymbol label)
        {
            var results = Operator.Invoke(
                "softmax_cross_entropy",
                _softmaxCrossEntropyParamNames,
                _empty,
                new[] { data, label });
            return results[0];
        }

        private static string[] _ActivationParamNames = new[] { "actType" };

        public static NDArray Activation(NDArrayOrSymbol data, ActivationActType actType)
        {
            var results = Operator.Invoke(
                "Activation",
                _ActivationParamNames,
                new[] { Convert((int)actType) },
                new[] { data });
            return results[0];
        }

        private static string[] _BatchNormParamNames = new[] { "eps", "momentum", "fixGamma", "useGlobalStats", "outputMeanVar", "axis", "cudnnOff" };

        public static NDArray BatchNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false)
        {
            var results = Operator.Invoke(
                "BatchNorm",
                _BatchNormParamNames,
                new[] { Convert(eps), Convert(momentum), Convert(fixGamma), Convert(useGlobalStats), Convert(outputMeanVar), Convert(axis), Convert(cudnnOff) },
                new[] { data, gamma, beta, movingMean, movingVar });
            return results[0];
        }

        private static string[] _ConvolutionParamNames = new[] { "kernel", "numFilter", "stride", "dilate", "pad", "numGroup", "workspace", "noBias", "cudnnTune", "cudnnOff", "layout" };

        public static NDArray Convolution(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionLayout? layout = null)
        {
            var results = Operator.Invoke(
                "Convolution",
                _ConvolutionParamNames,
                new[] { Convert(kernel), Convert(numFilter), Convert(stride), Convert(dilate), Convert(pad), Convert(numGroup), Convert(workspace), Convert(noBias), Convert((int)cudnnTune), Convert(cudnnOff), Convert((int)layout) },
                new[] { data, weight, bias });
            return results[0];
        }

        private static string[] _CTCLossParamNames = new[] { "useDataLengths", "useLabelLengths", "blankLabel" };

        public static NDArray CTCLoss(NDArrayOrSymbol data, NDArrayOrSymbol label, NDArrayOrSymbol dataLengths, NDArrayOrSymbol labelLengths, bool useDataLengths = false, bool useLabelLengths = false, CtclossBlankLabel blankLabel = CtclossBlankLabel.First)
        {
            var results = Operator.Invoke(
                "CTCLoss",
                _CTCLossParamNames,
                new[] { Convert(useDataLengths), Convert(useLabelLengths), Convert((int)blankLabel) },
                new[] { data, label, dataLengths, labelLengths });
            return results[0];
        }

        private static string[] _CuDNNBatchNormParamNames = new[] { "eps", "momentum", "fixGamma", "useGlobalStats", "outputMeanVar", "axis", "cudnnOff" };

        public static NDArray CuDNNBatchNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false)
        {
            var results = Operator.Invoke(
                "CuDNNBatchNorm",
                _CuDNNBatchNormParamNames,
                new[] { Convert(eps), Convert(momentum), Convert(fixGamma), Convert(useGlobalStats), Convert(outputMeanVar), Convert(axis), Convert(cudnnOff) },
                new[] { data, gamma, beta, movingMean, movingVar });
            return results[0];
        }

        private static string[] _DeconvolutionParamNames = new[] { "kernel", "numFilter", "stride", "dilate", "pad", "adj", "targetShape", "numGroup", "workspace", "noBias", "cudnnTune", "cudnnOff", "layout" };

        public static NDArray Deconvolution(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, NDShape adj = null, NDShape targetShape = null, uint numGroup = 1, ulong workspace = 512, bool noBias = true, DeconvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, DeconvolutionLayout? layout = null)
        {
            var results = Operator.Invoke(
                "Deconvolution",
                _DeconvolutionParamNames,
                new[] { Convert(kernel), Convert(numFilter), Convert(stride), Convert(dilate), Convert(pad), Convert(adj), Convert(targetShape), Convert(numGroup), Convert(workspace), Convert(noBias), Convert((int)cudnnTune), Convert(cudnnOff), Convert((int)layout) },
                new[] { data, weight, bias });
            return results[0];
        }

        private static string[] _DropoutParamNames = new[] { "p", "mode", "axes", "cudnnOff" };

        public static NDArray Dropout(NDArrayOrSymbol data, float p = 0.5f, DropoutMode mode = DropoutMode.Training, NDShape axes = null, bool? cudnnOff = false)
        {
            var results = Operator.Invoke(
                "Dropout",
                _DropoutParamNames,
                new[] { Convert(p), Convert((int)mode), Convert(axes), Convert(cudnnOff) },
                new[] { data });
            return results[0];
        }

        private static string[] _FullyConnectedParamNames = new[] { "numHidden", "noBias", "flatten" };

        public static NDArray FullyConnected(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, int numHidden, bool noBias = false, bool flatten = true)
        {
            var results = Operator.Invoke(
                "FullyConnected",
                _FullyConnectedParamNames,
                new[] { Convert(numHidden), Convert(noBias), Convert(flatten) },
                new[] { data, weight, bias });
            return results[0];
        }

        private static string[] _LayerNormParamNames = new[] { "axis", "eps", "outputMeanVar" };

        public static NDArray LayerNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, int axis = -1, float eps = 9.99999975e-06f, bool outputMeanVar = false)
        {
            var results = Operator.Invoke(
                "LayerNorm",
                _LayerNormParamNames,
                new[] { Convert(axis), Convert(eps), Convert(outputMeanVar) },
                new[] { data, gamma, beta });
            return results[0];
        }

        private static string[] _LRNParamNames = new[] { "nsize", "alpha", "beta", "knorm" };

        public static NDArray LRN(NDArrayOrSymbol data, uint nsize, float alpha = 9.99999975e-05f, float beta = 0.75f, float knorm = 2f)
        {
            var results = Operator.Invoke(
                "LRN",
                _LRNParamNames,
                new[] { Convert(nsize), Convert(alpha), Convert(beta), Convert(knorm) },
                new[] { data });
            return results[0];
        }

        private static string[] _momentsParamNames = new[] { "axes", "keepdims" };

        public static NDArray Moments(NDArrayOrSymbol data, NDShape axes = null, bool keepdims = false)
        {
            var results = Operator.Invoke(
                "moments",
                _momentsParamNames,
                new[] { Convert(axes), Convert(keepdims) },
                new[] { data });
            return results[0];
        }

        private static string[] _PoolingParamNames = new[] { "kernel", "poolType", "globalPool", "cudnnOff", "poolingConvention", "stride", "pad", "pValue", "countIncludePad", "layout" };

        public static NDArray Pooling(NDArrayOrSymbol data, NDShape kernel = null, PoolingPoolType poolType = PoolingPoolType.Max, bool globalPool = false, bool cudnnOff = false, PoolingPoolingConvention poolingConvention = PoolingPoolingConvention.Valid, NDShape stride = null, NDShape pad = null, int? pValue = null, bool? countIncludePad = null, PoolingLayout? layout = null)
        {
            var results = Operator.Invoke(
                "Pooling",
                _PoolingParamNames,
                new[] { Convert(kernel), Convert((int)poolType), Convert(globalPool), Convert(cudnnOff), Convert((int)poolingConvention), Convert(stride), Convert(pad), Convert(pValue), Convert(countIncludePad), Convert((int)layout) },
                new[] { data });
            return results[0];
        }

        private static string[] _softmaxParamNames = new[] { "axis", "temperature", "dtype" };

        public static NDArray Softmax(NDArrayOrSymbol data, int axis = -1, double? temperature = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "softmax",
                _softmaxParamNames,
                new[] { Convert(axis), Convert(temperature), Convert(dtype) },
                new[] { data });
            return results[0];
        }

        private static string[] _softminParamNames = new[] { "axis", "temperature", "dtype" };

        public static NDArray Softmin(NDArrayOrSymbol data, int axis = -1, double? temperature = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "softmin",
                _softminParamNames,
                new[] { Convert(axis), Convert(temperature), Convert(dtype) },
                new[] { data });
            return results[0];
        }

        private static string[] _logSoftmaxParamNames = new[] { "axis", "temperature", "dtype" };

        public static NDArray LogSoftmax(NDArrayOrSymbol data, int axis = -1, double? temperature = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "log_softmax",
                _logSoftmaxParamNames,
                new[] { Convert(axis), Convert(temperature), Convert(dtype) },
                new[] { data });
            return results[0];
        }

        private static string[] _SoftmaxActivationParamNames = new[] { "mode" };

        public static NDArray SoftmaxActivation(NDArrayOrSymbol data, SoftmaxactivationMode mode = SoftmaxactivationMode.Instance)
        {
            var results = Operator.Invoke(
                "SoftmaxActivation",
                _SoftmaxActivationParamNames,
                new[] { Convert((int)mode) },
                new[] { data });
            return results[0];
        }

        private static string[] _signsgdUpdateParamNames = new[] { "lr", "wd", "rescaleGrad", "clipGradient" };

        public static NDArray SignsgdUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var results = Operator.Invoke(
                "signsgd_update",
                _signsgdUpdateParamNames,
                new[] { Convert(lr), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad });
            return results[0];
        }

        private static string[] _signumUpdateParamNames = new[] { "lr", "momentum", "wd", "rescaleGrad", "clipGradient", "wdLh" };

        public static NDArray SignumUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float wdLh = 0f)
        {
            var results = Operator.Invoke(
                "signum_update",
                _signumUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(wdLh) },
                new[] { weight, grad, mom });
            return results[0];
        }

        private static string[] _sgdUpdateParamNames = new[] { "lr", "wd", "rescaleGrad", "clipGradient", "lazyUpdate" };

        public static NDArray SgdUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var results = Operator.Invoke(
                "sgd_update",
                _sgdUpdateParamNames,
                new[] { Convert(lr), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad });
            return results[0];
        }

        private static string[] _sgdMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescaleGrad", "clipGradient", "lazyUpdate" };

        public static NDArray SgdMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var results = Operator.Invoke(
                "sgd_mom_update",
                _sgdMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad, mom });
            return results[0];
        }

        private static string[] _mpSgdUpdateParamNames = new[] { "lr", "wd", "rescaleGrad", "clipGradient", "lazyUpdate" };

        public static NDArray MpSgdUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol weight32, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var results = Operator.Invoke(
                "mp_sgd_update",
                _mpSgdUpdateParamNames,
                new[] { Convert(lr), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad, weight32 });
            return results[0];
        }

        private static string[] _mpSgdMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescaleGrad", "clipGradient", "lazyUpdate" };

        public static NDArray MpSgdMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var results = Operator.Invoke(
                "mp_sgd_mom_update",
                _mpSgdMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad, mom, weight32 });
            return results[0];
        }

        private static string[] _ftmlUpdateParamNames = new[] { "lr", "t", "beta1", "beta2", "epsilon", "wd", "rescaleGrad", "clipGrad" };

        public static NDArray FtmlUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol d, NDArrayOrSymbol v, NDArrayOrSymbol z, float lr, int t, float beta1 = 0.600000024f, float beta2 = 0.999000013f, double epsilon = 9.9999999392252903e-09, float wd = 0f, float rescaleGrad = 1f, float clipGrad = -1f)
        {
            var results = Operator.Invoke(
                "ftml_update",
                _ftmlUpdateParamNames,
                new[] { Convert(lr), Convert(t), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGrad) },
                new[] { weight, grad, d, v, z });
            return results[0];
        }

        private static string[] _adamUpdateParamNames = new[] { "lr", "beta1", "beta2", "epsilon", "wd", "rescaleGrad", "clipGradient", "lazyUpdate" };

        public static NDArray AdamUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, float lr, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true)
        {
            var results = Operator.Invoke(
                "adam_update",
                _adamUpdateParamNames,
                new[] { Convert(lr), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad, mean, var });
            return results[0];
        }

        private static string[] _nagMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescaleGrad", "clipGradient" };

        public static NDArray NagMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var results = Operator.Invoke(
                "nag_mom_update",
                _nagMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad, mom });
            return results[0];
        }

        private static string[] _mpNagMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescaleGrad", "clipGradient" };

        public static NDArray MpNagMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var results = Operator.Invoke(
                "mp_nag_mom_update",
                _mpNagMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad, mom, weight32 });
            return results[0];
        }

        private static string[] _rmspropUpdateParamNames = new[] { "lr", "gamma1", "epsilon", "wd", "rescaleGrad", "clipGradient", "clipWeights" };

        public static NDArray RmspropUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol n, float lr, float gamma1 = 0.949999988f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f)
        {
            var results = Operator.Invoke(
                "rmsprop_update",
                _rmspropUpdateParamNames,
                new[] { Convert(lr), Convert(gamma1), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(clipWeights) },
                new[] { weight, grad, n });
            return results[0];
        }

        private static string[] _rmspropalexUpdateParamNames = new[] { "lr", "gamma1", "gamma2", "epsilon", "wd", "rescaleGrad", "clipGradient", "clipWeights" };

        public static NDArray RmspropalexUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol n, NDArrayOrSymbol g, NDArrayOrSymbol delta, float lr, float gamma1 = 0.949999988f, float gamma2 = 0.899999976f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f)
        {
            var results = Operator.Invoke(
                "rmspropalex_update",
                _rmspropalexUpdateParamNames,
                new[] { Convert(lr), Convert(gamma1), Convert(gamma2), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(clipWeights) },
                new[] { weight, grad, n, g, delta });
            return results[0];
        }

        private static string[] _ftrlUpdateParamNames = new[] { "lr", "lamda1", "beta", "wd", "rescaleGrad", "clipGradient" };

        public static NDArray FtrlUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol z, NDArrayOrSymbol n, float lr, float lamda1 = 0.00999999978f, float beta = 1f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var results = Operator.Invoke(
                "ftrl_update",
                _ftrlUpdateParamNames,
                new[] { Convert(lr), Convert(lamda1), Convert(beta), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad, z, n });
            return results[0];
        }

        private static string[] _sparseAdagradUpdateParamNames = new[] { "lr", "epsilon", "wd", "rescaleGrad", "clipGradient" };

        public static NDArray SparseAdagradUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol history, float lr, float epsilon = 1.00000001e-07f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f)
        {
            var results = Operator.Invoke(
                "_sparse_adagrad_update",
                _sparseAdagradUpdateParamNames,
                new[] { Convert(lr), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad, history });
            return results[0];
        }

        private static string[] _PadParamNames = new[] { "mode", "padWidth", "constantValue" };

        public static NDArray Pad(NDArrayOrSymbol data, PadMode mode, NDShape padWidth, double constantValue = 0)
        {
            var results = Operator.Invoke(
                "Pad",
                _PadParamNames,
                new[] { Convert((int)mode), Convert(padWidth), Convert(constantValue) },
                new[] { data });
            return results[0];
        }

        private static string[] _FlattenParamNames = _empty;

        public static NDArray Flatten(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "Flatten",
                _FlattenParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _sampleUniformParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleUniform(NDArrayOrSymbol low, NDArrayOrSymbol high, NDShape shape = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_sample_uniform",
                _sampleUniformParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { low, high });
            return results[0];
        }

        private static string[] _sampleNormalParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleNormal(NDArrayOrSymbol mu, NDArrayOrSymbol sigma, NDShape shape = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_sample_normal",
                _sampleNormalParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { mu, sigma });
            return results[0];
        }

        private static string[] _sampleGammaParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleGamma(NDArrayOrSymbol alpha, NDArrayOrSymbol beta, NDShape shape = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_sample_gamma",
                _sampleGammaParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { alpha, beta });
            return results[0];
        }

        private static string[] _sampleExponentialParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleExponential(NDArrayOrSymbol lam, NDShape shape = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_sample_exponential",
                _sampleExponentialParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { lam });
            return results[0];
        }

        private static string[] _samplePoissonParamNames = new[] { "shape", "dtype" };

        public static NDArray SamplePoisson(NDArrayOrSymbol lam, NDShape shape = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_sample_poisson",
                _samplePoissonParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { lam });
            return results[0];
        }

        private static string[] _sampleNegativeBinomialParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleNegativeBinomial(NDArrayOrSymbol k, NDArrayOrSymbol p, NDShape shape = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_sample_negative_binomial",
                _sampleNegativeBinomialParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { k, p });
            return results[0];
        }

        private static string[] _sampleGeneralizedNegativeBinomialParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleGeneralizedNegativeBinomial(NDArrayOrSymbol mu, NDArrayOrSymbol alpha, NDShape shape = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_sample_generalized_negative_binomial",
                _sampleGeneralizedNegativeBinomialParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { mu, alpha });
            return results[0];
        }

        private static string[] _sampleMultinomialParamNames = new[] { "shape", "getProb", "dtype" };

        public static NDArray SampleMultinomial(NDArrayOrSymbol data, NDShape shape = null, bool getProb = false, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_sample_multinomial",
                _sampleMultinomialParamNames,
                new[] { Convert(shape), Convert(getProb), Convert(dtype) },
                new[] { data });
            return results[0];
        }

        private static string[] _randomUniformParamNames = new[] { "low", "high", "shape", "ctx", "dtype" };

        public static NDArray RandomUniform(float low = 0f, float high = 1f, NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_random_uniform",
                _randomUniformParamNames,
                new[] { Convert(low), Convert(high), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _randomNormalParamNames = new[] { "loc", "scale", "shape", "ctx", "dtype" };

        public static NDArray RandomNormal(float loc = 0f, float scale = 1f, NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_random_normal",
                _randomNormalParamNames,
                new[] { Convert(loc), Convert(scale), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _randomGammaParamNames = new[] { "alpha", "beta", "shape", "ctx", "dtype" };

        public static NDArray RandomGamma(float alpha = 1f, float beta = 1f, NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_random_gamma",
                _randomGammaParamNames,
                new[] { Convert(alpha), Convert(beta), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _randomExponentialParamNames = new[] { "lam", "shape", "ctx", "dtype" };

        public static NDArray RandomExponential(float lam = 1f, NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_random_exponential",
                _randomExponentialParamNames,
                new[] { Convert(lam), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _randomPoissonParamNames = new[] { "lam", "shape", "ctx", "dtype" };

        public static NDArray RandomPoisson(float lam = 1f, NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_random_poisson",
                _randomPoissonParamNames,
                new[] { Convert(lam), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _randomNegativeBinomialParamNames = new[] { "k", "p", "shape", "ctx", "dtype" };

        public static NDArray RandomNegativeBinomial(int k = 1, float p = 1f, NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_random_negative_binomial",
                _randomNegativeBinomialParamNames,
                new[] { Convert(k), Convert(p), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _randomGeneralizedNegativeBinomialParamNames = new[] { "mu", "alpha", "shape", "ctx", "dtype" };

        public static NDArray RandomGeneralizedNegativeBinomial(float mu = 1f, float alpha = 1f, NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_random_generalized_negative_binomial",
                _randomGeneralizedNegativeBinomialParamNames,
                new[] { Convert(mu), Convert(alpha), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _randomRandintParamNames = new[] { "low", "high", "shape", "ctx", "dtype" };

        public static NDArray RandomRandint(Tuple<double> low, Tuple<double> high, NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_random_randint",
                _randomRandintParamNames,
                new[] { Convert(low), Convert(high), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _randomUniformLikeParamNames = new[] { "low", "high" };

        public static NDArray RandomUniformLike(NDArrayOrSymbol data, float low = 0f, float high = 1f)
        {
            var results = Operator.Invoke(
                "_random_uniform_like",
                _randomUniformLikeParamNames,
                new[] { Convert(low), Convert(high) },
                new[] { data });
            return results[0];
        }

        private static string[] _randomNormalLikeParamNames = new[] { "loc", "scale" };

        public static NDArray RandomNormalLike(NDArrayOrSymbol data, float loc = 0f, float scale = 1f)
        {
            var results = Operator.Invoke(
                "_random_normal_like",
                _randomNormalLikeParamNames,
                new[] { Convert(loc), Convert(scale) },
                new[] { data });
            return results[0];
        }

        private static string[] _randomGammaLikeParamNames = new[] { "alpha", "beta" };

        public static NDArray RandomGammaLike(NDArrayOrSymbol data, float alpha = 1f, float beta = 1f)
        {
            var results = Operator.Invoke(
                "_random_gamma_like",
                _randomGammaLikeParamNames,
                new[] { Convert(alpha), Convert(beta) },
                new[] { data });
            return results[0];
        }

        private static string[] _randomExponentialLikeParamNames = new[] { "lam" };

        public static NDArray RandomExponentialLike(NDArrayOrSymbol data, float lam = 1f)
        {
            var results = Operator.Invoke(
                "_random_exponential_like",
                _randomExponentialLikeParamNames,
                new[] { Convert(lam) },
                new[] { data });
            return results[0];
        }

        private static string[] _randomPoissonLikeParamNames = new[] { "lam" };

        public static NDArray RandomPoissonLike(NDArrayOrSymbol data, float lam = 1f)
        {
            var results = Operator.Invoke(
                "_random_poisson_like",
                _randomPoissonLikeParamNames,
                new[] { Convert(lam) },
                new[] { data });
            return results[0];
        }

        private static string[] _randomNegativeBinomialLikeParamNames = new[] { "k", "p" };

        public static NDArray RandomNegativeBinomialLike(NDArrayOrSymbol data, int k = 1, float p = 1f)
        {
            var results = Operator.Invoke(
                "_random_negative_binomial_like",
                _randomNegativeBinomialLikeParamNames,
                new[] { Convert(k), Convert(p) },
                new[] { data });
            return results[0];
        }

        private static string[] _randomGeneralizedNegativeBinomialLikeParamNames = new[] { "mu", "alpha" };

        public static NDArray RandomGeneralizedNegativeBinomialLike(NDArrayOrSymbol data, float mu = 1f, float alpha = 1f)
        {
            var results = Operator.Invoke(
                "_random_generalized_negative_binomial_like",
                _randomGeneralizedNegativeBinomialLikeParamNames,
                new[] { Convert(mu), Convert(alpha) },
                new[] { data });
            return results[0];
        }

        private static string[] _shuffleParamNames = _empty;

        public static NDArray Shuffle(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "_shuffle",
                _shuffleParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _sampleUniqueZipfianParamNames = new[] { "rangeMax", "shape" };

        public static NDArray SampleUniqueZipfian(int rangeMax, NDShape shape = null)
        {
            var results = Operator.Invoke(
                "_sample_unique_zipfian",
                _sampleUniqueZipfianParamNames,
                new[] { Convert(rangeMax), Convert(shape) },
                _emptyInput);
            return results[0];
        }

        private static string[] _LinearRegressionOutputParamNames = new[] { "gradScale" };

        public static NDArray LinearRegressionOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float gradScale = 1f)
        {
            var results = Operator.Invoke(
                "LinearRegressionOutput",
                _LinearRegressionOutputParamNames,
                new[] { Convert(gradScale) },
                new[] { data, label });
            return results[0];
        }

        private static string[] _MAERegressionOutputParamNames = new[] { "gradScale" };

        public static NDArray MAERegressionOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float gradScale = 1f)
        {
            var results = Operator.Invoke(
                "MAERegressionOutput",
                _MAERegressionOutputParamNames,
                new[] { Convert(gradScale) },
                new[] { data, label });
            return results[0];
        }

        private static string[] _LogisticRegressionOutputParamNames = new[] { "gradScale" };

        public static NDArray LogisticRegressionOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float gradScale = 1f)
        {
            var results = Operator.Invoke(
                "LogisticRegressionOutput",
                _LogisticRegressionOutputParamNames,
                new[] { Convert(gradScale) },
                new[] { data, label });
            return results[0];
        }

        private static string[] _RNNParamNames = new[] { "stateSize", "numLayers", "mode", "bidirectional", "p", "stateOutputs", "projectionSize", "lstmStateClipMin", "lstmStateClipMax", "lstmStateClipNan", "useSequenceLength" };

        public static NDArray RNN(NDArrayOrSymbol data, NDArrayOrSymbol parameters, NDArrayOrSymbol state, NDArrayOrSymbol stateCell, NDArrayOrSymbol sequenceLength, uint stateSize, uint numLayers, RNNMode mode, bool bidirectional = false, float p = 0f, bool stateOutputs = false, int? projectionSize = null, double? lstmStateClipMin = null, double? lstmStateClipMax = null, bool lstmStateClipNan = false, bool useSequenceLength = false)
        {
            var results = Operator.Invoke(
                "RNN",
                _RNNParamNames,
                new[] { Convert(stateSize), Convert(numLayers), Convert((int)mode), Convert(bidirectional), Convert(p), Convert(stateOutputs), Convert(projectionSize), Convert(lstmStateClipMin), Convert(lstmStateClipMax), Convert(lstmStateClipNan), Convert(useSequenceLength) },
                new[] { data, parameters, state, stateCell, sequenceLength });
            return results[0];
        }

        private static string[] _SliceChannelParamNames = new[] { "numOutputs", "axis", "squeezeAxis" };

        public static NDArray SliceChannel(NDArrayOrSymbol data, int numOutputs, int axis = 1, bool squeezeAxis = false)
        {
            var results = Operator.Invoke(
                "SliceChannel",
                _SliceChannelParamNames,
                new[] { Convert(numOutputs), Convert(axis), Convert(squeezeAxis) },
                new[] { data });
            return results[0];
        }

        private static string[] _SoftmaxOutputParamNames = new[] { "gradScale", "ignoreLabel", "multiOutput", "useIgnore", "preserveShape", "normalization", "outGrad", "smoothAlpha" };

        public static NDArray SoftmaxOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float gradScale = 1f, float ignoreLabel = -1f, bool multiOutput = false, bool useIgnore = false, bool preserveShape = false, SoftmaxoutputNormalization normalization = SoftmaxoutputNormalization.Null, bool outGrad = false, float smoothAlpha = 0f)
        {
            var results = Operator.Invoke(
                "SoftmaxOutput",
                _SoftmaxOutputParamNames,
                new[] { Convert(gradScale), Convert(ignoreLabel), Convert(multiOutput), Convert(useIgnore), Convert(preserveShape), Convert((int)normalization), Convert(outGrad), Convert(smoothAlpha) },
                new[] { data, label });
            return results[0];
        }

        private static string[] _sgMkldnnConvParamNames = _empty;

        public static NDArray SgMkldnnConv()
        {
            var results = Operator.Invoke(
                "_sg_mkldnn_conv",
                _sgMkldnnConvParamNames,
                _empty,
                _emptyInput);
            return results[0];
        }

        private static string[] _sgMkldnnFullyConnectedParamNames = _empty;

        public static NDArray SgMkldnnFullyConnected()
        {
            var results = Operator.Invoke(
                "_sg_mkldnn_fully_connected",
                _sgMkldnnFullyConnectedParamNames,
                _empty,
                _emptyInput);
            return results[0];
        }

        private static string[] _SwapAxisParamNames = new[] { "dim1", "dim2" };

        public static NDArray SwapAxis(NDArrayOrSymbol data, uint dim1 = 0, uint dim2 = 0)
        {
            var results = Operator.Invoke(
                "SwapAxis",
                _SwapAxisParamNames,
                new[] { Convert(dim1), Convert(dim2) },
                new[] { data });
            return results[0];
        }

        private static string[] _ampCastParamNames = new[] { "dtype" };

        public static NDArray AmpCast(NDArrayOrSymbol data, DType dtype)
        {
            var results = Operator.Invoke(
                "amp_cast",
                _ampCastParamNames,
                new[] { Convert(dtype) },
                new[] { data });
            return results[0];
        }

        private static string[] _argmaxParamNames = new[] { "axis", "keepdims" };

        public static NDArray Argmax(NDArrayOrSymbol data, int? axis = null, bool keepdims = false)
        {
            var results = Operator.Invoke(
                "argmax",
                _argmaxParamNames,
                new[] { Convert(axis), Convert(keepdims) },
                new[] { data });
            return results[0];
        }

        private static string[] _argminParamNames = new[] { "axis", "keepdims" };

        public static NDArray Argmin(NDArrayOrSymbol data, int? axis = null, bool keepdims = false)
        {
            var results = Operator.Invoke(
                "argmin",
                _argminParamNames,
                new[] { Convert(axis), Convert(keepdims) },
                new[] { data });
            return results[0];
        }

        private static string[] _argmaxChannelParamNames = _empty;

        public static NDArray ArgmaxChannel(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "argmax_channel",
                _argmaxChannelParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _pickParamNames = new[] { "axis", "keepdims", "mode" };

        public static NDArray Pick(NDArrayOrSymbol data, NDArrayOrSymbol index, int? axis = -1, bool keepdims = false, PickMode mode = PickMode.Clip)
        {
            var results = Operator.Invoke(
                "pick",
                _pickParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert((int)mode) },
                new[] { data, index });
            return results[0];
        }

        private static string[] _sumParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Sum(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var results = Operator.Invoke(
                "sum",
                _sumParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data });
            return results[0];
        }

        private static string[] _meanParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Mean(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var results = Operator.Invoke(
                "mean",
                _meanParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data });
            return results[0];
        }

        private static string[] _prodParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Prod(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var results = Operator.Invoke(
                "prod",
                _prodParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data });
            return results[0];
        }

        private static string[] _nansumParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Nansum(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var results = Operator.Invoke(
                "nansum",
                _nansumParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data });
            return results[0];
        }

        private static string[] _nanprodParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Nanprod(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var results = Operator.Invoke(
                "nanprod",
                _nanprodParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data });
            return results[0];
        }

        private static string[] _maxParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Max(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var results = Operator.Invoke(
                "max",
                _maxParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data });
            return results[0];
        }

        private static string[] _minParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Min(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var results = Operator.Invoke(
                "min",
                _minParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data });
            return results[0];
        }

        private static string[] _broadcastAxisParamNames = new[] { "axis", "size" };

        public static NDArray BroadcastAxis(NDArrayOrSymbol data, NDShape axis = null, NDShape size = null)
        {
            var results = Operator.Invoke(
                "broadcast_axis",
                _broadcastAxisParamNames,
                new[] { Convert(axis), Convert(size) },
                new[] { data });
            return results[0];
        }

        private static string[] _broadcastToParamNames = new[] { "shape" };

        public static NDArray BroadcastTo(NDArrayOrSymbol data, NDShape shape = null)
        {
            var results = Operator.Invoke(
                "broadcast_to",
                _broadcastToParamNames,
                new[] { Convert(shape) },
                new[] { data });
            return results[0];
        }

        private static string[] _broadcastLikeParamNames = new[] { "lhsAxes", "rhsAxes" };

        public static NDArray BroadcastLike(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDShape lhsAxes = null, NDShape rhsAxes = null)
        {
            var results = Operator.Invoke(
                "broadcast_like",
                _broadcastLikeParamNames,
                new[] { Convert(lhsAxes), Convert(rhsAxes) },
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _normParamNames = new[] { "ord", "axis", "outDtype", "keepdims" };

        public static NDArray Norm(NDArrayOrSymbol data, int ord = 2, NDShape axis = null, NormOutDtype? outDtype = null, bool keepdims = false)
        {
            var results = Operator.Invoke(
                "norm",
                _normParamNames,
                new[] { Convert(ord), Convert(axis), Convert((int)outDtype), Convert(keepdims) },
                new[] { data });
            return results[0];
        }

        private static string[] _castStorageParamNames = new[] { "stype" };

        public static NDArray CastStorage(NDArrayOrSymbol data, CastStorageStype stype)
        {
            var results = Operator.Invoke(
                "cast_storage",
                _castStorageParamNames,
                new[] { Convert((int)stype) },
                new[] { data });
            return results[0];
        }

        private static string[] _whereParamNames = _empty;

        public static NDArray Where(NDArrayOrSymbol condition, NDArrayOrSymbol x, NDArrayOrSymbol y)
        {
            var results = Operator.Invoke(
                "where",
                _whereParamNames,
                _empty,
                new[] { condition, x, y });
            return results[0];
        }

        private static string[] _diagParamNames = new[] { "k", "axis1", "axis2" };

        public static NDArray Diag(NDArrayOrSymbol data, int k = 0, int axis1 = 0, int axis2 = 1)
        {
            var results = Operator.Invoke(
                "diag",
                _diagParamNames,
                new[] { Convert(k), Convert(axis1), Convert(axis2) },
                new[] { data });
            return results[0];
        }

        private static string[] _dotParamNames = new[] { "transposeA", "transposeB", "forwardStype" };

        public static NDArray Dot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, DotForwardStype? forwardStype = null)
        {
            var results = Operator.Invoke(
                "dot",
                _dotParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert((int)forwardStype) },
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _batchDotParamNames = new[] { "transposeA", "transposeB", "forwardStype" };

        public static NDArray BatchDot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, BatchDotForwardStype? forwardStype = null)
        {
            var results = Operator.Invoke(
                "batch_dot",
                _batchDotParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert((int)forwardStype) },
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastAddParamNames = _empty;

        public static NDArray BroadcastAdd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_add",
                _broadcastAddParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastSubParamNames = _empty;

        public static NDArray BroadcastSub(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_sub",
                _broadcastSubParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastMulParamNames = _empty;

        public static NDArray BroadcastMul(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_mul",
                _broadcastMulParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastDivParamNames = _empty;

        public static NDArray BroadcastDiv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_div",
                _broadcastDivParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastModParamNames = _empty;

        public static NDArray BroadcastMod(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_mod",
                _broadcastModParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastPowerParamNames = _empty;

        public static NDArray BroadcastPower(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_power",
                _broadcastPowerParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastMaximumParamNames = _empty;

        public static NDArray BroadcastMaximum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_maximum",
                _broadcastMaximumParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastMinimumParamNames = _empty;

        public static NDArray BroadcastMinimum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_minimum",
                _broadcastMinimumParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastHypotParamNames = _empty;

        public static NDArray BroadcastHypot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_hypot",
                _broadcastHypotParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastEqualParamNames = _empty;

        public static NDArray BroadcastEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_equal",
                _broadcastEqualParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastNotEqualParamNames = _empty;

        public static NDArray BroadcastNotEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_not_equal",
                _broadcastNotEqualParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastGreaterParamNames = _empty;

        public static NDArray BroadcastGreater(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_greater",
                _broadcastGreaterParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastGreaterEqualParamNames = _empty;

        public static NDArray BroadcastGreaterEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_greater_equal",
                _broadcastGreaterEqualParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastLesserParamNames = _empty;

        public static NDArray BroadcastLesser(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_lesser",
                _broadcastLesserParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastLesserEqualParamNames = _empty;

        public static NDArray BroadcastLesserEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_lesser_equal",
                _broadcastLesserEqualParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastLogicalAndParamNames = _empty;

        public static NDArray BroadcastLogicalAnd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_logical_and",
                _broadcastLogicalAndParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastLogicalOrParamNames = _empty;

        public static NDArray BroadcastLogicalOr(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_logical_or",
                _broadcastLogicalOrParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _broadcastLogicalXorParamNames = _empty;

        public static NDArray BroadcastLogicalXor(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "broadcast_logical_xor",
                _broadcastLogicalXorParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _elemwiseAddParamNames = _empty;

        public static NDArray ElemwiseAdd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "elemwise_add",
                _elemwiseAddParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _gradAddParamNames = _empty;

        public static NDArray GradAdd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_grad_add",
                _gradAddParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _elemwiseSubParamNames = _empty;

        public static NDArray ElemwiseSub(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "elemwise_sub",
                _elemwiseSubParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _elemwiseMulParamNames = _empty;

        public static NDArray ElemwiseMul(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "elemwise_mul",
                _elemwiseMulParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _elemwiseDivParamNames = _empty;

        public static NDArray ElemwiseDiv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "elemwise_div",
                _elemwiseDivParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _modParamNames = _empty;

        public static NDArray Mod(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_mod",
                _modParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _powerParamNames = _empty;

        public static NDArray Power(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_power",
                _powerParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _maximumParamNames = _empty;

        public static NDArray Maximum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_maximum",
                _maximumParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _minimumParamNames = _empty;

        public static NDArray Minimum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_minimum",
                _minimumParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _hypotParamNames = _empty;

        public static NDArray Hypot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_hypot",
                _hypotParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _equalParamNames = _empty;

        public static NDArray Equal(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_equal",
                _equalParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _notEqualParamNames = _empty;

        public static NDArray NotEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_not_equal",
                _notEqualParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _greaterParamNames = _empty;

        public static NDArray Greater(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_greater",
                _greaterParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _greaterEqualParamNames = _empty;

        public static NDArray GreaterEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_greater_equal",
                _greaterEqualParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _lesserParamNames = _empty;

        public static NDArray Lesser(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_lesser",
                _lesserParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _lesserEqualParamNames = _empty;

        public static NDArray LesserEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_lesser_equal",
                _lesserEqualParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _logicalAndParamNames = _empty;

        public static NDArray LogicalAnd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_logical_and",
                _logicalAndParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _logicalOrParamNames = _empty;

        public static NDArray LogicalOr(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_logical_or",
                _logicalOrParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _logicalXorParamNames = _empty;

        public static NDArray LogicalXor(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_logical_xor",
                _logicalXorParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _plusScalarParamNames = new[] { "scalar" };

        public static NDArray PlusScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_plus_scalar",
                _plusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _minusScalarParamNames = new[] { "scalar" };

        public static NDArray MinusScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_minus_scalar",
                _minusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _rminusScalarParamNames = new[] { "scalar" };

        public static NDArray RminusScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_rminus_scalar",
                _rminusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _mulScalarParamNames = new[] { "scalar" };

        public static NDArray MulScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_mul_scalar",
                _mulScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _divScalarParamNames = new[] { "scalar" };

        public static NDArray DivScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_div_scalar",
                _divScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _rdivScalarParamNames = new[] { "scalar" };

        public static NDArray RdivScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_rdiv_scalar",
                _rdivScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _modScalarParamNames = new[] { "scalar" };

        public static NDArray ModScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_mod_scalar",
                _modScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _rmodScalarParamNames = new[] { "scalar" };

        public static NDArray RmodScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_rmod_scalar",
                _rmodScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _maximumScalarParamNames = new[] { "scalar" };

        public static NDArray MaximumScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_maximum_scalar",
                _maximumScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _minimumScalarParamNames = new[] { "scalar" };

        public static NDArray MinimumScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_minimum_scalar",
                _minimumScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _powerScalarParamNames = new[] { "scalar" };

        public static NDArray PowerScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_power_scalar",
                _powerScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _rpowerScalarParamNames = new[] { "scalar" };

        public static NDArray RpowerScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_rpower_scalar",
                _rpowerScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _hypotScalarParamNames = new[] { "scalar" };

        public static NDArray HypotScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_hypot_scalar",
                _hypotScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _smoothL1ParamNames = new[] { "scalar" };

        public static NDArray SmoothL1(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "smooth_l1",
                _smoothL1ParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _equalScalarParamNames = new[] { "scalar" };

        public static NDArray EqualScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_equal_scalar",
                _equalScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _notEqualScalarParamNames = new[] { "scalar" };

        public static NDArray NotEqualScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_not_equal_scalar",
                _notEqualScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _greaterScalarParamNames = new[] { "scalar" };

        public static NDArray GreaterScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_greater_scalar",
                _greaterScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _greaterEqualScalarParamNames = new[] { "scalar" };

        public static NDArray GreaterEqualScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_greater_equal_scalar",
                _greaterEqualScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _lesserScalarParamNames = new[] { "scalar" };

        public static NDArray LesserScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_lesser_scalar",
                _lesserScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _lesserEqualScalarParamNames = new[] { "scalar" };

        public static NDArray LesserEqualScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_lesser_equal_scalar",
                _lesserEqualScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _logicalAndScalarParamNames = new[] { "scalar" };

        public static NDArray LogicalAndScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_logical_and_scalar",
                _logicalAndScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _logicalOrScalarParamNames = new[] { "scalar" };

        public static NDArray LogicalOrScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_logical_or_scalar",
                _logicalOrScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _logicalXorScalarParamNames = new[] { "scalar" };

        public static NDArray LogicalXorScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_logical_xor_scalar",
                _logicalXorScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _scatterElemwiseDivParamNames = _empty;

        public static NDArray ScatterElemwiseDiv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_scatter_elemwise_div",
                _scatterElemwiseDivParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _scatterPlusScalarParamNames = new[] { "scalar" };

        public static NDArray ScatterPlusScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_scatter_plus_scalar",
                _scatterPlusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _scatterMinusScalarParamNames = new[] { "scalar" };

        public static NDArray ScatterMinusScalar(NDArrayOrSymbol data, float scalar)
        {
            var results = Operator.Invoke(
                "_scatter_minus_scalar",
                _scatterMinusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data });
            return results[0];
        }

        private static string[] _reluParamNames = _empty;

        public static NDArray Relu(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "relu",
                _reluParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _sigmoidParamNames = _empty;

        public static NDArray Sigmoid(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "sigmoid",
                _sigmoidParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _hardSigmoidParamNames = new[] { "alpha", "beta" };

        public static NDArray HardSigmoid(NDArrayOrSymbol data, float alpha = 0.200000003f, float beta = 0.5f)
        {
            var results = Operator.Invoke(
                "hard_sigmoid",
                _hardSigmoidParamNames,
                new[] { Convert(alpha), Convert(beta) },
                new[] { data });
            return results[0];
        }

        private static string[] _softsignParamNames = _empty;

        public static NDArray Softsign(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "softsign",
                _softsignParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _copyParamNames = _empty;

        public static NDArray Copy(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "_copy",
                _copyParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _BlockGradParamNames = _empty;

        public static NDArray BlockGrad(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "BlockGrad",
                _BlockGradParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _makeLossParamNames = _empty;

        public static NDArray MakeLoss(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "make_loss",
                _makeLossParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _identityWithAttrLikeRhsParamNames = _empty;

        public static NDArray IdentityWithAttrLikeRhs(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "_identity_with_attr_like_rhs",
                _identityWithAttrLikeRhsParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _reshapeLikeParamNames = _empty;

        public static NDArray ReshapeLike(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs)
        {
            var results = Operator.Invoke(
                "reshape_like",
                _reshapeLikeParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _shapeArrayParamNames = new[] { "lhsBegin", "lhsEnd", "rhsBegin", "rhsEnd" };

        public static NDArray ShapeArray(NDArrayOrSymbol data, int? lhsBegin = null, int? lhsEnd = null, int? rhsBegin = null, int? rhsEnd = null)
        {
            var results = Operator.Invoke(
                "shape_array",
                _shapeArrayParamNames,
                new[] { Convert(lhsBegin), Convert(lhsEnd), Convert(rhsBegin), Convert(rhsEnd) },
                new[] { data });
            return results[0];
        }

        private static string[] _sizeArrayParamNames = _empty;

        public static NDArray SizeArray(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "size_array",
                _sizeArrayParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _CastParamNames = new[] { "dtype" };

        public static NDArray Cast(NDArrayOrSymbol data, DType dtype)
        {
            var results = Operator.Invoke(
                "Cast",
                _CastParamNames,
                new[] { Convert(dtype) },
                new[] { data });
            return results[0];
        }

        private static string[] _negativeParamNames = _empty;

        public static NDArray Negative(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "negative",
                _negativeParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _reciprocalParamNames = _empty;

        public static NDArray Reciprocal(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "reciprocal",
                _reciprocalParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _absParamNames = _empty;

        public static NDArray Abs(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "abs",
                _absParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _signParamNames = _empty;

        public static NDArray Sign(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "sign",
                _signParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _roundParamNames = _empty;

        public static NDArray Round(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "round",
                _roundParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _rintParamNames = _empty;

        public static NDArray Rint(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "rint",
                _rintParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _ceilParamNames = _empty;

        public static NDArray Ceil(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "ceil",
                _ceilParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _floorParamNames = _empty;

        public static NDArray Floor(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "floor",
                _floorParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _truncParamNames = _empty;

        public static NDArray Trunc(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "trunc",
                _truncParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _fixParamNames = _empty;

        public static NDArray Fix(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "fix",
                _fixParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _squareParamNames = _empty;

        public static NDArray Square(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "square",
                _squareParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _sqrtParamNames = _empty;

        public static NDArray Sqrt(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "sqrt",
                _sqrtParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _rsqrtParamNames = _empty;

        public static NDArray Rsqrt(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "rsqrt",
                _rsqrtParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _cbrtParamNames = _empty;

        public static NDArray Cbrt(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "cbrt",
                _cbrtParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _erfParamNames = _empty;

        public static NDArray Erf(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "erf",
                _erfParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _erfinvParamNames = _empty;

        public static NDArray Erfinv(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "erfinv",
                _erfinvParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _rcbrtParamNames = _empty;

        public static NDArray Rcbrt(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "rcbrt",
                _rcbrtParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _expParamNames = _empty;

        public static NDArray Exp(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "exp",
                _expParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _logParamNames = _empty;

        public static NDArray Log(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "log",
                _logParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _log10ParamNames = _empty;

        public static NDArray Log10(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "log10",
                _log10ParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _log2ParamNames = _empty;

        public static NDArray Log2(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "log2",
                _log2ParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _log1pParamNames = _empty;

        public static NDArray Log1p(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "log1p",
                _log1pParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _expm1ParamNames = _empty;

        public static NDArray Expm1(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "expm1",
                _expm1ParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _gammaParamNames = _empty;

        public static NDArray Gamma(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "gamma",
                _gammaParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _gammalnParamNames = _empty;

        public static NDArray Gammaln(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "gammaln",
                _gammalnParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _logicalNotParamNames = _empty;

        public static NDArray LogicalNot(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "logical_not",
                _logicalNotParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _sinParamNames = _empty;

        public static NDArray Sin(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "sin",
                _sinParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _cosParamNames = _empty;

        public static NDArray Cos(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "cos",
                _cosParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _tanParamNames = _empty;

        public static NDArray Tan(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "tan",
                _tanParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _arcsinParamNames = _empty;

        public static NDArray Arcsin(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "arcsin",
                _arcsinParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _arccosParamNames = _empty;

        public static NDArray Arccos(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "arccos",
                _arccosParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _arctanParamNames = _empty;

        public static NDArray Arctan(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "arctan",
                _arctanParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _degreesParamNames = _empty;

        public static NDArray Degrees(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "degrees",
                _degreesParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _radiansParamNames = _empty;

        public static NDArray Radians(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "radians",
                _radiansParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _sinhParamNames = _empty;

        public static NDArray Sinh(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "sinh",
                _sinhParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _coshParamNames = _empty;

        public static NDArray Cosh(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "cosh",
                _coshParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _tanhParamNames = _empty;

        public static NDArray Tanh(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "tanh",
                _tanhParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _arcsinhParamNames = _empty;

        public static NDArray Arcsinh(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "arcsinh",
                _arcsinhParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _arccoshParamNames = _empty;

        public static NDArray Arccosh(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "arccosh",
                _arccoshParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _arctanhParamNames = _empty;

        public static NDArray Arctanh(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "arctanh",
                _arctanhParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _histogramParamNames = new[] { "binCnt", "range" };

        public static NDArray Histogram(NDArrayOrSymbol data, NDArrayOrSymbol bins, int? binCnt = null, Tuple<double> range = null)
        {
            var results = Operator.Invoke(
                "_histogram",
                _histogramParamNames,
                new[] { Convert(binCnt), Convert(range) },
                new[] { data, bins });
            return results[0];
        }

        private static string[] _EmbeddingParamNames = new[] { "inputDim", "outputDim", "dtype", "sparseGrad" };

        public static NDArray Embedding(NDArrayOrSymbol data, NDArrayOrSymbol weight, int inputDim, int outputDim, DType dtype = null, bool sparseGrad = false)
        {
            var results = Operator.Invoke(
                "Embedding",
                _EmbeddingParamNames,
                new[] { Convert(inputDim), Convert(outputDim), Convert(dtype), Convert(sparseGrad) },
                new[] { data, weight });
            return results[0];
        }

        private static string[] _takeParamNames = new[] { "axis", "mode" };

        public static NDArray Take(NDArrayOrSymbol a, NDArrayOrSymbol indices, int axis = 0, TakeMode mode = TakeMode.Clip)
        {
            var results = Operator.Invoke(
                "take",
                _takeParamNames,
                new[] { Convert(axis), Convert((int)mode) },
                new[] { a, indices });
            return results[0];
        }

        private static string[] _batchTakeParamNames = _empty;

        public static NDArray BatchTake(NDArrayOrSymbol a, NDArrayOrSymbol indices)
        {
            var results = Operator.Invoke(
                "batch_take",
                _batchTakeParamNames,
                _empty,
                new[] { a, indices });
            return results[0];
        }

        private static string[] _oneHotParamNames = new[] { "depth", "onValue", "offValue", "dtype" };

        public static NDArray OneHot(NDArrayOrSymbol indices, int depth, double onValue = 1, double offValue = 0, DType dtype = null)
        {
            var results = Operator.Invoke(
                "one_hot",
                _oneHotParamNames,
                new[] { Convert(depth), Convert(onValue), Convert(offValue), Convert(dtype) },
                new[] { indices });
            return results[0];
        }

        private static string[] _gatherNdParamNames = _empty;

        public static NDArray GatherNd(NDArrayOrSymbol data, NDArrayOrSymbol indices)
        {
            var results = Operator.Invoke(
                "gather_nd",
                _gatherNdParamNames,
                _empty,
                new[] { data, indices });
            return results[0];
        }

        private static string[] _scatterNdParamNames = new[] { "shape" };

        public static NDArray ScatterNd(NDArrayOrSymbol data, NDArrayOrSymbol indices, NDShape shape)
        {
            var results = Operator.Invoke(
                "scatter_nd",
                _scatterNdParamNames,
                new[] { Convert(shape) },
                new[] { data, indices });
            return results[0];
        }

        private static string[] _scatterSetNdParamNames = new[] { "shape" };

        public static NDArray ScatterSetNd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArrayOrSymbol indices, NDShape shape)
        {
            var results = Operator.Invoke(
                "_scatter_set_nd",
                _scatterSetNdParamNames,
                new[] { Convert(shape) },
                new[] { lhs, rhs, indices });
            return results[0];
        }

        private static string[] _zerosWithoutDtypeParamNames = new[] { "shape", "ctx", "dtype" };

        public static NDArray ZerosWithoutDtype(NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_zeros_without_dtype",
                _zerosWithoutDtypeParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _zerosParamNames = new[] { "shape", "ctx", "dtype" };

        public static NDArray Zeros(NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_zeros",
                _zerosParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _eyeParamNames = new[] { "N", "M", "k", "ctx", "dtype" };

        public static NDArray Eye(Tuple<double> N, int M = 0, int k = 0, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_eye",
                _eyeParamNames,
                new[] { Convert(N), Convert(M), Convert(k), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _onesParamNames = new[] { "shape", "ctx", "dtype" };

        public static NDArray Ones(NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_ones",
                _onesParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _fullParamNames = new[] { "value", "shape", "ctx", "dtype" };

        public static NDArray Full(double value, NDShape shape = null, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_full",
                _fullParamNames,
                new[] { Convert(value), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _arangeParamNames = new[] { "start", "stop", "step", "repeat", "inferRange", "ctx", "dtype" };

        public static NDArray Arange(double start, double? stop = null, double step = 1, int repeat = 1, bool inferRange = false, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_arange",
                _arangeParamNames,
                new[] { Convert(start), Convert(stop), Convert(step), Convert(repeat), Convert(inferRange), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _linspaceParamNames = new[] { "start", "stop", "step", "repeat", "inferRange", "ctx", "dtype" };

        public static NDArray Linspace(double start, double? stop = null, double step = 1, int repeat = 1, bool inferRange = false, Context ctx = null, DType dtype = null)
        {
            var results = Operator.Invoke(
                "_linspace",
                _linspaceParamNames,
                new[] { Convert(start), Convert(stop), Convert(step), Convert(repeat), Convert(inferRange), Convert(ctx), Convert(dtype) },
                _emptyInput);
            return results[0];
        }

        private static string[] _zerosLikeParamNames = _empty;

        public static NDArray ZerosLike(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "zeros_like",
                _zerosLikeParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _onesLikeParamNames = _empty;

        public static NDArray OnesLike(NDArrayOrSymbol data)
        {
            var results = Operator.Invoke(
                "ones_like",
                _onesLikeParamNames,
                _empty,
                new[] { data });
            return results[0];
        }

        private static string[] _linalgGemmParamNames = new[] { "transposeA", "transposeB", "alpha", "beta", "axis" };

        public static NDArray LinalgGemm(NDArrayOrSymbol A, NDArrayOrSymbol B, NDArrayOrSymbol C, bool transposeA = false, bool transposeB = false, double alpha = 1, double beta = 1, int axis = -2)
        {
            var results = Operator.Invoke(
                "_linalg_gemm",
                _linalgGemmParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert(alpha), Convert(beta), Convert(axis) },
                new[] { A, B, C });
            return results[0];
        }

        private static string[] _linalgGemm2ParamNames = new[] { "transposeA", "transposeB", "alpha", "axis" };

        public static NDArray LinalgGemm2(NDArrayOrSymbol A, NDArrayOrSymbol B, bool transposeA = false, bool transposeB = false, double alpha = 1, int axis = -2)
        {
            var results = Operator.Invoke(
                "_linalg_gemm2",
                _linalgGemm2ParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert(alpha), Convert(axis) },
                new[] { A, B });
            return results[0];
        }

        private static string[] _linalgPotrfParamNames = _empty;

        public static NDArray LinalgPotrf(NDArrayOrSymbol A)
        {
            var results = Operator.Invoke(
                "_linalg_potrf",
                _linalgPotrfParamNames,
                _empty,
                new[] { A });
            return results[0];
        }

        private static string[] _linalgPotriParamNames = _empty;

        public static NDArray LinalgPotri(NDArrayOrSymbol A)
        {
            var results = Operator.Invoke(
                "_linalg_potri",
                _linalgPotriParamNames,
                _empty,
                new[] { A });
            return results[0];
        }

        private static string[] _linalgTrmmParamNames = new[] { "transpose", "rightside", "lower", "alpha" };

        public static NDArray LinalgTrmm(NDArrayOrSymbol A, NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1)
        {
            var results = Operator.Invoke(
                "_linalg_trmm",
                _linalgTrmmParamNames,
                new[] { Convert(transpose), Convert(rightside), Convert(lower), Convert(alpha) },
                new[] { A, B });
            return results[0];
        }

        private static string[] _linalgTrsmParamNames = new[] { "transpose", "rightside", "lower", "alpha" };

        public static NDArray LinalgTrsm(NDArrayOrSymbol A, NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1)
        {
            var results = Operator.Invoke(
                "_linalg_trsm",
                _linalgTrsmParamNames,
                new[] { Convert(transpose), Convert(rightside), Convert(lower), Convert(alpha) },
                new[] { A, B });
            return results[0];
        }

        private static string[] _linalgSumlogdiagParamNames = _empty;

        public static NDArray LinalgSumlogdiag(NDArrayOrSymbol A)
        {
            var results = Operator.Invoke(
                "_linalg_sumlogdiag",
                _linalgSumlogdiagParamNames,
                _empty,
                new[] { A });
            return results[0];
        }

        private static string[] _linalgExtractdiagParamNames = new[] { "offset" };

        public static NDArray LinalgExtractdiag(NDArrayOrSymbol A, int offset = 0)
        {
            var results = Operator.Invoke(
                "_linalg_extractdiag",
                _linalgExtractdiagParamNames,
                new[] { Convert(offset) },
                new[] { A });
            return results[0];
        }

        private static string[] _linalgMakediagParamNames = new[] { "offset" };

        public static NDArray LinalgMakediag(NDArrayOrSymbol A, int offset = 0)
        {
            var results = Operator.Invoke(
                "_linalg_makediag",
                _linalgMakediagParamNames,
                new[] { Convert(offset) },
                new[] { A });
            return results[0];
        }

        private static string[] _linalgExtracttrianParamNames = new[] { "offset", "lower" };

        public static NDArray LinalgExtracttrian(NDArrayOrSymbol A, int offset = 0, bool lower = true)
        {
            var results = Operator.Invoke(
                "_linalg_extracttrian",
                _linalgExtracttrianParamNames,
                new[] { Convert(offset), Convert(lower) },
                new[] { A });
            return results[0];
        }

        private static string[] _linalgMaketrianParamNames = new[] { "offset", "lower" };

        public static NDArray LinalgMaketrian(NDArrayOrSymbol A, int offset = 0, bool lower = true)
        {
            var results = Operator.Invoke(
                "_linalg_maketrian",
                _linalgMaketrianParamNames,
                new[] { Convert(offset), Convert(lower) },
                new[] { A });
            return results[0];
        }

        private static string[] _linalgSyrkParamNames = new[] { "transpose", "alpha" };

        public static NDArray LinalgSyrk(NDArrayOrSymbol A, bool transpose = false, double alpha = 1)
        {
            var results = Operator.Invoke(
                "_linalg_syrk",
                _linalgSyrkParamNames,
                new[] { Convert(transpose), Convert(alpha) },
                new[] { A });
            return results[0];
        }

        private static string[] _linalgGelqfParamNames = _empty;

        public static NDArray LinalgGelqf(NDArrayOrSymbol A)
        {
            var results = Operator.Invoke(
                "_linalg_gelqf",
                _linalgGelqfParamNames,
                _empty,
                new[] { A });
            return results[0];
        }

        private static string[] _linalgSyevdParamNames = _empty;

        public static NDArray LinalgSyevd(NDArrayOrSymbol A)
        {
            var results = Operator.Invoke(
                "_linalg_syevd",
                _linalgSyevdParamNames,
                _empty,
                new[] { A });
            return results[0];
        }

        private static string[] _linalgInverseParamNames = _empty;

        public static NDArray LinalgInverse(NDArrayOrSymbol A)
        {
            var results = Operator.Invoke(
                "_linalg_inverse",
                _linalgInverseParamNames,
                _empty,
                new[] { A });
            return results[0];
        }

        private static string[] _ReshapeParamNames = new[] { "shape", "reverse" };

        public static NDArray Reshape(NDArrayOrSymbol data, NDShape shape = null, bool reverse = false)
        {
            var results = Operator.Invoke(
                "Reshape",
                _ReshapeParamNames,
                new[] { Convert(shape), Convert(reverse) },
                new[] { data });
            return results[0];
        }

        private static string[] _transposeParamNames = new[] { "axes" };

        public static NDArray Transpose(NDArrayOrSymbol data, NDShape axes = null)
        {
            var results = Operator.Invoke(
                "transpose",
                _transposeParamNames,
                new[] { Convert(axes) },
                new[] { data });
            return results[0];
        }

        private static string[] _expandDimsParamNames = new[] { "axis" };

        public static NDArray ExpandDims(NDArrayOrSymbol data, int axis)
        {
            var results = Operator.Invoke(
                "expand_dims",
                _expandDimsParamNames,
                new[] { Convert(axis) },
                new[] { data });
            return results[0];
        }

        private static string[] _sliceParamNames = new[] { "begin", "end", "step" };

        public static NDArray Slice(NDArrayOrSymbol data, NDShape begin, NDShape end, NDShape step = null)
        {
            var results = Operator.Invoke(
                "slice",
                _sliceParamNames,
                new[] { Convert(begin), Convert(end), Convert(step) },
                new[] { data });
            return results[0];
        }

        private static string[] _sliceAssignParamNames = new[] { "begin", "end", "step" };

        public static NDArray SliceAssign(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDShape begin, NDShape end, NDShape step = null)
        {
            var results = Operator.Invoke(
                "_slice_assign",
                _sliceAssignParamNames,
                new[] { Convert(begin), Convert(end), Convert(step) },
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _sliceAssignScalarParamNames = new[] { "begin", "end", "scalar", "step" };

        public static NDArray SliceAssignScalar(NDArrayOrSymbol data, NDShape begin, NDShape end, double scalar = 0, NDShape step = null)
        {
            var results = Operator.Invoke(
                "_slice_assign_scalar",
                _sliceAssignScalarParamNames,
                new[] { Convert(begin), Convert(end), Convert(scalar), Convert(step) },
                new[] { data });
            return results[0];
        }

        private static string[] _sliceAxisParamNames = new[] { "axis", "begin", "end" };

        public static NDArray SliceAxis(NDArrayOrSymbol data, int axis, int begin, int? end)
        {
            var results = Operator.Invoke(
                "slice_axis",
                _sliceAxisParamNames,
                new[] { Convert(axis), Convert(begin), Convert(end) },
                new[] { data });
            return results[0];
        }

        private static string[] _sliceLikeParamNames = new[] { "axes" };

        public static NDArray SliceLike(NDArrayOrSymbol data, NDArrayOrSymbol shapeLike, NDShape axes = null)
        {
            var results = Operator.Invoke(
                "slice_like",
                _sliceLikeParamNames,
                new[] { Convert(axes) },
                new[] { data, shapeLike });
            return results[0];
        }

        private static string[] _clipParamNames = new[] { "aMin", "aMax" };

        public static NDArray Clip(NDArrayOrSymbol data, float aMin, float aMax)
        {
            var results = Operator.Invoke(
                "clip",
                _clipParamNames,
                new[] { Convert(aMin), Convert(aMax) },
                new[] { data });
            return results[0];
        }

        private static string[] _repeatParamNames = new[] { "repeats", "axis" };

        public static NDArray Repeat(NDArrayOrSymbol data, int repeats, int? axis = null)
        {
            var results = Operator.Invoke(
                "repeat",
                _repeatParamNames,
                new[] { Convert(repeats), Convert(axis) },
                new[] { data });
            return results[0];
        }

        private static string[] _tileParamNames = new[] { "reps" };

        public static NDArray Tile(NDArrayOrSymbol data, NDShape reps)
        {
            var results = Operator.Invoke(
                "tile",
                _tileParamNames,
                new[] { Convert(reps) },
                new[] { data });
            return results[0];
        }

        private static string[] _reverseParamNames = new[] { "axis" };

        public static NDArray Reverse(NDArrayOrSymbol data, NDShape axis)
        {
            var results = Operator.Invoke(
                "reverse",
                _reverseParamNames,
                new[] { Convert(axis) },
                new[] { data });
            return results[0];
        }

        private static string[] _depthToSpaceParamNames = new[] { "blockSize" };

        public static NDArray DepthToSpace(NDArrayOrSymbol data, int blockSize)
        {
            var results = Operator.Invoke(
                "depth_to_space",
                _depthToSpaceParamNames,
                new[] { Convert(blockSize) },
                new[] { data });
            return results[0];
        }

        private static string[] _spaceToDepthParamNames = new[] { "blockSize" };

        public static NDArray SpaceToDepth(NDArrayOrSymbol data, int blockSize)
        {
            var results = Operator.Invoke(
                "space_to_depth",
                _spaceToDepthParamNames,
                new[] { Convert(blockSize) },
                new[] { data });
            return results[0];
        }

        private static string[] _splitV2ParamNames = new[] { "indices", "axis", "squeezeAxis", "sections" };

        public static NDArray SplitV2(NDArrayOrSymbol data, NDShape indices, int axis = 1, bool squeezeAxis = false, int sections = 0)
        {
            var results = Operator.Invoke(
                "_split_v2",
                _splitV2ParamNames,
                new[] { Convert(indices), Convert(axis), Convert(squeezeAxis), Convert(sections) },
                new[] { data });
            return results[0];
        }

        private static string[] _topkParamNames = new[] { "axis", "k", "retTyp", "isAscend", "dtype" };

        public static NDArray Topk(NDArrayOrSymbol data, int? axis = -1, int k = 1, TopkRetTyp retTyp = TopkRetTyp.Indices, bool isAscend = false, DType dtype = null)
        {
            var results = Operator.Invoke(
                "topk",
                _topkParamNames,
                new[] { Convert(axis), Convert(k), Convert((int)retTyp), Convert(isAscend), Convert(dtype) },
                new[] { data });
            return results[0];
        }

        private static string[] _sortParamNames = new[] { "axis", "isAscend" };

        public static NDArray Sort(NDArrayOrSymbol data, int? axis = -1, bool isAscend = true)
        {
            var results = Operator.Invoke(
                "sort",
                _sortParamNames,
                new[] { Convert(axis), Convert(isAscend) },
                new[] { data });
            return results[0];
        }

        private static string[] _argsortParamNames = new[] { "axis", "isAscend", "dtype" };

        public static NDArray Argsort(NDArrayOrSymbol data, int? axis = -1, bool isAscend = true, DType dtype = null)
        {
            var results = Operator.Invoke(
                "argsort",
                _argsortParamNames,
                new[] { Convert(axis), Convert(isAscend), Convert(dtype) },
                new[] { data });
            return results[0];
        }

        private static string[] _ravelMultiIndexParamNames = new[] { "shape" };

        public static NDArray RavelMultiIndex(NDArrayOrSymbol data, NDShape shape = null)
        {
            var results = Operator.Invoke(
                "_ravel_multi_index",
                _ravelMultiIndexParamNames,
                new[] { Convert(shape) },
                new[] { data });
            return results[0];
        }

        private static string[] _unravelIndexParamNames = new[] { "shape" };

        public static NDArray UnravelIndex(NDArrayOrSymbol data, NDShape shape = null)
        {
            var results = Operator.Invoke(
                "_unravel_index",
                _unravelIndexParamNames,
                new[] { Convert(shape) },
                new[] { data });
            return results[0];
        }

        private static string[] _sparseRetainParamNames = _empty;

        public static NDArray SparseRetain(NDArrayOrSymbol data, NDArrayOrSymbol indices)
        {
            var results = Operator.Invoke(
                "_sparse_retain",
                _sparseRetainParamNames,
                _empty,
                new[] { data, indices });
            return results[0];
        }

        private static string[] _squareSumParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray SquareSum(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false)
        {
            var results = Operator.Invoke(
                "_square_sum",
                _squareSumParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data });
            return results[0];
        }

        private static string[] _BilinearSamplerParamNames = new[] { "cudnnOff" };

        public static NDArray BilinearSampler(NDArrayOrSymbol data, NDArrayOrSymbol grid, bool? cudnnOff = null)
        {
            var results = Operator.Invoke(
                "BilinearSampler",
                _BilinearSamplerParamNames,
                new[] { Convert(cudnnOff) },
                new[] { data, grid });
            return results[0];
        }

        private static string[] _ConvolutionV1ParamNames = new[] { "kernel", "numFilter", "stride", "dilate", "pad", "numGroup", "workspace", "noBias", "cudnnTune", "cudnnOff", "layout" };

        public static NDArray ConvolutionV1(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionV1CudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionV1Layout? layout = null)
        {
            var results = Operator.Invoke(
                "Convolution_v1",
                _ConvolutionV1ParamNames,
                new[] { Convert(kernel), Convert(numFilter), Convert(stride), Convert(dilate), Convert(pad), Convert(numGroup), Convert(workspace), Convert(noBias), Convert((int)cudnnTune), Convert(cudnnOff), Convert((int)layout) },
                new[] { data, weight, bias });
            return results[0];
        }

        private static string[] _CorrelationParamNames = new[] { "kernelSize", "maxDisplacement", "stride1", "stride2", "padSize", "isMultiply" };

        public static NDArray Correlation(NDArrayOrSymbol data1, NDArrayOrSymbol data2, uint kernelSize = 1, uint maxDisplacement = 1, uint stride1 = 1, uint stride2 = 1, uint padSize = 0, bool isMultiply = true)
        {
            var results = Operator.Invoke(
                "Correlation",
                _CorrelationParamNames,
                new[] { Convert(kernelSize), Convert(maxDisplacement), Convert(stride1), Convert(stride2), Convert(padSize), Convert(isMultiply) },
                new[] { data1, data2 });
            return results[0];
        }

        private static string[] _CrossDeviceCopyParamNames = _empty;

        public static NDArray CrossDeviceCopy()
        {
            var results = Operator.Invoke(
                "_CrossDeviceCopy",
                _CrossDeviceCopyParamNames,
                _empty,
                _emptyInput);
            return results[0];
        }

        private static string[] _GridGeneratorParamNames = new[] { "transformType", "targetShape" };

        public static NDArray GridGenerator(NDArrayOrSymbol data, GridgeneratorTransformType transformType, NDShape targetShape = null)
        {
            var results = Operator.Invoke(
                "GridGenerator",
                _GridGeneratorParamNames,
                new[] { Convert((int)transformType), Convert(targetShape) },
                new[] { data });
            return results[0];
        }

        private static string[] _InstanceNormParamNames = new[] { "eps" };

        public static NDArray InstanceNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, float eps = 0.00100000005f)
        {
            var results = Operator.Invoke(
                "InstanceNorm",
                _InstanceNormParamNames,
                new[] { Convert(eps) },
                new[] { data, gamma, beta });
            return results[0];
        }

        private static string[] _L2NormalizationParamNames = new[] { "eps", "mode" };

        public static NDArray L2Normalization(NDArrayOrSymbol data, float eps = 1.00000001e-10f, L2normalizationMode mode = L2normalizationMode.Instance)
        {
            var results = Operator.Invoke(
                "L2Normalization",
                _L2NormalizationParamNames,
                new[] { Convert(eps), Convert((int)mode) },
                new[] { data });
            return results[0];
        }

        private static string[] _MakeLossParamNames = new[] { "gradScale", "validThresh", "normalization" };

        public static NDArray MakeLoss(NDArrayOrSymbol data, float gradScale = 1f, float validThresh = 0f, MakelossNormalization normalization = MakelossNormalization.Null)
        {
            var results = Operator.Invoke(
                "MakeLoss",
                _MakeLossParamNames,
                new[] { Convert(gradScale), Convert(validThresh), Convert((int)normalization) },
                new[] { data });
            return results[0];
        }

        private static string[] _PoolingV1ParamNames = new[] { "kernel", "poolType", "globalPool", "poolingConvention", "stride", "pad" };

        public static NDArray PoolingV1(NDArrayOrSymbol data, NDShape kernel = null, PoolingV1PoolType poolType = PoolingV1PoolType.Max, bool globalPool = false, PoolingV1PoolingConvention poolingConvention = PoolingV1PoolingConvention.Valid, NDShape stride = null, NDShape pad = null)
        {
            var results = Operator.Invoke(
                "Pooling_v1",
                _PoolingV1ParamNames,
                new[] { Convert(kernel), Convert((int)poolType), Convert(globalPool), Convert((int)poolingConvention), Convert(stride), Convert(pad) },
                new[] { data });
            return results[0];
        }

        private static string[] _ROIPoolingParamNames = new[] { "pooledSize", "spatialScale" };

        public static NDArray ROIPooling(NDArrayOrSymbol data, NDArrayOrSymbol rois, NDShape pooledSize, float spatialScale)
        {
            var results = Operator.Invoke(
                "ROIPooling",
                _ROIPoolingParamNames,
                new[] { Convert(pooledSize), Convert(spatialScale) },
                new[] { data, rois });
            return results[0];
        }

        private static string[] _SequenceLastParamNames = new[] { "useSequenceLength", "axis" };

        public static NDArray SequenceLast(NDArrayOrSymbol data, NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0)
        {
            var results = Operator.Invoke(
                "SequenceLast",
                _SequenceLastParamNames,
                new[] { Convert(useSequenceLength), Convert(axis) },
                new[] { data, sequenceLength });
            return results[0];
        }

        private static string[] _SequenceMaskParamNames = new[] { "useSequenceLength", "value", "axis" };

        public static NDArray SequenceMask(NDArrayOrSymbol data, NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, float value = 0f, int axis = 0)
        {
            var results = Operator.Invoke(
                "SequenceMask",
                _SequenceMaskParamNames,
                new[] { Convert(useSequenceLength), Convert(value), Convert(axis) },
                new[] { data, sequenceLength });
            return results[0];
        }

        private static string[] _SequenceReverseParamNames = new[] { "useSequenceLength", "axis" };

        public static NDArray SequenceReverse(NDArrayOrSymbol data, NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0)
        {
            var results = Operator.Invoke(
                "SequenceReverse",
                _SequenceReverseParamNames,
                new[] { Convert(useSequenceLength), Convert(axis) },
                new[] { data, sequenceLength });
            return results[0];
        }

        private static string[] _SpatialTransformerParamNames = new[] { "transformType", "samplerType", "targetShape", "cudnnOff" };

        public static NDArray SpatialTransformer(NDArrayOrSymbol data, NDArrayOrSymbol loc, SpatialtransformerTransformType transformType, SpatialtransformerSamplerType samplerType, NDShape targetShape = null, bool? cudnnOff = null)
        {
            var results = Operator.Invoke(
                "SpatialTransformer",
                _SpatialTransformerParamNames,
                new[] { Convert((int)transformType), Convert((int)samplerType), Convert(targetShape), Convert(cudnnOff) },
                new[] { data, loc });
            return results[0];
        }

        private static string[] _SVMOutputParamNames = new[] { "margin", "regularizationCoefficient", "useLinear" };

        public static NDArray SVMOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float margin = 1f, float regularizationCoefficient = 1f, bool useLinear = false)
        {
            var results = Operator.Invoke(
                "SVMOutput",
                _SVMOutputParamNames,
                new[] { Convert(margin), Convert(regularizationCoefficient), Convert(useLinear) },
                new[] { data, label });
            return results[0];
        }

        private static string[] _onehotEncodeParamNames = _empty;

        public static NDArray OnehotEncode(Symbol lhs, Symbol rhs)
        {
            var results = Operator.Invoke(
                "_onehot_encode",
                _onehotEncodeParamNames,
                _empty,
                new[] { lhs, rhs });
            return results[0];
        }

        private static string[] _fillElement0indexParamNames = _empty;

        public static NDArray FillElement0index(Symbol lhs, Symbol mhs, Symbol rhs)
        {
            var results = Operator.Invoke(
                "fill_element_0index",
                _fillElement0indexParamNames,
                _empty,
                new[] { lhs, mhs, rhs });
            return results[0];
        }

        private static string[] _imdecodeParamNames = new[] { "index", "x0", "y0", "x1", "y1", "c", "size" };

        public static NDArray Imdecode(NDArrayOrSymbol mean, int index, int x0, int y0, int x1, int y1, int c, int size)
        {
            var results = Operator.Invoke(
                "_imdecode",
                _imdecodeParamNames,
                new[] { Convert(index), Convert(x0), Convert(y0), Convert(x1), Convert(y1), Convert(c), Convert(size) },
                new[] { mean });
            return results[0];
        }
    }
}
