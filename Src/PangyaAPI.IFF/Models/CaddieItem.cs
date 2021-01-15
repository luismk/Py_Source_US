using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file CaddieItem.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CaddieItem
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x27 + 1)]
        public string MPet;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x27 + 1)]
        public string TexTure;
        public UInt16 Price1;
        public UInt16 Price15;
        public UInt16 PriceUN;
        public UInt16 Price30;
        public UInt32 Un4;
    }
}
