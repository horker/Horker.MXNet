using MxNet;
using OpenCvSharp.XImgProc.Segmentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Horker.MxNet.PowerShell
{
    public static class NDArrayExtensions
    {
        private static IEnumerable<int[]> EnumerateDimensions(int[] shape)
        {
            var ndims = shape.Length;
            var dims = new int[ndims];
            while (dims[0] < shape[0])
            {
                yield return dims;
                ++dims[ndims - 1];
                for (var i = ndims - 1; i > 0; --i)
                {
                    if (dims[i] < shape[i])
                        break;
                    dims[i] = 0;
                    ++dims[i - 1];
                }
            }
        }

        private static Shape GetAdjacent(Shape shape)
        {
            var dims = shape.Data;
            for (var i = 0; i < dims.Count; ++i)
                dims[i] += 1;

            return new Shape(dims);
        }

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
            builder.AppendFormat("[{0}, {1}]", string.Join(" x ", array.Shape.Data), typeof(T).Name);
        }

        public static string ToStringInLongFormat<T>(this NDArray array)
            where T : new()
        {
            var builder = new StringBuilder();
            BuildHeader<T>(builder, array);
            builder.AppendLine();

            var ndims = array.Shape.Dimension;

            if (ndims == 0)
            {
                builder.Append("<empty>");
                return builder.ToString();
            }

            var data = array.GetValues<T>();

            var w = GetDisplayWidth(data);
            var displayWidth = w.Item1;
            var hasFraction = w.Item2;

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
                Array.Copy(array.Shape.Data.ToArray(), shape, ndims - 2);

                var matrixSize = array.Shape[ndims - 2] * array.Shape[ndims - 1];
                var offset = 0;
                foreach (var d in EnumerateDimensions(shape))
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
            if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
                formatString = "{0:0.###}";
            else
                formatString = "{0}";

            var data = array.GetValues<T>();
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
            var dtype = DType.GetType(array.GetDType());

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
            var dtype = DType.GetType(array.GetDType());

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

        public static T[,] To2DArray<T>(this NDArray self)
        {
//            if (typeof(T) != DType.GetType(self.GetDType()))
//                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (self.Shape.Dimension != 2)
                throw new InvalidOperationException("NDArray is not 2-dimensional");

            var size = self.Size;
            var result = new T[self.Shape[0], self.Shape[1]];
            self.SyncCopyToCPU(result, size);

            return result;
        }

        public static T[,,] To3DArray<T>(this NDArray self)
        {
//            if (typeof(T) != DType.RuntimeType)
//                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (self.Shape.Dimension != 3)
                throw new InvalidOperationException("NDArray is not 3-dimensional");

            var size = self.Size;
            var result = new T[self.Shape[0], self.Shape[1], self.Shape[2]];
            self.SyncCopyToCPU(result, size);

            return result;
        }

        public static T[,,,] To4DArray<T>(this NDArray self)
        {
//            if (typeof(T) != DType.RuntimeType)
//                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (self.Shape.Dimension != 4)
                throw new InvalidOperationException("NDArray is not 4-dimensional");

            var size = self.Size;
            var result = new T[self.Shape[0], self.Shape[1], self.Shape[2], self.Shape[3]];
            self.SyncCopyToCPU(result, size);

            return result;
        }

        public static T[][] To2DJagged<T>(this NDArray self)
        {
//            if (typeof(T) != DType.RuntimeType)
//                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (self.Shape.Dimension != 2)
                throw new InvalidOperationException("NDArray is not 2-dimensional");

            var size = self.Size;
            var s0 = self.Shape[0];
            var s1 = self.Shape[1];

            var data = self.To2DArray<T>();
            var result = new T[s0][];

            for (var i0 = 0; i0 < result.Length; ++i0)
            {
                result[i0] = new T[s1];
                for (var i1 = 0; i1 < result[i0].Length; ++i1)
                {
                    result[i0][i1] = data[i0, i1];
                }
            }

            return result;
        }

        public static T[][][] To3DJagged<T>(this NDArray self)
        {
//            if (typeof(T) != DType.RuntimeType)
//                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (self.Shape.Dimension != 3)
                throw new InvalidOperationException("NDArray is not 3-dimensional");

            var size = self.Size;
            var s0 = self.Shape[0];
            var s1 = self.Shape[1];
            var s2 = self.Shape[2];

            var data = self.To3DArray<T>();
            var result = new T[s0][][];

            for (var i0 = 0; i0 < result.Length; ++i0)
            {
                result[i0] = new T[s1][];
                for (var i1 = 0; i1 < result[i0].Length; ++i1)
                {
                    result[i0][i1] = new T[s2];
                    for (var i2 = 0; i2 < result[i0][i1].Length; ++i1)
                    {
                        result[i0][i1][i2] = data[i0, i1, i2];
                    }
                }
            }

            return result;
        }

        public static T[][][][] To4DJagged<T>(this NDArray self)
        {
//            if (typeof(T) != DType.RuntimeType)
//                throw new ArgumentException($"T must be {DType.RuntimeType.FullName} for this NDArray");

            if (self.Shape.Dimension != 4)
                throw new InvalidOperationException("NDArray is not 4-dimensional");

            var size = self.Size;
            var s0 = self.Shape[0];
            var s1 = self.Shape[1];
            var s2 = self.Shape[2];
            var s3 = self.Shape[3];

            var data = self.To4DArray<T>();
            var result = new T[s0][][][];

            for (var i0 = 0; i0 < result.Length; ++i0)
            {
                result[i0] = new T[s1][][];
                for (var i1 = 0; i1 < result[i0].Length; ++i1)
                {
                    result[i0][i1] = new T[s2][];
                    for (var i2 = 0; i2 < result[i0][i1].Length; ++i1)
                    {
                        result[i0][i1][i2] = new T[s3];
                        for (var i3 = 0; i3 < result[i0][i1][i2].Length; ++i1)
                        {
                            result[i0][i1][i2][i3] = data[i0, i1, i2, i3];
                        }
                    }
                }
            }

            return result;
        }
    }
}
