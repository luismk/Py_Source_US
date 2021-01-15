using PangyaAPI.Helper.Tools;
using PangyaAPI.IFF.Flags;
using PangyaAPI.IFF.Tools;
using PangyaAPI.ZIP.Tools;
using System;
using System.IO;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Manager
{
    public partial class IFFFile
    {
        #region Methods 

        public void SetFileName(string name)
        {
            this.FileName = name;
        }

        public void LoadIff()
        {
            bool result;
            try
            {
                if (Directory.Exists("data") && File.Exists(FileName))
                {
                    var zip = ZipFileEx.Open(FileName);

                    result = Part.Load(zip.GetFileData("Part.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Part]: Items Loads {Part.Count}", ConsoleColor.Cyan);
                    }

                    result = Card.Load(zip.GetFileData("Card.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Card]: Items Loads {Card.Count}", ConsoleColor.Cyan);
                    }

                    result = Caddie.Load(zip.GetFileData("Caddie.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Caddie]: Items Loads {Caddie.Count}", ConsoleColor.Cyan);
                    }

                    result = Item.Load(zip.GetFileData("Item.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Item]: Items Loads {Item.Count}", ConsoleColor.Cyan);
                    }

                    result = LevelUpPrizeItem.Load(zip.GetFileData("LevelUpPrizeItem.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.LevelUpPrizeItem]: Items Loads {LevelUpPrizeItem.Count}", ConsoleColor.Cyan);
                    }

                    result = Character.Load(zip.GetFileData("Character.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Character]: Items Loads {Character.Count}", ConsoleColor.Cyan);
                    }

                    result = Ball.Load(zip.GetFileData("Ball.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Ball]: Items Loads {Ball.Count}", ConsoleColor.Cyan);
                    }

                    result = Ability.Load(zip.GetFileData("Ability.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Ability]: Items Loads {Ability.Count}", ConsoleColor.Cyan);
                    }

                    result = Skin.Load(zip.GetFileData("Skin.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Skin]: Items Loads {Skin.Count}", ConsoleColor.Cyan);
                    }
                    result = CaddieItem.Load(zip.GetFileData("CaddieItem.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.CaddieItem]: Items Loads {CaddieItem.Count}", ConsoleColor.Cyan);
                    }

                    result = Club.Load(zip.GetFileData("Club.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Club]: Items Loads {Club.Count}", ConsoleColor.Cyan);
                    }

                    result = ClubSet.Load(zip.GetFileData("ClubSet.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.ClubSet]: Items Loads {ClubSet.Count}", ConsoleColor.Cyan);
                    }

                    result = Course.Load(zip.GetFileData("Course.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Course]: Items Loads {Course.Count}", ConsoleColor.Cyan);
                    }

                    result = CutinInformation.Load(zip.GetFileData("CutinInfomation.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.CutinInformation]: Items Loads {CutinInformation.Count}", ConsoleColor.Cyan);
                    }

                    result = Desc.Load(zip.GetFileData("Desc.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Desc]: Items Loads {Desc.Count}", ConsoleColor.Cyan);
                    }

                    result = Furniture.Load(zip.GetFileData("Furniture.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Furniture]: Items Loads {Furniture.Count}", ConsoleColor.Cyan);
                    }

                    result = FurnitureAbility.Load(zip.GetFileData("FurnitureAbility.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.FurnitureAbility]: Items Loads {FurnitureAbility.Count}", ConsoleColor.Cyan);
                    }

                    result = Mascot.Load(zip.GetFileData("Mascot.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Mascot]: Items Loads {Mascot.Count}", ConsoleColor.Cyan);
                    }

                    result = TikiSpecialTable.Load(zip.GetFileData("TikiSpecialTable.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.TikiSpecialTable]: Items Loads {TikiSpecialTable.Count}", ConsoleColor.Cyan);
                    }

                    result = TikiRecipe.Load(zip.GetFileData("TikiRecipe.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.TikiRecipe]: Items Loads {TikiRecipe.Count}", ConsoleColor.Cyan);
                    }

                    result = TikiPointTable.Load(zip.GetFileData("TikiPointTable.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.TikiPointTable]: Items Loads {TikiPointTable.Count}", ConsoleColor.Cyan);
                    }

                    result = CadieMagicBox.Load(zip.GetFileData("CadieMagicBox.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.CadieMagicBox]: Items Loads {CadieMagicBox.Count}", ConsoleColor.Cyan);
                    }

                    result = CadieMagicBoxRandom.Load(zip.GetFileData("CadieMagicBoxRandom.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.CadieMagicBoxRandom]: Items Loads {CadieMagicBoxRandom.Count}", ConsoleColor.Cyan);
                    }

                    result = HairStyle.Load(zip.GetFileData("HairStyle.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.HairStyle]: Items Loads {HairStyle.Count}", ConsoleColor.Cyan);
                    }

                    result = Match.Load(zip.GetFileData("Match.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Match]: Items Loads {Match.Count}", ConsoleColor.Cyan);
                    }

                    result = SetItem.Load(zip.GetFileData("SetItem.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.SetItem]: Items Loads {SetItem.Count}", ConsoleColor.Cyan);
                    }

                    result = Enchant.Load(zip.GetFileData("Enchant.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Enchant]: Items Loads {Enchant.Count}", ConsoleColor.Cyan);
                    }

                    result = Achievement.Load(zip.GetFileData("Achievement.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.Achievement]: Items Loads {Achievement.Count}", ConsoleColor.Cyan);
                    }

                    result = QuestItem.Load(zip.GetFileData("QuestItem.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.QuestItem]: Items Loads {QuestItem.Count}", ConsoleColor.Cyan);
                    }

                    result = QuestStuff.Load(zip.GetFileData("QuestStuff.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.QuestStuff]: Items Loads {QuestStuff.Count}", ConsoleColor.Cyan);
                    }

                    result = SetEffectTable.Load(zip.GetFileData("SetEffectTable.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.SetEffectTable]: Items Loads {SetEffectTable.Count}", ConsoleColor.Cyan);
                    }

                    result = AuxPart.Load(zip.GetFileData("AuxPart.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.AuxPart]: Items Loads {AuxPart.Count}", ConsoleColor.Cyan);
                    }

                    result = MemorialShopCoinItem.Load(zip.GetFileData("MemorialShopCoinItem.sff"));
                    if (result)
                    {
                        WriteConsole.WriteLine($"[Pang.IFF.MemorialShopCoinItem]: Items Loads {MemorialShopCoinItem.Count}", ConsoleColor.Cyan);
                    }

                    result = MemorialShopRareItem.Load(zip.GetFileData("MemorialShopRareItem.iff"));
                    if (result)
                    {
                        WriteConsole.WriteLine($"[Pang.IFF.MemorialShopRareItem]: Items Loads {MemorialShopRareItem.Count}", ConsoleColor.Cyan);
                    }

                    result = GrandPrixData.Load(zip.GetFileData("GrandPrixData.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.GrandPrixData]: Items Loads {GrandPrixData.Count}", ConsoleColor.Cyan);
                    }

                    result = GrandPrixRankReward.Load(zip.GetFileData("GrandPrixRankReward.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.GrandPrixRankReward]: Items Loads {GrandPrixRankReward.Count}", ConsoleColor.Cyan);
                    }

                    result = GrandPrixSpecialHole.Load(zip.GetFileData("GrandPrixSpecialHole.iff"));
                    if (result)
                    {
                       WriteConsole.WriteLine($"[Pang.IFF.GrandPrixSpecialHole]: Items Loads {GrandPrixSpecialHole.Count}", ConsoleColor.Cyan);
                    }
                }
                else
                {
                    throw new Exception(" Failed to attempt to load data from file not found: " + FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pang.IFF.Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Rewriter Pangya_GB.iff
        /// </summary>
        public void Rebuilding()
        {
            if (this.Part.Update)
            { }
            if (this.Card.Update)
            { }
        }

        public uint GetRealQuantity(uint TypeID, uint Qty)
        {
            switch (TypeID.GetItemGroup())
            {
                case IffGroupFlag.ITEM_TYPE_USE:
                    return Item.GetRealQuantity(TypeID, Qty);
                case IffGroupFlag.ITEM_TYPE_BALL:
                    return Ball.GetRealQuantity(TypeID, Qty);
            }
            return Qty;
        }

        public uint GetRentalPrice(uint TypeID)
        {
            if (!(TypeID.GetItemGroup() == IffGroupFlag.ITEM_TYPE_PART))
            {
                return 0;
            }
            return Part.GetRentalPrice(TypeID);
        }

        /// <summary>
        /// Get Item Name by TypeID
        /// </summary>   
        public string GetName(uint TypeID)
        {
            switch (TypeID.GetItemGroup())
            {
                case IffGroupFlag.ITEM_TYPE_CHARACTER:
                    return Character.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_PART:
                    //Part
                    return Part.GetItemName(TypeID);
                case IffGroupFlag.ITEM_TYPE_HAIR_STYLE:
                    {
                        return HairStyle.GetItemName(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_CLUB:
                    return ClubSet.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_BALL:
                    // Ball
                    return Ball.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_USE:
                    // Normal Item
                    return Item.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_CADDIE:
                    // Cadie
                    return Caddie.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_CADDIE_ITEM:
                    return CaddieItem.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_SETITEM:
                    // Part
                    return SetItem.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_SKIN:
                    return Skin.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_MASCOT:
                    return Mascot.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_CARD:
                    return Card.GetItemName(TypeID);

                case IffGroupFlag.ITEM_TYPE_AUX:
                    return AuxPart.GetItemName(TypeID);

            }
            return "Unknown Item Name";
        }

        public byte GetItemTimeFlag(uint TypeID, uint Day)
        {
            switch (TypeID.GetItemGroup())
            {
                case IffGroupFlag.ITEM_TYPE_CADDIE:
                    if (Caddie.GetSalary(TypeID) > 0)
                    {
                        return 2;
                    }
                    return 0;
                case IffGroupFlag.ITEM_TYPE_MASCOT:
                    if (Mascot.GetSalary(TypeID, Day) > 0)
                    {
                        return 2;
                    }
                    return 0;
                case IffGroupFlag.ITEM_TYPE_SKIN:
                    // SKIN FLAG
                    return Skin.GetSkinFlag(TypeID);
                default:
                    return 0;
            }
        }

        public uint GetPrice(uint TypeID, uint ADay)
        {
            switch (TypeID.GetItemGroup())
            {
                case IffGroupFlag.ITEM_TYPE_BALL:
                    return Ball.GetPrice(TypeID);

                case IffGroupFlag.ITEM_TYPE_CLUB:
                    return ClubSet.GetPrice(TypeID);

                case IffGroupFlag.ITEM_TYPE_CHARACTER:
                    return Character.GetPrice(TypeID);

                case IffGroupFlag.ITEM_TYPE_PART:
                    return Part.GetPrice(TypeID);

                case IffGroupFlag.ITEM_TYPE_HAIR_STYLE:
                    return HairStyle.GetPrice(TypeID);

                case IffGroupFlag.ITEM_TYPE_USE:
                    return Item.GetPrice(TypeID);

                case IffGroupFlag.ITEM_TYPE_CADDIE:
                    return Caddie.GetPrice(TypeID);

                case IffGroupFlag.ITEM_TYPE_CADDIE_ITEM:
                    return CaddieItem.GetPrice(TypeID, ADay);

                case IffGroupFlag.ITEM_TYPE_SETITEM:
                    return SetItem.GetPrice(TypeID);

                case IffGroupFlag.ITEM_TYPE_SKIN:
                    return Skin.GetPrice(TypeID, ADay);

                case IffGroupFlag.ITEM_TYPE_MASCOT:
                    return Mascot.GetPrice(TypeID, ADay);

                case IffGroupFlag.ITEM_TYPE_CARD:
                    return Card.GetPrice(TypeID);

            }
            return 0;
        }

        public sbyte GetShopPriceType(uint TypeID)
        {
            switch (TypeID.GetItemGroup())
            {
                case IffGroupFlag.ITEM_TYPE_BALL:
                    return Ball.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_CLUB:
                    return ClubSet.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_CHARACTER:
                    return Character.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_PART:
                    return Part.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_HAIR_STYLE:
                    return HairStyle.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_USE:
                    return Item.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_CADDIE:
                    return Caddie.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_CADDIE_ITEM:
                    return CaddieItem.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_SETITEM:
                    return SetItem.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_SKIN:
                    return Skin.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_MASCOT:
                    return Mascot.GetShopPriceType(TypeID);

                case IffGroupFlag.ITEM_TYPE_CARD:
                    return Card.GetShopPriceType(TypeID);

            }
            return 0;
        }

        public bool IsBuyable(uint TypeID)
        {
            switch (TypeID.GetItemGroup())
            {
                case IffGroupFlag.ITEM_TYPE_BALL:
                    {
                        return Ball.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_CLUB:
                    {
                        return ClubSet.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_CHARACTER:
                    {
                        return Character.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_PART:
                    {
                        return Part.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_HAIR_STYLE:
                    {
                        return HairStyle.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_USE:
                    {
                        return Item.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_CADDIE:
                    {
                        return Caddie.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_CADDIE_ITEM:
                    {
                        return CaddieItem.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_SETITEM:
                    {
                        return SetItem.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_SKIN:
                    {
                        return Skin.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_MASCOT:
                    {
                        return Mascot.IsBuyable(TypeID);
                    }
                case IffGroupFlag.ITEM_TYPE_CARD:
                    {
                        return Card.IsBuyable(TypeID);
                    }

            }
            return false;
        }

        public Models.HairStyle GetByHairColor(uint itemIffId)
        {
            return HairStyle.GetItem(itemIffId);
        }

        public bool IsExist(uint TypeID)
        {
            switch (TypeID.GetItemGroup())
            {
                case IffGroupFlag.ITEM_TYPE_CLUB:
                    return ClubSet.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_CHARACTER:
                    return Character.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_PART:
                    //  Part
                    return Part.IsExist(TypeID);
                //Hair
                case IffGroupFlag.ITEM_TYPE_HAIR_STYLE:
                    return HairStyle.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_BALL:
                    //  Ball
                    return Ball.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_USE:
                    // Normal Item
                    return Item.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_CADDIE:
                    return Caddie.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_CADDIE_ITEM:
                    return CaddieItem.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_SETITEM:
                    return SetItem.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_SKIN:
                    return Skin.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_MASCOT:
                    return Mascot.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_CARD:
                    return Card.IsExist(TypeID);

                case IffGroupFlag.ITEM_TYPE_AUX:
                    return AuxPart.IsExist(TypeID);

            }
            return false;
        }

        public bool IsSelfDesign(uint TypeID)
        {
            switch (TypeID)
            {
                case 134258720:
                case 134242351:
                case 134258721:
                case 134242355:
                case 134496433:
                case 134496434:
                case 134512665:
                case 134496344:
                case 134512666:
                case 134496345:
                case 134783001:
                case 134758439:
                case 134783002:
                case 134758443:
                case 135020720:
                case 135020721:
                case 135045144:
                case 135020604:
                case 135045145:
                case 135020607:
                case 135299109:
                case 135282744:
                case 135299110:
                case 135282745:
                case 135545021:
                case 135545022:
                case 135569438:
                case 135544912:
                case 135569439:
                case 135544915:
                case 135807173:
                case 135807174:
                case 135823379:
                case 135807066:
                case 135823380:
                case 135807067:
                case 136093719:
                case 136069163:
                case 136093720:
                case 136069166:
                case 136331407:
                case 136331408:
                case 136355843:
                case 136331271:
                case 136355844:
                case 136331272:
                case 136593549:
                case 136593550:
                case 136617986:
                case 136593410:
                case 136617987:
                case 136593411:
                case 136880144:
                case 136855586:
                case 136880145:
                case 136855587:
                case 136855588:
                case 136855589:
                case 137379868:
                case 137379869:
                case 137404426:
                case 137379865:
                case 137404427:
                case 137379866:
                case 137904143:
                case 137904144:
                case 137928708:
                case 137904140:
                case 137928709:
                case 137904141:
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
}
