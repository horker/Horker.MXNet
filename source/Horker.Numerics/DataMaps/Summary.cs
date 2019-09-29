using System;
using System.Collections.Generic;
using System.Linq;

namespace Horker.Numerics.DataMaps
{
    public class Summary
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int NaN { get; set; }
        public int Unique { get; set; }
        public object Mean { get; set; }
        public object Min { get; set; }
        public object Q25 { get; set; }
        public object Median { get; set; }
        public object Q75 { get; set; }
        public object Max { get; set; }
        public object Std { get; set; }
    }
}
