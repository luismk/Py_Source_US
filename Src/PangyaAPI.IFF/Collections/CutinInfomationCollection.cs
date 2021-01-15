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
    public class CutinInformationCollection : List<CutinInformation>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            CutinInformation CutinInformation;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show("data\\CutinInfomation.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new CutinInformation());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"CutinInformation.iff the structure size is incorrect, Real: {recordLength}, CutinInformation.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        CutinInformation = (CutinInformation)Reader.Read(new CutinInformation());

                        this.Add(CutinInformation);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.CutinInformation");
                return false;
            }
        }

        //Destructor
        ~CutinInformationCollection()
        {
            this.Clear();
        }

      

        public bool IsExist(uint ID)
        {
            CutinInformation CutinInformation = new CutinInformation();
            if (!LoadCutinInformation(ID, ref CutinInformation))
            {
                return false;
            }
            if (CutinInformation.Enable == 1)
            {
                return true;
            }
            return false;
        }

        
        bool LoadCutinInformation(uint ID, ref CutinInformation CutinInformation)
        {
            var load = this.Where(c => c.TypeID == ID);
            if (load.Any())
            {
                CutinInformation = load.First();
                return false;
            }
            return true;
        }

        public CutinInformation LoadCutinInformation(uint ID)
        {
            CutinInformation CutinInformation = new CutinInformation();
            if (!LoadCutinInformation(ID, ref CutinInformation))
            {
                return CutinInformation;
            }
            return CutinInformation;
        }
    }
}
