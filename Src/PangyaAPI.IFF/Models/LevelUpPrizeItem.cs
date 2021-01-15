using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file LevelUpPrizeItem.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct LevelUpPrizeItem
    {
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] UN;
        public byte UN01;
        public uint Index;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public byte[] UN0;
        public ushort Level;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] ItemTypeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Quantity;
        public uint UN2;
        public uint UN3;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
        public string Congrats_Text;
    }
}
