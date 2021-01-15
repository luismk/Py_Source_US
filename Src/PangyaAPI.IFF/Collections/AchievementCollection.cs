using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Collections
{
    public class AchievementCollection : List<Achievement>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            Achievement Achievement;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Achievement.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Achievement());
                    var datacount = IffStructSize;
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Achievement.iff the structure size is incorrect, Real: {recordLength}, Achievement.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Achievement = (Achievement)Reader.Read(new Achievement());

                        this.Add(Achievement);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Achievement");
                return false;
            }
        }

        //Destructor
        ~AchievementCollection()
        {
            this.Clear();
        }
    }
}
