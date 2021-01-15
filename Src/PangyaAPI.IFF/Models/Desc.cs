using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Desc.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Desc
    {
        public uint TypeID;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string Description;
    }
}
