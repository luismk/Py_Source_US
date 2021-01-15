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
    public class MascotCollection : List<Mascot>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        #endregion

        public bool Load(MemoryStream data)
        {
            Mascot Mascot;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Mascot.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Mascot());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Mascot.iff the structure size is incorrect, Real: {recordLength}, Mascot.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Mascot = (Mascot)Reader.Read(new Mascot());

                        this.Add(Mascot);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Mascot");
                return false;
            }
        }

        //Destructor
        ~MascotCollection()
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
            Mascot Mascot = new Mascot();
            if (!LoadMascot(ID, ref Mascot))
            {
                return false;
            }
            if (Mascot.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID, uint aDay)
        {
            Mascot Mascot = new Mascot();
            if (!LoadMascot(ID, ref Mascot))
            {
                return 99999999;
            }
            return Mascot.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            Mascot Mascot = new Mascot();
            if (!LoadMascot(ID, ref Mascot))
            {
                return -1;
            }
            return (sbyte)Mascot.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            Mascot Mascot = new Mascot();
            if (!LoadMascot(ID, ref Mascot))
            {
                return false;
            }
            if (Mascot.Base.Enabled == 1 && Mascot.Base.MoneyFlag == 0 || Mascot.Base.MoneyFlag == Flags.MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }

        public uint GetSalary(uint TypeId, uint Day)
        {
            Mascot Item = new Mascot();
            if (!LoadMascot(TypeId, ref Item))
            {
                return 0;
            }
            if (Item.Base.Enabled == 1)
            {
                switch (Day)
                {
                    case 1:
                        return Item.Price1;
                    case 7:
                        return Item.Price7;
                    case 30:
                        return Item.Price30;
                }
            }
            return 0;
        }

        bool LoadMascot(uint ID, ref Mascot Mascot)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                Mascot = load.First();
                return false;
            }
            return true;
        }

        public Mascot LoadMascot(uint ID)
        {
            Mascot Mascot = new Mascot();
            if (!LoadMascot(ID, ref Mascot))
            {
                return Mascot;
            }
            return Mascot;
        }
    }
}
