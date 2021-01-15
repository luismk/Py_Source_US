using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Mascot.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Mascot
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture1;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Texture2;
        public ushort Price1;
        public ushort Price7;
        public ushort UN0;
        public ushort Price30;
        public byte C0;
        public byte C1;
        public byte C2;
        public byte C3;
        public byte C4;
        public byte Slot1;
        public byte Slot2;
        public byte Slot3;
        public byte Slot4;
        public byte Slot5;
        public uint Effect1;
        public uint Effect2;
        public uint Effect3;
        public ushort UN1;
        public ushort UN2;
        public ushort GetDay()
        {
            return 7;
        }
    }
}
