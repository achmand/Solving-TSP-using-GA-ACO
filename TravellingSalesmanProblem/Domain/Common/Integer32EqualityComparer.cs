using System.Collections.Generic;

namespace Domain.Common
{
    /* 
    * <author>Dylan Vassallo</author>
    * <date>12/03/2018</date>
    */

    /* NOTES:
        - If your key is a value type (struct, primitive, enum, etc.) providing your own EqualityComparer<T> is significantly faster - due to the fact the EqualityComparer<T>.Default boxes the value. 
        https://stackoverflow.com/questions/7143948/efficiency-of-using-iequalitycomparer-in-dictionary-vs-hashcode-and-equals
     */

    public sealed class Integer32EqualityComparer : IEqualityComparer<int>
    {
        private static Integer32EqualityComparer _defaultInstance;

        public static Integer32EqualityComparer Default => _defaultInstance ?? (_defaultInstance = new Integer32EqualityComparer());
        
        public bool Equals(int x, int y)
        {
            return x == y;
        }

        public int GetHashCode(int obj)
        {
            return obj.GetHashCode();
        }
    }
}
