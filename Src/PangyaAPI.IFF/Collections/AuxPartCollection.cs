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
    public class AuxPartCollection : List<AuxPart>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            AuxPart AuxPart;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\AuxPart.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new AuxPart());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"AuxPart.iff the structure size is incorrect, Real: {recordLength}, AuxPart.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        AuxPart = (AuxPart)Reader.Read(new AuxPart());

                        this.Add(AuxPart);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.AuxPart");
                return false;
            }
        }

        //Destructor
        ~AuxPartCollection()
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
            AuxPart auxPart = new AuxPart();
            if (!LoadAuxPart(ID, ref auxPart))
            {
                return false;
            }
            if (auxPart.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            AuxPart auxPart = new AuxPart();
            if (!LoadAuxPart(ID, ref auxPart))
            {
                return 99999999;
            }
            return auxPart.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            AuxPart auxPart = new AuxPart();
            if (!LoadAuxPart(ID, ref auxPart))
            {
                return -1;
            }
            return (sbyte)auxPart.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            AuxPart auxPart = new AuxPart();
            if (!LoadAuxPart(ID, ref auxPart))
            {
                return false;
            }
            if (auxPart.Base.Enabled == 1 && auxPart.Base.MoneyFlag == 0 || auxPart.Base.MoneyFlag == MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        bool LoadAuxPart(uint ID, ref AuxPart auxPart)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                auxPart = load.First();
                return false;
            }
            return true;
        }

        public AuxPart LoadAuxPart(uint ID)
        {
            AuxPart auxPart = new AuxPart();
            if (!LoadAuxPart(ID, ref auxPart))
            {
                return auxPart;
            }
            return auxPart;
        }
    }
}
