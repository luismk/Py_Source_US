using PangyaAPI.IFF.Common;
using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file SetItem.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct SetItem
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        public uint Total;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public uint[] Part_TypeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public ushort[] Part_Qty;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public string Blank;//bytes = 0x00
    }
}
