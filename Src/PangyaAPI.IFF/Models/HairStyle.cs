using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file HairStyle.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HairStyle
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        public byte HairColor;
        public HairColorFlag CharType;
        public ushort Blank;
    }
}
