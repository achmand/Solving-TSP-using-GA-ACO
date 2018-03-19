using System;

namespace Domain.Common
{
    public sealed class RandomProvider
    {
        private Random _random;

        private static RandomProvider _defaultInstance;
        public static RandomProvider Default => _defaultInstance ?? (_defaultInstance = new RandomProvider());


        public RandomProvider()
        {
            _random = new Random();
        }

        #region methods 

        #region public methods 

        public void RandomSubArrayIndexes(int startIndex, int endIndex, out int sIndex, out int eIndex)
        {
            var difference = endIndex - startIndex;
            if (difference < 2)
            {
                throw new Exception("Ending index and starting index must at least be 3 values apart.");
            }

            do
            {
                sIndex = _random.Next(endIndex);
                eIndex = _random.Next(sIndex + 1, endIndex);
            } while (sIndex == endIndex || (sIndex == startIndex && eIndex == endIndex)); 

            //sIndex = _random.Next(startIndex, endIndex);
            //do
            //{
            //    eIndex = _random.Next(startIndex, endIndex);
            //} // only unique values and not the same as starting index and ending index because it wont be a subarray 
            //while (eIndex == sIndex || (sIndex == startIndex && eIndex == endIndex) || (eIndex == startIndex && sIndex == endIndex));  

            //if (sIndex < eIndex)
            //{
            //    return;
            //}

            //var eIndexTmp = eIndex;

            //eIndex = sIndex;
            //sIndex = eIndexTmp;
        }

        #endregion

        #endregion
    }
}
