using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Horker.Numerics.DataMaps
{
    public class Column
    {
        private string _name;
        private IList _data;

        public string Name => _name;

        public IList Data
        {
            get => _data;
            internal set => _data = value;
        }

        public Type DataType => _data.GetDataType();

        public Column(string name, IList data)
        {
            while (data is Column c)
                data = c.Data;

            _name = name;
            _data = data;
        }
    }
}
