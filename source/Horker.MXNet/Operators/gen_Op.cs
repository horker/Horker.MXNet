using System;
using Horker.MXNet.Core;

namespace Horker.MXNet.Operators
{
    public class Op : OperatorsBase
    {
        private static string[] _CustomFunctionParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray CustomFunction(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_CustomFunction",
                _CustomFunctionParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardCustomFunctionParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCustomFunction(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_CustomFunction",
                _backwardCustomFunctionParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardCachedOpParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCachedOp(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_CachedOp",
                _backwardCachedOpParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _copytoParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">input data</param>
        public static NDArray Copyto(NDArray data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_copyto",
                _copytoParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _NoGradientParamNames = Empty;

        /// <summary>
        /// Place holder for variable who cannot perform gradient
        /// </summary>
        public static NDArray NoGradient(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_NoGradient",
                _NoGradientParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _BatchNormV1ParamNames = new[] { "eps", "momentum", "fix_gamma", "use_global_stats", "output_mean_var" };

        /// <summary>
        /// Batch normalization.
        /// 
        /// This operator is DEPRECATED. Perform BatchNorm on the input.
        /// 
        /// Normalizes a data batch by mean and variance, and applies a scale ``gamma`` as
        /// well as offset ``beta``.
        /// 
        /// Assume the input has more than one dimension and we normalize along axis 1.
        /// We first compute the mean and variance along this axis:
        /// 
        /// .. math::
        /// 
        ///   data\_mean[i] = mean(data[:,i,:,...]) \\
        ///   data\_var[i] = var(data[:,i,:,...])
        /// 
        /// Then compute the normalized output, which has the same shape as input, as following:
        /// 
        /// .. math::
        /// 
        ///   out[:,i,:,...] = \frac{data[:,i,:,...] - data\_mean[i]}{\sqrt{data\_var[i]+\epsilon}} * gamma[i] + beta[i]
        /// 
        /// Both *mean* and *var* returns a scalar by treating the input as a vector.
        /// 
        /// Assume the input has size *k* on axis 1, then both ``gamma`` and ``beta``
        /// have shape *(k,)*. If ``output_mean_var`` is set to be true, then outputs both ``data_mean`` and
        /// ``data_var`` as well, which are needed for the backward pass.
        /// 
        /// Besides the inputs and the outputs, this operator accepts two auxiliary
        /// states, ``moving_mean`` and ``moving_var``, which are *k*-length
        /// vectors. They are global statistics for the whole dataset, which are updated
        /// by::
        /// 
        ///   moving_mean = moving_mean * momentum + data_mean * (1 - momentum)
        ///   moving_var = moving_var * momentum + data_var * (1 - momentum)
        /// 
        /// If ``use_global_stats`` is set to be true, then ``moving_mean`` and
        /// ``moving_var`` are used instead of ``data_mean`` and ``data_var`` to compute
        /// the output. It is often used during inference.
        /// 
        /// Both ``gamma`` and ``beta`` are learnable parameters. But if ``fix_gamma`` is true,
        /// then set ``gamma`` to 1 and its gradient to 0.
        /// 
        /// There's no sparse support for this operator, and it will exhibit problematic behavior if used with
        /// sparse tensors.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\batch_norm_v1.cc:L95
        /// </summary>
        /// <param name="data">Input data to batch normalization</param>
        /// <param name="gamma">gamma array</param>
        /// <param name="beta">beta array</param>
        /// <param name="eps">Epsilon to prevent div 0</param>
        /// <param name="momentum">Momentum for moving average</param>
        /// <param name="fix_gamma">Fix gamma while training</param>
        /// <param name="use_global_stats">Whether use global moving statistics instead of local batch-norm. This will force change batch-norm into a scale shift operator.</param>
        /// <param name="output_mean_var">Output All,normal mean and var</param>
        public static NDArray BatchNormV1(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, double eps = 0.00100000005, double momentum = 0.899999976, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "BatchNorm_v1",
                _BatchNormV1ParamNames,
                new[] { Convert(eps), Convert(momentum), Convert(fixGamma), Convert(useGlobalStats), Convert(outputMeanVar) },
                new[] { data.Handle, gamma.Handle, beta.Handle },
                output
            );
            return result;
        }

        private static string[] _mpAdamwUpdateParamNames = new[] { "lr", "beta1", "beta2", "epsilon", "wd", "eta", "clip_gradient" };

        /// <summary>
        /// Update function for multi-precision AdamW optimizer.
        /// 
        /// AdamW is seen as a modification of Adam by decoupling the weight decay from the
        /// optimization steps taken w.r.t. the loss function.
        /// 
        /// Adam update consists of the following steps, where g represents gradient and m, v
        /// are 1st and 2nd order moment estimates (mean and variance).
        /// 
        /// .. math::
        /// 
        ///  g_t = \nabla J(W_{t-1})\\
        ///  m_t = \beta_1 m_{t-1} + (1 - \beta_1) g_t\\
        ///  v_t = \beta_2 v_{t-1} + (1 - \beta_2) g_t^2\\
        ///  W_t = W_{t-1} - \eta_t (\alpha \frac{ m_t }{ \sqrt{ v_t } + \epsilon } + wd W_{t-1})
        /// 
        /// It updates the weights using::
        /// 
        ///  m = beta1*m + (1-beta1)*grad
        ///  v = beta2*v + (1-beta2)*(grad**2)
        ///  w -= eta * (learning_rate * m / (sqrt(v) + epsilon) + w * wd)
        /// 
        /// Note that gradient is rescaled to grad = rescale_grad * grad. If rescale_grad is NaN, Inf, or 0,
        /// the update is skipped.
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\contrib\adamw.cc:L77
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="mean">Moving mean</param>
        /// <param name="var">Moving variance</param>
        /// <param name="weight32">Weight32</param>
        /// <param name="rescale_grad">Rescale gradient to rescale_grad * grad. If NaN, Inf, or 0, the update is skipped.</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="beta1">The decay rate for the 1st moment estimates.</param>
        /// <param name="beta2">The decay rate for the 2nd moment estimates.</param>
        /// <param name="epsilon">A small constant for numerical stability.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="eta">Learning rate schedule multiplier</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        public static NDArray MpAdamwUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol weight32, NDArrayOrSymbol rescaleGrad, double lr, double eta, double beta1 = 0.899999976, double beta2 = 0.999000013, double epsilon = 9.99999994e-09, double wd = 0, double clipGradient = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_mp_adamw_update",
                _mpAdamwUpdateParamNames,
                new[] { Convert(lr), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(eta), Convert(clipGradient) },
                new[] { weight.Handle, grad.Handle, mean.Handle, var.Handle, weight32.Handle, rescaleGrad.Handle },
                output
            );
            return result;
        }

        private static string[] _adamwUpdateParamNames = new[] { "lr", "beta1", "beta2", "epsilon", "wd", "eta", "clip_gradient" };

        /// <summary>
        /// Update function for AdamW optimizer. AdamW is seen as a modification of
        /// Adam by decoupling the weight decay from the optimization steps taken w.r.t. the loss function.
        /// 
        /// Adam update consists of the following steps, where g represents gradient and m, v
        /// are 1st and 2nd order moment estimates (mean and variance).
        /// 
        /// .. math::
        /// 
        ///  g_t = \nabla J(W_{t-1})\\
        ///  m_t = \beta_1 m_{t-1} + (1 - \beta_1) g_t\\
        ///  v_t = \beta_2 v_{t-1} + (1 - \beta_2) g_t^2\\
        ///  W_t = W_{t-1} - \eta_t (\alpha \frac{ m_t }{ \sqrt{ v_t } + \epsilon } + wd W_{t-1})
        /// 
        /// It updates the weights using::
        /// 
        ///  m = beta1*m + (1-beta1)*grad
        ///  v = beta2*v + (1-beta2)*(grad**2)
        ///  w -= eta * (learning_rate * m / (sqrt(v) + epsilon) + w * wd)
        /// 
        /// Note that gradient is rescaled to grad = rescale_grad * grad. If rescale_grad is NaN, Inf, or 0,
        /// the update is skipped.
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\contrib\adamw.cc:L120
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="mean">Moving mean</param>
        /// <param name="var">Moving variance</param>
        /// <param name="rescale_grad">Rescale gradient to rescale_grad * grad. If NaN, Inf, or 0, the update is skipped.</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="beta1">The decay rate for the 1st moment estimates.</param>
        /// <param name="beta2">The decay rate for the 2nd moment estimates.</param>
        /// <param name="epsilon">A small constant for numerical stability.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="eta">Learning rate schedule multiplier</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        public static NDArray AdamwUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, NDArrayOrSymbol rescaleGrad, double lr, double eta, double beta1 = 0.899999976, double beta2 = 0.999000013, double epsilon = 9.99999994e-09, double wd = 0, double clipGradient = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_adamw_update",
                _adamwUpdateParamNames,
                new[] { Convert(lr), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(eta), Convert(clipGradient) },
                new[] { weight.Handle, grad.Handle, mean.Handle, var.Handle, rescaleGrad.Handle },
                output
            );
            return result;
        }

        private static string[] _allFiniteParamNames = new[] { "init_output" };

        /// <summary>
        /// Check if all the float numbers in the array are finite (used for AMP)
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\contrib\all_finite.cc:L101
        /// </summary>
        /// <param name="data">Array</param>
        /// <param name="init_output">Initialize output to 1.</param>
        public static NDArray AllFinite(NDArray data, bool initOutput = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "all_finite",
                _allFiniteParamNames,
                new[] { Convert(initOutput) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardROIAlignParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardROIAlign(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_ROIAlign",
                _backwardROIAlignParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardForeachParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardForeach(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_foreach",
                _backwardForeachParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardWhileLoopParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardWhileLoop(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_while_loop",
                _backwardWhileLoopParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardCondParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCond(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_cond",
                _backwardCondParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardCustomParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCustom(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Custom",
                _backwardCustomParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _IdentityAttachKLSparseRegParamNames = new[] { "sparseness_target", "penalty", "momentum" };

        /// <summary>
        /// Apply a sparse regularization to the output a sigmoid activation function.
        /// </summary>
        /// <param name="data">Input data.</param>
        /// <param name="sparseness_target">The sparseness target</param>
        /// <param name="penalty">The tradeoff parameter for the sparseness penalty</param>
        /// <param name="momentum">The momentum for running average</param>
        public static NDArray IdentityAttachKLSparseReg(NDArrayOrSymbol data, double sparsenessTarget = 0.100000001, double penalty = 0.00100000005, double momentum = 0.899999976, NDArray output = null)
        {
            var result = Operator.Invoke(
                "IdentityAttachKLSparseReg",
                _IdentityAttachKLSparseRegParamNames,
                new[] { Convert(sparsenessTarget), Convert(penalty), Convert(momentum) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardImageCropParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardImageCrop(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_image_crop",
                _backwardImageCropParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardImageNormalizeParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardImageNormalize(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_image_normalize",
                _backwardImageNormalizeParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _LeakyReLUParamNames = new[] { "act_type", "slope", "lower_bound", "upper_bound" };

        /// <summary>
        /// Applies Leaky rectified linear unit activation element-wise to the input.
        /// 
        /// Leaky ReLUs attempt to fix the "dying ReLU" problem by allowing a small `slope`
        /// when the input is negative and has a slope of one when input is positive.
        /// 
        /// The following modified ReLU Activation functions are supported:
        /// 
        /// - *elu*: Exponential Linear Unit. `y = x > 0 ? x : slope * (exp(x)-1)`
        /// - *selu*: Scaled Exponential Linear Unit. `y = lambda * (x > 0 ? x : alpha * (exp(x) - 1))` where
        ///   *lambda = 1.0507009873554804934193349852946* and *alpha = 1.6732632423543772848170429916717*.
        /// - *leaky*: Leaky ReLU. `y = x > 0 ? x : slope * x`
        /// - *prelu*: Parametric ReLU. This is same as *leaky* except that `slope` is learnt during training.
        /// - *rrelu*: Randomized ReLU. same as *leaky* but the `slope` is uniformly and randomly chosen from
        ///   *[lower_bound, upper_bound)* for training, while fixed to be
        ///   *(lower_bound+upper_bound)/2* for inference.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\leaky_relu.cc:L65
        /// </summary>
        /// <param name="data">Input data to activation function.</param>
        /// <param name="gamma">Slope parameter for PReLU. Only required when act_type is 'prelu'. It should be either a vector of size 1, or the same size as the second dimension of data.</param>
        /// <param name="act_type">Activation function to be applied.</param>
        /// <param name="slope">Init slope for the activation. (For leaky and elu only)</param>
        /// <param name="lower_bound">Lower bound of random slope. (For rrelu only)</param>
        /// <param name="upper_bound">Upper bound of random slope. (For rrelu only)</param>
        public static NDArray LeakyReLU(NDArrayOrSymbol data, NDArrayOrSymbol gamma, string actType = "leaky", double slope = 0.25, double lowerBound = 0.125, double upperBound = 0.333999991, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LeakyReLU",
                _LeakyReLUParamNames,
                new[] { Convert(actType), Convert(slope), Convert(lowerBound), Convert(upperBound) },
                new[] { data.Handle, gamma.Handle },
                output
            );
            return result;
        }

        private static string[] _softmaxCrossEntropyParamNames = Empty;

        /// <summary>
        /// Calculate cross entropy of softmax output and one-hot label.
        /// 
        /// - This operator computes the cross entropy in two steps:
        ///   - Applies softmax function on the input array.
        ///   - Computes and returns the cross entropy loss between the softmax output and the labels.
        /// 
        /// - The softmax function and cross entropy loss is given by:
        /// 
        ///   - Softmax Function:
        /// 
        ///   .. math:: \text{softmax}(x)_i = \frac{exp(x_i)}{\sum_j exp(x_j)}
        /// 
        ///   - Cross Entropy Function:
        /// 
        ///   .. math:: \text{CE(label, output)} = - \sum_i \text{label}_i \log(\text{output}_i)
        /// 
        /// Example::
        /// 
        ///   x = [[1, 2, 3],
        ///        [11, 7, 5]]
        /// 
        ///   label = [2, 0]
        /// 
        ///   softmax(x) = [[0.09003057, 0.24472848, 0.66524094],
        ///                 [0.97962922, 0.01794253, 0.00242826]]
        /// 
        ///   softmax_cross_entropy(data, label) = - log(0.66524084) - log(0.97962922) = 0.4281871
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\loss_binary_op.cc:L59
        /// </summary>
        /// <param name="data">Input data</param>
        /// <param name="label">Input label</param>
        public static NDArray SoftmaxCrossEntropy(NDArrayOrSymbol data, NDArrayOrSymbol label, NDArray output = null)
        {
            var result = Operator.Invoke(
                "softmax_cross_entropy",
                _softmaxCrossEntropyParamNames,
                Empty,
                new[] { data.Handle, label.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSoftmaxCrossEntropyParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSoftmaxCrossEntropy(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_softmax_cross_entropy",
                _backwardSoftmaxCrossEntropyParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _ActivationParamNames = new[] { "act_type" };

        /// <summary>
        /// Applies an activation function element-wise to the input.
        /// 
        /// The following activation functions are supported:
        /// 
        /// - `relu`: Rectified Linear Unit, :math:`y = max(x, 0)`
        /// - `sigmoid`: :math:`y = \frac{1}{1 + exp(-x)}`
        /// - `tanh`: Hyperbolic tangent, :math:`y = \frac{exp(x) - exp(-x)}{exp(x) + exp(-x)}`
        /// - `softrelu`: Soft ReLU, or SoftPlus, :math:`y = log(1 + exp(x))`
        /// - `softsign`: :math:`y = \frac{x}{1 + abs(x)}`
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\activation.cc:L167
        /// </summary>
        /// <param name="data">The input array.</param>
        /// <param name="act_type">Activation function to be applied.</param>
        public static NDArray Activation(NDArrayOrSymbol data, string actType, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Activation",
                _ActivationParamNames,
                new[] { Convert(actType) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardActivationParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardActivation(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Activation",
                _backwardActivationParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _BatchNormParamNames = new[] { "eps", "momentum", "fix_gamma", "use_global_stats", "output_mean_var", "axis", "cudnn_off" };

        /// <summary>
        /// Batch normalization.
        /// 
        /// Normalizes a data batch by mean and variance, and applies a scale ``gamma`` as
        /// well as offset ``beta``.
        /// 
        /// Assume the input has more than one dimension and we normalize along axis 1.
        /// We first compute the mean and variance along this axis:
        /// 
        /// .. math::
        /// 
        ///   data\_mean[i] = mean(data[:,i,:,...]) \\
        ///   data\_var[i] = var(data[:,i,:,...])
        /// 
        /// Then compute the normalized output, which has the same shape as input, as following:
        /// 
        /// .. math::
        /// 
        ///   out[:,i,:,...] = \frac{data[:,i,:,...] - data\_mean[i]}{\sqrt{data\_var[i]+\epsilon}} * gamma[i] + beta[i]
        /// 
        /// Both *mean* and *var* returns a scalar by treating the input as a vector.
        /// 
        /// Assume the input has size *k* on axis 1, then both ``gamma`` and ``beta``
        /// have shape *(k,)*. If ``output_mean_var`` is set to be true, then outputs both ``data_mean`` and
        /// the inverse of ``data_var``, which are needed for the backward pass. Note that gradient of these
        /// two outputs are blocked.
        /// 
        /// Besides the inputs and the outputs, this operator accepts two auxiliary
        /// states, ``moving_mean`` and ``moving_var``, which are *k*-length
        /// vectors. They are global statistics for the whole dataset, which are updated
        /// by::
        /// 
        ///   moving_mean = moving_mean * momentum + data_mean * (1 - momentum)
        ///   moving_var = moving_var * momentum + data_var * (1 - momentum)
        /// 
        /// If ``use_global_stats`` is set to be true, then ``moving_mean`` and
        /// ``moving_var`` are used instead of ``data_mean`` and ``data_var`` to compute
        /// the output. It is often used during inference.
        /// 
        /// The parameter ``axis`` specifies which axis of the input shape denotes
        /// the 'channel' (separately normalized groups).  The default is 1.  Specifying -1 sets the channel
        /// axis to be the last item in the input shape.
        /// 
        /// Both ``gamma`` and ``beta`` are learnable parameters. But if ``fix_gamma`` is true,
        /// then set ``gamma`` to 1 and its gradient to 0.
        /// 
        /// .. Note::
        ///   When ``fix_gamma`` is set to True, no sparse support is provided. If ``fix_gamma is`` set to False,
        ///   the sparse tensors will fallback.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\batch_norm.cc:L572
        /// </summary>
        /// <param name="data">Input data to batch normalization</param>
        /// <param name="gamma">gamma array</param>
        /// <param name="beta">beta array</param>
        /// <param name="moving_mean">running mean of input</param>
        /// <param name="moving_var">running variance of input</param>
        /// <param name="eps">Epsilon to prevent div 0. Must be no less than CUDNN_BN_MIN_EPSILON defined in cudnn.h when using cudnn (usually 1e-5)</param>
        /// <param name="momentum">Momentum for moving average</param>
        /// <param name="fix_gamma">Fix gamma while training</param>
        /// <param name="use_global_stats">Whether use global moving statistics instead of local batch-norm. This will force change batch-norm into a scale shift operator.</param>
        /// <param name="output_mean_var">Output the mean and inverse std </param>
        /// <param name="axis">Specify which shape axis the channel is specified</param>
        /// <param name="cudnn_off">Do not select CUDNN operator, if available</param>
        public static NDArray BatchNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, double momentum = 0.899999976, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "BatchNorm",
                _BatchNormParamNames,
                new[] { Convert(eps), Convert(momentum), Convert(fixGamma), Convert(useGlobalStats), Convert(outputMeanVar), Convert(axis), Convert(cudnnOff) },
                new[] { data.Handle, gamma.Handle, beta.Handle, movingMean.Handle, movingVar.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBatchNormParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBatchNorm(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_BatchNorm",
                _backwardBatchNormParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardConcatParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardConcat(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Concat",
                _backwardConcatParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _ConvolutionParamNames = new[] { "kernel", "stride", "dilate", "pad", "num_filter", "num_group", "workspace", "no_bias", "cudnn_tune", "cudnn_off", "layout" };

        /// <summary>
        /// Compute *N*-D convolution on *(N+2)*-D input.
        /// 
        /// In the 2-D convolution, given input data with shape *(batch_size,
        /// channel, height, width)*, the output is computed by
        /// 
        /// .. math::
        /// 
        ///    out[n,i,:,:] = bias[i] + \sum_{j=0}^{channel} data[n,j,:,:] \star
        ///    weight[i,j,:,:]
        /// 
        /// where :math:`\star` is the 2-D cross-correlation operator.
        /// 
        /// For general 2-D convolution, the shapes are
        /// 
        /// - **data**: *(batch_size, channel, height, width)*
        /// - **weight**: *(num_filter, channel, kernel[0], kernel[1])*
        /// - **bias**: *(num_filter,)*
        /// - **out**: *(batch_size, num_filter, out_height, out_width)*.
        /// 
        /// Define::
        /// 
        ///   f(x,k,p,s,d) = floor((x+2*p-d*(k-1)-1)/s)+1
        /// 
        /// then we have::
        /// 
        ///   out_height=f(height, kernel[0], pad[0], stride[0], dilate[0])
        ///   out_width=f(width, kernel[1], pad[1], stride[1], dilate[1])
        /// 
        /// If ``no_bias`` is set to be true, then the ``bias`` term is ignored.
        /// 
        /// The default data ``layout`` is *NCHW*, namely *(batch_size, channel, height,
        /// width)*. We can choose other layouts such as *NWC*.
        /// 
        /// If ``num_group`` is larger than 1, denoted by *g*, then split the input ``data``
        /// evenly into *g* parts along the channel axis, and also evenly split ``weight``
        /// along the first dimension. Next compute the convolution on the *i*-th part of
        /// the data with the *i*-th weight part. The output is obtained by concatenating all
        /// the *g* results.
        /// 
        /// 1-D convolution does not have *height* dimension but only *width* in space.
        /// 
        /// - **data**: *(batch_size, channel, width)*
        /// - **weight**: *(num_filter, channel, kernel[0])*
        /// - **bias**: *(num_filter,)*
        /// - **out**: *(batch_size, num_filter, out_width)*.
        /// 
        /// 3-D convolution adds an additional *depth* dimension besides *height* and
        /// *width*. The shapes are
        /// 
        /// - **data**: *(batch_size, channel, depth, height, width)*
        /// - **weight**: *(num_filter, channel, kernel[0], kernel[1], kernel[2])*
        /// - **bias**: *(num_filter,)*
        /// - **out**: *(batch_size, num_filter, out_depth, out_height, out_width)*.
        /// 
        /// Both ``weight`` and ``bias`` are learnable parameters.
        /// 
        /// There are other options to tune the performance.
        /// 
        /// - **cudnn_tune**: enable this option leads to higher startup time but may give
        ///   faster speed. Options are
        /// 
        ///   - **off**: no tuning
        ///   - **limited_workspace**:run test and pick the fastest algorithm that doesn't
        ///     exceed workspace limit.
        ///   - **fastest**: pick the fastest algorithm and ignore workspace limit.
        ///   - **None** (default): the behavior is determined by environment variable
        ///     ``MXNET_CUDNN_AUTOTUNE_DEFAULT``. 0 for off, 1 for limited workspace
        ///     (default), 2 for fastest.
        /// 
        /// - **workspace**: A large number leads to more (GPU) memory usage but may improve
        ///   the performance.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\convolution.cc:L472
        /// </summary>
        /// <param name="data">Input data to the ConvolutionOp.</param>
        /// <param name="weight">Weight matrix.</param>
        /// <param name="bias">Bias parameter.</param>
        /// <param name="kernel">Convolution kernel size: (w,), (h, w) or (d, h, w)</param>
        /// <param name="stride">Convolution stride: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
        /// <param name="dilate">Convolution dilate: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
        /// <param name="pad">Zero pad for convolution: (w,), (h, w) or (d, h, w). Defaults to no padding.</param>
        /// <param name="num_filter">Convolution filter(channel) number</param>
        /// <param name="num_group">Number of group partitions.</param>
        /// <param name="workspace">Maximum temporary workspace allowed (MB) in convolution.This parameter has two usages. When CUDNN is not used, it determines the effective batch size of the convolution kernel. When CUDNN is used, it controls the maximum temporary storage used for tuning the best CUDNN kernel when `limited_workspace` strategy is used.</param>
        /// <param name="no_bias">Whether to disable bias parameter.</param>
        /// <param name="cudnn_tune">Whether to pick convolution algo by running performance test.</param>
        /// <param name="cudnn_off">Turn off cudnn for this layer.</param>
        /// <param name="layout">Set layout for input, output and weight. Empty for
        ///     default layout: NCW for 1d, NCHW for 2d and NCDHW for 3d.NHWC and NDHWC are only supported on GPU.</param>
        public static NDArray Convolution(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, int numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, int numGroup = 1, long workspace = 1024, bool noBias = false, string cudnnTune = null, bool cudnnOff = false, string layout = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Convolution",
                _ConvolutionParamNames,
                new[] { Convert(kernel), Convert(stride), Convert(dilate), Convert(pad), Convert(numFilter), Convert(numGroup), Convert(workspace), Convert(noBias), Convert(cudnnTune), Convert(cudnnOff), Convert(layout) },
                new[] { data.Handle, weight.Handle, bias.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardConvolutionParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardConvolution(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Convolution",
                _backwardConvolutionParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _CTCLossParamNames = new[] { "use_data_lengths", "use_label_lengths", "blank_label" };

        /// <summary>
        /// Connectionist Temporal Classification Loss.
        /// 
        /// .. note:: The existing alias ``contrib_CTCLoss`` is deprecated.
        /// 
        /// The shapes of the inputs and outputs:
        /// 
        /// - **data**: `(sequence_length, batch_size, alphabet_size)`
        /// - **label**: `(batch_size, label_sequence_length)`
        /// - **out**: `(batch_size)`
        /// 
        /// The `data` tensor consists of sequences of activation vectors (without applying softmax),
        /// with i-th channel in the last dimension corresponding to i-th label
        /// for i between 0 and alphabet_size-1 (i.e always 0-indexed).
        /// Alphabet size should include one additional value reserved for blank label.
        /// When `blank_label` is ``"first"``, the ``0``-th channel is be reserved for
        /// activation of blank label, or otherwise if it is "last", ``(alphabet_size-1)``-th channel should be
        /// reserved for blank label.
        /// 
        /// ``label`` is an index matrix of integers. When `blank_label` is ``"first"``,
        /// the value 0 is then reserved for blank label, and should not be passed in this matrix. Otherwise,
        /// when `blank_label` is ``"last"``, the value `(alphabet_size-1)` is reserved for blank label.
        /// 
        /// If a sequence of labels is shorter than *label_sequence_length*, use the special
        /// padding value at the end of the sequence to conform it to the correct
        /// length. The padding value is `0` when `blank_label` is ``"first"``, and `-1` otherwise.
        /// 
        /// For example, suppose the vocabulary is `[a, b, c]`, and in one batch we have three sequences
        /// 'ba', 'cbb', and 'abac'. When `blank_label` is ``"first"``, we can index the labels as
        /// `{'a': 1, 'b': 2, 'c': 3}`, and we reserve the 0-th channel for blank label in data tensor.
        /// The resulting `label` tensor should be padded to be::
        /// 
        ///   [[2, 1, 0, 0], [3, 2, 2, 0], [1, 2, 1, 3]]
        /// 
        /// When `blank_label` is ``"last"``, we can index the labels as
        /// `{'a': 0, 'b': 1, 'c': 2}`, and we reserve the channel index 3 for blank label in data tensor.
        /// The resulting `label` tensor should be padded to be::
        /// 
        ///   [[1, 0, -1, -1], [2, 1, 1, -1], [0, 1, 0, 2]]
        /// 
        /// ``out`` is a list of CTC loss values, one per example in the batch.
        /// 
        /// See *Connectionist Temporal Classification: Labelling Unsegmented
        /// Sequence Data with Recurrent Neural Networks*, A. Graves *et al*. for more
        /// information on the definition and the algorithm.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\ctc_loss.cc:L100
        /// </summary>
        /// <param name="data">Input ndarray</param>
        /// <param name="label">Ground-truth labels for the loss.</param>
        /// <param name="data_lengths">Lengths of data for each of the samples. Only required when use_data_lengths is true.</param>
        /// <param name="label_lengths">Lengths of labels for each of the samples. Only required when use_label_lengths is true.</param>
        /// <param name="use_data_lengths">Whether the data lenghts are decided by `data_lengths`. If false, the lengths are equal to the max sequence length.</param>
        /// <param name="use_label_lengths">Whether the label lenghts are decided by `label_lengths`, or derived from `padding_mask`. If false, the lengths are derived from the first occurrence of the value of `padding_mask`. The value of `padding_mask` is ``0`` when first CTC label is reserved for blank, and ``-1`` when last label is reserved for blank. See `blank_label`.</param>
        /// <param name="blank_label">Set the label that is reserved for blank label.If "first", 0-th label is reserved, and label values for tokens in the vocabulary are between ``1`` and ``alphabet_size-1``, and the padding mask is ``-1``. If "last", last label value ``alphabet_size-1`` is reserved for blank label instead, and label values for tokens in the vocabulary are between ``0`` and ``alphabet_size-2``, and the padding mask is ``0``.</param>
        public static NDArray CTCLoss(NDArrayOrSymbol data, NDArrayOrSymbol label, NDArrayOrSymbol dataLengths, NDArrayOrSymbol labelLengths, bool useDataLengths = false, bool useLabelLengths = false, string blankLabel = "first", NDArray output = null)
        {
            var result = Operator.Invoke(
                "CTCLoss",
                _CTCLossParamNames,
                new[] { Convert(useDataLengths), Convert(useLabelLengths), Convert(blankLabel) },
                new[] { data.Handle, label.Handle, dataLengths.Handle, labelLengths.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardCtcLossParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCtcLoss(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_ctc_loss",
                _backwardCtcLossParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _CuDNNBatchNormParamNames = new[] { "eps", "momentum", "fix_gamma", "use_global_stats", "output_mean_var", "axis", "cudnn_off" };

        /// <summary>
        /// Apply batch normalization to input.
        /// </summary>
        /// <param name="data">Input data to batch normalization</param>
        /// <param name="gamma">gamma array</param>
        /// <param name="beta">beta array</param>
        /// <param name="moving_mean">running mean of input</param>
        /// <param name="moving_var">running variance of input</param>
        /// <param name="eps">Epsilon to prevent div 0. Must be no less than CUDNN_BN_MIN_EPSILON defined in cudnn.h when using cudnn (usually 1e-5)</param>
        /// <param name="momentum">Momentum for moving average</param>
        /// <param name="fix_gamma">Fix gamma while training</param>
        /// <param name="use_global_stats">Whether use global moving statistics instead of local batch-norm. This will force change batch-norm into a scale shift operator.</param>
        /// <param name="output_mean_var">Output the mean and inverse std </param>
        /// <param name="axis">Specify which shape axis the channel is specified</param>
        /// <param name="cudnn_off">Do not select CUDNN operator, if available</param>
        public static NDArray CuDNNBatchNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, NDArrayOrSymbol movingMean, NDArrayOrSymbol movingVar, double eps = 0.0010000000474974513, double momentum = 0.899999976, bool fixGamma = true, bool useGlobalStats = false, bool outputMeanVar = false, int axis = 1, bool cudnnOff = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "CuDNNBatchNorm",
                _CuDNNBatchNormParamNames,
                new[] { Convert(eps), Convert(momentum), Convert(fixGamma), Convert(useGlobalStats), Convert(outputMeanVar), Convert(axis), Convert(cudnnOff) },
                new[] { data.Handle, gamma.Handle, beta.Handle, movingMean.Handle, movingVar.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardCuDNNBatchNormParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCuDNNBatchNorm(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_CuDNNBatchNorm",
                _backwardCuDNNBatchNormParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _DeconvolutionParamNames = new[] { "kernel", "stride", "dilate", "pad", "adj", "target_shape", "num_filter", "num_group", "workspace", "no_bias", "cudnn_tune", "cudnn_off", "layout" };

        /// <summary>
        /// Computes 1D or 2D transposed convolution (aka fractionally strided convolution) of the input tensor. This operation can be seen as the gradient of Convolution operation with respect to its input. Convolution usually reduces the size of the input. Transposed convolution works the other way, going from a smaller input to a larger output while preserving the connectivity pattern.
        /// </summary>
        /// <param name="data">Input tensor to the deconvolution operation.</param>
        /// <param name="weight">Weights representing the kernel.</param>
        /// <param name="bias">Bias added to the result after the deconvolution operation.</param>
        /// <param name="kernel">Deconvolution kernel size: (w,), (h, w) or (d, h, w). This is same as the kernel size used for the corresponding convolution</param>
        /// <param name="stride">The stride used for the corresponding convolution: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
        /// <param name="dilate">Dilation factor for each dimension of the input: (w,), (h, w) or (d, h, w). Defaults to 1 for each dimension.</param>
        /// <param name="pad">The amount of implicit zero padding added during convolution for each dimension of the input: (w,), (h, w) or (d, h, w). ``(kernel-1)/2`` is usually a good choice. If `target_shape` is set, `pad` will be ignored and a padding that will generate the target shape will be used. Defaults to no padding.</param>
        /// <param name="adj">Adjustment for output shape: (w,), (h, w) or (d, h, w). If `target_shape` is set, `adj` will be ignored and computed accordingly.</param>
        /// <param name="target_shape">Shape of the output tensor: (w,), (h, w) or (d, h, w).</param>
        /// <param name="num_filter">Number of output filters.</param>
        /// <param name="num_group">Number of groups partition.</param>
        /// <param name="workspace">Maximum temporary workspace allowed (MB) in deconvolution.This parameter has two usages. When CUDNN is not used, it determines the effective batch size of the deconvolution kernel. When CUDNN is used, it controls the maximum temporary storage used for tuning the best CUDNN kernel when `limited_workspace` strategy is used.</param>
        /// <param name="no_bias">Whether to disable bias parameter.</param>
        /// <param name="cudnn_tune">Whether to pick convolution algorithm by running performance test.</param>
        /// <param name="cudnn_off">Turn off cudnn for this layer.</param>
        /// <param name="layout">Set layout for input, output and weight. Empty for default layout, NCW for 1d, NCHW for 2d and NCDHW for 3d.NHWC and NDHWC are only supported on GPU.</param>
        public static NDArray Deconvolution(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, int numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, NDShape adj = null, NDShape targetShape = null, int numGroup = 1, long workspace = 512, bool noBias = true, string cudnnTune = null, bool cudnnOff = false, string layout = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Deconvolution",
                _DeconvolutionParamNames,
                new[] { Convert(kernel), Convert(stride), Convert(dilate), Convert(pad), Convert(adj), Convert(targetShape), Convert(numFilter), Convert(numGroup), Convert(workspace), Convert(noBias), Convert(cudnnTune), Convert(cudnnOff), Convert(layout) },
                new[] { data.Handle, weight.Handle, bias.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardDeconvolutionParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardDeconvolution(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Deconvolution",
                _backwardDeconvolutionParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _DropoutParamNames = new[] { "p", "mode", "axes", "cudnn_off" };

        /// <summary>
        /// Applies dropout operation to input array.
        /// 
        /// - During training, each element of the input is set to zero with probability p.
        ///   The whole array is rescaled by :math:`1/(1-p)` to keep the expected
        ///   sum of the input unchanged.
        /// 
        /// - During testing, this operator does not change the input if mode is 'training'.
        ///   If mode is 'always', the same computaion as during training will be applied.
        /// 
        /// Example::
        /// 
        ///   random.seed(998)
        ///   input_array = array([[3., 0.5,  -0.5,  2., 7.],
        ///                       [2., -0.4,   7.,  3., 0.2]])
        ///   a = symbol.Variable('a')
        ///   dropout = symbol.Dropout(a, p = 0.2)
        ///   executor = dropout.simple_bind(a = input_array.shape)
        /// 
        ///   ## If training
        ///   executor.forward(is_train = True, a = input_array)
        ///   executor.outputs
        ///   [[ 3.75   0.625 -0.     2.5    8.75 ]
        ///    [ 2.5   -0.5    8.75   3.75   0.   ]]
        /// 
        ///   ## If testing
        ///   executor.forward(is_train = False, a = input_array)
        ///   executor.outputs
        ///   [[ 3.     0.5   -0.5    2.     7.   ]
        ///    [ 2.    -0.4    7.     3.     0.2  ]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\dropout.cc:L97
        /// </summary>
        /// <param name="data">Input array to which dropout will be applied.</param>
        /// <param name="p">Fraction of the input that gets dropped out during training time.</param>
        /// <param name="mode">Whether to only turn on dropout during training or to also turn on for inference.</param>
        /// <param name="axes">Axes for variational dropout kernel.</param>
        /// <param name="cudnn_off">Whether to turn off cudnn in dropout operator. This option is ignored if axes is specified.</param>
        public static NDArray Dropout(NDArrayOrSymbol data, double p = 0.5, string mode = "training", NDShape axes = null, bool? cudnnOff = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Dropout",
                _DropoutParamNames,
                new[] { Convert(p), Convert(mode), Convert(axes), Convert(cudnnOff) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardDropoutParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardDropout(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Dropout",
                _backwardDropoutParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _FullyConnectedParamNames = new[] { "num_hidden", "no_bias", "flatten" };

        /// <summary>
        /// Applies a linear transformation: :math:`Y = XW^T + b`.
        /// 
        /// If ``flatten`` is set to be true, then the shapes are:
        /// 
        /// - **data**: `(batch_size, x1, x2, ..., xn)`
        /// - **weight**: `(num_hidden, x1 * x2 * ... * xn)`
        /// - **bias**: `(num_hidden,)`
        /// - **out**: `(batch_size, num_hidden)`
        /// 
        /// If ``flatten`` is set to be false, then the shapes are:
        /// 
        /// - **data**: `(x1, x2, ..., xn, input_dim)`
        /// - **weight**: `(num_hidden, input_dim)`
        /// - **bias**: `(num_hidden,)`
        /// - **out**: `(x1, x2, ..., xn, num_hidden)`
        /// 
        /// The learnable parameters include both ``weight`` and ``bias``.
        /// 
        /// If ``no_bias`` is set to be true, then the ``bias`` term is ignored.
        /// 
        /// .. Note::
        /// 
        ///     The sparse support for FullyConnected is limited to forward evaluation with `row_sparse`
        ///     weight and bias, where the length of `weight.indices` and `bias.indices` must be equal
        ///     to `num_hidden`. This could be useful for model inference with `row_sparse` weights
        ///     trained with importance sampling or noise contrastive estimation.
        /// 
        ///     To compute linear transformation with 'csr' sparse data, sparse.dot is recommended instead
        ///     of sparse.FullyConnected.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\fully_connected.cc:L277
        /// </summary>
        /// <param name="data">Input data.</param>
        /// <param name="weight">Weight matrix.</param>
        /// <param name="bias">Bias parameter.</param>
        /// <param name="num_hidden">Number of hidden nodes of the output.</param>
        /// <param name="no_bias">Whether to disable bias parameter.</param>
        /// <param name="flatten">Whether to collapse all but the first axis of the input data tensor.</param>
        public static NDArray FullyConnected(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, int numHidden, bool noBias = false, bool flatten = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "FullyConnected",
                _FullyConnectedParamNames,
                new[] { Convert(numHidden), Convert(noBias), Convert(flatten) },
                new[] { data.Handle, weight.Handle, bias.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardFullyConnectedParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardFullyConnected(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_FullyConnected",
                _backwardFullyConnectedParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _LayerNormParamNames = new[] { "axis", "eps", "output_mean_var" };

        /// <summary>
        /// Layer normalization.
        /// 
        /// Normalizes the channels of the input tensor by mean and variance, and applies a scale ``gamma`` as
        /// well as offset ``beta``.
        /// 
        /// Assume the input has more than one dimension and we normalize along axis 1.
        /// We first compute the mean and variance along this axis and then 
        /// compute the normalized output, which has the same shape as input, as following:
        /// 
        /// .. math::
        /// 
        ///   out = \frac{data - mean(data, axis)}{\sqrt{var(data, axis) + \epsilon}} * gamma + beta
        /// 
        /// Both ``gamma`` and ``beta`` are learnable parameters.
        /// 
        /// Unlike BatchNorm and InstanceNorm,  the *mean* and *var* are computed along the channel dimension.
        /// 
        /// Assume the input has size *k* on axis 1, then both ``gamma`` and ``beta``
        /// have shape *(k,)*. If ``output_mean_var`` is set to be true, then outputs both ``data_mean`` and
        /// ``data_std``. Note that no gradient will be passed through these two outputs.
        /// 
        /// The parameter ``axis`` specifies which axis of the input shape denotes
        /// the 'channel' (separately normalized groups).  The default is -1, which sets the channel
        /// axis to be the last item in the input shape.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\layer_norm.cc:L155
        /// </summary>
        /// <param name="data">Input data to layer normalization</param>
        /// <param name="gamma">gamma array</param>
        /// <param name="beta">beta array</param>
        /// <param name="axis">The axis to perform layer normalization. Usually, this should be be axis of the channel dimension. Negative values means indexing from right to left.</param>
        /// <param name="eps">An `epsilon` parameter to prevent division by 0.</param>
        /// <param name="output_mean_var">Output the mean and std calculated along the given axis.</param>
        public static NDArray LayerNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, int axis = -1, double eps = 9.99999975e-06, bool outputMeanVar = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LayerNorm",
                _LayerNormParamNames,
                new[] { Convert(axis), Convert(eps), Convert(outputMeanVar) },
                new[] { data.Handle, gamma.Handle, beta.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLayerNormParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLayerNorm(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_LayerNorm",
                _backwardLayerNormParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _LRNParamNames = new[] { "alpha", "beta", "knorm", "nsize" };

        /// <summary>
        /// Applies local response normalization to the input.
        /// 
        /// The local response normalization layer performs "lateral inhibition" by normalizing
        /// over local input regions.
        /// 
        /// If :math:`a_{x,y}^{i}` is the activity of a neuron computed by applying kernel :math:`i` at position
        /// :math:`(x, y)` and then applying the ReLU nonlinearity, the response-normalized
        /// activity :math:`b_{x,y}^{i}` is given by the expression:
        /// 
        /// .. math::
        ///    b_{x,y}^{i} = \frac{a_{x,y}^{i}}{\Bigg({k + \frac{\alpha}{n} \sum_{j=max(0, i-\frac{n}{2})}^{min(N-1, i+\frac{n}{2})} (a_{x,y}^{j})^{2}}\Bigg)^{\beta}}
        /// 
        /// where the sum runs over :math:`n` "adjacent" kernel maps at the same spatial position, and :math:`N` is the total
        /// number of kernels in the layer.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\lrn.cc:L164
        /// </summary>
        /// <param name="data">Input data to LRN</param>
        /// <param name="alpha">The variance scaling parameter :math:`lpha` in the LRN expression.</param>
        /// <param name="beta">The power parameter :math:`eta` in the LRN expression.</param>
        /// <param name="knorm">The parameter :math:`k` in the LRN expression.</param>
        /// <param name="nsize">normalization window width in elements.</param>
        public static NDArray LRN(NDArrayOrSymbol data, int nsize, double alpha = 9.99999975e-05, double beta = 0.75, double knorm = 2, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LRN",
                _LRNParamNames,
                new[] { Convert(alpha), Convert(beta), Convert(knorm), Convert(nsize) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLRNParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLRN(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_LRN",
                _backwardLRNParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _momentsParamNames = new[] { "axes", "keepdims" };

        /// <summary>
        /// 
        /// Calculate the mean and variance of `data`.
        /// 
        /// The mean and variance are calculated by aggregating the contents of data across axes.
        /// If x is 1-D and axes = [0] this is just the mean and variance of a vector.
        /// 
        /// Example:
        /// 
        ///      x = [[1, 2, 3], [4, 5, 6]]
        ///      mean, var = moments(data=x, axes=[0])
        ///      mean = [2.5, 3.5, 4.5]
        ///      var = [2.25, 2.25, 2.25]
        ///      mean, var = moments(data=x, axes=[1])
        ///      mean = [2.0, 5.0]
        ///      var = [0.66666667, 0.66666667]
        ///      mean, var = moments(data=x, axis=[0, 1])
        ///      mean = [3.5]
        ///      var = [2.9166667]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\moments.cc:L54
        /// </summary>
        /// <param name="data">Input ndarray</param>
        /// <param name="axes">Array of ints. Axes along which to compute mean and variance.</param>
        /// <param name="keepdims">produce moments with the same dimensionality as the input.</param>
        public static NDArray Moments(NDArrayOrSymbol data, NDShape axes = null, bool keepdims = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "moments",
                _momentsParamNames,
                new[] { Convert(axes), Convert(keepdims) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMomentsParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMoments(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_moments",
                _backwardMomentsParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _PoolingParamNames = new[] { "kernel", "pool_type", "global_pool", "cudnn_off", "pooling_convention", "stride", "pad", "p_value", "count_include_pad", "layout" };

        /// <summary>
        /// Performs pooling on the input.
        /// 
        /// The shapes for 1-D pooling are
        /// 
        /// - **data** and **out**: *(batch_size, channel, width)* (NCW layout) or
        ///   *(batch_size, width, channel)* (NWC layout),
        /// 
        /// The shapes for 2-D pooling are
        /// 
        /// - **data** and **out**: *(batch_size, channel, height, width)* (NCHW layout) or
        ///   *(batch_size, height, width, channel)* (NHWC layout),
        /// 
        ///     out_height = f(height, kernel[0], pad[0], stride[0])
        ///     out_width = f(width, kernel[1], pad[1], stride[1])
        /// 
        /// The definition of *f* depends on ``pooling_convention``, which has two options:
        /// 
        /// - **valid** (default)::
        /// 
        ///     f(x, k, p, s) = floor((x+2*p-k)/s)+1
        /// 
        /// - **full**, which is compatible with Caffe::
        /// 
        ///     f(x, k, p, s) = ceil((x+2*p-k)/s)+1
        /// 
        /// But ``global_pool`` is set to be true, then do a global pooling, namely reset
        /// ``kernel=(height, width)``.
        /// 
        /// Three pooling options are supported by ``pool_type``:
        /// 
        /// - **avg**: average pooling
        /// - **max**: max pooling
        /// - **sum**: sum pooling
        /// - **lp**: Lp pooling
        /// 
        /// For 3-D pooling, an additional *depth* dimension is added before
        /// *height*. Namely the input data and output will have shape *(batch_size, channel, depth,
        /// height, width)* (NCDHW layout) or *(batch_size, depth, height, width, channel)* (NDHWC layout).
        /// 
        /// Notes on Lp pooling:
        /// 
        /// Lp pooling was first introduced by this paper: https://arxiv.org/pdf/1204.3968.pdf.
        /// L-1 pooling is simply sum pooling, while L-inf pooling is simply max pooling.
        /// We can see that Lp pooling stands between those two, in practice the most common value for p is 2.
        /// 
        /// For each window ``X``, the mathematical expression for Lp pooling is:
        /// 
        /// :math:`f(X) = \sqrt[p]{\sum_{x}^{X} x^p}`
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\pooling.cc:L416
        /// </summary>
        /// <param name="data">Input data to the pooling operator.</param>
        /// <param name="kernel">Pooling kernel size: (y, x) or (d, y, x)</param>
        /// <param name="pool_type">Pooling type to be applied.</param>
        /// <param name="global_pool">Ignore kernel size, do global pooling based on current input feature map. </param>
        /// <param name="cudnn_off">Turn off cudnn pooling and use MXNet pooling operator. </param>
        /// <param name="pooling_convention">Pooling convention to be applied.</param>
        /// <param name="stride">Stride: for pooling (y, x) or (d, y, x). Defaults to 1 for each dimension.</param>
        /// <param name="pad">Pad for pooling: (y, x) or (d, y, x). Defaults to no padding.</param>
        /// <param name="p_value">Value of p for Lp pooling, can be 1 or 2, required for Lp Pooling.</param>
        /// <param name="count_include_pad">Only used for AvgPool, specify whether to count padding elements for averagecalculation. For example, with a 5*5 kernel on a 3*3 corner of a image,the sum of the 9 valid elements will be divided by 25 if this is set to true,or it will be divided by 9 if this is set to false. Defaults to true.</param>
        /// <param name="layout">Set layout for input and output. Empty for
        ///     default layout: NCW for 1d, NCHW for 2d and NCDHW for 3d.</param>
        public static NDArray Pooling(NDArrayOrSymbol data, NDShape kernel = null, string poolType = "max", bool globalPool = false, bool cudnnOff = false, string poolingConvention = "valid", NDShape stride = null, NDShape pad = null, int? pValue = null, bool? countIncludePad = null, string layout = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Pooling",
                _PoolingParamNames,
                new[] { Convert(kernel), Convert(poolType), Convert(globalPool), Convert(cudnnOff), Convert(poolingConvention), Convert(stride), Convert(pad), Convert(pValue), Convert(countIncludePad), Convert(layout) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardPoolingParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardPooling(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Pooling",
                _backwardPoolingParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _softmaxParamNames = new[] { "axis", "temperature", "dtype" };

        /// <summary>
        /// Applies the softmax function.
        /// 
        /// The resulting array contains elements in the range (0,1) and the elements along the given axis sum up to 1.
        /// 
        /// .. math::
        ///    softmax(\mathbf{z/t})_j = \frac{e^{z_j/t}}{\sum_{k=1}^K e^{z_k/t}}
        /// 
        /// for :math:`j = 1, ..., K`
        /// 
        /// t is the temperature parameter in softmax function. By default, t equals 1.0
        /// 
        /// Example::
        /// 
        ///   x = [[ 1.  1.  1.]
        ///        [ 1.  1.  1.]]
        /// 
        ///   softmax(x,axis=0) = [[ 0.5  0.5  0.5]
        ///                        [ 0.5  0.5  0.5]]
        /// 
        ///   softmax(x,axis=1) = [[ 0.33333334,  0.33333334,  0.33333334],
        ///                        [ 0.33333334,  0.33333334,  0.33333334]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\softmax.cc:L93
        /// </summary>
        /// <param name="data">The input array.</param>
        /// <param name="axis">The axis along which to compute softmax.</param>
        /// <param name="temperature">Temperature parameter in softmax</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to the same as input's dtype if not defined (dtype=None).</param>
        public static NDArray Softmax(NDArrayOrSymbol data, int axis = -1, double? temperature = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "softmax",
                _softmaxParamNames,
                new[] { Convert(axis), Convert(temperature), Convert(dtype) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _softminParamNames = new[] { "axis", "temperature", "dtype" };

        /// <summary>
        /// Applies the softmin function.
        /// 
        /// The resulting array contains elements in the range (0,1) and the elements along the given axis sum
        /// up to 1.
        /// 
        /// .. math::
        ///    softmin(\mathbf{z/t})_j = \frac{e^{-z_j/t}}{\sum_{k=1}^K e^{-z_k/t}}
        /// 
        /// for :math:`j = 1, ..., K`
        /// 
        /// t is the temperature parameter in softmax function. By default, t equals 1.0
        /// 
        /// Example::
        /// 
        ///   x = [[ 1.  2.  3.]
        ///        [ 3.  2.  1.]]
        /// 
        ///   softmin(x,axis=0) = [[ 0.88079703,  0.5,  0.11920292],
        ///                        [ 0.11920292,  0.5,  0.88079703]]
        /// 
        ///   softmin(x,axis=1) = [[ 0.66524094,  0.24472848,  0.09003057],
        ///                        [ 0.09003057,  0.24472848,  0.66524094]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\softmax.cc:L153
        /// </summary>
        /// <param name="data">The input array.</param>
        /// <param name="axis">The axis along which to compute softmax.</param>
        /// <param name="temperature">Temperature parameter in softmax</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to the same as input's dtype if not defined (dtype=None).</param>
        public static NDArray Softmin(NDArrayOrSymbol data, int axis = -1, double? temperature = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "softmin",
                _softminParamNames,
                new[] { Convert(axis), Convert(temperature), Convert(dtype) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _logSoftmaxParamNames = new[] { "axis", "temperature", "dtype" };

        /// <summary>
        /// Computes the log softmax of the input.
        /// This is equivalent to computing softmax followed by log.
        /// 
        /// Examples::
        /// 
        ///   >>> x = mx.nd.array([1, 2, .1])
        ///   >>> mx.nd.log_softmax(x).asnumpy()
        ///   array([-1.41702998, -0.41702995, -2.31702995], dtype=float32)
        /// 
        ///   >>> x = mx.nd.array( [[1, 2, .1],[.1, 2, 1]] )
        ///   >>> mx.nd.log_softmax(x, axis=0).asnumpy()
        ///   array([[-0.34115392, -0.69314718, -1.24115396],
        ///          [-1.24115396, -0.69314718, -0.34115392]], dtype=float32)
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="data">The input array.</param>
        /// <param name="axis">The axis along which to compute softmax.</param>
        /// <param name="temperature">Temperature parameter in softmax</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to the same as input's dtype if not defined (dtype=None).</param>
        public static NDArray LogSoftmax(NDArrayOrSymbol data, int axis = -1, double? temperature = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log_softmax",
                _logSoftmaxParamNames,
                new[] { Convert(axis), Convert(temperature), Convert(dtype) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _SoftmaxActivationParamNames = new[] { "mode" };

        /// <summary>
        /// Applies softmax activation to input. This is intended for internal layers.
        /// 
        /// .. note::
        /// 
        ///   This operator has been deprecated, please use `softmax`.
        /// 
        /// If `mode` = ``instance``, this operator will compute a softmax for each instance in the batch.
        /// This is the default mode.
        /// 
        /// If `mode` = ``channel``, this operator will compute a k-class softmax at each position
        /// of each instance, where `k` = ``num_channel``. This mode can only be used when the input array
        /// has at least 3 dimensions.
        /// This can be used for `fully convolutional network`, `image segmentation`, etc.
        /// 
        /// Example::
        /// 
        ///   >>> input_array = mx.nd.array([[3., 0.5, -0.5, 2., 7.],
        ///   >>>                            [2., -.4, 7.,   3., 0.2]])
        ///   >>> softmax_act = mx.nd.SoftmaxActivation(input_array)
        ///   >>> print softmax_act.asnumpy()
        ///   [[  1.78322066e-02   1.46375655e-03   5.38485940e-04   6.56010211e-03   9.73605454e-01]
        ///    [  6.56221947e-03   5.95310994e-04   9.73919690e-01   1.78379621e-02   1.08472735e-03]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\nn\softmax_activation.cc:L59
        /// </summary>
        /// <param name="data">The input array.</param>
        /// <param name="mode">Specifies how to compute the softmax. If set to ``instance``, it computes softmax for each instance. If set to ``channel``, It computes cross channel softmax for each position of each instance.</param>
        public static NDArray SoftmaxActivation(NDArrayOrSymbol data, string mode = "instance", NDArray output = null)
        {
            var result = Operator.Invoke(
                "SoftmaxActivation",
                _SoftmaxActivationParamNames,
                new[] { Convert(mode) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSoftmaxActivationParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSoftmaxActivation(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SoftmaxActivation",
                _backwardSoftmaxActivationParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardUpSamplingParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardUpSampling(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_UpSampling",
                _backwardUpSamplingParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _signsgdUpdateParamNames = new[] { "lr", "wd", "rescale_grad", "clip_gradient" };

        /// <summary>
        /// Update function for SignSGD optimizer.
        /// 
        /// .. math::
        /// 
        ///  g_t = \nabla J(W_{t-1})\\
        ///  W_t = W_{t-1} - \eta_t \text{sign}(g_t)
        /// 
        /// It updates the weights using::
        /// 
        ///  weight = weight - learning_rate * sign(gradient)
        /// 
        /// .. note::
        ///    - sparse ndarray not supported for this optimizer yet.
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L61
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        public static NDArray SignsgdUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, double lr, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "signsgd_update",
                _signsgdUpdateParamNames,
                new[] { Convert(lr), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight.Handle, grad.Handle },
                output
            );
            return result;
        }

        private static string[] _signumUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient", "wd_lh" };

        /// <summary>
        /// SIGN momentUM (Signum) optimizer.
        /// 
        /// .. math::
        /// 
        ///  g_t = \nabla J(W_{t-1})\\
        ///  m_t = \beta m_{t-1} + (1 - \beta) g_t\\
        ///  W_t = W_{t-1} - \eta_t \text{sign}(m_t)
        /// 
        /// It updates the weights using::
        ///  state = momentum * state + (1-momentum) * gradient
        ///  weight = weight - learning_rate * sign(state)
        /// 
        /// Where the parameter ``momentum`` is the decay rate of momentum estimates at each epoch.
        /// 
        /// .. note::
        ///    - sparse ndarray not supported for this optimizer yet.
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L90
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="mom">Momentum</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="momentum">The decay rate of momentum estimates at each epoch.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        /// <param name="wd_lh">The amount of weight decay that does not go into gradient/momentum calculationsotherwise do weight decay algorithmically only.</param>
        public static NDArray SignumUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, double lr, double momentum = 0, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, double wdLh = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "signum_update",
                _signumUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(wdLh) },
                new[] { weight.Handle, grad.Handle, mom.Handle },
                output
            );
            return result;
        }

        private static string[] _sgdUpdateParamNames = new[] { "lr", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        /// <summary>
        /// Update function for Stochastic Gradient Descent (SGD) optimizer.
        /// 
        /// It updates the weights using::
        /// 
        ///  weight = weight - learning_rate * (gradient + wd * weight)
        /// 
        /// However, if gradient is of ``row_sparse`` storage type and ``lazy_update`` is True,
        /// only the row slices whose indices appear in grad.indices are updated::
        /// 
        ///  for row in gradient.indices:
        ///      weight[row] = weight[row] - learning_rate * (gradient[row] + wd * weight[row])
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L522
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        /// <param name="lazy_update">If true, lazy updates are applied if gradient's stype is row_sparse.</param>
        public static NDArray SgdUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, double lr, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sgd_update",
                _sgdUpdateParamNames,
                new[] { Convert(lr), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight.Handle, grad.Handle },
                output
            );
            return result;
        }

        private static string[] _sgdMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        /// <summary>
        /// Momentum update function for Stochastic Gradient Descent (SGD) optimizer.
        /// 
        /// Momentum update has better convergence rates on neural networks. Mathematically it looks
        /// like below:
        /// 
        /// .. math::
        /// 
        ///   v_1 = \alpha * \nabla J(W_0)\\
        ///   v_t = \gamma v_{t-1} - \alpha * \nabla J(W_{t-1})\\
        ///   W_t = W_{t-1} + v_t
        /// 
        /// It updates the weights using::
        /// 
        ///   v = momentum * v - learning_rate * gradient
        ///   weight += v
        /// 
        /// Where the parameter ``momentum`` is the decay rate of momentum estimates at each epoch.
        /// 
        /// However, if grad's storage type is ``row_sparse``, ``lazy_update`` is True and weight's storage
        /// type is the same as momentum's storage type,
        /// only the row slices whose indices appear in grad.indices are updated (for both weight and momentum)::
        /// 
        ///   for row in gradient.indices:
        ///       v[row] = momentum[row] * v[row] - learning_rate * gradient[row]
        ///       weight[row] += v[row]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L563
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="mom">Momentum</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="momentum">The decay rate of momentum estimates at each epoch.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        /// <param name="lazy_update">If true, lazy updates are applied if gradient's stype is row_sparse and both weight and momentum have the same stype</param>
        public static NDArray SgdMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, double lr, double momentum = 0, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sgd_mom_update",
                _sgdMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight.Handle, grad.Handle, mom.Handle },
                output
            );
            return result;
        }

        private static string[] _mpSgdUpdateParamNames = new[] { "lr", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        /// <summary>
        /// Updater function for multi-precision sgd optimizer
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">gradient</param>
        /// <param name="weight32">Weight32</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        /// <param name="lazy_update">If true, lazy updates are applied if gradient's stype is row_sparse.</param>
        public static NDArray MpSgdUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol weight32, double lr, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "mp_sgd_update",
                _mpSgdUpdateParamNames,
                new[] { Convert(lr), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight.Handle, grad.Handle, weight32.Handle },
                output
            );
            return result;
        }

        private static string[] _mpSgdMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        /// <summary>
        /// Updater function for multi-precision sgd optimizer
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="mom">Momentum</param>
        /// <param name="weight32">Weight32</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="momentum">The decay rate of momentum estimates at each epoch.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        /// <param name="lazy_update">If true, lazy updates are applied if gradient's stype is row_sparse and both weight and momentum have the same stype</param>
        public static NDArray MpSgdMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, double lr, double momentum = 0, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "mp_sgd_mom_update",
                _mpSgdMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight.Handle, grad.Handle, mom.Handle, weight32.Handle },
                output
            );
            return result;
        }

        private static string[] _ftmlUpdateParamNames = new[] { "lr", "beta1", "beta2", "epsilon", "t", "wd", "rescale_grad", "clip_grad" };

        /// <summary>
        /// The FTML optimizer described in
        /// *FTML - Follow the Moving Leader in Deep Learning*,
        /// available at http://proceedings.mlr.press/v70/zheng17a/zheng17a.pdf.
        /// 
        /// .. math::
        /// 
        ///  g_t = \nabla J(W_{t-1})\\
        ///  v_t = \beta_2 v_{t-1} + (1 - \beta_2) g_t^2\\
        ///  d_t = \frac{ 1 - \beta_1^t }{ \eta_t } (\sqrt{ \frac{ v_t }{ 1 - \beta_2^t } } + \epsilon)
        ///  \sigma_t = d_t - \beta_1 d_{t-1}
        ///  z_t = \beta_1 z_{ t-1 } + (1 - \beta_1^t) g_t - \sigma_t W_{t-1}
        ///  W_t = - \frac{ z_t }{ d_t }
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L638
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="d">Internal state ``d_t``</param>
        /// <param name="v">Internal state ``v_t``</param>
        /// <param name="z">Internal state ``z_t``</param>
        /// <param name="lr">Learning rate.</param>
        /// <param name="beta1">Generally close to 0.5.</param>
        /// <param name="beta2">Generally close to 1.</param>
        /// <param name="epsilon">Epsilon to prevent div 0.</param>
        /// <param name="t">Number of update.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_grad">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        public static NDArray FtmlUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol d, NDArrayOrSymbol v, NDArrayOrSymbol z, double lr, int t, double beta1 = 0.600000024, double beta2 = 0.999000013, double epsilon = 9.9999999392252903e-09, double wd = 0, double rescaleGrad = 1, double clipGrad = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ftml_update",
                _ftmlUpdateParamNames,
                new[] { Convert(lr), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(t), Convert(wd), Convert(rescaleGrad), Convert(clipGrad) },
                new[] { weight.Handle, grad.Handle, d.Handle, v.Handle, z.Handle },
                output
            );
            return result;
        }

        private static string[] _adamUpdateParamNames = new[] { "lr", "beta1", "beta2", "epsilon", "wd", "rescale_grad", "clip_gradient", "lazy_update" };

        /// <summary>
        /// Update function for Adam optimizer. Adam is seen as a generalization
        /// of AdaGrad.
        /// 
        /// Adam update consists of the following steps, where g represents gradient and m, v
        /// are 1st and 2nd order moment estimates (mean and variance).
        /// 
        /// .. math::
        /// 
        ///  g_t = \nabla J(W_{t-1})\\
        ///  m_t = \beta_1 m_{t-1} + (1 - \beta_1) g_t\\
        ///  v_t = \beta_2 v_{t-1} + (1 - \beta_2) g_t^2\\
        ///  W_t = W_{t-1} - \alpha \frac{ m_t }{ \sqrt{ v_t } + \epsilon }
        /// 
        /// It updates the weights using::
        /// 
        ///  m = beta1*m + (1-beta1)*grad
        ///  v = beta2*v + (1-beta2)*(grad**2)
        ///  w += - learning_rate * m / (sqrt(v) + epsilon)
        /// 
        /// However, if grad's storage type is ``row_sparse``, ``lazy_update`` is True and the storage
        /// type of weight is the same as those of m and v,
        /// only the row slices whose indices appear in grad.indices are updated (for w, m and v)::
        /// 
        ///  for row in grad.indices:
        ///      m[row] = beta1*m[row] + (1-beta1)*grad[row]
        ///      v[row] = beta2*v[row] + (1-beta2)*(grad[row]**2)
        ///      w[row] += - learning_rate * m[row] / (sqrt(v[row]) + epsilon)
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L686
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="mean">Moving mean</param>
        /// <param name="var">Moving variance</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="beta1">The decay rate for the 1st moment estimates.</param>
        /// <param name="beta2">The decay rate for the 2nd moment estimates.</param>
        /// <param name="epsilon">A small constant for numerical stability.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        /// <param name="lazy_update">If true, lazy updates are applied if gradient's stype is row_sparse and all of w, m and v have the same stype</param>
        public static NDArray AdamUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mean, NDArrayOrSymbol var, double lr, double beta1 = 0.899999976, double beta2 = 0.999000013, double epsilon = 9.99999994e-09, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, bool lazyUpdate = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "adam_update",
                _adamUpdateParamNames,
                new[] { Convert(lr), Convert(beta1), Convert(beta2), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(lazyUpdate) },
                new[] { weight.Handle, grad.Handle, mean.Handle, var.Handle },
                output
            );
            return result;
        }

        private static string[] _nagMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient" };

        /// <summary>
        /// Update function for Nesterov Accelerated Gradient( NAG) optimizer.
        /// It updates the weights using the following formula,
        /// 
        /// .. math::
        ///   v_t = \gamma v_{t-1} + \eta * \nabla J(W_{t-1} - \gamma v_{t-1})\\
        ///   W_t = W_{t-1} - v_t
        /// 
        /// Where 
        /// :math:`\eta` is the learning rate of the optimizer
        /// :math:`\gamma` is the decay rate of the momentum estimate
        /// :math:`\v_t` is the update vector at time step `t`
        /// :math:`\W_t` is the weight vector at time step `t`
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L724
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="mom">Momentum</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="momentum">The decay rate of momentum estimates at each epoch.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        public static NDArray NagMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, double lr, double momentum = 0, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "nag_mom_update",
                _nagMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight.Handle, grad.Handle, mom.Handle },
                output
            );
            return result;
        }

        private static string[] _mpNagMomUpdateParamNames = new[] { "lr", "momentum", "wd", "rescale_grad", "clip_gradient" };

        /// <summary>
        /// Update function for multi-precision Nesterov Accelerated Gradient( NAG) optimizer.
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L743
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="mom">Momentum</param>
        /// <param name="weight32">Weight32</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="momentum">The decay rate of momentum estimates at each epoch.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        public static NDArray MpNagMomUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol mom, NDArrayOrSymbol weight32, double lr, double momentum = 0, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "mp_nag_mom_update",
                _mpNagMomUpdateParamNames,
                new[] { Convert(lr), Convert(momentum), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight.Handle, grad.Handle, mom.Handle, weight32.Handle },
                output
            );
            return result;
        }

        private static string[] _rmspropUpdateParamNames = new[] { "lr", "gamma1", "epsilon", "wd", "rescale_grad", "clip_gradient", "clip_weights" };

        /// <summary>
        /// Update function for `RMSProp` optimizer.
        /// 
        /// `RMSprop` is a variant of stochastic gradient descent where the gradients are
        /// divided by a cache which grows with the sum of squares of recent gradients?
        /// 
        /// `RMSProp` is similar to `AdaGrad`, a popular variant of `SGD` which adaptively
        /// tunes the learning rate of each parameter. `AdaGrad` lowers the learning rate for
        /// each parameter monotonically over the course of training.
        /// While this is analytically motivated for convex optimizations, it may not be ideal
        /// for non-convex problems. `RMSProp` deals with this heuristically by allowing the
        /// learning rates to rebound as the denominator decays over time.
        /// 
        /// Define the Root Mean Square (RMS) error criterion of the gradient as
        /// :math:`RMS[g]_t = \sqrt{E[g^2]_t + \epsilon}`, where :math:`g` represents
        /// gradient and :math:`E[g^2]_t` is the decaying average over past squared gradient.
        /// 
        /// The :math:`E[g^2]_t` is given by:
        /// 
        /// .. math::
        ///   E[g^2]_t = \gamma * E[g^2]_{t-1} + (1-\gamma) * g_t^2
        /// 
        /// The update step is
        /// 
        /// .. math::
        ///   \theta_{t+1} = \theta_t - \frac{\eta}{RMS[g]_t} g_t
        /// 
        /// The RMSProp code follows the version in
        /// http://www.cs.toronto.edu/~tijmen/csc321/slides/lecture_slides_lec6.pdf
        /// Tieleman & Hinton, 2012.
        /// 
        /// Hinton suggests the momentum term :math:`\gamma` to be 0.9 and the learning rate
        /// :math:`\eta` to be 0.001.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L795
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="n">n</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="gamma1">The decay rate of momentum estimates.</param>
        /// <param name="epsilon">A small constant for numerical stability.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        /// <param name="clip_weights">Clip weights to the range of [-clip_weights, clip_weights] If clip_weights <= 0, weight clipping is turned off. weights = max(min(weights, clip_weights), -clip_weights).</param>
        public static NDArray RmspropUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol n, double lr, double gamma1 = 0.949999988, double epsilon = 9.99999994e-09, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, double clipWeights = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rmsprop_update",
                _rmspropUpdateParamNames,
                new[] { Convert(lr), Convert(gamma1), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(clipWeights) },
                new[] { weight.Handle, grad.Handle, n.Handle },
                output
            );
            return result;
        }

        private static string[] _rmspropalexUpdateParamNames = new[] { "lr", "gamma1", "gamma2", "epsilon", "wd", "rescale_grad", "clip_gradient", "clip_weights" };

        /// <summary>
        /// Update function for RMSPropAlex optimizer.
        /// 
        /// `RMSPropAlex` is non-centered version of `RMSProp`.
        /// 
        /// Define :math:`E[g^2]_t` is the decaying average over past squared gradient and
        /// :math:`E[g]_t` is the decaying average over past gradient.
        /// 
        /// .. math::
        ///   E[g^2]_t = \gamma_1 * E[g^2]_{t-1} + (1 - \gamma_1) * g_t^2\\
        ///   E[g]_t = \gamma_1 * E[g]_{t-1} + (1 - \gamma_1) * g_t\\
        ///   \Delta_t = \gamma_2 * \Delta_{t-1} - \frac{\eta}{\sqrt{E[g^2]_t - E[g]_t^2 + \epsilon}} g_t\\
        /// 
        /// The update step is
        /// 
        /// .. math::
        ///   \theta_{t+1} = \theta_t + \Delta_t
        /// 
        /// The RMSPropAlex code follows the version in
        /// http://arxiv.org/pdf/1308.0850v5.pdf Eq(38) - Eq(45) by Alex Graves, 2013.
        /// 
        /// Graves suggests the momentum term :math:`\gamma_1` to be 0.95, :math:`\gamma_2`
        /// to be 0.9 and the learning rate :math:`\eta` to be 0.0001.
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L834
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="n">n</param>
        /// <param name="g">g</param>
        /// <param name="delta">delta</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="gamma1">Decay rate.</param>
        /// <param name="gamma2">Decay rate.</param>
        /// <param name="epsilon">A small constant for numerical stability.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        /// <param name="clip_weights">Clip weights to the range of [-clip_weights, clip_weights] If clip_weights <= 0, weight clipping is turned off. weights = max(min(weights, clip_weights), -clip_weights).</param>
        public static NDArray RmspropalexUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol n, NDArrayOrSymbol g, NDArrayOrSymbol delta, double lr, double gamma1 = 0.949999988, double gamma2 = 0.899999976, double epsilon = 9.99999994e-09, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, double clipWeights = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rmspropalex_update",
                _rmspropalexUpdateParamNames,
                new[] { Convert(lr), Convert(gamma1), Convert(gamma2), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient), Convert(clipWeights) },
                new[] { weight.Handle, grad.Handle, n.Handle, g.Handle, delta.Handle },
                output
            );
            return result;
        }

        private static string[] _ftrlUpdateParamNames = new[] { "lr", "lamda1", "beta", "wd", "rescale_grad", "clip_gradient" };

        /// <summary>
        /// Update function for Ftrl optimizer.
        /// Referenced from *Ad Click Prediction: a View from the Trenches*, available at
        /// http://dl.acm.org/citation.cfm?id=2488200.
        /// 
        /// It updates the weights using::
        /// 
        ///  rescaled_grad = clip(grad * rescale_grad, clip_gradient)
        ///  z += rescaled_grad - (sqrt(n + rescaled_grad**2) - sqrt(n)) * weight / learning_rate
        ///  n += rescaled_grad**2
        ///  w = (sign(z) * lamda1 - z) / ((beta + sqrt(n)) / learning_rate + wd) * (abs(z) > lamda1)
        /// 
        /// If w, z and n are all of ``row_sparse`` storage type,
        /// only the row slices whose indices appear in grad.indices are updated (for w, z and n)::
        /// 
        ///  for row in grad.indices:
        ///      rescaled_grad[row] = clip(grad[row] * rescale_grad, clip_gradient)
        ///      z[row] += rescaled_grad[row] - (sqrt(n[row] + rescaled_grad[row]**2) - sqrt(n[row])) * weight[row] / learning_rate
        ///      n[row] += rescaled_grad[row]**2
        ///      w[row] = (sign(z[row]) * lamda1 - z[row]) / ((beta + sqrt(n[row])) / learning_rate + wd) * (abs(z[row]) > lamda1)
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L874
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="z">z</param>
        /// <param name="n">Square of grad</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="lamda1">The L1 regularization coefficient.</param>
        /// <param name="beta">Per-Coordinate Learning Rate beta.</param>
        /// <param name="wd">Weight decay augments the objective function with a regularization term that penalizes large weights. The penalty scales with the square of the magnitude of each weight.</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        public static NDArray FtrlUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol z, NDArrayOrSymbol n, double lr, double lamda1 = 0.00999999978, double beta = 1, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ftrl_update",
                _ftrlUpdateParamNames,
                new[] { Convert(lr), Convert(lamda1), Convert(beta), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight.Handle, grad.Handle, z.Handle, n.Handle },
                output
            );
            return result;
        }

        private static string[] _sparseAdagradUpdateParamNames = new[] { "lr", "epsilon", "wd", "rescale_grad", "clip_gradient" };

        /// <summary>
        /// Update function for AdaGrad optimizer.
        /// 
        /// Referenced from *Adaptive Subgradient Methods for Online Learning and Stochastic Optimization*,
        /// and available at http://www.jmlr.org/papers/volume12/duchi11a/duchi11a.pdf.
        /// 
        /// Updates are applied by::
        /// 
        ///     rescaled_grad = clip(grad * rescale_grad, clip_gradient)
        ///     history = history + square(rescaled_grad)
        ///     w = w - learning_rate * rescaled_grad / sqrt(history + epsilon)
        /// 
        /// Note that non-zero values for the weight decay option are not supported.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\optimizer_op.cc:L907
        /// </summary>
        /// <param name="weight">Weight</param>
        /// <param name="grad">Gradient</param>
        /// <param name="history">History</param>
        /// <param name="lr">Learning rate</param>
        /// <param name="epsilon">epsilon</param>
        /// <param name="wd">weight decay</param>
        /// <param name="rescale_grad">Rescale gradient to grad = rescale_grad*grad.</param>
        /// <param name="clip_gradient">Clip gradient to the range of [-clip_gradient, clip_gradient] If clip_gradient <= 0, gradient clipping is turned off. grad = max(min(grad, clip_gradient), -clip_gradient).</param>
        public static NDArray SparseAdagradUpdate(NDArrayOrSymbol weight, NDArrayOrSymbol grad, NDArrayOrSymbol history, double lr, double epsilon = 1.00000001e-07, double wd = 0, double rescaleGrad = 1, double clipGradient = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sparse_adagrad_update",
                _sparseAdagradUpdateParamNames,
                new[] { Convert(lr), Convert(epsilon), Convert(wd), Convert(rescaleGrad), Convert(clipGradient) },
                new[] { weight.Handle, grad.Handle, history.Handle },
                output
            );
            return result;
        }

        private static string[] _PadParamNames = new[] { "mode", "pad_width", "constant_value" };

        /// <summary>
        /// Pads an input array with a constant or edge values of the array.
        /// 
        /// .. note:: `Pad` is deprecated. Use `pad` instead.
        /// 
        /// .. note:: Current implementation only supports 4D and 5D input arrays with padding applied
        ///    only on axes 1, 2 and 3. Expects axes 4 and 5 in `pad_width` to be zero.
        /// 
        /// This operation pads an input array with either a `constant_value` or edge values
        /// along each axis of the input array. The amount of padding is specified by `pad_width`.
        /// 
        /// `pad_width` is a tuple of integer padding widths for each axis of the format
        /// ``(before_1, after_1, ... , before_N, after_N)``. The `pad_width` should be of length ``2*N``
        /// where ``N`` is the number of dimensions of the array.
        /// 
        /// For dimension ``N`` of the input array, ``before_N`` and ``after_N`` indicates how many values
        /// to add before and after the elements of the array along dimension ``N``.
        /// The widths of the higher two dimensions ``before_1``, ``after_1``, ``before_2``,
        /// ``after_2`` must be 0.
        /// 
        /// Example::
        /// 
        ///    x = [[[[  1.   2.   3.]
        ///           [  4.   5.   6.]]
        /// 
        ///          [[  7.   8.   9.]
        ///           [ 10.  11.  12.]]]
        /// 
        /// 
        ///         [[[ 11.  12.  13.]
        ///           [ 14.  15.  16.]]
        /// 
        ///          [[ 17.  18.  19.]
        ///           [ 20.  21.  22.]]]]
        /// 
        ///    pad(x,mode="edge", pad_width=(0,0,0,0,1,1,1,1)) =
        /// 
        ///          [[[[  1.   1.   2.   3.   3.]
        ///             [  1.   1.   2.   3.   3.]
        ///             [  4.   4.   5.   6.   6.]
        ///             [  4.   4.   5.   6.   6.]]
        /// 
        ///            [[  7.   7.   8.   9.   9.]
        ///             [  7.   7.   8.   9.   9.]
        ///             [ 10.  10.  11.  12.  12.]
        ///             [ 10.  10.  11.  12.  12.]]]
        /// 
        /// 
        ///           [[[ 11.  11.  12.  13.  13.]
        ///             [ 11.  11.  12.  13.  13.]
        ///             [ 14.  14.  15.  16.  16.]
        ///             [ 14.  14.  15.  16.  16.]]
        /// 
        ///            [[ 17.  17.  18.  19.  19.]
        ///             [ 17.  17.  18.  19.  19.]
        ///             [ 20.  20.  21.  22.  22.]
        ///             [ 20.  20.  21.  22.  22.]]]]
        /// 
        ///    pad(x, mode="constant", constant_value=0, pad_width=(0,0,0,0,1,1,1,1)) =
        /// 
        ///          [[[[  0.   0.   0.   0.   0.]
        ///             [  0.   1.   2.   3.   0.]
        ///             [  0.   4.   5.   6.   0.]
        ///             [  0.   0.   0.   0.   0.]]
        /// 
        ///            [[  0.   0.   0.   0.   0.]
        ///             [  0.   7.   8.   9.   0.]
        ///             [  0.  10.  11.  12.   0.]
        ///             [  0.   0.   0.   0.   0.]]]
        /// 
        /// 
        ///           [[[  0.   0.   0.   0.   0.]
        ///             [  0.  11.  12.  13.   0.]
        ///             [  0.  14.  15.  16.   0.]
        ///             [  0.   0.   0.   0.   0.]]
        /// 
        ///            [[  0.   0.   0.   0.   0.]
        ///             [  0.  17.  18.  19.   0.]
        ///             [  0.  20.  21.  22.   0.]
        ///             [  0.   0.   0.   0.   0.]]]]
        /// 
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\pad.cc:L766
        /// </summary>
        /// <param name="data">An n-dimensional input array.</param>
        /// <param name="mode">Padding type to use. "constant" pads with `constant_value` "edge" pads using the edge values of the input array "reflect" pads by reflecting values with respect to the edges.</param>
        /// <param name="pad_width">Widths of the padding regions applied to the edges of each axis. It is a tuple of integer padding widths for each axis of the format ``(before_1, after_1, ... , before_N, after_N)``. It should be of length ``2*N`` where ``N`` is the number of dimensions of the array.This is equivalent to pad_width in numpy.pad, but flattened.</param>
        /// <param name="constant_value">The value used for padding when `mode` is "constant".</param>
        public static NDArray Pad(NDArrayOrSymbol data, string mode, NDShape padWidth, double constantValue = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Pad",
                _PadParamNames,
                new[] { Convert(mode), Convert(padWidth), Convert(constantValue) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _FlattenParamNames = Empty;

        /// <summary>
        /// Flattens the input array into a 2-D array by collapsing the higher dimensions.
        /// 
        /// .. note:: `Flatten` is deprecated. Use `flatten` instead.
        /// 
        /// For an input array with shape ``(d1, d2, ..., dk)``, `flatten` operation reshapes
        /// the input array into an output array of shape ``(d1, d2*...*dk)``.
        /// 
        /// Note that the bahavior of this function is different from numpy.ndarray.flatten,
        /// which behaves similar to mxnet.ndarray.reshape((-1,)).
        /// 
        /// Example::
        /// 
        ///     x = [[
        ///         [1,2,3],
        ///         [4,5,6],
        ///         [7,8,9]
        ///     ],
        ///     [    [1,2,3],
        ///         [4,5,6],
        ///         [7,8,9]
        ///     ]],
        /// 
        ///     flatten(x) = [[ 1.,  2.,  3.,  4.,  5.,  6.,  7.,  8.,  9.],
        ///        [ 1.,  2.,  3.,  4.,  5.,  6.,  7.,  8.,  9.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L293
        /// </summary>
        /// <param name="data">Input array.</param>
        public static NDArray Flatten(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Flatten",
                _FlattenParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _sampleUniformParamNames = new[] { "shape", "dtype" };

        /// <summary>
        /// Concurrent sampling from multiple
        /// uniform distributions on the intervals given by *[low,high)*.
        /// 
        /// The parameters of the distributions are provided as input arrays.
        /// Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*
        /// be the shape specified as the parameter of the operator, and *m* be the dimension
        /// of *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.
        /// 
        /// For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*
        /// will be an *m*-dimensional array that holds randomly drawn samples from the distribution
        /// which is parameterized by the input values at index *i*. If the shape parameter of the
        /// operator is not set, then one sample will be drawn per distribution and the output array
        /// has the same shape as the input arrays.
        /// 
        /// Examples::
        /// 
        ///    low = [ 0.0, 2.5 ]
        ///    high = [ 1.0, 3.7 ]
        /// 
        ///    // Draw a single sample for each distribution
        ///    sample_uniform(low, high) = [ 0.40451524,  3.18687344]
        /// 
        ///    // Draw a vector containing two samples for each distribution
        ///    sample_uniform(low, high, shape=(2)) = [[ 0.40451524,  0.18017688],
        ///                                            [ 3.18687344,  3.68352246]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\multisample_op.cc:L277
        /// </summary>
        /// <param name="low">Lower bounds of the distributions.</param>
        /// <param name="shape">Shape to be sampled from each random distribution.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        /// <param name="high">Upper bounds of the distributions.</param>
        public static NDArray SampleUniform(NDArrayOrSymbol low, NDArrayOrSymbol high, NDShape shape = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_uniform",
                _sampleUniformParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { low.Handle, high.Handle },
                output
            );
            return result;
        }

        private static string[] _sampleNormalParamNames = new[] { "shape", "dtype" };

        /// <summary>
        /// Concurrent sampling from multiple
        /// normal distributions with parameters *mu* (mean) and *sigma* (standard deviation).
        /// 
        /// The parameters of the distributions are provided as input arrays.
        /// Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*
        /// be the shape specified as the parameter of the operator, and *m* be the dimension
        /// of *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.
        /// 
        /// For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*
        /// will be an *m*-dimensional array that holds randomly drawn samples from the distribution
        /// which is parameterized by the input values at index *i*. If the shape parameter of the
        /// operator is not set, then one sample will be drawn per distribution and the output array
        /// has the same shape as the input arrays.
        /// 
        /// Examples::
        /// 
        ///    mu = [ 0.0, 2.5 ]
        ///    sigma = [ 1.0, 3.7 ]
        /// 
        ///    // Draw a single sample for each distribution
        ///    sample_normal(mu, sigma) = [-0.56410581,  0.95934606]
        /// 
        ///    // Draw a vector containing two samples for each distribution
        ///    sample_normal(mu, sigma, shape=(2)) = [[-0.56410581,  0.2928229 ],
        ///                                           [ 0.95934606,  4.48287058]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\multisample_op.cc:L279
        /// </summary>
        /// <param name="mu">Means of the distributions.</param>
        /// <param name="shape">Shape to be sampled from each random distribution.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        /// <param name="sigma">Standard deviations of the distributions.</param>
        public static NDArray SampleNormal(NDArrayOrSymbol mu, NDArrayOrSymbol sigma, NDShape shape = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_normal",
                _sampleNormalParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { mu.Handle, sigma.Handle },
                output
            );
            return result;
        }

        private static string[] _sampleGammaParamNames = new[] { "shape", "dtype" };

        /// <summary>
        /// Concurrent sampling from multiple
        /// gamma distributions with parameters *alpha* (shape) and *beta* (scale).
        /// 
        /// The parameters of the distributions are provided as input arrays.
        /// Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*
        /// be the shape specified as the parameter of the operator, and *m* be the dimension
        /// of *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.
        /// 
        /// For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*
        /// will be an *m*-dimensional array that holds randomly drawn samples from the distribution
        /// which is parameterized by the input values at index *i*. If the shape parameter of the
        /// operator is not set, then one sample will be drawn per distribution and the output array
        /// has the same shape as the input arrays.
        /// 
        /// Examples::
        /// 
        ///    alpha = [ 0.0, 2.5 ]
        ///    beta = [ 1.0, 0.7 ]
        /// 
        ///    // Draw a single sample for each distribution
        ///    sample_gamma(alpha, beta) = [ 0.        ,  2.25797319]
        /// 
        ///    // Draw a vector containing two samples for each distribution
        ///    sample_gamma(alpha, beta, shape=(2)) = [[ 0.        ,  0.        ],
        ///                                            [ 2.25797319,  1.70734084]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\multisample_op.cc:L282
        /// </summary>
        /// <param name="alpha">Alpha (shape) parameters of the distributions.</param>
        /// <param name="shape">Shape to be sampled from each random distribution.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        /// <param name="beta">Beta (scale) parameters of the distributions.</param>
        public static NDArray SampleGamma(NDArrayOrSymbol alpha, NDArrayOrSymbol beta, NDShape shape = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_gamma",
                _sampleGammaParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { alpha.Handle, beta.Handle },
                output
            );
            return result;
        }

        private static string[] _sampleExponentialParamNames = new[] { "shape", "dtype" };

        /// <summary>
        /// Concurrent sampling from multiple
        /// exponential distributions with parameters lambda (rate).
        /// 
        /// The parameters of the distributions are provided as an input array.
        /// Let *[s]* be the shape of the input array, *n* be the dimension of *[s]*, *[t]*
        /// be the shape specified as the parameter of the operator, and *m* be the dimension
        /// of *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.
        /// 
        /// For any valid *n*-dimensional index *i* with respect to the input array, *output[i]*
        /// will be an *m*-dimensional array that holds randomly drawn samples from the distribution
        /// which is parameterized by the input value at index *i*. If the shape parameter of the
        /// operator is not set, then one sample will be drawn per distribution and the output array
        /// has the same shape as the input array.
        /// 
        /// Examples::
        /// 
        ///    lam = [ 1.0, 8.5 ]
        /// 
        ///    // Draw a single sample for each distribution
        ///    sample_exponential(lam) = [ 0.51837951,  0.09994757]
        /// 
        ///    // Draw a vector containing two samples for each distribution
        ///    sample_exponential(lam, shape=(2)) = [[ 0.51837951,  0.19866663],
        ///                                          [ 0.09994757,  0.50447971]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\multisample_op.cc:L284
        /// </summary>
        /// <param name="lam">Lambda (rate) parameters of the distributions.</param>
        /// <param name="shape">Shape to be sampled from each random distribution.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        public static NDArray SampleExponential(NDArrayOrSymbol lam, NDShape shape = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_exponential",
                _sampleExponentialParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { lam.Handle },
                output
            );
            return result;
        }

        private static string[] _samplePoissonParamNames = new[] { "shape", "dtype" };

        /// <summary>
        /// Concurrent sampling from multiple
        /// Poisson distributions with parameters lambda (rate).
        /// 
        /// The parameters of the distributions are provided as an input array.
        /// Let *[s]* be the shape of the input array, *n* be the dimension of *[s]*, *[t]*
        /// be the shape specified as the parameter of the operator, and *m* be the dimension
        /// of *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.
        /// 
        /// For any valid *n*-dimensional index *i* with respect to the input array, *output[i]*
        /// will be an *m*-dimensional array that holds randomly drawn samples from the distribution
        /// which is parameterized by the input value at index *i*. If the shape parameter of the
        /// operator is not set, then one sample will be drawn per distribution and the output array
        /// has the same shape as the input array.
        /// 
        /// Samples will always be returned as a floating point data type.
        /// 
        /// Examples::
        /// 
        ///    lam = [ 1.0, 8.5 ]
        /// 
        ///    // Draw a single sample for each distribution
        ///    sample_poisson(lam) = [  0.,  13.]
        /// 
        ///    // Draw a vector containing two samples for each distribution
        ///    sample_poisson(lam, shape=(2)) = [[  0.,   4.],
        ///                                      [ 13.,   8.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\multisample_op.cc:L286
        /// </summary>
        /// <param name="lam">Lambda (rate) parameters of the distributions.</param>
        /// <param name="shape">Shape to be sampled from each random distribution.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        public static NDArray SamplePoisson(NDArrayOrSymbol lam, NDShape shape = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_poisson",
                _samplePoissonParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { lam.Handle },
                output
            );
            return result;
        }

        private static string[] _sampleNegativeBinomialParamNames = new[] { "shape", "dtype" };

        /// <summary>
        /// Concurrent sampling from multiple
        /// negative binomial distributions with parameters *k* (failure limit) and *p* (failure probability).
        /// 
        /// The parameters of the distributions are provided as input arrays.
        /// Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*
        /// be the shape specified as the parameter of the operator, and *m* be the dimension
        /// of *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.
        /// 
        /// For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*
        /// will be an *m*-dimensional array that holds randomly drawn samples from the distribution
        /// which is parameterized by the input values at index *i*. If the shape parameter of the
        /// operator is not set, then one sample will be drawn per distribution and the output array
        /// has the same shape as the input arrays.
        /// 
        /// Samples will always be returned as a floating point data type.
        /// 
        /// Examples::
        /// 
        ///    k = [ 20, 49 ]
        ///    p = [ 0.4 , 0.77 ]
        /// 
        ///    // Draw a single sample for each distribution
        ///    sample_negative_binomial(k, p) = [ 15.,  16.]
        /// 
        ///    // Draw a vector containing two samples for each distribution
        ///    sample_negative_binomial(k, p, shape=(2)) = [[ 15.,  50.],
        ///                                                 [ 16.,  12.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\multisample_op.cc:L289
        /// </summary>
        /// <param name="k">Limits of unsuccessful experiments.</param>
        /// <param name="shape">Shape to be sampled from each random distribution.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        /// <param name="p">Failure probabilities in each experiment.</param>
        public static NDArray SampleNegativeBinomial(NDArrayOrSymbol k, NDArrayOrSymbol p, NDShape shape = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_negative_binomial",
                _sampleNegativeBinomialParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { k.Handle, p.Handle },
                output
            );
            return result;
        }

        private static string[] _sampleGeneralizedNegativeBinomialParamNames = new[] { "shape", "dtype" };

        /// <summary>
        /// Concurrent sampling from multiple
        /// generalized negative binomial distributions with parameters *mu* (mean) and *alpha* (dispersion).
        /// 
        /// The parameters of the distributions are provided as input arrays.
        /// Let *[s]* be the shape of the input arrays, *n* be the dimension of *[s]*, *[t]*
        /// be the shape specified as the parameter of the operator, and *m* be the dimension
        /// of *[t]*. Then the output will be a *(n+m)*-dimensional array with shape *[s]x[t]*.
        /// 
        /// For any valid *n*-dimensional index *i* with respect to the input arrays, *output[i]*
        /// will be an *m*-dimensional array that holds randomly drawn samples from the distribution
        /// which is parameterized by the input values at index *i*. If the shape parameter of the
        /// operator is not set, then one sample will be drawn per distribution and the output array
        /// has the same shape as the input arrays.
        /// 
        /// Samples will always be returned as a floating point data type.
        /// 
        /// Examples::
        /// 
        ///    mu = [ 2.0, 2.5 ]
        ///    alpha = [ 1.0, 0.1 ]
        /// 
        ///    // Draw a single sample for each distribution
        ///    sample_generalized_negative_binomial(mu, alpha) = [ 0.,  3.]
        /// 
        ///    // Draw a vector containing two samples for each distribution
        ///    sample_generalized_negative_binomial(mu, alpha, shape=(2)) = [[ 0.,  3.],
        ///                                                                  [ 3.,  1.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\multisample_op.cc:L293
        /// </summary>
        /// <param name="mu">Means of the distributions.</param>
        /// <param name="shape">Shape to be sampled from each random distribution.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        /// <param name="alpha">Alpha (dispersion) parameters of the distributions.</param>
        public static NDArray SampleGeneralizedNegativeBinomial(NDArrayOrSymbol mu, NDArrayOrSymbol alpha, NDShape shape = null, string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_generalized_negative_binomial",
                _sampleGeneralizedNegativeBinomialParamNames,
                new[] { Convert(shape), Convert(dtype) },
                new[] { mu.Handle, alpha.Handle },
                output
            );
            return result;
        }

        private static string[] _sampleMultinomialParamNames = new[] { "shape", "get_prob", "dtype" };

        /// <summary>
        /// Concurrent sampling from multiple multinomial distributions.
        /// 
        /// *data* is an *n* dimensional array whose last dimension has length *k*, where
        /// *k* is the number of possible outcomes of each multinomial distribution. This
        /// operator will draw *shape* samples from each distribution. If shape is empty
        /// one sample will be drawn from each distribution.
        /// 
        /// If *get_prob* is true, a second array containing log likelihood of the drawn
        /// samples will also be returned. This is usually used for reinforcement learning
        /// where you can provide reward as head gradient for this array to estimate
        /// gradient.
        /// 
        /// Note that the input distribution must be normalized, i.e. *data* must sum to
        /// 1 along its last axis.
        /// 
        /// Examples::
        /// 
        ///    probs = [[0, 0.1, 0.2, 0.3, 0.4], [0.4, 0.3, 0.2, 0.1, 0]]
        /// 
        ///    // Draw a single sample for each distribution
        ///    sample_multinomial(probs) = [3, 0]
        /// 
        ///    // Draw a vector containing two samples for each distribution
        ///    sample_multinomial(probs, shape=(2)) = [[4, 2],
        ///                                            [0, 0]]
        /// 
        ///    // requests log likelihood
        ///    sample_multinomial(probs, get_prob=True) = [2, 1], [0.2, 0.3]
        /// 
        /// </summary>
        /// <param name="data">Distribution probabilities. Must sum to one on the last axis.</param>
        /// <param name="shape">Shape to be sampled from each random distribution.</param>
        /// <param name="get_prob">Whether to also return the log probability of sampled result. This is usually used for differentiating through stochastic variables, e.g. in reinforcement learning.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred.</param>
        public static NDArray SampleMultinomial(NDArrayOrSymbol data, NDShape shape = null, bool getProb = false, string dtype = "int32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_multinomial",
                _sampleMultinomialParamNames,
                new[] { Convert(shape), Convert(getProb), Convert(dtype) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSampleMultinomialParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSampleMultinomial(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_sample_multinomial",
                _backwardSampleMultinomialParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _randomUniformParamNames = new[] { "low", "high", "shape", "ctx", "dtype" };

        /// <summary>
        /// Draw random samples from a uniform distribution.
        /// 
        /// .. note:: The existing alias ``uniform`` is deprecated.
        /// 
        /// Samples are uniformly distributed over the half-open interval *[low, high)*
        /// (includes *low*, but excludes *high*).
        /// 
        /// Example::
        /// 
        ///    uniform(low=0, high=1, shape=(2,2)) = [[ 0.60276335,  0.85794562],
        ///                                           [ 0.54488319,  0.84725171]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L96
        /// </summary>
        /// <param name="low">Lower bound of the distribution.</param>
        /// <param name="high">Upper bound of the distribution.</param>
        /// <param name="shape">Shape of the output.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        public static NDArray RandomUniform(double low = 0, double high = 1, NDShape shape = null, string ctx = "", string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_uniform",
                _randomUniformParamNames,
                new[] { Convert(low), Convert(high), Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _randomNormalParamNames = new[] { "loc", "scale", "shape", "ctx", "dtype" };

        /// <summary>
        /// Draw random samples from a normal (Gaussian) distribution.
        /// 
        /// .. note:: The existing alias ``normal`` is deprecated.
        /// 
        /// Samples are distributed according to a normal distribution parametrized by *loc* (mean) and *scale*
        /// (standard deviation).
        /// 
        /// Example::
        /// 
        ///    normal(loc=0, scale=1, shape=(2,2)) = [[ 1.89171135, -1.16881478],
        ///                                           [-1.23474145,  1.55807114]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L113
        /// </summary>
        /// <param name="loc">Mean of the distribution.</param>
        /// <param name="scale">Standard deviation of the distribution.</param>
        /// <param name="shape">Shape of the output.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        public static NDArray RandomNormal(double loc = 0, double scale = 1, NDShape shape = null, string ctx = "", string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_normal",
                _randomNormalParamNames,
                new[] { Convert(loc), Convert(scale), Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _randomGammaParamNames = new[] { "alpha", "beta", "shape", "ctx", "dtype" };

        /// <summary>
        /// Draw random samples from a gamma distribution.
        /// 
        /// Samples are distributed according to a gamma distribution parametrized by *alpha* (shape) and *beta* (scale).
        /// 
        /// Example::
        /// 
        ///    gamma(alpha=9, beta=0.5, shape=(2,2)) = [[ 7.10486984,  3.37695289],
        ///                                             [ 3.91697288,  3.65933681]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L125
        /// </summary>
        /// <param name="alpha">Alpha parameter (shape) of the gamma distribution.</param>
        /// <param name="beta">Beta parameter (scale) of the gamma distribution.</param>
        /// <param name="shape">Shape of the output.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        public static NDArray RandomGamma(double alpha = 1, double beta = 1, NDShape shape = null, string ctx = "", string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_gamma",
                _randomGammaParamNames,
                new[] { Convert(alpha), Convert(beta), Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _randomExponentialParamNames = new[] { "lam", "shape", "ctx", "dtype" };

        /// <summary>
        /// Draw random samples from an exponential distribution.
        /// 
        /// Samples are distributed according to an exponential distribution parametrized by *lambda* (rate).
        /// 
        /// Example::
        /// 
        ///    exponential(lam=4, shape=(2,2)) = [[ 0.0097189 ,  0.08999364],
        ///                                       [ 0.04146638,  0.31715935]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L137
        /// </summary>
        /// <param name="lam">Lambda parameter (rate) of the exponential distribution.</param>
        /// <param name="shape">Shape of the output.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        public static NDArray RandomExponential(double lam = 1, NDShape shape = null, string ctx = "", string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_exponential",
                _randomExponentialParamNames,
                new[] { Convert(lam), Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _randomPoissonParamNames = new[] { "lam", "shape", "ctx", "dtype" };

        /// <summary>
        /// Draw random samples from a Poisson distribution.
        /// 
        /// Samples are distributed according to a Poisson distribution parametrized by *lambda* (rate).
        /// Samples will always be returned as a floating point data type.
        /// 
        /// Example::
        /// 
        ///    poisson(lam=4, shape=(2,2)) = [[ 5.,  2.],
        ///                                   [ 4.,  6.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L150
        /// </summary>
        /// <param name="lam">Lambda parameter (rate) of the Poisson distribution.</param>
        /// <param name="shape">Shape of the output.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        public static NDArray RandomPoisson(double lam = 1, NDShape shape = null, string ctx = "", string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_poisson",
                _randomPoissonParamNames,
                new[] { Convert(lam), Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _randomNegativeBinomialParamNames = new[] { "k", "p", "shape", "ctx", "dtype" };

        /// <summary>
        /// Draw random samples from a negative binomial distribution.
        /// 
        /// Samples are distributed according to a negative binomial distribution parametrized by
        /// *k* (limit of unsuccessful experiments) and *p* (failure probability in each experiment).
        /// Samples will always be returned as a floating point data type.
        /// 
        /// Example::
        /// 
        ///    negative_binomial(k=3, p=0.4, shape=(2,2)) = [[ 4.,  7.],
        ///                                                  [ 2.,  5.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L164
        /// </summary>
        /// <param name="k">Limit of unsuccessful experiments.</param>
        /// <param name="p">Failure probability in each experiment.</param>
        /// <param name="shape">Shape of the output.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        public static NDArray RandomNegativeBinomial(int k = 1, double p = 1, NDShape shape = null, string ctx = "", string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_negative_binomial",
                _randomNegativeBinomialParamNames,
                new[] { Convert(k), Convert(p), Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _randomGeneralizedNegativeBinomialParamNames = new[] { "mu", "alpha", "shape", "ctx", "dtype" };

        /// <summary>
        /// Draw random samples from a generalized negative binomial distribution.
        /// 
        /// Samples are distributed according to a generalized negative binomial distribution parametrized by
        /// *mu* (mean) and *alpha* (dispersion). *alpha* is defined as *1/k* where *k* is the failure limit of the
        /// number of unsuccessful experiments (generalized to real numbers).
        /// Samples will always be returned as a floating point data type.
        /// 
        /// Example::
        /// 
        ///    generalized_negative_binomial(mu=2.0, alpha=0.3, shape=(2,2)) = [[ 2.,  1.],
        ///                                                                     [ 6.,  4.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L179
        /// </summary>
        /// <param name="mu">Mean of the negative binomial distribution.</param>
        /// <param name="alpha">Alpha (dispersion) parameter of the negative binomial distribution.</param>
        /// <param name="shape">Shape of the output.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to float32 if not defined (dtype=None).</param>
        public static NDArray RandomGeneralizedNegativeBinomial(double mu = 1, double alpha = 1, NDShape shape = null, string ctx = "", string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_generalized_negative_binomial",
                _randomGeneralizedNegativeBinomialParamNames,
                new[] { Convert(mu), Convert(alpha), Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _randomRandintParamNames = new[] { "low", "high", "shape", "ctx", "dtype" };

        /// <summary>
        /// Draw random samples from a discrete uniform distribution.
        /// 
        /// Samples are uniformly distributed over the half-open interval *[low, high)*
        /// (includes *low*, but excludes *high*).
        /// 
        /// Example::
        /// 
        ///    randint(low=0, high=5, shape=(2,2)) = [[ 0,  2],
        ///                                           [ 3,  1]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L193
        /// </summary>
        /// <param name="low">Lower bound of the distribution.</param>
        /// <param name="high">Upper bound of the distribution.</param>
        /// <param name="shape">Shape of the output.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n). Only used for imperative calls.</param>
        /// <param name="dtype">DType of the output in case this can't be inferred. Defaults to int32 if not defined (dtype=None).</param>
        public static NDArray RandomRandint(double? low, double? high, NDShape shape = null, string ctx = "", string dtype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_randint",
                _randomRandintParamNames,
                new[] { Convert(low), Convert(high), Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _randomUniformLikeParamNames = new[] { "low", "high" };

        /// <summary>
        /// Draw random samples from a uniform distribution according to the input array shape.
        /// 
        /// Samples are uniformly distributed over the half-open interval *[low, high)*
        /// (includes *low*, but excludes *high*).
        /// 
        /// Example::
        /// 
        ///    uniform(low=0, high=1, data=ones(2,2)) = [[ 0.60276335,  0.85794562],
        ///                                              [ 0.54488319,  0.84725171]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L208
        /// </summary>
        /// <param name="low">Lower bound of the distribution.</param>
        /// <param name="high">Upper bound of the distribution.</param>
        /// <param name="data">The input</param>
        public static NDArray RandomUniformLike(NDArrayOrSymbol data, double low = 0, double high = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_uniform_like",
                _randomUniformLikeParamNames,
                new[] { Convert(low), Convert(high) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _randomNormalLikeParamNames = new[] { "loc", "scale" };

        /// <summary>
        /// Draw random samples from a normal (Gaussian) distribution according to the input array shape.
        /// 
        /// Samples are distributed according to a normal distribution parametrized by *loc* (mean) and *scale*
        /// (standard deviation).
        /// 
        /// Example::
        /// 
        ///    normal(loc=0, scale=1, data=ones(2,2)) = [[ 1.89171135, -1.16881478],
        ///                                              [-1.23474145,  1.55807114]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L220
        /// </summary>
        /// <param name="loc">Mean of the distribution.</param>
        /// <param name="scale">Standard deviation of the distribution.</param>
        /// <param name="data">The input</param>
        public static NDArray RandomNormalLike(NDArrayOrSymbol data, double loc = 0, double scale = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_normal_like",
                _randomNormalLikeParamNames,
                new[] { Convert(loc), Convert(scale) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _randomGammaLikeParamNames = new[] { "alpha", "beta" };

        /// <summary>
        /// Draw random samples from a gamma distribution according to the input array shape.
        /// 
        /// Samples are distributed according to a gamma distribution parametrized by *alpha* (shape) and *beta* (scale).
        /// 
        /// Example::
        /// 
        ///    gamma(alpha=9, beta=0.5, data=ones(2,2)) = [[ 7.10486984,  3.37695289],
        ///                                                [ 3.91697288,  3.65933681]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L231
        /// </summary>
        /// <param name="alpha">Alpha parameter (shape) of the gamma distribution.</param>
        /// <param name="beta">Beta parameter (scale) of the gamma distribution.</param>
        /// <param name="data">The input</param>
        public static NDArray RandomGammaLike(NDArrayOrSymbol data, double alpha = 1, double beta = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_gamma_like",
                _randomGammaLikeParamNames,
                new[] { Convert(alpha), Convert(beta) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _randomExponentialLikeParamNames = new[] { "lam" };

        /// <summary>
        /// Draw random samples from an exponential distribution according to the input array shape.
        /// 
        /// Samples are distributed according to an exponential distribution parametrized by *lambda* (rate).
        /// 
        /// Example::
        /// 
        ///    exponential(lam=4, data=ones(2,2)) = [[ 0.0097189 ,  0.08999364],
        ///                                          [ 0.04146638,  0.31715935]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L242
        /// </summary>
        /// <param name="lam">Lambda parameter (rate) of the exponential distribution.</param>
        /// <param name="data">The input</param>
        public static NDArray RandomExponentialLike(NDArrayOrSymbol data, double lam = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_exponential_like",
                _randomExponentialLikeParamNames,
                new[] { Convert(lam) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _randomPoissonLikeParamNames = new[] { "lam" };

        /// <summary>
        /// Draw random samples from a Poisson distribution according to the input array shape.
        /// 
        /// Samples are distributed according to a Poisson distribution parametrized by *lambda* (rate).
        /// Samples will always be returned as a floating point data type.
        /// 
        /// Example::
        /// 
        ///    poisson(lam=4, data=ones(2,2)) = [[ 5.,  2.],
        ///                                      [ 4.,  6.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L254
        /// </summary>
        /// <param name="lam">Lambda parameter (rate) of the Poisson distribution.</param>
        /// <param name="data">The input</param>
        public static NDArray RandomPoissonLike(NDArrayOrSymbol data, double lam = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_poisson_like",
                _randomPoissonLikeParamNames,
                new[] { Convert(lam) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _randomNegativeBinomialLikeParamNames = new[] { "k", "p" };

        /// <summary>
        /// Draw random samples from a negative binomial distribution according to the input array shape.
        /// 
        /// Samples are distributed according to a negative binomial distribution parametrized by
        /// *k* (limit of unsuccessful experiments) and *p* (failure probability in each experiment).
        /// Samples will always be returned as a floating point data type.
        /// 
        /// Example::
        /// 
        ///    negative_binomial(k=3, p=0.4, data=ones(2,2)) = [[ 4.,  7.],
        ///                                                     [ 2.,  5.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L267
        /// </summary>
        /// <param name="k">Limit of unsuccessful experiments.</param>
        /// <param name="p">Failure probability in each experiment.</param>
        /// <param name="data">The input</param>
        public static NDArray RandomNegativeBinomialLike(NDArrayOrSymbol data, int k = 1, double p = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_negative_binomial_like",
                _randomNegativeBinomialLikeParamNames,
                new[] { Convert(k), Convert(p) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _randomGeneralizedNegativeBinomialLikeParamNames = new[] { "mu", "alpha" };

        /// <summary>
        /// Draw random samples from a generalized negative binomial distribution according to the
        /// input array shape.
        /// 
        /// Samples are distributed according to a generalized negative binomial distribution parametrized by
        /// *mu* (mean) and *alpha* (dispersion). *alpha* is defined as *1/k* where *k* is the failure limit of the
        /// number of unsuccessful experiments (generalized to real numbers).
        /// Samples will always be returned as a floating point data type.
        /// 
        /// Example::
        /// 
        ///    generalized_negative_binomial(mu=2.0, alpha=0.3, data=ones(2,2)) = [[ 2.,  1.],
        ///                                                                        [ 6.,  4.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\sample_op.cc:L283
        /// </summary>
        /// <param name="mu">Mean of the negative binomial distribution.</param>
        /// <param name="alpha">Alpha (dispersion) parameter of the negative binomial distribution.</param>
        /// <param name="data">The input</param>
        public static NDArray RandomGeneralizedNegativeBinomialLike(NDArrayOrSymbol data, double mu = 1, double alpha = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_random_generalized_negative_binomial_like",
                _randomGeneralizedNegativeBinomialLikeParamNames,
                new[] { Convert(mu), Convert(alpha) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _shuffleParamNames = Empty;

        /// <summary>
        /// Randomly shuffle the elements.
        /// 
        /// This shuffles the array along the first axis.
        /// The order of the elements in each subarray does not change.
        /// For example, if a 2D array is given, the order of the rows randomly changes,
        /// but the order of the elements in each row does not change.
        /// 
        /// </summary>
        /// <param name="data">Data to be shuffled.</param>
        public static NDArray Shuffle(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_shuffle",
                _shuffleParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _sampleUniqueZipfianParamNames = new[] { "range_max", "shape" };

        /// <summary>
        /// Draw random samples from an an approximately log-uniform
        /// or Zipfian distribution without replacement.
        /// 
        /// This operation takes a 2-D shape `(batch_size, num_sampled)`,
        /// and randomly generates *num_sampled* samples from the range of integers [0, range_max)
        /// for each instance in the batch.
        /// 
        /// The elements in each instance are drawn without replacement from the base distribution.
        /// The base distribution for this operator is an approximately log-uniform or Zipfian distribution:
        /// 
        ///   P(class) = (log(class + 2) - log(class + 1)) / log(range_max + 1)
        /// 
        /// Additionaly, it also returns the number of trials used to obtain `num_sampled` samples for
        /// each instance in the batch.
        /// 
        /// Example::
        /// 
        ///    samples, trials = _sample_unique_zipfian(750000, shape=(4, 8192))
        ///    unique(samples[0]) = 8192
        ///    unique(samples[3]) = 8192
        ///    trials[0] = 16435
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\random\unique_sample_op.cc:L66
        /// </summary>
        /// <param name="range_max">The number of possible classes.</param>
        /// <param name="shape">2-D shape of the output, where shape[0] is the batch size, and shape[1] is the number of candidates to sample for each batch.</param>
        public static NDArray SampleUniqueZipfian(int rangeMax, NDShape shape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sample_unique_zipfian",
                _sampleUniqueZipfianParamNames,
                new[] { Convert(rangeMax), Convert(shape) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _LinearRegressionOutputParamNames = new[] { "grad_scale" };

        /// <summary>
        /// Computes and optimizes for squared loss during backward propagation.
        /// Just outputs ``data`` during forward propagation.
        /// 
        /// If :math:`\hat{y}_i` is the predicted value of the i-th sample, and :math:`y_i` is the corresponding target value,
        /// then the squared loss estimated over :math:`n` samples is defined as
        /// 
        /// :math:`\text{SquaredLoss}(\textbf{Y}, \hat{\textbf{Y}} ) = \frac{1}{n} \sum_{i=0}^{n-1} \lVert  \textbf{y}_i - \hat{\textbf{y}}_i  \rVert_2`
        /// 
        /// .. note::
        ///    Use the LinearRegressionOutput as the final output layer of a net.
        /// 
        /// The storage type of ``label`` can be ``default`` or ``csr``
        /// 
        /// - LinearRegressionOutput(default, default) = default
        /// - LinearRegressionOutput(default, csr) = default
        /// 
        /// By default, gradients of this loss function are scaled by factor `1/m`, where m is the number of regression outputs of a training example.
        /// The parameter `grad_scale` can be used to change this scale to `grad_scale/m`.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\regression_output.cc:L92
        /// </summary>
        /// <param name="data">Input data to the function.</param>
        /// <param name="label">Input label to the function.</param>
        /// <param name="grad_scale">Scale the gradient by a float factor</param>
        public static NDArray LinearRegressionOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, double gradScale = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LinearRegressionOutput",
                _LinearRegressionOutputParamNames,
                new[] { Convert(gradScale) },
                new[] { data.Handle, label.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinearRegOutParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinearRegOut(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linear_reg_out",
                _backwardLinearRegOutParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _MAERegressionOutputParamNames = new[] { "grad_scale" };

        /// <summary>
        /// Computes mean absolute error of the input.
        /// 
        /// MAE is a risk metric corresponding to the expected value of the absolute error.
        /// 
        /// If :math:`\hat{y}_i` is the predicted value of the i-th sample, and :math:`y_i` is the corresponding target value,
        /// then the mean absolute error (MAE) estimated over :math:`n` samples is defined as
        /// 
        /// :math:`\text{MAE}(\textbf{Y}, \hat{\textbf{Y}} ) = \frac{1}{n} \sum_{i=0}^{n-1} \lVert \textbf{y}_i - \hat{\textbf{y}}_i \rVert_1`
        /// 
        /// .. note::
        ///    Use the MAERegressionOutput as the final output layer of a net.
        /// 
        /// The storage type of ``label`` can be ``default`` or ``csr``
        /// 
        /// - MAERegressionOutput(default, default) = default
        /// - MAERegressionOutput(default, csr) = default
        /// 
        /// By default, gradients of this loss function are scaled by factor `1/m`, where m is the number of regression outputs of a training example.
        /// The parameter `grad_scale` can be used to change this scale to `grad_scale/m`.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\regression_output.cc:L120
        /// </summary>
        /// <param name="data">Input data to the function.</param>
        /// <param name="label">Input label to the function.</param>
        /// <param name="grad_scale">Scale the gradient by a float factor</param>
        public static NDArray MAERegressionOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, double gradScale = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "MAERegressionOutput",
                _MAERegressionOutputParamNames,
                new[] { Convert(gradScale) },
                new[] { data.Handle, label.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMaeRegOutParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMaeRegOut(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_mae_reg_out",
                _backwardMaeRegOutParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _LogisticRegressionOutputParamNames = new[] { "grad_scale" };

        /// <summary>
        /// Applies a logistic function to the input.
        /// 
        /// The logistic function, also known as the sigmoid function, is computed as
        /// :math:`\frac{1}{1+exp(-\textbf{x})}`.
        /// 
        /// Commonly, the sigmoid is used to squash the real-valued output of a linear model
        /// :math:`wTx+b` into the [0,1] range so that it can be interpreted as a probability.
        /// It is suitable for binary classification or probability prediction tasks.
        /// 
        /// .. note::
        ///    Use the LogisticRegressionOutput as the final output layer of a net.
        /// 
        /// The storage type of ``label`` can be ``default`` or ``csr``
        /// 
        /// - LogisticRegressionOutput(default, default) = default
        /// - LogisticRegressionOutput(default, csr) = default
        /// 
        /// The loss function used is the Binary Cross Entropy Loss:
        /// 
        /// :math:`-{(y\log(p) + (1 - y)\log(1 - p))}`
        /// 
        /// Where `y` is the ground truth probability of positive outcome for a given example, and `p` the probability predicted by the model. By default, gradients of this loss function are scaled by factor `1/m`, where m is the number of regression outputs of a training example.
        /// The parameter `grad_scale` can be used to change this scale to `grad_scale/m`.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\regression_output.cc:L152
        /// </summary>
        /// <param name="data">Input data to the function.</param>
        /// <param name="label">Input label to the function.</param>
        /// <param name="grad_scale">Scale the gradient by a float factor</param>
        public static NDArray LogisticRegressionOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, double gradScale = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "LogisticRegressionOutput",
                _LogisticRegressionOutputParamNames,
                new[] { Convert(gradScale) },
                new[] { data.Handle, label.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLogisticRegOutParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLogisticRegOut(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_logistic_reg_out",
                _backwardLogisticRegOutParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _RNNParamNames = new[] { "state_size", "num_layers", "bidirectional", "mode", "p", "state_outputs", "projection_size", "lstm_state_clip_min", "lstm_state_clip_max", "lstm_state_clip_nan", "use_sequence_length" };

        /// <summary>
        /// Applies recurrent layers to input data. Currently, vanilla RNN, LSTM and GRU are
        /// implemented, with both multi-layer and bidirectional support.
        /// 
        /// When the input data is of type float32 and the environment variables MXNET_CUDA_ALLOW_TENSOR_CORE
        /// and MXNET_CUDA_TENSOR_OP_MATH_ALLOW_CONVERSION are set to 1, this operator will try to use
        /// pseudo-float16 precision (float32 math with float16 I/O) precision in order to use
        /// Tensor Cores on suitable NVIDIA GPUs. This can sometimes give significant speedups.
        /// 
        /// **Vanilla RNN**
        /// 
        /// Applies a single-gate recurrent layer to input X. Two kinds of activation function are supported:
        /// ReLU and Tanh.
        /// 
        /// With ReLU activation function:
        /// 
        /// .. math::
        ///     h_t = relu(W_{ih} * x_t + b_{ih}  +  W_{hh} * h_{(t-1)} + b_{hh})
        /// 
        /// With Tanh activtion function:
        /// 
        /// .. math::
        ///     h_t = \tanh(W_{ih} * x_t + b_{ih}  +  W_{hh} * h_{(t-1)} + b_{hh})
        /// 
        /// Reference paper: Finding structure in time - Elman, 1988.
        /// https://crl.ucsd.edu/~elman/Papers/fsit.pdf
        /// 
        /// **LSTM**
        /// 
        /// Long Short-Term Memory - Hochreiter, 1997. http://www.bioinf.jku.at/publications/older/2604.pdf
        /// 
        /// .. math::
        ///   \begin{array}{ll}
        ///             i_t = \mathrm{sigmoid}(W_{ii} x_t + b_{ii} + W_{hi} h_{(t-1)} + b_{hi}) \\
        ///             f_t = \mathrm{sigmoid}(W_{if} x_t + b_{if} + W_{hf} h_{(t-1)} + b_{hf}) \\
        ///             g_t = \tanh(W_{ig} x_t + b_{ig} + W_{hc} h_{(t-1)} + b_{hg}) \\
        ///             o_t = \mathrm{sigmoid}(W_{io} x_t + b_{io} + W_{ho} h_{(t-1)} + b_{ho}) \\
        ///             c_t = f_t * c_{(t-1)} + i_t * g_t \\
        ///             h_t = o_t * \tanh(c_t)
        ///             \end{array}
        /// 
        /// **GRU**
        /// 
        /// Gated Recurrent Unit - Cho et al. 2014. http://arxiv.org/abs/1406.1078
        /// 
        /// The definition of GRU here is slightly different from paper but compatible with CUDNN.
        /// 
        /// .. math::
        ///   \begin{array}{ll}
        ///             r_t = \mathrm{sigmoid}(W_{ir} x_t + b_{ir} + W_{hr} h_{(t-1)} + b_{hr}) \\
        ///             z_t = \mathrm{sigmoid}(W_{iz} x_t + b_{iz} + W_{hz} h_{(t-1)} + b_{hz}) \\
        ///             n_t = \tanh(W_{in} x_t + b_{in} + r_t * (W_{hn} h_{(t-1)}+ b_{hn})) \\
        ///             h_t = (1 - z_t) * n_t + z_t * h_{(t-1)} \\
        ///             \end{array}
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\rnn.cc:L690
        /// </summary>
        /// <param name="data">Input data to RNN</param>
        /// <param name="parameters">Vector of all RNN trainable parameters concatenated</param>
        /// <param name="state">initial hidden state of the RNN</param>
        /// <param name="state_cell">initial cell state for LSTM networks (only for LSTM)</param>
        /// <param name="sequence_length">Vector of valid sequence lengths for each element in batch. (Only used if use_sequence_length kwarg is True)</param>
        /// <param name="state_size">size of the state for each layer</param>
        /// <param name="num_layers">number of stacked layers</param>
        /// <param name="bidirectional">whether to use bidirectional recurrent layers</param>
        /// <param name="mode">the type of RNN to compute</param>
        /// <param name="p">drop rate of the dropout on the outputs of each RNN layer, except the last layer.</param>
        /// <param name="state_outputs">Whether to have the states as symbol outputs.</param>
        /// <param name="projection_size">size of project size</param>
        /// <param name="lstm_state_clip_min">Minimum clip value of LSTM states. This option must be used together with lstm_state_clip_max.</param>
        /// <param name="lstm_state_clip_max">Maximum clip value of LSTM states. This option must be used together with lstm_state_clip_min.</param>
        /// <param name="lstm_state_clip_nan">Whether to stop NaN from propagating in state by clipping it to min/max. If clipping range is not specified, this option is ignored.</param>
        /// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
        public static NDArray RNN(NDArrayOrSymbol data, NDArrayOrSymbol parameters, NDArrayOrSymbol state, NDArrayOrSymbol stateCell, NDArrayOrSymbol sequenceLength, int stateSize, int numLayers, string mode, bool bidirectional = false, double p = 0, bool stateOutputs = false, int? projectionSize = null, double? lstmStateClipMin = null, double? lstmStateClipMax = null, bool lstmStateClipNan = false, bool useSequenceLength = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "RNN",
                _RNNParamNames,
                new[] { Convert(stateSize), Convert(numLayers), Convert(bidirectional), Convert(mode), Convert(p), Convert(stateOutputs), Convert(projectionSize), Convert(lstmStateClipMin), Convert(lstmStateClipMax), Convert(lstmStateClipNan), Convert(useSequenceLength) },
                new[] { data.Handle, parameters.Handle, state.Handle, stateCell.Handle, sequenceLength.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardRNNParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardRNN(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_RNN",
                _backwardRNNParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _SliceChannelParamNames = new[] { "num_outputs", "axis", "squeeze_axis" };

        /// <summary>
        /// Splits an array along a particular axis into multiple sub-arrays.
        /// 
        /// .. note:: ``SliceChannel`` is deprecated. Use ``split`` instead.
        /// 
        /// **Note** that `num_outputs` should evenly divide the length of the axis
        /// along which to split the array.
        /// 
        /// Example::
        /// 
        ///    x  = [[[ 1.]
        ///           [ 2.]]
        ///          [[ 3.]
        ///           [ 4.]]
        ///          [[ 5.]
        ///           [ 6.]]]
        ///    x.shape = (3, 2, 1)
        /// 
        ///    y = split(x, axis=1, num_outputs=2) // a list of 2 arrays with shape (3, 1, 1)
        ///    y = [[[ 1.]]
        ///         [[ 3.]]
        ///         [[ 5.]]]
        /// 
        ///        [[[ 2.]]
        ///         [[ 4.]]
        ///         [[ 6.]]]
        /// 
        ///    y[0].shape = (3, 1, 1)
        /// 
        ///    z = split(x, axis=0, num_outputs=3) // a list of 3 arrays with shape (1, 2, 1)
        ///    z = [[[ 1.]
        ///          [ 2.]]]
        /// 
        ///        [[[ 3.]
        ///          [ 4.]]]
        /// 
        ///        [[[ 5.]
        ///          [ 6.]]]
        /// 
        ///    z[0].shape = (1, 2, 1)
        /// 
        /// `squeeze_axis=1` removes the axis with length 1 from the shapes of the output arrays.
        /// **Note** that setting `squeeze_axis` to ``1`` removes axis with length 1 only
        /// along the `axis` which it is split.
        /// Also `squeeze_axis` can be set to true only if ``input.shape[axis] == num_outputs``.
        /// 
        /// Example::
        /// 
        ///    z = split(x, axis=0, num_outputs=3, squeeze_axis=1) // a list of 3 arrays with shape (2, 1)
        ///    z = [[ 1.]
        ///         [ 2.]]
        /// 
        ///        [[ 3.]
        ///         [ 4.]]
        /// 
        ///        [[ 5.]
        ///         [ 6.]]
        ///    z[0].shape = (2 ,1 )
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\slice_channel.cc:L107
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="num_outputs">Number of splits. Note that this should evenly divide the length of the `axis`.</param>
        /// <param name="axis">Axis along which to split.</param>
        /// <param name="squeeze_axis">If true, Removes the axis with length 1 from the shapes of the output arrays. **Note** that setting `squeeze_axis` to ``true`` removes axis with length 1 only along the `axis` which it is split. Also `squeeze_axis` can be set to ``true`` only if ``input.shape[axis] == num_outputs``.</param>
        public static NDArray SliceChannel(NDArrayOrSymbol data, int numOutputs, int axis = 1, bool squeezeAxis = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SliceChannel",
                _SliceChannelParamNames,
                new[] { Convert(numOutputs), Convert(axis), Convert(squeezeAxis) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _SoftmaxOutputParamNames = new[] { "grad_scale", "ignore_label", "multi_output", "use_ignore", "preserve_shape", "normalization", "out_grad", "smooth_alpha" };

        /// <summary>
        /// Computes the gradient of cross entropy loss with respect to softmax output.
        /// 
        /// - This operator computes the gradient in two steps.
        ///   The cross entropy loss does not actually need to be computed.
        /// 
        ///   - Applies softmax function on the input array.
        ///   - Computes and returns the gradient of cross entropy loss w.r.t. the softmax output.
        /// 
        /// - The softmax function, cross entropy loss and gradient is given by:
        /// 
        ///   - Softmax Function:
        /// 
        ///     .. math:: \text{softmax}(x)_i = \frac{exp(x_i)}{\sum_j exp(x_j)}
        /// 
        ///   - Cross Entropy Function:
        /// 
        ///     .. math:: \text{CE(label, output)} = - \sum_i \text{label}_i \log(\text{output}_i)
        /// 
        ///   - The gradient of cross entropy loss w.r.t softmax output:
        /// 
        ///     .. math:: \text{gradient} = \text{output} - \text{label}
        /// 
        /// - During forward propagation, the softmax function is computed for each instance in the input array.
        /// 
        ///   For general *N*-D input arrays with shape :math:`(d_1, d_2, ..., d_n)`. The size is
        ///   :math:`s=d_1 \cdot d_2 \cdot \cdot \cdot d_n`. We can use the parameters `preserve_shape`
        ///   and `multi_output` to specify the way to compute softmax:
        /// 
        ///   - By default, `preserve_shape` is ``false``. This operator will reshape the input array
        ///     into a 2-D array with shape :math:`(d_1, \frac{s}{d_1})` and then compute the softmax function for
        ///     each row in the reshaped array, and afterwards reshape it back to the original shape
        ///     :math:`(d_1, d_2, ..., d_n)`.
        ///   - If `preserve_shape` is ``true``, the softmax function will be computed along
        ///     the last axis (`axis` = ``-1``).
        ///   - If `multi_output` is ``true``, the softmax function will be computed along
        ///     the second axis (`axis` = ``1``).
        /// 
        /// - During backward propagation, the gradient of cross-entropy loss w.r.t softmax output array is computed.
        ///   The provided label can be a one-hot label array or a probability label array.
        /// 
        ///   - If the parameter `use_ignore` is ``true``, `ignore_label` can specify input instances
        ///     with a particular label to be ignored during backward propagation. **This has no effect when
        ///     softmax `output` has same shape as `label`**.
        /// 
        ///     Example::
        /// 
        ///       data = [[1,2,3,4],[2,2,2,2],[3,3,3,3],[4,4,4,4]]
        ///       label = [1,0,2,3]
        ///       ignore_label = 1
        ///       SoftmaxOutput(data=data, label = label,\
        ///                     multi_output=true, use_ignore=true,\
        ///                     ignore_label=ignore_label)
        ///       ## forward softmax output
        ///       [[ 0.0320586   0.08714432  0.23688284  0.64391428]
        ///        [ 0.25        0.25        0.25        0.25      ]
        ///        [ 0.25        0.25        0.25        0.25      ]
        ///        [ 0.25        0.25        0.25        0.25      ]]
        ///       ## backward gradient output
        ///       [[ 0.    0.    0.    0.  ]
        ///        [-0.75  0.25  0.25  0.25]
        ///        [ 0.25  0.25 -0.75  0.25]
        ///        [ 0.25  0.25  0.25 -0.75]]
        ///       ## notice that the first row is all 0 because label[0] is 1, which is equal to ignore_label.
        /// 
        ///   - The parameter `grad_scale` can be used to rescale the gradient, which is often used to
        ///     give each loss function different weights.
        /// 
        ///   - This operator also supports various ways to normalize the gradient by `normalization`,
        ///     The `normalization` is applied if softmax output has different shape than the labels.
        ///     The `normalization` mode can be set to the followings:
        /// 
        ///     - ``'null'``: do nothing.
        ///     - ``'batch'``: divide the gradient by the batch size.
        ///     - ``'valid'``: divide the gradient by the number of instances which are not ignored.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\softmax_output.cc:L230
        /// </summary>
        /// <param name="data">Input array.</param>
        /// <param name="label">Ground truth label.</param>
        /// <param name="grad_scale">Scales the gradient by a float factor.</param>
        /// <param name="ignore_label">The instances whose `labels` == `ignore_label` will be ignored during backward, if `use_ignore` is set to ``true``).</param>
        /// <param name="multi_output">If set to ``true``, the softmax function will be computed along axis ``1``. This is applied when the shape of input array differs from the shape of label array.</param>
        /// <param name="use_ignore">If set to ``true``, the `ignore_label` value will not contribute to the backward gradient.</param>
        /// <param name="preserve_shape">If set to ``true``, the softmax function will be computed along the last axis (``-1``).</param>
        /// <param name="normalization">Normalizes the gradient.</param>
        /// <param name="out_grad">Multiplies gradient with output gradient element-wise.</param>
        /// <param name="smooth_alpha">Constant for computing a label smoothed version of cross-entropyfor the backwards pass.  This constant gets subtracted from theone-hot encoding of the gold label and distributed uniformly toall other labels.</param>
        public static NDArray SoftmaxOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, double gradScale = 1, double ignoreLabel = -1, bool multiOutput = false, bool useIgnore = false, bool preserveShape = false, string normalization = "null", bool outGrad = false, double smoothAlpha = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SoftmaxOutput",
                _SoftmaxOutputParamNames,
                new[] { Convert(gradScale), Convert(ignoreLabel), Convert(multiOutput), Convert(useIgnore), Convert(preserveShape), Convert(normalization), Convert(outGrad), Convert(smoothAlpha) },
                new[] { data.Handle, label.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSoftmaxOutputParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSoftmaxOutput(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SoftmaxOutput",
                _backwardSoftmaxOutputParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _sgMkldnnConvParamNames = Empty;

        /// <summary>
        /// _sg_mkldnn_conv
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\subgraph\mkldnn\mkldnn_conv.cc:L770
        /// </summary>
        public static NDArray SgMkldnnConv(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sg_mkldnn_conv",
                _sgMkldnnConvParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _sgMkldnnFullyConnectedParamNames = Empty;

        /// <summary>
        /// _sg_mkldnn_fully_connected
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\subgraph\mkldnn\mkldnn_fc.cc:L410
        /// </summary>
        public static NDArray SgMkldnnFullyConnected(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sg_mkldnn_fully_connected",
                _sgMkldnnFullyConnectedParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _SwapAxisParamNames = new[] { "dim1", "dim2" };

        /// <summary>
        /// Interchanges two axes of an array.
        /// 
        /// Examples::
        /// 
        ///   x = [[1, 2, 3]])
        ///   swapaxes(x, 0, 1) = [[ 1],
        ///                        [ 2],
        ///                        [ 3]]
        /// 
        ///   x = [[[ 0, 1],
        ///         [ 2, 3]],
        ///        [[ 4, 5],
        ///         [ 6, 7]]]  // (2,2,2) array
        /// 
        ///  swapaxes(x, 0, 2) = [[[ 0, 4],
        ///                        [ 2, 6]],
        ///                       [[ 1, 5],
        ///                        [ 3, 7]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\swapaxis.cc:L70
        /// </summary>
        /// <param name="data">Input array.</param>
        /// <param name="dim1">the first axis to be swapped.</param>
        /// <param name="dim2">the second axis to be swapped.</param>
        public static NDArray SwapAxis(NDArrayOrSymbol data, int dim1 = 0, int dim2 = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SwapAxis",
                _SwapAxisParamNames,
                new[] { Convert(dim1), Convert(dim2) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _ampCastParamNames = new[] { "dtype" };

        /// <summary>
        /// Cast function between low precision float/FP32 used by AMP.
        /// 
        /// It casts only between low precision float/FP32 and does not do anything for other types.
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\amp_cast.cc:L37
        /// </summary>
        /// <param name="data">The input.</param>
        /// <param name="dtype">Output data type.</param>
        public static NDArray AmpCast(NDArrayOrSymbol data, string dtype, NDArray output = null)
        {
            var result = Operator.Invoke(
                "amp_cast",
                _ampCastParamNames,
                new[] { Convert(dtype) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardAmpCastParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardAmpCast(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_amp_cast",
                _backwardAmpCastParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _argmaxParamNames = new[] { "axis", "keepdims" };

        /// <summary>
        /// Returns indices of the maximum values along an axis.
        /// 
        /// In the case of multiple occurrences of maximum values, the indices corresponding to the first occurrence
        /// are returned.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 0.,  1.,  2.],
        ///        [ 3.,  4.,  5.]]
        /// 
        ///   // argmax along axis 0
        ///   argmax(x, axis=0) = [ 1.,  1.,  1.]
        /// 
        ///   // argmax along axis 1
        ///   argmax(x, axis=1) = [ 2.,  2.]
        /// 
        ///   // argmax along axis 1 keeping same dims as an input array
        ///   argmax(x, axis=1, keepdims=True) = [[ 2.],
        ///                                       [ 2.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L52
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis along which to perform the reduction. Negative values means indexing from right to left. ``Requires axis to be set as int, because global reduction is not supported yet.``</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axis is left in the result as dimension with size one.</param>
        public static NDArray Argmax(NDArrayOrSymbol data, int? axis = null, bool keepdims = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "argmax",
                _argmaxParamNames,
                new[] { Convert(axis), Convert(keepdims) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _argminParamNames = new[] { "axis", "keepdims" };

        /// <summary>
        /// Returns indices of the minimum values along an axis.
        /// 
        /// In the case of multiple occurrences of minimum values, the indices corresponding to the first occurrence
        /// are returned.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 0.,  1.,  2.],
        ///        [ 3.,  4.,  5.]]
        /// 
        ///   // argmin along axis 0
        ///   argmin(x, axis=0) = [ 0.,  0.,  0.]
        /// 
        ///   // argmin along axis 1
        ///   argmin(x, axis=1) = [ 0.,  0.]
        /// 
        ///   // argmin along axis 1 keeping same dims as an input array
        ///   argmin(x, axis=1, keepdims=True) = [[ 0.],
        ///                                       [ 0.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L77
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis along which to perform the reduction. Negative values means indexing from right to left. ``Requires axis to be set as int, because global reduction is not supported yet.``</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axis is left in the result as dimension with size one.</param>
        public static NDArray Argmin(NDArrayOrSymbol data, int? axis = null, bool keepdims = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "argmin",
                _argminParamNames,
                new[] { Convert(axis), Convert(keepdims) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _argmaxChannelParamNames = Empty;

        /// <summary>
        /// Returns argmax indices of each channel from the input array.
        /// 
        /// The result will be an NDArray of shape (num_channel,).
        /// 
        /// In case of multiple occurrences of the maximum values, the indices corresponding to the first occurrence
        /// are returned.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 0.,  1.,  2.],
        ///        [ 3.,  4.,  5.]]
        /// 
        ///   argmax_channel(x) = [ 2.,  2.]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L97
        /// </summary>
        /// <param name="data">The input array</param>
        public static NDArray ArgmaxChannel(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "argmax_channel",
                _argmaxChannelParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _pickParamNames = new[] { "axis", "keepdims", "mode" };

        /// <summary>
        /// Picks elements from an input array according to the input indices along the given axis.
        /// 
        /// Given an input array of shape ``(d0, d1)`` and indices of shape ``(i0,)``, the result will be
        /// an output array of shape ``(i0,)`` with::
        /// 
        ///   output[i] = input[i, indices[i]]
        /// 
        /// By default, if any index mentioned is too large, it is replaced by the index that addresses
        /// the last element along an axis (the `clip` mode).
        /// 
        /// This function supports n-dimensional input and (n-1)-dimensional indices arrays.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 1.,  2.],
        ///        [ 3.,  4.],
        ///        [ 5.,  6.]]
        /// 
        ///   // picks elements with specified indices along axis 0
        ///   pick(x, y=[0,1], 0) = [ 1.,  4.]
        /// 
        ///   // picks elements with specified indices along axis 1
        ///   pick(x, y=[0,1,0], 1) = [ 1.,  4.,  5.]
        /// 
        ///   y = [[ 1.],
        ///        [ 0.],
        ///        [ 2.]]
        /// 
        ///   // picks elements with specified indices along axis 1 using 'wrap' mode
        ///   // to place indicies that would normally be out of bounds
        ///   pick(x, y=[2,-1,-2], 1, mode='wrap') = [ 1.,  4.,  5.]
        /// 
        ///   y = [[ 1.],
        ///        [ 0.],
        ///        [ 2.]]
        /// 
        ///   // picks elements with specified indices along axis 1 and dims are maintained
        ///   pick(x,y, 1, keepdims=True) = [[ 2.],
        ///                                  [ 3.],
        ///                                  [ 6.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_index.cc:L154
        /// </summary>
        /// <param name="data">The input array</param>
        /// <param name="index">The index array</param>
        /// <param name="axis">int or None. The axis to picking the elements. Negative values means indexing from right to left. If is `None`, the elements in the index w.r.t the flattened input will be picked.</param>
        /// <param name="keepdims">If true, the axis where we pick the elements is left in the result as dimension with size one.</param>
        /// <param name="mode">Specify how out-of-bound indices behave. Default is "clip". "clip" means clip to the range. So, if all indices mentioned are too large, they are replaced by the index that addresses the last element along an axis.  "wrap" means to wrap around.</param>
        public static NDArray Pick(NDArrayOrSymbol data, NDArrayOrSymbol index, int? axis = -1, bool keepdims = false, string mode = "clip", NDArray output = null)
        {
            var result = Operator.Invoke(
                "pick",
                _pickParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(mode) },
                new[] { data.Handle, index.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardPickParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardPick(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_pick",
                _backwardPickParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _sumParamNames = new[] { "axis", "keepdims", "exclude" };

        /// <summary>
        /// Computes the sum of array elements over given axes.
        /// 
        /// .. Note::
        /// 
        ///   `sum` and `sum_axis` are equivalent.
        ///   For ndarray of csr storage type summation along axis 0 and axis 1 is supported.
        ///   Setting keepdims or exclude to True will cause a fallback to dense operator.
        /// 
        /// Example::
        /// 
        ///   data = [[[1, 2], [2, 3], [1, 3]],
        ///           [[1, 4], [4, 3], [5, 2]],
        ///           [[7, 1], [7, 2], [7, 3]]]
        /// 
        ///   sum(data, axis=1)
        ///   [[  4.   8.]
        ///    [ 10.   9.]
        ///    [ 21.   6.]]
        /// 
        ///   sum(data, axis=[1,2])
        ///   [ 12.  19.  27.]
        /// 
        ///   data = [[1, 2, 0],
        ///           [3, 0, 1],
        ///           [4, 1, 0]]
        /// 
        ///   csr = cast_storage(data, 'csr')
        /// 
        ///   sum(csr, axis=0)
        ///   [ 8.  3.  1.]
        /// 
        ///   sum(csr, axis=1)
        ///   [ 3.  4.  5.]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L116
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis or axes along which to perform the reduction.
        /// 
        ///       The default, `axis=()`, will compute over all elements into a
        ///       scalar array with shape `(1,)`.
        /// 
        ///       If `axis` is int, a reduction is performed on a particular axis.
        /// 
        ///       If `axis` is a tuple of ints, a reduction is performed on all the axes
        ///       specified in the tuple.
        /// 
        ///       If `exclude` is true, reduction will be performed on the axes that are
        ///       NOT in axis instead.
        /// 
        ///       Negative values means indexing from right to left.</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
        /// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
        public static NDArray Sum(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sum",
                _sumParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSumParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSum(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_sum",
                _backwardSumParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _meanParamNames = new[] { "axis", "keepdims", "exclude" };

        /// <summary>
        /// Computes the mean of array elements over given axes.
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L132
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis or axes along which to perform the reduction.
        /// 
        ///       The default, `axis=()`, will compute over all elements into a
        ///       scalar array with shape `(1,)`.
        /// 
        ///       If `axis` is int, a reduction is performed on a particular axis.
        /// 
        ///       If `axis` is a tuple of ints, a reduction is performed on all the axes
        ///       specified in the tuple.
        /// 
        ///       If `exclude` is true, reduction will be performed on the axes that are
        ///       NOT in axis instead.
        /// 
        ///       Negative values means indexing from right to left.</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
        /// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
        public static NDArray Mean(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "mean",
                _meanParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMeanParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMean(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_mean",
                _backwardMeanParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _prodParamNames = new[] { "axis", "keepdims", "exclude" };

        /// <summary>
        /// Computes the product of array elements over given axes.
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L147
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis or axes along which to perform the reduction.
        /// 
        ///       The default, `axis=()`, will compute over all elements into a
        ///       scalar array with shape `(1,)`.
        /// 
        ///       If `axis` is int, a reduction is performed on a particular axis.
        /// 
        ///       If `axis` is a tuple of ints, a reduction is performed on all the axes
        ///       specified in the tuple.
        /// 
        ///       If `exclude` is true, reduction will be performed on the axes that are
        ///       NOT in axis instead.
        /// 
        ///       Negative values means indexing from right to left.</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
        /// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
        public static NDArray Prod(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "prod",
                _prodParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardProdParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardProd(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_prod",
                _backwardProdParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _nansumParamNames = new[] { "axis", "keepdims", "exclude" };

        /// <summary>
        /// Computes the sum of array elements over given axes treating Not a Numbers (``NaN``) as zero.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L162
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis or axes along which to perform the reduction.
        /// 
        ///       The default, `axis=()`, will compute over all elements into a
        ///       scalar array with shape `(1,)`.
        /// 
        ///       If `axis` is int, a reduction is performed on a particular axis.
        /// 
        ///       If `axis` is a tuple of ints, a reduction is performed on all the axes
        ///       specified in the tuple.
        /// 
        ///       If `exclude` is true, reduction will be performed on the axes that are
        ///       NOT in axis instead.
        /// 
        ///       Negative values means indexing from right to left.</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
        /// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
        public static NDArray Nansum(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "nansum",
                _nansumParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardNansumParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardNansum(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_nansum",
                _backwardNansumParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _nanprodParamNames = new[] { "axis", "keepdims", "exclude" };

        /// <summary>
        /// Computes the product of array elements over given axes treating Not a Numbers (``NaN``) as one.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L177
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis or axes along which to perform the reduction.
        /// 
        ///       The default, `axis=()`, will compute over all elements into a
        ///       scalar array with shape `(1,)`.
        /// 
        ///       If `axis` is int, a reduction is performed on a particular axis.
        /// 
        ///       If `axis` is a tuple of ints, a reduction is performed on all the axes
        ///       specified in the tuple.
        /// 
        ///       If `exclude` is true, reduction will be performed on the axes that are
        ///       NOT in axis instead.
        /// 
        ///       Negative values means indexing from right to left.</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
        /// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
        public static NDArray Nanprod(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "nanprod",
                _nanprodParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardNanprodParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardNanprod(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_nanprod",
                _backwardNanprodParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _maxParamNames = new[] { "axis", "keepdims", "exclude" };

        /// <summary>
        /// Computes the max of array elements over given axes.
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L191
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis or axes along which to perform the reduction.
        /// 
        ///       The default, `axis=()`, will compute over all elements into a
        ///       scalar array with shape `(1,)`.
        /// 
        ///       If `axis` is int, a reduction is performed on a particular axis.
        /// 
        ///       If `axis` is a tuple of ints, a reduction is performed on all the axes
        ///       specified in the tuple.
        /// 
        ///       If `exclude` is true, reduction will be performed on the axes that are
        ///       NOT in axis instead.
        /// 
        ///       Negative values means indexing from right to left.</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
        /// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
        public static NDArray Max(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "max",
                _maxParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMaxParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMax(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_max",
                _backwardMaxParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _minParamNames = new[] { "axis", "keepdims", "exclude" };

        /// <summary>
        /// Computes the min of array elements over given axes.
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L205
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis or axes along which to perform the reduction.
        /// 
        ///       The default, `axis=()`, will compute over all elements into a
        ///       scalar array with shape `(1,)`.
        /// 
        ///       If `axis` is int, a reduction is performed on a particular axis.
        /// 
        ///       If `axis` is a tuple of ints, a reduction is performed on all the axes
        ///       specified in the tuple.
        /// 
        ///       If `exclude` is true, reduction will be performed on the axes that are
        ///       NOT in axis instead.
        /// 
        ///       Negative values means indexing from right to left.</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
        /// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
        public static NDArray Min(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "min",
                _minParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMinParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMin(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_min",
                _backwardMinParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastAxisParamNames = new[] { "axis", "size" };

        /// <summary>
        /// Broadcasts the input array over particular axes.
        /// 
        /// Broadcasting is allowed on axes with size 1, such as from `(2,1,3,1)` to
        /// `(2,8,3,9)`. Elements will be duplicated on the broadcasted axes.
        /// 
        /// Example::
        /// 
        ///    // given x of shape (1,2,1)
        ///    x = [[[ 1.],
        ///          [ 2.]]]
        /// 
        ///    // broadcast x on on axis 2
        ///    broadcast_axis(x, axis=2, size=3) = [[[ 1.,  1.,  1.],
        ///                                          [ 2.,  2.,  2.]]]
        ///    // broadcast x on on axes 0 and 2
        ///    broadcast_axis(x, axis=(0,2), size=(2,3)) = [[[ 1.,  1.,  1.],
        ///                                                  [ 2.,  2.,  2.]],
        ///                                                 [[ 1.,  1.,  1.],
        ///                                                  [ 2.,  2.,  2.]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L238
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axes to perform the broadcasting.</param>
        /// <param name="size">Target sizes of the broadcasting axes.</param>
        public static NDArray BroadcastAxis(NDArrayOrSymbol data, NDShape axis = null, NDShape size = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_axis",
                _broadcastAxisParamNames,
                new[] { Convert(axis), Convert(size) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastToParamNames = new[] { "shape" };

        /// <summary>
        /// Broadcasts the input array to a new shape.
        /// 
        /// Broadcasting is a mechanism that allows NDArrays to perform arithmetic operations
        /// with arrays of different shapes efficiently without creating multiple copies of arrays.
        /// Also see, `Broadcasting <https://docs.scipy.org/doc/numpy/user/basics.broadcasting.html>`_ for more explanation.
        /// 
        /// Broadcasting is allowed on axes with size 1, such as from `(2,1,3,1)` to
        /// `(2,8,3,9)`. Elements will be duplicated on the broadcasted axes.
        /// 
        /// For example::
        /// 
        ///    broadcast_to([[1,2,3]], shape=(2,3)) = [[ 1.,  2.,  3.],
        ///                                            [ 1.,  2.,  3.]])
        /// 
        /// The dimension which you do not want to change can also be kept as `0` which means copy the original value.
        /// So with `shape=(2,0)`, we will obtain the same result as in the above example.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L262
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="shape">The shape of the desired array. We can set the dim to zero if it's same as the original. E.g `A = broadcast_to(B, shape=(10, 0, 0))` has the same meaning as `A = broadcast_axis(B, axis=0, size=10)`.</param>
        public static NDArray BroadcastTo(NDArrayOrSymbol data, NDShape shape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_to",
                _broadcastToParamNames,
                new[] { Convert(shape) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastBackwardParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BroadcastBackward(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_broadcast_backward",
                _broadcastBackwardParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastLikeParamNames = new[] { "lhs_axes", "rhs_axes" };

        /// <summary>
        /// Broadcasts lhs to have the same shape as rhs.
        /// 
        /// Broadcasting is a mechanism that allows NDArrays to perform arithmetic operations
        /// with arrays of different shapes efficiently without creating multiple copies of arrays.
        /// Also see, `Broadcasting <https://docs.scipy.org/doc/numpy/user/basics.broadcasting.html>`_ for more explanation.
        /// 
        /// Broadcasting is allowed on axes with size 1, such as from `(2,1,3,1)` to
        /// `(2,8,3,9)`. Elements will be duplicated on the broadcasted axes.
        /// 
        /// For example::
        /// 
        ///    broadcast_like([[1,2,3]], [[5,6,7],[7,8,9]]) = [[ 1.,  2.,  3.],
        ///                                                    [ 1.,  2.,  3.]])
        /// 
        ///    broadcast_like([9], [1,2,3,4,5], lhs_axes=(0,), rhs_axes=(-1,)) = [9,9,9,9,9]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L315
        /// </summary>
        /// <param name="lhs">First input.</param>
        /// <param name="rhs">Second input.</param>
        /// <param name="lhs_axes">Axes to perform broadcast on in the first input array</param>
        /// <param name="rhs_axes">Axes to copy from the second input array</param>
        public static NDArray BroadcastLike(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDShape lhsAxes = null, NDShape rhsAxes = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_like",
                _broadcastLikeParamNames,
                new[] { Convert(lhsAxes), Convert(rhsAxes) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _normParamNames = new[] { "ord", "axis", "out_dtype", "keepdims" };

        /// <summary>
        /// Computes the norm on an NDArray.
        /// 
        /// This operator computes the norm on an NDArray with the specified axis, depending
        /// on the value of the ord parameter. By default, it computes the L2 norm on the entire
        /// array. Currently only ord=2 supports sparse ndarrays.
        /// 
        /// Examples::
        /// 
        ///   x = [[[1, 2],
        ///         [3, 4]],
        ///        [[2, 2],
        ///         [5, 6]]]
        /// 
        ///   norm(x, ord=2, axis=1) = [[3.1622777 4.472136 ]
        ///                             [5.3851647 6.3245554]]
        /// 
        ///   norm(x, ord=1, axis=1) = [[4., 6.],
        ///                             [7., 8.]]
        /// 
        ///   rsp = x.cast_storage('row_sparse')
        /// 
        ///   norm(rsp) = [5.47722578]
        /// 
        ///   csr = x.cast_storage('csr')
        /// 
        ///   norm(csr) = [5.47722578]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\broadcast_reduce_op_value.cc:L350
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="ord">Order of the norm. Currently ord=1 and ord=2 is supported.</param>
        /// <param name="axis">The axis or axes along which to perform the reduction.
        ///       The default, `axis=()`, will compute over all elements into a
        ///       scalar array with shape `(1,)`.
        ///       If `axis` is int, a reduction is performed on a particular axis.
        ///       If `axis` is a 2-tuple, it specifies the axes that hold 2-D matrices,
        ///       and the matrix norms of these matrices are computed.</param>
        /// <param name="out_dtype">The data type of the output.</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axis is left in the result as dimension with size one.</param>
        public static NDArray Norm(NDArrayOrSymbol data, int ord = 2, NDShape axis = null, string outDtype = null, bool keepdims = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "norm",
                _normParamNames,
                new[] { Convert(ord), Convert(axis), Convert(outDtype), Convert(keepdims) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardNormParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardNorm(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_norm",
                _backwardNormParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _castStorageParamNames = new[] { "stype" };

        /// <summary>
        /// Casts tensor storage type to the new type.
        /// 
        /// When an NDArray with default storage type is cast to csr or row_sparse storage,
        /// the result is compact, which means:
        /// 
        /// - for csr, zero values will not be retained
        /// - for row_sparse, row slices of all zeros will not be retained
        /// 
        /// The storage type of ``cast_storage`` output depends on stype parameter:
        /// 
        /// - cast_storage(csr, 'default') = default
        /// - cast_storage(row_sparse, 'default') = default
        /// - cast_storage(default, 'csr') = csr
        /// - cast_storage(default, 'row_sparse') = row_sparse
        /// - cast_storage(csr, 'csr') = csr
        /// - cast_storage(row_sparse, 'row_sparse') = row_sparse
        /// 
        /// Example::
        /// 
        ///     dense = [[ 0.,  1.,  0.],
        ///              [ 2.,  0.,  3.],
        ///              [ 0.,  0.,  0.],
        ///              [ 0.,  0.,  0.]]
        /// 
        ///     # cast to row_sparse storage type
        ///     rsp = cast_storage(dense, 'row_sparse')
        ///     rsp.indices = [0, 1]
        ///     rsp.values = [[ 0.,  1.,  0.],
        ///                   [ 2.,  0.,  3.]]
        /// 
        ///     # cast to csr storage type
        ///     csr = cast_storage(dense, 'csr')
        ///     csr.indices = [1, 0, 2]
        ///     csr.values = [ 1.,  2.,  3.]
        ///     csr.indptr = [0, 1, 3, 3, 3]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\cast_storage.cc:L71
        /// </summary>
        /// <param name="data">The input.</param>
        /// <param name="stype">Output storage type.</param>
        public static NDArray CastStorage(NDArrayOrSymbol data, string stype, NDArray output = null)
        {
            var result = Operator.Invoke(
                "cast_storage",
                _castStorageParamNames,
                new[] { Convert(stype) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _whereParamNames = Empty;

        /// <summary>
        /// Return the elements, either from x or y, depending on the condition.
        /// 
        /// Given three ndarrays, condition, x, and y, return an ndarray with the elements from x or y,
        /// depending on the elements from condition are true or false. x and y must have the same shape.
        /// If condition has the same shape as x, each element in the output array is from x if the
        /// corresponding element in the condition is true, and from y if false.
        /// 
        /// If condition does not have the same shape as x, it must be a 1D array whose size is
        /// the same as x's first dimension size. Each row of the output array is from x's row
        /// if the corresponding element from condition is true, and from y's row if false.
        /// 
        /// Note that all non-zero values are interpreted as ``True`` in condition.
        /// 
        /// Examples::
        /// 
        ///   x = [[1, 2], [3, 4]]
        ///   y = [[5, 6], [7, 8]]
        ///   cond = [[0, 1], [-1, 0]]
        /// 
        ///   where(cond, x, y) = [[5, 2], [3, 8]]
        /// 
        ///   csr_cond = cast_storage(cond, 'csr')
        /// 
        ///   where(csr_cond, x, y) = [[5, 2], [3, 8]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\control_flow_op.cc:L57
        /// </summary>
        /// <param name="condition">condition array</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static NDArray Where(NDArrayOrSymbol condition, NDArrayOrSymbol x, NDArrayOrSymbol y, NDArray output = null)
        {
            var result = Operator.Invoke(
                "where",
                _whereParamNames,
                Empty,
                new[] { condition.Handle, x.Handle, y.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardWhereParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardWhere(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_where",
                _backwardWhereParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _diagParamNames = new[] { "k", "axis1", "axis2" };

        /// <summary>
        /// Extracts a diagonal or constructs a diagonal array.
        /// 
        /// ``diag``'s behavior depends on the input array dimensions:
        /// 
        /// - 1-D arrays: constructs a 2-D array with the input as its diagonal, all other elements are zero.
        /// - N-D arrays: extracts the diagonals of the sub-arrays with axes specified by ``axis1`` and ``axis2``.
        ///   The output shape would be decided by removing the axes numbered ``axis1`` and ``axis2`` from the
        ///   input shape and appending to the result a new axis with the size of the diagonals in question.
        /// 
        ///   For example, when the input shape is `(2, 3, 4, 5)`, ``axis1`` and ``axis2`` are 0 and 2
        ///   respectively and ``k`` is 0, the resulting shape would be `(3, 5, 2)`.
        /// 
        /// Examples::
        /// 
        ///   x = [[1, 2, 3],
        ///        [4, 5, 6]]
        /// 
        ///   diag(x) = [1, 5]
        /// 
        ///   diag(x, k=1) = [2, 6]
        /// 
        ///   diag(x, k=-1) = [4]
        /// 
        ///   x = [1, 2, 3]
        /// 
        ///   diag(x) = [[1, 0, 0],
        ///              [0, 2, 0],
        ///              [0, 0, 3]]
        /// 
        ///   diag(x, k=1) = [[0, 1, 0],
        ///                   [0, 0, 2],
        ///                   [0, 0, 0]]
        /// 
        ///   diag(x, k=-1) = [[0, 0, 0],
        ///                    [1, 0, 0],
        ///                    [0, 2, 0]]
        /// 
        ///   x = [[[1, 2],
        ///         [3, 4]],
        /// 
        ///        [[5, 6],
        ///         [7, 8]]]
        /// 
        ///   diag(x) = [[1, 7],
        ///              [2, 8]]
        /// 
        ///   diag(x, k=1) = [[3],
        ///                   [4]]
        /// 
        ///   diag(x, axis1=-2, axis2=-1) = [[1, 4],
        ///                                  [5, 8]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\diag_op.cc:L87
        /// </summary>
        /// <param name="data">Input ndarray</param>
        /// <param name="k">Diagonal in question. The default is 0. Use k>0 for diagonals above the main diagonal, and k<0 for diagonals below the main diagonal. If input has shape (S0 S1) k must be between -S0 and S1</param>
        /// <param name="axis1">The first axis of the sub-arrays of interest. Ignored when the input is a 1-D array.</param>
        /// <param name="axis2">The second axis of the sub-arrays of interest. Ignored when the input is a 1-D array.</param>
        public static NDArray Diag(NDArrayOrSymbol data, int k = 0, int axis1 = 0, int axis2 = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "diag",
                _diagParamNames,
                new[] { Convert(k), Convert(axis1), Convert(axis2) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardDiagParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardDiag(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_diag",
                _backwardDiagParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _dotParamNames = new[] { "transpose_a", "transpose_b", "forward_stype" };

        /// <summary>
        /// Dot product of two arrays.
        /// 
        /// ``dot``'s behavior depends on the input array dimensions:
        /// 
        /// - 1-D arrays: inner product of vectors
        /// - 2-D arrays: matrix multiplication
        /// - N-D arrays: a sum product over the last axis of the first input and the first
        ///   axis of the second input
        /// 
        ///   For example, given 3-D ``x`` with shape `(n,m,k)` and ``y`` with shape `(k,r,s)`, the
        ///   result array will have shape `(n,m,r,s)`. It is computed by::
        /// 
        ///     dot(x,y)[i,j,a,b] = sum(x[i,j,:]*y[:,a,b])
        /// 
        ///   Example::
        /// 
        ///     x = reshape([0,1,2,3,4,5,6,7], shape=(2,2,2))
        ///     y = reshape([7,6,5,4,3,2,1,0], shape=(2,2,2))
        ///     dot(x,y)[0,0,1,1] = 0
        ///     sum(x[0,0,:]*y[:,1,1]) = 0
        /// 
        /// The storage type of ``dot`` output depends on storage types of inputs, transpose option and
        /// forward_stype option for output storage type. Implemented sparse operations include:
        /// 
        /// - dot(default, default, transpose_a=True/False, transpose_b=True/False) = default
        /// - dot(csr, default, transpose_a=True) = default
        /// - dot(csr, default, transpose_a=True) = row_sparse
        /// - dot(csr, default) = default
        /// - dot(csr, row_sparse) = default
        /// - dot(default, csr) = csr (CPU only)
        /// - dot(default, csr, forward_stype='default') = default
        /// - dot(default, csr, transpose_b=True, forward_stype='default') = default
        /// 
        /// If the combination of input storage types and forward_stype does not match any of the
        /// above patterns, ``dot`` will fallback and generate output with default storage.
        /// 
        /// .. Note::
        /// 
        ///     If the storage type of the lhs is "csr", the storage type of gradient w.r.t rhs will be
        ///     "row_sparse". Only a subset of optimizers support sparse gradients, including SGD, AdaGrad
        ///     and Adam. Note that by default lazy updates is turned on, which may perform differently
        ///     from standard updates. For more details, please check the Optimization API at:
        ///     https://mxnet.incubator.apache.org/api/python/optimization/optimization.html
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\dot.cc:L77
        /// </summary>
        /// <param name="lhs">The first input</param>
        /// <param name="rhs">The second input</param>
        /// <param name="transpose_a">If true then transpose the first input before dot.</param>
        /// <param name="transpose_b">If true then transpose the second input before dot.</param>
        /// <param name="forward_stype">The desired storage type of the forward output given by user, if thecombination of input storage types and this hint does not matchany implemented ones, the dot operator will perform fallback operationand still produce an output of the desired storage type.</param>
        public static NDArray Dot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, string forwardStype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "dot",
                _dotParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert(forwardStype) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardDotParamNames = new[] { "transpose_a", "transpose_b", "forward_stype" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transpose_a">If true then transpose the first input before dot.</param>
        /// <param name="transpose_b">If true then transpose the second input before dot.</param>
        /// <param name="forward_stype">The desired storage type of the forward output given by user, if thecombination of input storage types and this hint does not matchany implemented ones, the dot operator will perform fallback operationand still produce an output of the desired storage type.</param>
        public static NDArray BackwardDot(bool transposeA = false, bool transposeB = false, string forwardStype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_dot",
                _backwardDotParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert(forwardStype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _batchDotParamNames = new[] { "transpose_a", "transpose_b", "forward_stype" };

        /// <summary>
        /// Batchwise dot product.
        /// 
        /// ``batch_dot`` is used to compute dot product of ``x`` and ``y`` when ``x`` and
        /// ``y`` are data in batch, namely 3D arrays in shape of `(batch_size, :, :)`.
        /// 
        /// For example, given ``x`` with shape `(batch_size, n, m)` and ``y`` with shape
        /// `(batch_size, m, k)`, the result array will have shape `(batch_size, n, k)`,
        /// which is computed by::
        /// 
        ///    batch_dot(x,y)[i,:,:] = dot(x[i,:,:], y[i,:,:])
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\dot.cc:L125
        /// </summary>
        /// <param name="lhs">The first input</param>
        /// <param name="rhs">The second input</param>
        /// <param name="transpose_a">If true then transpose the first input before dot.</param>
        /// <param name="transpose_b">If true then transpose the second input before dot.</param>
        /// <param name="forward_stype">The desired storage type of the forward output given by user, if thecombination of input storage types and this hint does not matchany implemented ones, the dot operator will perform fallback operationand still produce an output of the desired storage type.</param>
        public static NDArray BatchDot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, bool transposeA = false, bool transposeB = false, string forwardStype = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "batch_dot",
                _batchDotParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert(forwardStype) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBatchDotParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBatchDot(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_batch_dot",
                _backwardBatchDotParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastAddParamNames = Empty;

        /// <summary>
        /// Returns element-wise sum of the input arrays with broadcasting.
        /// 
        /// `broadcast_plus` is an alias to the function `broadcast_add`.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_add(x, y) = [[ 1.,  1.,  1.],
        ///                           [ 2.,  2.,  2.]]
        /// 
        ///    broadcast_plus(x, y) = [[ 1.,  1.,  1.],
        ///                            [ 2.,  2.,  2.]]
        /// 
        /// Supported sparse operations:
        /// 
        ///    broadcast_add(csr, dense(1D)) = dense
        ///    broadcast_add(dense(1D), csr) = dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L58
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastAdd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_add",
                _broadcastAddParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBroadcastAddParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBroadcastAdd(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_broadcast_add",
                _backwardBroadcastAddParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastSubParamNames = Empty;

        /// <summary>
        /// Returns element-wise difference of the input arrays with broadcasting.
        /// 
        /// `broadcast_minus` is an alias to the function `broadcast_sub`.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_sub(x, y) = [[ 1.,  1.,  1.],
        ///                           [ 0.,  0.,  0.]]
        /// 
        ///    broadcast_minus(x, y) = [[ 1.,  1.,  1.],
        ///                             [ 0.,  0.,  0.]]
        /// 
        /// Supported sparse operations:
        /// 
        ///    broadcast_sub/minus(csr, dense(1D)) = dense
        ///    broadcast_sub/minus(dense(1D), csr) = dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L106
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastSub(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_sub",
                _broadcastSubParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBroadcastSubParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBroadcastSub(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_broadcast_sub",
                _backwardBroadcastSubParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastMulParamNames = Empty;

        /// <summary>
        /// Returns element-wise product of the input arrays with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_mul(x, y) = [[ 0.,  0.,  0.],
        ///                           [ 1.,  1.,  1.]]
        /// 
        /// Supported sparse operations:
        /// 
        ///    broadcast_mul(csr, dense(1D)) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L146
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastMul(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_mul",
                _broadcastMulParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBroadcastMulParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBroadcastMul(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_broadcast_mul",
                _backwardBroadcastMulParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastDivParamNames = Empty;

        /// <summary>
        /// Returns element-wise division of the input arrays with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 6.,  6.,  6.],
        ///         [ 6.,  6.,  6.]]
        /// 
        ///    y = [[ 2.],
        ///         [ 3.]]
        /// 
        ///    broadcast_div(x, y) = [[ 3.,  3.,  3.],
        ///                           [ 2.,  2.,  2.]]
        /// 
        /// Supported sparse operations:
        /// 
        ///    broadcast_div(csr, dense(1D)) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L187
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastDiv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_div",
                _broadcastDivParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBroadcastDivParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBroadcastDiv(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_broadcast_div",
                _backwardBroadcastDivParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastModParamNames = Empty;

        /// <summary>
        /// Returns element-wise modulo of the input arrays with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 8.,  8.,  8.],
        ///         [ 8.,  8.,  8.]]
        /// 
        ///    y = [[ 2.],
        ///         [ 3.]]
        /// 
        ///    broadcast_mod(x, y) = [[ 0.,  0.,  0.],
        ///                           [ 2.,  2.,  2.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_basic.cc:L222
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastMod(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_mod",
                _broadcastModParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBroadcastModParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBroadcastMod(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_broadcast_mod",
                _backwardBroadcastModParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastPowerParamNames = Empty;

        /// <summary>
        /// Returns result of first array elements raised to powers from second array, element-wise with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_power(x, y) = [[ 2.,  2.,  2.],
        ///                             [ 4.,  4.,  4.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L45
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastPower(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_power",
                _broadcastPowerParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBroadcastPowerParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBroadcastPower(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_broadcast_power",
                _backwardBroadcastPowerParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastMaximumParamNames = Empty;

        /// <summary>
        /// Returns element-wise maximum of the input arrays with broadcasting.
        /// 
        /// This function compares two input arrays and returns a new array having the element-wise maxima.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_maximum(x, y) = [[ 1.,  1.,  1.],
        ///                               [ 1.,  1.,  1.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L80
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastMaximum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_maximum",
                _broadcastMaximumParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBroadcastMaximumParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBroadcastMaximum(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_broadcast_maximum",
                _backwardBroadcastMaximumParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastMinimumParamNames = Empty;

        /// <summary>
        /// Returns element-wise minimum of the input arrays with broadcasting.
        /// 
        /// This function compares two input arrays and returns a new array having the element-wise minima.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_maximum(x, y) = [[ 0.,  0.,  0.],
        ///                               [ 1.,  1.,  1.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L115
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastMinimum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_minimum",
                _broadcastMinimumParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBroadcastMinimumParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBroadcastMinimum(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_broadcast_minimum",
                _backwardBroadcastMinimumParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastHypotParamNames = Empty;

        /// <summary>
        ///  Returns the hypotenuse of a right angled triangle, given its "legs"
        /// with broadcasting.
        /// 
        /// It is equivalent to doing :math:`sqrt(x_1^2 + x_2^2)`.
        /// 
        /// Example::
        /// 
        ///    x = [[ 3.,  3.,  3.]]
        /// 
        ///    y = [[ 4.],
        ///         [ 4.]]
        /// 
        ///    broadcast_hypot(x, y) = [[ 5.,  5.,  5.],
        ///                             [ 5.,  5.,  5.]]
        /// 
        ///    z = [[ 0.],
        ///         [ 4.]]
        /// 
        ///    broadcast_hypot(x, z) = [[ 3.,  3.,  3.],
        ///                             [ 5.,  5.,  5.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_extended.cc:L156
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastHypot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_hypot",
                _broadcastHypotParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBroadcastHypotParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBroadcastHypot(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_broadcast_hypot",
                _backwardBroadcastHypotParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _broadcastEqualParamNames = Empty;

        /// <summary>
        /// Returns the result of element-wise **equal to** (==) comparison operation with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_equal(x, y) = [[ 0.,  0.,  0.],
        ///                             [ 1.,  1.,  1.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L46
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_equal",
                _broadcastEqualParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastNotEqualParamNames = Empty;

        /// <summary>
        /// Returns the result of element-wise **not equal to** (!=) comparison operation with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_not_equal(x, y) = [[ 1.,  1.,  1.],
        ///                                 [ 0.,  0.,  0.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L64
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastNotEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_not_equal",
                _broadcastNotEqualParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastGreaterParamNames = Empty;

        /// <summary>
        /// Returns the result of element-wise **greater than** (>) comparison operation with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_greater(x, y) = [[ 1.,  1.,  1.],
        ///                               [ 0.,  0.,  0.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L82
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastGreater(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_greater",
                _broadcastGreaterParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastGreaterEqualParamNames = Empty;

        /// <summary>
        /// Returns the result of element-wise **greater than or equal to** (>=) comparison operation with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_greater_equal(x, y) = [[ 1.,  1.,  1.],
        ///                                     [ 1.,  1.,  1.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L100
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastGreaterEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_greater_equal",
                _broadcastGreaterEqualParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastLesserParamNames = Empty;

        /// <summary>
        /// Returns the result of element-wise **lesser than** (<) comparison operation with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_lesser(x, y) = [[ 0.,  0.,  0.],
        ///                              [ 0.,  0.,  0.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L118
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastLesser(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_lesser",
                _broadcastLesserParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastLesserEqualParamNames = Empty;

        /// <summary>
        /// Returns the result of element-wise **lesser than or equal to** (<=) comparison operation with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_lesser_equal(x, y) = [[ 0.,  0.,  0.],
        ///                                    [ 1.,  1.,  1.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L136
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastLesserEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_lesser_equal",
                _broadcastLesserEqualParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastLogicalAndParamNames = Empty;

        /// <summary>
        /// Returns the result of element-wise **logical and** with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  1.],
        ///         [ 1.,  1.,  1.]]
        /// 
        ///    y = [[ 0.],
        ///         [ 1.]]
        /// 
        ///    broadcast_logical_and(x, y) = [[ 0.,  0.,  0.],
        ///                                   [ 1.,  1.,  1.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L154
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastLogicalAnd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_logical_and",
                _broadcastLogicalAndParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastLogicalOrParamNames = Empty;

        /// <summary>
        /// Returns the result of element-wise **logical or** with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  0.],
        ///         [ 1.,  1.,  0.]]
        /// 
        ///    y = [[ 1.],
        ///         [ 0.]]
        /// 
        ///    broadcast_logical_or(x, y) = [[ 1.,  1.,  1.],
        ///                                  [ 1.,  1.,  0.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L172
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastLogicalOr(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_logical_or",
                _broadcastLogicalOrParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _broadcastLogicalXorParamNames = Empty;

        /// <summary>
        /// Returns the result of element-wise **logical xor** with broadcasting.
        /// 
        /// Example::
        /// 
        ///    x = [[ 1.,  1.,  0.],
        ///         [ 1.,  1.,  0.]]
        /// 
        ///    y = [[ 1.],
        ///         [ 0.]]
        /// 
        ///    broadcast_logical_xor(x, y) = [[ 0.,  0.,  1.],
        ///                                   [ 1.,  1.,  0.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_broadcast_op_logic.cc:L190
        /// </summary>
        /// <param name="lhs">First input to the function</param>
        /// <param name="rhs">Second input to the function</param>
        public static NDArray BroadcastLogicalXor(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "broadcast_logical_xor",
                _broadcastLogicalXorParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _elemwiseAddParamNames = Empty;

        /// <summary>
        /// Adds arguments element-wise.
        /// 
        /// The storage type of ``elemwise_add`` output depends on storage types of inputs
        /// 
        ///    - elemwise_add(row_sparse, row_sparse) = row_sparse
        ///    - elemwise_add(csr, csr) = csr
        ///    - elemwise_add(default, csr) = default
        ///    - elemwise_add(csr, default) = default
        ///    - elemwise_add(default, rsp) = default
        ///    - elemwise_add(rsp, default) = default
        ///    - otherwise, ``elemwise_add`` generates output with default storage
        /// 
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray ElemwiseAdd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "elemwise_add",
                _elemwiseAddParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _gradAddParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray GradAdd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_grad_add",
                _gradAddParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardAddParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardAdd(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_add",
                _backwardAddParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _elemwiseSubParamNames = Empty;

        /// <summary>
        /// Subtracts arguments element-wise.
        /// 
        /// The storage type of ``elemwise_sub`` output depends on storage types of inputs
        /// 
        ///    - elemwise_sub(row_sparse, row_sparse) = row_sparse
        ///    - elemwise_sub(csr, csr) = csr
        ///    - elemwise_sub(default, csr) = default
        ///    - elemwise_sub(csr, default) = default
        ///    - elemwise_sub(default, rsp) = default
        ///    - elemwise_sub(rsp, default) = default
        ///    - otherwise, ``elemwise_sub`` generates output with default storage
        /// 
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray ElemwiseSub(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "elemwise_sub",
                _elemwiseSubParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSubParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSub(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_sub",
                _backwardSubParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _elemwiseMulParamNames = Empty;

        /// <summary>
        /// Multiplies arguments element-wise.
        /// 
        /// The storage type of ``elemwise_mul`` output depends on storage types of inputs
        /// 
        ///    - elemwise_mul(default, default) = default
        ///    - elemwise_mul(row_sparse, row_sparse) = row_sparse
        ///    - elemwise_mul(default, row_sparse) = row_sparse
        ///    - elemwise_mul(row_sparse, default) = row_sparse
        ///    - elemwise_mul(csr, csr) = csr
        ///    - otherwise, ``elemwise_mul`` generates output with default storage
        /// 
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray ElemwiseMul(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "elemwise_mul",
                _elemwiseMulParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMulParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMul(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_mul",
                _backwardMulParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _elemwiseDivParamNames = Empty;

        /// <summary>
        /// Divides arguments element-wise.
        /// 
        /// The storage type of ``elemwise_div`` output is always dense
        /// 
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray ElemwiseDiv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "elemwise_div",
                _elemwiseDivParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardDivParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardDiv(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_div",
                _backwardDivParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _modParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray Mod(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_mod",
                _modParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardModParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMod(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_mod",
                _backwardModParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _powerParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray Power(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_power",
                _powerParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardPowerParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardPower(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_power",
                _backwardPowerParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _maximumParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray Maximum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_maximum",
                _maximumParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMaximumParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMaximum(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_maximum",
                _backwardMaximumParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _minimumParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray Minimum(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_minimum",
                _minimumParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMinimumParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMinimum(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_minimum",
                _backwardMinimumParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _hypotParamNames = Empty;

        /// <summary>
        /// Given the "legs" of a right triangle, return its hypotenuse.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_op_extended.cc:L79
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray Hypot(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_hypot",
                _hypotParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardHypotParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardHypot(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_hypot",
                _backwardHypotParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _equalParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray Equal(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_equal",
                _equalParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _notEqualParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray NotEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_not_equal",
                _notEqualParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _greaterParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray Greater(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_greater",
                _greaterParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _greaterEqualParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray GreaterEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_greater_equal",
                _greaterEqualParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _lesserParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray Lesser(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_lesser",
                _lesserParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _lesserEqualParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray LesserEqual(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_lesser_equal",
                _lesserEqualParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _logicalAndParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray LogicalAnd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_and",
                _logicalAndParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _logicalOrParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray LogicalOr(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_or",
                _logicalOrParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _logicalXorParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray LogicalXor(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_xor",
                _logicalXorParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _plusScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray PlusScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_plus_scalar",
                _plusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _minusScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray MinusScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_minus_scalar",
                _minusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _rminusScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray RminusScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_rminus_scalar",
                _rminusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _mulScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// Multiply an array with a scalar.
        /// 
        /// ``_mul_scalar`` only operates on data array of input if input is sparse.
        /// 
        /// For example, if input of shape (100, 100) has only 2 non zero elements,
        /// i.e. input.data = [5, 6], scalar = nan,
        /// it will result output.data = [nan, nan] instead of 10000 nans.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_scalar_op_basic.cc:L149
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray MulScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_mul_scalar",
                _mulScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMulScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray BackwardMulScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_mul_scalar",
                _backwardMulScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _divScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// Divide an array with a scalar.
        /// 
        /// ``_div_scalar`` only operates on data array of input if input is sparse.
        /// 
        /// For example, if input of shape (100, 100) has only 2 non zero elements,
        /// i.e. input.data = [5, 6], scalar = nan,
        /// it will result output.data = [nan, nan] instead of 10000 nans.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_scalar_op_basic.cc:L171
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray DivScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_div_scalar",
                _divScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardDivScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray BackwardDivScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_div_scalar",
                _backwardDivScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _rdivScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray RdivScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_rdiv_scalar",
                _rdivScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardRdivScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        /// <param name="scalar">scalar value</param>
        public static NDArray BackwardRdivScalar(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_rdiv_scalar",
                _backwardRdivScalarParamNames,
                new[] { Convert(scalar) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _modScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray ModScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_mod_scalar",
                _modScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardModScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        /// <param name="scalar">scalar value</param>
        public static NDArray BackwardModScalar(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_mod_scalar",
                _backwardModScalarParamNames,
                new[] { Convert(scalar) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _rmodScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray RmodScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_rmod_scalar",
                _rmodScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardRmodScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        /// <param name="scalar">scalar value</param>
        public static NDArray BackwardRmodScalar(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_rmod_scalar",
                _backwardRmodScalarParamNames,
                new[] { Convert(scalar) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _maximumScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray MaximumScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_maximum_scalar",
                _maximumScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMaximumScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        /// <param name="scalar">scalar value</param>
        public static NDArray BackwardMaximumScalar(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_maximum_scalar",
                _backwardMaximumScalarParamNames,
                new[] { Convert(scalar) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _minimumScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray MinimumScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_minimum_scalar",
                _minimumScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardMinimumScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        /// <param name="scalar">scalar value</param>
        public static NDArray BackwardMinimumScalar(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_minimum_scalar",
                _backwardMinimumScalarParamNames,
                new[] { Convert(scalar) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _powerScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray PowerScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_power_scalar",
                _powerScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardPowerScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        /// <param name="scalar">scalar value</param>
        public static NDArray BackwardPowerScalar(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_power_scalar",
                _backwardPowerScalarParamNames,
                new[] { Convert(scalar) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _rpowerScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray RpowerScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_rpower_scalar",
                _rpowerScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardRpowerScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        /// <param name="scalar">scalar value</param>
        public static NDArray BackwardRpowerScalar(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_rpower_scalar",
                _backwardRpowerScalarParamNames,
                new[] { Convert(scalar) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _hypotScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray HypotScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_hypot_scalar",
                _hypotScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardHypotScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        /// <param name="scalar">scalar value</param>
        public static NDArray BackwardHypotScalar(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_hypot_scalar",
                _backwardHypotScalarParamNames,
                new[] { Convert(scalar) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _smoothL1ParamNames = new[] { "scalar" };

        /// <summary>
        /// Calculate Smooth L1 Loss(lhs, scalar) by summing
        /// 
        /// .. math::
        /// 
        ///     f(x) =
        ///     \begin{cases}
        ///     (\sigma x)^2/2,& \text{if }x < 1/\sigma^2\\
        ///     |x|-0.5/\sigma^2,& \text{otherwise}
        ///     \end{cases}
        /// 
        /// where :math:`x` is an element of the tensor *lhs* and :math:`\sigma` is the scalar.
        /// 
        /// Example::
        /// 
        ///   smooth_l1([1, 2, 3, 4]) = [0.5, 1.5, 2.5, 3.5]
        ///   smooth_l1([1, 2, 3, 4], scalar=1) = [0.5, 1.5, 2.5, 3.5]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_binary_scalar_op_extended.cc:L104
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray SmoothL1(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "smooth_l1",
                _smoothL1ParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSmoothL1ParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardSmoothL1(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_smooth_l1",
                _backwardSmoothL1ParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _equalScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray EqualScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_equal_scalar",
                _equalScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _notEqualScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray NotEqualScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_not_equal_scalar",
                _notEqualScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _greaterScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray GreaterScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_greater_scalar",
                _greaterScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _greaterEqualScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray GreaterEqualScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_greater_equal_scalar",
                _greaterEqualScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _lesserScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray LesserScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_lesser_scalar",
                _lesserScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _lesserEqualScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray LesserEqualScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_lesser_equal_scalar",
                _lesserEqualScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _logicalAndScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray LogicalAndScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_and_scalar",
                _logicalAndScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _logicalOrScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray LogicalOrScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_or_scalar",
                _logicalOrScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _logicalXorScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray LogicalXorScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_logical_xor_scalar",
                _logicalXorScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _scatterElemwiseDivParamNames = Empty;

        /// <summary>
        /// Divides arguments element-wise.  If the left-hand-side input is 'row_sparse', then
        /// only the values which exist in the left-hand sparse array are computed.  The 'missing' values
        /// are ignored.
        /// 
        /// The storage type of ``_scatter_elemwise_div`` output depends on storage types of inputs
        /// 
        /// - _scatter_elemwise_div(row_sparse, row_sparse) = row_sparse
        /// - _scatter_elemwise_div(row_sparse, dense) = row_sparse
        /// - _scatter_elemwise_div(row_sparse, csr) = row_sparse
        /// - otherwise, ``_scatter_elemwise_div`` behaves exactly like elemwise_div and generates output
        /// with default storage
        /// 
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray ScatterElemwiseDiv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_scatter_elemwise_div",
                _scatterElemwiseDivParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _scatterPlusScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// Adds a scalar to a tensor element-wise.  If the left-hand-side input is
        /// 'row_sparse' or 'csr', then only the values which exist in the left-hand sparse array are computed.
        /// The 'missing' values are ignored.
        /// 
        /// The storage type of ``_scatter_plus_scalar`` output depends on storage types of inputs
        /// 
        /// - _scatter_plus_scalar(row_sparse, scalar) = row_sparse
        /// - _scatter_plus_scalar(csr, scalar) = csr
        /// - otherwise, ``_scatter_plus_scalar`` behaves exactly like _plus_scalar and generates output
        /// with default storage
        /// 
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray ScatterPlusScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_scatter_plus_scalar",
                _scatterPlusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _scatterMinusScalarParamNames = new[] { "scalar" };

        /// <summary>
        /// Subtracts a scalar to a tensor element-wise.  If the left-hand-side input is
        /// 'row_sparse' or 'csr', then only the values which exist in the left-hand sparse array are computed.
        /// The 'missing' values are ignored.
        /// 
        /// The storage type of ``_scatter_minus_scalar`` output depends on storage types of inputs
        /// 
        /// - _scatter_minus_scalar(row_sparse, scalar) = row_sparse
        /// - _scatter_minus_scalar(csr, scalar) = csr
        /// - otherwise, ``_scatter_minus_scalar`` behaves exactly like _minus_scalar and generates output
        /// with default storage
        /// 
        /// 
        /// </summary>
        /// <param name="data">source input</param>
        /// <param name="scalar">scalar input</param>
        public static NDArray ScatterMinusScalar(NDArrayOrSymbol data, double scalar, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_scatter_minus_scalar",
                _scatterMinusScalarParamNames,
                new[] { Convert(scalar) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _reluParamNames = Empty;

        /// <summary>
        /// Computes rectified linear activation.
        /// 
        /// .. math::
        ///    max(features, 0)
        /// 
        /// The storage type of ``relu`` output depends upon the input storage type:
        /// 
        ///    - relu(default) = default
        ///    - relu(row_sparse) = row_sparse
        ///    - relu(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L85
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Relu(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "relu",
                _reluParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardReluParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardRelu(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_relu",
                _backwardReluParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _sigmoidParamNames = Empty;

        /// <summary>
        /// Computes sigmoid of x element-wise.
        /// 
        /// .. math::
        ///    y = 1 / (1 + exp(-x))
        /// 
        /// The storage type of ``sigmoid`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L119
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Sigmoid(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sigmoid",
                _sigmoidParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSigmoidParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardSigmoid(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_sigmoid",
                _backwardSigmoidParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _hardSigmoidParamNames = new[] { "alpha", "beta" };

        /// <summary>
        /// Computes hard sigmoid of x element-wise.
        /// 
        /// .. math::
        ///    y = max(0, min(1, alpha * x + beta))
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L133
        /// </summary>
        /// <param name="data">The input array.</param>
        /// <param name="alpha">Slope of hard sigmoid</param>
        /// <param name="beta">Bias of hard sigmoid.</param>
        public static NDArray HardSigmoid(NDArrayOrSymbol data, double alpha = 0.200000003, double beta = 0.5, NDArray output = null)
        {
            var result = Operator.Invoke(
                "hard_sigmoid",
                _hardSigmoidParamNames,
                new[] { Convert(alpha), Convert(beta) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardHardSigmoidParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardHardSigmoid(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_hard_sigmoid",
                _backwardHardSigmoidParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _softsignParamNames = Empty;

        /// <summary>
        /// Computes softsign of x element-wise.
        /// 
        /// .. math::
        ///    y = x / (1 + abs(x))
        /// 
        /// The storage type of ``softsign`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L163
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Softsign(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "softsign",
                _softsignParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSoftsignParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardSoftsign(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_softsign",
                _backwardSoftsignParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _copyParamNames = Empty;

        /// <summary>
        /// Returns a copy of the input.
        /// 
        /// From:C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:218
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Copy(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_copy",
                _copyParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardCopyParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCopy(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_copy",
                _backwardCopyParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardReshapeParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardReshape(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_reshape",
                _backwardReshapeParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _BlockGradParamNames = Empty;

        /// <summary>
        /// Stops gradient computation.
        /// 
        /// Stops the accumulated gradient of the inputs from flowing through this operator
        /// in the backward direction. In other words, this operator prevents the contribution
        /// of its inputs to be taken into account for computing gradients.
        /// 
        /// Example::
        /// 
        ///   v1 = [1, 2]
        ///   v2 = [0, 1]
        ///   a = Variable('a')
        ///   b = Variable('b')
        ///   b_stop_grad = stop_gradient(3 * b)
        ///   loss = MakeLoss(b_stop_grad + a)
        /// 
        ///   executor = loss.simple_bind(ctx=cpu(), a=(1,2), b=(1,2))
        ///   executor.forward(is_train=True, a=v1, b=v2)
        ///   executor.outputs
        ///   [ 1.  5.]
        /// 
        ///   executor.backward()
        ///   executor.grad_arrays
        ///   [ 0.  0.]
        ///   [ 1.  1.]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L299
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray BlockGrad(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "BlockGrad",
                _BlockGradParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _makeLossParamNames = Empty;

        /// <summary>
        /// Make your own loss function in network construction.
        /// 
        /// This operator accepts a customized loss function symbol as a terminal loss and
        /// the symbol should be an operator with no backward dependency.
        /// The output of this function is the gradient of loss with respect to the input data.
        /// 
        /// For example, if you are a making a cross entropy loss function. Assume ``out`` is the
        /// predicted output and ``label`` is the true label, then the cross entropy can be defined as::
        /// 
        ///   cross_entropy = label * log(out) + (1 - label) * log(1 - out)
        ///   loss = make_loss(cross_entropy)
        /// 
        /// We will need to use ``make_loss`` when we are creating our own loss function or we want to
        /// combine multiple loss functions. Also we may want to stop some variables' gradients
        /// from backpropagation. See more detail in ``BlockGrad`` or ``stop_gradient``.
        /// 
        /// The storage type of ``make_loss`` output depends upon the input storage type:
        /// 
        ///    - make_loss(default) = default
        ///    - make_loss(row_sparse) = row_sparse
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L332
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray MakeLoss(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "make_loss",
                _makeLossParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _identityWithAttrLikeRhsParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">First input.</param>
        /// <param name="rhs">Second input.</param>
        public static NDArray IdentityWithAttrLikeRhs(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_identity_with_attr_like_rhs",
                _identityWithAttrLikeRhsParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _reshapeLikeParamNames = Empty;

        /// <summary>
        /// Reshape some or all dimensions of `lhs` to have the same shape as some or all dimensions of `rhs`.
        /// 
        /// Returns a **view** of the `lhs` array with a new shape without altering any data.
        /// 
        /// Example::
        /// 
        ///   x = [1, 2, 3, 4, 5, 6]
        ///   y = [[0, -4], [3, 2], [2, 2]]
        ///   reshape_like(x, y) = [[1, 2], [3, 4], [5, 6]]
        /// 
        /// More precise control over how dimensions are inherited is achieved by specifying \
        /// slices over the `lhs` and `rhs` array dimensions. Only the sliced `lhs` dimensions \
        /// are reshaped to the `rhs` sliced dimensions, with the non-sliced `lhs` dimensions staying the same.
        /// 
        ///   Examples::
        /// 
        ///   - lhs shape = (30,7), rhs shape = (15,2,4), lhs_begin=0, lhs_end=1, rhs_begin=0, rhs_end=2, output shape = (15,2,7)
        ///   - lhs shape = (3, 5), rhs shape = (1,15,4), lhs_begin=0, lhs_end=2, rhs_begin=1, rhs_end=2, output shape = (15)
        /// 
        /// Negative indices are supported, and `None` can be used for either `lhs_end` or `rhs_end` to indicate the end of the range.
        /// 
        ///   Example::
        /// 
        ///   - lhs shape = (30, 12), rhs shape = (4, 2, 2, 3), lhs_begin=-1, lhs_end=None, rhs_begin=1, rhs_end=None, output shape = (30, 2, 2, 3)
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L485
        /// </summary>
        /// <param name="lhs">First input.</param>
        /// <param name="rhs">Second input.</param>
        public static NDArray ReshapeLike(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "reshape_like",
                _reshapeLikeParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _shapeArrayParamNames = new[] { "lhs_begin", "lhs_end", "rhs_begin", "rhs_end" };

        /// <summary>
        /// Returns a 1D int64 array containing the shape of data.
        /// 
        /// Example::
        /// 
        ///   shape_array([[1,2,3,4], [5,6,7,8]]) = [2,4]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L544
        /// </summary>
        /// <param name="data">Input Array.</param>
        /// <param name="lhs_begin">Defaults to 0. The beginning index along which the lhs dimensions are to be reshaped. Supports negative indices.</param>
        /// <param name="lhs_end">Defaults to None. The ending index along which the lhs dimensions are to be used for reshaping. Supports negative indices.</param>
        /// <param name="rhs_begin">Defaults to 0. The beginning index along which the rhs dimensions are to be used for reshaping. Supports negative indices.</param>
        /// <param name="rhs_end">Defaults to None. The ending index along which the rhs dimensions are to be used for reshaping. Supports negative indices.</param>
        public static NDArray ShapeArray(NDArrayOrSymbol data, int? lhsBegin = null, int? lhsEnd = null, int? rhsBegin = null, int? rhsEnd = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "shape_array",
                _shapeArrayParamNames,
                new[] { Convert(lhsBegin), Convert(lhsEnd), Convert(rhsBegin), Convert(rhsEnd) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _sizeArrayParamNames = Empty;

        /// <summary>
        /// Returns a 1D int64 array containing the size of data.
        /// 
        /// Example::
        /// 
        ///   size_array([[1,2,3,4], [5,6,7,8]]) = [8]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L596
        /// </summary>
        /// <param name="data">Input Array.</param>
        public static NDArray SizeArray(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "size_array",
                _sizeArrayParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _CastParamNames = new[] { "dtype" };

        /// <summary>
        /// Casts all elements of the input to a new type.
        /// 
        /// .. note:: ``Cast`` is deprecated. Use ``cast`` instead.
        /// 
        /// Example::
        /// 
        ///    cast([0.9, 1.3], dtype='int32') = [0, 1]
        ///    cast([1e20, 11.1], dtype='float16') = [inf, 11.09375]
        ///    cast([300, 11.1, 10.9, -1, -3], dtype='uint8') = [44, 11, 10, 255, 253]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L634
        /// </summary>
        /// <param name="data">The input.</param>
        /// <param name="dtype">Output data type.</param>
        public static NDArray Cast(NDArrayOrSymbol data, string dtype, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Cast",
                _CastParamNames,
                new[] { Convert(dtype) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardCastParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCast(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_cast",
                _backwardCastParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _negativeParamNames = Empty;

        /// <summary>
        /// Numerical negative of the argument, element-wise.
        /// 
        /// The storage type of ``negative`` output depends upon the input storage type:
        /// 
        ///    - negative(default) = default
        ///    - negative(row_sparse) = row_sparse
        ///    - negative(csr) = csr
        /// 
        /// 
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Negative(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "negative",
                _negativeParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _reciprocalParamNames = Empty;

        /// <summary>
        /// Returns the reciprocal of the argument, element-wise.
        /// 
        /// Calculates 1/x.
        /// 
        /// Example::
        /// 
        ///     reciprocal([-2, 1, 3, 1.6, 0.2]) = [-0.5, 1.0, 0.33333334, 0.625, 5.0]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L686
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Reciprocal(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "reciprocal",
                _reciprocalParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardReciprocalParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardReciprocal(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_reciprocal",
                _backwardReciprocalParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _absParamNames = Empty;

        /// <summary>
        /// Returns element-wise absolute value of the input.
        /// 
        /// Example::
        /// 
        ///    abs([-2, 0, 3]) = [2, 0, 3]
        /// 
        /// The storage type of ``abs`` output depends upon the input storage type:
        /// 
        ///    - abs(default) = default
        ///    - abs(row_sparse) = row_sparse
        ///    - abs(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L708
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Abs(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "abs",
                _absParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardAbsParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardAbs(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_abs",
                _backwardAbsParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _signParamNames = Empty;

        /// <summary>
        /// Returns element-wise sign of the input.
        /// 
        /// Example::
        /// 
        ///    sign([-2, 0, 3]) = [-1, 0, 1]
        /// 
        /// The storage type of ``sign`` output depends upon the input storage type:
        /// 
        ///    - sign(default) = default
        ///    - sign(row_sparse) = row_sparse
        ///    - sign(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L727
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Sign(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sign",
                _signParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSignParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardSign(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_sign",
                _backwardSignParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _roundParamNames = Empty;

        /// <summary>
        /// Returns element-wise rounded value to the nearest integer of the input.
        /// 
        /// Example::
        /// 
        ///    round([-1.5, 1.5, -1.9, 1.9, 2.1]) = [-2.,  2., -2.,  2.,  2.]
        /// 
        /// The storage type of ``round`` output depends upon the input storage type:
        /// 
        ///   - round(default) = default
        ///   - round(row_sparse) = row_sparse
        ///   - round(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L746
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Round(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "round",
                _roundParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _rintParamNames = Empty;

        /// <summary>
        /// Returns element-wise rounded value to the nearest integer of the input.
        /// 
        /// .. note::
        ///    - For input ``n.5`` ``rint`` returns ``n`` while ``round`` returns ``n+1``.
        ///    - For input ``-n.5`` both ``rint`` and ``round`` returns ``-n-1``.
        /// 
        /// Example::
        /// 
        ///    rint([-1.5, 1.5, -1.9, 1.9, 2.1]) = [-2.,  1., -2.,  2.,  2.]
        /// 
        /// The storage type of ``rint`` output depends upon the input storage type:
        /// 
        ///    - rint(default) = default
        ///    - rint(row_sparse) = row_sparse
        ///    - rint(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L767
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Rint(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rint",
                _rintParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _ceilParamNames = Empty;

        /// <summary>
        /// Returns element-wise ceiling of the input.
        /// 
        /// The ceil of the scalar x is the smallest integer i, such that i >= x.
        /// 
        /// Example::
        /// 
        ///    ceil([-2.1, -1.9, 1.5, 1.9, 2.1]) = [-2., -1.,  2.,  2.,  3.]
        /// 
        /// The storage type of ``ceil`` output depends upon the input storage type:
        /// 
        ///    - ceil(default) = default
        ///    - ceil(row_sparse) = row_sparse
        ///    - ceil(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L786
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Ceil(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ceil",
                _ceilParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _floorParamNames = Empty;

        /// <summary>
        /// Returns element-wise floor of the input.
        /// 
        /// The floor of the scalar x is the largest integer i, such that i <= x.
        /// 
        /// Example::
        /// 
        ///    floor([-2.1, -1.9, 1.5, 1.9, 2.1]) = [-3., -2.,  1.,  1.,  2.]
        /// 
        /// The storage type of ``floor`` output depends upon the input storage type:
        /// 
        ///    - floor(default) = default
        ///    - floor(row_sparse) = row_sparse
        ///    - floor(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L805
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Floor(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "floor",
                _floorParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _truncParamNames = Empty;

        /// <summary>
        /// Return the element-wise truncated value of the input.
        /// 
        /// The truncated value of the scalar x is the nearest integer i which is closer to
        /// zero than x is. In short, the fractional part of the signed number x is discarded.
        /// 
        /// Example::
        /// 
        ///    trunc([-2.1, -1.9, 1.5, 1.9, 2.1]) = [-2., -1.,  1.,  1.,  2.]
        /// 
        /// The storage type of ``trunc`` output depends upon the input storage type:
        /// 
        ///    - trunc(default) = default
        ///    - trunc(row_sparse) = row_sparse
        ///    - trunc(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L825
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Trunc(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "trunc",
                _truncParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _fixParamNames = Empty;

        /// <summary>
        /// Returns element-wise rounded value to the nearest \
        /// integer towards zero of the input.
        /// 
        /// Example::
        /// 
        ///    fix([-2.1, -1.9, 1.9, 2.1]) = [-2., -1.,  1., 2.]
        /// 
        /// The storage type of ``fix`` output depends upon the input storage type:
        /// 
        ///    - fix(default) = default
        ///    - fix(row_sparse) = row_sparse
        ///    - fix(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L843
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Fix(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "fix",
                _fixParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _squareParamNames = Empty;

        /// <summary>
        /// Returns element-wise squared value of the input.
        /// 
        /// .. math::
        ///    square(x) = x^2
        /// 
        /// Example::
        /// 
        ///    square([2, 3, 4]) = [4, 9, 16]
        /// 
        /// The storage type of ``square`` output depends upon the input storage type:
        /// 
        ///    - square(default) = default
        ///    - square(row_sparse) = row_sparse
        ///    - square(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L883
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Square(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "square",
                _squareParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSquareParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardSquare(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_square",
                _backwardSquareParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _sqrtParamNames = Empty;

        /// <summary>
        /// Returns element-wise square-root value of the input.
        /// 
        /// .. math::
        ///    \textrm{sqrt}(x) = \sqrt{x}
        /// 
        /// Example::
        /// 
        ///    sqrt([4, 9, 16]) = [2, 3, 4]
        /// 
        /// The storage type of ``sqrt`` output depends upon the input storage type:
        /// 
        ///    - sqrt(default) = default
        ///    - sqrt(row_sparse) = row_sparse
        ///    - sqrt(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L907
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Sqrt(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sqrt",
                _sqrtParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSqrtParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardSqrt(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_sqrt",
                _backwardSqrtParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _rsqrtParamNames = Empty;

        /// <summary>
        /// Returns element-wise inverse square-root value of the input.
        /// 
        /// .. math::
        ///    rsqrt(x) = 1/\sqrt{x}
        /// 
        /// Example::
        /// 
        ///    rsqrt([4,9,16]) = [0.5, 0.33333334, 0.25]
        /// 
        /// The storage type of ``rsqrt`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L927
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Rsqrt(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rsqrt",
                _rsqrtParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardRsqrtParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardRsqrt(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_rsqrt",
                _backwardRsqrtParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _cbrtParamNames = Empty;

        /// <summary>
        /// Returns element-wise cube-root value of the input.
        /// 
        /// .. math::
        ///    cbrt(x) = \sqrt[3]{x}
        /// 
        /// Example::
        /// 
        ///    cbrt([1, 8, -125]) = [1, 2, -5]
        /// 
        /// The storage type of ``cbrt`` output depends upon the input storage type:
        /// 
        ///    - cbrt(default) = default
        ///    - cbrt(row_sparse) = row_sparse
        ///    - cbrt(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L950
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Cbrt(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "cbrt",
                _cbrtParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardCbrtParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardCbrt(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_cbrt",
                _backwardCbrtParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _erfParamNames = Empty;

        /// <summary>
        /// Returns element-wise gauss error function of the input.
        /// 
        /// Example::
        /// 
        ///    erf([0, -1., 10.]) = [0., -0.8427, 1.]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L964
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Erf(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "erf",
                _erfParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardErfParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardErf(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_erf",
                _backwardErfParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _erfinvParamNames = Empty;

        /// <summary>
        /// Returns element-wise inverse gauss error function of the input.
        /// 
        /// Example::
        /// 
        ///    erfinv([0, 0.5., -1.]) = [0., 0.4769, -inf]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L985
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Erfinv(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "erfinv",
                _erfinvParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardErfinvParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardErfinv(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_erfinv",
                _backwardErfinvParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _rcbrtParamNames = Empty;

        /// <summary>
        /// Returns element-wise inverse cube-root value of the input.
        /// 
        /// .. math::
        ///    rcbrt(x) = 1/\sqrt[3]{x}
        /// 
        /// Example::
        /// 
        ///    rcbrt([1,8,-125]) = [1.0, 0.5, -0.2]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L1004
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Rcbrt(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "rcbrt",
                _rcbrtParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardRcbrtParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardRcbrt(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_rcbrt",
                _backwardRcbrtParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _expParamNames = Empty;

        /// <summary>
        /// Returns element-wise exponential value of the input.
        /// 
        /// .. math::
        ///    exp(x) = e^x \approx 2.718^x
        /// 
        /// Example::
        /// 
        ///    exp([0, 1, 2]) = [1., 2.71828175, 7.38905621]
        /// 
        /// The storage type of ``exp`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L1044
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Exp(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "exp",
                _expParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _logParamNames = Empty;

        /// <summary>
        /// Returns element-wise Natural logarithmic value of the input.
        /// 
        /// The natural logarithm is logarithm in base *e*, so that ``log(exp(x)) = x``
        /// 
        /// The storage type of ``log`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L1057
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Log(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log",
                _logParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _log10ParamNames = Empty;

        /// <summary>
        /// Returns element-wise Base-10 logarithmic value of the input.
        /// 
        /// ``10**log10(x) = x``
        /// 
        /// The storage type of ``log10`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L1074
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Log10(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log10",
                _log10ParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _log2ParamNames = Empty;

        /// <summary>
        /// Returns element-wise Base-2 logarithmic value of the input.
        /// 
        /// ``2**log2(x) = x``
        /// 
        /// The storage type of ``log2`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L1086
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Log2(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log2",
                _log2ParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLogParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardLog(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_log",
                _backwardLogParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLog10ParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardLog10(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_log10",
                _backwardLog10ParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLog2ParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardLog2(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_log2",
                _backwardLog2ParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _log1pParamNames = Empty;

        /// <summary>
        /// Returns element-wise ``log(1 + x)`` value of the input.
        /// 
        /// This function is more accurate than ``log(1 + x)``  for small ``x`` so that
        /// :math:`1+x\approx 1`
        /// 
        /// The storage type of ``log1p`` output depends upon the input storage type:
        /// 
        ///    - log1p(default) = default
        ///    - log1p(row_sparse) = row_sparse
        ///    - log1p(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L1171
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Log1p(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "log1p",
                _log1pParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLog1pParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardLog1p(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_log1p",
                _backwardLog1pParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _expm1ParamNames = Empty;

        /// <summary>
        /// Returns ``exp(x) - 1`` computed element-wise on the input.
        /// 
        /// This function provides greater precision than ``exp(x) - 1`` for small values of ``x``.
        /// 
        /// The storage type of ``expm1`` output depends upon the input storage type:
        /// 
        ///    - expm1(default) = default
        ///    - expm1(row_sparse) = row_sparse
        ///    - expm1(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_basic.cc:L1189
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Expm1(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "expm1",
                _expm1ParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardExpm1ParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardExpm1(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_expm1",
                _backwardExpm1ParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _gammaParamNames = Empty;

        /// <summary>
        /// Returns the gamma function (extension of the factorial function \
        /// to the reals), computed element-wise on the input array.
        /// 
        /// The storage type of ``gamma`` output is always dense
        /// 
        /// 
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Gamma(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "gamma",
                _gammaParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardGammaParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardGamma(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_gamma",
                _backwardGammaParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _gammalnParamNames = Empty;

        /// <summary>
        /// Returns element-wise log of the absolute value of the gamma function \
        /// of the input.
        /// 
        /// The storage type of ``gammaln`` output is always dense
        /// 
        /// 
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Gammaln(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "gammaln",
                _gammalnParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardGammalnParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardGammaln(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_gammaln",
                _backwardGammalnParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _logicalNotParamNames = Empty;

        /// <summary>
        /// Returns the result of logical NOT (!) function
        /// 
        /// Example:
        ///   logical_not([-2., 0., 1.]) = [0., 1., 0.]
        /// 
        /// 
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray LogicalNot(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "logical_not",
                _logicalNotParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _sinParamNames = Empty;

        /// <summary>
        /// Computes the element-wise sine of the input array.
        /// 
        /// The input should be in radians (:math:`2\pi` rad equals 360 degrees).
        /// 
        /// .. math::
        ///    sin([0, \pi/4, \pi/2]) = [0, 0.707, 1]
        /// 
        /// The storage type of ``sin`` output depends upon the input storage type:
        /// 
        ///    - sin(default) = default
        ///    - sin(row_sparse) = row_sparse
        ///    - sin(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L46
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Sin(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sin",
                _sinParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSinParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardSin(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_sin",
                _backwardSinParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _cosParamNames = Empty;

        /// <summary>
        /// Computes the element-wise cosine of the input array.
        /// 
        /// The input should be in radians (:math:`2\pi` rad equals 360 degrees).
        /// 
        /// .. math::
        ///    cos([0, \pi/4, \pi/2]) = [1, 0.707, 0]
        /// 
        /// The storage type of ``cos`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L89
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Cos(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "cos",
                _cosParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardCosParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardCos(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_cos",
                _backwardCosParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _tanParamNames = Empty;

        /// <summary>
        /// Computes the element-wise tangent of the input array.
        /// 
        /// The input should be in radians (:math:`2\pi` rad equals 360 degrees).
        /// 
        /// .. math::
        ///    tan([0, \pi/4, \pi/2]) = [0, 1, -inf]
        /// 
        /// The storage type of ``tan`` output depends upon the input storage type:
        /// 
        ///    - tan(default) = default
        ///    - tan(row_sparse) = row_sparse
        ///    - tan(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L139
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Tan(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "tan",
                _tanParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardTanParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardTan(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_tan",
                _backwardTanParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _arcsinParamNames = Empty;

        /// <summary>
        /// Returns element-wise inverse sine of the input array.
        /// 
        /// The input should be in the range `[-1, 1]`.
        /// The output is in the closed interval of [:math:`-\pi/2`, :math:`\pi/2`].
        /// 
        /// .. math::
        ///    arcsin([-1, -.707, 0, .707, 1]) = [-\pi/2, -\pi/4, 0, \pi/4, \pi/2]
        /// 
        /// The storage type of ``arcsin`` output depends upon the input storage type:
        /// 
        ///    - arcsin(default) = default
        ///    - arcsin(row_sparse) = row_sparse
        ///    - arcsin(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L160
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Arcsin(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arcsin",
                _arcsinParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardArcsinParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardArcsin(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_arcsin",
                _backwardArcsinParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _arccosParamNames = Empty;

        /// <summary>
        /// Returns element-wise inverse cosine of the input array.
        /// 
        /// The input should be in range `[-1, 1]`.
        /// The output is in the closed interval :math:`[0, \pi]`
        /// 
        /// .. math::
        ///    arccos([-1, -.707, 0, .707, 1]) = [\pi, 3\pi/4, \pi/2, \pi/4, 0]
        /// 
        /// The storage type of ``arccos`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L179
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Arccos(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arccos",
                _arccosParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardArccosParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardArccos(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_arccos",
                _backwardArccosParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _arctanParamNames = Empty;

        /// <summary>
        /// Returns element-wise inverse tangent of the input array.
        /// 
        /// The output is in the closed interval :math:`[-\pi/2, \pi/2]`
        /// 
        /// .. math::
        ///    arctan([-1, 0, 1]) = [-\pi/4, 0, \pi/4]
        /// 
        /// The storage type of ``arctan`` output depends upon the input storage type:
        /// 
        ///    - arctan(default) = default
        ///    - arctan(row_sparse) = row_sparse
        ///    - arctan(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L200
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Arctan(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arctan",
                _arctanParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardArctanParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardArctan(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_arctan",
                _backwardArctanParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _degreesParamNames = Empty;

        /// <summary>
        /// Converts each element of the input array from radians to degrees.
        /// 
        /// .. math::
        ///    degrees([0, \pi/2, \pi, 3\pi/2, 2\pi]) = [0, 90, 180, 270, 360]
        /// 
        /// The storage type of ``degrees`` output depends upon the input storage type:
        /// 
        ///    - degrees(default) = default
        ///    - degrees(row_sparse) = row_sparse
        ///    - degrees(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L219
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Degrees(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "degrees",
                _degreesParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardDegreesParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardDegrees(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_degrees",
                _backwardDegreesParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _radiansParamNames = Empty;

        /// <summary>
        /// Converts each element of the input array from degrees to radians.
        /// 
        /// .. math::
        ///    radians([0, 90, 180, 270, 360]) = [0, \pi/2, \pi, 3\pi/2, 2\pi]
        /// 
        /// The storage type of ``radians`` output depends upon the input storage type:
        /// 
        ///    - radians(default) = default
        ///    - radians(row_sparse) = row_sparse
        ///    - radians(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L238
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Radians(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "radians",
                _radiansParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardRadiansParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardRadians(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_radians",
                _backwardRadiansParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _sinhParamNames = Empty;

        /// <summary>
        /// Returns the hyperbolic sine of the input array, computed element-wise.
        /// 
        /// .. math::
        ///    sinh(x) = 0.5\times(exp(x) - exp(-x))
        /// 
        /// The storage type of ``sinh`` output depends upon the input storage type:
        /// 
        ///    - sinh(default) = default
        ///    - sinh(row_sparse) = row_sparse
        ///    - sinh(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L257
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Sinh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sinh",
                _sinhParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSinhParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardSinh(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_sinh",
                _backwardSinhParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _coshParamNames = Empty;

        /// <summary>
        /// Returns the hyperbolic cosine  of the input array, computed element-wise.
        /// 
        /// .. math::
        ///    cosh(x) = 0.5\times(exp(x) + exp(-x))
        /// 
        /// The storage type of ``cosh`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L272
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Cosh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "cosh",
                _coshParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardCoshParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardCosh(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_cosh",
                _backwardCoshParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _tanhParamNames = Empty;

        /// <summary>
        /// Returns the hyperbolic tangent of the input array, computed element-wise.
        /// 
        /// .. math::
        ///    tanh(x) = sinh(x) / cosh(x)
        /// 
        /// The storage type of ``tanh`` output depends upon the input storage type:
        /// 
        ///    - tanh(default) = default
        ///    - tanh(row_sparse) = row_sparse
        ///    - tanh(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L290
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Tanh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "tanh",
                _tanhParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardTanhParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardTanh(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_tanh",
                _backwardTanhParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _arcsinhParamNames = Empty;

        /// <summary>
        /// Returns the element-wise inverse hyperbolic sine of the input array, \
        /// computed element-wise.
        /// 
        /// The storage type of ``arcsinh`` output depends upon the input storage type:
        /// 
        ///    - arcsinh(default) = default
        ///    - arcsinh(row_sparse) = row_sparse
        ///    - arcsinh(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L306
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Arcsinh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arcsinh",
                _arcsinhParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardArcsinhParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardArcsinh(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_arcsinh",
                _backwardArcsinhParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _arccoshParamNames = Empty;

        /// <summary>
        /// Returns the element-wise inverse hyperbolic cosine of the input array, \
        /// computed element-wise.
        /// 
        /// The storage type of ``arccosh`` output is always dense
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L320
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Arccosh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arccosh",
                _arccoshParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardArccoshParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardArccosh(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_arccosh",
                _backwardArccoshParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _arctanhParamNames = Empty;

        /// <summary>
        /// Returns the element-wise inverse hyperbolic tangent of the input array, \
        /// computed element-wise.
        /// 
        /// The storage type of ``arctanh`` output depends upon the input storage type:
        /// 
        ///    - arctanh(default) = default
        ///    - arctanh(row_sparse) = row_sparse
        ///    - arctanh(csr) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\elemwise_unary_op_trig.cc:L337
        /// </summary>
        /// <param name="data">The input array.</param>
        public static NDArray Arctanh(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "arctanh",
                _arctanhParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardArctanhParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">first input</param>
        /// <param name="rhs">second input</param>
        public static NDArray BackwardArctanh(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_arctanh",
                _backwardArctanhParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _histogramParamNames = new[] { "bin_cnt", "range" };

        /// <summary>
        /// This operators implements the histogram function.
        /// 
        /// Example::
        ///   x = [[0, 1], [2, 2], [3, 4]]
        ///   histo, bin_edges = histogram(data=x, bin_bounds=[], bin_cnt=5, range=(0,5))
        ///   histo = [1, 1, 2, 1, 1]
        ///   bin_edges = [0., 1., 2., 3., 4.]
        ///   histo, bin_edges = histogram(data=x, bin_bounds=[0., 2.1, 3.])
        ///   histo = [4, 1]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\histogram.cc:L136
        /// </summary>
        /// <param name="data">Input ndarray</param>
        /// <param name="bins">Input ndarray</param>
        /// <param name="bin_cnt">Number of bins for uniform case</param>
        /// <param name="range">The lower and upper range of the bins. if not provided, range is simply (a.min(), a.max()). values outside the range are ignored. the first element of the range must be less than or equal to the second. range affects the automatic bin computation as well. while bin width is computed to be optimal based on the actual data within range, the bin count will fill the entire range including portions containing no data.</param>
        public static NDArray Histogram(NDArrayOrSymbol data, NDArrayOrSymbol bins, int? binCnt = null, double? range = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_histogram",
                _histogramParamNames,
                new[] { Convert(binCnt), Convert(range) },
                new[] { data.Handle, bins.Handle },
                output
            );
            return result;
        }

        private static string[] _EmbeddingParamNames = new[] { "input_dim", "output_dim", "dtype", "sparse_grad" };

        /// <summary>
        /// Maps integer indices to vector representations (embeddings).
        /// 
        /// This operator maps words to real-valued vectors in a high-dimensional space,
        /// called word embeddings. These embeddings can capture semantic and syntactic properties of the words.
        /// For example, it has been noted that in the learned embedding spaces, similar words tend
        /// to be close to each other and dissimilar words far apart.
        /// 
        /// For an input array of shape (d1, ..., dK),
        /// the shape of an output array is (d1, ..., dK, output_dim).
        /// All the input values should be integers in the range [0, input_dim).
        /// 
        /// If the input_dim is ip0 and output_dim is op0, then shape of the embedding weight matrix must be
        /// (ip0, op0).
        /// 
        /// By default, if any index mentioned is too large, it is replaced by the index that addresses
        /// the last vector in an embedding matrix.
        /// 
        /// Examples::
        /// 
        ///   input_dim = 4
        ///   output_dim = 5
        /// 
        ///   // Each row in weight matrix y represents a word. So, y = (w0,w1,w2,w3)
        ///   y = [[  0.,   1.,   2.,   3.,   4.],
        ///        [  5.,   6.,   7.,   8.,   9.],
        ///        [ 10.,  11.,  12.,  13.,  14.],
        ///        [ 15.,  16.,  17.,  18.,  19.]]
        /// 
        ///   // Input array x represents n-grams(2-gram). So, x = [(w1,w3), (w0,w2)]
        ///   x = [[ 1.,  3.],
        ///        [ 0.,  2.]]
        /// 
        ///   // Mapped input x to its vector representation y.
        ///   Embedding(x, y, 4, 5) = [[[  5.,   6.,   7.,   8.,   9.],
        ///                             [ 15.,  16.,  17.,  18.,  19.]],
        /// 
        ///                            [[  0.,   1.,   2.,   3.,   4.],
        ///                             [ 10.,  11.,  12.,  13.,  14.]]]
        /// 
        /// 
        /// The storage type of weight can be either row_sparse or default.
        /// 
        /// .. Note::
        /// 
        ///     If "sparse_grad" is set to True, the storage type of gradient w.r.t weights will be
        ///     "row_sparse". Only a subset of optimizers support sparse gradients, including SGD, AdaGrad
        ///     and Adam. Note that by default lazy updates is turned on, which may perform differently
        ///     from standard updates. For more details, please check the Optimization API at:
        ///     https://mxnet.incubator.apache.org/api/python/optimization/optimization.html
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\indexing_op.cc:L519
        /// </summary>
        /// <param name="data">The input array to the embedding operator.</param>
        /// <param name="weight">The embedding weight matrix.</param>
        /// <param name="input_dim">Vocabulary size of the input indices.</param>
        /// <param name="output_dim">Dimension of the embedding vectors.</param>
        /// <param name="dtype">Data type of weight.</param>
        /// <param name="sparse_grad">Compute row sparse gradient in the backward calculation. If set to True, the grad's storage type is row_sparse.</param>
        public static NDArray Embedding(NDArrayOrSymbol data, NDArrayOrSymbol weight, int inputDim, int outputDim, string dtype = "float32", bool sparseGrad = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Embedding",
                _EmbeddingParamNames,
                new[] { Convert(inputDim), Convert(outputDim), Convert(dtype), Convert(sparseGrad) },
                new[] { data.Handle, weight.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardEmbeddingParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardEmbedding(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Embedding",
                _backwardEmbeddingParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardSparseEmbeddingParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSparseEmbedding(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SparseEmbedding",
                _backwardSparseEmbeddingParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _takeParamNames = new[] { "axis", "mode" };

        /// <summary>
        /// Takes elements from an input array along the given axis.
        /// 
        /// This function slices the input array along a particular axis with the provided indices.
        /// 
        /// Given data tensor of rank r >= 1, and indices tensor of rank q, gather entries of the axis
        /// dimension of data (by default outer-most one as axis=0) indexed by indices, and concatenates them
        /// in an output tensor of rank q + (r - 1).
        /// 
        /// Examples::
        /// 
        ///   x = [4.  5.  6.]
        /// 
        ///   // Trivial case, take the second element along the first axis.
        /// 
        ///   take(x, [1]) = [ 5. ]
        /// 
        ///   // The other trivial case, axis=-1, take the third element along the first axis
        /// 
        ///   take(x, [3], axis=-1, mode='clip') = [ 6. ]
        /// 
        ///   x = [[ 1.,  2.],
        ///        [ 3.,  4.],
        ///        [ 5.,  6.]]
        /// 
        ///   // In this case we will get rows 0 and 1, then 1 and 2. Along axis 0
        /// 
        ///   take(x, [[0,1],[1,2]]) = [[[ 1.,  2.],
        ///                              [ 3.,  4.]],
        /// 
        ///                             [[ 3.,  4.],
        ///                              [ 5.,  6.]]]
        /// 
        ///   // In this case we will get rows 0 and 1, then 1 and 2 (calculated by wrapping around).
        ///   // Along axis 1
        /// 
        ///   take(x, [[0, 3], [-1, -2]], axis=1, mode='wrap') = [[[ 1.  2.]
        ///                                                        [ 2.  1.]]
        /// 
        ///                                                       [[ 3.  4.]
        ///                                                        [ 4.  3.]]
        /// 
        ///                                                       [[ 5.  6.]
        ///                                                        [ 6.  5.]]]
        /// 
        /// The storage type of ``take`` output depends upon the input storage type:
        /// 
        ///    - take(default, default) = default
        ///    - take(csr, default, axis=0) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\indexing_op.cc:L695
        /// </summary>
        /// <param name="a">The input array.</param>
        /// <param name="indices">The indices of the values to be extracted.</param>
        /// <param name="axis">The axis of input array to be taken.For input tensor of rank r, it could be in the range of [-r, r-1]</param>
        /// <param name="mode">Specify how out-of-bound indices bahave. Default is "clip". "clip" means clip to the range. So, if all indices mentioned are too large, they are replaced by the index that addresses the last element along an axis.  "wrap" means to wrap around.  "raise" means to raise an error, not supported yet.</param>
        public static NDArray Take(NDArrayOrSymbol a, NDArrayOrSymbol indices, int axis = 0, string mode = "clip", NDArray output = null)
        {
            var result = Operator.Invoke(
                "take",
                _takeParamNames,
                new[] { Convert(axis), Convert(mode) },
                new[] { a.Handle, indices.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardTakeParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardTake(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_take",
                _backwardTakeParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _batchTakeParamNames = Empty;

        /// <summary>
        /// Takes elements from a data batch.
        /// 
        /// .. note::
        ///   `batch_take` is deprecated. Use `pick` instead.
        /// 
        /// Given an input array of shape ``(d0, d1)`` and indices of shape ``(i0,)``, the result will be
        /// an output array of shape ``(i0,)`` with::
        /// 
        ///   output[i] = input[i, indices[i]]
        /// 
        /// Examples::
        /// 
        ///   x = [[ 1.,  2.],
        ///        [ 3.,  4.],
        ///        [ 5.,  6.]]
        /// 
        ///   // takes elements with specified indices
        ///   batch_take(x, [0,1,0]) = [ 1.  4.  5.]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\indexing_op.cc:L753
        /// </summary>
        /// <param name="a">The input array</param>
        /// <param name="indices">The index array</param>
        public static NDArray BatchTake(NDArrayOrSymbol a, NDArrayOrSymbol indices, NDArray output = null)
        {
            var result = Operator.Invoke(
                "batch_take",
                _batchTakeParamNames,
                Empty,
                new[] { a.Handle, indices.Handle },
                output
            );
            return result;
        }

        private static string[] _oneHotParamNames = new[] { "depth", "on_value", "off_value", "dtype" };

        /// <summary>
        /// Returns a one-hot array.
        /// 
        /// The locations represented by `indices` take value `on_value`, while all
        /// other locations take value `off_value`.
        /// 
        /// `one_hot` operation with `indices` of shape ``(i0, i1)`` and `depth`  of ``d`` would result
        /// in an output array of shape ``(i0, i1, d)`` with::
        /// 
        ///   output[i,j,:] = off_value
        ///   output[i,j,indices[i,j]] = on_value
        /// 
        /// Examples::
        /// 
        ///   one_hot([1,0,2,0], 3) = [[ 0.  1.  0.]
        ///                            [ 1.  0.  0.]
        ///                            [ 0.  0.  1.]
        ///                            [ 1.  0.  0.]]
        /// 
        ///   one_hot([1,0,2,0], 3, on_value=8, off_value=1,
        ///           dtype='int32') = [[1 8 1]
        ///                             [8 1 1]
        ///                             [1 1 8]
        ///                             [8 1 1]]
        /// 
        ///   one_hot([[1,0],[1,0],[2,0]], 3) = [[[ 0.  1.  0.]
        ///                                       [ 1.  0.  0.]]
        /// 
        ///                                      [[ 0.  1.  0.]
        ///                                       [ 1.  0.  0.]]
        /// 
        ///                                      [[ 0.  0.  1.]
        ///                                       [ 1.  0.  0.]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\indexing_op.cc:L799
        /// </summary>
        /// <param name="indices">array of locations where to set on_value</param>
        /// <param name="depth">Depth of the one hot dimension.</param>
        /// <param name="on_value">The value assigned to the locations represented by indices.</param>
        /// <param name="off_value">The value assigned to the locations not represented by indices.</param>
        /// <param name="dtype">DType of the output</param>
        public static NDArray OneHot(NDArrayOrSymbol indices, int depth, double onValue = 1, double offValue = 0, string dtype = "float32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "one_hot",
                _oneHotParamNames,
                new[] { Convert(depth), Convert(onValue), Convert(offValue), Convert(dtype) },
                new[] { indices.Handle },
                output
            );
            return result;
        }

        private static string[] _gatherNdParamNames = Empty;

        /// <summary>
        /// Gather elements or slices from `data` and store to a tensor whose
        /// shape is defined by `indices`.
        /// 
        /// Given `data` with shape `(X_0, X_1, ..., X_{N-1})` and indices with shape
        /// `(M, Y_0, ..., Y_{K-1})`, the output will have shape `(Y_0, ..., Y_{K-1}, X_M, ..., X_{N-1})`,
        /// where `M <= N`. If `M == N`, output shape will simply be `(Y_0, ..., Y_{K-1})`.
        /// 
        /// The elements in output is defined as follows::
        /// 
        ///   output[y_0, ..., y_{K-1}, x_M, ..., x_{N-1}] = data[indices[0, y_0, ..., y_{K-1}],
        ///                                                       ...,
        ///                                                       indices[M-1, y_0, ..., y_{K-1}],
        ///                                                       x_M, ..., x_{N-1}]
        /// 
        /// Examples::
        /// 
        ///   data = [[0, 1], [2, 3]]
        ///   indices = [[1, 1, 0], [0, 1, 0]]
        ///   gather_nd(data, indices) = [2, 3, 0]
        /// 
        ///   data = [[[1, 2], [3, 4]], [[5, 6], [7, 8]]]
        ///   indices = [[0, 1], [1, 0]]
        ///   gather_nd(data, indices) = [[3, 4], [5, 6]]
        /// 
        /// 
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="indices">indices</param>
        public static NDArray GatherNd(NDArrayOrSymbol data, NDArrayOrSymbol indices, NDArray output = null)
        {
            var result = Operator.Invoke(
                "gather_nd",
                _gatherNdParamNames,
                Empty,
                new[] { data.Handle, indices.Handle },
                output
            );
            return result;
        }

        private static string[] _scatterNdParamNames = new[] { "shape" };

        /// <summary>
        /// Scatters data into a new tensor according to indices.
        /// 
        /// Given `data` with shape `(Y_0, ..., Y_{K-1}, X_M, ..., X_{N-1})` and indices with shape
        /// `(M, Y_0, ..., Y_{K-1})`, the output will have shape `(X_0, X_1, ..., X_{N-1})`,
        /// where `M <= N`. If `M == N`, data shape should simply be `(Y_0, ..., Y_{K-1})`.
        /// 
        /// The elements in output is defined as follows::
        /// 
        ///   output[indices[0, y_0, ..., y_{K-1}],
        ///          ...,
        ///          indices[M-1, y_0, ..., y_{K-1}],
        ///          x_M, ..., x_{N-1}] = data[y_0, ..., y_{K-1}, x_M, ..., x_{N-1}]
        /// 
        /// all other entries in output are 0.
        /// 
        /// .. warning::
        /// 
        ///     If the indices have duplicates, the result will be non-deterministic and
        ///     the gradient of `scatter_nd` will not be correct!!
        /// 
        /// 
        /// Examples::
        /// 
        ///   data = [2, 3, 0]
        ///   indices = [[1, 1, 0], [0, 1, 0]]
        ///   shape = (2, 2)
        ///   scatter_nd(data, indices, shape) = [[0, 0], [2, 3]]
        /// 
        ///   data = [[[1, 2], [3, 4]], [[5, 6], [7, 8]]]
        ///   indices = [[0, 1], [1, 1]]
        ///   shape = (2, 2, 2, 2)
        ///   scatter_nd(data, indices, shape) = [[[[0, 0],
        ///                                         [0, 0]],
        /// 
        ///                                        [[1, 2],
        ///                                         [3, 4]]],
        /// 
        ///                                       [[[0, 0],
        ///                                         [0, 0]],
        /// 
        ///                                        [[5, 6],
        ///                                         [7, 8]]]]
        /// 
        /// 
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="indices">indices</param>
        /// <param name="shape">Shape of output.</param>
        public static NDArray ScatterNd(NDArrayOrSymbol data, NDArrayOrSymbol indices, NDShape shape, NDArray output = null)
        {
            var result = Operator.Invoke(
                "scatter_nd",
                _scatterNdParamNames,
                new[] { Convert(shape) },
                new[] { data.Handle, indices.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardGatherNdParamNames = new[] { "shape" };

        /// <summary>
        /// Accumulates data according to indices and get the result. It's the backward of
        /// `gather_nd`.
        /// 
        /// Given `data` with shape `(Y_0, ..., Y_{K-1}, X_M, ..., X_{N-1})` and indices with shape
        /// `(M, Y_0, ..., Y_{K-1})`, the output will have shape `(X_0, X_1, ..., X_{N-1})`,
        /// where `M <= N`. If `M == N`, data shape should simply be `(Y_0, ..., Y_{K-1})`.
        /// 
        /// The elements in output is defined as follows::
        /// 
        ///   output[indices[0, y_0, ..., y_{K-1}],
        ///          ...,
        ///          indices[M-1, y_0, ..., y_{K-1}],
        ///          x_M, ..., x_{N-1}] += data[y_0, ..., y_{K-1}, x_M, ..., x_{N-1}]
        /// 
        /// all other entries in output are 0 or the original value if AddTo is triggered.
        /// 
        /// Examples::
        /// 
        ///   data = [2, 3, 0]
        ///   indices = [[1, 1, 0], [0, 1, 0]]
        ///   shape = (2, 2)
        ///   _backward_gather_nd(data, indices, shape) = [[0, 0], [2, 3]] # Same as scatter_nd
        /// 
        ///   # The difference between scatter_nd and scatter_nd_acc is the latter will accumulate
        ///   #  the values that point to the same index.
        /// 
        ///   data = [2, 3, 0]
        ///   indices = [[1, 1, 0], [1, 1, 0]]
        ///   shape = (2, 2)
        ///   _backward_gather_nd(data, indices, shape) = [[0, 0], [0, 5]]
        /// 
        /// 
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="indices">indices</param>
        /// <param name="shape">Shape of output.</param>
        public static NDArray BackwardGatherNd(NDArrayOrSymbol data, NDArrayOrSymbol indices, NDShape shape, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_gather_nd",
                _backwardGatherNdParamNames,
                new[] { Convert(shape) },
                new[] { data.Handle, indices.Handle },
                output
            );
            return result;
        }

        private static string[] _scatterSetNdParamNames = new[] { "shape" };

        /// <summary>
        /// This operator has the same functionality as scatter_nd
        /// except that it does not reset the elements not indexed by the input
        /// index `NDArray` in the input data `NDArray`. output should be explicitly
        /// given and be the same as lhs.
        /// 
        /// .. note:: This operator is for internal use only.
        /// 
        /// Examples::
        /// 
        ///   data = [2, 3, 0]
        ///   indices = [[1, 1, 0], [0, 1, 0]]
        ///   out = [[1, 1], [1, 1]]
        ///   _scatter_set_nd(lhs=out, rhs=data, indices=indices, out=out)
        ///   out = [[0, 1], [2, 3]]
        /// 
        /// 
        /// </summary>
        /// <param name="lhs">source input</param>
        /// <param name="rhs">value to assign</param>
        /// <param name="indices">indices</param>
        /// <param name="shape">Shape of output.</param>
        public static NDArray ScatterSetNd(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDArrayOrSymbol indices, NDShape shape, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_scatter_set_nd",
                _scatterSetNdParamNames,
                new[] { Convert(shape) },
                new[] { lhs.Handle, rhs.Handle, indices.Handle },
                output
            );
            return result;
        }

        private static string[] _zerosWithoutDtypeParamNames = new[] { "shape", "ctx", "dtype" };

        /// <summary>
        /// fill target with zeros without default dtype
        /// </summary>
        /// <param name="shape">The shape of the output</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
        /// <param name="dtype">Target data type.</param>
        public static NDArray ZerosWithoutDtype(NDShape shape = null, string ctx = "", int dtype = -1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_zeros_without_dtype",
                _zerosWithoutDtypeParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _zerosParamNames = new[] { "shape", "ctx", "dtype" };

        /// <summary>
        /// fill target with zeros
        /// </summary>
        /// <param name="shape">The shape of the output</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
        /// <param name="dtype">Target data type.</param>
        public static NDArray Zeros(NDShape shape = null, string ctx = "", string dtype = "float32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "_zeros",
                _zerosParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _eyeParamNames = new[] { "N", "M", "k", "ctx", "dtype" };

        /// <summary>
        /// Return a 2-D array with ones on the diagonal and zeros elsewhere.
        /// </summary>
        /// <param name="N">Number of rows in the output.</param>
        /// <param name="M">Number of columns in the output. If 0, defaults to N</param>
        /// <param name="k">Index of the diagonal. 0 (the default) refers to the main diagonal.A positive value refers to an upper diagonal.A negative value to a lower diagonal.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
        /// <param name="dtype">Target data type.</param>
        public static NDArray Eye(double? N, double? M = 0, double? k = 0, string ctx = "", string dtype = "float32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "_eye",
                _eyeParamNames,
                new[] { Convert(N), Convert(M), Convert(k), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _onesParamNames = new[] { "shape", "ctx", "dtype" };

        /// <summary>
        /// fill target with ones
        /// </summary>
        /// <param name="shape">The shape of the output</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
        /// <param name="dtype">Target data type.</param>
        public static NDArray Ones(NDShape shape = null, string ctx = "", string dtype = "float32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "_ones",
                _onesParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _fullParamNames = new[] { "shape", "ctx", "dtype", "value" };

        /// <summary>
        /// fill target with a scalar value
        /// </summary>
        /// <param name="shape">The shape of the output</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
        /// <param name="dtype">Target data type.</param>
        /// <param name="value">Value with which to fill newly created tensor</param>
        public static NDArray Full(double value, NDShape shape = null, string ctx = "", string dtype = "float32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "_full",
                _fullParamNames,
                new[] { Convert(shape), Convert(ctx), Convert(dtype), Convert(value) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _arangeParamNames = new[] { "start", "stop", "step", "repeat", "infer_range", "ctx", "dtype" };

        /// <summary>
        /// Return evenly spaced values within a given interval. Similar to Numpy
        /// </summary>
        /// <param name="start">Start of interval. The interval includes this value. The default start value is 0.</param>
        /// <param name="stop">End of interval. The interval does not include this value, except in some cases where step is not an integer and floating point round-off affects the length of out.</param>
        /// <param name="step">Spacing between values.</param>
        /// <param name="repeat">The repeating time of all elements. E.g repeat=3, the element a will be repeated three times --> a, a, a.</param>
        /// <param name="infer_range">When set to True, infer the stop position from the start, step, repeat, and output tensor size.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
        /// <param name="dtype">Target data type.</param>
        public static NDArray Arange(double start, double? stop = null, double step = 1, int repeat = 1, bool inferRange = false, string ctx = "", string dtype = "float32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "_arange",
                _arangeParamNames,
                new[] { Convert(start), Convert(stop), Convert(step), Convert(repeat), Convert(inferRange), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linspaceParamNames = new[] { "start", "stop", "step", "repeat", "infer_range", "ctx", "dtype" };

        /// <summary>
        /// Return evenly spaced numbers over a specified interval. Similar to Numpy
        /// </summary>
        /// <param name="start">Start of interval. The interval includes this value. The default start value is 0.</param>
        /// <param name="stop">End of interval. The interval does not include this value, except in some cases where step is not an integer and floating point round-off affects the length of out.</param>
        /// <param name="step">Spacing between values.</param>
        /// <param name="repeat">The repeating time of all elements. E.g repeat=3, the element a will be repeated three times --> a, a, a.</param>
        /// <param name="infer_range">When set to True, infer the stop position from the start, step, repeat, and output tensor size.</param>
        /// <param name="ctx">Context of output, in format [cpu|gpu|cpu_pinned](n).Only used for imperative calls.</param>
        /// <param name="dtype">Target data type.</param>
        public static NDArray Linspace(double start, double? stop = null, double step = 1, int repeat = 1, bool inferRange = false, string ctx = "", string dtype = "float32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linspace",
                _linspaceParamNames,
                new[] { Convert(start), Convert(stop), Convert(step), Convert(repeat), Convert(inferRange), Convert(ctx), Convert(dtype) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _zerosLikeParamNames = Empty;

        /// <summary>
        /// Return an array of zeros with the same shape, type and storage type
        /// as the input array.
        /// 
        /// The storage type of ``zeros_like`` output depends on the storage type of the input
        /// 
        /// - zeros_like(row_sparse) = row_sparse
        /// - zeros_like(csr) = csr
        /// - zeros_like(default) = default
        /// 
        /// Examples::
        /// 
        ///   x = [[ 1.,  1.,  1.],
        ///        [ 1.,  1.,  1.]]
        /// 
        ///   zeros_like(x) = [[ 0.,  0.,  0.],
        ///                    [ 0.,  0.,  0.]]
        /// 
        /// 
        /// </summary>
        /// <param name="data">The input</param>
        public static NDArray ZerosLike(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "zeros_like",
                _zerosLikeParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _onesLikeParamNames = Empty;

        /// <summary>
        /// Return an array of ones with the same shape and type
        /// as the input array.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 0.,  0.,  0.],
        ///        [ 0.,  0.,  0.]]
        /// 
        ///   ones_like(x) = [[ 1.,  1.,  1.],
        ///                   [ 1.,  1.,  1.]]
        /// 
        /// 
        /// </summary>
        /// <param name="data">The input</param>
        public static NDArray OnesLike(NDArrayOrSymbol data, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ones_like",
                _onesLikeParamNames,
                Empty,
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _linalgGemmParamNames = new[] { "transpose_a", "transpose_b", "alpha", "beta", "axis" };

        /// <summary>
        /// Performs general matrix multiplication and accumulation.
        /// Input are tensors *A*, *B*, *C*, each of dimension *n >= 2* and having the same shape
        /// on the leading *n-2* dimensions.
        /// 
        /// If *n=2*, the BLAS3 function *gemm* is performed:
        /// 
        ///    *out* = *alpha* \* *op*\ (*A*) \* *op*\ (*B*) + *beta* \* *C*
        /// 
        /// Here, *alpha* and *beta* are scalar parameters, and *op()* is either the identity or
        /// matrix transposition (depending on *transpose_a*, *transpose_b*).
        /// 
        /// If *n>2*, *gemm* is performed separately for a batch of matrices. The column indices of the matrices
        /// are given by the last dimensions of the tensors, the row indices by the axis specified with the *axis*
        /// parameter. By default, the trailing two dimensions will be used for matrix encoding.
        /// 
        /// For a non-default axis parameter, the operation performed is equivalent to a series of swapaxes/gemm/swapaxes
        /// calls. For example let *A*, *B*, *C* be 5 dimensional tensors. Then gemm(*A*, *B*, *C*, axis=1) is equivalent
        /// to the following without the overhead of the additional swapaxis operations::
        /// 
        ///     A1 = swapaxes(A, dim1=1, dim2=3)
        ///     B1 = swapaxes(B, dim1=1, dim2=3)
        ///     C = swapaxes(C, dim1=1, dim2=3)
        ///     C = gemm(A1, B1, C)
        ///     C = swapaxis(C, dim1=1, dim2=3)
        /// 
        /// When the input data is of type float32 and the environment variables MXNET_CUDA_ALLOW_TENSOR_CORE
        /// and MXNET_CUDA_TENSOR_OP_MATH_ALLOW_CONVERSION are set to 1, this operator will try to use
        /// pseudo-float16 precision (float32 math with float16 I/O) precision in order to use
        /// Tensor Cores on suitable NVIDIA GPUs. This can sometimes give significant speedups.
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///    // Single matrix multiply-add
        ///    A = [[1.0, 1.0], [1.0, 1.0]]
        ///    B = [[1.0, 1.0], [1.0, 1.0], [1.0, 1.0]]
        ///    C = [[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]
        ///    gemm(A, B, C, transpose_b=True, alpha=2.0, beta=10.0)
        ///            = [[14.0, 14.0, 14.0], [14.0, 14.0, 14.0]]
        /// 
        ///    // Batch matrix multiply-add
        ///    A = [[[1.0, 1.0]], [[0.1, 0.1]]]
        ///    B = [[[1.0, 1.0]], [[0.1, 0.1]]]
        ///    C = [[[10.0]], [[0.01]]]
        ///    gemm(A, B, C, transpose_b=True, alpha=2.0 , beta=10.0)
        ///            = [[[104.0]], [[0.14]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L89
        /// </summary>
        /// <param name="A">Tensor of input matrices</param>
        /// <param name="B">Tensor of input matrices</param>
        /// <param name="C">Tensor of input matrices</param>
        /// <param name="transpose_a">Multiply with transposed of first input (A).</param>
        /// <param name="transpose_b">Multiply with transposed of second input (B).</param>
        /// <param name="alpha">Scalar factor multiplied with A*B.</param>
        /// <param name="beta">Scalar factor multiplied with C.</param>
        /// <param name="axis">Axis corresponding to the matrix rows.</param>
        public static NDArray LinalgGemm(NDArrayOrSymbol A, NDArrayOrSymbol B, NDArrayOrSymbol C, bool transposeA = false, bool transposeB = false, double alpha = 1, double beta = 1, int axis = -2, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_gemm",
                _linalgGemmParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert(alpha), Convert(beta), Convert(axis) },
                new[] { A.Handle, B.Handle, C.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgGemmParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgGemm(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_gemm",
                _backwardLinalgGemmParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgGemm2ParamNames = new[] { "transpose_a", "transpose_b", "alpha", "axis" };

        /// <summary>
        /// Performs general matrix multiplication.
        /// Input are tensors *A*, *B*, each of dimension *n >= 2* and having the same shape
        /// on the leading *n-2* dimensions.
        /// 
        /// If *n=2*, the BLAS3 function *gemm* is performed:
        /// 
        ///    *out* = *alpha* \* *op*\ (*A*) \* *op*\ (*B*)
        /// 
        /// Here *alpha* is a scalar parameter and *op()* is either the identity or the matrix
        /// transposition (depending on *transpose_a*, *transpose_b*).
        /// 
        /// If *n>2*, *gemm* is performed separately for a batch of matrices. The column indices of the matrices
        /// are given by the last dimensions of the tensors, the row indices by the axis specified with the *axis*
        /// parameter. By default, the trailing two dimensions will be used for matrix encoding.
        /// 
        /// For a non-default axis parameter, the operation performed is equivalent to a series of swapaxes/gemm/swapaxes
        /// calls. For example let *A*, *B* be 5 dimensional tensors. Then gemm(*A*, *B*, axis=1) is equivalent to
        /// the following without the overhead of the additional swapaxis operations::
        /// 
        ///     A1 = swapaxes(A, dim1=1, dim2=3)
        ///     B1 = swapaxes(B, dim1=1, dim2=3)
        ///     C = gemm2(A1, B1)
        ///     C = swapaxis(C, dim1=1, dim2=3)
        /// 
        /// When the input data is of type float32 and the environment variables MXNET_CUDA_ALLOW_TENSOR_CORE
        /// and MXNET_CUDA_TENSOR_OP_MATH_ALLOW_CONVERSION are set to 1, this operator will try to use
        /// pseudo-float16 precision (float32 math with float16 I/O) precision in order to use
        /// Tensor Cores on suitable NVIDIA GPUs. This can sometimes give significant speedups.
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///    // Single matrix multiply
        ///    A = [[1.0, 1.0], [1.0, 1.0]]
        ///    B = [[1.0, 1.0], [1.0, 1.0], [1.0, 1.0]]
        ///    gemm2(A, B, transpose_b=True, alpha=2.0)
        ///             = [[4.0, 4.0, 4.0], [4.0, 4.0, 4.0]]
        /// 
        ///    // Batch matrix multiply
        ///    A = [[[1.0, 1.0]], [[0.1, 0.1]]]
        ///    B = [[[1.0, 1.0]], [[0.1, 0.1]]]
        ///    gemm2(A, B, transpose_b=True, alpha=2.0)
        ///            = [[[4.0]], [[0.04 ]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L163
        /// </summary>
        /// <param name="A">Tensor of input matrices</param>
        /// <param name="B">Tensor of input matrices</param>
        /// <param name="transpose_a">Multiply with transposed of first input (A).</param>
        /// <param name="transpose_b">Multiply with transposed of second input (B).</param>
        /// <param name="alpha">Scalar factor multiplied with A*B.</param>
        /// <param name="axis">Axis corresponding to the matrix row indices.</param>
        public static NDArray LinalgGemm2(NDArrayOrSymbol A, NDArrayOrSymbol B, bool transposeA = false, bool transposeB = false, double alpha = 1, int axis = -2, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_gemm2",
                _linalgGemm2ParamNames,
                new[] { Convert(transposeA), Convert(transposeB), Convert(alpha), Convert(axis) },
                new[] { A.Handle, B.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgGemm2ParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgGemm2(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_gemm2",
                _backwardLinalgGemm2ParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgPotrfParamNames = Empty;

        /// <summary>
        /// Performs Cholesky factorization of a symmetric positive-definite matrix.
        /// Input is a tensor *A* of dimension *n >= 2*.
        /// 
        /// If *n=2*, the Cholesky factor *B* of the symmetric, positive definite matrix *A* is
        /// computed. *B* is triangular (entries of upper or lower triangle are all zero), has
        /// positive diagonal entries, and:
        /// 
        ///   *A* = *B* \* *B*\ :sup:`T`  if *lower* = *true*
        ///   *A* = *B*\ :sup:`T` \* *B*  if *lower* = *false*
        /// 
        /// If *n>2*, *potrf* is performed separately on the trailing two dimensions for all inputs
        /// (batch mode).
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///    // Single matrix factorization
        ///    A = [[4.0, 1.0], [1.0, 4.25]]
        ///    potrf(A) = [[2.0, 0], [0.5, 2.0]]
        /// 
        ///    // Batch matrix factorization
        ///    A = [[[4.0, 1.0], [1.0, 4.25]], [[16.0, 4.0], [4.0, 17.0]]]
        ///    potrf(A) = [[[2.0, 0], [0.5, 2.0]], [[4.0, 0], [1.0, 4.0]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L214
        /// </summary>
        /// <param name="A">Tensor of input matrices to be decomposed</param>
        public static NDArray LinalgPotrf(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_potrf",
                _linalgPotrfParamNames,
                Empty,
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgPotrfParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgPotrf(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_potrf",
                _backwardLinalgPotrfParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgPotriParamNames = Empty;

        /// <summary>
        /// Performs matrix inversion from a Cholesky factorization.
        /// Input is a tensor *A* of dimension *n >= 2*.
        /// 
        /// If *n=2*, *A* is a triangular matrix (entries of upper or lower triangle are all zero)
        /// with positive diagonal. We compute:
        /// 
        ///   *out* = *A*\ :sup:`-T` \* *A*\ :sup:`-1` if *lower* = *true*
        ///   *out* = *A*\ :sup:`-1` \* *A*\ :sup:`-T` if *lower* = *false*
        /// 
        /// In other words, if *A* is the Cholesky factor of a symmetric positive definite matrix
        /// *B* (obtained by *potrf*), then
        /// 
        ///   *out* = *B*\ :sup:`-1`
        /// 
        /// If *n>2*, *potri* is performed separately on the trailing two dimensions for all inputs
        /// (batch mode).
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// .. note:: Use this operator only if you are certain you need the inverse of *B*, and
        ///           cannot use the Cholesky factor *A* (*potrf*), together with backsubstitution
        ///           (*trsm*). The latter is numerically much safer, and also cheaper.
        /// 
        /// Examples::
        /// 
        ///    // Single matrix inverse
        ///    A = [[2.0, 0], [0.5, 2.0]]
        ///    potri(A) = [[0.26563, -0.0625], [-0.0625, 0.25]]
        /// 
        ///    // Batch matrix inverse
        ///    A = [[[2.0, 0], [0.5, 2.0]], [[4.0, 0], [1.0, 4.0]]]
        ///    potri(A) = [[[0.26563, -0.0625], [-0.0625, 0.25]],
        ///                [[0.06641, -0.01562], [-0.01562, 0,0625]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L275
        /// </summary>
        /// <param name="A">Tensor of lower triangular matrices</param>
        public static NDArray LinalgPotri(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_potri",
                _linalgPotriParamNames,
                Empty,
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgPotriParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgPotri(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_potri",
                _backwardLinalgPotriParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgTrmmParamNames = new[] { "transpose", "rightside", "lower", "alpha" };

        /// <summary>
        /// Performs multiplication with a lower triangular matrix.
        /// Input are tensors *A*, *B*, each of dimension *n >= 2* and having the same shape
        /// on the leading *n-2* dimensions.
        /// 
        /// If *n=2*, *A* must be triangular. The operator performs the BLAS3 function
        /// *trmm*:
        /// 
        ///    *out* = *alpha* \* *op*\ (*A*) \* *B*
        /// 
        /// if *rightside=False*, or
        /// 
        ///    *out* = *alpha* \* *B* \* *op*\ (*A*)
        /// 
        /// if *rightside=True*. Here, *alpha* is a scalar parameter, and *op()* is either the
        /// identity or the matrix transposition (depending on *transpose*).
        /// 
        /// If *n>2*, *trmm* is performed separately on the trailing two dimensions for all inputs
        /// (batch mode).
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///    // Single triangular matrix multiply
        ///    A = [[1.0, 0], [1.0, 1.0]]
        ///    B = [[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]
        ///    trmm(A, B, alpha=2.0) = [[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]]
        /// 
        ///    // Batch triangular matrix multiply
        ///    A = [[[1.0, 0], [1.0, 1.0]], [[1.0, 0], [1.0, 1.0]]]
        ///    B = [[[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]], [[0.5, 0.5, 0.5], [0.5, 0.5, 0.5]]]
        ///    trmm(A, B, alpha=2.0) = [[[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]],
        ///                             [[1.0, 1.0, 1.0], [2.0, 2.0, 2.0]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L333
        /// </summary>
        /// <param name="A">Tensor of lower triangular matrices</param>
        /// <param name="B">Tensor of matrices</param>
        /// <param name="transpose">Use transposed of the triangular matrix</param>
        /// <param name="rightside">Multiply triangular matrix from the right to non-triangular one.</param>
        /// <param name="lower">True if the triangular matrix is lower triangular, false if it is upper triangular.</param>
        /// <param name="alpha">Scalar factor to be applied to the result.</param>
        public static NDArray LinalgTrmm(NDArrayOrSymbol A, NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_trmm",
                _linalgTrmmParamNames,
                new[] { Convert(transpose), Convert(rightside), Convert(lower), Convert(alpha) },
                new[] { A.Handle, B.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgTrmmParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgTrmm(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_trmm",
                _backwardLinalgTrmmParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgTrsmParamNames = new[] { "transpose", "rightside", "lower", "alpha" };

        /// <summary>
        /// Solves matrix equation involving a lower triangular matrix.
        /// Input are tensors *A*, *B*, each of dimension *n >= 2* and having the same shape
        /// on the leading *n-2* dimensions.
        /// 
        /// If *n=2*, *A* must be triangular. The operator performs the BLAS3 function
        /// *trsm*, solving for *out* in:
        /// 
        ///    *op*\ (*A*) \* *out* = *alpha* \* *B*
        /// 
        /// if *rightside=False*, or
        /// 
        ///    *out* \* *op*\ (*A*) = *alpha* \* *B*
        /// 
        /// if *rightside=True*. Here, *alpha* is a scalar parameter, and *op()* is either the
        /// identity or the matrix transposition (depending on *transpose*).
        /// 
        /// If *n>2*, *trsm* is performed separately on the trailing two dimensions for all inputs
        /// (batch mode).
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///    // Single matrix solve
        ///    A = [[1.0, 0], [1.0, 1.0]]
        ///    B = [[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]]
        ///    trsm(A, B, alpha=0.5) = [[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]
        /// 
        ///    // Batch matrix solve
        ///    A = [[[1.0, 0], [1.0, 1.0]], [[1.0, 0], [1.0, 1.0]]]
        ///    B = [[[2.0, 2.0, 2.0], [4.0, 4.0, 4.0]],
        ///         [[4.0, 4.0, 4.0], [8.0, 8.0, 8.0]]]
        ///    trsm(A, B, alpha=0.5) = [[[1.0, 1.0, 1.0], [1.0, 1.0, 1.0]],
        ///                             [[2.0, 2.0, 2.0], [2.0, 2.0, 2.0]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L396
        /// </summary>
        /// <param name="A">Tensor of lower triangular matrices</param>
        /// <param name="B">Tensor of matrices</param>
        /// <param name="transpose">Use transposed of the triangular matrix</param>
        /// <param name="rightside">Multiply triangular matrix from the right to non-triangular one.</param>
        /// <param name="lower">True if the triangular matrix is lower triangular, false if it is upper triangular.</param>
        /// <param name="alpha">Scalar factor to be applied to the result.</param>
        public static NDArray LinalgTrsm(NDArrayOrSymbol A, NDArrayOrSymbol B, bool transpose = false, bool rightside = false, bool lower = true, double alpha = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_trsm",
                _linalgTrsmParamNames,
                new[] { Convert(transpose), Convert(rightside), Convert(lower), Convert(alpha) },
                new[] { A.Handle, B.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgTrsmParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgTrsm(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_trsm",
                _backwardLinalgTrsmParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgSumlogdiagParamNames = Empty;

        /// <summary>
        /// Computes the sum of the logarithms of the diagonal elements of a square matrix.
        /// Input is a tensor *A* of dimension *n >= 2*.
        /// 
        /// If *n=2*, *A* must be square with positive diagonal entries. We sum the natural
        /// logarithms of the diagonal elements, the result has shape (1,).
        /// 
        /// If *n>2*, *sumlogdiag* is performed separately on the trailing two dimensions for all
        /// inputs (batch mode).
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///    // Single matrix reduction
        ///    A = [[1.0, 1.0], [1.0, 7.0]]
        ///    sumlogdiag(A) = [1.9459]
        /// 
        ///    // Batch matrix reduction
        ///    A = [[[1.0, 1.0], [1.0, 7.0]], [[3.0, 0], [0, 17.0]]]
        ///    sumlogdiag(A) = [1.9459, 3.9318]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L445
        /// </summary>
        /// <param name="A">Tensor of square matrices</param>
        public static NDArray LinalgSumlogdiag(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_sumlogdiag",
                _linalgSumlogdiagParamNames,
                Empty,
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgSumlogdiagParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgSumlogdiag(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_sumlogdiag",
                _backwardLinalgSumlogdiagParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgExtractdiagParamNames = new[] { "offset" };

        /// <summary>
        /// Extracts the diagonal entries of a square matrix.
        /// Input is a tensor *A* of dimension *n >= 2*.
        /// 
        /// If *n=2*, then *A* represents a single square matrix which diagonal elements get extracted as a 1-dimensional tensor.
        /// 
        /// If *n>2*, then *A* represents a batch of square matrices on the trailing two dimensions. The extracted diagonals are returned as an *n-1*-dimensional tensor.
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///     // Single matrix diagonal extraction
        ///     A = [[1.0, 2.0],
        ///          [3.0, 4.0]]
        /// 
        ///     extractdiag(A) = [1.0, 4.0]
        /// 
        ///     extractdiag(A, 1) = [2.0]
        /// 
        ///     // Batch matrix diagonal extraction
        ///     A = [[[1.0, 2.0],
        ///           [3.0, 4.0]],
        ///          [[5.0, 6.0],
        ///           [7.0, 8.0]]]
        /// 
        ///     extractdiag(A) = [[1.0, 4.0],
        ///                       [5.0, 8.0]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L495
        /// </summary>
        /// <param name="A">Tensor of square matrices</param>
        /// <param name="offset">Offset of the diagonal versus the main diagonal. 0 corresponds to the main diagonal, a negative/positive value to diagonals below/above the main diagonal.</param>
        public static NDArray LinalgExtractdiag(NDArrayOrSymbol A, int offset = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_extractdiag",
                _linalgExtractdiagParamNames,
                new[] { Convert(offset) },
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgExtractdiagParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgExtractdiag(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_extractdiag",
                _backwardLinalgExtractdiagParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgMakediagParamNames = new[] { "offset" };

        /// <summary>
        /// Constructs a square matrix with the input as diagonal.
        /// Input is a tensor *A* of dimension *n >= 1*.
        /// 
        /// If *n=1*, then *A* represents the diagonal entries of a single square matrix. This matrix will be returned as a 2-dimensional tensor.
        /// If *n>1*, then *A* represents a batch of diagonals of square matrices. The batch of diagonal matrices will be returned as an *n+1*-dimensional tensor.
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///     // Single diagonal matrix construction
        ///     A = [1.0, 2.0]
        /// 
        ///     makediag(A)    = [[1.0, 0.0],
        ///                       [0.0, 2.0]]
        /// 
        ///     makediag(A, 1) = [[0.0, 1.0, 0.0],
        ///                       [0.0, 0.0, 2.0],
        ///                       [0.0, 0.0, 0.0]]
        /// 
        ///     // Batch diagonal matrix construction
        ///     A = [[1.0, 2.0],
        ///          [3.0, 4.0]]
        /// 
        ///     makediag(A) = [[[1.0, 0.0],
        ///                     [0.0, 2.0]],
        ///                    [[3.0, 0.0],
        ///                     [0.0, 4.0]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L547
        /// </summary>
        /// <param name="A">Tensor of diagonal entries</param>
        /// <param name="offset">Offset of the diagonal versus the main diagonal. 0 corresponds to the main diagonal, a negative/positive value to diagonals below/above the main diagonal.</param>
        public static NDArray LinalgMakediag(NDArrayOrSymbol A, int offset = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_makediag",
                _linalgMakediagParamNames,
                new[] { Convert(offset) },
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgMakediagParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgMakediag(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_makediag",
                _backwardLinalgMakediagParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgExtracttrianParamNames = new[] { "offset", "lower" };

        /// <summary>
        /// Extracts a triangular sub-matrix from a square matrix.
        /// Input is a tensor *A* of dimension *n >= 2*.
        /// 
        /// If *n=2*, then *A* represents a single square matrix from which a triangular sub-matrix is extracted as a 1-dimensional tensor.
        /// 
        /// If *n>2*, then *A* represents a batch of square matrices on the trailing two dimensions. The extracted triangular sub-matrices are returned as an *n-1*-dimensional tensor.
        /// 
        /// The *offset* and *lower* parameters determine the triangle to be extracted:
        /// 
        /// - When *offset = 0* either the lower or upper triangle with respect to the main diagonal is extracted depending on the value of parameter *lower*.
        /// - When *offset = k > 0* the upper triangle with respect to the k-th diagonal above the main diagonal is extracted. 
        /// - When *offset = k < 0* the lower triangle with respect to the k-th diagonal below the main diagonal is extracted. 
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///     // Single triagonal extraction
        ///     A = [[1.0, 2.0],
        ///          [3.0, 4.0]]
        /// 
        ///     extracttrian(A) = [1.0, 3.0, 4.0]
        ///     extracttrian(A, lower=False) = [1.0, 2.0, 4.0]
        ///     extracttrian(A, 1) = [2.0]
        ///     extracttrian(A, -1) = [3.0]
        /// 
        ///     // Batch triagonal extraction
        ///     A = [[[1.0, 2.0],
        ///           [3.0, 4.0]],
        ///          [[5.0, 6.0],
        ///           [7.0, 8.0]]]
        /// 
        ///     extracttrian(A) = [[1.0, 3.0, 4.0],
        ///                        [5.0, 7.0, 8.0]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L605
        /// </summary>
        /// <param name="A">Tensor of square matrices</param>
        /// <param name="offset">Offset of the diagonal versus the main diagonal. 0 corresponds to the main diagonal, a negative/positive value to diagonals below/above the main diagonal.</param>
        /// <param name="lower">Refer to the lower triangular matrix if lower=true, refer to the upper otherwise. Only relevant when offset=0</param>
        public static NDArray LinalgExtracttrian(NDArrayOrSymbol A, int offset = 0, bool lower = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_extracttrian",
                _linalgExtracttrianParamNames,
                new[] { Convert(offset), Convert(lower) },
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgExtracttrianParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgExtracttrian(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_extracttrian",
                _backwardLinalgExtracttrianParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgMaketrianParamNames = new[] { "offset", "lower" };

        /// <summary>
        /// Constructs a square matrix with the input representing a specific triangular sub-matrix.
        /// This is basically the inverse of *linalg.extracttrian*. Input is a tensor *A* of dimension *n >= 1*.
        /// 
        /// If *n=1*, then *A* represents the entries of a triangular matrix which is lower triangular if *offset<0* or *offset=0*, *lower=true*. The resulting matrix is derived by first constructing the square
        /// matrix with the entries outside the triangle set to zero and then adding *offset*-times an additional 
        /// diagonal with zero entries to the square matrix. 
        /// 
        /// If *n>1*, then *A* represents a batch of triangular sub-matrices. The batch of corresponding square matrices is returned as an *n+1*-dimensional tensor.
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///     // Single  matrix construction
        ///     A = [1.0, 2.0, 3.0]
        /// 
        ///     maketrian(A)              = [[1.0, 0.0],
        ///                                  [2.0, 3.0]]
        /// 
        ///     maketrian(A, lower=false) = [[1.0, 2.0],
        ///                                  [0.0, 3.0]]
        /// 
        ///     maketrian(A, offset=1)    = [[0.0, 1.0, 2.0],
        ///                                  [0.0, 0.0, 3.0],
        ///                                  [0.0, 0.0, 0.0]]
        ///     maketrian(A, offset=-1)   = [[0.0, 0.0, 0.0],
        ///                                  [1.0, 0.0, 0.0],
        ///                                  [2.0, 3.0, 0.0]]
        /// 
        ///     // Batch matrix construction
        ///     A = [[1.0, 2.0, 3.0],
        ///          [4.0, 5.0, 6.0]]
        /// 
        ///     maketrian(A)           = [[[1.0, 0.0],
        ///                                [2.0, 3.0]],
        ///                               [[4.0, 0.0],
        ///                                [5.0, 6.0]]]
        /// 
        ///     maketrian(A, offset=1) = [[[0.0, 1.0, 2.0],
        ///                                [0.0, 0.0, 3.0],
        ///                                [0.0, 0.0, 0.0]],
        ///                               [[0.0, 4.0, 5.0],
        ///                                [0.0, 0.0, 6.0],
        ///                                [0.0, 0.0, 0.0]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L673
        /// </summary>
        /// <param name="A">Tensor of triangular matrices stored as vectors</param>
        /// <param name="offset">Offset of the diagonal versus the main diagonal. 0 corresponds to the main diagonal, a negative/positive value to diagonals below/above the main diagonal.</param>
        /// <param name="lower">Refer to the lower triangular matrix if lower=true, refer to the upper otherwise. Only relevant when offset=0</param>
        public static NDArray LinalgMaketrian(NDArrayOrSymbol A, int offset = 0, bool lower = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_maketrian",
                _linalgMaketrianParamNames,
                new[] { Convert(offset), Convert(lower) },
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgMaketrianParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgMaketrian(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_maketrian",
                _backwardLinalgMaketrianParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgSyrkParamNames = new[] { "transpose", "alpha" };

        /// <summary>
        /// Multiplication of matrix with its transpose.
        /// Input is a tensor *A* of dimension *n >= 2*.
        /// 
        /// If *n=2*, the operator performs the BLAS3 function *syrk*:
        /// 
        ///   *out* = *alpha* \* *A* \* *A*\ :sup:`T`
        /// 
        /// if *transpose=False*, or
        /// 
        ///   *out* = *alpha* \* *A*\ :sup:`T` \ \* *A*
        /// 
        /// if *transpose=True*.
        /// 
        /// If *n>2*, *syrk* is performed separately on the trailing two dimensions for all
        /// inputs (batch mode).
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///    // Single matrix multiply
        ///    A = [[1., 2., 3.], [4., 5., 6.]]
        ///    syrk(A, alpha=1., transpose=False)
        ///             = [[14., 32.],
        ///                [32., 77.]]
        ///    syrk(A, alpha=1., transpose=True)
        ///             = [[17., 22., 27.],
        ///                [22., 29., 36.],
        ///                [27., 36., 45.]]
        /// 
        ///    // Batch matrix multiply
        ///    A = [[[1., 1.]], [[0.1, 0.1]]]
        ///    syrk(A, alpha=2., transpose=False) = [[[4.]], [[0.04]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L730
        /// </summary>
        /// <param name="A">Tensor of input matrices</param>
        /// <param name="transpose">Use transpose of input matrix.</param>
        /// <param name="alpha">Scalar factor to be applied to the result.</param>
        public static NDArray LinalgSyrk(NDArrayOrSymbol A, bool transpose = false, double alpha = 1, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_syrk",
                _linalgSyrkParamNames,
                new[] { Convert(transpose), Convert(alpha) },
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgSyrkParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgSyrk(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_syrk",
                _backwardLinalgSyrkParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgGelqfParamNames = Empty;

        /// <summary>
        /// LQ factorization for general matrix.
        /// Input is a tensor *A* of dimension *n >= 2*.
        /// 
        /// If *n=2*, we compute the LQ factorization (LAPACK *gelqf*, followed by *orglq*). *A*
        /// must have shape *(x, y)* with *x <= y*, and must have full rank *=x*. The LQ
        /// factorization consists of *L* with shape *(x, x)* and *Q* with shape *(x, y)*, so
        /// that:
        /// 
        ///    *A* = *L* \* *Q*
        /// 
        /// Here, *L* is lower triangular (upper triangle equal to zero) with nonzero diagonal,
        /// and *Q* is row-orthonormal, meaning that
        /// 
        ///    *Q* \* *Q*\ :sup:`T`
        /// 
        /// is equal to the identity matrix of shape *(x, x)*.
        /// 
        /// If *n>2*, *gelqf* is performed separately on the trailing two dimensions for all
        /// inputs (batch mode).
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///    // Single LQ factorization
        ///    A = [[1., 2., 3.], [4., 5., 6.]]
        ///    Q, L = gelqf(A)
        ///    Q = [[-0.26726124, -0.53452248, -0.80178373],
        ///         [0.87287156, 0.21821789, -0.43643578]]
        ///    L = [[-3.74165739, 0.],
        ///         [-8.55235974, 1.96396101]]
        /// 
        ///    // Batch LQ factorization
        ///    A = [[[1., 2., 3.], [4., 5., 6.]],
        ///         [[7., 8., 9.], [10., 11., 12.]]]
        ///    Q, L = gelqf(A)
        ///    Q = [[[-0.26726124, -0.53452248, -0.80178373],
        ///          [0.87287156, 0.21821789, -0.43643578]],
        ///         [[-0.50257071, -0.57436653, -0.64616234],
        ///          [0.7620735, 0.05862104, -0.64483142]]]
        ///    L = [[[-3.74165739, 0.],
        ///          [-8.55235974, 1.96396101]],
        ///         [[-13.92838828, 0.],
        ///          [-19.09768702, 0.52758934]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L798
        /// </summary>
        /// <param name="A">Tensor of input matrices to be factorized</param>
        public static NDArray LinalgGelqf(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_gelqf",
                _linalgGelqfParamNames,
                Empty,
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgGelqfParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgGelqf(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_gelqf",
                _backwardLinalgGelqfParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgSyevdParamNames = Empty;

        /// <summary>
        /// Eigendecomposition for symmetric matrix.
        /// Input is a tensor *A* of dimension *n >= 2*.
        /// 
        /// If *n=2*, *A* must be symmetric, of shape *(x, x)*. We compute the eigendecomposition,
        /// resulting in the orthonormal matrix *U* of eigenvectors, shape *(x, x)*, and the
        /// vector *L* of eigenvalues, shape *(x,)*, so that:
        /// 
        ///    *U* \* *A* = *diag(L)* \* *U*
        /// 
        /// Here:
        /// 
        ///    *U* \* *U*\ :sup:`T` = *U*\ :sup:`T` \* *U* = *I*
        /// 
        /// where *I* is the identity matrix. Also, *L(0) <= L(1) <= L(2) <= ...* (ascending order).
        /// 
        /// If *n>2*, *syevd* is performed separately on the trailing two dimensions of *A* (batch
        /// mode). In this case, *U* has *n* dimensions like *A*, and *L* has *n-1* dimensions.
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// .. note:: Derivatives for this operator are defined only if *A* is such that all its
        ///           eigenvalues are distinct, and the eigengaps are not too small. If you need
        ///           gradients, do not apply this operator to matrices with multiple eigenvalues.
        /// 
        /// Examples::
        /// 
        ///    // Single symmetric eigendecomposition
        ///    A = [[1., 2.], [2., 4.]]
        ///    U, L = syevd(A)
        ///    U = [[0.89442719, -0.4472136],
        ///         [0.4472136, 0.89442719]]
        ///    L = [0., 5.]
        /// 
        ///    // Batch symmetric eigendecomposition
        ///    A = [[[1., 2.], [2., 4.]],
        ///         [[1., 2.], [2., 5.]]]
        ///    U, L = syevd(A)
        ///    U = [[[0.89442719, -0.4472136],
        ///          [0.4472136, 0.89442719]],
        ///         [[0.92387953, -0.38268343],
        ///          [0.38268343, 0.92387953]]]
        ///    L = [[0., 5.],
        ///         [0.17157288, 5.82842712]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L867
        /// </summary>
        /// <param name="A">Tensor of input matrices to be factorized</param>
        public static NDArray LinalgSyevd(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_syevd",
                _linalgSyevdParamNames,
                Empty,
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgSyevdParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgSyevd(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_syevd",
                _backwardLinalgSyevdParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _linalgInverseParamNames = Empty;

        /// <summary>
        /// Compute the inverse of a matrix.
        /// Input is a tensor *A* of dimension *n >= 2*.
        /// 
        /// If *n=2*, *A* is a square matrix. We compute:
        /// 
        ///   *out* = *A*\ :sup:`-1`
        /// 
        /// If *n>2*, *inverse* is performed separately on the trailing two dimensions
        /// for all inputs (batch mode).
        /// 
        /// .. note:: The operator supports float32 and float64 data types only.
        /// 
        /// Examples::
        /// 
        ///    // Single matrix inversion
        ///    A = [[1., 4.], [2., 3.]]
        ///    inverse(A) = [[-0.6, 0.8], [0.4, -0.2]]
        /// 
        ///    // Batch matrix inversion
        ///    A = [[[1., 4.], [2., 3.]],
        ///         [[1., 3.], [2., 4.]]]
        ///    inverse(A) = [[[-0.6, 0.8], [0.4, -0.2]],
        ///                  [[-2., 1.5], [1., -0.5]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\la_op.cc:L917
        /// </summary>
        /// <param name="A">Tensor of square matrix</param>
        public static NDArray LinalgInverse(NDArrayOrSymbol A, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_linalg_inverse",
                _linalgInverseParamNames,
                Empty,
                new[] { A.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardLinalgInverseParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLinalgInverse(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_linalg_inverse",
                _backwardLinalgInverseParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _ReshapeParamNames = new[] { "shape", "reverse", "target_shape", "keep_highest" };

        /// <summary>
        /// Reshapes the input array.
        /// 
        /// .. note:: ``Reshape`` is deprecated, use ``reshape``
        /// 
        /// Given an array and a shape, this function returns a copy of the array in the new shape.
        /// The shape is a tuple of integers such as (2,3,4). The size of the new shape should be same as the size of the input array.
        /// 
        /// Example::
        /// 
        ///   reshape([1,2,3,4], shape=(2,2)) = [[1,2], [3,4]]
        /// 
        /// Some dimensions of the shape can take special values from the set {0, -1, -2, -3, -4}. The significance of each is explained below:
        /// 
        /// - ``0``  copy this dimension from the input to the output shape.
        /// 
        ///   Example::
        /// 
        ///   - input shape = (2,3,4), shape = (4,0,2), output shape = (4,3,2)
        ///   - input shape = (2,3,4), shape = (2,0,0), output shape = (2,3,4)
        /// 
        /// - ``-1`` infers the dimension of the output shape by using the remainder of the input dimensions
        ///   keeping the size of the new array same as that of the input array.
        ///   At most one dimension of shape can be -1.
        /// 
        ///   Example::
        /// 
        ///   - input shape = (2,3,4), shape = (6,1,-1), output shape = (6,1,4)
        ///   - input shape = (2,3,4), shape = (3,-1,8), output shape = (3,1,8)
        ///   - input shape = (2,3,4), shape=(-1,), output shape = (24,)
        /// 
        /// - ``-2`` copy all/remainder of the input dimensions to the output shape.
        /// 
        ///   Example::
        /// 
        ///   - input shape = (2,3,4), shape = (-2,), output shape = (2,3,4)
        ///   - input shape = (2,3,4), shape = (2,-2), output shape = (2,3,4)
        ///   - input shape = (2,3,4), shape = (-2,1,1), output shape = (2,3,4,1,1)
        /// 
        /// - ``-3`` use the product of two consecutive dimensions of the input shape as the output dimension.
        /// 
        ///   Example::
        /// 
        ///   - input shape = (2,3,4), shape = (-3,4), output shape = (6,4)
        ///   - input shape = (2,3,4,5), shape = (-3,-3), output shape = (6,20)
        ///   - input shape = (2,3,4), shape = (0,-3), output shape = (2,12)
        ///   - input shape = (2,3,4), shape = (-3,-2), output shape = (6,4)
        /// 
        /// - ``-4`` split one dimension of the input into two dimensions passed subsequent to -4 in shape (can contain -1).
        /// 
        ///   Example::
        /// 
        ///   - input shape = (2,3,4), shape = (-4,1,2,-2), output shape =(1,2,3,4)
        ///   - input shape = (2,3,4), shape = (2,-4,-1,3,-2), output shape = (2,1,3,4)
        /// 
        /// If the argument `reverse` is set to 1, then the special values are inferred from right to left.
        /// 
        ///   Example::
        /// 
        ///   - without reverse=1, for input shape = (10,5,4), shape = (-1,0), output shape would be (40,5)
        ///   - with reverse=1, output shape will be (50,4).
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L201
        /// </summary>
        /// <param name="data">Input data to reshape.</param>
        /// <param name="shape">The target shape</param>
        /// <param name="reverse">If true then the special values are inferred from right to left</param>
        /// <param name="target_shape">(Deprecated! Use ``shape`` instead.) Target new shape. One and only one dim can be 0, in which case it will be inferred from the rest of dims</param>
        /// <param name="keep_highest">(Deprecated! Use ``shape`` instead.) Whether keep the highest dim unchanged.If set to true, then the first dim in target_shape is ignored,and always fixed as input</param>
        public static NDArray Reshape(NDArrayOrSymbol data, NDShape shape = null, bool reverse = false, NDShape targetShape = null, bool keepHighest = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Reshape",
                _ReshapeParamNames,
                new[] { Convert(shape), Convert(reverse), Convert(targetShape), Convert(keepHighest) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _transposeParamNames = new[] { "axes" };

        /// <summary>
        /// Permutes the dimensions of an array.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 1, 2],
        ///        [ 3, 4]]
        /// 
        ///   transpose(x) = [[ 1.,  3.],
        ///                   [ 2.,  4.]]
        /// 
        ///   x = [[[ 1.,  2.],
        ///         [ 3.,  4.]],
        /// 
        ///        [[ 5.,  6.],
        ///         [ 7.,  8.]]]
        /// 
        ///   transpose(x) = [[[ 1.,  5.],
        ///                    [ 3.,  7.]],
        /// 
        ///                   [[ 2.,  6.],
        ///                    [ 4.,  8.]]]
        /// 
        ///   transpose(x, axes=(1,0,2)) = [[[ 1.,  2.],
        ///                                  [ 5.,  6.]],
        /// 
        ///                                 [[ 3.,  4.],
        ///                                  [ 7.,  8.]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L377
        /// </summary>
        /// <param name="data">Source input</param>
        /// <param name="axes">Target axis order. By default the axes will be inverted.</param>
        public static NDArray Transpose(NDArrayOrSymbol data, NDShape axes = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "transpose",
                _transposeParamNames,
                new[] { Convert(axes) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _expandDimsParamNames = new[] { "axis" };

        /// <summary>
        /// Inserts a new axis of size 1 into the array shape
        /// 
        /// For example, given ``x`` with shape ``(2,3,4)``, then ``expand_dims(x, axis=1)``
        /// will return a new array with shape ``(2,1,3,4)``.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L418
        /// </summary>
        /// <param name="data">Source input</param>
        /// <param name="axis">Position where new axis is to be inserted. Suppose that the input `NDArray`'s dimension is `ndim`, the range of the inserted axis is `[-ndim, ndim]`</param>
        public static NDArray ExpandDims(NDArrayOrSymbol data, int axis, NDArray output = null)
        {
            var result = Operator.Invoke(
                "expand_dims",
                _expandDimsParamNames,
                new[] { Convert(axis) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _sliceParamNames = new[] { "begin", "end", "step" };

        /// <summary>
        /// Slices a region of the array.
        /// 
        /// .. note:: ``crop`` is deprecated. Use ``slice`` instead.
        /// 
        /// This function returns a sliced array between the indices given
        /// by `begin` and `end` with the corresponding `step`.
        /// 
        /// For an input array of ``shape=(d_0, d_1, ..., d_n-1)``,
        /// slice operation with ``begin=(b_0, b_1...b_m-1)``,
        /// ``end=(e_0, e_1, ..., e_m-1)``, and ``step=(s_0, s_1, ..., s_m-1)``,
        /// where m <= n, results in an array with the shape
        /// ``(|e_0-b_0|/|s_0|, ..., |e_m-1-b_m-1|/|s_m-1|, d_m, ..., d_n-1)``.
        /// 
        /// The resulting array's *k*-th dimension contains elements
        /// from the *k*-th dimension of the input array starting
        /// from index ``b_k`` (inclusive) with step ``s_k``
        /// until reaching ``e_k`` (exclusive).
        /// 
        /// If the *k*-th elements are `None` in the sequence of `begin`, `end`,
        /// and `step`, the following rule will be used to set default values.
        /// If `s_k` is `None`, set `s_k=1`. If `s_k > 0`, set `b_k=0`, `e_k=d_k`;
        /// else, set `b_k=d_k-1`, `e_k=-1`.
        /// 
        /// The storage type of ``slice`` output depends on storage types of inputs
        /// 
        /// - slice(csr) = csr
        /// - otherwise, ``slice`` generates output with default storage
        /// 
        /// .. note:: When input data storage type is csr, it only supports
        ///    step=(), or step=(None,), or step=(1,) to generate a csr output.
        ///    For other step parameter values, it falls back to slicing
        ///    a dense tensor.
        /// 
        /// Example::
        /// 
        ///   x = [[  1.,   2.,   3.,   4.],
        ///        [  5.,   6.,   7.,   8.],
        ///        [  9.,  10.,  11.,  12.]]
        /// 
        ///   slice(x, begin=(0,1), end=(2,4)) = [[ 2.,  3.,  4.],
        ///                                      [ 6.,  7.,  8.]]
        ///   slice(x, begin=(None, 0), end=(None, 3), step=(-1, 2)) = [[9., 11.],
        ///                                                             [5.,  7.],
        ///                                                             [1.,  3.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L508
        /// </summary>
        /// <param name="data">Source input</param>
        /// <param name="begin">starting indices for the slice operation, supports negative indices.</param>
        /// <param name="end">ending indices for the slice operation, supports negative indices.</param>
        /// <param name="step">step for the slice operation, supports negative values.</param>
        public static NDArray Slice(NDArrayOrSymbol data, NDShape begin, NDShape end, NDShape step = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "slice",
                _sliceParamNames,
                new[] { Convert(begin), Convert(end), Convert(step) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSliceParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSlice(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_slice",
                _backwardSliceParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _sliceAssignParamNames = new[] { "begin", "end", "step" };

        /// <summary>
        /// Assign the rhs to a cropped subset of lhs.
        /// 
        /// Requirements
        /// ------------
        /// - output should be explicitly given and be the same as lhs.
        /// - lhs and rhs are of the same data type, and on the same device.
        /// 
        /// 
        /// From:C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:537
        /// </summary>
        /// <param name="lhs">Source input</param>
        /// <param name="rhs">value to assign</param>
        /// <param name="begin">starting indices for the slice operation, supports negative indices.</param>
        /// <param name="end">ending indices for the slice operation, supports negative indices.</param>
        /// <param name="step">step for the slice operation, supports negative values.</param>
        public static NDArray SliceAssign(NDArrayOrSymbol lhs, NDArrayOrSymbol rhs, NDShape begin, NDShape end, NDShape step = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_slice_assign",
                _sliceAssignParamNames,
                new[] { Convert(begin), Convert(end), Convert(step) },
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _sliceAssignScalarParamNames = new[] { "scalar", "begin", "end", "step" };

        /// <summary>
        /// (Assign the scalar to a cropped subset of the input.
        /// 
        /// Requirements
        /// ------------
        /// - output should be explicitly given and be the same as input
        /// )
        /// 
        /// From:C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:562
        /// </summary>
        /// <param name="data">Source input</param>
        /// <param name="scalar">The scalar value for assignment.</param>
        /// <param name="begin">starting indices for the slice operation, supports negative indices.</param>
        /// <param name="end">ending indices for the slice operation, supports negative indices.</param>
        /// <param name="step">step for the slice operation, supports negative values.</param>
        public static NDArray SliceAssignScalar(NDArrayOrSymbol data, NDShape begin, NDShape end, double scalar = 0, NDShape step = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_slice_assign_scalar",
                _sliceAssignScalarParamNames,
                new[] { Convert(scalar), Convert(begin), Convert(end), Convert(step) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _sliceAxisParamNames = new[] { "axis", "begin", "end" };

        /// <summary>
        /// Slices along a given axis.
        /// 
        /// Returns an array slice along a given `axis` starting from the `begin` index
        /// to the `end` index.
        /// 
        /// Examples::
        /// 
        ///   x = [[  1.,   2.,   3.,   4.],
        ///        [  5.,   6.,   7.,   8.],
        ///        [  9.,  10.,  11.,  12.]]
        /// 
        ///   slice_axis(x, axis=0, begin=1, end=3) = [[  5.,   6.,   7.,   8.],
        ///                                            [  9.,  10.,  11.,  12.]]
        /// 
        ///   slice_axis(x, axis=1, begin=0, end=2) = [[  1.,   2.],
        ///                                            [  5.,   6.],
        ///                                            [  9.,  10.]]
        /// 
        ///   slice_axis(x, axis=1, begin=-3, end=-1) = [[  2.,   3.],
        ///                                              [  6.,   7.],
        ///                                              [ 10.,  11.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L598
        /// </summary>
        /// <param name="data">Source input</param>
        /// <param name="axis">Axis along which to be sliced, supports negative indexes.</param>
        /// <param name="begin">The beginning index along the axis to be sliced,  supports negative indexes.</param>
        /// <param name="end">The ending index along the axis to be sliced,  supports negative indexes.</param>
        public static NDArray SliceAxis(NDArrayOrSymbol data, int axis, int begin, int? end, NDArray output = null)
        {
            var result = Operator.Invoke(
                "slice_axis",
                _sliceAxisParamNames,
                new[] { Convert(axis), Convert(begin), Convert(end) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSliceAxisParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSliceAxis(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_slice_axis",
                _backwardSliceAxisParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _sliceLikeParamNames = new[] { "axes" };

        /// <summary>
        /// Slices a region of the array like the shape of another array.
        /// 
        /// This function is similar to ``slice``, however, the `begin` are always `0`s
        /// and `end` of specific axes are inferred from the second input `shape_like`.
        /// 
        /// Given the second `shape_like` input of ``shape=(d_0, d_1, ..., d_n-1)``,
        /// a ``slice_like`` operator with default empty `axes`, it performs the
        /// following operation:
        /// 
        /// `` out = slice(input, begin=(0, 0, ..., 0), end=(d_0, d_1, ..., d_n-1))``.
        /// 
        /// When `axes` is not empty, it is used to speficy which axes are being sliced.
        /// 
        /// Given a 4-d input data, ``slice_like`` operator with ``axes=(0, 2, -1)``
        /// will perform the following operation:
        /// 
        /// `` out = slice(input, begin=(0, 0, 0, 0), end=(d_0, None, d_2, d_3))``.
        /// 
        /// Note that it is allowed to have first and second input with different dimensions,
        /// however, you have to make sure the `axes` are specified and not exceeding the
        /// dimension limits.
        /// 
        /// For example, given `input_1` with ``shape=(2,3,4,5)`` and `input_2` with
        /// ``shape=(1,2,3)``, it is not allowed to use:
        /// 
        /// `` out = slice_like(a, b)`` because ndim of `input_1` is 4, and ndim of `input_2`
        /// is 3.
        /// 
        /// The following is allowed in this situation:
        /// 
        /// `` out = slice_like(a, b, axes=(0, 2))``
        /// 
        /// Example::
        /// 
        ///   x = [[  1.,   2.,   3.,   4.],
        ///        [  5.,   6.,   7.,   8.],
        ///        [  9.,  10.,  11.,  12.]]
        /// 
        ///   y = [[  0.,   0.,   0.],
        ///        [  0.,   0.,   0.]]
        /// 
        ///   slice_like(x, y) = [[ 1.,  2.,  3.]
        ///                       [ 5.,  6.,  7.]]
        ///   slice_like(x, y, axes=(0, 1)) = [[ 1.,  2.,  3.]
        ///                                    [ 5.,  6.,  7.]]
        ///   slice_like(x, y, axes=(0)) = [[ 1.,  2.,  3.,  4.]
        ///                                 [ 5.,  6.,  7.,  8.]]
        ///   slice_like(x, y, axes=(-1)) = [[  1.,   2.,   3.]
        ///                                  [  5.,   6.,   7.]
        ///                                  [  9.,  10.,  11.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L667
        /// </summary>
        /// <param name="data">Source input</param>
        /// <param name="shape_like">Shape like input</param>
        /// <param name="axes">List of axes on which input data will be sliced according to the corresponding size of the second input. By default will slice on all axes. Negative axes are supported.</param>
        public static NDArray SliceLike(NDArrayOrSymbol data, NDArrayOrSymbol shapeLike, NDShape axes = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "slice_like",
                _sliceLikeParamNames,
                new[] { Convert(axes) },
                new[] { data.Handle, shapeLike.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSliceLikeParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSliceLike(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_slice_like",
                _backwardSliceLikeParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _clipParamNames = new[] { "a_min", "a_max" };

        /// <summary>
        /// Clips (limits) the values in an array.
        /// 
        /// Given an interval, values outside the interval are clipped to the interval edges.
        /// Clipping ``x`` between `a_min` and `a_x` would be::
        /// 
        ///    clip(x, a_min, a_max) = max(min(x, a_max), a_min))
        /// 
        /// Example::
        /// 
        ///     x = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
        /// 
        ///     clip(x,1,8) = [ 1.,  1.,  2.,  3.,  4.,  5.,  6.,  7.,  8.,  8.]
        /// 
        /// The storage type of ``clip`` output depends on storage types of inputs and the a_min, a_max \
        /// parameter values:
        /// 
        ///    - clip(default) = default
        ///    - clip(row_sparse, a_min <= 0, a_max >= 0) = row_sparse
        ///    - clip(csr, a_min <= 0, a_max >= 0) = csr
        ///    - clip(row_sparse, a_min < 0, a_max < 0) = default
        ///    - clip(row_sparse, a_min > 0, a_max > 0) = default
        ///    - clip(csr, a_min < 0, a_max < 0) = csr
        ///    - clip(csr, a_min > 0, a_max > 0) = csr
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L725
        /// </summary>
        /// <param name="data">Input array.</param>
        /// <param name="a_min">Minimum value</param>
        /// <param name="a_max">Maximum value</param>
        public static NDArray Clip(NDArrayOrSymbol data, double aMin, double aMax, NDArray output = null)
        {
            var result = Operator.Invoke(
                "clip",
                _clipParamNames,
                new[] { Convert(aMin), Convert(aMax) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardClipParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardClip(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_clip",
                _backwardClipParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _repeatParamNames = new[] { "repeats", "axis" };

        /// <summary>
        /// Repeats elements of an array.
        /// 
        /// By default, ``repeat`` flattens the input array into 1-D and then repeats the
        /// elements::
        /// 
        ///   x = [[ 1, 2],
        ///        [ 3, 4]]
        /// 
        ///   repeat(x, repeats=2) = [ 1.,  1.,  2.,  2.,  3.,  3.,  4.,  4.]
        /// 
        /// The parameter ``axis`` specifies the axis along which to perform repeat::
        /// 
        ///   repeat(x, repeats=2, axis=1) = [[ 1.,  1.,  2.,  2.],
        ///                                   [ 3.,  3.,  4.,  4.]]
        /// 
        ///   repeat(x, repeats=2, axis=0) = [[ 1.,  2.],
        ///                                   [ 1.,  2.],
        ///                                   [ 3.,  4.],
        ///                                   [ 3.,  4.]]
        /// 
        ///   repeat(x, repeats=2, axis=-1) = [[ 1.,  1.,  2.,  2.],
        ///                                    [ 3.,  3.,  4.,  4.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L798
        /// </summary>
        /// <param name="data">Input data array</param>
        /// <param name="repeats">The number of repetitions for each element.</param>
        /// <param name="axis">The axis along which to repeat values. The negative numbers are interpreted counting from the backward. By default, use the flattened input array, and return a flat output array.</param>
        public static NDArray Repeat(NDArrayOrSymbol data, int repeats, int? axis = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "repeat",
                _repeatParamNames,
                new[] { Convert(repeats), Convert(axis) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardRepeatParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardRepeat(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_repeat",
                _backwardRepeatParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _tileParamNames = new[] { "reps" };

        /// <summary>
        /// Repeats the whole array multiple times.
        /// 
        /// If ``reps`` has length *d*, and input array has dimension of *n*. There are
        /// three cases:
        /// 
        /// - **n=d**. Repeat *i*-th dimension of the input by ``reps[i]`` times::
        /// 
        ///     x = [[1, 2],
        ///          [3, 4]]
        /// 
        ///     tile(x, reps=(2,3)) = [[ 1.,  2.,  1.,  2.,  1.,  2.],
        ///                            [ 3.,  4.,  3.,  4.,  3.,  4.],
        ///                            [ 1.,  2.,  1.,  2.,  1.,  2.],
        ///                            [ 3.,  4.,  3.,  4.,  3.,  4.]]
        /// 
        /// - **n>d**. ``reps`` is promoted to length *n* by pre-pending 1's to it. Thus for
        ///   an input shape ``(2,3)``, ``repos=(2,)`` is treated as ``(1,2)``::
        /// 
        /// 
        ///     tile(x, reps=(2,)) = [[ 1.,  2.,  1.,  2.],
        ///                           [ 3.,  4.,  3.,  4.]]
        /// 
        /// - **n<d**. The input is promoted to be d-dimensional by prepending new axes. So a
        ///   shape ``(2,2)`` array is promoted to ``(1,2,2)`` for 3-D replication::
        /// 
        ///     tile(x, reps=(2,2,3)) = [[[ 1.,  2.,  1.,  2.,  1.,  2.],
        ///                               [ 3.,  4.,  3.,  4.,  3.,  4.],
        ///                               [ 1.,  2.,  1.,  2.,  1.,  2.],
        ///                               [ 3.,  4.,  3.,  4.,  3.,  4.]],
        /// 
        ///                              [[ 1.,  2.,  1.,  2.,  1.,  2.],
        ///                               [ 3.,  4.,  3.,  4.,  3.,  4.],
        ///                               [ 1.,  2.,  1.,  2.,  1.,  2.],
        ///                               [ 3.,  4.,  3.,  4.,  3.,  4.]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L859
        /// </summary>
        /// <param name="data">Input data array</param>
        /// <param name="reps">The number of times for repeating the tensor a. Each dim size of reps must be a positive integer. If reps has length d, the result will have dimension of max(d, a.ndim); If a.ndim < d, a is promoted to be d-dimensional by prepending new axes. If a.ndim > d, reps is promoted to a.ndim by pre-pending 1's to it.</param>
        public static NDArray Tile(NDArrayOrSymbol data, NDShape reps, NDArray output = null)
        {
            var result = Operator.Invoke(
                "tile",
                _tileParamNames,
                new[] { Convert(reps) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardTileParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardTile(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_tile",
                _backwardTileParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _reverseParamNames = new[] { "axis" };

        /// <summary>
        /// Reverses the order of elements along given axis while preserving array shape.
        /// 
        /// Note: reverse and flip are equivalent. We use reverse in the following examples.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 0.,  1.,  2.,  3.,  4.],
        ///        [ 5.,  6.,  7.,  8.,  9.]]
        /// 
        ///   reverse(x, axis=0) = [[ 5.,  6.,  7.,  8.,  9.],
        ///                         [ 0.,  1.,  2.,  3.,  4.]]
        /// 
        ///   reverse(x, axis=1) = [[ 4.,  3.,  2.,  1.,  0.],
        ///                         [ 9.,  8.,  7.,  6.,  5.]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L900
        /// </summary>
        /// <param name="data">Input data array</param>
        /// <param name="axis">The axis which to reverse elements.</param>
        public static NDArray Reverse(NDArrayOrSymbol data, NDShape axis, NDArray output = null)
        {
            var result = Operator.Invoke(
                "reverse",
                _reverseParamNames,
                new[] { Convert(axis) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardReverseParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardReverse(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_reverse",
                _backwardReverseParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardStackParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardStack(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_stack",
                _backwardStackParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardSqueezeParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSqueeze(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_squeeze",
                _backwardSqueezeParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _depthToSpaceParamNames = new[] { "block_size" };

        /// <summary>
        /// Rearranges(permutes) data from depth into blocks of spatial data.
        /// Similar to ONNX DepthToSpace operator:
        /// https://github.com/onnx/onnx/blob/master/docs/Operators.md#DepthToSpace.
        /// The output is a new tensor where the values from depth dimension are moved in spatial blocks 
        /// to height and width dimension. The reverse of this operation is ``space_to_depth``.
        /// 
        /// .. math::
        /// 
        ///     \begin{gather*}
        ///     x \prime = reshape(x, [N, block\_size, block\_size, C / (block\_size ^ 2), H * block\_size, W * block\_size]) \\
        ///     x \prime \prime = transpose(x \prime, [0, 3, 4, 1, 5, 2]) \\
        ///     y = reshape(x \prime \prime, [N, C / (block\_size ^ 2), H * block\_size, W * block\_size])
        ///     \end{gather*}
        /// 
        /// where :math:`x` is an input tensor with default layout as :math:`[N, C, H, W]`: [batch, channels, height, width] 
        /// and :math:`y` is the output tensor of layout :math:`[N, C / (block\_size ^ 2), H * block\_size, W * block\_size]`
        /// 
        /// Example::
        /// 
        ///   x = [[[[0, 1, 2],
        ///          [3, 4, 5]],
        ///         [[6, 7, 8],
        ///          [9, 10, 11]],
        ///         [[12, 13, 14],
        ///          [15, 16, 17]],
        ///         [[18, 19, 20],
        ///          [21, 22, 23]]]]
        /// 
        ///   depth_to_space(x, 2) = [[[[0, 6, 1, 7, 2, 8],
        ///                             [12, 18, 13, 19, 14, 20],
        ///                             [3, 9, 4, 10, 5, 11],
        ///                             [15, 21, 16, 22, 17, 23]]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L1052
        /// </summary>
        /// <param name="data">Input ndarray</param>
        /// <param name="block_size">Blocks of [block_size. block_size] are moved</param>
        public static NDArray DepthToSpace(NDArrayOrSymbol data, int blockSize, NDArray output = null)
        {
            var result = Operator.Invoke(
                "depth_to_space",
                _depthToSpaceParamNames,
                new[] { Convert(blockSize) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _spaceToDepthParamNames = new[] { "block_size" };

        /// <summary>
        /// Rearranges(permutes) blocks of spatial data into depth.
        /// Similar to ONNX SpaceToDepth operator:
        /// https://github.com/onnx/onnx/blob/master/docs/Operators.md#SpaceToDepth 
        /// 
        /// The output is a new tensor where the values from height and width dimension are 
        /// moved to the depth dimension. The reverse of this operation is ``depth_to_space``.
        /// 
        /// .. math::
        /// 
        ///     \begin{gather*}
        ///     x \prime = reshape(x, [N, C, H / block\_size, block\_size, W / block\_size, block\_size]) \\
        ///     x \prime \prime = transpose(x \prime, [0, 3, 5, 1, 2, 4]) \\
        ///     y = reshape(x \prime \prime, [N, C * (block\_size ^ 2), H / block\_size, W / block\_size])
        ///     \end{gather*}
        /// 
        /// where :math:`x` is an input tensor with default layout as :math:`[N, C, H, W]`: [batch, channels, height, width] 
        /// and :math:`y` is the output tensor of layout :math:`[N, C * (block\_size ^ 2), H / block\_size, W / block\_size]`
        /// 
        /// Example::
        /// 
        ///   x = [[[[0, 6, 1, 7, 2, 8],
        ///          [12, 18, 13, 19, 14, 20],
        ///          [3, 9, 4, 10, 5, 11],
        ///          [15, 21, 16, 22, 17, 23]]]]
        /// 
        /// 
        ///   space_to_depth(x, 2) = [[[[0, 1, 2],
        ///                             [3, 4, 5]],
        ///                            [[6, 7, 8],
        ///                             [9, 10, 11]],
        ///                            [[12, 13, 14],
        ///                             [15, 16, 17]],
        ///                            [[18, 19, 20],
        ///                             [21, 22, 23]]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L1106
        /// </summary>
        /// <param name="data">Input ndarray</param>
        /// <param name="block_size">Blocks of [block_size. block_size] are moved</param>
        public static NDArray SpaceToDepth(NDArrayOrSymbol data, int blockSize, NDArray output = null)
        {
            var result = Operator.Invoke(
                "space_to_depth",
                _spaceToDepthParamNames,
                new[] { Convert(blockSize) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _splitV2ParamNames = new[] { "indices", "axis", "squeeze_axis", "sections" };

        /// <summary>
        /// Splits an array along a particular axis into multiple sub-arrays.
        /// 
        /// Example::
        /// 
        ///    x  = [[[ 1.]
        ///           [ 2.]]
        ///          [[ 3.]
        ///           [ 4.]]
        ///          [[ 5.]
        ///           [ 6.]]]
        ///    x.shape = (3, 2, 1)
        /// 
        ///    y = split_v2(x, axis=1, indices_or_sections=2) // a list of 2 arrays with shape (3, 1, 1)
        ///    y = [[[ 1.]]
        ///         [[ 3.]]
        ///         [[ 5.]]]
        /// 
        ///        [[[ 2.]]
        ///         [[ 4.]]
        ///         [[ 6.]]]
        /// 
        ///    y[0].shape = (3, 1, 1)
        /// 
        ///    z = split_v2(x, axis=0, indices_or_sections=3) // a list of 3 arrays with shape (1, 2, 1)
        ///    z = [[[ 1.]
        ///          [ 2.]]]
        /// 
        ///        [[[ 3.]
        ///          [ 4.]]]
        /// 
        ///        [[[ 5.]
        ///          [ 6.]]]
        /// 
        ///    z[0].shape = (1, 2, 1)
        /// 
        ///    w = split_v2(x, axis=0, indices_or_sections=(1,)) // a list of 2 arrays with shape [(1, 2, 1), (2, 2, 1)]
        ///    w = [[[ 1.]
        ///          [ 2.]]]
        /// 
        ///        [[[3.]
        ///          [4.]]
        /// 
        ///         [[5.]
        ///          [6.]]]
        /// 
        ///   w[0].shape = (1, 2, 1)
        ///   w[1].shape = (2, 2, 1)
        /// 
        /// `squeeze_axis=True` removes the axis with length 1 from the shapes of the output arrays.
        /// **Note** that setting `squeeze_axis` to ``1`` removes axis with length 1 only
        /// along the `axis` which it is split.
        /// Also `squeeze_axis` can be set to true only if ``input.shape[axis] == indices_or_sections``.
        /// 
        /// Example::
        /// 
        ///    z = split_v2(x, axis=0, indices_or_sections=3, squeeze_axis=1) // a list of 3 arrays with shape (2, 1)
        ///    z = [[ 1.]
        ///         [ 2.]]
        /// 
        ///        [[ 3.]
        ///         [ 4.]]
        /// 
        ///        [[ 5.]
        ///         [ 6.]]
        ///    z[0].shape = (2, 1)
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\matrix_op.cc:L1192
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="indices">Indices of splits. The elements should denote the boundaries of at which split is performed along the `axis`.</param>
        /// <param name="axis">Axis along which to split.</param>
        /// <param name="squeeze_axis">If true, Removes the axis with length 1 from the shapes of the output arrays. **Note** that setting `squeeze_axis` to ``true`` removes axis with length 1 only along the `axis` which it is split. Also `squeeze_axis` can be set to ``true`` only if ``input.shape[axis] == num_outputs``.</param>
        /// <param name="sections">Number of sections if equally splitted. Default to 0 which means split by indices.</param>
        public static NDArray SplitV2(NDArrayOrSymbol data, NDShape indices, int axis = 1, bool squeezeAxis = false, int sections = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_split_v2",
                _splitV2ParamNames,
                new[] { Convert(indices), Convert(axis), Convert(squeezeAxis), Convert(sections) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _splitV2BackwardParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray SplitV2Backward(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_split_v2_backward",
                _splitV2BackwardParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _topkParamNames = new[] { "axis", "k", "ret_typ", "is_ascend", "dtype" };

        /// <summary>
        /// Returns the top *k* elements in an input array along the given axis.
        ///  The returned elements will be sorted.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 0.3,  0.2,  0.4],
        ///        [ 0.1,  0.3,  0.2]]
        /// 
        ///   // returns an index of the largest element on last axis
        ///   topk(x) = [[ 2.],
        ///              [ 1.]]
        /// 
        ///   // returns the value of top-2 largest elements on last axis
        ///   topk(x, ret_typ='value', k=2) = [[ 0.4,  0.3],
        ///                                    [ 0.3,  0.2]]
        /// 
        ///   // returns the value of top-2 smallest elements on last axis
        ///   topk(x, ret_typ='value', k=2, is_ascend=1) = [[ 0.2 ,  0.3],
        ///                                                [ 0.1 ,  0.2]]
        /// 
        ///   // returns the value of top-2 largest elements on axis 0
        ///   topk(x, axis=0, ret_typ='value', k=2) = [[ 0.3,  0.3,  0.4],
        ///                                            [ 0.1,  0.2,  0.2]]
        /// 
        ///   // flattens and then returns list of both values and indices
        ///   topk(x, ret_typ='both', k=2) = [[[ 0.4,  0.3], [ 0.3,  0.2]] ,  [[ 2.,  0.], [ 1.,  2.]]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\ordering_op.cc:L64
        /// </summary>
        /// <param name="data">The input array</param>
        /// <param name="axis">Axis along which to choose the top k indices. If not given, the flattened array is used. Default is -1.</param>
        /// <param name="k">Number of top elements to select, should be always smaller than or equal to the element number in the given axis. A global sort is performed if set k < 1.</param>
        /// <param name="ret_typ">The return type.
        ///  "value" means to return the top k values, "indices" means to return the indices of the top k values, "mask" means to return a mask array containing 0 and 1. 1 means the top k values. "both" means to return a list of both values and indices of top k elements.</param>
        /// <param name="is_ascend">Whether to choose k largest or k smallest elements. Top K largest elements will be chosen if set to false.</param>
        /// <param name="dtype">DType of the output indices when ret_typ is "indices" or "both". An error will be raised if the selected data type cannot precisely represent the indices.</param>
        public static NDArray Topk(NDArrayOrSymbol data, int? axis = -1, int k = 1, string retTyp = "indices", bool isAscend = false, string dtype = "float32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "topk",
                _topkParamNames,
                new[] { Convert(axis), Convert(k), Convert(retTyp), Convert(isAscend), Convert(dtype) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardTopkParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardTopk(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_topk",
                _backwardTopkParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _sortParamNames = new[] { "axis", "is_ascend" };

        /// <summary>
        /// Returns a sorted copy of an input array along the given axis.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 1, 4],
        ///        [ 3, 1]]
        /// 
        ///   // sorts along the last axis
        ///   sort(x) = [[ 1.,  4.],
        ///              [ 1.,  3.]]
        /// 
        ///   // flattens and then sorts
        ///   sort(x) = [ 1.,  1.,  3.,  4.]
        /// 
        ///   // sorts along the first axis
        ///   sort(x, axis=0) = [[ 1.,  1.],
        ///                      [ 3.,  4.]]
        /// 
        ///   // in a descend order
        ///   sort(x, is_ascend=0) = [[ 4.,  1.],
        ///                           [ 3.,  1.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\ordering_op.cc:L127
        /// </summary>
        /// <param name="data">The input array</param>
        /// <param name="axis">Axis along which to choose sort the input tensor. If not given, the flattened array is used. Default is -1.</param>
        /// <param name="is_ascend">Whether to sort in ascending or descending order.</param>
        public static NDArray Sort(NDArrayOrSymbol data, int? axis = -1, bool isAscend = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "sort",
                _sortParamNames,
                new[] { Convert(axis), Convert(isAscend) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _argsortParamNames = new[] { "axis", "is_ascend", "dtype" };

        /// <summary>
        /// Returns the indices that would sort an input array along the given axis.
        /// 
        /// This function performs sorting along the given axis and returns an array of indices having same shape
        /// as an input array that index data in sorted order.
        /// 
        /// Examples::
        /// 
        ///   x = [[ 0.3,  0.2,  0.4],
        ///        [ 0.1,  0.3,  0.2]]
        /// 
        ///   // sort along axis -1
        ///   argsort(x) = [[ 1.,  0.,  2.],
        ///                 [ 0.,  2.,  1.]]
        /// 
        ///   // sort along axis 0
        ///   argsort(x, axis=0) = [[ 1.,  0.,  1.]
        ///                         [ 0.,  1.,  0.]]
        /// 
        ///   // flatten and then sort
        ///   argsort(x) = [ 3.,  1.,  5.,  0.,  4.,  2.]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\ordering_op.cc:L177
        /// </summary>
        /// <param name="data">The input array</param>
        /// <param name="axis">Axis along which to sort the input tensor. If not given, the flattened array is used. Default is -1.</param>
        /// <param name="is_ascend">Whether to sort in ascending or descending order.</param>
        /// <param name="dtype">DType of the output indices. It is only valid when ret_typ is "indices" or "both". An error will be raised if the selected data type cannot precisely represent the indices.</param>
        public static NDArray Argsort(NDArrayOrSymbol data, int? axis = -1, bool isAscend = true, string dtype = "float32", NDArray output = null)
        {
            var result = Operator.Invoke(
                "argsort",
                _argsortParamNames,
                new[] { Convert(axis), Convert(isAscend), Convert(dtype) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _ravelMultiIndexParamNames = new[] { "shape" };

        /// <summary>
        /// Converts a batch of index arrays into an array of flat indices. The operator follows numpy conventions so a single multi index is given by a column of the input matrix. The leading dimension may be left unspecified by using -1 as placeholder.  
        /// 
        /// Examples::
        ///    
        ///    A = [[3,6,6],[4,5,1]]
        ///    ravel(A, shape=(7,6)) = [22,41,37]
        ///    ravel(A, shape=(-1,6)) = [22,41,37]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\ravel.cc:L42
        /// </summary>
        /// <param name="data">Batch of multi-indices</param>
        /// <param name="shape">Shape of the array into which the multi-indices apply.</param>
        public static NDArray RavelMultiIndex(NDArrayOrSymbol data, NDShape shape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_ravel_multi_index",
                _ravelMultiIndexParamNames,
                new[] { Convert(shape) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _unravelIndexParamNames = new[] { "shape" };

        /// <summary>
        /// Converts an array of flat indices into a batch of index arrays. The operator follows numpy conventions so a single multi index is given by a column of the output matrix. The leading dimension may be left unspecified by using -1 as placeholder.  
        /// 
        /// Examples::
        /// 
        ///    A = [22,41,37]
        ///    unravel(A, shape=(7,6)) = [[3,6,6],[4,5,1]]
        ///    unravel(A, shape=(-1,6)) = [[3,6,6],[4,5,1]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\ravel.cc:L67
        /// </summary>
        /// <param name="data">Array of flat indices</param>
        /// <param name="shape">Shape of the array into which the multi-indices apply.</param>
        public static NDArray UnravelIndex(NDArrayOrSymbol data, NDShape shape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_unravel_index",
                _unravelIndexParamNames,
                new[] { Convert(shape) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _sparseRetainParamNames = Empty;

        /// <summary>
        /// pick rows specified by user input index array from a row sparse matrix
        /// and save them in the output sparse matrix.
        /// 
        /// Example::
        /// 
        ///   data = [[1, 2], [3, 4], [5, 6]]
        ///   indices = [0, 1, 3]
        ///   shape = (4, 2)
        ///   rsp_in = row_sparse(data, indices)
        ///   to_retain = [0, 3]
        ///   rsp_out = retain(rsp_in, to_retain)
        ///   rsp_out.values = [[1, 2], [5, 6]]
        ///   rsp_out.indices = [0, 3]
        /// 
        /// The storage type of ``retain`` output depends on storage types of inputs
        /// 
        /// - retain(row_sparse, default) = row_sparse
        /// - otherwise, ``retain`` is not supported
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\sparse_retain.cc:L53
        /// </summary>
        /// <param name="data">The input array for sparse_retain operator.</param>
        /// <param name="indices">The index array of rows ids that will be retained.</param>
        public static NDArray SparseRetain(NDArrayOrSymbol data, NDArrayOrSymbol indices, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_sparse_retain",
                _sparseRetainParamNames,
                Empty,
                new[] { data.Handle, indices.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSparseRetainParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSparseRetain(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_sparse_retain",
                _backwardSparseRetainParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _squareSumParamNames = new[] { "axis", "keepdims", "exclude" };

        /// <summary>
        /// Computes the square sum of array elements over a given axis
        /// for row-sparse matrix. This is a temporary solution for fusing ops square and
        /// sum together for row-sparse matrix to save memory for storing gradients.
        /// It will become deprecated once the functionality of fusing operators is finished
        /// in the future.
        /// 
        /// Example::
        /// 
        ///   dns = mx.nd.array([[0, 0], [1, 2], [0, 0], [3, 4], [0, 0]])
        ///   rsp = dns.tostype('row_sparse')
        ///   sum = mx.nd._internal._square_sum(rsp, axis=1)
        ///   sum = [0, 5, 0, 25, 0]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\tensor\square_sum.cc:L63
        /// </summary>
        /// <param name="data">The input</param>
        /// <param name="axis">The axis or axes along which to perform the reduction.
        /// 
        ///       The default, `axis=()`, will compute over all elements into a
        ///       scalar array with shape `(1,)`.
        /// 
        ///       If `axis` is int, a reduction is performed on a particular axis.
        /// 
        ///       If `axis` is a tuple of ints, a reduction is performed on all the axes
        ///       specified in the tuple.
        /// 
        ///       If `exclude` is true, reduction will be performed on the axes that are
        ///       NOT in axis instead.
        /// 
        ///       Negative values means indexing from right to left.</param>
        /// <param name="keepdims">If this is set to `True`, the reduced axes are left in the result as dimension with size one.</param>
        /// <param name="exclude">Whether to perform reduction on axis that are NOT in axis instead.</param>
        public static NDArray SquareSum(NDArrayOrSymbol data, NDShape axis = null, bool keepdims = false, bool exclude = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_square_sum",
                _squareSumParamNames,
                new[] { Convert(axis), Convert(keepdims), Convert(exclude) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSquareSumParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSquareSum(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_square_sum",
                _backwardSquareSumParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardBatchNormV1ParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBatchNormV1(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_BatchNorm_v1",
                _backwardBatchNormV1ParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _BilinearSamplerParamNames = new[] { "cudnn_off" };

        /// <summary>
        /// Applies bilinear sampling to input feature map.
        /// 
        /// Bilinear Sampling is the key of  [NIPS2015] \"Spatial Transformer Networks\". The usage of the operator is very similar to remap function in OpenCV,
        /// except that the operator has the backward pass.
        /// 
        /// Given :math:`data` and :math:`grid`, then the output is computed by
        /// 
        /// .. math::
        ///   x_{src} = grid[batch, 0, y_{dst}, x_{dst}] \\
        ///   y_{src} = grid[batch, 1, y_{dst}, x_{dst}] \\
        ///   output[batch, channel, y_{dst}, x_{dst}] = G(data[batch, channel, y_{src}, x_{src})
        /// 
        /// :math:`x_{dst}`, :math:`y_{dst}` enumerate all spatial locations in :math:`output`, and :math:`G()` denotes the bilinear interpolation kernel.
        /// The out-boundary points will be padded with zeros.The shape of the output will be (data.shape[0], data.shape[1], grid.shape[2], grid.shape[3]).
        /// 
        /// The operator assumes that :math:`data` has 'NCHW' layout and :math:`grid` has been normalized to [-1, 1].
        /// 
        /// BilinearSampler often cooperates with GridGenerator which generates sampling grids for BilinearSampler.
        /// GridGenerator supports two kinds of transformation: ``affine`` and ``warp``.
        /// If users want to design a CustomOp to manipulate :math:`grid`, please firstly refer to the code of GridGenerator.
        /// 
        /// Example 1::
        /// 
        ///   ## Zoom out data two times
        ///   data = array([[[[1, 4, 3, 6],
        ///                   [1, 8, 8, 9],
        ///                   [0, 4, 1, 5],
        ///                   [1, 0, 1, 3]]]])
        /// 
        ///   affine_matrix = array([[2, 0, 0],
        ///                          [0, 2, 0]])
        /// 
        ///   affine_matrix = reshape(affine_matrix, shape=(1, 6))
        /// 
        ///   grid = GridGenerator(data=affine_matrix, transform_type='affine', target_shape=(4, 4))
        /// 
        ///   out = BilinearSampler(data, grid)
        /// 
        ///   out
        ///   [[[[ 0,   0,     0,   0],
        ///      [ 0,   3.5,   6.5, 0],
        ///      [ 0,   1.25,  2.5, 0],
        ///      [ 0,   0,     0,   0]]]
        /// 
        /// 
        /// Example 2::
        /// 
        ///   ## shift data horizontally by -1 pixel
        /// 
        ///   data = array([[[[1, 4, 3, 6],
        ///                   [1, 8, 8, 9],
        ///                   [0, 4, 1, 5],
        ///                   [1, 0, 1, 3]]]])
        /// 
        ///   warp_maxtrix = array([[[[1, 1, 1, 1],
        ///                           [1, 1, 1, 1],
        ///                           [1, 1, 1, 1],
        ///                           [1, 1, 1, 1]],
        ///                          [[0, 0, 0, 0],
        ///                           [0, 0, 0, 0],
        ///                           [0, 0, 0, 0],
        ///                           [0, 0, 0, 0]]]])
        /// 
        ///   grid = GridGenerator(data=warp_matrix, transform_type='warp')
        ///   out = BilinearSampler(data, grid)
        /// 
        ///   out
        ///   [[[[ 4,  3,  6,  0],
        ///      [ 8,  8,  9,  0],
        ///      [ 4,  1,  5,  0],
        ///      [ 0,  1,  3,  0]]]
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\bilinear_sampler.cc:L256
        /// </summary>
        /// <param name="data">Input data to the BilinearsamplerOp.</param>
        /// <param name="grid">Input grid to the BilinearsamplerOp.grid has two channels: x_src, y_src</param>
        /// <param name="cudnn_off">whether to turn cudnn off</param>
        public static NDArray BilinearSampler(NDArrayOrSymbol data, NDArrayOrSymbol grid, bool? cudnnOff = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "BilinearSampler",
                _BilinearSamplerParamNames,
                new[] { Convert(cudnnOff) },
                new[] { data.Handle, grid.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardBilinearSamplerParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardBilinearSampler(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_BilinearSampler",
                _backwardBilinearSamplerParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _ConvolutionV1ParamNames = new[] { "kernel", "stride", "dilate", "pad", "num_filter", "num_group", "workspace", "no_bias", "cudnn_tune", "cudnn_off", "layout" };

        /// <summary>
        /// This operator is DEPRECATED. Apply convolution to input then add a bias.
        /// </summary>
        /// <param name="data">Input data to the ConvolutionV1Op.</param>
        /// <param name="weight">Weight matrix.</param>
        /// <param name="bias">Bias parameter.</param>
        /// <param name="kernel">convolution kernel size: (h, w) or (d, h, w)</param>
        /// <param name="stride">convolution stride: (h, w) or (d, h, w)</param>
        /// <param name="dilate">convolution dilate: (h, w) or (d, h, w)</param>
        /// <param name="pad">pad for convolution: (h, w) or (d, h, w)</param>
        /// <param name="num_filter">convolution filter(channel) number</param>
        /// <param name="num_group">Number of group partitions. Equivalent to slicing input into num_group
        ///     partitions, apply convolution on each, then concatenate the results</param>
        /// <param name="workspace">Maximum temporary workspace allowed for convolution (MB).This parameter determines the effective batch size of the convolution kernel, which may be smaller than the given batch size. Also, the workspace will be automatically enlarged to make sure that we can run the kernel with batch_size=1</param>
        /// <param name="no_bias">Whether to disable bias parameter.</param>
        /// <param name="cudnn_tune">Whether to pick convolution algo by running performance test.
        ///     Leads to higher startup time but may give faster speed. Options are:
        ///     'off': no tuning
        ///     'limited_workspace': run test and pick the fastest algorithm that doesn't exceed workspace limit.
        ///     'fastest': pick the fastest algorithm and ignore workspace limit.
        ///     If set to None (default), behavior is determined by environment
        ///     variable MXNET_CUDNN_AUTOTUNE_DEFAULT: 0 for off,
        ///     1 for limited workspace (default), 2 for fastest.</param>
        /// <param name="cudnn_off">Turn off cudnn for this layer.</param>
        /// <param name="layout">Set layout for input, output and weight. Empty for
        ///     default layout: NCHW for 2d and NCDHW for 3d.</param>
        public static NDArray ConvolutionV1(NDArrayOrSymbol data, NDArrayOrSymbol weight, NDArrayOrSymbol bias, NDShape kernel, int numFilter, NDShape stride = null, NDShape dilate = null, NDShape pad = null, int numGroup = 1, long workspace = 1024, bool noBias = false, string cudnnTune = null, bool cudnnOff = false, string layout = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Convolution_v1",
                _ConvolutionV1ParamNames,
                new[] { Convert(kernel), Convert(stride), Convert(dilate), Convert(pad), Convert(numFilter), Convert(numGroup), Convert(workspace), Convert(noBias), Convert(cudnnTune), Convert(cudnnOff), Convert(layout) },
                new[] { data.Handle, weight.Handle, bias.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardConvolutionV1ParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardConvolutionV1(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Convolution_v1",
                _backwardConvolutionV1ParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _CorrelationParamNames = new[] { "kernel_size", "max_displacement", "stride1", "stride2", "pad_size", "is_multiply" };

        /// <summary>
        /// Applies correlation to inputs.
        /// 
        /// The correlation layer performs multiplicative patch comparisons between two feature maps.
        /// 
        /// Given two multi-channel feature maps :math:`f_{1}, f_{2}`, with :math:`w`, :math:`h`, and :math:`c` being their width, height, and number of channels,
        /// the correlation layer lets the network compare each patch from :math:`f_{1}` with each patch from :math:`f_{2}`.
        /// 
        /// For now we consider only a single comparison of two patches. The 'correlation' of two patches centered at :math:`x_{1}` in the first map and
        /// :math:`x_{2}` in the second map is then defined as:
        /// 
        /// .. math::
        /// 
        ///    c(x_{1}, x_{2}) = \sum_{o \in [-k,k] \times [-k,k]} <f_{1}(x_{1} + o), f_{2}(x_{2} + o)>
        /// 
        /// for a square patch of size :math:`K:=2k+1`.
        /// 
        /// Note that the equation above is identical to one step of a convolution in neural networks, but instead of convolving data with a filter, it convolves data with other
        /// data. For this reason, it has no training weights.
        /// 
        /// Computing :math:`c(x_{1}, x_{2})` involves :math:`c * K^{2}` multiplications. Comparing all patch combinations involves :math:`w^{2}*h^{2}` such computations.
        /// 
        /// Given a maximum displacement :math:`d`, for each location :math:`x_{1}` it computes correlations :math:`c(x_{1}, x_{2})` only in a neighborhood of size :math:`D:=2d+1`,
        /// by limiting the range of :math:`x_{2}`. We use strides :math:`s_{1}, s_{2}`, to quantize :math:`x_{1}` globally and to quantize :math:`x_{2}` within the neighborhood
        /// centered around :math:`x_{1}`.
        /// 
        /// The final output is defined by the following expression:
        /// 
        /// .. math::
        ///   out[n, q, i, j] = c(x_{i, j}, x_{q})
        /// 
        /// where :math:`i` and :math:`j` enumerate spatial locations in :math:`f_{1}`, and :math:`q` denotes the :math:`q^{th}` neighborhood of :math:`x_{i,j}`.
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\correlation.cc:L198
        /// </summary>
        /// <param name="data1">Input data1 to the correlation.</param>
        /// <param name="data2">Input data2 to the correlation.</param>
        /// <param name="kernel_size">kernel size for Correlation must be an odd number</param>
        /// <param name="max_displacement">Max displacement of Correlation </param>
        /// <param name="stride1">stride1 quantize data1 globally</param>
        /// <param name="stride2">stride2 quantize data2 within the neighborhood centered around data1</param>
        /// <param name="pad_size">pad for Correlation</param>
        /// <param name="is_multiply">operation type is either multiplication or subduction</param>
        public static NDArray Correlation(NDArrayOrSymbol data1, NDArrayOrSymbol data2, int kernelSize = 1, int maxDisplacement = 1, int stride1 = 1, int stride2 = 1, int padSize = 0, bool isMultiply = true, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Correlation",
                _CorrelationParamNames,
                new[] { Convert(kernelSize), Convert(maxDisplacement), Convert(stride1), Convert(stride2), Convert(padSize), Convert(isMultiply) },
                new[] { data1.Handle, data2.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardCorrelationParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCorrelation(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Correlation",
                _backwardCorrelationParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardCropParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardCrop(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Crop",
                _backwardCropParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _CrossDeviceCopyParamNames = Empty;

        /// <summary>
        /// Special op to copy data cross device
        /// </summary>
        public static NDArray CrossDeviceCopy(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_CrossDeviceCopy",
                _CrossDeviceCopyParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backward_CrossDeviceCopyParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray Backward_CrossDeviceCopy(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward__CrossDeviceCopy",
                _backward_CrossDeviceCopyParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backward_NativeParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray Backward_Native(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward__Native",
                _backward_NativeParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backward_NDArrayParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray Backward_NDArray(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward__NDArray",
                _backward_NDArrayParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _GridGeneratorParamNames = new[] { "transform_type", "target_shape" };

        /// <summary>
        /// Generates 2D sampling grid for bilinear sampling.
        /// </summary>
        /// <param name="data">Input data to the function.</param>
        /// <param name="transform_type">The type of transformation. For `affine`, input data should be an affine matrix of size (batch, 6). For `warp`, input data should be an optical flow of size (batch, 2, h, w).</param>
        /// <param name="target_shape">Specifies the output shape (H, W). This is required if transformation type is `affine`. If transformation type is `warp`, this parameter is ignored.</param>
        public static NDArray GridGenerator(NDArrayOrSymbol data, string transformType, NDShape targetShape = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "GridGenerator",
                _GridGeneratorParamNames,
                new[] { Convert(transformType), Convert(targetShape) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardGridGeneratorParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardGridGenerator(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_GridGenerator",
                _backwardGridGeneratorParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardIdentityAttachKLSparseRegParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardIdentityAttachKLSparseReg(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_IdentityAttachKLSparseReg",
                _backwardIdentityAttachKLSparseRegParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _InstanceNormParamNames = new[] { "eps" };

        /// <summary>
        /// Applies instance normalization to the n-dimensional input array.
        /// 
        /// This operator takes an n-dimensional input array where (n>2) and normalizes
        /// the input using the following formula:
        /// 
        /// .. math::
        /// 
        ///   out = \frac{x - mean[data]}{ \sqrt{Var[data]} + \epsilon} * gamma + beta
        /// 
        /// This layer is similar to batch normalization layer (`BatchNorm`)
        /// with two differences: first, the normalization is
        /// carried out per example (instance), not over a batch. Second, the
        /// same normalization is applied both at test and train time. This
        /// operation is also known as `contrast normalization`.
        /// 
        /// If the input data is of shape [batch, channel, spacial_dim1, spacial_dim2, ...],
        /// `gamma` and `beta` parameters must be vectors of shape [channel].
        /// 
        /// This implementation is based on paper:
        /// 
        /// .. [1] Instance Normalization: The Missing Ingredient for Fast Stylization,
        ///    D. Ulyanov, A. Vedaldi, V. Lempitsky, 2016 (arXiv:1607.08022v2).
        /// 
        /// Examples::
        /// 
        ///   // Input of shape (2,1,2)
        ///   x = [[[ 1.1,  2.2]],
        ///        [[ 3.3,  4.4]]]
        /// 
        ///   // gamma parameter of length 1
        ///   gamma = [1.5]
        /// 
        ///   // beta parameter of length 1
        ///   beta = [0.5]
        /// 
        ///   // Instance normalization is calculated with the above formula
        ///   InstanceNorm(x,gamma,beta) = [[[-0.997527  ,  1.99752665]],
        ///                                 [[-0.99752653,  1.99752724]]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\instance_norm.cc:L95
        /// </summary>
        /// <param name="data">An n-dimensional input array (n > 2) of the form [batch, channel, spatial_dim1, spatial_dim2, ...].</param>
        /// <param name="gamma">A vector of length 'channel', which multiplies the normalized input.</param>
        /// <param name="beta">A vector of length 'channel', which is added to the product of the normalized input and the weight.</param>
        /// <param name="eps">An `epsilon` parameter to prevent division by 0.</param>
        public static NDArray InstanceNorm(NDArrayOrSymbol data, NDArrayOrSymbol gamma, NDArrayOrSymbol beta, double eps = 0.00100000005, NDArray output = null)
        {
            var result = Operator.Invoke(
                "InstanceNorm",
                _InstanceNormParamNames,
                new[] { Convert(eps) },
                new[] { data.Handle, gamma.Handle, beta.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardInstanceNormParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardInstanceNorm(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_InstanceNorm",
                _backwardInstanceNormParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _L2NormalizationParamNames = new[] { "eps", "mode" };

        /// <summary>
        /// Normalize the input array using the L2 norm.
        /// 
        /// For 1-D NDArray, it computes::
        /// 
        ///   out = data / sqrt(sum(data ** 2) + eps)
        /// 
        /// For N-D NDArray, if the input array has shape (N, N, ..., N),
        /// 
        /// with ``mode`` = ``instance``, it normalizes each instance in the multidimensional
        /// array by its L2 norm.::
        /// 
        ///   for i in 0...N
        ///     out[i,:,:,...,:] = data[i,:,:,...,:] / sqrt(sum(data[i,:,:,...,:] ** 2) + eps)
        /// 
        /// with ``mode`` = ``channel``, it normalizes each channel in the array by its L2 norm.::
        /// 
        ///   for i in 0...N
        ///     out[:,i,:,...,:] = data[:,i,:,...,:] / sqrt(sum(data[:,i,:,...,:] ** 2) + eps)
        /// 
        /// with ``mode`` = ``spatial``, it normalizes the cross channel norm for each position
        /// in the array by its L2 norm.::
        /// 
        ///   for dim in 2...N
        ///     for i in 0...N
        ///       out[.....,i,...] = take(out, indices=i, axis=dim) / sqrt(sum(take(out, indices=i, axis=dim) ** 2) + eps)
        ///           -dim-
        /// 
        /// Example::
        /// 
        ///   x = [[[1,2],
        ///         [3,4]],
        ///        [[2,2],
        ///         [5,6]]]
        /// 
        ///   L2Normalization(x, mode='instance')
        ///   =[[[ 0.18257418  0.36514837]
        ///      [ 0.54772252  0.73029673]]
        ///     [[ 0.24077171  0.24077171]
        ///      [ 0.60192931  0.72231513]]]
        /// 
        ///   L2Normalization(x, mode='channel')
        ///   =[[[ 0.31622776  0.44721359]
        ///      [ 0.94868326  0.89442718]]
        ///     [[ 0.37139067  0.31622776]
        ///      [ 0.92847669  0.94868326]]]
        /// 
        ///   L2Normalization(x, mode='spatial')
        ///   =[[[ 0.44721359  0.89442718]
        ///      [ 0.60000002  0.80000001]]
        ///     [[ 0.70710677  0.70710677]
        ///      [ 0.6401844   0.76822126]]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\l2_normalization.cc:L196
        /// </summary>
        /// <param name="data">Input array to normalize.</param>
        /// <param name="eps">A small constant for numerical stability.</param>
        /// <param name="mode">Specify the dimension along which to compute L2 norm.</param>
        public static NDArray L2Normalization(NDArrayOrSymbol data, double eps = 1.00000001e-10, string mode = "instance", NDArray output = null)
        {
            var result = Operator.Invoke(
                "L2Normalization",
                _L2NormalizationParamNames,
                new[] { Convert(eps), Convert(mode) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardL2NormalizationParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardL2Normalization(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_L2Normalization",
                _backwardL2NormalizationParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardLeakyReLUParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardLeakyReLU(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_LeakyReLU",
                _backwardLeakyReLUParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardMakeLossParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardMakeLoss(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_MakeLoss",
                _backwardMakeLossParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardPadParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardPad(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Pad",
                _backwardPadParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _PoolingV1ParamNames = new[] { "kernel", "pool_type", "global_pool", "pooling_convention", "stride", "pad" };

        /// <summary>
        /// This operator is DEPRECATED.
        /// Perform pooling on the input.
        /// 
        /// The shapes for 2-D pooling is
        /// 
        /// - **data**: *(batch_size, channel, height, width)*
        /// - **out**: *(batch_size, num_filter, out_height, out_width)*, with::
        /// 
        ///     out_height = f(height, kernel[0], pad[0], stride[0])
        ///     out_width = f(width, kernel[1], pad[1], stride[1])
        /// 
        /// The definition of *f* depends on ``pooling_convention``, which has two options:
        /// 
        /// - **valid** (default)::
        /// 
        ///     f(x, k, p, s) = floor((x+2*p-k)/s)+1
        /// 
        /// - **full**, which is compatible with Caffe::
        /// 
        ///     f(x, k, p, s) = ceil((x+2*p-k)/s)+1
        /// 
        /// But ``global_pool`` is set to be true, then do a global pooling, namely reset
        /// ``kernel=(height, width)``.
        /// 
        /// Three pooling options are supported by ``pool_type``:
        /// 
        /// - **avg**: average pooling
        /// - **max**: max pooling
        /// - **sum**: sum pooling
        /// 
        /// 1-D pooling is special case of 2-D pooling with *weight=1* and
        /// *kernel[1]=1*.
        /// 
        /// For 3-D pooling, an additional *depth* dimension is added before
        /// *height*. Namely the input data will have shape *(batch_size, channel, depth,
        /// height, width)*.
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\pooling_v1.cc:L104
        /// </summary>
        /// <param name="data">Input data to the pooling operator.</param>
        /// <param name="kernel">pooling kernel size: (y, x) or (d, y, x)</param>
        /// <param name="pool_type">Pooling type to be applied.</param>
        /// <param name="global_pool">Ignore kernel size, do global pooling based on current input feature map. </param>
        /// <param name="pooling_convention">Pooling convention to be applied.</param>
        /// <param name="stride">stride: for pooling (y, x) or (d, y, x)</param>
        /// <param name="pad">pad for pooling: (y, x) or (d, y, x)</param>
        public static NDArray PoolingV1(NDArrayOrSymbol data, NDShape kernel = null, string poolType = "max", bool globalPool = false, string poolingConvention = "valid", NDShape stride = null, NDShape pad = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "Pooling_v1",
                _PoolingV1ParamNames,
                new[] { Convert(kernel), Convert(poolType), Convert(globalPool), Convert(poolingConvention), Convert(stride), Convert(pad) },
                new[] { data.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardPoolingV1ParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardPoolingV1(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_Pooling_v1",
                _backwardPoolingV1ParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _ROIPoolingParamNames = new[] { "pooled_size", "spatial_scale" };

        /// <summary>
        /// Performs region of interest(ROI) pooling on the input array.
        /// 
        /// ROI pooling is a variant of a max pooling layer, in which the output size is fixed and
        /// region of interest is a parameter. Its purpose is to perform max pooling on the inputs
        /// of non-uniform sizes to obtain fixed-size feature maps. ROI pooling is a neural-net
        /// layer mostly used in training a `Fast R-CNN` network for object detection.
        /// 
        /// This operator takes a 4D feature map as an input array and region proposals as `rois`,
        /// then it pools over sub-regions of input and produces a fixed-sized output array
        /// regardless of the ROI size.
        /// 
        /// To crop the feature map accordingly, you can resize the bounding box coordinates
        /// by changing the parameters `rois` and `spatial_scale`.
        /// 
        /// The cropped feature maps are pooled by standard max pooling operation to a fixed size output
        /// indicated by a `pooled_size` parameter. batch_size will change to the number of region
        /// bounding boxes after `ROIPooling`.
        /// 
        /// The size of each region of interest doesn't have to be perfectly divisible by
        /// the number of pooling sections(`pooled_size`).
        /// 
        /// Example::
        /// 
        ///   x = [[[[  0.,   1.,   2.,   3.,   4.,   5.],
        ///          [  6.,   7.,   8.,   9.,  10.,  11.],
        ///          [ 12.,  13.,  14.,  15.,  16.,  17.],
        ///          [ 18.,  19.,  20.,  21.,  22.,  23.],
        ///          [ 24.,  25.,  26.,  27.,  28.,  29.],
        ///          [ 30.,  31.,  32.,  33.,  34.,  35.],
        ///          [ 36.,  37.,  38.,  39.,  40.,  41.],
        ///          [ 42.,  43.,  44.,  45.,  46.,  47.]]]]
        /// 
        ///   // region of interest i.e. bounding box coordinates.
        ///   y = [[0,0,0,4,4]]
        /// 
        ///   // returns array of shape (2,2) according to the given roi with max pooling.
        ///   ROIPooling(x, y, (2,2), 1.0) = [[[[ 14.,  16.],
        ///                                     [ 26.,  28.]]]]
        /// 
        ///   // region of interest is changed due to the change in `spacial_scale` parameter.
        ///   ROIPooling(x, y, (2,2), 0.7) = [[[[  7.,   9.],
        ///                                     [ 19.,  21.]]]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\roi_pooling.cc:L295
        /// </summary>
        /// <param name="data">The input array to the pooling operator,  a 4D Feature maps </param>
        /// <param name="rois">Bounding box coordinates, a 2D array of [[batch_index, x1, y1, x2, y2]], where (x1, y1) and (x2, y2) are top left and bottom right corners of designated region of interest. `batch_index` indicates the index of corresponding image in the input array</param>
        /// <param name="pooled_size">ROI pooling output shape (h,w) </param>
        /// <param name="spatial_scale">Ratio of input feature map height (or w) to raw image height (or w). Equals the reciprocal of total stride in convolutional layers</param>
        public static NDArray ROIPooling(NDArrayOrSymbol data, NDArrayOrSymbol rois, NDShape pooledSize, double spatialScale, NDArray output = null)
        {
            var result = Operator.Invoke(
                "ROIPooling",
                _ROIPoolingParamNames,
                new[] { Convert(pooledSize), Convert(spatialScale) },
                new[] { data.Handle, rois.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardROIPoolingParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardROIPooling(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_ROIPooling",
                _backwardROIPoolingParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _SequenceLastParamNames = new[] { "use_sequence_length", "axis" };

        /// <summary>
        /// Takes the last element of a sequence.
        /// 
        /// This function takes an n-dimensional input array of the form
        /// [max_sequence_length, batch_size, other_feature_dims] and returns a (n-1)-dimensional array
        /// of the form [batch_size, other_feature_dims].
        /// 
        /// Parameter `sequence_length` is used to handle variable-length sequences. `sequence_length` should be
        /// an input array of positive ints of dimension [batch_size]. To use this parameter,
        /// set `use_sequence_length` to `True`, otherwise each example in the batch is assumed
        /// to have the max sequence length.
        /// 
        /// .. note:: Alternatively, you can also use `take` operator.
        /// 
        /// Example::
        /// 
        ///    x = [[[  1.,   2.,   3.],
        ///          [  4.,   5.,   6.],
        ///          [  7.,   8.,   9.]],
        /// 
        ///         [[ 10.,   11.,   12.],
        ///          [ 13.,   14.,   15.],
        ///          [ 16.,   17.,   18.]],
        /// 
        ///         [[  19.,   20.,   21.],
        ///          [  22.,   23.,   24.],
        ///          [  25.,   26.,   27.]]]
        /// 
        ///    // returns last sequence when sequence_length parameter is not used
        ///    SequenceLast(x) = [[  19.,   20.,   21.],
        ///                       [  22.,   23.,   24.],
        ///                       [  25.,   26.,   27.]]
        /// 
        ///    // sequence_length is used
        ///    SequenceLast(x, sequence_length=[1,1,1], use_sequence_length=True) =
        ///             [[  1.,   2.,   3.],
        ///              [  4.,   5.,   6.],
        ///              [  7.,   8.,   9.]]
        /// 
        ///    // sequence_length is used
        ///    SequenceLast(x, sequence_length=[1,2,3], use_sequence_length=True) =
        ///             [[  1.,    2.,   3.],
        ///              [  13.,  14.,  15.],
        ///              [  25.,  26.,  27.]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\sequence_last.cc:L100
        /// </summary>
        /// <param name="data">n-dimensional input array of the form [max_sequence_length, batch_size, other_feature_dims] where n>2</param>
        /// <param name="sequence_length">vector of sequence lengths of the form [batch_size]</param>
        /// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
        /// <param name="axis">The sequence axis. Only values of 0 and 1 are currently supported.</param>
        public static NDArray SequenceLast(NDArrayOrSymbol data, NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SequenceLast",
                _SequenceLastParamNames,
                new[] { Convert(useSequenceLength), Convert(axis) },
                new[] { data.Handle, sequenceLength.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSequenceLastParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSequenceLast(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SequenceLast",
                _backwardSequenceLastParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _SequenceMaskParamNames = new[] { "use_sequence_length", "value", "axis" };

        /// <summary>
        /// Sets all elements outside the sequence to a constant value.
        /// 
        /// This function takes an n-dimensional input array of the form
        /// [max_sequence_length, batch_size, other_feature_dims] and returns an array of the same shape.
        /// 
        /// Parameter `sequence_length` is used to handle variable-length sequences. `sequence_length`
        /// should be an input array of positive ints of dimension [batch_size].
        /// To use this parameter, set `use_sequence_length` to `True`,
        /// otherwise each example in the batch is assumed to have the max sequence length and
        /// this operator works as the `identity` operator.
        /// 
        /// Example::
        /// 
        ///    x = [[[  1.,   2.,   3.],
        ///          [  4.,   5.,   6.]],
        /// 
        ///         [[  7.,   8.,   9.],
        ///          [ 10.,  11.,  12.]],
        /// 
        ///         [[ 13.,  14.,   15.],
        ///          [ 16.,  17.,   18.]]]
        /// 
        ///    // Batch 1
        ///    B1 = [[  1.,   2.,   3.],
        ///          [  7.,   8.,   9.],
        ///          [ 13.,  14.,  15.]]
        /// 
        ///    // Batch 2
        ///    B2 = [[  4.,   5.,   6.],
        ///          [ 10.,  11.,  12.],
        ///          [ 16.,  17.,  18.]]
        /// 
        ///    // works as identity operator when sequence_length parameter is not used
        ///    SequenceMask(x) = [[[  1.,   2.,   3.],
        ///                        [  4.,   5.,   6.]],
        /// 
        ///                       [[  7.,   8.,   9.],
        ///                        [ 10.,  11.,  12.]],
        /// 
        ///                       [[ 13.,  14.,   15.],
        ///                        [ 16.,  17.,   18.]]]
        /// 
        ///    // sequence_length [1,1] means 1 of each batch will be kept
        ///    // and other rows are masked with default mask value = 0
        ///    SequenceMask(x, sequence_length=[1,1], use_sequence_length=True) =
        ///                 [[[  1.,   2.,   3.],
        ///                   [  4.,   5.,   6.]],
        /// 
        ///                  [[  0.,   0.,   0.],
        ///                   [  0.,   0.,   0.]],
        /// 
        ///                  [[  0.,   0.,   0.],
        ///                   [  0.,   0.,   0.]]]
        /// 
        ///    // sequence_length [2,3] means 2 of batch B1 and 3 of batch B2 will be kept
        ///    // and other rows are masked with value = 1
        ///    SequenceMask(x, sequence_length=[2,3], use_sequence_length=True, value=1) =
        ///                 [[[  1.,   2.,   3.],
        ///                   [  4.,   5.,   6.]],
        /// 
        ///                  [[  7.,   8.,   9.],
        ///                   [  10.,  11.,  12.]],
        /// 
        ///                  [[   1.,   1.,   1.],
        ///                   [  16.,  17.,  18.]]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\sequence_mask.cc:L186
        /// </summary>
        /// <param name="data">n-dimensional input array of the form [max_sequence_length, batch_size, other_feature_dims] where n>2</param>
        /// <param name="sequence_length">vector of sequence lengths of the form [batch_size]</param>
        /// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
        /// <param name="value">The value to be used as a mask.</param>
        /// <param name="axis">The sequence axis. Only values of 0 and 1 are currently supported.</param>
        public static NDArray SequenceMask(NDArrayOrSymbol data, NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, double value = 0, int axis = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SequenceMask",
                _SequenceMaskParamNames,
                new[] { Convert(useSequenceLength), Convert(value), Convert(axis) },
                new[] { data.Handle, sequenceLength.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSequenceMaskParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSequenceMask(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SequenceMask",
                _backwardSequenceMaskParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _SequenceReverseParamNames = new[] { "use_sequence_length", "axis" };

        /// <summary>
        /// Reverses the elements of each sequence.
        /// 
        /// This function takes an n-dimensional input array of the form [max_sequence_length, batch_size, other_feature_dims]
        /// and returns an array of the same shape.
        /// 
        /// Parameter `sequence_length` is used to handle variable-length sequences.
        /// `sequence_length` should be an input array of positive ints of dimension [batch_size].
        /// To use this parameter, set `use_sequence_length` to `True`,
        /// otherwise each example in the batch is assumed to have the max sequence length.
        /// 
        /// Example::
        /// 
        ///    x = [[[  1.,   2.,   3.],
        ///          [  4.,   5.,   6.]],
        /// 
        ///         [[  7.,   8.,   9.],
        ///          [ 10.,  11.,  12.]],
        /// 
        ///         [[ 13.,  14.,   15.],
        ///          [ 16.,  17.,   18.]]]
        /// 
        ///    // Batch 1
        ///    B1 = [[  1.,   2.,   3.],
        ///          [  7.,   8.,   9.],
        ///          [ 13.,  14.,  15.]]
        /// 
        ///    // Batch 2
        ///    B2 = [[  4.,   5.,   6.],
        ///          [ 10.,  11.,  12.],
        ///          [ 16.,  17.,  18.]]
        /// 
        ///    // returns reverse sequence when sequence_length parameter is not used
        ///    SequenceReverse(x) = [[[ 13.,  14.,   15.],
        ///                           [ 16.,  17.,   18.]],
        /// 
        ///                          [[  7.,   8.,   9.],
        ///                           [ 10.,  11.,  12.]],
        /// 
        ///                          [[  1.,   2.,   3.],
        ///                           [  4.,   5.,   6.]]]
        /// 
        ///    // sequence_length [2,2] means 2 rows of
        ///    // both batch B1 and B2 will be reversed.
        ///    SequenceReverse(x, sequence_length=[2,2], use_sequence_length=True) =
        ///                      [[[  7.,   8.,   9.],
        ///                        [ 10.,  11.,  12.]],
        /// 
        ///                       [[  1.,   2.,   3.],
        ///                        [  4.,   5.,   6.]],
        /// 
        ///                       [[ 13.,  14.,   15.],
        ///                        [ 16.,  17.,   18.]]]
        /// 
        ///    // sequence_length [2,3] means 2 of batch B2 and 3 of batch B3
        ///    // will be reversed.
        ///    SequenceReverse(x, sequence_length=[2,3], use_sequence_length=True) =
        ///                     [[[  7.,   8.,   9.],
        ///                       [ 16.,  17.,  18.]],
        /// 
        ///                      [[  1.,   2.,   3.],
        ///                       [ 10.,  11.,  12.]],
        /// 
        ///                      [[ 13.,  14,   15.],
        ///                       [  4.,   5.,   6.]]]
        /// 
        /// 
        /// 
        /// Defined in C:\Jenkins\workspace\mxnet-tag\mxnet\src\operator\sequence_reverse.cc:L122
        /// </summary>
        /// <param name="data">n-dimensional input array of the form [max_sequence_length, batch_size, other dims] where n>2 </param>
        /// <param name="sequence_length">vector of sequence lengths of the form [batch_size]</param>
        /// <param name="use_sequence_length">If set to true, this layer takes in an extra input parameter `sequence_length` to specify variable length sequence</param>
        /// <param name="axis">The sequence axis. Only 0 is currently supported.</param>
        public static NDArray SequenceReverse(NDArrayOrSymbol data, NDArrayOrSymbol sequenceLength, bool useSequenceLength = false, int axis = 0, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SequenceReverse",
                _SequenceReverseParamNames,
                new[] { Convert(useSequenceLength), Convert(axis) },
                new[] { data.Handle, sequenceLength.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSequenceReverseParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSequenceReverse(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SequenceReverse",
                _backwardSequenceReverseParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardSliceChannelParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSliceChannel(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SliceChannel",
                _backwardSliceChannelParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _SpatialTransformerParamNames = new[] { "target_shape", "transform_type", "sampler_type", "cudnn_off" };

        /// <summary>
        /// Applies a spatial transformer to input feature map.
        /// </summary>
        /// <param name="data">Input data to the SpatialTransformerOp.</param>
        /// <param name="loc">localisation net, the output dim should be 6 when transform_type is affine. You shold initialize the weight and bias with identity tranform.</param>
        /// <param name="target_shape">output shape(h, w) of spatial transformer: (y, x)</param>
        /// <param name="transform_type">transformation type</param>
        /// <param name="sampler_type">sampling type</param>
        /// <param name="cudnn_off">whether to turn cudnn off</param>
        public static NDArray SpatialTransformer(NDArrayOrSymbol data, NDArrayOrSymbol loc, string transformType, string samplerType, NDShape targetShape = null, bool? cudnnOff = null, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SpatialTransformer",
                _SpatialTransformerParamNames,
                new[] { Convert(targetShape), Convert(transformType), Convert(samplerType), Convert(cudnnOff) },
                new[] { data.Handle, loc.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSpatialTransformerParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSpatialTransformer(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SpatialTransformer",
                _backwardSpatialTransformerParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _SVMOutputParamNames = new[] { "margin", "regularization_coefficient", "use_linear" };

        /// <summary>
        /// Computes support vector machine based transformation of the input.
        /// 
        /// This tutorial demonstrates using SVM as output layer for classification instead of softmax:
        /// https://github.com/dmlc/mxnet/tree/master/example/svm_mnist.
        /// 
        /// 
        /// </summary>
        /// <param name="data">Input data for SVM transformation.</param>
        /// <param name="label">Class label for the input data.</param>
        /// <param name="margin">The loss function penalizes outputs that lie outside this margin. Default margin is 1.</param>
        /// <param name="regularization_coefficient">Regularization parameter for the SVM. This balances the tradeoff between coefficient size and error.</param>
        /// <param name="use_linear">Whether to use L1-SVM objective. L2-SVM objective is used by default.</param>
        public static NDArray SVMOutput(NDArrayOrSymbol data, NDArrayOrSymbol label, double margin = 1, double regularizationCoefficient = 1, bool useLinear = false, NDArray output = null)
        {
            var result = Operator.Invoke(
                "SVMOutput",
                _SVMOutputParamNames,
                new[] { Convert(margin), Convert(regularizationCoefficient), Convert(useLinear) },
                new[] { data.Handle, label.Handle },
                output
            );
            return result;
        }

        private static string[] _backwardSVMOutputParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSVMOutput(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SVMOutput",
                _backwardSVMOutputParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _backwardSwapAxisParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        public static NDArray BackwardSwapAxis(NDArray output = null)
        {
            var result = Operator.Invoke(
                "_backward_SwapAxis",
                _backwardSwapAxisParamNames,
                Empty,
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _setValueParamNames = new[] { "src" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src">Source input to the function.</param>
        public static NDArray SetValue(double src, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_set_value",
                _setValueParamNames,
                new[] { Convert(src) },
                EmptyInput,
                output
            );
            return result;
        }

        private static string[] _onehotEncodeParamNames = Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">Left operand to the function.</param>
        /// <param name="rhs">Right operand to the function.</param>
        public static NDArray OnehotEncode(NDArray lhs, NDArray rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_onehot_encode",
                _onehotEncodeParamNames,
                Empty,
                new[] { lhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _fillElement0indexParamNames = Empty;

        /// <summary>
        /// Fill one element of each line(row for python, column for R/Julia) in lhs according to index indicated by rhs and values indicated by mhs. This function assume rhs uses 0-based index.
        /// </summary>
        /// <param name="lhs">Left operand to the function.</param>
        /// <param name="mhs">Middle operand to the function.</param>
        /// <param name="rhs">Right operand to the function.</param>
        public static NDArray FillElement0index(NDArray lhs, NDArray mhs, NDArray rhs, NDArray output = null)
        {
            var result = Operator.Invoke(
                "fill_element_0index",
                _fillElement0indexParamNames,
                Empty,
                new[] { lhs.Handle, mhs.Handle, rhs.Handle },
                output
            );
            return result;
        }

        private static string[] _imdecodeParamNames = new[] { "index", "x0", "y0", "x1", "y1", "c", "size" };

        /// <summary>
        /// Decode an image, clip to (x0, y0, x1, y1), subtract mean, and write to buffer
        /// </summary>
        /// <param name="mean">image mean</param>
        /// <param name="index">buffer position for output</param>
        /// <param name="x0">x0</param>
        /// <param name="y0">y0</param>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="c">channel</param>
        /// <param name="size">length of str_img</param>
        public static NDArray Imdecode(NDArrayOrSymbol mean, int index, int x0, int y0, int x1, int y1, int c, int size, NDArray output = null)
        {
            var result = Operator.Invoke(
                "_imdecode",
                _imdecodeParamNames,
                new[] { Convert(index), Convert(x0), Convert(y0), Convert(x1), Convert(y1), Convert(c), Convert(size) },
                new[] { mean.Handle },
                output
            );
            return result;
        }

    }
}
