using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace PittsburghBabaTemple.Core.Extensions
{
    public static class StringExtension
    {
        private static MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

        public static byte[] ToMD5(this string input)
        {
            byte[] data = Encoding.ASCII.GetBytes(input);
            data = MD5.ComputeHash(data);
            return data;
        }

        public static string ToHexString(this byte[] input)
        {
            var sb = new StringBuilder();
            foreach (byte b in input)
            {
                sb.Append(b.ToString("X2").ToLower());
            }
            return sb.ToString();
        }

        public static string SplitPascal(this string input)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < input.Length; i++)
            {
                var ch = input.Substring(i, 1);

                if (ch == "_")
                {
                    continue;
                }
                if (i > 0 && ch == ch.ToUpper())
                    sb.Append(" ");
                sb.Append(ch);
            }

            return sb.ToString();
        }

        public static IList<int> ToIntList(this string[] strings)
        {
            return strings.Select(s => Int32.Parse(s)).ToList();
        }
    }
}
