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
    public class SkinCollection : List<Skin>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        #endregion

        public bool Load(MemoryStream data)
        {
            Skin Skin;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Skin.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Skin());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Skin.iff the structure size is incorrect, Real: {recordLength}, Skin.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Skin = (Skin)Reader.Read(new Skin());

                        this.Add(Skin);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Skin");
                return false;
            }
        }

        //Destructor
        ~SkinCollection()
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
            Skin Skin = new Skin();
            if (!LoadSkin(ID, ref Skin))
            {
                return false;
            }
            if (Skin.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID, uint aDay)
        {
            Skin Skin = new Skin();
            if (!LoadSkin(ID, ref Skin))
            {
                return 99999999;
            }
            return Skin.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            Skin Skin = new Skin();
            if (!LoadSkin(ID, ref Skin))
            {
                return -1;
            }
            return (sbyte)Skin.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            Skin Skin = new Skin();
            if (!LoadSkin(ID, ref Skin))
            {
                return false;
            }
            if (Skin.Base.Enabled == 1 && Skin.Base.MoneyFlag == 0 || Skin.Base.MoneyFlag == Flags.MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        public byte GetSkinFlag(uint TypeId)
        {
            Skin Items = new Skin();
            if (!LoadSkin(TypeId, ref Items))
            {
                return 0;
            }
            if ((Items.Base.TypeID == TypeId) && (Items.Base.Enabled == 1))
            {
                if ((Items.Price7 == 0) && (Items.Price30 == 0) && (Items.PriceUnk == 0))
                {
                    return 0;
                }
                else
                {
                    return 0x20;
                }
            }
            return 0;
        }


        bool LoadSkin(uint ID, ref Skin Skin)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                Skin = load.First();
                return false;
            }
            return true;
        }

        public Skin LoadSkin(uint ID)
        {
            Skin Skin = new Skin();
            if (!LoadSkin(ID, ref Skin))
            {
                return Skin;
            }
            return Skin;
        }
    }
}
