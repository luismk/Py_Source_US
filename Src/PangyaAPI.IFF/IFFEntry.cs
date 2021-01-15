using PangyaAPI.IFF.Collections;
using PangyaAPI.IFF.Manager;
using PangyaAPI.IFF.Models;

namespace PangyaAPI.IFF
{
    /// <summary>
    /// static class for load iff
    /// </summary>
    public static class IFFEntry
    {
        #region Fields

        static readonly IFFFile IFF;

        /// <summary>
        /// Read data from the Part.iff file
        /// </summary>
        public static PartCollection Part { get { return IFF.Part; } set { IFF.Part = value; } }

        /// <summary>
        /// Read data from the Card.iff file
        /// </summary>
        public static CardCollection Card { get { return IFF.Card; } set { IFF.Card = value; } }

        /// <summary>
        /// Read data from the Part.iff file
        /// </summary>
        public static CaddieCollection Caddie { get { return IFF.Caddie; } set { IFF.Caddie = value; } }

        /// <summary>
        /// Read data from the Item.iff file
        /// </summary>
        public static ItemCollection Item { get { return IFF.Item; } set { IFF.Item = value; } }

        /// <summary>
        /// Read data from the LevelUpPrizeItem.iff file
        /// </summary>
        public static LevelUpPrizeItemCollection LevelUpPrizeItem { get { return IFF.LevelUpPrizeItem; } set { IFF.LevelUpPrizeItem = value; } }

        /// <summary>
        /// Read data from the Character.iff file
        /// </summary>
        public static CharacterCollection Character { get { return IFF.Character; } set { IFF.Character = value; } }

        /// <summary>
        /// Read data from the Ball.iff file
        /// </summary>
        public static BallCollection Ball { get { return IFF.Ball; } set { IFF.Ball = value; } }

        /// <summary>
        /// Read data from the Ability.iff file
        /// </summary>
        public static AbilityCollection Ability { get { return IFF.Ability; } set { IFF.Ability = value; } }

        /// <summary>
        /// Read data from the Skin.iff file
        /// </summary>
        public static SkinCollection Skin { get { return IFF.Skin; } set { IFF.Skin = value; } }

        /// <summary>
        /// Read data from the CaddieItem.iff file
        /// </summary>
        public static CaddieItemCollection CaddieItem { get { return IFF.CaddieItem; } set { IFF.CaddieItem = value; } }

        /// <summary>
        /// Read data from the Club.iff file
        /// </summary>
        public static ClubCollection Club { get { return IFF.Club; } set { IFF.Club = value; } }

        /// <summary>
        /// Read data from the ClubSet.iff file
        /// </summary>
        public static ClubSetCollection ClubSet { get { return IFF.ClubSet; } set { IFF.ClubSet = value; } }

        /// <summary>
        /// Read data from the Course.iff file
        /// </summary>
        public static CourseCollection Course { get { return IFF.Course; } set { IFF.Course = value; } }

        /// <summary>
        /// Read data from the CutinInformation.iff file
        /// </summary>
        public static CutinInformationCollection CutinInformation { get { return IFF.CutinInformation; } set { IFF.CutinInformation = value; } }

        /// <summary>
        /// Read data from the Desc.iff file
        /// </summary>
        public static DescCollection Desc { get { return IFF.Desc; } set { IFF.Desc = value; } }

        /// <summary>
        /// Read data from the Furniture.iff file
        /// </summary>
        public static FurnitureCollection Furniture { get { return IFF.Furniture; } set { IFF.Furniture = value; } }

        /// <summary>
        /// Read data from the FurnitureAbility.iff file
        /// </summary>
        public static FurnitureAbilityCollection FurnitureAbility { get { return IFF.FurnitureAbility; } set { IFF.FurnitureAbility = value; } }

        /// <summary>
        /// Read data from the Mascot.iff file
        /// </summary>
        public static MascotCollection Mascot { get { return IFF.Mascot; } set { IFF.Mascot = value; } }

        /// <summary>
        /// Read data from the TikiSpecialTable.iff file
        /// </summary>
        public static TikiSpecialTableCollection TikiSpecialTable { get { return IFF.TikiSpecialTable; } set { IFF.TikiSpecialTable = value; } }

        /// <summary>
        /// Read data from the TikiRecipe.iff file
        /// </summary>
        public static TikiRecipeCollection TikiRecipe { get { return IFF.TikiRecipe; } set { IFF.TikiRecipe = value; } }

        /// <summary>
        /// Read data from the TikiPointTable.iff file
        /// </summary>
        public static TikiPointTableCollection TikiPointTable { get { return IFF.TikiPointTable; } set { IFF.TikiPointTable = value; } }

        /// <summary>
        /// Read data from the CadieMagicBox.iff file
        /// </summary>
        public static CadieMagicBoxCollection CadieMagicBox { get { return IFF.CadieMagicBox; } set { IFF.CadieMagicBox = value; } }

        /// <summary>
        /// Read data from the CadieMagicBoxRandom.iff file
        /// </summary>
        public static CadieMagicBoxRandomCollection CadieMagicBoxRandom { get { return IFF.CadieMagicBoxRandom; } set { IFF.CadieMagicBoxRandom = value; } }

        /// <summary>
        /// Read data from the HairStyle.iff file
        /// </summary>
        public static HairStyleCollection HairStyle { get { return IFF.HairStyle; } set { IFF.HairStyle = value; } }

        /// <summary>
        /// Read data from the Match.iff file
        /// </summary>
        public static MatchCollection Match { get { return IFF.Match; } set { IFF.Match = value; } }

        /// <summary>
        /// Read data from the SetItem.iff file
        /// </summary>
        public static SetItemCollection SetItem { get { return IFF.SetItem; } set { IFF.SetItem = value; } }

        /// <summary>
        /// Read data from the Enchant.iff file
        /// </summary>
        public static EnchantCollection Enchant { get { return IFF.Enchant; } set { IFF.Enchant = value; } }

        /// <summary>
        /// Read data from the Achievement.iff file
        /// </summary>
        public static AchievementCollection Achievement { get { return IFF.Achievement; } set { IFF.Achievement = value; } }

        /// <summary>
        /// Read data from the QuestStuff.iff file
        /// </summary>
        public static QuestStuffCollection QuestStuff { get { return IFF.QuestStuff; } set { IFF.QuestStuff = value; } }

        /// <summary>
        /// Read data from the QuestItem.iff file
        /// </summary>
        public static QuestItemCollection QuestItem { get { return IFF.QuestItem; } set { IFF.QuestItem = value; } }

        /// <summary>
        /// Read data from the SetEffectTable.iff file
        /// </summary>
        public static SetEffectTableCollection SetEffectTable { get { return IFF.SetEffectTable; } set { IFF.SetEffectTable = value; } }

        /// <summary>
        /// Read data from the AuxPart.iff file
        /// </summary>
        public static AuxPartCollection AuxPart { get { return IFF.AuxPart; } set { IFF.AuxPart = value; } }

        /// <summary>
        /// Read data from the GrandPrixData.iff file
        /// </summary>
        public static GrandPrixDataCollection GrandPrixData { get { return IFF.GrandPrixData; } set { IFF.GrandPrixData = value; } }

        /// <summary>
        /// Read data from the GrandPrixRankReward.iff file
        /// </summary>
        public static GrandPrixRankRewardCollection GrandPrixRankReward { get { return IFF.GrandPrixRankReward; } set { IFF.GrandPrixRankReward = value; } }

        /// <summary>
        /// Read data from the GrandPrixSpecialHole.iff file
        /// </summary>
        public static GrandPrixSpecialHoleCollection GrandPrixSpecialHole { get { return IFF.GrandPrixSpecialHole; } set { IFF.GrandPrixSpecialHole = value; } }

        /// <summary>
        /// Read data from the MemorialShopCoinItem.iff file
        /// </summary>
        public static MemorialShopCoinItemCollection MemorialShopCoinItem { get { return IFF.MemorialShopCoinItem; } set { IFF.MemorialShopCoinItem = value; } }

        /// <summary>
        /// Read data from the  MemorialShopRareItem.iff file
        /// </summary>
        public static MemorialShopRareItemCollection MemorialShopRareItem { get { return IFF.MemorialShopRareItem; } set { IFF.MemorialShopRareItem = value; } }
        #endregion End

        #region Methods
        static IFFEntry()
        {
            IFF = new IFFFile();
        }

        public static void Load()
        {
            IFF.LoadIff();
        }

        public static void Load(string Filename)
        {
            IFF.SetFileName(Filename);
            IFF.LoadIff();
        }

        public static uint GetRealQuantity(uint TypeID, uint Qty)
        {
            return IFF.GetRealQuantity(TypeID, Qty);
        }

        public static uint GetRentalPrice(uint TypeID)
        {
            return IFF.GetRentalPrice(TypeID);
        }

        /// <summary>
        /// Get Item Name by TypeID
        /// </summary>   
        public static string GetName(uint TypeID)
        {
            return IFF.GetName(TypeID);
        }

        public static byte GetItemTimeFlag(uint TypeID, uint Day)
        {
            return IFF.GetItemTimeFlag(TypeID, Day);
        }

        public static uint GetPrice(uint TypeID, uint ADay)
        {
            return IFF.GetPrice(TypeID, ADay);
        }

        public static sbyte GetShopPriceType(uint TypeID)
        {
            return IFF.GetShopPriceType(TypeID);
        }

        public static bool IsBuyable(uint TypeID)
        {
            return IFF.IsBuyable(TypeID);
        }

        public static HairStyle GetByHairColor(uint itemIffId)
        {
            return HairStyle.GetItem(itemIffId);
        }

        public static bool IsExist(uint TypeID)
        {
            return IFF.IsExist(TypeID);
        }

        public static bool IsSelfDesign(uint TypeID)
        {
            return IFF.IsSelfDesign(TypeID);
        }

        #endregion
    }
}
