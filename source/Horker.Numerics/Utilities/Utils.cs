using Horker.Numerics.DataMaps.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
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

        public static IEnumerable<T> StripOffPSObjects<T>(object[] objs)
        {
            if (objs == null)
                yield break;

            foreach (var obj in objs)
            {
                if (obj is PSObject pso && pso.BaseObject != null)
                    yield return (T)pso.BaseObject;
                else
                    yield return (T)obj;
            }
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

        public static List<T> CreateListTyped<T>(int capacity, int count)
        {
            var result = new List<T>(capacity);
            var nan = (T)TypeTrait<T>.GetNaN();
            for (var i = 0; i < count; ++i)
                result.Add(nan);

            return result;
        }

        private static MethodInfo _methodCreateListTyped =
            typeof(Utils).GetMethod(nameof(CreateListTyped), BindingFlags.Static | BindingFlags.Public);

        public static IList CreateList(Type dataType, int capacity, int count)
        {
            var gm = _methodCreateListTyped.MakeGenericMethod(new[] { dataType });
            return (IList)gm.Invoke(null, new object[] { capacity, count });
        }
    }
}
