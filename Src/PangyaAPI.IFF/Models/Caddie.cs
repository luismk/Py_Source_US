using PangyaAPI.IFF.Common;
using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Caddie.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Caddie
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base;//iff base data
        public uint Salary;
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string MPet;
        public ushort C0;
        public ushort C1;
        public ushort C2;
        public ushort C3;
        public ushort C4;
        public ushort Un4;
    }
}
