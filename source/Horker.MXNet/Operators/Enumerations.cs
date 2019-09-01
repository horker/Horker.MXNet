using System;

namespace Horker.MXNet.Operators
{
    public class LeakyReLUActType
    {
        private static readonly string[] _values = new[] {
            "elu", "gelu", "leaky", "prelu", "rrelu", "selu"
        };

        public static LeakyReLUActType ELU { get; private set; }
        public static LeakyReLUActType GeLU { get; private set; }
        public static LeakyReLUActType Leaky { get; private set; }
        public static LeakyReLUActType PReLU { get; private set; }
        public static LeakyReLUActType RReLU { get; private set; }
        public static LeakyReLUActType SeLU { get; private set; }

        public static LeakyReLUActType DefaultValue { get; private set; }

        private string _value;

        static LeakyReLUActType()
        {
            ELU = new LeakyReLUActType() { _value = _values[0] };
            GeLU = new LeakyReLUActType() { _value = _values[1] };
            Leaky = new LeakyReLUActType() { _value = _values[2] };
            PReLU = new LeakyReLUActType() { _value = _values[3] };
            RReLU = new LeakyReLUActType() { _value = _values[4] };
            SeLU = new LeakyReLUActType() { _value = _values[5] };

            DefaultValue = new LeakyReLUActType() { _value = "leaky" };
        }

        private LeakyReLUActType()
        {
        }

        public LeakyReLUActType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(LeakyReLUActType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class ActType
    {
        private static readonly string[] _values = new[] {
            "relu", "sigmoid", "softrelu", "softsign", "tanh"
        };

        public static ActType ReLU { get; private set; }
        public static ActType sigmoid { get; private set; }
        public static ActType SoftReLU { get; private set; }
        public static ActType SoftSign { get; private set; }
        public static ActType Tanh { get; private set; }

        public static ActType DefaultValue { get; private set; }

        private string _value;

        static ActType()
        {
            ReLU = new ActType() { _value = _values[0] };
            sigmoid = new ActType() { _value = _values[1] };
            SoftReLU = new ActType() { _value = _values[2] };
            SoftSign = new ActType() { _value = _values[3] };
            Tanh = new ActType() { _value = _values[4] };

            DefaultValue = new ActType() { _value = "relu" };
        }

        private ActType()
        {
        }

        public ActType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(ActType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class BlankLabelType
    {
        private static readonly string[] _values = new[] {
            "first", "last"
        };

        public static BlankLabelType First { get; private set; }
        public static BlankLabelType Last { get; private set; }

        public static BlankLabelType DefaultValue { get; private set; }

        private string _value;

        static BlankLabelType()
        {
            First = new BlankLabelType() { _value = _values[0] };
            Last = new BlankLabelType() { _value = _values[1] };

            DefaultValue = new BlankLabelType() { _value = "first" };
        }

        private BlankLabelType()
        {
        }

        public BlankLabelType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(BlankLabelType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class CuDNNTuneType
    {
        private static readonly string[] _values = new[] {
            "fastest", "limited_workspace", "off"
        };

        public static CuDNNTuneType Fastest { get; private set; }
        public static CuDNNTuneType LimitedWorkspace { get; private set; }
        public static CuDNNTuneType Off { get; private set; }

        public static CuDNNTuneType DefaultValue { get; private set; }

        private string _value;

        static CuDNNTuneType()
        {
            Fastest = new CuDNNTuneType() { _value = _values[0] };
            LimitedWorkspace = new CuDNNTuneType() { _value = _values[1] };
            Off = new CuDNNTuneType() { _value = _values[2] };

            DefaultValue = new CuDNNTuneType() { _value = null };
        }

        private CuDNNTuneType()
        {
        }

        public CuDNNTuneType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(CuDNNTuneType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class FormatType
    {
        private static readonly string[] _values = new[] {
            "center", "corner"
        };

        public static FormatType Center { get; private set; }
        public static FormatType Corner { get; private set; }

        public static FormatType DefaultValue { get; private set; }

        private string _value;

        static FormatType()
        {
            Center = new FormatType() { _value = _values[0] };
            Corner = new FormatType() { _value = _values[1] };

            DefaultValue = new FormatType() { _value = "corner" };
        }

        private FormatType()
        {
        }

        public FormatType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(FormatType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class SType
    {
        private static readonly string[] _values = new[] {
            "csr", "default", "row_sparse"
        };

        public static SType Csr { get; private set; }
        public static SType Default { get; private set; }
        public static SType RowSparse { get; private set; }

        public static SType DefaultValue { get; private set; }

        private string _value;

        static SType()
        {
            Csr = new SType() { _value = _values[0] };
            Default = new SType() { _value = _values[1] };
            RowSparse = new SType() { _value = _values[2] };

            DefaultValue = new SType() { _value = null };
        }

        private SType()
        {
        }

        public SType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(SType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class LayoutType
    {
        private static readonly string[] _values = new[] {
            "NCDHW", "NCHW", "NCW", "NDHWC", "NHWC", "NWC"
        };

        public static LayoutType NCDHW { get; private set; }
        public static LayoutType NCHW { get; private set; }
        public static LayoutType NCW { get; private set; }
        public static LayoutType NDHWC { get; private set; }
        public static LayoutType NHWC { get; private set; }
        public static LayoutType NWC { get; private set; }

        public static LayoutType DefaultValue { get; private set; }

        private string _value;

        static LayoutType()
        {
            NCDHW = new LayoutType() { _value = _values[0] };
            NCHW = new LayoutType() { _value = _values[1] };
            NCW = new LayoutType() { _value = _values[2] };
            NDHWC = new LayoutType() { _value = _values[3] };
            NHWC = new LayoutType() { _value = _values[4] };
            NWC = new LayoutType() { _value = _values[5] };

            DefaultValue = new LayoutType() { _value = null };
        }

        private LayoutType()
        {
        }

        public LayoutType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(LayoutType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class MultiInputModeType
    {
        private static readonly string[] _values = new[] {
            "concat", "sum"
        };

        public static MultiInputModeType Concat { get; private set; }
        public static MultiInputModeType Sum { get; private set; }

        public static MultiInputModeType DefaultValue { get; private set; }

        private string _value;

        static MultiInputModeType()
        {
            Concat = new MultiInputModeType() { _value = _values[0] };
            Sum = new MultiInputModeType() { _value = _values[1] };

            DefaultValue = new MultiInputModeType() { _value = "Concat" };
        }

        private MultiInputModeType()
        {
        }

        public MultiInputModeType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(MultiInputModeType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class NormalizationType
    {
        private static readonly string[] _values = new[] {
            "batch", "null", "valid"
        };

        public static NormalizationType Batch { get; private set; }
        public static NormalizationType Null { get; private set; }
        public static NormalizationType Valid { get; private set; }

        public static NormalizationType DefaultValue { get; private set; }

        private string _value;

        static NormalizationType()
        {
            Batch = new NormalizationType() { _value = _values[0] };
            Null = new NormalizationType() { _value = _values[1] };
            Valid = new NormalizationType() { _value = _values[2] };

            DefaultValue = new NormalizationType() { _value = "null" };
        }

        private NormalizationType()
        {
        }

        public NormalizationType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(NormalizationType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class PoolType
    {
        private static readonly string[] _values = new[] {
            "avg", "lp", "max", "sum"
        };

        public static PoolType Avg { get; private set; }
        public static PoolType Lp { get; private set; }
        public static PoolType Max { get; private set; }
        public static PoolType Sum { get; private set; }

        public static PoolType DefaultValue { get; private set; }

        private string _value;

        static PoolType()
        {
            Avg = new PoolType() { _value = _values[0] };
            Lp = new PoolType() { _value = _values[1] };
            Max = new PoolType() { _value = _values[2] };
            Sum = new PoolType() { _value = _values[3] };

            DefaultValue = new PoolType() { _value = "Max" };
        }

        private PoolType()
        {
        }

        public PoolType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(PoolType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class PoolingConventionType
    {
        private static readonly string[] _values = new[] {
            "full", "same", "valid"
        };

        public static PoolingConventionType Full { get; private set; }
        public static PoolingConventionType Same { get; private set; }
        public static PoolingConventionType Valid { get; private set; }

        public static PoolingConventionType DefaultValue { get; private set; }

        private string _value;

        static PoolingConventionType()
        {
            Full = new PoolingConventionType() { _value = _values[0] };
            Same = new PoolingConventionType() { _value = _values[1] };
            Valid = new PoolingConventionType() { _value = _values[2] };

            DefaultValue = new PoolingConventionType() { _value = "Valid" };
        }

        private PoolingConventionType()
        {
        }

        public PoolingConventionType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(PoolingConventionType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class RetType
    {
        private static readonly string[] _values = new[] {
            "both", "indices", "mask", "value"
        };

        public static RetType Both { get; private set; }
        public static RetType Indices { get; private set; }
        public static RetType Mask { get; private set; }
        public static RetType Value { get; private set; }

        public static RetType DefaultValue { get; private set; }

        private string _value;

        static RetType()
        {
            Both = new RetType() { _value = _values[0] };
            Indices = new RetType() { _value = _values[1] };
            Mask = new RetType() { _value = _values[2] };
            Value = new RetType() { _value = _values[3] };

            DefaultValue = new RetType() { _value = "Indices" };
        }

        private RetType()
        {
        }

        public RetType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(RetType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class SampleType
    {
        private static readonly string[] _values = new[] {
            "bilinear", "nearest"
        };

        public static SampleType Bilinear { get; private set; }
        public static SampleType Nearest { get; private set; }

        public static SampleType DefaultValue { get; private set; }

        private string _value;

        static SampleType()
        {
            Bilinear = new SampleType() { _value = _values[0] };
            Nearest = new SampleType() { _value = _values[1] };

            DefaultValue = new SampleType() { _value = null };
        }

        private SampleType()
        {
        }

        public SampleType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(SampleType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

    public class TransformType
    {
        private static readonly string[] _values = new[] {
            "affine", "warp"
        };

        public static TransformType Affine { get; private set; }
        public static TransformType Warp { get; private set; }

        public static TransformType DefaultValue { get; private set; }

        private string _value;

        static TransformType()
        {
            Affine = new TransformType() { _value = _values[0] };
            Warp = new TransformType() { _value = _values[1] };

            DefaultValue = new TransformType() { _value = null };
        }

        private TransformType()
        {
        }

        public TransformType(string s)
        {
            for (var i = 0; i < _values.Length; ++i)
            {
                if (s.Equals(_values[i], StringComparison.OrdinalIgnoreCase))
                {
                    _value = _values[i];
                    return;
                }
            }

            throw new ArgumentException($"Invalid type string: {s}");
        }

        public static implicit operator string(TransformType obj)
        {
            return obj._value ?? DefaultValue._value;
        }
    }

}
