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
    public class CadieMagicBoxCollection : List<CadieMagicBox>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            CadieMagicBox CadieMagicBox;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\CadieMagicBox.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new CadieMagicBox());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"CadieMagicBox.iff the structure size is incorrect, Real: {recordLength}, CadieMagicBox.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        CadieMagicBox = (CadieMagicBox)Reader.Read(new CadieMagicBox());

                        this.Add(CadieMagicBox);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.CadieMagicBox");
                return false;
            }
        }

        //Destructor
        ~CadieMagicBoxCollection()
        {
            this.Clear();
        }

        public string GetItemName(uint ID)
        {
            foreach (var item in this)
            {
                if (item.BoxID == ID)
                {
                    return item.Name;
                }
            }
            return "";
        }

        public bool IsExist(uint ID)
        {
            CadieMagicBox cadieMagicBox = new CadieMagicBox();
            if (!LoadCadieMagicBox(ID, ref cadieMagicBox))
            {
                return false;
            }
            if (cadieMagicBox.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        bool LoadCadieMagicBox(uint ID, ref CadieMagicBox ball)
        {
            var load = this.Where(c => c.TypeID == ID);
            if (load.Any())
            {
                ball = load.First();
                return false;
            }
            return true;
        }

        public CadieMagicBox LoadCadieMagicBox(uint ID)
        {
            CadieMagicBox cadieMagicBox = new CadieMagicBox();
            if (!LoadCadieMagicBox(ID, ref cadieMagicBox))
            {
                return cadieMagicBox;
            }
            return cadieMagicBox;
        }
    }
}
