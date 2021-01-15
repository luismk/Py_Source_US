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
    public class CourseCollection : List<Course>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            Course Course;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Course.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Course());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Course.iff the structure size is incorrect, Real: {recordLength}, Course.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Course = (Course)Reader.Read(new Course());

                        this.Add(Course);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Course");
                return false;
            }
        }

        //Destructor
        ~CourseCollection()
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
            Course Course = new Course();
            if (!LoadCourse(ID, ref Course))
            {
                return false;
            }
            if (Course.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint ID)
        {
            Course Course = new Course();
            if (!LoadCourse(ID, ref Course))
            {
                return 99999999;
            }
            return Course.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint ID)
        {
            Course Course = new Course();
            if (!LoadCourse(ID, ref Course))
            {
                return -1;
            }
            return (sbyte)Course.Base.PriceType;
        }

        public bool IsBuyable(uint ID)
        {
            Course Course = new Course();
            if (!LoadCourse(ID, ref Course))
            {
                return false;
            }
            if (Course.Base.Enabled == 1 && Course.Base.MoneyFlag == 0 || Course.Base.MoneyFlag == Flags.MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        bool LoadCourse(uint ID, ref Course Course)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                Course = load.First();
                return false;
            }
            return true;
        }

        public Course LoadCourse(uint ID)
        {
            Course Course = new Course();
            if (!LoadCourse(ID, ref Course))
            {
                return Course;
            }
            return Course;
        }
    }
}
