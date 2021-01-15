using PangyaAPI.IFF.Common;
using System;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file Enchant.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Enchant
    {
        public uint Enable;
        public uint TypeID;
        public long Pang;
    }
}
