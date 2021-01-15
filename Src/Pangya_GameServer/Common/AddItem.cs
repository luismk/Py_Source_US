using System;
namespace Pangya_GameServer.Common
{
    public struct AddItem
    {
        public uint ItemIffId { get; set; }
        public Boolean Transaction { get; set; }
        public uint Quantity { get; set; }
        public uint Day { get; set; }
    }
}
