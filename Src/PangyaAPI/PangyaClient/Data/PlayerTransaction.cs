using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.Helper.Tools;
using PangyaAPI.SqlConnector.Tools;
using System;
namespace PangyaAPI.PangyaClient.Data
{
   public struct PlayerTransaction
    {
        #region Field
        public byte Types { get; set; }
        public uint TypeID { get; set; }
        public uint Index { get; set; }
        public uint PreviousQuan { get; set; }
        public uint NewQuan { get; set; }
        public DateTime DayStart { get; set; }
        // As Unix Datetime
        public DateTime DayEnd { get; set; }
        // As Unix Datetime
        public string UCC { get; set; }
        public byte UCCStatus { get; set; }
        public byte UCCCopyCount { get; set; }
        public ushort C0_SLOT { get; set; }
        public ushort C1_SLOT { get; set; }
        public ushort C2_SLOT { get; set; }
        public ushort C3_SLOT { get; set; }
        public ushort C4_SLOT { get; set; }
        public uint ClubPoint { get; set; }
        public uint WorkshopCount { get; set; }
        public uint CancelledCount { get; set; }
        public uint CardTypeID { get; set; }
        public byte CharSlot { get; set; }

        #endregion

        #region Method

        public byte[] GetInfoData()
        {
            PangyaBinaryWriter result;

            result = new PangyaBinaryWriter();
            result.WriteByte(Compare.IfCompare<byte>(Types <= 0, 0x2, Types));
            result.WriteUInt32(TypeID);
            result.WriteUInt32(Index);
            result.WriteUInt32(Compare.IfCompare<uint>(DayEnd > DateTime.Now, 1, 0));
            // ## if the item has a period time
            if (DayEnd > DateTime.Now)
            {
                result.WriteUInt32(DayStart.UnixTimeConvert());
                result.WriteUInt32(DayEnd.UnixTimeConvert());
                result.WriteUInt32((uint)(DayEnd - DayStart).TotalDays);
            }
            else
            {
                result.WriteUInt32(PreviousQuan);
                result.WriteUInt32(NewQuan);
                result.WriteUInt32(NewQuan - PreviousQuan);
            }
            if (Types == 0xC9)
            {
                result.WriteUInt16(C0_SLOT);
                result.WriteUInt16(C1_SLOT);
                result.WriteUInt16(C2_SLOT);
                result.WriteUInt16(C3_SLOT);
                result.WriteUInt16(C4_SLOT);
            }
            else if ((DayEnd > DayStart))
            {
                result.WriteZero(0x8);
                result.WriteUInt16((ushort)(DayEnd - DayStart).TotalDays);
            }
            else
            {
                result.WriteZero(0xA);
            }
            result.WriteUInt16((ushort)UCC.Length);
            result.WriteStr(UCC, 0x8);
            if (UCC.Length >= 8)
            {
                result.WriteUInt32(UCCStatus);
                result.WriteUInt16(UCCCopyCount);
                result.WriteZero(0x7);
            }
            else if (Types == 0xCB)
            {
                result.WriteUInt32(CardTypeID);
                result.WriteByte(CharSlot);
            }
            else if ((Types == 0xCC))
            {
                result.WriteUInt32(0);
                result.WriteByte(0);
                result.WriteUInt16(C0_SLOT);
                result.WriteUInt16(C1_SLOT);
                result.WriteUInt16(C2_SLOT);
                result.WriteUInt16(C3_SLOT);
                result.WriteUInt16(C4_SLOT);
                result.WriteUInt32(ClubPoint);
                result.WriteByte(Compare.IfCompare<byte>(WorkshopCount > 0, 0, 0xFF));
                result.WriteUInt32(WorkshopCount);
                result.WriteUInt32(CancelledCount);
            }
            else
            {
                result.WriteUInt32(0);
                result.WriteByte(0);
            }
            return result.GetBytes();
        }

        #endregion
    }
}
