using System;

/* 
 * <author>Dylan Vassallo</author>
 * <date>12/03/2018</date>
*/

namespace Domain.Common
{
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

        #region properties 

        /// <summary>
        /// (readonly) Calculate the squared magnitude of two vectors (length). 
        /// To compare distances use this instead of <see cref="Magnitude"/> as it is faster to compute.
        /// </summary>
        public float SqrMagnitude => X * X + Y * Y;

        /// <summary>
        /// (readonly) Calculate the magnitude of two vectors (length). 
        /// To compare distances use of <see cref="SqrMagnitude"/> since it is faster to compute.
        /// </summary>
        public float Magnitude => (float)Math.Sqrt(X * X + Y * Y);

        #endregion properties 

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

        #region public methods 

        /// <summary>
        /// Calculate the squared magnitude of two vectors (length). 
        /// To compare distances use this function since the square root calculation is not used, 
        /// thus making it faster than the <see cref="CalcMagnitude(Vector2)"/> function. 
        /// </summary>
        /// <param name="target">The target to calculate the squared length to.</param>
        /// <returns>Returns the squared length of this vector in relation to the target.</returns>
        public float CalcSqrMagnitude(Vector2 target)
        {
            var xSub = target.X - X;
            var ySub = target.Y - Y;

            return xSub * xSub + ySub * ySub;
        }

        /// <summary>
        /// Calculate the magnitude of two vectors (length). 
        /// To compare distances use the <see cref="CalcSqrMagnitude(Vector2)"/> since the square root calculation is not used,
        /// thus making it faster.  
        /// </summary>
        /// <param name="target">The target to calculate the length to.</param>>
        /// <returns>Returns the length of this vector in relation to the target.</returns>
        public float CalcMagnitude(Vector2 target)
        {
            var xSub = target.X - X;
            var ySub = target.Y - Y;

            return (float)Math.Sqrt(xSub * xSub + ySub * ySub);
        }

        #endregion public methods 

        #region operators 

        /// <summary>
        /// Subtracts each component of v1 from v2.
        /// </summary>
        /// <param name="v1"><see cref="Vector2"/> instance (v1).</param>
        /// <param name="v2"><see cref="Vector2"/> instance (v2).</param>
        /// <returns>Returns the subtraction of two vectors.</returns>
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v2.X - v1.X, v2.Y - v1.Y);
        }

        #endregion operators
    }
}
