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
    public class CaddieItemCollection : List<CaddieItem>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            CaddieItem CaddieItem;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\CaddieItem.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new CaddieItem());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"CaddieItem.iff the structure size is incorrect, Real: {recordLength}, CaddieItem.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        CaddieItem = (CaddieItem)Reader.Read(new CaddieItem());

                        this.Add(CaddieItem);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.CaddieItem");
                return false;
            }
        }

        //Destructor
        ~CaddieItemCollection()
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
            CaddieItem caddieItem = new CaddieItem();
            if (!LoadCaddieItem(ID, ref caddieItem))
            {
                return false;
            }
            if (caddieItem.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID, uint aDay)
        {
            CaddieItem caddieItem = new CaddieItem();
            if (!LoadCaddieItem(ID, ref caddieItem))
            {
                return 99999999;
            }
            return caddieItem.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            CaddieItem caddieItem = new CaddieItem();
            if (!LoadCaddieItem(ID, ref caddieItem))
            {
                return -1;
            }
            return (sbyte)caddieItem.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            CaddieItem caddieItem = new CaddieItem();
            if (!LoadCaddieItem(ID, ref caddieItem))
            {
                return false;
            }
            if (caddieItem.Base.Enabled == 1 && caddieItem.Base.MoneyFlag == 0 || caddieItem.Base.MoneyFlag == MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        bool LoadCaddieItem(uint ID, ref CaddieItem ball)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                ball = load.First();
                return false;
            }
            return true;
        }

        public CaddieItem LoadCaddieItem(uint ID)
        {
            CaddieItem caddieItem = new CaddieItem();
            if (!LoadCaddieItem(ID, ref caddieItem))
            {
                return caddieItem;
            }
            return caddieItem;
        }
    }
}
