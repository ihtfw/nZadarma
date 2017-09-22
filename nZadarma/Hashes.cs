using System;
using System.Linq;

using System.Security.Cryptography;

namespace nZadarma
{
    using System.Text;

    public static class Hashes
    {
        public static string MD5(string input)
        {
            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] encriptedBytes = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            return encriptedBytes.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }

        public static string HMACSHA1(string input, string key)
        {
            var cryptoProvider = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] encriptedBytes = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            return encriptedBytes.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }
    }
}