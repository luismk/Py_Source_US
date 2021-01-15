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
    public class PartCollection : List<Part>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion
        public bool Load(MemoryStream data)
        {
            Part Part;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Part.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Part());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Part.iff the structure size is incorrect, Real: {recordLength}, Part.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Part = (Part)Reader.Read(new Part());

                        this.Add(Part);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Part");
                return false;
            }
        }

        //Destructor
        ~PartCollection()
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
            Part Part = new Part();
            if (!LoadPart(ID, ref Part))
            {
                return false;
            }
            if (Part.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            Part Part = new Part();
            if (!LoadPart(ID, ref Part))
            {
                return 99999999;
            }
            return Part.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            Part Part = new Part();
            if (!LoadPart(ID, ref Part))
            {
                return -1;
            }
            return (sbyte)Part.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            Part Part = new Part();
            if (!LoadPart(ID, ref Part))
            {
                return false;
            }
            if (Part.Base.Enabled == 1 && Part.Base.MoneyFlag == 0 || Part.Base.MoneyFlag == Flags.MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }



        public UInt32 GetRentalPrice(UInt32 TypeId)
        {
            Part Part = new Part();
            if (!LoadPart(TypeId, ref Part))
            {
                return 0;
            }
            if ((Part.Base.Enabled == 1))
            {
                return Part.RentPang;
            }
            return 0;
        }

        bool LoadPart(uint ID, ref Part Part)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                Part = load.First();
                return false;
            }
            return true;
        }

        public Part LoadPart(uint ID)
        {
            Part Part = new Part();
            if (!LoadPart(ID, ref Part))
            {
                return Part;
            }
            return Part;
        }
    }
}
