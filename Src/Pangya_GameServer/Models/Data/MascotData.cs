using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Tools;
using PangyaAPI.PangyaClient.Data;
using System;
using System.Text;
namespace Pangya_GameServer.Models.Data
{
    public class MascotData
    {
        public PlayerMascot Header;
        public ushort MascotDayToEnd { get; set; }
        public DateTime MascotEndDate { get { return Header.EndDate.ToDateTime(); } set { Header.EndDate = value.ToSystemTime(); } }
        public bool MascotNeedUpdate { get; set; }
        // Mascots
        public void AddDay(uint DayTotal)
        {
            this.MascotNeedUpdate = true;
            if ((MascotEndDate == DateTime.MinValue) || (MascotEndDate < DateTime.Now))
            {
                this.MascotEndDate = DateTime.Now.AddDays(Convert.ToDouble(DayTotal));
                return;
            }
            this.MascotEndDate = this.MascotEndDate.AddDays(Convert.ToDouble(DayTotal));

            Update();
        }

        public byte[] GetMascotInfo()
        {
            using (var Packet = new PangyaBinaryWriter())
            {               
                Packet.WriteStruct(Header);
                var result = Packet.GetBytes();
                return result;
            }
        }

        public bool Update()
        {
            this.MascotNeedUpdate = true;
            return true;
        }
        public void SetText(string Text)
        {
            this.Header.Message = Text;
            Update();
        }

        internal string GetSqlUpdateString()
        {
            StringBuilder SQLString;
            SQLString = new StringBuilder();
            try
            {
                SQLString.Append('^');
                SQLString.Append(Header.Index);
                SQLString.Append('^');
                SQLString.Append(Header.TypeID);
                SQLString.Append('^');
                SQLString.Append(Header.Message);
                SQLString.Append('^');
                SQLString.Append(Header.IsValid);
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
