﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps.Extensions
{
    public static partial class GenericIListExtensions
    {
        public static bool IsNaN(double self) { return double.IsNaN(self); }
        public static bool IsNaN(float self) { return double.IsNaN(self); }
        public static bool IsNaN(long self) { return false; }
        public static bool IsNaN(int self) { return false; }
        public static bool IsNaN(short self) { return false; }
        public static bool IsNaN(byte self) { return false; }
        public static bool IsNaN(sbyte self) { return false; }
        public static bool IsNaN(decimal self) { return false; }
        public static bool IsNaN(string self) { return string.IsNullOrEmpty(self); }
        public static bool IsNaN(object self) { return self == null; }

        public static int CountNaN(this IList<string> self)
        {
            int count = 0;
            foreach (var value in self)
            {
                if (IsNaN(value))
                    ++count;
            }
            return count;
        }
    }
}