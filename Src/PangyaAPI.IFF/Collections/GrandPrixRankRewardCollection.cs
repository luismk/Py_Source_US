using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Collections
{
    public class GrandPrixRankRewardCollection : List<GrandPrixRankReward>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            GrandPrixRankReward GrandPrixRankReward;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\GrandPrixRankReward.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new GrandPrixRankReward());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"GrandPrixRankReward.iff the structure size is incorrect, Real: {recordLength}, GrandPrixRankReward.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        GrandPrixRankReward = (GrandPrixRankReward)Reader.Read(new GrandPrixRankReward());

                        this.Add(GrandPrixRankReward);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.GrandPrixRankReward");
                return false;
            }
        }

        //Destructor
        ~GrandPrixRankRewardCollection()
        {
            this.Clear();
        }
    }
}
