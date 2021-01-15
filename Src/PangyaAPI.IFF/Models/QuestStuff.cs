using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file QuestStuff.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct QuestStuff    
    {
        public uint Enabled;
        public uint TypeID;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string Name;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public byte[] UNK;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public uint[] CounterItem_TypeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public uint[] CounterItem_Qnt;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] Item_Reward_TypeID;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] Item_Reward_Qty;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] Blank;
    }
}
