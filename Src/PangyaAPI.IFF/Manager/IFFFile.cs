using PangyaAPI.IFF.Collections;
namespace PangyaAPI.IFF.Manager
{
    public partial class IFFFile
    {
        #region Fields

        string FileName;
        /// <summary>
        /// Read data from the Part.iff file
        /// </summary>
        public PartCollection Part { get; set; }

        /// <summary>
        /// Read data from the Card.iff file
        /// </summary>
        public CardCollection Card { get; set; }

        /// <summary>
        /// Read data from the Part.iff file
        /// </summary>
        public CaddieCollection Caddie { get; set; }

        /// <summary>
        /// Read data from the Item.iff file
        /// </summary>
        public ItemCollection Item { get; set; }

        /// <summary>
        /// Read data from the LevelUpPrizeItem.iff file
        /// </summary>
        public LevelUpPrizeItemCollection LevelUpPrizeItem { get; set; }

        /// <summary>
        /// Read data from the Character.iff file
        /// </summary>
        public CharacterCollection Character { get; set; }

        /// <summary>
        /// Read data from the Ball.iff file
        /// </summary>
        public BallCollection Ball { get; set; }

        /// <summary>
        /// Read data from the Ability.iff file
        /// </summary>
        public AbilityCollection Ability { get; set; }

        /// <summary>
        /// Read data from the Skin.iff file
        /// </summary>
        public SkinCollection Skin { get; set; }

        /// <summary>
        /// Read data from the CaddieItem.iff file
        /// </summary>
        public CaddieItemCollection CaddieItem { get; set; }

        /// <summary>
        /// Read data from the Club.iff file
        /// </summary>
        public ClubCollection Club { get; set; }

        /// <summary>
        /// Read data from the ClubSet.iff file
        /// </summary>
        public ClubSetCollection ClubSet { get; set; }

        /// <summary>
        /// Read data from the Course.iff file
        /// </summary>
        public CourseCollection Course { get; set; }

        /// <summary>
        /// Read data from the CutinInformation.iff file
        /// </summary>
        public CutinInformationCollection CutinInformation { get; set; }

        /// <summary>
        /// Read data from the Desc.iff file
        /// </summary>
        public DescCollection Desc { get; set; }

        /// <summary>
        /// Read data from the Furniture.iff file
        /// </summary>
        public FurnitureCollection Furniture { get; set; }

        /// <summary>
        /// Read data from the FurnitureAbility.iff file
        /// </summary>
        public FurnitureAbilityCollection FurnitureAbility { get; set; }

        /// <summary>
        /// Read data from the Mascot.iff file
        /// </summary>
        public MascotCollection Mascot { get; set; }

        /// <summary>
        /// Read data from the TikiSpecialTable.iff file
        /// </summary>
        public TikiSpecialTableCollection TikiSpecialTable { get; set; }

        /// <summary>
        /// Read data from the TikiRecipe.iff file
        /// </summary>
        public TikiRecipeCollection TikiRecipe { get; set; }

        /// <summary>
        /// Read data from the TikiPointTable.iff file
        /// </summary>
        public TikiPointTableCollection TikiPointTable { get; set; }

        /// <summary>
        /// Read data from the CadieMagicBox.iff file
        /// </summary>
        public CadieMagicBoxCollection CadieMagicBox { get; set; }

        /// <summary>
        /// Read data from the CadieMagicBoxRandom.iff file
        /// </summary>
        public CadieMagicBoxRandomCollection CadieMagicBoxRandom { get; set; }

        /// <summary>
        /// Read data from the HairStyle.iff file
        /// </summary>
        public HairStyleCollection HairStyle { get; set; }

        /// <summary>
        /// Read data from the Match.iff file
        /// </summary>
        public MatchCollection Match { get; set; }

        /// <summary>
        /// Read data from the SetItem.iff file
        /// </summary>
        public SetItemCollection SetItem { get; set; }

        /// <summary>
        /// Read data from the Enchant.iff file
        /// </summary>
        public EnchantCollection Enchant { get; set; }

        /// <summary>
        /// Read data from the Achievement.iff file
        /// </summary>
        public AchievementCollection Achievement { get; set; }

        /// <summary>
        /// Read data from the QuestStuff.iff file
        /// </summary>
        public QuestStuffCollection QuestStuff { get; set; }

        /// <summary>
        /// Read data from the QuestItem.iff file
        /// </summary>
        public QuestItemCollection QuestItem { get; set; }

        /// <summary>
        /// Read data from the SetEffectTable.iff file
        /// </summary>
        public SetEffectTableCollection SetEffectTable { get; set; }

        /// <summary>
        /// Read data from the AuxPart.iff file
        /// </summary>
       public AuxPartCollection AuxPart { get; set; }

        /// <summary>
        /// Read data from the GrandPrixData.iff file
        /// </summary>
        public GrandPrixDataCollection GrandPrixData { get; set; }

        /// <summary>
        /// Read data from the GrandPrixRankReward.iff file
        /// </summary>
        public GrandPrixRankRewardCollection GrandPrixRankReward { get; set; }

        /// <summary>
        /// Read data from the GrandPrixSpecialHole.iff file
        /// </summary>
        public GrandPrixSpecialHoleCollection GrandPrixSpecialHole { get; set; }

        /// <summary>
        /// Read data from the MemorialShopCoinItem.iff file
        /// </summary>
        public MemorialShopCoinItemCollection MemorialShopCoinItem { get; set; }

        /// <summary>
        /// Read data from the  MemorialShopRareItem.iff file
        /// </summary>
        public MemorialShopRareItemCollection MemorialShopRareItem { get; set; }
        #endregion

        #region Constructor

        public IFFFile(string filename)
        {
            FileName = filename;
            Part = new PartCollection();
            Card = new CardCollection();
            Caddie = new CaddieCollection();
            Item = new ItemCollection();
            LevelUpPrizeItem = new LevelUpPrizeItemCollection();
            Character = new CharacterCollection();
            Ball = new BallCollection();
            Ability = new AbilityCollection();
            Skin = new SkinCollection();
            CaddieItem = new CaddieItemCollection();
            Club = new ClubCollection();
            ClubSet = new ClubSetCollection();
            Course = new CourseCollection();
            CutinInformation = new CutinInformationCollection();
            Desc = new DescCollection();
            Furniture = new FurnitureCollection();
            FurnitureAbility = new FurnitureAbilityCollection();
            Mascot = new MascotCollection();
            TikiSpecialTable = new TikiSpecialTableCollection();
            TikiRecipe = new TikiRecipeCollection();
            TikiPointTable = new TikiPointTableCollection();
            CadieMagicBox = new CadieMagicBoxCollection();
            CadieMagicBoxRandom = new CadieMagicBoxRandomCollection();
            HairStyle = new HairStyleCollection();
            Match = new MatchCollection();
            SetItem = new SetItemCollection();
            Enchant = new EnchantCollection();
            Achievement = new AchievementCollection();
            AuxPart = new AuxPartCollection();
            GrandPrixData = new GrandPrixDataCollection();
            GrandPrixRankReward = new GrandPrixRankRewardCollection();
            GrandPrixSpecialHole = new GrandPrixSpecialHoleCollection();
            MemorialShopCoinItem = new MemorialShopCoinItemCollection();
            MemorialShopRareItem = new MemorialShopRareItemCollection();
        }

        public IFFFile()
        {
            FileName = "data/pangya_gb.iff";
            Part = new PartCollection();
            Card = new CardCollection();
            Caddie = new CaddieCollection();
            Item = new ItemCollection();
            LevelUpPrizeItem = new LevelUpPrizeItemCollection();
            Character = new CharacterCollection();
            Ball = new BallCollection();
            Ability = new AbilityCollection();
            Skin = new SkinCollection();
            CaddieItem = new CaddieItemCollection();
            Club = new ClubCollection();
            ClubSet = new ClubSetCollection();
            Course = new CourseCollection();
            CutinInformation = new CutinInformationCollection();
            Desc = new DescCollection();
            Furniture = new FurnitureCollection();
            FurnitureAbility = new FurnitureAbilityCollection();
            Mascot = new MascotCollection();
            TikiSpecialTable = new TikiSpecialTableCollection();
            TikiRecipe = new TikiRecipeCollection();
            TikiPointTable = new TikiPointTableCollection();
            CadieMagicBox = new CadieMagicBoxCollection();
            CadieMagicBoxRandom = new CadieMagicBoxRandomCollection();
            HairStyle = new HairStyleCollection();
            Match = new MatchCollection();
            SetItem = new SetItemCollection();
            Enchant = new EnchantCollection();
            Achievement = new AchievementCollection();
            QuestStuff = new QuestStuffCollection();
            QuestItem = new QuestItemCollection();
            SetEffectTable = new SetEffectTableCollection();
            AuxPart = new AuxPartCollection();
            GrandPrixData = new GrandPrixDataCollection();
            GrandPrixRankReward = new GrandPrixRankRewardCollection();
            GrandPrixSpecialHole = new GrandPrixSpecialHoleCollection();
            MemorialShopCoinItem = new MemorialShopCoinItemCollection();
            MemorialShopRareItem = new MemorialShopRareItemCollection();
        }

        #endregion
    }
}
