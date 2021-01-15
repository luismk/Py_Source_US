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
    public class SetItemCollection : List<SetItem>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            SetItem SetItem;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\SetItem.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new SetItem());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"SetItem.iff the structure size is incorrect, Real: {recordLength}, SetItem.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        SetItem = (SetItem)Reader.Read(new SetItem());

                        this.Add(SetItem);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.SetItem");
                return false;
            }
        }

        //Destructor
        ~SetItemCollection()
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
            SetItem SetItem = new SetItem();
            if (!LoadSetItem(ID, ref SetItem))
            {
                return false;
            }
            if (SetItem.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            SetItem SetItem = new SetItem();
            if (!LoadSetItem(ID, ref SetItem))
            {
                return 99999999;
            }
            return SetItem.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            SetItem SetItem = new SetItem();
            if (!LoadSetItem(ID, ref SetItem))
            {
                return -1;
            }
            return (sbyte)SetItem.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            SetItem SetItem = new SetItem();
            if (!LoadSetItem(ID, ref SetItem))
            {
                return false;
            }
            if (SetItem.Base.Enabled == 1 && SetItem.Base.MoneyFlag == 0 || SetItem.Base.MoneyFlag == Flags.MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        bool LoadSetItem(uint ID, ref SetItem SetItem)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                SetItem = load.First();
                return false;
            }
            return true;
        }

        public SetItem LoadSetItem(uint ID)
        {
            SetItem SetItem = new SetItem();
            if (!LoadSetItem(ID, ref SetItem))
            {
                return SetItem;
            }
            return SetItem;
        }

        public List<Dictionary<uint, uint>> SetList(uint TypeID)
        {
            List<Dictionary<uint, uint>> result;
            SetItem Items = new SetItem();
            byte Count;
            result = new List<Dictionary<uint, uint>>();
            if (!LoadSetItem(TypeID, ref Items))
            {
                return result;
            }
            for (Count = 0; Count <= Items.Part_TypeID.Length - 1; Count++)
            {
                if (Items.Part_TypeID[Count] > 0)
                {
                    if (Items.Part_Qty[Count] <= 0)
                    {
                        Items.Part_Qty[Count] = 1;
                    }
                    result.Add(new Dictionary<uint, uint>() { { Items.Part_TypeID[Count], Items.Part_Qty[Count] } });
                }
            }
            return result;
        }

    }
}
