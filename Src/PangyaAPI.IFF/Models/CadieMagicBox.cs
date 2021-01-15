using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file CadieMagicBox.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CadieMagicBox
    {
        public uint MagicID;
        public uint Enabled;
        public uint Sector;
        public CadieBoxFlag BoxType;
        public uint Level;
        public uint Un1;
        public uint TypeID;
        public uint Quatity;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public UInt32[] TradeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public UInt32[] TradeQuantity;
        public UInt32 BoxID;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Name;
        [field: MarshalAs(UnmanagedType.Struct)]
        public SystemTime StartTime;
        [field: MarshalAs(UnmanagedType.Struct)]
        public SystemTime EndTime;
    }
}
