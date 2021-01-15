using PangyaAPI.IFF.Common;
using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Part.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Part
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string MPet;
        public uint UCCType;
        public uint SlotCount;
        public uint Un1;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture1;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture2;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture3;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture4;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture5;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture6;
        public ushort C0;
        public ushort C1;
        public ushort C2;
        public ushort C3;
        public ushort C4;
        public ushort Slot1;
        public ushort Slot2;
        public ushort Slot3;
        public ushort Slot4;
        public ushort Slot5;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] Blank;
        public uint Un2;
        public uint Un3;
        public uint RentPang;
        public uint Un4;
    }
}
