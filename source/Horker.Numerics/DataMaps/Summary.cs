using System;
using System.Collections.Generic;
using System.Linq;

namespace Horker.Numerics.DataMaps
{
    public interface ISummary
    {
        string Name { get; set; }
        int Count { get; set; }
        int NaNCount { get; set; }
        object Min { get; set; }
        object Q1 { get; set; }
        object Mean { get; set; }
        object Median { get; set; }
        object Q3 { get; set; }
        object Max { get; set; }
    }

    public class Summary<T> : ISummary
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int NaNCount { get; set; }
        public T Min { get; set; }
        public T Q1 { get; set; }
        public T Mean { get; set; }
        public T Median { get; set; }
        public T Q3 { get; set; }
        public T Max { get; set; }

        object ISummary.Min { get => Min; set => Min = (T)value; }
        object ISummary.Q1 { get => Q1; set => Q1 = (T)value; }
        object ISummary.Mean { get => Mean; set => Mean = (T)value; }
        object ISummary.Median { get => Median; set => Median = (T)value; }
        object ISummary.Q3 { get => Q3; set => Q3 = (T)value; }
        object ISummary.Max { get => Max; set => Max = (T)value; }
    }
}
