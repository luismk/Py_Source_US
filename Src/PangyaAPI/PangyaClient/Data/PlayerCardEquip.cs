using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Flags;
using PangyaAPI.IFF.Tools;
using System;
using System.Text;
namespace PangyaAPI.PangyaClient.Data
{
    public class PlayerCardEquip
    {
        public uint ID { get; set; }
        public uint CID { get; set; }
        public uint CHAR_TYPEID { get; set; }
        public uint CARD_TYPEID { get; set; }
        public byte SLOT { get; set; }
        public byte FLAG { get; set; }
        public DateTime REGDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public byte VALID { get; set; }
        public bool NEEDUPDATE { get; set; }

        public bool CheckCard(uint ID, uint CardSlot)
        {
            return (CID == ID) && (SLOT == CardSlot) && (FLAG == 0) && (VALID == 1);
        }

        public uint GetType(UInt32 TypeID)
        {
            switch (IFFTools.GetCardType(TypeID))
            {
                case CardTypeFlag.Normal:
                    return 0;
                case CardTypeFlag.Caddie:
                    return 1;
                case CardTypeFlag.NPC:
                    return 5;
                case CardTypeFlag.Special:
                    return 2;
                default:
                    return 0;
            }
        }

        public byte[] CardEquipInfo()
        {
            PangyaBinaryWriter result;

            result = new PangyaBinaryWriter();
            //var P = IffEntry.Card.GetSPCL(CARD_TYPEID);
            result.WriteUInt32(0);
            result.WriteUInt32(CARD_TYPEID);
            result.WriteUInt32(CHAR_TYPEID);
            result.WriteUInt32(CID);
            result.WriteUInt32(0);
            result.WriteUInt32(0);
            //if (P != null)
            //{
            //    result.WriteUInt32(P.First().Key);
            //    result.WriteUInt32(P.First().Value);
            //}
            //else
            //{

            //}
            result.WriteUInt32(SLOT);
            result.WriteTime(REGDATE);
            result.WriteTime(ENDDATE);
            result.WriteUInt32(GetType(CARD_TYPEID));
            result.WriteByte(1);
            return result.GetBytes();
        }

        public string GetSqlUpdateString()
        {
            StringBuilder SQLString;
            SQLString = new StringBuilder();
            try
            {
                SQLString.Append('^');
                SQLString.Append(ID);
                SQLString.Append('^');
                SQLString.Append(CARD_TYPEID);
                SQLString.Append('^');
                SQLString.Append(ENDDATE);
                SQLString.Append('^');
                SQLString.Append(VALID);
                SQLString.Append(',');
                // close for next player
                return SQLString.ToString();
            }
            finally
            {
                SQLString.Clear();
            }
        }
    }
}
