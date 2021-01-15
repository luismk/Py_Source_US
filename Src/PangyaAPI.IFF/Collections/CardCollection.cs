using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using PangyaAPI.IFF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Collections
{
    public class CardCollection : List<Card>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            Card Card;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Card.iff is not loaded", "Pangya.IFF");
                return false;
            }

            try
            {
                using (var Reader = new PangyaBinaryReader(data))
                {
                    if (new string(Reader.ReadChars(2)) == "PK")
                    {
                        throw new Exception("The given IFF file is a ZIP file, please unpack it before attempting to parse it");
                    }
                    Reader.Seek(0, 0);

                    IFF_FILE_HEADER = (IFFHeader)Reader.Read(new IFFHeader());

                    long recordLength = (Reader.GetSize - 8L) / IFF_FILE_HEADER.RecordCount;

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Card());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Card.iff the structure size is incorrect, Real: {recordLength}, Card.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Card = (Card)Reader.Read(new Card());

                        this.Add(Card);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Card");
                return false;
            }
        }

        //Destructor
        ~CardCollection()
        {
            this.Clear();
        }

        public string GetItemName(uint ID)
        {
            foreach (var item in this)
            {
                if (item.Base.TypeID == ID)
                {
                    return item.Base.Name;
                }
            }
            return "";
        }

        public bool IsExist(uint ID)
        {
            Card card = new Card();
            if (!LoadCard(ID, ref card))
            {
                return false;
            }
            if (card.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            Card card = new Card();
            if (!LoadCard(ID, ref card))
            {
                return 99999999;
            }
            return card.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            Card card = new Card();
            if (!LoadCard(ID, ref card))
            {
                return -1;
            }
            return (sbyte)card.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            Card card = new Card();
            if (!LoadCard(ID, ref card))
            {
                return false;
            }
            if (card.Base.Enabled == 1 && card.Base.MoneyFlag == 0 || card.Base.MoneyFlag == MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        bool LoadCard(uint ID, ref Card ball)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                ball = load.First();
                return false;
            }
            return true;
        }

        public Card LoadCard(uint ID)
        {
            Card Card = new Card();
            if (!LoadCard(ID, ref Card))
            {
                return Card;
            }
            return Card;
        }
    }
}
