using LZ4Sharp;

namespace Infrastructure.Resources.Compression
{
    public class LZ4CompressionProvider : ICompressionService
    {
        public byte[] Compress(byte[] input)
        {
            return LZ4.Compress(input);
        }

        public byte[] Decompress(byte[] input)
        {
            return LZ4.Decompress(input);
        }
    }
}
