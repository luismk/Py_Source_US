using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file AuxPart.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct AuxPart
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        public uint Quantity { get; set; }
        public uint Un1 { get; set; }
        public ushort Un2 { get; set; }
        public byte C0 { get; set; }
        public byte C1 { get; set; }
        public byte C2 { get; set; }
        public byte C3 { get; set; }
        public byte C4 { get; set; }
        public byte C5 { get; set; }
        public byte Slot1 { get; set; }
        public byte Slot2 { get; set; }
        public byte Slot3 { get; set; }
        public byte Slot4 { get; set; }
        public byte Slot5 { get; set; }
        public ushort Eff1 { get; set; }
        public ushort Eff2 { get; set; }
        public ushort Eff3 { get; set; }
        public ushort Eff4 { get; set; }
        public ushort Eff5 { get; set; }
        public ushort Eff6 { get; set; }
        public ushort Unknown { get; set; }
    }
}
