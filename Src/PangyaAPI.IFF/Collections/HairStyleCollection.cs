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
    public class HairStyleCollection : List<HairStyle>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        #endregion

        public bool Load(MemoryStream data)
        {
            HairStyle HairStyle;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\HairStyle.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new HairStyle());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"HairStyle.iff the structure size is incorrect, Real: {recordLength}, HairStyle.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        HairStyle = (HairStyle)Reader.Read(new HairStyle());

                        this.Add(HairStyle);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.HairStyle");
                return false;
            }
        }

        //Destructor
        ~HairStyleCollection()
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
            HairStyle HairStyle = new HairStyle();
            if (!LoadHairStyle(ID, ref HairStyle))
            {
                return false;
            }
            if (HairStyle.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            HairStyle HairStyle = new HairStyle();
            if (!LoadHairStyle(ID, ref HairStyle))
            {
                return 99999999;
            }
            return HairStyle.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            HairStyle HairStyle = new HairStyle();
            if (!LoadHairStyle(ID, ref HairStyle))
            {
                return -1;
            }
            return (sbyte)HairStyle.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            HairStyle HairStyle = new HairStyle();
            if (!LoadHairStyle(ID, ref HairStyle))
            {
                return false;
            }
            if (HairStyle.Base.Enabled == 1 && HairStyle.Base.MoneyFlag == 0 || HairStyle.Base.MoneyFlag == Flags.MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }

        public HairStyle GetItem(uint TypeID)
        {
            HairStyle HairStyle = new HairStyle();
            if (!LoadHairStyle(TypeID, ref HairStyle))
            {
                return HairStyle;
            }
            return HairStyle;
        }

        bool LoadHairStyle(uint ID, ref HairStyle HairStyle)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                HairStyle = load.First();
                return false;
            }
            return true;
        }

        public HairStyle LoadHairStyle(uint ID)
        {
            HairStyle HairStyle = new HairStyle();
            if (!LoadHairStyle(ID, ref HairStyle))
            {
                return HairStyle;
            }
            return HairStyle;
        }

    }
}
