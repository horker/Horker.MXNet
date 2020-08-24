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
    [Cmdlet("Get", "MxRandomUniformNDArray")]
    [Alias("mx.nd.random.Uniform")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomUniformNDArray : PSCmdlet
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

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.Uniform(Low, High, Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxRandomNormalNDArray")]
    [Alias("mx.nd.random.Normal")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomNormalNDArray : PSCmdlet
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

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.Normal(Loc, Scale, Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxRandomGammaNDArray")]
    [Alias("mx.nd.random.Gamma")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomGammaNDArray : PSCmdlet
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

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.Gamma(Alpha, Beta, Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxRandomExponentialNDArray")]
    [Alias("mx.nd.random.Exponential")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomExponentialNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Lam { get; set; } = 1f;

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.Exponential(Lam, Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxRandomPoissonNDArray")]
    [Alias("mx.nd.random.Poisson")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomPoissonNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public float Lam { get; set; } = 1f;

        [Parameter(Position = 1, Mandatory = false)]
        public Shape Shape { get; set; } = null;

        [Parameter(Position = 2, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        [Parameter(Position = 3, Mandatory = false)]
        public DType Dtype { get; set; } = null;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.Poisson(Lam, Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxRandomNegativeBinomialNDArray")]
    [Alias("mx.nd.random.NegativeBinomial")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomNegativeBinomialNDArray : PSCmdlet
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

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.NegativeBinomial(K, P, Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxRandomGeneralizedNegativeBinomialNDArray")]
    [Alias("mx.nd.random.GeneralizedNegativeBinomial")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomGeneralizedNegativeBinomialNDArray : PSCmdlet
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

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.GeneralizedNegativeBinomial(Mu, Alpha, Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxRandomRandintNDArray")]
    [Alias("mx.nd.random.Randint")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomRandintNDArray : PSCmdlet
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

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.Randint(Low, High, Shape, Ctx, Dtype));
        }
    }

    [Cmdlet("Get", "MxRandomUniformLikeNDArray")]
    [Alias("mx.nd.random.UniformLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomUniformLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Low { get; set; } = 0f;

        [Parameter(Position = 2, Mandatory = false)]
        public float High { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.UniformLike(Data, Low, High));
        }
    }

    [Cmdlet("Get", "MxRandomNormalLikeNDArray")]
    [Alias("mx.nd.random.NormalLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomNormalLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Loc { get; set; } = 0f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Scale { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.NormalLike(Data, Loc, Scale));
        }
    }

    [Cmdlet("Get", "MxRandomGammaLikeNDArray")]
    [Alias("mx.nd.random.GammaLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomGammaLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Alpha { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Beta { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.GammaLike(Data, Alpha, Beta));
        }
    }

    [Cmdlet("Get", "MxRandomExponentialLikeNDArray")]
    [Alias("mx.nd.random.ExponentialLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomExponentialLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Lam { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.ExponentialLike(Data, Lam));
        }
    }

    [Cmdlet("Get", "MxRandomPoissonLikeNDArray")]
    [Alias("mx.nd.random.PoissonLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomPoissonLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Lam { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.PoissonLike(Data, Lam));
        }
    }

    [Cmdlet("Get", "MxRandomNegativeBinomialLikeNDArray")]
    [Alias("mx.nd.random.NegativeBinomialLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomNegativeBinomialLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public int K { get; set; } = 1;

        [Parameter(Position = 2, Mandatory = false)]
        public float P { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.NegativeBinomialLike(Data, K, P));
        }
    }

    [Cmdlet("Get", "MxRandomGeneralizedNegativeBinomialLikeNDArray")]
    [Alias("mx.nd.random.GeneralizedNegativeBinomialLike")]
    [OutputType(typeof(NDArray))]
    public class GetMxRandomGeneralizedNegativeBinomialLikeNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public NDArray Data { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public float Mu { get; set; } = 1f;

        [Parameter(Position = 2, Mandatory = false)]
        public float Alpha { get; set; } = 1f;

        protected override void BeginProcessing()
        {
            WriteObject(global::MxNet.nd.Random.GeneralizedNegativeBinomialLike(Data, Mu, Alpha));
        }
    }

    [Cmdlet("Set", "MxRandomSeedNDArray")]
    [Alias("mx.nd.random.Seed")]
    [OutputType(typeof(void))]
    public class SetMxRandomSeedNDArray : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public int Seed { get; set; }

        [Parameter(Position = 1, Mandatory = false)]
        public Context Ctx { get; set; } = null;

        protected override void BeginProcessing()
        {
            global::MxNet.nd.Random.Seed(Seed, Ctx);
        }
    }
}
