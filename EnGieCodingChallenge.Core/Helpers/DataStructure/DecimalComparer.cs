using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnGieCodingChallenge.Core.Helpers.DataStructure
{
    public class DecimalComparer : IComparer<decimal>
    {
        public int Compare(decimal x, decimal y)
        {
            if(x.CompareTo(y) == 0)
            {
                return 1;
            }
            return x.CompareTo(y);
        }
    }
}
