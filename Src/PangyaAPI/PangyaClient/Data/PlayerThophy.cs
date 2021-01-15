using System.Runtime.InteropServices;
namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerTrophy
    {
        public ushort AMA_6_G { get; set; }
        public ushort AMA_6_S { get; set; }
        public ushort AMA_6_B { get; set; }
        public ushort AMA_5_G { get; set; }
        public ushort AMA_5_S { get; set; }
        public ushort AMA_5_B { get; set; }
        public ushort AMA_4_G { get; set; }
        public ushort AMA_4_S { get; set; }
        public ushort AMA_4_B { get; set; }
        public ushort AMA_3_G { get; set; }
        public ushort AMA_3_S { get; set; }
        public ushort AMA_3_B { get; set; }
        public ushort AMA_2_G { get; set; }
        public ushort AMA_2_S { get; set; }
        public ushort AMA_2_B { get; set; }
        public ushort AMA_1_G { get; set; }
        public ushort AMA_1_S { get; set; }
        public ushort AMA_1_B { get; set; }
        public ushort PRO_1_G { get; set; }
        public ushort PRO_1_S { get; set; }
        public ushort PRO_1_B { get; set; }
        public ushort PRO_2_G { get; set; }
        public ushort PRO_2_S { get; set; }
        public ushort PRO_2_B { get; set; }
        public ushort PRO_3_G { get; set; }
        public ushort PRO_3_S { get; set; }
        public ushort PRO_3_B { get; set; }
        public ushort PRO_4_G { get; set; }
        public ushort PRO_4_S { get; set; }
        public ushort PRO_4_B { get; set; }
        public ushort PRO_5_G { get; set; }
        public ushort PRO_5_S { get; set; }
        public ushort PRO_5_B { get; set; }
        public ushort PRO_6_G { get; set; }
        public ushort PRO_6_S { get; set; }
        public ushort PRO_6_B { get; set; }
        public ushort PRO_7_G { get; set; }
        public ushort PRO_7_S { get; set; }
        public ushort PRO_7_B { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerTrophySpecial
    {
        public uint Index { get; set; }
        public uint TypeID { get; set; }
        public uint Quantity { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerTrophyGrandPrix
    {
        public uint Index { get; set; }
        public uint TypeID { get; set; }
        public uint Quantity { get; set; }
    }
}
