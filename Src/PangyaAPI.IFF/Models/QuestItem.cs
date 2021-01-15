using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file QuestItem.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct QuestItem
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        public uint UNK;
        public uint Quest_Type;
        public uint Quest_Qty;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public uint[] Quest_TypeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Quest_Reward_TypeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Quest_Reward_Qty;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] Blank;
    }
}
