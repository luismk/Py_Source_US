using System.IO;
namespace PangyaAPI.Helper.BinaryModels
{
   public class MemoryStreamExtension : MemoryStream
    {
        public static MemoryStream Memory(byte[] data)
        {
            return new MemoryStream(data);
        }
    }
}
