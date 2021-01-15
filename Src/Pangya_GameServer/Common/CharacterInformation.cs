using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace Pangya_GameServer.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CharacterInformation
    {
        public uint TypeID { get; set; }
        public uint Index { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public uint[] EquipTypeID { get; set; }
        public ushort HairColor { get; set; }
        public Byte Power { get; set; }
        public Byte Control { get; set; }
        public Byte Impact { get; set; }
        public Byte Spin { get; set; }
        public Byte Curve { get; set; }
        public byte Unknown1 { get; set; }
        public uint AuxPartRight { get; set; }
        public uint AuxPartLeft { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 232)]
        public byte[] Unknown3 { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public uint[] EquipIndex { get; set; }
        public uint CutinIndex { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Unknown5 { get; set; }
    }
}
