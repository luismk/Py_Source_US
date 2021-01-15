using System.Runtime.InteropServices;
namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerFurniture
    {
        public uint Index { get; set; }
        public uint TypeID { get; set; }
        public ushort Unknown { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float R { get; set; }
        public byte Unknown2 { get; set; }
    }
}
