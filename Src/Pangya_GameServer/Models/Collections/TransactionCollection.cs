using Pangya_GameServer.Models.Data;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.PangyaClient.Data;
using System;
using System.Collections.Generic;
namespace Pangya_GameServer.Models.Collections
{
    public class TransactionsCollection : List<PlayerTransaction>
    {
        public void AddInfo(PlayerTransaction Tran)
        {
            this.Add(Tran);
        }

        public void AddChar(Byte ShowType, CharacterData Char)
        {
            PlayerTransaction Tran;
            if ((Char == null))
            {
                return;
            }
            Tran = new PlayerTransaction()
            {
                Types = ShowType,
                TypeID = Char.Header.TypeID,
                Index = Char.Header.Index,
                PreviousQuan = 0,
                NewQuan = 0,
                UCC = String.Empty
            };
            this.AddInfo(Tran);
        }

        public void AddItem(Byte ShowType, WarehouseData Item, UInt32 Add)
        {
            PlayerTransaction Tran;
            if ((Item == null))
            {
                return;
            }
            Tran = new PlayerTransaction()
            {
                Types = ShowType,
                TypeID = Item.Header.ItemTypeID,
                Index = Item.Header.ItemIndex,
                PreviousQuan = Item.Header.ItemC0 - Add,
                NewQuan = Item.Header.ItemC0,
                UCC = string.Empty
            };
            this.AddInfo(Tran);
        }


        public void AddCard(Byte ShowType, CardData Card, UInt32 Add)
        {
            PlayerTransaction Tran;
            if ((Card == null))
            {
                return;
            }
            Tran = new PlayerTransaction()
            {
                Types = ShowType,
                TypeID = Card.Header.TypeID,
                Index = Card.Header.Index,
                PreviousQuan = Card.Header.Quantity - Add,
                NewQuan = Card.Header.Quantity,
                UCC = string.Empty
            };
            this.AddInfo(Tran);
        }

        public void AddCharStatus(Byte ShowType, CharacterData Char)
        {
            PlayerTransaction Tran;
            if ((Char == null))
            {
                return;
            }
            Tran = new PlayerTransaction()
            {
                Types = ShowType,
                TypeID = Char.Header.TypeID,
                Index = Char.Header.Index,
                PreviousQuan = 0,
                NewQuan = 0,
                UCC = string.Empty,
                C0_SLOT = Char.Header.Power,
                C1_SLOT = Char.Header.Control,
                C2_SLOT = Char.Header.Impact,
                C3_SLOT = Char.Header.Spin,
                C4_SLOT = Char.Header.Curve
            };
            this.AddInfo(Tran);
        }

        public void AddClubSystem(WarehouseData Item)
        {
            PlayerTransaction Tran;
            if ((Item == null))
            {
                return;
            }
            Tran = new PlayerTransaction()
            {
                Types = 0xCC,
                TypeID = Item.Header.ItemTypeID,
                Index = Item.Header.ItemIndex,
                PreviousQuan = 0,
                NewQuan = 0,
                UCC = string.Empty,
                C0_SLOT = Item.ItemC0Slot,
                C1_SLOT = Item.ItemC1Slot,
                C2_SLOT = Item.ItemC2Slot,
                C3_SLOT = Item.ItemC3Slot,
                C4_SLOT = Item.ItemC4Slot,
                ClubPoint = (uint)Item.ItemClubPoint,
                WorkshopCount = (uint)Item.ItemClubWorkCount,
                CancelledCount = (uint)Item.ItemClubSlotCancelledCount
            };
            this.AddInfo(Tran);
        }


        public byte[] GetTran()
        {
            PangyaBinaryWriter result;

            result = new PangyaBinaryWriter();
            var rnd = new Random();
            result.Write(new byte[] { 0x16, 0x02 });
            result.WriteInt32(rnd.Next());//number random
            result.WriteUInt32(Count);
            foreach (PlayerTransaction Tran in this)
            {
                result.Write(Tran.GetInfoData());
            }
            this.Clear();
            return result.GetBytes();
        }
    }
}
