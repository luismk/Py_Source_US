using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
namespace PangyaAPI.ZIP.Compression
{
    public class ZipFile : ZipArchive
    {
        public ZipFile(Stream stream) : base(stream)
        {
        }

        public ZipFile(Stream stream, ZipArchiveMode mode) : base(stream, mode)
        {
        }

        public ZipFile(Stream stream, ZipArchiveMode mode, bool leaveOpen) : base(stream, mode, leaveOpen)
        {
        }

        public ZipFile(Stream stream, ZipArchiveMode mode, bool leaveOpen, Encoding entryNameEncoding) : base(stream, mode, leaveOpen, entryNameEncoding)
        {
        }
    }

}