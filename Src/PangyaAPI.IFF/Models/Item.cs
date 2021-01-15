using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Item
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;
        public uint ItemType;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture;
        public ushort Power;
        public ushort Control;
        public ushort Accuracy;
        public ushort Spin;
        public ushort Curve;
        public ushort Unkown;
    }
}