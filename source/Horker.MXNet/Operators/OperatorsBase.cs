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
        protected static string[] _empty = new string[0];
        protected static NDArrayOrSymbol[] _emptyInput = new NDArrayOrSymbol[0];

        public static string Convert(double value)
        {
            return value.ToString();
        }

        public static string Convert(double? value)
        {
            return value.Value.ToString();
        }

        public static string Convert(float value)
        {
            return value.ToString();
        }

        public static string Convert(float? value)
        {
            return value.Value.ToString();
        }

        public static string Convert(string value)
        {
            return value;
        }

        public static string Convert(int value)
        {
            return value.ToString();
        }

        public static string Convert(bool value)
        {
            return value ? "1" : "0";
        }

        public static string Convert(bool? value)
        {
            return value.HasValue ? (value.Value ? "1" : "0") : "0";
        }

        public static string Convert(Tuple<double> value)
        {
            return value.Item1.ToString();
        }

        public static string Convert(Context value)
        {
            if (value == null)
                value = Context.DefaultContext;

            return value.ToString();
        }

        public static string Convert(DType value)
        {
            if (value == null)
                value = DType.Float16;

            return value.ToString();
        }

        public static string Convert(NDShape value)
        {
            if (value == null)
                return "()";

            return value.ToString();
        }
    }
}
