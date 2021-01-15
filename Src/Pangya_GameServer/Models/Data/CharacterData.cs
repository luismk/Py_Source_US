using PangyaAPI.PangyaClient.Data;
using PangyaAPI.SqlConnector.DataBase;
using System;
using System.Linq;
using System.Text;

namespace Pangya_GameServer.Models.Data
{
    public class CharacterData
    {
        public PlayerCharacter Header;
        public bool NEEDUPDATE { get; set; }

        public bool UpgradeSlot(Byte Slot)
        {
            switch (Slot)
            {
                case 0:
                    this.Header.Power += 1;
                    break;
                case 1:
                    this.Header.Control += 1;
                    break;
                case 2:
                    this.Header.Impact += 1;
                    break;
                case 3:
                    this.Header.Spin += 1;
                    break;
                case 4:
                    this.Header.Curve += 1;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public bool DowngradeSlot(Byte Slot)
        {
            switch (Slot)
            {
                case 0:
                    if ((this.Header.Power <= 0))
                    {
                        return false;
                    }
                    this.Header.Power -= 1;
                    break;
                case 1:
                    if ((this.Header.Control <= 0))
                    {
                        return false;
                    }
                    this.Header.Control -= 1;
                    break;
                case 2:
                    if ((this.Header.Impact <= 0))
                    {
                        return false;
                    }
                    this.Header.Impact -= 1;
                    break;
                case 3:
                    if ((this.Header.Spin <= 0))
                    {
                        return false;
                    }
                    this.Header.Spin -= 1;
                    break;
                case 4:
                    if ((this.Header.Curve <= 0))
                    {
                        return false;
                    }
                    this.Header.Curve -= 1;
                    break;
            }
            return true;
        }

        public void Update(CharacterData info)
        {
            Header = info.Header;
            NEEDUPDATE = true;
        }

        public uint GetPangUpgrade(byte Slot)
        {
            const uint POWPANG = 2100, CONPANG = 1700, IMPPANG = 2400, SPINPANG = 1900, CURVPANG = 1900;

            switch (Slot)
            {
                case 0:
                    return (Header.Power * POWPANG) + POWPANG;
                case 1:
                    return (Header.Control * CONPANG) + CONPANG;
                case 2:
                    return (Header.Impact * IMPPANG) + IMPPANG;
                case 3:
                    return (Header.Spin * SPINPANG) + SPINPANG;
                case 4:
                    return (Header.Curve * CURVPANG) + CURVPANG;
            }
            return 0;
        }

        public string SaveChar(uint UID)
        {
            var SQLString = new StringBuilder();
            if (NEEDUPDATE)
            {
                SQLString.Append('^');
                SQLString.Append(Header.Index);
                SQLString.Append('^');
                SQLString.Append(Header.Power);
                SQLString.Append('^');
                SQLString.Append(Header.Control);
                SQLString.Append('^');
                SQLString.Append(Header.Impact);
                SQLString.Append('^');
                SQLString.Append(Header.Spin);
                SQLString.Append('^');
                SQLString.Append(Header.Curve);
                SQLString.Append('^');
                SQLString.Append(Header.CutinIndex);
                SQLString.Append('^');
                SQLString.Append(Header.HairColour);
                SQLString.Append('^');
                SQLString.Append(Header.AuxPart);
                SQLString.Append('^');
                SQLString.Append(Header.AuxPart2);
                var Table4 = $"Exec dbo.USP_SAVE_CHARACTER_STATS  @UID = '{(int)UID}', @ITEMSTR = '{SQLString}'";
                var _db = new PangyaEntities();

                _db.Database.SqlQuery<PangyaEntities>(Table4).FirstOrDefault();
            }
            return SQLString.ToString();
        }
        internal string GetSqlCharInfo()
        {
            StringBuilder SQLString;
            SQLString = new StringBuilder();
            try
            {
                SQLString.Append('^');
                SQLString.Append(Header.Index);
                for (int i = 0; i <= 23; i++)
                {
                    SQLString.Append('^');
                    SQLString.Append(Header.EquipTypeID[i]);
                }
                for (int i = 0; i <= 23; i++)
                {
                    SQLString.Append('^');
                    SQLString.Append(Header.EquipIndex[i]);
                }
                SQLString.Append(',');

                return SQLString.ToString();
            }
            finally
            {
                SQLString.Clear();
            }
        }
    }
}
