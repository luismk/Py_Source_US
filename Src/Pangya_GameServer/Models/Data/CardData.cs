using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.PangyaClient.Data;
using System;
using System.Text;

namespace Pangya_GameServer.Models.Data
{
    public class CardData
    {
        public PlayerCard Header;

        public bool NeedUpdate { get; set; }
        public byte IsValid { get;  set; }

        // 
        public void AddQuantity(UInt32 Qty)
        {
            this.Header.Quantity += Qty;
            this.NeedUpdate = true;
        }

        public bool RemoveQuantity(UInt32 Count)
        {
            this.Header.Quantity -= Count;
            if (this.Header.Quantity <= 0)
            {
                this.Header.Isvalid = 0;
            }
            this.NeedUpdate = true;
            return true;
        }

        public bool Exist(UInt32 TypeID, UInt32 Index, UInt32 Quantity)
        {
            return (Header.TypeID == TypeID) && (Header.Index == Index) && (Header.Quantity >= Quantity) && (Header.Isvalid == 1);
        }

        public byte[] GetInfo()
        {
            PangyaBinaryWriter Reply;

            Reply = new PangyaBinaryWriter();
            Reply.WriteStruct(Header);
            return Reply.GetBytes();
        }

        public string GetSqlUpdateString()
        {
            StringBuilder SQLString;
            SQLString = new StringBuilder();
            try
            {
                SQLString.Append('^');
                SQLString.Append(Header.Index);
                SQLString.Append('^');
                SQLString.Append(Header.Quantity);
                SQLString.Append('^');
                SQLString.Append(Header.Isvalid);
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
