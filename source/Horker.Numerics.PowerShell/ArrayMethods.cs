using Horker.Numerics.DataMaps.Extensions;
using Horker.Numerics.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.PowerShell
{
    public static class ArrayMethods
    {
        public static PSObject Mean(PSObject self, bool skipNaN = true)
        {
            object array = self.BaseObject;

			// Convert values to double if they are not numeric
            var type = array.GetType().GetElementType();
            if (!Utils.IsNumeric(type))
                array = SmartConverter.ConvertTo<double>((dynamic)array);

            return GenericIListExtensions.Mean((dynamic)array, skipNaN);
        }

        public static PSObject Median(PSObject self, bool skipNaN = true)
        {
            object array = self.BaseObject;

			// Convert values to double if they are not numeric
            var type = array.GetType().GetElementType();
            if (!Utils.IsNumeric(type))
                array = SmartConverter.ConvertTo<double>((dynamic)array);

            return GenericIListExtensions.Median((dynamic)array, skipNaN);
        }

        public static PSObject Mode(PSObject self, bool skipNaN = true)
        {
            object array = self.BaseObject;

			// Convert values to double if they are not numeric
            var type = array.GetType().GetElementType();
            if (!Utils.IsNumeric(type))
                array = SmartConverter.ConvertTo<double>((dynamic)array);

            return GenericIListExtensions.Mode((dynamic)array, skipNaN);
        }

    }
}