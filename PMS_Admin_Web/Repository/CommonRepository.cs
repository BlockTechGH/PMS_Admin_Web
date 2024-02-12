using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Repository
{
    public class CommonRepository
    {
        public static string Truncate(string input, int maxLength)
        {
            if (input.Length > maxLength)
            {
                input = input.Substring(0, maxLength - 3) + "...";
            }

            return input;
        }

        private static String[] units = { "Zero", "One", "Two", "Three",
    "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
    "Seventeen", "Eighteen", "Nineteen" };
        private static String[] tens = { "", "", "Twenty", "Thirty", "Forty",
    "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public static String ConvertAmount(string amount)
        {
            try
            {
                Int64 amount_int = (Int64)Convert.ToInt32(amount);
                Int64 amount_dec = (Int64)Math.Round((Convert.ToInt32(amount) - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return ConvertTo(amount_int) + " Only.";
                }
                else
                {
                    return ConvertTo(amount_int) + " Point " + ConvertTo(amount_dec) + " Only.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public static String ConvertTo(Int64 i)
        {
            if (i < 20)
            {
                return units[i];
            }
            if (i < 100)
            {
                return tens[i / 10] + ((i % 10 > 0) ? " " + ConvertTo(i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " Hundred"
                        + ((i % 100 > 0) ? " And " + ConvertTo(i % 100) : "");
            }
            if (i < 100000)
            {
                return ConvertTo(i / 1000) + " Thousand "
                + ((i % 1000 > 0) ? " " + ConvertTo(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return ConvertTo(i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + ConvertTo(i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return ConvertTo(i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + ConvertTo(i % 10000000) : "");
            }
            return ConvertTo(i / 1000000000) + " Arab "
                    + ((i % 1000000000 > 0) ? " " + ConvertTo(i % 1000000000) : "");
        }


    }
}
