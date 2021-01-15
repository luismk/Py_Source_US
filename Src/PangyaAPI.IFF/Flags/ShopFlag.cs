namespace PangyaAPI.IFF.Flags
{
    /// <summary>
    /// This flag is handling buying conditions
    /// </summary>
    public enum ShopFlag : byte
    {
        Display = 85,
        /// <summary>
        /// Unknown value
        /// </summary>
        Unknown1 = 128,

        /// <summary>
        /// Unknown value
        /// </summary>
        Unknown2 = 64,

        /// <summary>
        /// Unknown value
        /// </summary>
        Unknown3 = 32,

        Active = 37,

        /// <summary>
        /// Unknown value
        /// </summary>
        Unknown4 = 16,

        /// <summary>
        /// Unknown value
        /// </summary>
        Unknown5 = 8,

        Tradeable = 7,

        Unknown0 = 5,
        /// <summary>
        /// This shop item is a coupon
        /// </summary>
        Coupon = 4,

        /// <summary>
        /// This shop item is not giftable
        /// </summary>
        NonGiftable1 = 69,
        NonGiftable = 2,

        /// <summary>
        /// This shop item is giftable
        /// </summary>
        Giftable = 1,

        /// <summary>
        /// No special buying conditions
        /// </summary>
        None = 0,
    }
}
