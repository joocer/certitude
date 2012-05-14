namespace Infrastructure.Resources.Compression
{
    public interface ICompressionService
    {
        byte[] Compress(byte[] input);
        byte[] Decompress(byte[] input);
    }
}
