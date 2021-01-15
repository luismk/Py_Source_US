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
    public class ClubSetCollection : List<ClubSet>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            ClubSet ClubSet;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\ClubSet.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new ClubSet());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"ClubSet.iff the structure size is incorrect, Real: {recordLength}, ClubSet.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        ClubSet = (ClubSet)Reader.Read(new ClubSet());

                        this.Add(ClubSet);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.ClubSet");
                return false;
            }
        }

        //Destructor
        ~ClubSetCollection()
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
            ClubSet ClubSet = new ClubSet();
            if (!LoadClubSet(ID, ref ClubSet))
            {
                return false;
            }
            if (ClubSet.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            ClubSet ClubSet = new ClubSet();
            if (!LoadClubSet(ID, ref ClubSet))
            {
                return 99999999;
            }
            return ClubSet.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            ClubSet ClubSet = new ClubSet();
            if (!LoadClubSet(ID, ref ClubSet))
            {
                return -1;
            }
            return (sbyte)ClubSet.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            ClubSet ClubSet = new ClubSet();
            if (!LoadClubSet(ID, ref ClubSet))
            {
                return false;
            }
            if (ClubSet.Base.Enabled == 1 && ClubSet.Base.MoneyFlag == 0 || ClubSet.Base.MoneyFlag == MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


      public bool LoadClubSet(uint ID, ref ClubSet ball)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                ball = load.First();
                return false;
            }
            return true;
        }

        public ClubSet LoadClubSet(uint ID)
        {
            ClubSet ClubSet = new ClubSet();
            if (!LoadClubSet(ID, ref ClubSet))
            {
                return ClubSet;
            }
            return ClubSet;
        }
    }
}
