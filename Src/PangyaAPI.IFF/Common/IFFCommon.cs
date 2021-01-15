using PangyaAPI.IFF.Flags;
using System.Runtime.InteropServices;
namespace PangyaAPI.IFF.Common
{
    /// <summary>
    /// Common data structure found at the head of many IFF datasets(not 100%)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IFFCommon //size = 0x98(152 bytes)
    {
        /// <summary>
        /// Status of this object
        /// </summary>
        public uint Enabled { get; set; }//4 position
        /// <summary>
        /// TypeID of this object
        /// </summary>
        public uint TypeID { get; set; }//8 position
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        /// <summary>
        /// Name of this object
        /// </summary>
        public string Name { get; set; }//48 position
        /// <summary>
        /// Level requirement for this object
        /// </summary>
        public LevelFlag Level { get; set; }//49 position
        /// <summary>
        /// Icon for this object
        /// </summary>
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Icon { get; set; }//89 position
        public ushort UNK { get; set; }//91 position
        /// <summary>
        /// Price of this object
        /// </summary>
        public uint ItemPrice { get; set; }//95 position
        /// <summary>
        /// Discounted price of this object
        /// </summary>
        public uint DiscountPrice { get; set; }//99 position
        /// <summary>
        /// Used price of this object
        /// </summary>
        public uint UsedPrice { get; set; }//103 position
        /// <summary>
        /// Instance of <see cref="PangyaAPI.IFF.Flags.ShopFlag"/>
        /// </summary>
        public ShopFlag PriceType { get; set; }//104 position
        /// <summary>
        /// Instance of <see cref="PangyaAPI.IFF.Flags.MoneyFlag"/>
        /// </summary>
        public MoneyFlag MoneyFlag { get; set; }//105 position(0x01 in stock; 0x02 disable gift; 0x03 Special; 0x08 new; 0x10 hot)
        /// <summary>
        /// A time flag
        /// </summary>
        public byte TimeFlag { get; set; }//106 position
        /// <summary>
        /// A time flag
        /// </summary>
        public byte TimeByte { get; set; }//107 position
        public uint TPItemCount { get; set; }//111 position
        public uint TPCount { get; set; }// 115 positon
        public ushort Mileage { get; set; }// 117 position
        public ushort BonusProb { get; set; }// 119 position
        public ushort Mileage2 { get; set; }// 121 position
        public ushort Mileage3 { get; set; }// 123 position
        public uint TikiPointShop { get; set; }// 127 position
        public uint TikiPang { get; set; }// 131 position
        public uint Active { get; set; }// 135 position is exist?
        /// <summary>
        /// Time this object becomes available
        /// </summary>
        [field: MarshalAs(UnmanagedType.Struct)]
        public SystemTime StartTime { get; set; }// 149 position
        /// <summary>
        /// Time this object stops being available
        /// </summary>
        [field: MarshalAs(UnmanagedType.Struct)]
        public SystemTime EndTime { get; set; }// 163 position
    }
}
