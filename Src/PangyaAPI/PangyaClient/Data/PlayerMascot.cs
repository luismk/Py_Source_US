using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerMascot
    {
        public uint Index;
        public uint TypeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public byte[] Unknown;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string Message;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public byte[] Unknown2;
        public ushort IsValid;
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFF.Common.SystemTime EndDate;
        public byte Unknown3;
    }
}
