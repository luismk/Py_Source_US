using PangyaAPI.BinaryModels;
using Py_Game.IffManager.Data.MemorialShopRareItem;
using Py_Game.ItemList;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
namespace Py_Game.IffManager.Collection
{
   public class MemorialShopRareItemCollection : List<IffMemorialRareItemData>
    {
        List<SpecialItem>  SpecialItemData = null;
        /// <summary>
        /// ID determining relation to other IFF files
        /// </summary>
        public ushort BindingID;

        /// <summary>
        /// Version of this IFF file
        /// </summary>
        public uint Version;
        private void AddSPList()
        {

            SpecialItem SPT;
            // 1. ## Strength Boost x5
            SPT = new SpecialItem
            {
                Number = 1,
                TypeID = 402653188,
                Quantity = 5,
                RareType = uint.MaxValue
            };
             SpecialItemData.Add(SPT);
            // 2. ## Miracle Sign x5
            SPT = new SpecialItem
            {
                Number = 2,
                TypeID = 402653189,
                Quantity = 5,
                RareType = uint.MaxValue
            };
             SpecialItemData.Add(SPT);
            // 3. ## Spin Mastery x5
            SPT = new SpecialItem
            {
                Number = 3,
                TypeID = 402653184,
                Quantity = 5,
                RareType = uint.MaxValue
            };
             SpecialItemData.Add(SPT);
            // 4. ## Curve Mastery x5
            SPT = new SpecialItem
            {
                Number = 4,
                TypeID = 402653185,
                Quantity = 5,
                RareType = uint.MaxValue
            };
             SpecialItemData.Add(SPT);
            // 5. ## Generic Lucky Pangya x5
            SPT = new SpecialItem
            {
                Number = 5,
                TypeID = 402653191,
                Quantity = 5,
                RareType = uint.MaxValue
            };
             SpecialItemData.Add(SPT);
            // 6. ## Generic Nerve Stabilizer x5
            SPT = new SpecialItem
            {
                Number = 6,
                TypeID = 402653192,
                Quantity = 5,
                RareType = uint.MaxValue
            };
             SpecialItemData.Add(SPT);
            // 7. ## Club Modification Kit x1
            SPT = new SpecialItem
            {
                Number = 7,
                TypeID = 436208143,
                Quantity = 1,
            };
             SpecialItemData.Add(SPT);
            // Premium Coin Set No.1
            SPT = new SpecialItem
            {
                Number = 8,
                TypeID = 402653190,
                // ## Silent Wind
                Quantity = 3
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 8,
                TypeID = 436208015,
                // ## Bongdari Clip
                Quantity = 1
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 8,
                TypeID = 335544321,
                // ## Bomber Aztec
                Quantity = 30
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 8,
                TypeID = 436207633,
                // ## Timer Boost
                Quantity = 30
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 8,
                TypeID = 436207680,
                // ## Auto Clipper
                Quantity = 30
            };
             SpecialItemData.Add(SPT);
            // End Premium Coin
            // Premium Coin Set No.2
            SPT = new SpecialItem
            {
                Number = 9,
                TypeID = 436208145,
                // ## UCIM CHIP
                Quantity = 2
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 9,
                TypeID = 335544342,
                // ## Watermelon Aztec
                Quantity = 40
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 9,
                TypeID = 402653224,
                // ## Safe Tee
                Quantity = 5
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 9,
                TypeID = 436207633,
                // ## Timer Boost
                Quantity = 100
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 9,
                TypeID = 436207680,
                // ## Auto Clipper
                Quantity = 100
            };
             SpecialItemData.Add(SPT);
            // End Premium Coin
            // Premium Coin Set No.3
            SPT = new SpecialItem
            {
                Number = 10,
                TypeID = 436208144,
                // ## Abbot Coating
                Quantity = 3
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 10,
                TypeID = 335544332,
                // ## Clover Aztec
                Quantity = 50
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 10,
                TypeID = 402653223,
                // ## Double Strength Boost
                Quantity = 10
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 10,
                TypeID = 436207815,
                // ## Air Note
                Quantity = 60
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 10,
                TypeID = 436207633,
                // ## Timer Boost
                Quantity = 60
            };
             SpecialItemData.Add(SPT);
            // End Premium Coin
            // Premium Coin Set No.4
            SPT = new SpecialItem
            {
                Number = 11,
                TypeID = 2092957696,
                // ## Card Pack No. 1
                Quantity = 1
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 11,
                TypeID = 335544350,
                // ## Sakura Aztec
                Quantity = 50
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 11,
                TypeID = 402653230,
                // ## Double P.Strength Boost
                Quantity = 3
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 11,
                TypeID = 436207618,
                // ## Pang Mastery
                Quantity = 20
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 11,
                TypeID = 436207633,
                // ## Timer Boost
                Quantity = 50
            };
             SpecialItemData.Add(SPT);
            // End Premium Coin
            // Premium Coin Set No.5
            SPT = new SpecialItem
            {
                Number = 12,
                TypeID = 2092957700,
                // ## Card Pack No.2
                Quantity = 1
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 12,
                TypeID = 335544369,
                // ## Halloween Skull Aztec
                Quantity = 30
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 12,
                TypeID = 402653194,
                // ## Dual Lucky Pangya
                Quantity = 5
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 12,
                TypeID = 436207618,
                // ## Pang Mastery
                Quantity = 30
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 12,
                TypeID = 436207633,
                // ## Timer Boost
                Quantity = 50
            };
             SpecialItemData.Add(SPT);
            // End Premium Coin
            // Premium Coin Set No.6
            SPT = new SpecialItem
            {
                Number = 13,
                TypeID = 2092957701,
                // ## Card Pack No.3
                Quantity = 1
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 13,
                TypeID = 335544352,
                // ## Rainbow Aztec
                Quantity = 30
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 13,
                TypeID = 402653195,
                // ## Dual Tran
                Quantity = 5
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 13,
                TypeID = 436207618,
                // ## Pang Mastery
                Quantity = 30
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 13,
                TypeID = 436207680,
                // ## Auto Clipper
                Quantity = 40
            };
             SpecialItemData.Add(SPT);
            // End Premium Coin
            // Premium Coin Set No.7
            SPT = new SpecialItem
            {
                Number = 14,
                TypeID = 2092957703,
                // ## Card Pack No.4
                Quantity = 1
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 14,
                TypeID = 335544465,
                // ## Smiling Goblin Aztec
                Quantity = 50
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 14,
                TypeID = 402653223,
                // ## Double Strength Boost
                Quantity = 5
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 14,
                TypeID = 436207633,
                // ## Timer Boost
                Quantity = 50
            };
             SpecialItemData.Add(SPT);
            SPT = new SpecialItem
            {
                Number = 14,
                TypeID = 436207680,
                // ## Auto Clipper
                Quantity = 50
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 15,
                TypeID = 436207709,
                // ##Fragment of time (Fall)
                Quantity = 1
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 16,
                TypeID = 436207707,
                // ##Fragment of time (Spring)
                Quantity = 1
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 17,
                TypeID = 436207708,
                // ##Fragment of time (Summer)
                Quantity = 1
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 18,
                TypeID = 436207710,
                // ##Fragment of time (Winter)
                Quantity = 1
            };
             SpecialItemData.Add(SPT);

            // NOVOS ITENS!
            SPT = new SpecialItem
            {
                Number = 19,
                TypeID = 436208242, // Moeda do Memorial Premium
                Quantity = 5,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 20,
                TypeID = 608256015, // Patinhas Spika
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 21,
                TypeID = 608174132, // Patinhas Nuri
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 22,
                TypeID = 608182322, // Patinhas Hana
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 23,
                TypeID = 608190501, // Patinhas Azer
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 24,
                TypeID = 608198722, // Patinhas Cecilia
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 25,
                TypeID = 608206889, // Patinhas Max
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 26,
                TypeID = 608215139, // Patinhas Kooh
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 27,
                TypeID = 608223292, // Patinhas Arin
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 28,
                TypeID = 608231468, // Patinhas Kaz
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 29,
                TypeID = 608239698, // Patinhas Lucia
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);

            SPT = new SpecialItem
            {
                Number = 30,
                TypeID = 608247843, // Patinhas Nell
                Quantity = 1,
                RareType = 4
            };
             SpecialItemData.Add(SPT);
            // End Premium Coin
            // for SPT in SpItem do
            // WriteLn(Format('%d %s %d' ,[SPT.Number, IffEntry.GetItemName(SPT.TypeID), SPT.Quantity]));

        }
        //Constructor 
        public MemorialShopRareItemCollection()
        {
            MemoryStream Buffer;
            IffMemorialRareItemData Rare;
             SpecialItemData = new List<SpecialItem>();

            PangyaBinaryReader Reader = null;

            using (var zip = ZipFile.OpenRead("data/pangya_gb.iff"))//ler o arquivo de base
            {
                var FileZip = zip.Entries.FirstOrDefault(c => c.Name == "MemorialShopRareItem.iff");//verifica se existe o arquivo

                if (FileZip == null)
                {
                    PangyaAPI.WriteConsole.WriteLine(" data\\MemorialShopRareItem.iff is not loaded");
                    return;
                }
                else
                {
                    Buffer = new MemoryStream();
                    FileZip.Open().CopyTo(Buffer);
                }
            }

            try
            {
                Reader = new PangyaBinaryReader(Buffer);
                if (new string(Reader.ReadChars(2)) == "PK")
                {
                    throw new Exception("The given IFF file is a ZIP file, please unpack it before attempting to parse it");
                }

                Reader.Seek(0, 0);

                Reader.ReadUInt16(out ushort recordCount);
                long recordLength = ((Reader.GetSize() - 8L) / (recordCount));
                Reader.ReadUInt16(out BindingID);
                Reader.ReadUInt32(out Version);

                for (int i = 0; i < recordCount; i++)
                {
                    Rare = (IffMemorialRareItemData)Reader.Read(new IffMemorialRareItemData());
                    this.Add(Rare);
                }
                this.AddSPList();
            }
            finally
            {
                Reader.Dispose();
                Buffer.Dispose();
            }

        }
       
        public IffMemorialRareItemData GetItem(UInt32 TypeID)
        {
            foreach (var item in this)
            {
                if (item.TypeID == TypeID)
                {
                    return item;
                }
            }
            return new IffMemorialRareItemData();
        }
        public List<SpecialItem> GetNormalItem(UInt32 TypeID)
        {
            Random rnd;
            byte PairNum;
            List<SpecialItem> result = new List<SpecialItem>();
            switch (TypeID)
            {
                case 436208242:
                    rnd = new Random();
                    PairNum = (byte)rnd.Next(8, 14);
                    break;
                default:
                    rnd = new Random();
                    PairNum = (byte)rnd.Next(1, 7);
                    break;
            }
            foreach (SpecialItem SpecialItem in  SpecialItemData)
            {
                if (SpecialItem.Number == PairNum)
                {
                    result.Add(SpecialItem);
                }
            }
            return result;
        }
        public ItemRandomClass GetRareItem(UInt32 CoinTypeID, byte Pooling)
        {
            ItemRandomClass result;
            result = new ItemRandomClass();
            switch (Pooling)
            {
                case 0:
                    foreach (IffMemorialRareItemData Item in this)
                    {
                        result.AddItems(Item.TypeID, 1, Item.RareType, (ushort)result.rnd.Next((int)Item.Probabilities - result.rnd.Next(20)));
                    }
                    break;
                default:
                    foreach (IffMemorialRareItemData Item in this)
                    {
                        if (Item.CharacterType == Pooling)
                        {
                            result.AddItems(Item.TypeID, 1, Item.RareType, (ushort)result.rnd.Next((int)Item.Probabilities + result.rnd.Next(20)));
                        }
                    }
                    break;
            }
            result.SetCanDup(false);
            result.Arrange();
            return result;
        }
    }
}
