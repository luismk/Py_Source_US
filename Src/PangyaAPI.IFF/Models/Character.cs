using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Character.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Character
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string MPet;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture1;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture2;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture3;
        public ushort C0;
        public ushort C1;
        public ushort C2;
        public ushort C3;
        public ushort C4;
        public byte Slot1;
        public byte Slot2;
        public byte Slot3;
        public byte Slot4;
        public byte Slot5;
        public byte Un1;
        public float MasteryProb;
        public byte Stat1;
        public byte Stat2;
        public byte Stat3;
        public byte Stat4;
        public byte Stat5;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture4;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Un2;
    }
}
