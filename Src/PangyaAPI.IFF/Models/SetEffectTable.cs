using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file SetEffectTable.iff(not 100% result)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SetEffectTable
    {
        public uint ID;
        public uint Enable;
        public uint Unknown;
        public uint Unknown2;
        public uint Unknown3;
        public uint Unknown4;
        public uint Unknown5;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] TypeID_Items;
        public uint Unknown8;
        public uint Unknown9;
        public uint Unknown10;
        public byte Unknown11;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public byte[] Unknown12;
    }
}
