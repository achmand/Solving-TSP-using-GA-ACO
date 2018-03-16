using System;

namespace Domain.Common
{
    /* 
    * <author>Dylan Vassallo</author>
    * <date>16/03/2018</date>
    */

    /// <summary>
    /// A helper static class which holds common/helper methods. 
    /// </summary>
    public static class Helpers
    {
        #region method/s 

        #region public method/s 

        /// <summary>
        /// Maps an integer value between the starting range (s) to the target range (t). 
        /// </summary>
        /// <param name="starting">The starting value to map between two ranges.</param>
        /// <param name="sMinRange">The starting range (minimum).</param>
        /// <param name="sMaxRange">The starting range (maximum).</param>
        /// <param name="tMinRange">The target range (minimum).</param>
        /// <param name="tMaxRange">The target range (maximum).</param>
        /// <returns></returns>
        public static int Map(int starting, int sMinRange, int sMaxRange, int tMinRange, int tMaxRange)
        {
            if (starting > sMaxRange)
            {
                throw new Exception("Starting value cannot exceed starting max range.");
            }

            return tMinRange + (starting - sMinRange) * (tMaxRange - tMinRange) / (sMaxRange - sMinRange);
        }

        /// <summary>
        /// Maps a float value between the starting range (s) to the target range (t). 
        /// </summary>
        /// <param name="starting">The starting value to map between two ranges.</param>
        /// <param name="sMinRange">The starting range (minimum).</param>
        /// <param name="sMaxRange">The starting range (maximum).</param>
        /// <param name="tMinRange">The target range (minimum).</param>
        /// <param name="tMaxRange">The target range (maximum).</param>
        /// <returns></returns>
        public static float Map(float starting, float sMinRange, float sMaxRange, float tMinRange, float tMaxRange)
        {
            if (starting > sMaxRange)
            {
                throw new Exception("Starting value cannot exceed starting max range.");
            }

            return tMinRange + (starting - sMinRange) * (tMaxRange - tMinRange) / (sMaxRange - sMinRange);
        }

        /// <summary>
        /// Maps a double value between the starting range (s) to the target range (t). 
        /// </summary>
        /// <param name="starting">The starting value to map between two ranges.</param>
        /// <param name="sMinRange">The starting range (minimum).</param>
        /// <param name="sMaxRange">The starting range (maximum).</param>
        /// <param name="tMinRange">The target range (minimum).</param>
        /// <param name="tMaxRange">The target range (maximum).</param>
        /// <returns></returns>
        public static double Map(double starting, double sMinRange, double sMaxRange, double tMinRange, double tMaxRange)
        {
            if (starting > sMaxRange)
            {
                throw new Exception("Starting value cannot exceed starting max range.");
            }

            return tMinRange + (starting - sMinRange) * (tMaxRange - tMinRange) / (sMaxRange - sMinRange);
        }

        #endregion public method/s

        #endregion method/s
    }
}
