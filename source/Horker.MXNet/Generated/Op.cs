using System;
using System.Collections.Generic;
using Horker.MXNet.Core;

namespace Horker.MXNet.Operators
{
    public partial class Op : OperatorsBase
    {

        private static string[] _CustomFunctionParamNames = _empty;

        public static NDArray CustomFunction(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_CustomFunction",
                _CustomFunctionParamNames,
                _empty,
                _emptyInput,
                output);
            return result;
        }

        private static string[] _cvimdecodeParamNames = new[] { "flag", "to_rgb" };

        public static NDArray Cvimdecode(Symbol buf, int flag = 1, bool toRgb = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_cvimdecode",
                _cvimdecodeParamNames,
                new[] { Convert(flag), Convert(toRgb) },
                new[] { buf },
                output);
            return result;
        }

        private static string[] _cvimreadParamNames = new[] { "filename", "flag", "to_rgb" };

        public static NDArray Cvimread(string filename, int flag = 1, bool toRgb = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_cvimread",
                _cvimreadParamNames,
                new[] { Convert(filename), Convert(flag), Convert(toRgb) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _cvimresizeParamNames = new[] { "w", "h", "interp" };

        public static NDArray Cvimresize(Symbol data, int w, int h, int interp = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_cvimresize",
                _cvimresizeParamNames,
                new[] { Convert(w), Convert(h), Convert(interp) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _cvcopyMakeBorderParamNames = new[] { "top", "bot", "left", "right", "type", "values" };

        public static NDArray CvcopyMakeBorder(Symbol data, int top, int bot, int left, int right, int type = 0, Tuple<double> values = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_cvcopyMakeBorder",
                _cvcopyMakeBorderParamNames,
                new[] { Convert(top), Convert(bot), Convert(left), Convert(right), Convert(type), Convert(values) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _copytoParamNames = _empty;

        public static NDArray Copyto(Symbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_copyto",
                _copytoParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _NoGradientParamNames = _empty;

        public static NDArray NoGradient(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_NoGradient",
                _NoGradientParamNames,
                _empty,
                _emptyInput,
                output);
            return result;
        }

        private static string[] _BatchNormV1ParamNames = new[] { "eps", "momentum", "fix_gamma", "use_global_stats", "output_mean_var" };

        public static NDArray BatchNormV1(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, float eps = 0.00100000005f, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "BatchNorm_v1",
                _BatchNormV1ParamNames,
                new[] { Convert(eps), Convert(momentum), Convert(fixGamma), Convert(useGlobalStats), Convert(outputMeanVar) },
                new[] { data, gamma, beta },
                output);
            return result;
        }

        private static string[] _mpAdamwUpdateParamNames = new[] { "lr", "eta", "beta1", "beta2", "epsilon", "wd", "clip_gradient" };

        public static NDArray MpAdamwUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol weight32, NDArrayOrSymbol rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_mp_adamw_update",
                _mpAdamwUpdateParamNames,
                new[] { Convert(lr), Convert(eta), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(clipGradient) },
                new[] { weight, grad, mean, var, weight32, rescaleGrad },
                output);
            return result;
        }

        private static string[] _adamwUpdateParamNames = new[] { "lr", "eta", "beta1", "beta2", "epsilon", "wd", "clip_gradient" };

        public static NDArray AdamwUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol rescaleGrad, float lr, float eta, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float clipGradient = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_adamw_update",
                _adamwUpdateParamNames,
                new[] { Convert(lr), Convert(eta), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(clipGradient) },
                new[] { weight, grad, mean, var, rescaleGrad },
                output);
            return result;
        }

        private static string[] _allFiniteParamNames = new[] { "init_output" };

        public static NDArray AllFinite(Symbol data, bool initOutput = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "all_finite",
                _allFiniteParamNames,
                new[] { Convert(initOutput) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _IdentityAttachKLSparseRegParamNames = new[] { "sparseness_target", "penalty", "momentum" };

        public static NDArray IdentityAttachKLSparseReg(NDArrayOrSymbol data, float sparsenessTarget = 0.100000001f, float penalty = 0.00100000005f, float momentum = 0.899999976f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "IdentityAttachKLSparseReg",
                _IdentityAttachKLSparseRegParamNames,
                new[] { Convert(sparsenessTarget), Convert(penalty), Convert(momentum) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _LeakyReLUParamNames = new[] { "act_type", "slope", "lower_bound", "upper_bound" };

        public static NDArray LeakyReLU(NDArrayOrSymbol data, NDArrayOrSymbol gamma, LeakyreluActType actType = LeakyreluActType.Leaky, float slope = 0.25f, float lowerBound = 0.125f, float upperBound = 0.333999991f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LeakyReLU",
                _LeakyReLUParamNames,
                new[] { Convert((int)actType), Convert(slope), Convert(lowerBound), Convert(upperBound) },
                new[] { data, gamma },
                output);
            return result;
        }

        private static string[] _softmaxCrossEntropyParamNames = _empty;

        public static NDArray SoftmaxCrossEntropy(NDArrayOrSymbol data, NDArrayOrSymbol label, NDArray output = null)
        {
            var result = Operator.Invoke(
                "softmax_cross_entropy",
                _softmaxCrossEntropyParamNames,
                _empty,
                new[] { data, label },
                output);
            return result;
        }

        private static string[] _ActivationParamNames = new[] { "act_type" };

        public static NDArray Activation(NDArrayOrSymbol data, ActivationActType actType, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Activation",
                _ActivationParamNames,
                new[] { Convert((int)actType) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _BatchNormParamNames = new[] { "eps", "momentum", "fix_gamma", "use_global_stats", "output_mean_var", "axis", "cudnn_off" };

        public static NDArray BatchNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "BatchNorm",
                _BatchNormParamNames,
                new[] { Convert(eps), Convert(momentum), Convert(fixGamma), Convert(useGlobalStats), Convert(outputMeanVar), Convert(axis), Convert(cudnnOff) },
                new[] { data, gamma, beta, movingMean, movingVar },
                output);
            return result;
        }

        private static string[] _ConvolutionParamNames = new[] { "kernel", "num_filter", "stride", "dilate", "pad", "num_group", "workspace", "no_bias", "cudnn_tune", "cudnn_off", "layout" };

        public static NDArray Convolution(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionLayout? layout = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Convolution",
                _ConvolutionParamNames,
                new[] { Convert(kernel), Convert(numFilter), Convert(stride), Convert(dilate), Convert(pad), Convert(numGroup), Convert(workspace), Convert(noBias), Convert((int)cudnnTune), Convert(cudnnOff), Convert((int)layout) },
                new[] { data, weight, bias },
                output);
            return result;
        }

        private static string[] _CTCLossParamNames = new[] { "use_data_lengths", "use_label_lengths", "blank_label" };

        public static NDArray CTCLoss(NDArrayOrSymbol data, NDArrayOrSymbol label, NDArrayOrSymbol dataLengths, NDArrayOrSymbol labelLengths, bool useDataLengths = false, bool useLabelLengths = false, CtclossBlankLabel blankLabel = CtclossBlankLabel.First, NDArray output = null)
        {
            var result = Operator.Invoke(
                "CTCLoss",
                _CTCLossParamNames,
                new[] { Convert(useDataLengths), Convert(useLabelLengths), Convert((int)blankLabel) },
                new[] { data, label, dataLengths, labelLengths },
                output);
            return result;
        }

        private static string[] _CuDNNBatchNormParamNames = new[] { "eps", "momentum", "fix_gamma", "use_global_stats", "output_mean_var", "axis", "cudnn_off" };

        public static NDArray CuDNNBatchNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, float momentum = 0.899999976f, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "CuDNNBatchNorm",
                _CuDNNBatchNormParamNames,
                new[] { Convert(eps), Convert(momentum), Convert(fixGamma), Convert(useGlobalStats), Convert(outputMeanVar), Convert(axis), Convert(cudnnOff) },
                new[] { data, gamma, beta, movingMean, movingVar },
                output);
            return result;
        }

        private static string[] _DeconvolutionParamNames = new[] { "kernel", "num_filter", "stride", "dilate", "pad", "adj", "target_shape", "num_group", "workspace", "no_bias", "cudnn_tune", "cudnn_off", "layout" };

        public static NDArray Deconvolution(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, NDShape adj = null, NDShape targetShape = null, uint numGroup = 1, ulong workspace = 512, bool noBias = true, DeconvolutionCudnnTune? cudnnTune = null, bool cudnnOff = false, DeconvolutionLayout? layout = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Deconvolution",
                _DeconvolutionParamNames,
                new[] { Convert(kernel), Convert(numFilter), Convert(stride), Convert(dilate), Convert(pad), Convert(adj), Convert(targetShape), Convert(numGroup), Convert(workspace), Convert(noBias), Convert((int)cudnnTune), Convert(cudnnOff), Convert((int)layout) },
                new[] { data, weight, bias },
                output);
            return result;
        }

        private static string[] _DropoutParamNames = new[] { "p", "mode", "axes", "cudnn_off" };

        public static NDArray Dropout(NDArrayOrSymbol data, float p = 0.5f, DropoutMode mode = DropoutMode.Training, NDShape axes = null, bool? cudnnOff = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Dropout",
                _DropoutParamNames,
                new[] { Convert(p), Convert((int)mode), Convert(axes), Convert(cudnnOff) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _FullyConnectedParamNames = new[] { "num_hidden", "no_bias", "flatten" };

        public static NDArray FullyConnected(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, int numHidden, bool noBias = false, bool flatten = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "FullyConnected",
                _FullyConnectedParamNames,
                new[] { Convert(numHidden), Convert(noBias), Convert(flatten) },
                new[] { data, weight, bias },
                output);
            return result;
        }

        private static string[] _LayerNormParamNames = new[] { "axis", "eps", "output_mean_var" };

        public static NDArray LayerNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, int axis = -1, float eps = 9.99999975e-06f, bool outputMeanVar = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LayerNorm",
                _LayerNormParamNames,
                new[] { Convert(axis), Convert(eps), Convert(outputMeanVar) },
                new[] { data, gamma, beta },
                output);
            return result;
        }

        private static string[] _LRNParamNames = new[] { "nsize", "alpha", "beta", "knorm" };

        public static NDArray LRN(NDArrayOrSymbol data, uint nsize, float alpha = 9.99999975e-05f, float beta = 0.75f, float knorm = 2f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LRN",
                _LRNParamNames,
                new[] { Convert(nsize), Convert(alpha), Convert(beta), Convert(knorm) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _momentsParamNames = new[] { "axes", "keepdims" };

        public static NDArray Moments(NDArrayOrSymbol data, NDShape axes = null, bool keepdims = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "moments",
                _momentsParamNames,
                new[] { Convert(axes), Convert(keepdims) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _PoolingParamNames = new[] { "kernel", "pool_type", "global_pool", "cudnn_off", "pooling_convention", "stride", "pad", "p_value", "count_include_pad", "layout" };

        public static NDArray Pooling(NDArrayOrSymbol data, NDShape kernel = null, PoolingPoolType poolType = PoolingPoolType.Max, bool globalPool = false, bool cudnnOff = false, PoolingPoolingConvention poolingConvention = PoolingPoolingConvention.Valid, NDShape stride = null, NDShape pad = null, int? pValue = null, bool? countIncludePad = null, PoolingLayout? layout = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Pooling",
                _PoolingParamNames,
                new[] { Convert(kernel), Convert((int)poolType), Convert(globalPool), Convert(cudnnOff), Convert((int)poolingConvention), Convert(stride), Convert(pad), Convert(pValue), Convert(countIncludePad), Convert((int)layout) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _softmaxParamNames = new[] { "axis", "temperature", "dtype" };

        public static NDArray Softmax(NDArrayOrSymbol data, int axis = -1, double? temperature = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "softmax",
                _softmaxParamNames,
                new[] { Convert(axis), Convert(temperature), Convert(dtype) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _softminParamNames = new[] { "axis", "temperature", "dtype" };

        public static NDArray Softmin(NDArrayOrSymbol data, int axis = -1, double? temperature = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "softmin",
                _softminParamNames,
                new[] { Convert(axis), Convert(temperature), Convert(dtype) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _logSoftmaxParamNames = new[] { "axis", "temperature", "dtype" };

        public static NDArray LogSoftmax(NDArrayOrSymbol data, int axis = -1, double? temperature = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log_softmax",
                _logSoftmaxParamNames,
                new[] { Convert(axis), Convert(temperature), Convert(dtype) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _SoftmaxActivationParamNames = new[] { "mode" };

        public static NDArray SoftmaxActivation(NDArrayOrSymbol data, SoftmaxactivationMode mode = SoftmaxactivationMode.Instance, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SoftmaxActivation",
                _SoftmaxActivationParamNames,
                new[] { Convert((int)mode) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _signsgdUpdateParamNames = new[] { "lr", "wd", "rescale_grad", "clip_gradient" };

        public static NDArray SignsgdUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "signsgd_update",
                _signsgdUpdateParamNames,
                new[] { Convert(lr), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad },
                output);
            return result;
        }

        private static string[] _signumUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient", "wd_lh" };

        public static NDArray SignumUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float wdLh = 0f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "signum_update",
                _signumUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(wdLh) },
                new[] { weight, grad, mom },
                output);
            return result;
        }

        private static string[] _sgdUpdateParamNames = new[] { "lr", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        public static NDArray SgdUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sgd_update",
                _sgdUpdateParamNames,
                new[] { Convert(lr), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad },
                output);
            return result;
        }

        private static string[] _sgdMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        public static NDArray SgdMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sgd_mom_update",
                _sgdMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad, mom },
                output);
            return result;
        }

        private static string[] _mpSgdUpdateParamNames = new[] { "lr", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        public static NDArray MpSgdUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol weight32, float lr, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "mp_sgd_update",
                _mpSgdUpdateParamNames,
                new[] { Convert(lr), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad, weight32 },
                output);
            return result;
        }

        private static string[] _mpSgdMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        public static NDArray MpSgdMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "mp_sgd_mom_update",
                _mpSgdMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad, mom, weight32 },
                output);
            return result;
        }

        private static string[] _ftmlUpdateParamNames = new[] { "lr", "t", "beta1", "beta2", "epsilon", "wd", "rescale_grad", "clip_grad" };

        public static NDArray FtmlUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol d, NDArrayOrSymbol v, NDArrayOrSymbol z, float lr, int t, float beta1 = 0.600000024f, float beta2 = 0.999000013f, double epsilon = 9.9999999392252903e-09, float wd = 0f, float rescaleGrad = 1f, float clipGrad = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ftml_update",
                _ftmlUpdateParamNames,
                new[] { Convert(lr), Convert(t), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGrad) },
                new[] { weight, grad, d, v, z },
                output);
            return result;
        }

        private static string[] _adamUpdateParamNames = new[] { "lr", "beta1", "beta2", "epsilon", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        public static NDArray AdamUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, float lr, float beta1 = 0.899999976f, float beta2 = 0.999000013f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "adam_update",
                _adamUpdateParamNames,
                new[] { Convert(lr), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight, grad, mean, var },
                output);
            return result;
        }

        private static string[] _nagMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient" };

        public static NDArray NagMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "nag_mom_update",
                _nagMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad, mom },
                output);
            return result;
        }

        private static string[] _mpNagMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient" };

        public static NDArray MpNagMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, float lr, float momentum = 0f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "mp_nag_mom_update",
                _mpNagMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad, mom, weight32 },
                output);
            return result;
        }

        private static string[] _rmspropUpdateParamNames = new[] { "lr", "gamma1", "epsilon", "wd", "rescale_grad", "clip_gradient", "clip_weights" };

        public static NDArray RmspropUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol n, float lr, float gamma1 = 0.949999988f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rmsprop_update",
                _rmspropUpdateParamNames,
                new[] { Convert(lr), Convert(gamma1), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(clipWeights) },
                new[] { weight, grad, n },
                output);
            return result;
        }

        private static string[] _rmspropalexUpdateParamNames = new[] { "lr", "gamma1", "gamma2", "epsilon", "wd", "rescale_grad", "clip_gradient", "clip_weights" };

        public static NDArray RmspropalexUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol n, NDArrayOrSymbol g, NDArrayOrSymbol delta, float lr, float gamma1 = 0.949999988f, float gamma2 = 0.899999976f, float epsilon = 9.99999994e-09f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, float clipWeights = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rmspropalex_update",
                _rmspropalexUpdateParamNames,
                new[] { Convert(lr), Convert(gamma1), Convert(gamma2), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(clipWeights) },
                new[] { weight, grad, n, g, delta },
                output);
            return result;
        }

        private static string[] _ftrlUpdateParamNames = new[] { "lr", "lamda1", "beta", "wd", "rescale_grad", "clip_gradient" };

        public static NDArray FtrlUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol z, NDArrayOrSymbol n, float lr, float lamda1 = 0.00999999978f, float beta = 1f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ftrl_update",
                _ftrlUpdateParamNames,
                new[] { Convert(lr), Convert(lamda1), Convert(beta), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad, z, n },
                output);
            return result;
        }

        private static string[] _sparseAdagradUpdateParamNames = new[] { "lr", "epsilon", "wd", "rescale_grad", "clip_gradient" };

        public static NDArray SparseAdagradUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol history, float lr, float epsilon = 1.00000001e-07f, float wd = 0f, float rescaleGrad = 1f, float clipGradient = -1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sparse_adagrad_update",
                _sparseAdagradUpdateParamNames,
                new[] { Convert(lr), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight, grad, history },
                output);
            return result;
        }

        private static string[] _PadParamNames = new[] { "mode", "pad_width", "constant_value" };

        public static NDArray Pad(NDArrayOrSymbol data, PadMode mode, NDShape padWidth, double constantValue = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Pad",
                _PadParamNames,
                new[] { Convert((int)mode), Convert(padWidth), Convert(constantValue) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _FlattenParamNames = _empty;

        public static NDArray Flatten(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Flatten",
                _FlattenParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _sampleUniformParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleUniform(NDArrayOrSymbol low, NDArrayOrSymbol high, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_uniform",
                _sampleUniformParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { low, high },
                output);
            return result;
        }

        private static string[] _sampleNormalParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleNormal(NDArrayOrSymbol mu, NDArrayOrSymbol sigma, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_normal",
                _sampleNormalParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { mu, sigma },
                output);
            return result;
        }

        private static string[] _sampleGammaParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleGamma(NDArrayOrSymbol alpha, NDArrayOrSymbol beta, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_gamma",
                _sampleGammaParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { alpha, beta },
                output);
            return result;
        }

        private static string[] _sampleExponentialParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleExponential(NDArrayOrSymbol lam, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_exponential",
                _sampleExponentialParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { lam },
                output);
            return result;
        }

        private static string[] _samplePoissonParamNames = new[] { "shape", "dtype" };

        public static NDArray SamplePoisson(NDArrayOrSymbol lam, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_poisson",
                _samplePoissonParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { lam },
                output);
            return result;
        }

        private static string[] _sampleNegativeBinomialParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleNegativeBinomial(NDArrayOrSymbol k, NDArrayOrSymbol p, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_negative_binomial",
                _sampleNegativeBinomialParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { k, p },
                output);
            return result;
        }

        private static string[] _sampleGeneralizedNegativeBinomialParamNames = new[] { "shape", "dtype" };

        public static NDArray SampleGeneralizedNegativeBinomial(NDArrayOrSymbol mu, NDArrayOrSymbol alpha, NDShape shape = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_generalized_negative_binomial",
                _sampleGeneralizedNegativeBinomialParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { mu, alpha },
                output);
            return result;
        }

        private static string[] _sampleMultinomialParamNames = new[] { "shape", "get_prob", "dtype" };

        public static NDArray SampleMultinomial(NDArrayOrSymbol data, NDShape shape = null, bool getProb = false, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_multinomial",
                _sampleMultinomialParamNames,
                new[] { Convert(shape), Convert(getProb), Convert(dtype) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _randomUniformParamNames = new[] { "low", "high", "shape", "ctx", "dtype" };

        public static NDArray RandomUniform(float low = 0f, float high = 1f, NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_uniform",
                _randomUniformParamNames,
                new[] { Convert(low), Convert(high), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _randomNormalParamNames = new[] { "loc", "scale", "shape", "ctx", "dtype" };

        public static NDArray RandomNormal(float loc = 0f, float scale = 1f, NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_normal",
                _randomNormalParamNames,
                new[] { Convert(loc), Convert(scale), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _randomGammaParamNames = new[] { "alpha", "beta", "shape", "ctx", "dtype" };

        public static NDArray RandomGamma(float alpha = 1f, float beta = 1f, NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_gamma",
                _randomGammaParamNames,
                new[] { Convert(alpha), Convert(beta), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _randomExponentialParamNames = new[] { "lam", "shape", "ctx", "dtype" };

        public static NDArray RandomExponential(float lam = 1f, NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_exponential",
                _randomExponentialParamNames,
                new[] { Convert(lam), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _randomPoissonParamNames = new[] { "lam", "shape", "ctx", "dtype" };

        public static NDArray RandomPoisson(float lam = 1f, NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_poisson",
                _randomPoissonParamNames,
                new[] { Convert(lam), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _randomNegativeBinomialParamNames = new[] { "k", "p", "shape", "ctx", "dtype" };

        public static NDArray RandomNegativeBinomial(int k = 1, float p = 1f, NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_negative_binomial",
                _randomNegativeBinomialParamNames,
                new[] { Convert(k), Convert(p), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _randomGeneralizedNegativeBinomialParamNames = new[] { "mu", "alpha", "shape", "ctx", "dtype" };

        public static NDArray RandomGeneralizedNegativeBinomial(float mu = 1f, float alpha = 1f, NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_generalized_negative_binomial",
                _randomGeneralizedNegativeBinomialParamNames,
                new[] { Convert(mu), Convert(alpha), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _randomRandintParamNames = new[] { "low", "high", "shape", "ctx", "dtype" };

        public static NDArray RandomRandint(Tuple<double> low, Tuple<double> high, NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_randint",
                _randomRandintParamNames,
                new[] { Convert(low), Convert(high), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _randomUniformLikeParamNames = new[] { "low", "high" };

        public static NDArray RandomUniformLike(NDArrayOrSymbol data, float low = 0f, float high = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_uniform_like",
                _randomUniformLikeParamNames,
                new[] { Convert(low), Convert(high) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _randomNormalLikeParamNames = new[] { "loc", "scale" };

        public static NDArray RandomNormalLike(NDArrayOrSymbol data, float loc = 0f, float scale = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_normal_like",
                _randomNormalLikeParamNames,
                new[] { Convert(loc), Convert(scale) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _randomGammaLikeParamNames = new[] { "alpha", "beta" };

        public static NDArray RandomGammaLike(NDArrayOrSymbol data, float alpha = 1f, float beta = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_gamma_like",
                _randomGammaLikeParamNames,
                new[] { Convert(alpha), Convert(beta) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _randomExponentialLikeParamNames = new[] { "lam" };

        public static NDArray RandomExponentialLike(NDArrayOrSymbol data, float lam = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_exponential_like",
                _randomExponentialLikeParamNames,
                new[] { Convert(lam) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _randomPoissonLikeParamNames = new[] { "lam" };

        public static NDArray RandomPoissonLike(NDArrayOrSymbol data, float lam = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_poisson_like",
                _randomPoissonLikeParamNames,
                new[] { Convert(lam) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _randomNegativeBinomialLikeParamNames = new[] { "k", "p" };

        public static NDArray RandomNegativeBinomialLike(NDArrayOrSymbol data, int k = 1, float p = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_negative_binomial_like",
                _randomNegativeBinomialLikeParamNames,
                new[] { Convert(k), Convert(p) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _randomGeneralizedNegativeBinomialLikeParamNames = new[] { "mu", "alpha" };

        public static NDArray RandomGeneralizedNegativeBinomialLike(NDArrayOrSymbol data, float mu = 1f, float alpha = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_generalized_negative_binomial_like",
                _randomGeneralizedNegativeBinomialLikeParamNames,
                new[] { Convert(mu), Convert(alpha) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _shuffleParamNames = _empty;

        public static NDArray Shuffle(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_shuffle",
                _shuffleParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _sampleUniqueZipfianParamNames = new[] { "range_max", "shape" };

        public static NDArray SampleUniqueZipfian(int rangeMax, NDShape shape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_unique_zipfian",
                _sampleUniqueZipfianParamNames,
                new[] { Convert(rangeMax), Convert(shape) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _LinearRegressionOutputParamNames = new[] { "grad_scale" };

        public static NDArray LinearRegressionOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float gradScale = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LinearRegressionOutput",
                _LinearRegressionOutputParamNames,
                new[] { Convert(gradScale) },
                new[] { data, label },
                output);
            return result;
        }

        private static string[] _MAERegressionOutputParamNames = new[] { "grad_scale" };

        public static NDArray MAERegressionOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float gradScale = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "MAERegressionOutput",
                _MAERegressionOutputParamNames,
                new[] { Convert(gradScale) },
                new[] { data, label },
                output);
            return result;
        }

        private static string[] _LogisticRegressionOutputParamNames = new[] { "grad_scale" };

        public static NDArray LogisticRegressionOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float gradScale = 1f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LogisticRegressionOutput",
                _LogisticRegressionOutputParamNames,
                new[] { Convert(gradScale) },
                new[] { data, label },
                output);
            return result;
        }

        private static string[] _RNNParamNames = new[] { "state_size", "num_layers", "mode", "bidirectional", "p", "state_outputs", "projection_size", "lstm_state_clip_min", "lstm_state_clip_max", "lstm_state_clip_nan", "use_sequence_length" };

        public static NDArray RNN(NDArrayOrSymbol data, NDArrayOrSymbol parameters, NDArrayOrSymbol state, NDArrayOrSymbol stateCell, NDArrayOrSymbol sequenceLength, uint stateSize, uint numLayers, RNNMode mode, bool bidirectional = false, float p = 0f, bool stateOutputs = false, int? projectionSize = null, double? lstmStateClipMin = null, double? lstmStateClipMax = null, bool lstmStateClipNan = false, bool useSequenceLength = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "RNN",
                _RNNParamNames,
                new[] { Convert(stateSize), Convert(numLayers), Convert((int)mode), Convert(bidirectional), Convert(p), Convert(stateOutputs), Convert(projectionSize), Convert(lstmStateClipMin), Convert(lstmStateClipMax), Convert(lstmStateClipNan), Convert(useSequenceLength) },
                new[] { data, parameters, state, stateCell, sequenceLength },
                output);
            return result;
        }

        private static string[] _SliceChannelParamNames = new[] { "num_outputs", "axis", "squeeze_axis" };

        public static NDArray SliceChannel(NDArrayOrSymbol data, int numOutputs, int axis = 1, bool squeezeAxis = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SliceChannel",
                _SliceChannelParamNames,
                new[] { Convert(numOutputs), Convert(axis), Convert(squeezeAxis) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _SoftmaxOutputParamNames = new[] { "grad_scale", "ignore_label", "multi_output", "use_ignore", "preserve_shape", "normalization", "out_grad", "smooth_alpha" };

        public static NDArray SoftmaxOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float gradScale = 1f, float ignoreLabel = -1f, bool multiOutput = false, bool useIgnore = false, bool preserveShape = false, SoftmaxoutputNormalization normalization = SoftmaxoutputNormalization.Null, bool outGrad = false, float smoothAlpha = 0f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SoftmaxOutput",
                _SoftmaxOutputParamNames,
                new[] { Convert(gradScale), Convert(ignoreLabel), Convert(multiOutput), Convert(useIgnore), Convert(preserveShape), Convert((int)normalization), Convert(outGrad), Convert(smoothAlpha) },
                new[] { data, label },
                output);
            return result;
        }

        private static string[] _sgMkldnnConvParamNames = _empty;

        public static NDArray SgMkldnnConv(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sg_mkldnn_conv",
                _sgMkldnnConvParamNames,
                _empty,
                _emptyInput,
                output);
            return result;
        }

        private static string[] _sgMkldnnFullyConnectedParamNames = _empty;

        public static NDArray SgMkldnnFullyConnected(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sg_mkldnn_fully_connected",
                _sgMkldnnFullyConnectedParamNames,
                _empty,
                _emptyInput,
                output);
            return result;
        }

        private static string[] _SwapAxisParamNames = new[] { "dim1", "dim2" };

        public static NDArray SwapAxis(NDArrayOrSymbol data, uint dim1 = 0, uint dim2 = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SwapAxis",
                _SwapAxisParamNames,
                new[] { Convert(dim1), Convert(dim2) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _ampCastParamNames = new[] { "dtype" };

        public static NDArray AmpCast(NDArrayOrSymbol data, DType dtype, NDArray output = null)
        {
            var result = Operator.Invoke(
                "amp_cast",
                _ampCastParamNames,
                new[] { Convert(dtype) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _argmaxParamNames = new[] { "axis", "keepdims" };

        public static NDArray Argmax(NDArrayOrSymbol data, int? axis = null, bool keepdims = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "argmax",
                _argmaxParamNames,
                new[] { Convert(axis), Convert(keepdims) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _argminParamNames = new[] { "axis", "keepdims" };

        public static NDArray Argmin(NDArrayOrSymbol data, int? axis = null, bool keepdims = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "argmin",
                _argminParamNames,
                new[] { Convert(axis), Convert(keepdims) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _argmaxChannelParamNames = _empty;

        public static NDArray ArgmaxChannel(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "argmax_channel",
                _argmaxChannelParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _pickParamNames = new[] { "axis", "keepdims", "mode" };

        public static NDArray Pick(NDArrayOrSymbol data, NDArrayOrSymbol index, int? axis = -1, bool keepdims = false, PickMode mode = PickMode.Clip, NDArray output = null)
        {
            var result = Operator.Invoke(
                "pick",
                _pickParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert((int)mode) },
                new[] { data, index },
                output);
            return result;
        }

        private static string[] _sumParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Sum(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sum",
                _sumParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _meanParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Mean(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "mean",
                _meanParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _prodParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Prod(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "prod",
                _prodParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _nansumParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Nansum(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "nansum",
                _nansumParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _nanprodParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Nanprod(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "nanprod",
                _nanprodParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _maxParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Max(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "max",
                _maxParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _minParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray Min(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "min",
                _minParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _broadcastAxisParamNames = new[] { "axis", "size" };

        public static NDArray BroadcastAxis(NDArrayOrSymbol data, NDShape axis = null, NDShape size = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_axis",
                _broadcastAxisParamNames,
                new[] { Convert(axis), Convert(size) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _broadcastToParamNames = new[] { "shape" };

        public static NDArray BroadcastTo(NDArrayOrSymbol data, NDShape shape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_to",
                _broadcastToParamNames,
                new[] { Convert(shape) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _broadcastLikeParamNames = new[] { "lhs_axes", "rhs_axes" };

        public static NDArray BroadcastLike(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDShape lhsAxes = null, NDShape rhsAxes = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_like",
                _broadcastLikeParamNames,
                new[] { Convert(lhsAxes), Convert(rhsAxes) },
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _normParamNames = new[] { "ord", "axis", "out_dtype", "keepdims" };

        public static NDArray Norm(NDArrayOrSymbol data, int ord = 2, NDShape axis = null, NormOutDtype? outDtype = null, bool keepdims = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "norm",
                _normParamNames,
                new[] { Convert(ord), Convert(axis), Convert((int)outDtype), Convert(keepdims) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _castStorageParamNames = new[] { "stype" };

        public static NDArray CastStorage(NDArrayOrSymbol data, CastStorageStype stype, NDArray output = null)
        {
            var result = Operator.Invoke(
                "cast_storage",
                _castStorageParamNames,
                new[] { Convert((int)stype) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _whereParamNames = _empty;

        public static NDArray Where(NDArrayOrSymbol condition, NDArrayOrSymbol x, NDArrayOrSymbol y, NDArray output = null)
        {
            var result = Operator.Invoke(
                "where",
                _whereParamNames,
                _empty,
                new[] { condition, x, y },
                output);
            return result;
        }

        private static string[] _diagParamNames = new[] { "k", "axis1", "axis2" };

        public static NDArray Diag(NDArrayOrSymbol data, int k = 0, int axis1 = 0, int axis2 = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "diag",
                _diagParamNames,
                new[] { Convert(k), Convert(axis1), Convert(axis2) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _dotParamNames = new[] { "transpose_a", "transpose_b", "forward_stype" };

        public static NDArray Dot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, DotForwardStype? forwardStype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "dot",
                _dotParamNames,
                new[] { Convert(transposeA), Convert(transposeB), forwardStype.HasValue ? Convert((int)forwardStype) : null },
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _batchDotParamNames = new[] { "transpose_a", "transpose_b", "forward_stype" };

        public static NDArray BatchDot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, BatchDotForwardStype? forwardStype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "batch_dot",
                _batchDotParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert((int)forwardStype) },
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastAddParamNames = _empty;

        public static NDArray BroadcastAdd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_add",
                _broadcastAddParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastSubParamNames = _empty;

        public static NDArray BroadcastSub(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_sub",
                _broadcastSubParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastMulParamNames = _empty;

        public static NDArray BroadcastMul(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_mul",
                _broadcastMulParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastDivParamNames = _empty;

        public static NDArray BroadcastDiv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_div",
                _broadcastDivParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastModParamNames = _empty;

        public static NDArray BroadcastMod(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_mod",
                _broadcastModParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastPowerParamNames = _empty;

        public static NDArray BroadcastPower(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_power",
                _broadcastPowerParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastMaximumParamNames = _empty;

        public static NDArray BroadcastMaximum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_maximum",
                _broadcastMaximumParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastMinimumParamNames = _empty;

        public static NDArray BroadcastMinimum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_minimum",
                _broadcastMinimumParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastHypotParamNames = _empty;

        public static NDArray BroadcastHypot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_hypot",
                _broadcastHypotParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastEqualParamNames = _empty;

        public static NDArray BroadcastEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_equal",
                _broadcastEqualParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastNotEqualParamNames = _empty;

        public static NDArray BroadcastNotEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_not_equal",
                _broadcastNotEqualParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastGreaterParamNames = _empty;

        public static NDArray BroadcastGreater(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_greater",
                _broadcastGreaterParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastGreaterEqualParamNames = _empty;

        public static NDArray BroadcastGreaterEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_greater_equal",
                _broadcastGreaterEqualParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastLesserParamNames = _empty;

        public static NDArray BroadcastLesser(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_lesser",
                _broadcastLesserParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastLesserEqualParamNames = _empty;

        public static NDArray BroadcastLesserEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_lesser_equal",
                _broadcastLesserEqualParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastLogicalAndParamNames = _empty;

        public static NDArray BroadcastLogicalAnd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_logical_and",
                _broadcastLogicalAndParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastLogicalOrParamNames = _empty;

        public static NDArray BroadcastLogicalOr(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_logical_or",
                _broadcastLogicalOrParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _broadcastLogicalXorParamNames = _empty;

        public static NDArray BroadcastLogicalXor(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_logical_xor",
                _broadcastLogicalXorParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _elemwiseAddParamNames = _empty;

        public static NDArray ElemwiseAdd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "elemwise_add",
                _elemwiseAddParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _gradAddParamNames = _empty;

        public static NDArray GradAdd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_grad_add",
                _gradAddParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _elemwiseSubParamNames = _empty;

        public static NDArray ElemwiseSub(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "elemwise_sub",
                _elemwiseSubParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _elemwiseMulParamNames = _empty;

        public static NDArray ElemwiseMul(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "elemwise_mul",
                _elemwiseMulParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _elemwiseDivParamNames = _empty;

        public static NDArray ElemwiseDiv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "elemwise_div",
                _elemwiseDivParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _modParamNames = _empty;

        public static NDArray Mod(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_mod",
                _modParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _powerParamNames = _empty;

        public static NDArray Power(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_power",
                _powerParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _maximumParamNames = _empty;

        public static NDArray Maximum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_maximum",
                _maximumParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _minimumParamNames = _empty;

        public static NDArray Minimum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_minimum",
                _minimumParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _hypotParamNames = _empty;

        public static NDArray Hypot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_hypot",
                _hypotParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _equalParamNames = _empty;

        public static NDArray Equal(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_equal",
                _equalParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _notEqualParamNames = _empty;

        public static NDArray NotEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_not_equal",
                _notEqualParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _greaterParamNames = _empty;

        public static NDArray Greater(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_greater",
                _greaterParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _greaterEqualParamNames = _empty;

        public static NDArray GreaterEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_greater_equal",
                _greaterEqualParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _lesserParamNames = _empty;

        public static NDArray Lesser(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_lesser",
                _lesserParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _lesserEqualParamNames = _empty;

        public static NDArray LesserEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_lesser_equal",
                _lesserEqualParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _logicalAndParamNames = _empty;

        public static NDArray LogicalAnd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_and",
                _logicalAndParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _logicalOrParamNames = _empty;

        public static NDArray LogicalOr(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_or",
                _logicalOrParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _logicalXorParamNames = _empty;

        public static NDArray LogicalXor(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_xor",
                _logicalXorParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _plusScalarParamNames = new[] { "scalar" };

        public static NDArray PlusScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_plus_scalar",
                _plusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _minusScalarParamNames = new[] { "scalar" };

        public static NDArray MinusScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_minus_scalar",
                _minusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _rminusScalarParamNames = new[] { "scalar" };

        public static NDArray RminusScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_rminus_scalar",
                _rminusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _mulScalarParamNames = new[] { "scalar" };

        public static NDArray MulScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_mul_scalar",
                _mulScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _divScalarParamNames = new[] { "scalar" };

        public static NDArray DivScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_div_scalar",
                _divScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _rdivScalarParamNames = new[] { "scalar" };

        public static NDArray RdivScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_rdiv_scalar",
                _rdivScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _modScalarParamNames = new[] { "scalar" };

        public static NDArray ModScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_mod_scalar",
                _modScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _rmodScalarParamNames = new[] { "scalar" };

        public static NDArray RmodScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_rmod_scalar",
                _rmodScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _maximumScalarParamNames = new[] { "scalar" };

        public static NDArray MaximumScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_maximum_scalar",
                _maximumScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _minimumScalarParamNames = new[] { "scalar" };

        public static NDArray MinimumScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_minimum_scalar",
                _minimumScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _powerScalarParamNames = new[] { "scalar" };

        public static NDArray PowerScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_power_scalar",
                _powerScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _rpowerScalarParamNames = new[] { "scalar" };

        public static NDArray RpowerScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_rpower_scalar",
                _rpowerScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _hypotScalarParamNames = new[] { "scalar" };

        public static NDArray HypotScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_hypot_scalar",
                _hypotScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _smoothL1ParamNames = new[] { "scalar" };

        public static NDArray SmoothL1(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "smooth_l1",
                _smoothL1ParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _equalScalarParamNames = new[] { "scalar" };

        public static NDArray EqualScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_equal_scalar",
                _equalScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _notEqualScalarParamNames = new[] { "scalar" };

        public static NDArray NotEqualScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_not_equal_scalar",
                _notEqualScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _greaterScalarParamNames = new[] { "scalar" };

        public static NDArray GreaterScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_greater_scalar",
                _greaterScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _greaterEqualScalarParamNames = new[] { "scalar" };

        public static NDArray GreaterEqualScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_greater_equal_scalar",
                _greaterEqualScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _lesserScalarParamNames = new[] { "scalar" };

        public static NDArray LesserScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_lesser_scalar",
                _lesserScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _lesserEqualScalarParamNames = new[] { "scalar" };

        public static NDArray LesserEqualScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_lesser_equal_scalar",
                _lesserEqualScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _logicalAndScalarParamNames = new[] { "scalar" };

        public static NDArray LogicalAndScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_and_scalar",
                _logicalAndScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _logicalOrScalarParamNames = new[] { "scalar" };

        public static NDArray LogicalOrScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_or_scalar",
                _logicalOrScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _logicalXorScalarParamNames = new[] { "scalar" };

        public static NDArray LogicalXorScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_xor_scalar",
                _logicalXorScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _scatterElemwiseDivParamNames = _empty;

        public static NDArray ScatterElemwiseDiv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_scatter_elemwise_div",
                _scatterElemwiseDivParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _scatterPlusScalarParamNames = new[] { "scalar" };

        public static NDArray ScatterPlusScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_scatter_plus_scalar",
                _scatterPlusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _scatterMinusScalarParamNames = new[] { "scalar" };

        public static NDArray ScatterMinusScalar(NDArrayOrSymbol data, float scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_scatter_minus_scalar",
                _scatterMinusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _reluParamNames = _empty;

        public static NDArray Relu(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "relu",
                _reluParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _sigmoidParamNames = _empty;

        public static NDArray Sigmoid(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sigmoid",
                _sigmoidParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _hardSigmoidParamNames = new[] { "alpha", "beta" };

        public static NDArray HardSigmoid(NDArrayOrSymbol data, float alpha = 0.200000003f, float beta = 0.5f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "hard_sigmoid",
                _hardSigmoidParamNames,
                new[] { Convert(alpha), Convert(beta) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _softsignParamNames = _empty;

        public static NDArray Softsign(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "softsign",
                _softsignParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _copyParamNames = _empty;

        public static NDArray Copy(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_copy",
                _copyParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _BlockGradParamNames = _empty;

        public static NDArray BlockGrad(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "BlockGrad",
                _BlockGradParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _makeLossParamNames = _empty;

        public static NDArray MakeLoss(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "make_loss",
                _makeLossParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _identityWithAttrLikeRhsParamNames = _empty;

        public static NDArray IdentityWithAttrLikeRhs(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_identity_with_attr_like_rhs",
                _identityWithAttrLikeRhsParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _reshapeLikeParamNames = _empty;

        public static NDArray ReshapeLike(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "reshape_like",
                _reshapeLikeParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _shapeArrayParamNames = new[] { "lhs_begin", "lhs_end", "rhs_begin", "rhs_end" };

        public static NDArray ShapeArray(NDArrayOrSymbol data, int? lhsBegin = null, int? lhsEnd = null, int? rhsBegin = null, int? rhsEnd = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "shape_array",
                _shapeArrayParamNames,
                new[] { Convert(lhsBegin), Convert(lhsEnd), Convert(rhsBegin), Convert(rhsEnd) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _sizeArrayParamNames = _empty;

        public static NDArray SizeArray(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "size_array",
                _sizeArrayParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _CastParamNames = new[] { "dtype" };

        public static NDArray Cast(NDArrayOrSymbol data, DType dtype, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Cast",
                _CastParamNames,
                new[] { Convert(dtype) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _negativeParamNames = _empty;

        public static NDArray Negative(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "negative",
                _negativeParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _reciprocalParamNames = _empty;

        public static NDArray Reciprocal(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "reciprocal",
                _reciprocalParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _absParamNames = _empty;

        public static NDArray Abs(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "abs",
                _absParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _signParamNames = _empty;

        public static NDArray Sign(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sign",
                _signParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _roundParamNames = _empty;

        public static NDArray Round(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "round",
                _roundParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _rintParamNames = _empty;

        public static NDArray Rint(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rint",
                _rintParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _ceilParamNames = _empty;

        public static NDArray Ceil(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ceil",
                _ceilParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _floorParamNames = _empty;

        public static NDArray Floor(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "floor",
                _floorParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _truncParamNames = _empty;

        public static NDArray Trunc(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "trunc",
                _truncParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _fixParamNames = _empty;

        public static NDArray Fix(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "fix",
                _fixParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _squareParamNames = _empty;

        public static NDArray Square(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "square",
                _squareParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _sqrtParamNames = _empty;

        public static NDArray Sqrt(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sqrt",
                _sqrtParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _rsqrtParamNames = _empty;

        public static NDArray Rsqrt(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rsqrt",
                _rsqrtParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _cbrtParamNames = _empty;

        public static NDArray Cbrt(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "cbrt",
                _cbrtParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _erfParamNames = _empty;

        public static NDArray Erf(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "erf",
                _erfParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _erfinvParamNames = _empty;

        public static NDArray Erfinv(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "erfinv",
                _erfinvParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _rcbrtParamNames = _empty;

        public static NDArray Rcbrt(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rcbrt",
                _rcbrtParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _expParamNames = _empty;

        public static NDArray Exp(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "exp",
                _expParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _logParamNames = _empty;

        public static NDArray Log(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log",
                _logParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _log10ParamNames = _empty;

        public static NDArray Log10(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log10",
                _log10ParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _log2ParamNames = _empty;

        public static NDArray Log2(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log2",
                _log2ParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _log1pParamNames = _empty;

        public static NDArray Log1p(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log1p",
                _log1pParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _expm1ParamNames = _empty;

        public static NDArray Expm1(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "expm1",
                _expm1ParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _gammaParamNames = _empty;

        public static NDArray Gamma(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "gamma",
                _gammaParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _gammalnParamNames = _empty;

        public static NDArray Gammaln(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "gammaln",
                _gammalnParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _logicalNotParamNames = _empty;

        public static NDArray LogicalNot(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "logical_not",
                _logicalNotParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _sinParamNames = _empty;

        public static NDArray Sin(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sin",
                _sinParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _cosParamNames = _empty;

        public static NDArray Cos(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "cos",
                _cosParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _tanParamNames = _empty;

        public static NDArray Tan(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "tan",
                _tanParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _arcsinParamNames = _empty;

        public static NDArray Arcsin(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arcsin",
                _arcsinParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _arccosParamNames = _empty;

        public static NDArray Arccos(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arccos",
                _arccosParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _arctanParamNames = _empty;

        public static NDArray Arctan(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arctan",
                _arctanParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _degreesParamNames = _empty;

        public static NDArray Degrees(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "degrees",
                _degreesParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _radiansParamNames = _empty;

        public static NDArray Radians(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "radians",
                _radiansParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _sinhParamNames = _empty;

        public static NDArray Sinh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sinh",
                _sinhParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _coshParamNames = _empty;

        public static NDArray Cosh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "cosh",
                _coshParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _tanhParamNames = _empty;

        public static NDArray Tanh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "tanh",
                _tanhParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _arcsinhParamNames = _empty;

        public static NDArray Arcsinh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arcsinh",
                _arcsinhParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _arccoshParamNames = _empty;

        public static NDArray Arccosh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arccosh",
                _arccoshParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _arctanhParamNames = _empty;

        public static NDArray Arctanh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arctanh",
                _arctanhParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _histogramParamNames = new[] { "bin_cnt", "range" };

        public static NDArray Histogram(NDArrayOrSymbol data, NDArrayOrSymbol bins, int? binCnt = null, Tuple<double> range = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_histogram",
                _histogramParamNames,
                new[] { Convert(binCnt), Convert(range) },
                new[] { data, bins },
                output);
            return result;
        }

        private static string[] _EmbeddingParamNames = new[] { "input_dim", "output_dim", "dtype", "sparse_grad" };

        public static NDArray Embedding(NDArrayOrSymbol data, NDArrayOrSymbol weight, int inputDim, int outputDim, DType dtype = null, bool sparseGrad = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Embedding",
                _EmbeddingParamNames,
                new[] { Convert(inputDim), Convert(outputDim), Convert(dtype), Convert(sparseGrad) },
                new[] { data, weight },
                output);
            return result;
        }

        private static string[] _takeParamNames = new[] { "axis", "mode" };

        public static NDArray Take(NDArrayOrSymbol a, NDArrayOrSymbol indices, int axis = 0, TakeMode mode = TakeMode.Clip, NDArray output = null)
        {
            var result = Operator.Invoke(
                "take",
                _takeParamNames,
                new[] { Convert(axis), Convert((int)mode) },
                new[] { a, indices },
                output);
            return result;
        }

        private static string[] _batchTakeParamNames = _empty;

        public static NDArray BatchTake(NDArrayOrSymbol a, NDArrayOrSymbol indices, NDArray output = null)
        {
            var result = Operator.Invoke(
                "batch_take",
                _batchTakeParamNames,
                _empty,
                new[] { a, indices },
                output);
            return result;
        }

        private static string[] _oneHotParamNames = new[] { "depth", "on_value", "off_value", "dtype" };

        public static NDArray OneHot(NDArrayOrSymbol indices, int depth, double onValue = 1, double offValue = 0, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "one_hot",
                _oneHotParamNames,
                new[] { Convert(depth), Convert(onValue), Convert(offValue), Convert(dtype) },
                new[] { indices },
                output);
            return result;
        }

        private static string[] _gatherNdParamNames = _empty;

        public static NDArray GatherNd(NDArrayOrSymbol data, NDArrayOrSymbol indices, NDArray output = null)
        {
            var result = Operator.Invoke(
                "gather_nd",
                _gatherNdParamNames,
                _empty,
                new[] { data, indices },
                output);
            return result;
        }

        private static string[] _scatterNdParamNames = new[] { "shape" };

        public static NDArray ScatterNd(NDArrayOrSymbol data, NDArrayOrSymbol indices, NDShape shape, NDArray output = null)
        {
            var result = Operator.Invoke(
                "scatter_nd",
                _scatterNdParamNames,
                new[] { Convert(shape) },
                new[] { data, indices },
                output);
            return result;
        }

        private static string[] _scatterSetNdParamNames = new[] { "shape" };

        public static NDArray ScatterSetNd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArrayOrSymbol indices, NDShape shape, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_scatter_set_nd",
                _scatterSetNdParamNames,
                new[] { Convert(shape) },
                new[] { lhs, rhs, indices },
                output);
            return result;
        }

        private static string[] _zerosWithoutDtypeParamNames = new[] { "shape", "ctx", "dtype" };

        public static NDArray ZerosWithoutDtype(NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_zeros_without_dtype",
                _zerosWithoutDtypeParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _zerosParamNames = new[] { "shape", "ctx", "dtype" };

        public static NDArray Zeros(NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_zeros",
                _zerosParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _eyeParamNames = new[] { "N", "M", "k", "ctx", "dtype" };

        public static NDArray Eye(Tuple<double> N, int M = 0, int k = 0, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_eye",
                _eyeParamNames,
                new[] { Convert(N), Convert(M), Convert(k), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _onesParamNames = new[] { "shape", "ctx", "dtype" };

        public static NDArray Ones(NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_ones",
                _onesParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _fullParamNames = new[] { "value", "shape", "ctx", "dtype" };

        public static NDArray Full(double value, NDShape shape = null, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_full",
                _fullParamNames,
                new[] { Convert(value), Convert(shape), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _arangeParamNames = new[] { "start", "stop", "step", "repeat", "infer_range", "ctx", "dtype" };

        public static NDArray Arange(double start, double? stop = null, double step = 1, int repeat = 1, bool inferRange = false, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_arange",
                _arangeParamNames,
                new[] { Convert(start), Convert(stop), Convert(step), Convert(repeat), Convert(inferRange), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _linspaceParamNames = new[] { "start", "stop", "step", "repeat", "infer_range", "ctx", "dtype" };

        public static NDArray Linspace(double start, double? stop = null, double step = 1, int repeat = 1, bool inferRange = false, Context ctx = null, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linspace",
                _linspaceParamNames,
                new[] { Convert(start), Convert(stop), Convert(step), Convert(repeat), Convert(inferRange), Convert(ctx), Convert(dtype) },
                _emptyInput,
                output);
            return result;
        }

        private static string[] _zerosLikeParamNames = _empty;

        public static NDArray ZerosLike(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "zeros_like",
                _zerosLikeParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _onesLikeParamNames = _empty;

        public static NDArray OnesLike(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ones_like",
                _onesLikeParamNames,
                _empty,
                new[] { data },
                output);
            return result;
        }

        private static string[] _linalgGemmParamNames = new[] { "transpose_a", "transpose_b", "alpha", "beta", "axis" };

        public static NDArray LinalgGemm(NDArrayOrSymbol A, NDArrayOrSymbol B, NDArrayOrSymbol C, bool transposeA = false, bool transposeB = false, double alpha = 1, double beta = 1, int axis = -2, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_gemm",
                _linalgGemmParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert(alpha), Convert(beta), Convert(axis) },
                new[] { A, B, C },
                output);
            return result;
        }

        private static string[] _linalgGemm2ParamNames = new[] { "transpose_a", "transpose_b", "alpha", "axis" };

        public static NDArray LinalgGemm2(NDArrayOrSymbol A, NDArrayOrSymbol B, bool transposeA = false, bool transposeB = false, double alpha = 1, int axis = -2, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_gemm2",
                _linalgGemm2ParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert(alpha), Convert(axis) },
                new[] { A, B },
                output);
            return result;
        }

        private static string[] _linalgPotrfParamNames = _empty;

        public static NDArray LinalgPotrf(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_potrf",
                _linalgPotrfParamNames,
                _empty,
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgPotriParamNames = _empty;

        public static NDArray LinalgPotri(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_potri",
                _linalgPotriParamNames,
                _empty,
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgTrmmParamNames = new[] { "transpose", "rightside", "lower", "alpha" };

        public static NDArray LinalgTrmm(NDArrayOrSymbol A, NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_trmm",
                _linalgTrmmParamNames,
                new[] { Convert(transpose), Convert(rightside), Convert(lower), Convert(alpha) },
                new[] { A, B },
                output);
            return result;
        }

        private static string[] _linalgTrsmParamNames = new[] { "transpose", "rightside", "lower", "alpha" };

        public static NDArray LinalgTrsm(NDArrayOrSymbol A, NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_trsm",
                _linalgTrsmParamNames,
                new[] { Convert(transpose), Convert(rightside), Convert(lower), Convert(alpha) },
                new[] { A, B },
                output);
            return result;
        }

        private static string[] _linalgSumlogdiagParamNames = _empty;

        public static NDArray LinalgSumlogdiag(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_sumlogdiag",
                _linalgSumlogdiagParamNames,
                _empty,
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgExtractdiagParamNames = new[] { "offset" };

        public static NDArray LinalgExtractdiag(NDArrayOrSymbol A, int offset = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_extractdiag",
                _linalgExtractdiagParamNames,
                new[] { Convert(offset) },
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgMakediagParamNames = new[] { "offset" };

        public static NDArray LinalgMakediag(NDArrayOrSymbol A, int offset = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_makediag",
                _linalgMakediagParamNames,
                new[] { Convert(offset) },
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgExtracttrianParamNames = new[] { "offset", "lower" };

        public static NDArray LinalgExtracttrian(NDArrayOrSymbol A, int offset = 0, bool lower = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_extracttrian",
                _linalgExtracttrianParamNames,
                new[] { Convert(offset), Convert(lower) },
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgMaketrianParamNames = new[] { "offset", "lower" };

        public static NDArray LinalgMaketrian(NDArrayOrSymbol A, int offset = 0, bool lower = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_maketrian",
                _linalgMaketrianParamNames,
                new[] { Convert(offset), Convert(lower) },
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgSyrkParamNames = new[] { "transpose", "alpha" };

        public static NDArray LinalgSyrk(NDArrayOrSymbol A, bool transpose = false, double alpha = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_syrk",
                _linalgSyrkParamNames,
                new[] { Convert(transpose), Convert(alpha) },
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgGelqfParamNames = _empty;

        public static NDArray LinalgGelqf(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_gelqf",
                _linalgGelqfParamNames,
                _empty,
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgSyevdParamNames = _empty;

        public static NDArray LinalgSyevd(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_syevd",
                _linalgSyevdParamNames,
                _empty,
                new[] { A },
                output);
            return result;
        }

        private static string[] _linalgInverseParamNames = _empty;

        public static NDArray LinalgInverse(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_inverse",
                _linalgInverseParamNames,
                _empty,
                new[] { A },
                output);
            return result;
        }

        private static string[] _ReshapeParamNames = new[] { "shape", "reverse" };

        public static NDArray Reshape(NDArrayOrSymbol data, NDShape shape = null, bool reverse = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Reshape",
                _ReshapeParamNames,
                new[] { Convert(shape), Convert(reverse) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _transposeParamNames = new[] { "axes" };

        public static NDArray Transpose(NDArrayOrSymbol data, NDShape axes = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "transpose",
                _transposeParamNames,
                new[] { Convert(axes) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _expandDimsParamNames = new[] { "axis" };

        public static NDArray ExpandDims(NDArrayOrSymbol data, int axis, NDArray output = null)
        {
            var result = Operator.Invoke(
                "expand_dims",
                _expandDimsParamNames,
                new[] { Convert(axis) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _sliceParamNames = new[] { "begin", "end", "step" };

        public static NDArray Slice(NDArrayOrSymbol data, NDShape begin, NDShape end, NDShape step = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "slice",
                _sliceParamNames,
                new[] { Convert(begin), Convert(end), Convert(step) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _sliceAssignParamNames = new[] { "begin", "end", "step" };

        public static NDArray SliceAssign(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDShape begin, NDShape end, NDShape step = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_slice_assign",
                _sliceAssignParamNames,
                new[] { Convert(begin), Convert(end), Convert(step) },
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _sliceAssignScalarParamNames = new[] { "begin", "end", "scalar", "step" };

        public static NDArray SliceAssignScalar(NDArrayOrSymbol data, NDShape begin, NDShape end, double scalar = 0, NDShape step = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_slice_assign_scalar",
                _sliceAssignScalarParamNames,
                new[] { Convert(begin), Convert(end), Convert(scalar), Convert(step) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _sliceAxisParamNames = new[] { "axis", "begin", "end" };

        public static NDArray SliceAxis(NDArrayOrSymbol data, int axis, int begin, int? end, NDArray output = null)
        {
            var result = Operator.Invoke(
                "slice_axis",
                _sliceAxisParamNames,
                new[] { Convert(axis), Convert(begin), Convert(end) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _sliceLikeParamNames = new[] { "axes" };

        public static NDArray SliceLike(NDArrayOrSymbol data, NDArrayOrSymbol shapeLike, NDShape axes = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "slice_like",
                _sliceLikeParamNames,
                new[] { Convert(axes) },
                new[] { data, shapeLike },
                output);
            return result;
        }

        private static string[] _clipParamNames = new[] { "a_min", "a_max" };

        public static NDArray Clip(NDArrayOrSymbol data, float aMin, float aMax, NDArray output = null)
        {
            var result = Operator.Invoke(
                "clip",
                _clipParamNames,
                new[] { Convert(aMin), Convert(aMax) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _repeatParamNames = new[] { "repeats", "axis" };

        public static NDArray Repeat(NDArrayOrSymbol data, int repeats, int? axis = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "repeat",
                _repeatParamNames,
                new[] { Convert(repeats), Convert(axis) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _tileParamNames = new[] { "reps" };

        public static NDArray Tile(NDArrayOrSymbol data, NDShape reps, NDArray output = null)
        {
            var result = Operator.Invoke(
                "tile",
                _tileParamNames,
                new[] { Convert(reps) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _reverseParamNames = new[] { "axis" };

        public static NDArray Reverse(NDArrayOrSymbol data, NDShape axis, NDArray output = null)
        {
            var result = Operator.Invoke(
                "reverse",
                _reverseParamNames,
                new[] { Convert(axis) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _depthToSpaceParamNames = new[] { "block_size" };

        public static NDArray DepthToSpace(NDArrayOrSymbol data, int blockSize, NDArray output = null)
        {
            var result = Operator.Invoke(
                "depth_to_space",
                _depthToSpaceParamNames,
                new[] { Convert(blockSize) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _spaceToDepthParamNames = new[] { "block_size" };

        public static NDArray SpaceToDepth(NDArrayOrSymbol data, int blockSize, NDArray output = null)
        {
            var result = Operator.Invoke(
                "space_to_depth",
                _spaceToDepthParamNames,
                new[] { Convert(blockSize) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _splitV2ParamNames = new[] { "indices", "axis", "squeeze_axis", "sections" };

        public static NDArray SplitV2(NDArrayOrSymbol data, NDShape indices, int axis = 1, bool squeezeAxis = false, int sections = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_split_v2",
                _splitV2ParamNames,
                new[] { Convert(indices), Convert(axis), Convert(squeezeAxis), Convert(sections) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _topkParamNames = new[] { "axis", "k", "ret_typ", "is_ascend", "dtype" };

        public static NDArray Topk(NDArrayOrSymbol data, int? axis = -1, int k = 1, TopkRetTyp retTyp = TopkRetTyp.Indices, bool isAscend = false, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "topk",
                _topkParamNames,
                new[] { Convert(axis), Convert(k), Convert((int)retTyp), Convert(isAscend), Convert(dtype) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _sortParamNames = new[] { "axis", "is_ascend" };

        public static NDArray Sort(NDArrayOrSymbol data, int? axis = -1, bool isAscend = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sort",
                _sortParamNames,
                new[] { Convert(axis), Convert(isAscend) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _argsortParamNames = new[] { "axis", "is_ascend", "dtype" };

        public static NDArray Argsort(NDArrayOrSymbol data, int? axis = -1, bool isAscend = true, DType dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "argsort",
                _argsortParamNames,
                new[] { Convert(axis), Convert(isAscend), Convert(dtype) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _ravelMultiIndexParamNames = new[] { "shape" };

        public static NDArray RavelMultiIndex(NDArrayOrSymbol data, NDShape shape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_ravel_multi_index",
                _ravelMultiIndexParamNames,
                new[] { Convert(shape) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _unravelIndexParamNames = new[] { "shape" };

        public static NDArray UnravelIndex(NDArrayOrSymbol data, NDShape shape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_unravel_index",
                _unravelIndexParamNames,
                new[] { Convert(shape) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _sparseRetainParamNames = _empty;

        public static NDArray SparseRetain(NDArrayOrSymbol data, NDArrayOrSymbol indices, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sparse_retain",
                _sparseRetainParamNames,
                _empty,
                new[] { data, indices },
                output);
            return result;
        }

        private static string[] _squareSumParamNames = new[] { "axis", "keepdims", "exclude" };

        public static NDArray SquareSum(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_square_sum",
                _squareSumParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _BilinearSamplerParamNames = new[] { "cudnn_off" };

        public static NDArray BilinearSampler(NDArrayOrSymbol data, NDArrayOrSymbol grid, bool? cudnnOff = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "BilinearSampler",
                _BilinearSamplerParamNames,
                new[] { Convert(cudnnOff) },
                new[] { data, grid },
                output);
            return result;
        }

        private static string[] _ConvolutionV1ParamNames = new[] { "kernel", "num_filter", "stride", "dilate", "pad", "num_group", "workspace", "no_bias", "cudnn_tune", "cudnn_off", "layout" };

        public static NDArray ConvolutionV1(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, uint numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, uint numGroup = 1, ulong workspace = 1024, bool noBias = false, ConvolutionV1CudnnTune? cudnnTune = null, bool cudnnOff = false, ConvolutionV1Layout? layout = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Convolution_v1",
                _ConvolutionV1ParamNames,
                new[] { Convert(kernel), Convert(numFilter), Convert(stride), Convert(dilate), Convert(pad), Convert(numGroup), Convert(workspace), Convert(noBias), Convert((int)cudnnTune), Convert(cudnnOff), Convert((int)layout) },
                new[] { data, weight, bias },
                output);
            return result;
        }

        private static string[] _CorrelationParamNames = new[] { "kernel_size", "max_displacement", "stride1", "stride2", "pad_size", "is_multiply" };

        public static NDArray Correlation(NDArrayOrSymbol data1, NDArrayOrSymbol data2, uint kernelSize = 1, uint maxDisplacement = 1, uint stride1 = 1, uint stride2 = 1, uint padSize = 0, bool isMultiply = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Correlation",
                _CorrelationParamNames,
                new[] { Convert(kernelSize), Convert(maxDisplacement), Convert(stride1), Convert(stride2), Convert(padSize), Convert(isMultiply) },
                new[] { data1, data2 },
                output);
            return result;
        }

        private static string[] _CrossDeviceCopyParamNames = _empty;

        public static NDArray CrossDeviceCopy(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_CrossDeviceCopy",
                _CrossDeviceCopyParamNames,
                _empty,
                _emptyInput,
                output);
            return result;
        }

        private static string[] _GridGeneratorParamNames = new[] { "transform_type", "target_shape" };

        public static NDArray GridGenerator(NDArrayOrSymbol data, GridgeneratorTransformType transformType, NDShape targetShape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "GridGenerator",
                _GridGeneratorParamNames,
                new[] { Convert((int)transformType), Convert(targetShape) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _InstanceNormParamNames = new[] { "eps" };

        public static NDArray InstanceNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, float eps = 0.00100000005f, NDArray output = null)
        {
            var result = Operator.Invoke(
                "InstanceNorm",
                _InstanceNormParamNames,
                new[] { Convert(eps) },
                new[] { data, gamma, beta },
                output);
            return result;
        }

        private static string[] _L2NormalizationParamNames = new[] { "eps", "mode" };

        public static NDArray L2Normalization(NDArrayOrSymbol data, float eps = 1.00000001e-10f, L2normalizationMode mode = L2normalizationMode.Instance, NDArray output = null)
        {
            var result = Operator.Invoke(
                "L2Normalization",
                _L2NormalizationParamNames,
                new[] { Convert(eps), Convert((int)mode) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _PoolingV1ParamNames = new[] { "kernel", "pool_type", "global_pool", "pooling_convention", "stride", "pad" };

        public static NDArray PoolingV1(NDArrayOrSymbol data, NDShape kernel = null, PoolingV1PoolType poolType = PoolingV1PoolType.Max, bool globalPool = false, PoolingV1PoolingConvention poolingConvention = PoolingV1PoolingConvention.Valid, NDShape stride = null, NDShape pad = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Pooling_v1",
                _PoolingV1ParamNames,
                new[] { Convert(kernel), Convert((int)poolType), Convert(globalPool), Convert((int)poolingConvention), Convert(stride), Convert(pad) },
                new[] { data },
                output);
            return result;
        }

        private static string[] _ROIPoolingParamNames = new[] { "pooled_size", "spatial_scale" };

        public static NDArray ROIPooling(NDArrayOrSymbol data, NDArrayOrSymbol rois, NDShape pooledSize, float spatialScale, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ROIPooling",
                _ROIPoolingParamNames,
                new[] { Convert(pooledSize), Convert(spatialScale) },
                new[] { data, rois },
                output);
            return result;
        }

        private static string[] _SequenceLastParamNames = new[] { "use_sequence_length", "axis" };

        public static NDArray SequenceLast(NDArrayOrSymbol data, NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SequenceLast",
                _SequenceLastParamNames,
                new[] { Convert(useSequenceLength), Convert(axis) },
                new[] { data, sequenceLength },
                output);
            return result;
        }

        private static string[] _SequenceMaskParamNames = new[] { "use_sequence_length", "value", "axis" };

        public static NDArray SequenceMask(NDArrayOrSymbol data, NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, float value = 0f, int axis = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SequenceMask",
                _SequenceMaskParamNames,
                new[] { Convert(useSequenceLength), Convert(value), Convert(axis) },
                new[] { data, sequenceLength },
                output);
            return result;
        }

        private static string[] _SequenceReverseParamNames = new[] { "use_sequence_length", "axis" };

        public static NDArray SequenceReverse(NDArrayOrSymbol data, NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SequenceReverse",
                _SequenceReverseParamNames,
                new[] { Convert(useSequenceLength), Convert(axis) },
                new[] { data, sequenceLength },
                output);
            return result;
        }

        private static string[] _SpatialTransformerParamNames = new[] { "transform_type", "sampler_type", "target_shape", "cudnn_off" };

        public static NDArray SpatialTransformer(NDArrayOrSymbol data, NDArrayOrSymbol loc, SpatialtransformerTransformType transformType, SpatialtransformerSamplerType samplerType, NDShape targetShape = null, bool? cudnnOff = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SpatialTransformer",
                _SpatialTransformerParamNames,
                new[] { Convert((int)transformType), Convert((int)samplerType), Convert(targetShape), Convert(cudnnOff) },
                new[] { data, loc },
                output);
            return result;
        }

        private static string[] _SVMOutputParamNames = new[] { "margin", "regularization_coefficient", "use_linear" };

        public static NDArray SVMOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, float margin = 1f, float regularizationCoefficient = 1f, bool useLinear = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SVMOutput",
                _SVMOutputParamNames,
                new[] { Convert(margin), Convert(regularizationCoefficient), Convert(useLinear) },
                new[] { data, label },
                output);
            return result;
        }

        private static string[] _onehotEncodeParamNames = _empty;

        public static NDArray OnehotEncode(Symbol lhs, Symbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_onehot_encode",
                _onehotEncodeParamNames,
                _empty,
                new[] { lhs, rhs },
                output);
            return result;
        }

        private static string[] _fillElement0indexParamNames = _empty;

        public static NDArray FillElement0index(Symbol lhs, Symbol mhs, Symbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "fill_element_0index",
                _fillElement0indexParamNames,
                _empty,
                new[] { lhs, mhs, rhs },
                output);
            return result;
        }

        private static string[] _imdecodeParamNames = new[] { "index", "x0", "y0", "x1", "y1", "c", "size" };

        public static NDArray Imdecode(NDArrayOrSymbol mean, int index, int x0, int y0, int x1, int y1, int c, int size, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_imdecode",
                _imdecodeParamNames,
                new[] { Convert(index), Convert(x0), Convert(y0), Convert(x1), Convert(y1), Convert(c), Convert(size) },
                new[] { mean },
                output);
            return result;
        }
    }
}
