﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxNetLib.OpGenerator
{
    class DType
    {
        private static readonly Dictionary<string, DType> StringToDtypeMap = new Dictionary<string, DType>();
        private static readonly Dictionary<int, DType> IndexToDtypeMap = new Dictionary<int, DType>();
        public static readonly DType Float32 = new DType("float32", "Float32", 0);
        public static readonly DType Float64 = new DType("float64", "Float64", 1);
        public static readonly DType Float16 = new DType("float16", "Float16", 2);
        public static readonly DType Uint8 = new DType("uint8", "Uint8", 3);
        public static readonly DType Int32 = new DType("int32", "Int32", 4);
        public static readonly DType Int8 = new DType("int8", "Int8", 5);
        public static readonly DType Int64 = new DType("int64", "Int64", 6);
  
        public string Name { get; }
        public string CsName { get; }
        public int Index { get; }

        public DType(string name, string csName, int index)
        {
            Name = name;
            CsName = csName;
            Index = index;
            StringToDtypeMap.Add(Name, this);
            IndexToDtypeMap.Add(index,this);
        }
        public static implicit operator string(DType value)
        {
            return value.Name;
        }
        public static implicit operator DType(string value)
        {
            return StringToDtypeMap[value];
        }
        public static explicit operator DType(int index)
        {
            return IndexToDtypeMap[index];
        }

        public static string GetCode(string value)
        {
            if (value == "-1")
                value = "float32";
            var temp = StringToDtypeMap[value];
            return $"DType.{temp.CsName}";
        }
    }
}
