using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Ball.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Ball
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        public uint Unknown0;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Model;
        public uint Unknown2;
        public uint Unknown3;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallSequence1;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallSequence2;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallSequence3;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallSequence4;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallSequence5;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallSequence6;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallSequence7;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallFx1;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallFx2;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallFx3;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallFx4;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallFx5;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallFx6;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string BallFx7;
        public ushort Power;
        public ushort Control;
        public ushort Accuracy;
        public ushort Spin;
        public ushort Curve;
        public ushort Unknown4;

    }
}
