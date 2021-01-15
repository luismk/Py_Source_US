using System.Runtime.InteropServices;
namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PlayerSelectionBar
    {
        public uint CaddieIndex;
        public uint CharacterIndex;
        public uint ClubSetIndex;
        public uint BallTypeID;
        [field: MarshalAs(UnmanagedType.Struct)]
       public PlayerItemSlot ItemSlot;
        public uint BackGroundIndex;
        public uint FrameIndex;
        public uint StickerIndex;
        public uint SlotIndex;
        public uint Unknown;
        public uint TitleIndex;
        [field: MarshalAs(UnmanagedType.Struct)]
       public PlayerItemDecoration Decoration;
        public uint MascotIndex;
        public uint Poster1;
        public uint Poster2;
    }
}
