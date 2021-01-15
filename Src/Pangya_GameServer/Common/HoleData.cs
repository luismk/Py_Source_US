using System.Runtime.InteropServices;
namespace Pangya_GameServer.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HoleData
    {
        public uint HolePosition { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x04)]
        public byte[] Unknown { get; set; }
        public byte Par { get; set; }
        //start pos?
        public float A { get; set; }
        public float B { get; set; }
        //hole position
        public float X { get; set; }
        public float Z { get; set; }
    }
}
