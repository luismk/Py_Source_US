using System;
using System.Runtime.InteropServices;

namespace Pangya_GameServer.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GuildData
    {
        public UInt32 ID { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string Name { get; set; }
        public uint Pangs { get; set; }
        public uint LevelPoint { get; set; }
        public uint Position { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public string Image { get; set; }
        public uint TotalMember { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string Notice { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 101)]
        public string Introducing { get; set; }
        public uint LeaderUID { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 22)]
        public string LeaderNickname { get; set; }
    }
}
