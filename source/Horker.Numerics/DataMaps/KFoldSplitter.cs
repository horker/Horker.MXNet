using Horker.Numerics.DataMaps.Extensions;
using Horker.Numerics.Random;
using System;
using System.Collections.Generic;

namespace Horker.Numerics.DataMaps
{
    public class KFold
    {
        public int K { get; private set; }
        public DataMap Training { get; private set; }
        public DataMap Validation { get; private set; }

        public KFold(int k, DataMap training, DataMap validation)
        {
            K = k;
            Training = training;
            Validation = validation;
        }
    }

    public class KFoldSplitter
    {
        private int _k;
        private DataMap _dataMap;
        private int[] _folds;

        public KFoldSplitter(DataMap dataMap, int k, bool shuffle = false, IRandom random = null)
        {
            _k = k;
            _dataMap = dataMap;
            _folds = new int[dataMap.MaxRowCount];

            for (var i = 0; i < _folds.Length; ++i)
                _folds[i] = (int)(i / k);

            if (shuffle)
                _folds.ShuffleFill(random);
        }

        public IEnumerable<KFold> EnumerateFolds()
        {
            for (var i = 0; i < _k; ++i)
            {
                var trainFilter = new bool[_folds.Length];
                var validFilter = new bool[_folds.Length];

                for (var j = 0; j < _folds.Length; ++j)
                {
                    trainFilter[j] = _folds[j] != i;
                    validFilter[j] = _folds[j] == i;
                }

                var train = _dataMap.Filter(trainFilter);
                var valid = _dataMap.Filter(validFilter);

                yield return new KFold(i, train, valid);
            }
        }
    }
}
