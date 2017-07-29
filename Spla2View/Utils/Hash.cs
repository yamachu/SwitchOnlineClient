using System;
using System.Text;
using System.Security.Cryptography;

namespace Spla2View.Utils
{
    internal class Hash
    {
        public static string GenerateRandomToken(int size)
        {
            var array = new char[size];
            const string a2Z = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var random = new Random();

            for (var i = 0; i < size; i++)
            {
                array[i] = a2Z[random.Next(a2Z.Length)];
            }

            return new string(array);
        }

        public static string String2Hash256(string original)
        {
            var bytes = Encoding.UTF8.GetBytes(original);
            var sha256 = SHA256.Create();
            sha256.Initialize();

            return Convert.ToBase64String(sha256.ComputeHash(bytes))
                          .Replace("+", "-")
                          .Replace("/", "_")
                          .Replace("=", "");
        }
    }
}
