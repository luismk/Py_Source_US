using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Furniture.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Furniture
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Model;
        public ushort Unknown;
        public ushort Unknown2;
        public ushort Unknown3;
        public ushort Unknown4;
        public uint Unknown5;
        public uint Unknown6;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
        public string Texture;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
        public string Texture2;
    }
}
