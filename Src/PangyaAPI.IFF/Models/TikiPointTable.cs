using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file TikiPointTable.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TikiPointTable
    {
        public uint Index;
        public byte TypeID;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 35)]
        public string Name;
        public uint Qty;
        public uint TypeID_Item;
    }
}
