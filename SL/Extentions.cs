
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ClearArchitecture.SL
{
    public static class Extentions
    {
        public static int NumToken(this string str, string delimitor)
        {
            if (string.IsNullOrEmpty(str)) return 0;
            if (string.IsNullOrEmpty(delimitor)) return 0;

            char[] d = delimitor.ToCharArray();
            string[] words = str.Split(d, System.StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        public static string Token(this string str, string delimitor, int number)
        {
            if (string.IsNullOrEmpty(str)) return "";
            if (string.IsNullOrEmpty(delimitor)) return "";
            if (number < 1) return "";

            char[] d = delimitor.ToCharArray();
            string[] words = str.Split(d, System.StringSplitOptions.RemoveEmptyEntries);
            if (number <= words.Length)
            {
                return words[number - 1];
            }
            return "";
        }

        public static List<string> Tokens(this string str, string delimitor)
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(str)) return list;
            if (string.IsNullOrEmpty(delimitor)) return list;

            char[] d = delimitor.ToCharArray();
            string[] words = str.Split(d, System.StringSplitOptions.RemoveEmptyEntries);
            list.AddRange(words);
            return list;
        }

        public static int ToInt32(this string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;

            int value = 0;

            try {
                string s = str.Replace(",", ".");
                if (s.IndexOf(".") == 0)
                {
                    return 0;
                }
                if (s.Substring(s.Length - 1) == ".")
                {
                    s = s.Substring(0, s.Length - 2);
                }
                if (s.IndexOf(".") > 0)
                {
                    s = s.Substring(0, s.IndexOf("."));
                    Console.WriteLine(DateTime.Now.ToString("G") + ": " + s);
                }
                value = int.Parse(s, NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString("G") + ": "+ str + " " + e.Message);
            }
            return value;
        }

        public static double ToDouble(this string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;

            double value = 0;

            try
            {
                string s = str.Replace(",", ".");
                value = double.Parse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString("G") + ": " + str + " " + e.Message);
            }
            return value;
        }
    }
}
