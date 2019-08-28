using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Core;

namespace Horker.MXNet.Core
{
    public static class NDArrayExtensions
    {
        private static Tuple<int, int> GetDisplayWidth<T>(IList<T> data)
        {
            var count = data.Count;
            if (count > 100)
                count = 100;

            int width = 0;
            int fractionWidth = 0;

            for (var i = 0; i < count; ++i)
            {
                var value = Math.Abs(Convert.ToDouble(data[i]));

                var w = (int)Math.Floor(Math.Log10(value)) + 1;
                if (w > width)
                    width = w;

                var fl = (value - Math.Floor(value)).ToString().Length - 1;
                if (fl > fractionWidth)
                    fractionWidth = fl;
            }

            ++width; // minus sign

            if (fractionWidth > 5)
                fractionWidth = 5;

            return Tuple.Create(width + fractionWidth, fractionWidth - 1);
        }

        private static void BuildMatrixString<T>(StringBuilder builder, IList<T> data, int offset, int rowCount, int columnCount, int displayWidth, int fractionWidth)
        {
            int count = rowCount * columnCount;

            string formatString = "{0," + displayWidth;
            if (fractionWidth > 0)
                formatString += ":0." + new string('0', fractionWidth);
            formatString += "}";

            for (var row = 0; row < rowCount; ++row)
            {
                for (var column = 0; column < columnCount; ++column)
                {
                    if (column > 0)
                        builder.Append(' ');

                    var value = data[row * columnCount + column + offset];
                    builder.AppendFormat(formatString, value);
                }
                builder.AppendLine();
            }
        }

        private static void BuildHeader<T>(StringBuilder builder, NDArray array)
        {
            builder.AppendFormat("[{0}, {1}]", string.Join(" x ", array.Shape.Dimensions), typeof(T).Name);
        }

        public static string ToStringInLongFormat<T>(this NDArray array)
            where T : new()
        {
            var builder = new StringBuilder();
            BuildHeader<T>(builder, array);
            builder.AppendLine();

            var ndims = array.Shape.NDimensions;

            if (ndims == 0)
            {
                builder.Append("<empty>");
                return builder.ToString();
            }

            var data = array.ToArray<T>();

            var (displayWidth, hasFraction) = GetDisplayWidth(data);

            if (ndims == 1)
            {
                BuildMatrixString(builder, data, 0, 1, array.Shape[0], displayWidth, hasFraction);
            }
            else if (ndims == 2)
            {
                BuildMatrixString(builder, data, 0, array.Shape[0], array.Shape[1], displayWidth, hasFraction);
            }
            else
            {
                var shape = new int[ndims - 2];
                Array.Copy(array.Shape.Dimensions, shape, ndims - 2);

                var matrixSize = array.Shape[ndims - 2] * array.Shape[ndims - 1];
                var offset = 0;
                foreach (var d in ShapeHelpers.Enumerate(shape))
                {
                    builder.AppendFormat("({0}, _, _) =", string.Join(", ", d));
                    builder.AppendLine();
                    BuildMatrixString(builder, data, offset, array.Shape[ndims - 2], array.Shape[ndims - 1], displayWidth, hasFraction);
                    offset += matrixSize;
                }
            }

            // Remove the last newline
            while (builder[builder.Length - 1] == '\r' || builder[builder.Length - 1] == '\n')
                builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        public static string ToStringInShortFormat<T>(NDArray array, bool includeHeader)
            where T : new()
        {
            var builder = new StringBuilder();
            if (includeHeader)
                BuildHeader<T>(builder, array);

            string formatString;
            if (TypeTest.IsNumeric(typeof(T)))
                formatString = "{0:0.###}";
            else
                formatString = "{0}";

            var data = array.ToArray<T>();
            for (var i = 0; i < Math.Min(5, data.Length); ++i)
            {
                builder.Append(' ');
                var value = data[i];
                builder.AppendFormat(formatString, value);
            }

            if (data.Length > 5)
                builder.Append("...");

            return builder.ToString();
        }

        public static string ToStringInLongFormat(this NDArray array)
        {
            var dtype = array.DType;

            if (dtype == DType.Float64)
                return ToStringInLongFormat<double>(array);
            if (dtype == DType.Float32)
                return ToStringInLongFormat<float>(array);
            if (dtype == DType.Int64)
                return ToStringInLongFormat<long>(array);
            if (dtype == DType.Int32)
                return ToStringInLongFormat<int>(array);
            if (dtype == DType.Int8)
                return ToStringInLongFormat<sbyte>(array);
            if (dtype == DType.UInt8)
                return ToStringInLongFormat<byte>(array);

            throw new ArgumentException($"Type {dtype} cannot be displayed");
        }

        public static string ToStringInShortFormat(this NDArray array, bool includeHeader)
        {
            var dtype = array.DType;

            if (dtype == DType.Float64)
                return ToStringInShortFormat<double>(array, includeHeader);
            if (dtype == DType.Float32)
                return ToStringInShortFormat<float>(array, includeHeader);
            if (dtype == DType.Int64)
                return ToStringInShortFormat<long>(array, includeHeader);
            if (dtype == DType.Int32)
                return ToStringInShortFormat<int>(array, includeHeader);
            if (dtype == DType.Int8)
                return ToStringInShortFormat<sbyte>(array, includeHeader);
            if (dtype == DType.UInt8)
                return ToStringInShortFormat<byte>(array, includeHeader);

            throw new ArgumentException($"Type {dtype} cannot be displayed");
        }
    }
}
