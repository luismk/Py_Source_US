using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file TikiSpecialTable.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TikiSpecialTable
    {
        public uint Enable;
        public byte TypeID;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 35)]
        public string Name;
        public uint Qty;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] TypeID_Item;
    }
}
