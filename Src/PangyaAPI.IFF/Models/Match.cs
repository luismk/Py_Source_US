using PangyaAPI.IFF.Flags;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Match.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Match
    {
        public uint Enable;
        public uint TypeID;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string Name;
        public LevelFlag Level;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string TrophyTexture1;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string TrophyTexture2;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string TrophyTexture3;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 120)]
        public byte[] Blank;
    }
}
