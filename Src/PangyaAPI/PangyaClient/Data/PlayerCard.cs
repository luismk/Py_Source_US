using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerCard    
    {
        public uint Index { get; set; }
        public uint TypeID { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] Unknown;
        public uint Quantity { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Unknown2;
        public ushort Isvalid { get; set; }
    }
}
