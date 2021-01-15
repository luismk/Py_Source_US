using PangyaAPI.IFF.Common;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Models
{
    /// <summary>
    /// Is Struct file CadieMagicBoxRandom.iff
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct CadieMagicBoxRandom
    {
        public uint Index;
        public uint TypeID;
        public uint Qty;
        public uint Rate;
    }
}
