using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    public static class TypeTest
    {
        public static bool IsNumeric(Type type)
        {
            return type == typeof(byte) ||
                type == typeof(sbyte) ||
                type == typeof(short) ||
                type == typeof(int) ||
                type == typeof(long) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(decimal);
        }

        public static bool IsFloatingPointNumber(Type type)
        {
            return type == typeof(float) || type == typeof(double);
        }
    }
}
