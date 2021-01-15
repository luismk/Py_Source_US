using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Collections
{
    public class GrandPrixDataCollection : List<GrandPrixData>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            GrandPrixData GrandPrixData;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\GrandPrixData.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new GrandPrixData());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"GrandPrixData.iff the structure size is incorrect, Real: {recordLength}, GrandPrixData.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        GrandPrixData = (GrandPrixData)Reader.Read(new GrandPrixData());

                        this.Add(GrandPrixData);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.GrandPrixData");
                return false;
            }
        }

        //Destructor
        ~GrandPrixDataCollection()
        {
            this.Clear();
        }

        public GrandPrixData GetGP(UInt32 TypeId)
        {
            GrandPrixData GP = new GrandPrixData();
            if (!LoadGP(TypeId, ref GP))
            {
                return GP;
            }
            return GP;
        }

        public bool IsGPExist(UInt32 TypeId)
        {
            GrandPrixData GP = new GrandPrixData();
            if (!LoadGP(TypeId, ref GP))
            {
                return false;
            }
            return true;
        }

        public bool LoadGP(UInt32 ID, ref GrandPrixData GrandPrix)
        {
            var load = this.Where(c => c.TypeID == ID);
            if (load.Any())
            {
                GrandPrix = load.First();
                return false;
            }
            return true;
        }
    }
}
