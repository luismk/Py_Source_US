using PangyaAPI.Helper.BinaryModels;
using System.Collections.Generic;
using Pangya_GameServer.Models.Data;
using System.Text;
namespace Pangya_GameServer.Models.Collections
{
    public class CardCollection : List<CardData>
    {
        //Constructor  Create()
        public CardCollection(int UID)
        {
            Build(UID);
        }

        public int CardAdd(CardData Value)
        {
            Value.NeedUpdate = false;
            Add(Value);
            return Count;
        }

        void Build(int UID)
        {
            
        }
        public byte[] Build()
        {
            PangyaBinaryWriter Packet;

            Packet = new PangyaBinaryWriter();
            try
            {
                Packet.Write(new byte[] { 0x38, 0x01 });
                Packet.WriteUInt32(0);
                Packet.WriteUInt16((ushort)Count);
                foreach (var Card in this)
                {
                    Packet.WriteStruct(Card.Header);
                }
                return Packet.GetBytes();
            }
            finally
            {

            }
        }
        public CardData GetCard(uint ID)
        {
            foreach (CardData Card in this)
            {
                if ((Card.Header.Index == ID) && (Card.Header.Quantity >= 1) && (Card.Header.Isvalid == 1))
                {
                    return Card;
                }
            }
            return null;
        }

        public CardData GetCard(uint ID, uint Quantity)
        {
            foreach (CardData Card in this)
            {
                if ((Card.Header.TypeID == ID) && (Card.Header.Quantity >= Quantity) && (Card.Header.Isvalid == 1))
                {
                    return Card;
                }
            }
            return null;
        }

        public CardData GetCard(uint TypeID, uint Index, uint Quantity)
        {
            foreach (CardData Card in this)
            {
                if ((Card.Header.TypeID == TypeID) && (Card.Header.Index == Index) && (Card.Header.Quantity >= Quantity) && (Card.Header.Isvalid == 1))
                {
                    return Card;
                }
            }
            return null;
        }

        public string GetSqlUpdateCard()
        {
            StringBuilder SQLString;
            SQLString = new StringBuilder();

            try
            {
                foreach (CardData Cards in this)
                {
                    if (Cards.NeedUpdate)
                    {
                        SQLString.Append(Cards.GetSqlUpdateString());
                        // ## set update to false when request string
                        Cards.NeedUpdate = false;

                    }
                }
                return SQLString.ToString();
            }
            finally
            {
                SQLString.Clear();
            }
        }

        public bool IsExist(uint TypeID, uint Index, uint Quantity)
        {
            foreach (CardData Card in this)
            {
                if (Card.Exist(TypeID, Index, Quantity))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
