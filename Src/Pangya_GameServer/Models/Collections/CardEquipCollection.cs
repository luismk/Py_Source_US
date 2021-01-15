using System;
using System.Collections.Generic;
using System.Text;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.PangyaClient.Data;
namespace Pangya_GameServer.Models.Collections
{
    public class CardEquipCollection : List<PlayerCardEquip>
    {
        public CardEquipCollection(int UID)
        {
            Build(UID);
        }
        void Build(int UID)
        {
        }

        public void AddCard(PlayerCardEquip P)
        {
            this.Add(P);
        }

        public byte[] Build()
        {
            PangyaBinaryWriter result;
            result = new PangyaBinaryWriter();

            result.Write(new byte[] { 0x37, 0x01 });
            result.WriteUInt16((ushort)this.Count);
            foreach (PlayerCardEquip C in this)
            {
                result.Write(C.CardEquipInfo());
            }
            return result.GetBytes();
        }

        public byte[] MapCard(uint CID)
        {
            uint[] TC = new uint[10];
            PangyaBinaryWriter Packet;

            foreach (var PC in this)
            {
                if (PC.CID == CID)
                {
                    TC[PC.SLOT] = PC.CARD_TYPEID;
                }
            }
            Packet = new PangyaBinaryWriter();
            try
            {
                Packet.WriteUInt32(TC[0]);
                Packet.WriteUInt32(TC[1]);
                Packet.WriteUInt32(TC[2]);
                Packet.WriteUInt32(TC[3]);
                Packet.WriteUInt32(TC[4]);
                Packet.WriteUInt32(TC[6]);
                Packet.WriteUInt32(TC[7]);
                Packet.WriteUInt32(TC[8]);
                Packet.WriteUInt32(TC[9]);
                Packet.WriteUInt32(TC[10]);
                return Packet.GetBytes();
            }
            finally
            {
                Packet.Dispose();
            }
        }

        // CHARACTER CARD
        public PlayerCardEquip GetCard(UInt32 CID, UInt32 SLOT)
        {
            foreach (PlayerCardEquip result in this)
            {
                if (result.CheckCard(CID, SLOT))
                {
                    return result;
                }
            }
            return null;
        }

        //public Dictionary<bool, CardEquipData> UpdateCard(UInt32 UID, UInt32 CID, UInt32 CHARTYPEID, UInt32 CARDTYPEID, byte SLOT, byte FLAG, byte TIME)
        //{
        //    CardEquipData UP;
        //    UP = null;
        //    foreach (CardEquipData P in this)
        //    {
        //        switch (FLAG)
        //        {
        //            case 0:
        //                if ((P.CID == CID) && (P.CHAR_TYPEID == CHARTYPEID) && (P.SLOT == SLOT) && (P.FLAG == 0) && (P.VALID == 1))
        //                {
        //                    UP = P;
        //                    break;
        //                }
        //                break;
        //            case 1:
        //                if ((P.CID == CID) && (P.CARD_TYPEID == CARDTYPEID) && (P.SLOT == SLOT) && (P.FLAG == 1) && (P.ENDDATE > DateTime.Now) && (P.VALID == 1))
        //                {
        //                    UP = P;
        //                    break;
        //                }
        //                break;
        //        }
        //    }
        //    if (UP == null)
        //    {
        //        try
        //        {

        //            var _db = new PangyaEntities();
        //            var card = _db.USP_ADD_CARD_EQUIP((int)UID, (int)CID, (int)CHARTYPEID, (int)CARDTYPEID, SLOT, FLAG, TIME).FirstOrDefault();
        //            if (!(card.CODE == 0))
        //            {
        //                return new Dictionary<bool, CardEquipData>() { { false, null } };
        //            }

        //            this.Clear();
        //            Build((int)UID);
        //            return new Dictionary<bool, CardEquipData>() { { true, new CardEquipData()
        //            {
        //                ID = (uint)card.OUT_INDEX,
        //                CID = (uint)card.CID,
        //                CHAR_TYPEID = (uint)card.CHARTYPEID,
        //                CARD_TYPEID = (uint)card.CARDTYPEID,
        //                SLOT = (byte)card.SLOT,
        //                REGDATE = card.REGDATE,
        //                ENDDATE = (DateTime)card.ENDDATE,
        //                FLAG = (byte)card.FLAG,
        //                VALID = 1,
        //                NEEDUPDATE = false
        //            } } };
        //        }
        //        finally
        //        {
        //        }
        //    }
        //    else
        //    {
        //        UP.CARD_TYPEID = CARDTYPEID;
        //        UP.NEEDUPDATE = true;
        //        if (FLAG == 1)
        //        {
        //            UP.ENDDATE = DateTime.Now.AddMinutes(TIME);
        //        }
        //    }
        //    return new Dictionary<bool, CardEquipData>() { { true, UP } };
        //}


        public string GetSqlUpdateCardEquip()
        {
            StringBuilder SQLString;
            SQLString = new StringBuilder();
            try
            {
                foreach (var P in this)
                {
                    if (P.NEEDUPDATE)
                    {
                        SQLString.Append(P.GetSqlUpdateString());

                        // ## set update to false when request string
                        P.NEEDUPDATE = false;
                    }
                }
                return SQLString.ToString();
            }
            finally
            {

                SQLString.Clear();
            }
        }
    }
}
