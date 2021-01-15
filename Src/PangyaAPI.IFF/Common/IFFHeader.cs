using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Common
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IFFHeader
    {
        /// <summary>
        /// IFF Number of existing items
        /// </summary>
        public ushort RecordCount;
        /// <summary>
        /// IFF Region(Default = 0(US/GLOBAL?))
        /// </summary>
        public ushort Region;
        /// <summary>
        /// IFF version
        /// </summary>
        public uint Version;
    }
}
