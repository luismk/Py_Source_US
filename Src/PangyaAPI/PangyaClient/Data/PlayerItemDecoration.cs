using System.Runtime.InteropServices;
namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerItemDecoration
    {
        public uint BackGroundTypeID { get; set; }
        public uint FrameTypeID { get; set; }
        public uint StickerTypeID { get; set; }
        public uint SlotTypeID { get; set; }
        public uint UnknownTypeID { get; set; }
        public uint TitleTypeID { get; set; }
    }
}