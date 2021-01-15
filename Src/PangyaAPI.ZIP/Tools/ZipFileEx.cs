using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using PangyaAPI.ZIP.Compression;
namespace PangyaAPI.ZIP.Tools
{
    public static class ZipFileEx
    {        
        public static ZipFile Open(string namefile)
        {
            return new ZipFile(File.OpenRead(namefile));
        }

        public static MemoryStream GetFileData(this ZipFile zip, string archiveFileName)
        {
            if (zip.CheckIFF(archiveFileName))
            {
                var _ms = new MemoryStream();
                zip.Entries.FirstOrDefault(c => c.Name == archiveFileName).Open().CopyTo(_ms);
                return _ms;
            }
            return new MemoryStream(new byte[0]);
        }

        static bool CheckIFF(this ZipFile zip, string archiveFileName)
        {
            return zip.Entries.Any(c => c.Name == archiveFileName);
        }
    }
}
