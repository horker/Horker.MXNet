using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics
{
    public static partial class NDArrayExtensions
    {
        private static readonly string NULL_REPR = "null";

        private static Tuple<int, int> GetDisplayWidth<T>(IList<T> data)
        {
            var count = data.Count;
            if (count > 100)
                count = 100;

            int width = 0;
            int fractionWidth = 0;
            if (TypeTest.IsNumeric(typeof(T)))
            {
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
            }
            else
            {
                for (var i = 0; i < count; ++i)
                {
                    int w;
                    if (data[i] == null)
                        w = NULL_REPR.Length;
                    else
                        w = data[i].ToString().Length;
                    if (w > width)
                        width = w;
                }
            }

            return Tuple.Create(width + fractionWidth, fractionWidth - 1);
        }

        private static void BuildMatrixString<T>(StringBuilder builder, IList<T> data, int offset, int rowCount, int columnCount, int displayWidth, int fractionWidth)
        {
            int count = rowCount * columnCount;

            string formatString;
            if (TypeTest.IsNumeric(typeof(T)))
            {
                formatString = "{0," + displayWidth;
                if (fractionWidth > 0)
                    formatString += ":0." + new string('0', fractionWidth);
                formatString += "}";
            }
            else
            {
                formatString = "{0,-" + displayWidth + "}";
            }

            for (var row = 0; row < rowCount; ++row)
            {
                for (var column = 0; column < columnCount; ++column)
                {
                    if (column > 0)
                        builder.Append(' ');

                    var value = data[row * columnCount + column + offset];
                    if (value == null)
                        builder.AppendFormat(formatString, NULL_REPR);
                    else
                        builder.AppendFormat(formatString, value);
                }
                builder.AppendLine();
            }
        }

        private static void BuildHeader<T>(StringBuilder builder, NDArray<T> array)
        {
            builder.AppendFormat("[{0}, {1}]", string.Join(" x ", array.Shape), typeof(T).Name);
        }

        public static string ToStringInLongFormat<T>(this NDArray<T> array)
        {
            var builder = new StringBuilder();
            BuildHeader(builder, array);
            builder.AppendLine();

            var ndims = array.Shape.Length;

            if (ndims == 0)
            {
                builder.Append("<empty>");
                return builder.ToString();
            }

            var data = array.ToArray();

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
                Array.Copy(array.Shape, shape, ndims - 2);

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

        public static string ToStringInShortFormat<T>(this NDArray<T> array, bool includeHeader)
        {
            var builder = new StringBuilder();
            if (includeHeader)
                BuildHeader(builder, array);

            string formatString;
            if (TypeTest.IsNumeric(typeof(T)))
                formatString = "{0:0.###}";
            else
                formatString = "{0}";

            var data = array.ToArray();
            for (var i = 0; i < Math.Min(5, data.Length); ++i)
            {
                builder.Append(' ');
                var value = data[i];
                if (value == null)
                    builder.Append(NULL_REPR);
                else
                    builder.AppendFormat(formatString, value);
            }

            if (data.Length > 5)
                builder.Append("...");

            return builder.ToString();
        }
    }
}
