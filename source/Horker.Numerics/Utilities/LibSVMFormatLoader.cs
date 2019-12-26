using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.Utilities
{
    public static class LibSVMFormatLoader
    {
        public static Tuple<T[], T[,]> Load<T>(TextReader reader)
        {
            var lines = new List<string[]>();

            var split = new[] { ' ', '\t', ',' };
            while (reader.Peek() > -1)
            {
                var line = reader.ReadLine().Split(split);
                lines.Add(line);
            }

            var maxIndex = 0;
            var cellSplit = new[] { ':' };
            foreach (var line in lines)
            {
                var last = line[line.Length - 1];
                var tuple = last.Split(cellSplit);
                if (tuple.Length != 2)
                    throw new ArgumentException("bad format");
                var index = int.Parse(tuple[0]);
                if (index > maxIndex)
                    maxIndex = index;
            }

            var labels = new T[lines.Count];
            var data = new T[lines.Count, maxIndex];

            for (var row = 0; row < lines.Count; ++row)
            {
                var line = lines[row];

                var label = double.Parse(line[0]);
                labels[row] = (T)(object)label;

                for (var column = 1; column < line.Length; ++column)
                {
                    var cell = line[column];
                    var tuple = cell.Split(cellSplit);
                    if (tuple.Length != 2)
                        throw new ArgumentException("bad format");
                    var index = int.Parse(tuple[0]);
                    var value = double.Parse(tuple[1]);

                    data[row, index - 1] = (T)(object)value;
                }
            }

            return Tuple.Create(labels, data);
        }
    }
}
