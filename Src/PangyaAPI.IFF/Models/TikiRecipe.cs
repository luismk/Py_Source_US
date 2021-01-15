using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file TikiRecipe.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TikiRecipe
    {
        public uint Enable;
        public byte TypeID;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 35)]
        public string Name;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] Unknown;
    }
}
