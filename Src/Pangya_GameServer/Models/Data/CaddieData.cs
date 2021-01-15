using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Tools;
using PangyaAPI.PangyaClient.Data;
using PangyaAPI.SqlConnector.Tools;
using System;
using System.Text;

namespace Pangya_GameServer.Models.Data
{
    public class CaddieData
    {
        public PlayerCaddie Header;
        public DateTime? SkinEndDate { get; set; }
        public DateTime DateEnd { get; set; }
        public bool NeedUpdate { get; set; }

        public byte[] GetExpirationNotice()
        {
            using (var Packet = new PangyaBinaryWriter())
            {
                if (Header.Type == 2 && Header.Day_Left == 0 || Header.Day_Left >= short.MaxValue)
                {
                    Packet.WriteUInt32(1);
                    Packet.WriteStruct(Header);
                }
                return Packet.GetBytes();
            }
        }

        public string GetSQLUpdateString()
        {
            StringBuilder SQLString;
            SQLString = new StringBuilder();
            try
            {
                SQLString.Append('^');
                SQLString.Append(Header.Index);
                SQLString.Append('^');
                SQLString.Append(Header.Skin_TypeID);
                SQLString.Append('^');
                SQLString.Append(SkinEndDate.GetSQLTime());
                SQLString.Append('^');
                SQLString.Append(Header.Pay_Day);
                SQLString.Append(',');
                // close for next player
                return SQLString.ToString();
            }
            finally
            {
                SQLString.Clear();
            }
        }
        public bool Update()
        {
            NeedUpdate = true;

            return true;
        }
        public void UpdateSkin(UInt32 SkinTypeId, UInt32 Period)
        {

            Header.Skin_TypeID = SkinTypeId;
            if ((SkinEndDate == DateTime.MinValue) || (SkinEndDate < DateTime.Now))
            {
                SkinEndDate = DateTime.Now.AddDays(Convert.ToDouble(Period));
                return;
            }
            SkinEndDate = SkinEndDate.Value.AddDays(Convert.ToDouble(Period));
        }

        public bool Exist(uint SkinTypeId)
        {
            return (Header.TypeID == IFFTools.GetCaddieTypeIDBySkinID(SkinTypeId));
        }

        public byte[] GetInfo()
        {
            var resp = new PangyaBinaryWriter();
            resp.WriteStruct(Header);
            return resp.GetBytes();
        }
    }
}
