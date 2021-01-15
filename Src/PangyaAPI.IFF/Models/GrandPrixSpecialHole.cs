using System;
namespace PangyaAPI.IFF.Models
{
    public struct GrandPrixSpecialHole
    {
        public UInt32 Enable { get; set; }
        public UInt32 TypeID { get; set; }
        public UInt32 HolePOS { get; set; }
        public UInt32 Map { get; set; }
        public UInt32 Hole { get; set; }
    }
}
