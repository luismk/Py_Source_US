using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file CutinInformation.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CutinInformation
    {
        public uint Enable;
        public uint TypeID;
        public uint Seq;
        public uint Sector;
        public uint Num1;
        public uint Num2;
        public uint NumImg1;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string IMG1;
        public uint NumImg2;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string IMG2;
        public uint NumImg3;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string IMG3;
        public uint Time;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
        public byte[] UN;
        public uint Num4;
    }
}
