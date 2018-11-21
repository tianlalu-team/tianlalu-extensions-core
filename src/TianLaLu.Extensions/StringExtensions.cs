using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static string ComputeHash(this string str, string provider = "MD5", bool? toUpper = null)
        {
            return Encoding.UTF8.GetBytes(str).ComputeHash(provider, toUpper);
        }
    }
}