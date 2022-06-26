
using System.Collections.Generic;

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
            if (number < words.Length)
            {
                return words[number];
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
    }
}
