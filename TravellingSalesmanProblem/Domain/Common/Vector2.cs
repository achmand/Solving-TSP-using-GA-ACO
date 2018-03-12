namespace Domain.Common
{
    /* 
     * <author>Dylan Vassallo</author>
     * <date>12/03/2018</date>
    */

    /// <summary>
    /// Represents a vector with two single-precision floating-point values.
    /// </summary>
    public struct Vector2
    {
        #region public fields 

        /// <summary>
        /// X coordinate.
        /// </summary>
        public float X;

        /// <summary>
        /// Y coordinate. 
        /// </summary>
        public float Y;

        #endregion public fields 

        #region constructors 

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> class that and sets the fields to the X and Y coordinates passed.
        /// </summary>
        /// <param name="x">X coordinate to set.</param>
        /// <param name="y">Y coordinate to set.</param>
        public Vector2(float x, float y)
        {
            X = x;
            Y = y; 
        }

        #endregion constructors 
    }
}
