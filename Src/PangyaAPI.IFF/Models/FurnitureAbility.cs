using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file FurnitureAbility.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct FurnitureAbility
    {
        public uint Enabled;
        public uint TypeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public uint[] Unknown;
        [field: MarshalAs(UnmanagedType.Struct)]
        public SystemTime StartTime;
        public uint Unknown2;
        public uint TypeID_Item;
        public uint Price;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Unknown3;
    }
}
