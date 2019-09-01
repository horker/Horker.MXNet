using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Core;

namespace Horker.MXNet.Operators
{
    public class OperatorsBase
    {
        internal static readonly string[] Empty = new string[0];
        internal static readonly IntPtr[] EmptyInput = new IntPtr[0];

        internal static string Convert(object value)
        {
            throw new NotImplementedException("Parameter of object type is not implemented");
        }

        internal static string Convert(double value)
        {
            return value.ToString();
        }

        internal static string Convert(double? value)
        {
            return value.Value.ToString();
        }

        internal static string Convert(float value)
        {
            return value.ToString();
        }

        internal static string Convert(float? value)
        {
            return value.Value.ToString();
        }

        internal static string Convert(string value)
        {
            return value;
        }

        internal static string Convert(int value)
        {
            return value.ToString();
        }

        internal static string Convert(bool value)
        {
            return value ? "1" : "0";
        }

        internal static string Convert(bool? value)
        {
            return value.HasValue ? (value.Value ? "1" : "0") : "0";
        }

        internal static string Convert(Tuple<double> value)
        {
            return value.Item1.ToString();
        }

        internal static string Convert(Context value)
        {
            if (value == null)
                value = Context.DefaultContext;

            return value.ToString();
        }

        internal static string Convert(DType value)
        {
            if (value == null)
                value = DType.Float16;

            return value.ToString();
        }

        internal static string Convert(NDShape value)
        {
            if (value == null)
                return "()";

            return value.ToString();
        }
    }
}
