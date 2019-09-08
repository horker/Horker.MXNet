using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Horker.Numerics.DataMaps
{
    public class Column : IList
    {
        private string _name;
        private IList _value;

        public string Name => _name;
        public IList Data => _value;

        public Type DataType
        {
            get
            {
                if (_value is Array)
                    return _value.GetType().GetElementType();

                var ga = _value.GetType().GetGenericArguments();

                if (ga == null || ga.Length == 0)
                    return typeof(object);

                return ga[0];
            }
        }

        public Column(string name, IList value)
        {
            _name = name;
            _value = value;
        }

        // IList methods

        public object this[int index] { get => _value[index]; set => _value[index] = value; }

        public int Count => _value.Count;

        public bool IsReadOnly => _value.IsReadOnly;

        public bool IsFixedSize => _value.IsFixedSize;

        public object SyncRoot => _value.SyncRoot;

        public bool IsSynchronized => _value.IsSynchronized;

        object IList.this[int index] { get => _value[index]; set => _value[index] = value; }

        public int Add(object item)
        {
            return _value.Add(item);
        }

        public void Clear()
        {
            _value.Clear();
        }

        public bool Contains(object item)
        {
            return _value.Contains(item);
        }

        public void CopyTo(Array array, int arrayIndex)
        {
            _value.CopyTo(array, arrayIndex);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(Column))
                return false;
            var other = obj as Column;
            return _name.Equals(other._name) && _value.Equals(other._value);
        }

        public IEnumerator GetEnumerator()
        {
            return _value.GetEnumerator();
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode() * 17 + _value.GetHashCode();
        }

        public int IndexOf(object item)
        {
            return _value.IndexOf(item);
        }

        public void Insert(int index, object item)
        {
            _value.Insert(index, item);
        }

        public void Remove(object item)
        {
            _value.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _value.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _value.GetEnumerator();
        }

        // Type converters

        public T[] ToArray<T>()
        {
            if (DataType == typeof(T))
                ((IList<T>)_value).ToArray();

            var result = new T[_value.Count];
            for (var i = 0; i < result.Length; ++i)
                result[i] = (T)_value[i];

            return result;
        }

        public IList<T> ToList<T>()
        {
            if (_value is IList<T> l)
                return new List<T>(l);

            var result = new List<T>(_value.Count);
            for (var i = 0; i < result.Count; ++i)
                result.Add((T)_value[i]);

            return result;
        }

        public T[] AsArray<T>()
        {
            if (_value is Array && DataType == typeof(T))
                return (T[])_value;

            if (DataType == typeof(T))
                ((IList<T>)_value).ToArray();

            var result = new T[_value.Count];
            for (var i = 0; i < result.Length; ++i)
                result[i] = (T)_value[i];

            return result;
        }

        public IList<T> AsList<T>()
        {
            if (_value is IList<T> l)
                return l;

            var result = new List<T>(_value.Count);
            for (var i = 0; i < result.Count; ++i)
                result.Add((T)_value[i]);

            return result;
        }

        public List<T> ConvertTo<T>()
        {
            var result = new List<T>(_value.Count);
            for (var i = 0; i < _value.Count; ++i)
                result.Add(SmartConverter.ConvertTo<T>(_value[i]));

            return result;
        }

        public IList ConvertTo(Type type)
        {
            return SmartConverter.ConvertTo(type, _value);
        }

        public IList ConvertTo(Type[] possibleTypes, bool raiseError = false)
        {
            var type = DataType;
            foreach (var t in possibleTypes)
            {
                if (t == type)
                    return _value;
            }

            ArgumentException cause = null;
            foreach (var t in possibleTypes)
            {
                try
                {
                    return SmartConverter.ConvertTo(t, _value);
                }
                catch (ArgumentException ex)
                {
                    cause = ex;
                }
            }

            if (raiseError)
                throw new ArgumentException("Failed to convert to any possible types", cause);

            return _value;
        }

        public static implicit operator double[](Column value)
        {
            return value.AsArray<double>();
        }

        public static implicit operator float[](Column value)
        {
            return value.AsArray<float>();
        }

        public static implicit operator long[](Column value)
        {
            return value.AsArray<long>();
        }

        public static implicit operator int[](Column value)
        {
            return value.AsArray<int>();
        }

        public static implicit operator short[](Column value)
        {
            return value.AsArray<short>();
        }

        public static implicit operator byte[](Column value)
        {
            return value.AsArray<byte>();
        }

        public static implicit operator sbyte[](Column value)
        {
            return value.AsArray<sbyte>();
        }

        public static implicit operator bool[](Column value)
        {
            return value.AsArray<bool>();
        }

        public static implicit operator DateTime[](Column value)
        {
            return value.AsArray<DateTime>();
        }

        public static implicit operator DateTimeOffset[](Column value)
        {
            return value.AsArray<DateTimeOffset>();
        }
    }
}
