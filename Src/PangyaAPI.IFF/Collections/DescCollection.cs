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
    public class DescCollection : List<Desc>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            Desc Desc;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Desc.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Desc());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Desc.iff the structure size is incorrect, Real: {recordLength}, Desc.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Desc = (Desc)Reader.Read(new Desc());

                        this.Add(Desc);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Desc");
                return false;
            }
        }

        //Destructor
        ~DescCollection()
        {
            this.Clear();
        }

        public string GetItemDescription(uint ID)
        {
            foreach (var item in this)
            {
                if (item.TypeID == ID)
                {
                    return item.Description;
                }
            }
            return "";
        }


        bool LoadDesc(uint ID, ref Desc Desc)
        {
            var load = this.Where(c => c.TypeID == ID);
            if (load.Any())
            {
                Desc = load.First();
                return false;
            }
            return true;
        }

        public Desc LoadDesc(uint ID)
        {
            Desc Desc = new Desc();
            if (!LoadDesc(ID, ref Desc))
            {
                return Desc;
            }
            return Desc;
        }
    }
}
