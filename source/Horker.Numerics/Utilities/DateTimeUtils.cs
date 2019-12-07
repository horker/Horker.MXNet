using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.Utilities
{
    static class DateTimeUtils
    {
        public static int GetDaysPassed(DateTime date)
        {
            return (date - (new DateTime(date.Year, 1, 1))).Days;
        }

        public static int GetDaysPassed(DateTimeOffset date)
        {
            return (date - (new DateTime(date.Year, 1, 1))).Days;
        }

        public static double GetDaysInRadian(DateTime date)
        {
            var year = date.Year;
            var daysPassed = (date - (new DateTime(year, 1, 1))).Days;
            var yearDays = year % 4 == 0 && !(year % 100 == 0 && year % 400 != 0) ? 366 : 365;
            return Math.PI * 2 * daysPassed / yearDays;
        }

        public static double GetDaysInRadian(DateTimeOffset date)
        {
            var year = date.Year;
            var daysPassed = (date - (new DateTime(year, 1, 1))).Days;
            var yearDays = year % 4 == 0 && !(year % 100 == 0 && year % 400 != 0) ? 366 : 365;
            return Math.PI * 2 * daysPassed / yearDays;
        }
    }
}
