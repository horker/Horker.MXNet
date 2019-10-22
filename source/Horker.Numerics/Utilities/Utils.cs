using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.Utilities
{
    public static class Utils
    {
        public static object StripOffPSObject(object obj)
        {
            if (obj is PSObject pso && pso.BaseObject != null)
                return pso.BaseObject;

            return obj;
        }

        public static bool IsNumeric(Type type)
        {
            return type == typeof(double) ||
                type == typeof(float) ||
                type == typeof(long) ||
                type == typeof(int) ||
                type == typeof(short) ||
                type == typeof(byte) ||
                type == typeof(sbyte) ||
                type == typeof(decimal);
        }
    }
}
