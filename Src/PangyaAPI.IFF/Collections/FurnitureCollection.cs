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
    public class FurnitureCollection : List<Furniture>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            Furniture Furniture;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Furniture.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Furniture());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Furniture.iff the structure size is incorrect, Real: {recordLength}, Furniture.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Furniture = (Furniture)Reader.Read(new Furniture());

                        this.Add(Furniture);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Furniture");
                return false;
            }
        }

        //Destructor
        ~FurnitureCollection()
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
            Furniture Furniture = new Furniture();
            if (!LoadFurniture(ID, ref Furniture))
            {
                return false;
            }
            if (Furniture.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            Furniture Furniture = new Furniture();
            if (!LoadFurniture(ID, ref Furniture))
            {
                return 99999999;
            }
            return Furniture.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            Furniture Furniture = new Furniture();
            if (!LoadFurniture(ID, ref Furniture))
            {
                return -1;
            }
            return (sbyte)Furniture.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            Furniture Furniture = new Furniture();
            if (!LoadFurniture(ID, ref Furniture))
            {
                return false;
            }
            if (Furniture.Base.Enabled == 1 && Furniture.Base.MoneyFlag == 0 || Furniture.Base.MoneyFlag == Flags.MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        bool LoadFurniture(uint ID, ref Furniture Furniture)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                Furniture = load.First();
                return false;
            }
            return true;
        }

        public Furniture LoadFurniture(uint ID)
        {
            Furniture Furniture = new Furniture();
            if (!LoadFurniture(ID, ref Furniture))
            {
                return Furniture;
            }
            return Furniture;
        }
    }
}
