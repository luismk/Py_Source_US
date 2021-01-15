using Pangya_GameServer.Flags;
using PangyaAPI.Helper;
using System.Runtime.InteropServices;
namespace Pangya_GameServer.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ShotData
    {
        public uint ConnectionId { get; set; }
        [field: MarshalAs(UnmanagedType.Struct)]
        public Point3D Pos;
        public ShopTypeFlag ShotType { get; set; }
        public ushort Un1 { get; set; }
        public uint Pang { get; set; }
        public uint BonusPang { get; set; }
        public uint Un2 { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x06)]
        public byte[] MatchData;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public byte[] ShotDecrypt;

        public ShotData Start()
        {
            Pos = new Point3D();
            return this;
        }
    }
}
