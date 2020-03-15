using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Horker.Numerics.DataMaps
{
    public class Column
    {
        private string _name;
        private SeriesBase _data;

        public string Name => _name;

        internal void SetName(string newName) => _name = newName;

        public SeriesBase Data
        {
            get => _data;
            set => _data = value;
        }

        public Type DataType => _data.DataType;

        public Column(string name, SeriesBase data)
        {
            _name = name;
            _data = data;
        }
    }
}
