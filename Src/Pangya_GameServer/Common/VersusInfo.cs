using System;
namespace Pangya_GameServer.Common
{
    public struct VersusInfo
    {
        public bool LoadComplete { get; set; }
        public bool LoadHole { get; set; }
        public bool LoadAnimation { get; set; }
        public bool ShotSync { get; set; }
        public Double HoleDistance { get; set; }
        // public VSMatch Team { get; set; }
        public UInt32 LastHit { get; set; }
        // as timestamp
        public sbyte LastScore { get; set; }
        public byte Loading { get; set; }
    }
}
