using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Skin.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Skin
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string MPet;
        public uint PriceUnk;
        public uint Price7;
        public uint Price30;

    }
}
