using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Course.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Course
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string ShortName { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string LocalizedName { get; set; }
        public byte CourseFlag { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string PropertyFileName { get; set; }
        public uint Unknown1 { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string CourseSequence { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] Unknown2 { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
        public byte[] Par_Hole { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
        public byte[] Min_Score_Hole { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
        public byte[] Max_Score_Hole { get; set; }
        public short Unknown3 { get; set; }
    }
}
