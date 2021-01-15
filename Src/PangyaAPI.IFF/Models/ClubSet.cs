using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file ClubSet.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ClubSet
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        public uint Club1;
        public uint Club2;
        public uint Club3;
        public uint Club4;
        public ushort C0;
        public ushort C1;
        public ushort C2;
        public ushort C3;
        public ushort C4;
        public ushort MaxPow;
        public ushort MaxCon;
        public ushort MaxImp;
        public ushort MaxSpin;
        public ushort MaxCurve;
        public uint ClubType;
        public uint ClubSPoint;
        public uint RecoveryLimit;
        public float RateWorkshop;
        public uint Rank_WorkShop;
        public ushort Transafer;
        public ushort Flag1;
        public uint Unknown7;
        public uint Real_TypeID;
    }
}
