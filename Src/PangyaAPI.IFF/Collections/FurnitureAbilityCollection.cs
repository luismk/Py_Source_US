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
    public class FurnitureAbilityCollection : List<FurnitureAbility>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            FurnitureAbility FurnitureAbility;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\FurnitureAbility.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new FurnitureAbility());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"FurnitureAbility.iff the structure size is incorrect, Real: {recordLength}, FurnitureAbility.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        FurnitureAbility = (FurnitureAbility)Reader.Read(new FurnitureAbility());

                        this.Add(FurnitureAbility);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.FurnitureAbility");
                return false;
            }
        }

        //Destructor
        ~FurnitureAbilityCollection()
        {
            this.Clear();
        }
    }
}
