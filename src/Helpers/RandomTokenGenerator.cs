using System;
using System.Text;

namespace Helpers
{
    public static class RandomTokenGenerator
    {
        public static string GetGuidBasedToken() => Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", "");

        public static string GetTokenBySize(uint size)
        {
            const string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var rnd = new Random();
            var result = new StringBuilder((int) size);
            for (var i = 0; i < size; i++)
            {
                result.Append(characters[rnd.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}