using System.Linq;

namespace System
{
    public static class ByteArrayExtensions
    {
        public static string ComputeHash(this byte[] bytes, string provider = "MD5", bool? toUpper = null)
        {
            return HashHelper.ComputeHash(bytes, provider, toUpper);
        }

        public static string ToHexString(this byte[] bytes, bool? toUpper = null)
        {
            var hex = bytes.Aggregate(string.Empty, (current, t) => current + t.ToString("x2"));
            return toUpper.HasValue ? (toUpper.Value ? hex.ToUpper() : hex.ToLower()) : hex;
        }
    }
}