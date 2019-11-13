using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public class Sha1
    {
        public static string GetSHA1(string str)
        {
            using (var sha1 = SHA1.Create())
            {
                var encoding = new ASCIIEncoding();
                var sb = new StringBuilder();
                var stream = sha1.ComputeHash(encoding.GetBytes(str));
                foreach (var t in stream)
                    sb.AppendFormat("{0:x2}", t);

                return sb.ToString();
            }
        }
    }
}