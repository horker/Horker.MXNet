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
    }
}
