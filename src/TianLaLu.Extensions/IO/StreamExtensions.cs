namespace System.IO
{
    public static class StreamExtensions
    {
        public static string ComputeHash(this Stream stream, string provider = "MD5", bool? toUpper = null)
        {
            if(!stream.CanSeek)
                throw new NotSupportedException("stream can not seek! you can read stream to bytes and try compute hash with that bytes!");
            
            return HashHelper.ComputeHash(stream, provider, toUpper);
        }
    }
}