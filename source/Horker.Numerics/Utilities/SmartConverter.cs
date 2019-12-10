using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Horker.Numerics
{
    public static class SmartConverter
    {
        private static readonly Regex NUMERIC_RE = new Regex(@"(NaN|(?:[+-]?((?:[\d,]+\.?(?:\d*))|(?:\.\d+))(?:[eE][+-]?\d+)?))");

        // ref.
        // Currency Symbols
        // http://www.unicode.org/charts/PDF/U20A0.pdf
        // Halfwidth and fullwidth form
        // http://www.unicode.org/charts/PDF/UFF00.pdf
        //
        // Note: The backslash is displayed as currency sign in several Asian environments.

        private static readonly Regex CURRENCY_RE = new Regex("[\u20a0-\u20cf\u0024\u00a2\u00a3\u00a4\u00a5\u0192\u058f\u060b\u09f2\u09f3\u0af1\u0bf9\u0e3f\u17db\u2133\u5143\u5186\u5706\u5713\ufdfc\uff04\uffe0\uffe1\uffe5\uffe6\\\\]");

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

        public static bool IsFloatingPoint(Type type)
        {
            return type == typeof(float) || type == typeof(double);
        }

        public static string ExtractNumber(string input)
        {
            var match = NUMERIC_RE.Match(input);
            return match.Captures[0].Value.Replace(",", "");
        }

        private static T ReturnFallbackValue<T>(object input, T? fallback, Exception cause)
            where T : struct
        {
            if (fallback.HasValue)
                return fallback.Value;
            throw new ArgumentException($"Failed convert into type {nameof(T)}: {input}", cause);
        }

        public static double ToDouble(object input, double? fallback = null)
        {
            if (input is PSObject pso)
                input = pso.BaseObject;

            if (input is double d)
                return d;

            if (input is bool b)
                return b ? 1.0 : 0.0;

            if (input is null || DBNull.Value.Equals(input))
                return double.NaN;

            try
            {
                if (input is string s)
                {
                    if (string.IsNullOrWhiteSpace(s))
                        return double.NaN;

                    s = CURRENCY_RE.Replace(s, "");

                    if (s == "∞" || s == "+∞")
                        return double.PositiveInfinity;

                    if (s == "-∞")
                        return double.NegativeInfinity;

                    var success = double.TryParse(s, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out double result);
                    if (!success)
                        return ReturnFallbackValue(input, fallback, null);

                    return result;
                }

                return Convert.ToDouble(input);
            }
            catch (Exception e)
            {
                return ReturnFallbackValue(input, fallback, e);
            }
        }

        public static float ToSingle(object input, float? fallback = null)
        {
            try
            {
                return (float)ToDouble(input, fallback);
            }
            catch (Exception e)
            {
                return ReturnFallbackValue(input, fallback, e);
            }
        }

        public static float ToFloat(object input, float? fallback = null)
        {
            return ToSingle(input, fallback);
        }

        public static long ToInt64(object input, long? fallback = null)
        {
            if (input is PSObject pso)
                input = pso.BaseObject;

            if (input is long i)
                return i;

            if (input is bool b)
                return b ? 1 : 0;

            try
            {
                if (input is string s)
                {
                    s = CURRENCY_RE.Replace(s, "");
                    var success = long.TryParse(s, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out long result);
                    if (!success)
                        return ReturnFallbackValue(input, fallback, null);

                    return result;
                }

                return Convert.ToInt32(input);
            }
            catch (Exception e)
            {
                return ReturnFallbackValue(input, fallback, e);
            }
        }

        public static long ToLong(object input, long? fallback = null)
        {
            return ToInt64(input, fallback);
        }

        public static int ToInt32(object input, int? fallback = null)
        {
            try
            {
                return (int)ToInt64(input, fallback);
            }
            catch (Exception e)
            {
                return ReturnFallbackValue(input, fallback, e);
            }
        }

        public static int ToInt(object input, int? fallback = null)
        {
            return ToInt32(input, fallback);
        }

        public static short ToInt16(object input, short? fallback = null)
        {
            try
            {
                return (short)ToInt64(input, fallback);
            }
            catch (Exception e)
            {
                return ReturnFallbackValue(input, fallback, e);
            }
        }

        public static short ToShort(object input, short? fallback = null)
        {
            return ToInt16(input, fallback);
        }

        public static byte ToUInt8(object input, byte? fallback = null)
        {
            try
            {
                return (byte)ToInt64(input, fallback);
            }
            catch (Exception e)
            {
                return ReturnFallbackValue(input, fallback, e);
            }
        }

        public static byte ToByte(object input, byte? fallback = null)
        {
            return ToUInt8(input, fallback);
        }

        public static sbyte ToInt8(object input, sbyte? fallback = null)
        {
            try
            {
                return (sbyte)ToInt64(input, fallback);
            }
            catch (Exception e)
            {
                return ReturnFallbackValue(input, fallback, e);
            }
        }

        public static sbyte ToSByte(object input, sbyte? fallback = null)
        {
            return ToInt8(input, fallback);
        }

        public static bool ToBool(object input, bool? fallback = null)
        {
            if (input is PSObject pso)
                input = pso.BaseObject;

            if (input is bool b)
                return b;

            if (input == null)
                return false;

            if (IsNumeric(input.GetType()))
            {
                var value = Convert.ToInt32(input);
                if (value == 1 || value == -1)
                    return true;
                if (value == 0)
                    return false;
            }

            if (input is string s)
            {
                s = s.ToLower();
                if (s == "t" || s == "true")
                    return true;
                if (s == "f" || s == "false")
                    return false;
            }

            return ReturnFallbackValue(input, fallback, null);
        }

        public static DateTime ToDateTime(object input, bool assumeLocal = true, string format = null, CultureInfo culture = null, DateTime? fallback = null)
        {
            if (input is PSObject pso)
                input = pso.BaseObject;

            if (input is DateTime dt)
                return dt;

            if (input is DateTimeOffset dto)
                return dto.DateTime;

            try
            {
                if (input is string s)
                {
                    if (s.Length == 14)
                    {
                        if (int.TryParse(s.Substring(0, 4), out var year) &&
                            int.TryParse(s.Substring(4, 2), out var month) &&
                            int.TryParse(s.Substring(6, 2), out var day) &&
                            int.TryParse(s.Substring(8, 2), out var hour) &&
                            int.TryParse(s.Substring(10, 2), out var minute) &&
                            int.TryParse(s.Substring(12, 2), out var second))
                            return new DateTime(year, month, day, hour, minute, second, assumeLocal ? DateTimeKind.Local : DateTimeKind.Utc);
                    }
                    else if (s.Length == 8)
                    {
                        if (int.TryParse(s.Substring(0, 4), out var year) &&
                            int.TryParse(s.Substring(4, 2), out var month) &&
                            int.TryParse(s.Substring(6, 2), out var day))
                            return new DateTime(year, month, day, 0, 0, 0, assumeLocal ? DateTimeKind.Local : DateTimeKind.Utc);
                    }
                    else if (s.Length == 6)
                    {
                        if (int.TryParse(s.Substring(0, 2), out var year) &&
                            int.TryParse(s.Substring(2, 2), out var month) &&
                            int.TryParse(s.Substring(4, 2), out var day))
                        {
                            year += year >= 30 ? 1900 : 2000;
                            return new DateTime(year, month, day, 0, 0, 0, assumeLocal ? DateTimeKind.Local : DateTimeKind.Utc);
                        }
                    }

                    DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;
                    if (assumeLocal)
                        style |= DateTimeStyles.AssumeLocal;
                    else
                        style |= DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal;


                    if (culture == null)
                        culture = CultureInfo.CurrentCulture;

                    if (string.IsNullOrEmpty(format))
                    {
                        var success = DateTime.TryParse(s, culture.DateTimeFormat, style, out var result);
                        if (!success)
                            return ReturnFallbackValue(input, fallback, null);
                        return result;
                    }
                    else
                    {
                        var success = DateTime.TryParseExact(s, format, culture, style, out var result);
                        if (!success)
                            return ReturnFallbackValue(input, fallback, null);
                        return result;
                    }
                }

                return Convert.ToDateTime(input);
            }
            catch (Exception e)
            {
                return ReturnFallbackValue(input, fallback, e);
            }
        }

        public static DateTimeOffset ToDateTimeOffset(object input, bool assumeLocal = true, string format = null, CultureInfo culture = null, DateTimeOffset? fallback = null)
        {
            if (input is PSObject pso)
                input = pso.BaseObject;

            if (input is DateTimeOffset dto)
                return dto;

            if (input is DateTime dt)
                return new DateTimeOffset(dt);

            try
            {
                if (input is string s)
                {
                    if (s.Length == 14)
                    {
                        if (int.TryParse(s.Substring(0, 4), out var year) &&
                            int.TryParse(s.Substring(4, 2), out var month) &&
                            int.TryParse(s.Substring(6, 2), out var day) &&
                            int.TryParse(s.Substring(8, 2), out var hour) &&
                            int.TryParse(s.Substring(10, 2), out var minute) &&
                            int.TryParse(s.Substring(12, 2), out var second))
                        {
                            var d = new DateTime(year, month, day, hour, minute, second, assumeLocal ? DateTimeKind.Local : DateTimeKind.Utc);
                            return new DateTimeOffset(d);
                        }

                    }
                    else if (s.Length == 8)
                    {
                        if (int.TryParse(s.Substring(0, 4), out var year) &&
                            int.TryParse(s.Substring(4, 2), out var month) &&
                            int.TryParse(s.Substring(6, 2), out var day))
                        {
                            var d = new DateTime(year, month, day, 0, 0, 0, assumeLocal ? DateTimeKind.Local : DateTimeKind.Utc);
                            return new DateTimeOffset(d);
                        }
                    }
                    else if (s.Length == 6)
                    {
                        if (int.TryParse(s.Substring(0, 2), out var year) &&
                            int.TryParse(s.Substring(2, 2), out var month) &&
                            int.TryParse(s.Substring(4, 2), out var day))
                        {
                            year += year >= 30 ? 1900 : 2000;
                            var d = new DateTime(year, month, day, 0, 0, 0, assumeLocal ? DateTimeKind.Local : DateTimeKind.Utc);
                            return new DateTimeOffset(d);
                        }
                    }

                    DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;
                    if (assumeLocal)
                        style |= DateTimeStyles.AssumeLocal;
                    else
                        style |= DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal;

                    if (culture == null)
                        culture = CultureInfo.CurrentCulture;

                    if (string.IsNullOrEmpty(format))
                    {
                        var success = DateTimeOffset.TryParse(s, culture.DateTimeFormat, style, out var result);
                        if (!success)
                            return ReturnFallbackValue(input, fallback, null);
                        return result;
                    }
                    else
                    {
                        var success = DateTimeOffset.TryParseExact(s, format, culture, style, out var result);
                        if (!success)
                            return ReturnFallbackValue(input, fallback, null);
                        return result;
                    }
                }

                return new DateTimeOffset(Convert.ToDateTime(input));
            }
            catch (Exception e)
            {
                return ReturnFallbackValue(input, fallback, e);
            }
        }

        public static T ConvertTo<T>(object input)
        {
            if (typeof(T) == typeof(double)) return (T)(object)ToDouble(input);
            if (typeof(T) == typeof(float)) return (T)(object)ToFloat(input);
            if (typeof(T) == typeof(long)) return (T)(object)ToLong(input);
            if (typeof(T) == typeof(int)) return (T)(object)ToInt(input);
            if (typeof(T) == typeof(short)) return (T)(object)ToShort(input);
            if (typeof(T) == typeof(byte)) return (T)(object)ToByte(input);
            if (typeof(T) == typeof(sbyte)) return (T)(object)ToSByte(input);
            if (typeof(T) == typeof(bool)) return (T)(object)ToBool(input);
            if (typeof(T) == typeof(DateTime)) return (T)(object)ToDateTime(input);
            if (typeof(T) == typeof(DateTimeOffset)) return (T)(object)ToDateTimeOffset(input);
            if (typeof(T) == typeof(string)) return (T)(object)Convert.ToString(input);

            return (T)input;
        }

        public static List<T> ConvertTo<T>(IList input)
        {
            var result = new List<T>(input.Count);

            foreach (var e in input)
                result.Add(ConvertTo<T>(e));

            return result;
        }

        public static T[] ConvertTo<T, U>(U[] input)
        {
            var result = new T[input.Length];

            for (var i = 0; i < input.Length; ++i)
                result[i] = ConvertTo<T>(input[i]);

            return result;
        }

        public static IList ConvertTo(Type type, IList input)
        {
            if (type == typeof(double)) return ConvertTo<double>(input);
            if (type == typeof(float)) return ConvertTo<float>(input);
            if (type == typeof(long)) return ConvertTo<long>(input);
            if (type == typeof(int)) return ConvertTo<int>(input);
            if (type == typeof(short)) return ConvertTo<short>(input);
            if (type == typeof(byte)) return ConvertTo<byte>(input);
            if (type == typeof(sbyte)) return ConvertTo<sbyte>(input);
            if (type == typeof(bool)) return ConvertTo<bool>(input);
            if (type == typeof(DateTime)) return ConvertTo<DateTime>(input);
            if (type == typeof(DateTimeOffset)) return ConvertTo<DateTimeOffset>(input);
            if (type == typeof(string)) return ConvertTo<string>(input);

            throw new NotImplementedException($"Conversion to this type is not supported so far: {type.FullName}");
        }
    }
}
