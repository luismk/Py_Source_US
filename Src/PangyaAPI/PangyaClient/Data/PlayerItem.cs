using PangyaAPI.Helper.BinaryModels;
using System.Runtime.InteropServices;

namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerItem
    {
        public uint ItemIndex { get; set; }
        public uint ItemTypeID { get; set; }
        public ushort ItemC0 { get; set; }
        public ushort ItemC1 { get; set; }
        public ushort ItemC2 { get; set; }
        public ushort ItemC3 { get; set; }
        public ushort ItemC4 { get; set; }
        public byte Unknown0 { get; set; }
        public byte? ItemFlag { get; set; }
        public uint? ItemEndDate { get; set; }
        public uint Unknown1 { get; set; }
        public uint? ItemRegDate { get; set; }
        public uint Unknown2 { get; set; }
        public byte Unknown3 { get; set; }
        //tamanho de 16 
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string ItemUCCName { get; set; }
        //tamanho de 25
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
        public byte[] Unknown4 { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ItemUCCUnique { get; set; }
        public byte? ItemUCCStatus { get; set; }
        public ushort? ItemUCCCopyCount { get; set; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string ItemUCCDrawer { get; set; }
        //tamanho de 62
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 62)]
        public byte[] Unknown5 { get; set; }
        public byte[] GetItem()
        {
            var resp = new PangyaBinaryWriter();
            resp.WriteStruct(this);
            return resp.GetBytes();
        }
    }
}
