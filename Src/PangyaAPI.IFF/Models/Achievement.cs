using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Achievement.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Achievement
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        public uint Achievement_Index;
        public uint Achievement_Type;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name2;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name3;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name4;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name5;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name6;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name7;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name8;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name9;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string Achievement_Name10;
        public ushort UNK2;
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public uint[] Achievement_QuestTypeID;
        public uint UNK3;
    }
}
