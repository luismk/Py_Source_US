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
    public class CaddieCollection : List<Caddie>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            Caddie Caddie;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Caddie.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Caddie());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Caddie.iff the structure size is incorrect, Real: {recordLength}, Caddie.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Caddie = (Caddie)Reader.Read(new Caddie());

                        this.Add(Caddie);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Caddie");
                return false;
            }
        }

        //Destructor
        ~CaddieCollection()
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
            Caddie caddie = new Caddie();
            if (!LoadCaddie(ID, ref caddie))
            {
                return false;
            }
            if (caddie.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            Caddie caddie = new Caddie();
            if (!LoadCaddie(ID, ref caddie))
            {
                return 99999999;
            }
            return caddie.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            Caddie caddie = new Caddie();
            if (!LoadCaddie(ID, ref caddie))
            {
                return -1;
            }
            return (sbyte)caddie.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            Caddie caddie = new Caddie();
            if (!LoadCaddie(ID, ref caddie))
            {
                return false;
            }
            if (caddie.Base.Enabled == 1 && caddie.Base.MoneyFlag == 0 || caddie.Base.MoneyFlag == MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        public uint GetSalary(uint TypeId)
        {
            foreach (var Item in this)
            {
                if (Item.Base.TypeID == TypeId && Item.Base.Enabled == 1)
                {
                    return Item.Salary;
                }
            }
            return 0;
        }

        bool LoadCaddie(uint ID, ref Caddie ball)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                ball = load.First();
                return false;
            }
            return true;
        }

        public Caddie LoadCaddie(uint ID)
        {
            Caddie caddie = new Caddie();
            if (!LoadCaddie(ID, ref caddie))
            {
                return caddie;
            }
            return caddie;
        }

    }
}
