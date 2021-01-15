using Pangya_GameServer.Models.Data;
namespace Pangya_GameServer.Common
{
    public class TClubUpgradeTemporary
    {
        public WarehouseData PClub;
        public sbyte UpgradeType;
        public byte Count;
        public TClubUpgradeTemporary()
        {
            PClub = new WarehouseData();
        }
        // TClubUpgradeTemporary
        public void Clear()
        {
            PClub = null;
            UpgradeType = -1;
        }
    }
}
