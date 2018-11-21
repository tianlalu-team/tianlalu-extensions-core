using System.IO;
using System.Security.Cryptography;

namespace System
{
    public static class HashHelper
    {
        internal static string ComputeHash(byte[] bytes, string provider, bool? toUpper)
        {
            using (var algorithm = HashAlgorithm.Create(provider))
            {
                var hash = algorithm.ComputeHash(bytes);
                return hash.ToHexString(toUpper);
            }
        }

        internal static string ComputeHash(Stream stream, string provider, bool? toUpper)
        {
            using (var algorithm = HashAlgorithm.Create(provider))
            {
                var hash = algorithm.ComputeHash(stream);
                return hash.ToHexString(toUpper);
            }
        }
    }
}