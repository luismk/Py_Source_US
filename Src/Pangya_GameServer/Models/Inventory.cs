using PangyaAPI.PangyaClient.Data;
using Pangya_GameServer.Models.Collections;
namespace Pangya_GameServer.Models
{
    public partial class Inventory
    {
        readonly uint UID;        
        public uint TranCount
        {
            get { return (uint)ItemTransaction.Count; }
        }
        public ItemCollection ItemWarehouse { get; set; }
        public CaddieCollection ItemCaddie { get; set; }
        public CharacterCollection ItemCharacter { get; set; }
        public MascotCollection ItemMascot { get; set; }
        public CardCollection ItemCard { get; set; }
        public CardEquipCollection ItemCardEquip { get; set; }
        public FurnitureCollection ItemRoom { get; set; }
        public TrophyCollection ItemTrophies { get; set; }
        public TrophySpecialCollection ItemTrophySpecial { get; set; }
        public TransactionsCollection ItemTransaction { get; set; }
        public PlayerSelectionBar ToolBar;

        public Inventory(uint uID)
        {
            UID = uID;
            ItemCardEquip = new CardEquipCollection((int)UID);
            ItemCharacter = new CharacterCollection((int)UID);
            ItemMascot = new MascotCollection((int)UID);
            ItemWarehouse = new ItemCollection((int)UID);
            ItemCaddie = new CaddieCollection((int)UID);
            ItemCard = new CardCollection((int)UID);
            ItemTransaction = new TransactionsCollection();
            ItemRoom = new FurnitureCollection((int)UID);
            ToolBar = new PlayerSelectionBar();
            ItemTrophies = new TrophyCollection();
            ItemTrophySpecial = new TrophySpecialCollection();
            ToolBar.CharacterIndex = ItemCharacter[0].Header.Index;
            ToolBar.MascotIndex = ItemMascot[0].Header.Index;
        }
    }
}
