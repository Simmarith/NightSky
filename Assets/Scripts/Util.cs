using System;
namespace Application
{
    public static class Util
    {
        public static string FormatRevenue(long revenue)
        {
            if (revenue > 999999)
                return string.Format("US$ {0:0.###}M", Math.Round(revenue / 1000000.0, 3));
            if (revenue > 999)
                return string.Format("US$ {0:0.###}k", Math.Round(revenue / 1000.0, 3));

            return string.Format("US$ {0:D}", revenue);
        }
    }
}
