using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Collections
{
    public class ItemCollection : List<Item>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion
        public bool Load(MemoryStream data)
        {
            Item Item;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Item.iff is not loaded", "Pangya.IFF");
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


                    var IffStructSize = Tools.IFFTools.SizeStruct(new Item());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Item.iff the structure size is incorrect, Real: {recordLength}, Item.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Item = (Item)Reader.Read(new Item());

                        this.Add(Item);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Item");
                return false;
            }
        }

        //Destructor
        ~ItemCollection()
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
            Item Item = new Item();
            if (!LoadItem(ID, ref Item))
            {
                return false;
            }
            if (Item.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            Item Item = new Item();
            if (!LoadItem(ID, ref Item))
            {
                return 99999999;
            }
            return Item.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            Item Item = new Item();
            if (!LoadItem(ID, ref Item))
            {
                return -1;
            }
            return (sbyte)Item.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            Item Item = new Item();
            if (!LoadItem(ID, ref Item))
            {
                return false;
            }
            if (Item.Base.Enabled == 1 && Item.Base.MoneyFlag == 0 || Item.Base.MoneyFlag == Flags.MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        public UInt32 GetRealQuantity(UInt32 TypeId, UInt32 Qty)
        {
            Item Item = new Item();
            if (!LoadItem(TypeId, ref Item))
            {
                return 0;
            }
            if ((Item.Base.Enabled == 1) && (Item.Power > 0))
            {
                return Item.Power;
            }
            return Qty;
        }

        bool LoadItem(uint ID, ref Item Item)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                Item = load.First();
                return false;
            }
            return true;
        }

        public Item LoadItem(uint ID)
        {
            Item Item = new Item();
            if (!LoadItem(ID, ref Item))
            {
                return Item;
            }
            return Item;
        }
    }
}
