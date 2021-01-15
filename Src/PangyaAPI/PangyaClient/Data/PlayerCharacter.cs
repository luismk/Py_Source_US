using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerCharacter// 513 bytes
    {
        public uint TypeID;
        public uint Index;
        public ushort HairColour;
        public ushort GiftFlag;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public uint[] EquipTypeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public uint[] EquipIndex;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 216)]
        public byte[] Unknown;
        public uint AuxPart;//Ring Type ID
        public uint AuxPart2;//Ring Type ID 2
        public uint Unknown2;
        public uint Unknown3;
        public uint Unknown4;
        public uint CutinIndex;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] Unknown5;
        public byte Power;
        public byte Control;
        public byte Impact;
        public byte Spin;
        public byte Curve;
        public uint MasteryPoint;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public byte[] CardData;//cards equip in character
        public uint Unknown6;
        public uint Unknown7;

      
    }
}
