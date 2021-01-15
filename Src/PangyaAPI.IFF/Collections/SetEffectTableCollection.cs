using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Collections
{
    public class SetEffectTableCollection : List<SetEffectTable>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            SetEffectTable SetEffectTable;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\SetEffectTable.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new SetEffectTable());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"SetEffectTable.iff the structure size is incorrect, Real: {recordLength}, SetEffectTable.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        SetEffectTable = (SetEffectTable)Reader.Read(new SetEffectTable());

                        this.Add(SetEffectTable);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.SetEffectTable");
                return false;
            }
        }

        //Destructor
        ~SetEffectTableCollection()
        {
            this.Clear();
        }
    }
}
