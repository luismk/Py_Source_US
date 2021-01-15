using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Collections
{
    public class MemorialShopRareItemCollection : List<MemorialShopRareItem>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            MemorialShopRareItem MemorialShopRareItem;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\MemorialShopRareItem.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new MemorialShopRareItem());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"MemorialShopRareItem.iff the structure size is incorrect, Real: {recordLength}, MemorialShopRareItem.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        MemorialShopRareItem = (MemorialShopRareItem)Reader.Read(new MemorialShopRareItem());

                        this.Add(MemorialShopRareItem);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.MemorialShopRareItem");
                return false;
            }
        }

        //Destructor
        ~MemorialShopRareItemCollection()
        {
            this.Clear();
        }
    }
}
