using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.DataMaps
{
    internal static class OperatorFuncs<T>
    {
        // Unary: +, -, !, ~, ++, --
        public static readonly Func<T, T> UnaryPlus = GetUnaryOperatorFunc(Expression.UnaryPlus, "+");
        public static readonly Func<T, T> Negate = GetUnaryOperatorFunc(Expression.Negate, "-");
//        public static readonly Func<T, T> Not = GetUnaryOperatorFunc(Expression.Not, "!");
//        public static readonly Func<T, T> OnesComplement = GetUnaryOperatorFunc(Expression.OnesComplement, "~");
        public static readonly Func<T, T> Increment = GetUnaryOperatorFunc(Expression.Increment, "++");
        public static readonly Func<T, T> Decrement = GetUnaryOperatorFunc(Expression.Decrement, "--");

        // Binary: +, -, *, /, %, &, |, ^, <<, >>
        public static readonly Func<T, T, T> Add = GetBinaryOperatorFunc(Expression.Add, "+");
        public static readonly Func<T, T, T> Subtract = GetBinaryOperatorFunc(Expression.Subtract, "-");
        public static readonly Func<T, T, T> Multiply = GetBinaryOperatorFunc(Expression.Multiply, "*");
        public static readonly Func<T, T, T> Divide = GetBinaryOperatorFunc(Expression.Divide, "/");
        public static readonly Func<T, T, T> Modulo = GetBinaryOperatorFunc(Expression.Modulo, "%");
//        public static readonly Func<T, T, T> And = GetBinaryOperatorFunc(Expression.And);
//        public static readonly Func<T, T, T> Or = GetBinaryOperatorFunc(Expression.Or);
//        public static readonly Func<T, T, T> ExclusiveOr = GetBinaryOperatorFunc(Expression.ExclusiveOr);
//        public static readonly Func<T, T, T> LeftShift = GetBinaryOperatorFunc(Expression.LeftShift);
//        public static readonly Func<T, T, T> RightShift = GetBinaryOperatorFunc(Expression.RightShift);

        // Comparison: ==, !=, <, >, <=, >=
        public static readonly Func<T, T, bool> Equal = GetBooleanOperatorFunc(Expression.Equal, "==");
        public static readonly Func<T, T, bool> NotEqual = GetBooleanOperatorFunc(Expression.NotEqual, "!=");
        public static readonly Func<T, T, bool> LessThan = GetBooleanOperatorFunc(Expression.LessThan, "<");
        public static readonly Func<T, T, bool> GreaterThan = GetBooleanOperatorFunc(Expression.GreaterThan, ">");
        public static readonly Func<T, T, bool> LessThanOrEqual = GetBooleanOperatorFunc(Expression.LessThanOrEqual, "<=");
        public static readonly Func<T, T, bool> GreaterThanOrEqual = GetBooleanOperatorFunc(Expression.GreaterThanOrEqual, ">=");

        // Composite function
        public static readonly Func<T, T, T, T> MultiplyAdd = GetMultiplyAddFunc();

        // Conversion
        public static Func<T, U> GetConvertOperatorFunc<U>()
        {
            var x = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, U>>(Expression.Convert(x, typeof(U)), x).Compile();
        }

        // Conversion
        public static Func<object, T> GetConvertOperatorFunc(Type actualType)
        {
            var x = Expression.Parameter(typeof(object));
            return Expression.Lambda<Func<object, T>>(
                Expression.Convert(Expression.Convert(x, actualType), typeof(T)), x).Compile();
        }

        private static Dictionary<Type, Func<object, T>> _doublyCastingFuncs = new Dictionary<Type, Func<object, T>>();

        public static Func<object, T> GetDoublyCastingFunc(Type elementType)
        {
            Func<object, T> f = null;
            if (_doublyCastingFuncs.TryGetValue(elementType, out f))
                return f;

            // Boxed value objects should be cast twice; First time for unboxing and second time for converting types.
            var x = Expression.Parameter(typeof(object));
            f = Expression.Lambda<Func<object, T>>(Expression.Convert(Expression.Convert(x, elementType), typeof(T)), x).Compile();

            _doublyCastingFuncs.Add(elementType, f);

            return f;
        }

        private static Func<T, T> GetUnaryOperatorFunc(Func<ParameterExpression, UnaryExpression> ex, string name)
        {
            try
            {
                var x = Expression.Parameter(typeof(T));
                return Expression.Lambda<Func<T, T>>(ex(x), x).Compile();
            }
            // InvalidOperationException is thrown when the type doesn't support the operation.
            // ArgumentException is thrown when the operator returns a different type from T.
            // These exceptions are subclasses of SystemException.
            catch (SystemException)
            {
                return (x) => throw new ArgumentException(string.Format("Operation '{0}' is not supported for type {1}", name, typeof(T).Name));
            }
        }

        private static Func<T, T, T> GetBinaryOperatorFunc(Func<ParameterExpression, ParameterExpression, BinaryExpression> ex, string name)
        {
            try
            {
                var x = Expression.Parameter(typeof(T));
                var y = Expression.Parameter(typeof(T));
                return Expression.Lambda<Func<T, T, T>>(ex(x, y), x, y).Compile();
            }
            catch (SystemException)
            {
                return (x, y) => throw new ArgumentException(string.Format("Operation '{0}' is not supported for type {1}", name, typeof(T).Name));
            }
        }

        private static Func<T, T, bool> GetBooleanOperatorFunc(Func<ParameterExpression, ParameterExpression, BinaryExpression> ex, string name)
        {
            try
            {
                var x = Expression.Parameter(typeof(T));
                var y = Expression.Parameter(typeof(T));
                return Expression.Lambda<Func<T, T, bool>>(ex(x, y), x, y).Compile();
            }
            catch (SystemException)
            {
                return (x, y) => throw new ArgumentException(string.Format("Operation '{0}' is not supported for type {1}", name, typeof(T).Name));
            }
        }

        private static Func<T, T, T, T> GetMultiplyAddFunc()
        {
            try
            {
                var x = Expression.Parameter(typeof(T));
                var y = Expression.Parameter(typeof(T));
                var z = Expression.Parameter(typeof(T));
                return Expression.Lambda<Func<T, T, T, T>>(Expression.Add(Expression.Multiply(x, y), z), x, y, z).Compile();
            }
            catch (SystemException)
            {
                return (x, y, z) => throw new ArgumentException(string.Format("Operations '*' and '+' are not supported for type {0}", typeof(T).Name));
            }
        }
    }
}
