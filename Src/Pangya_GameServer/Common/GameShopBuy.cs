namespace Pangya_GameServer.Common
{
    public struct GameShopBuy
    {
        public uint UN1 { get; set; }
        public uint TypeID { get; set; }
        public ushort Day { get; set; }
        public ushort UN2 { get; set; }
        public uint Qty { get; set; }
        public uint PangPrice { get; set; }
        public uint CookiePrice { get; set; }
    }
}
