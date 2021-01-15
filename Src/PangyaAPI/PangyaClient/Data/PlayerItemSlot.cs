using System.Runtime.InteropServices;
namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerItemSlot
    {
        public uint Slot1 { get; set; }
        public uint Slot2 { get; set; }
        public uint Slot3 { get; set; }
        public uint Slot4 { get; set; }
        public uint Slot5 { get; set; }
        public uint Slot6 { get; set; }
        public uint Slot7 { get; set; }
        public uint Slot8 { get; set; }
        public uint Slot9 { get; set; }
        public uint Slot10 { get; set; }
    }
}
