using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace schedInterface
{
    public class functions
    {
        public DateTime? ConvertUnixTimeStamp(string unixTimeStamp)
        {
            if (!string.IsNullOrEmpty(unixTimeStamp))
            {
                return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(unixTimeStamp));
            }
            else
            {
                return null;
            }
        }   
    }
}
