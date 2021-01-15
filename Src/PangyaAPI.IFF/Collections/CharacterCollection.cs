using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.IFF.Common;
using PangyaAPI.IFF.Flags;
using PangyaAPI.IFF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace PangyaAPI.IFF.Collections
{
    public class CharacterCollection : List<Character>
    {
        #region Fields
        IFFHeader IFF_FILE_HEADER;
        public bool Update { get; set; }
        #endregion

        public bool Load(MemoryStream data)
        {
            Character Character;

            if (data == null || data.Length == 0)
            {
                MessageBox.Show(" data\\Character.iff is not loaded", "Pangya.IFF");
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

                    var IffStructSize = Tools.IFFTools.SizeStruct(new Character());
                    if (IffStructSize != recordLength)
                    {
                        throw new Exception($"Character.iff the structure size is incorrect, Real: {recordLength}, Character.cs: {IffStructSize} ");
                    }

                    for (int i = 0; i < IFF_FILE_HEADER.RecordCount; i++)
                    {
                        Character = (Character)Reader.Read(new Character());

                        this.Add(Character);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Error Struct]:  " + ex.Message, "Pangya.IFF.Model.Character");
                return false;
            }
        }

        //Destructor
        ~CharacterCollection()
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
            Character Item = new Character();
            if (!LoadCharacter(ID, ref Item))
            {
                return false;
            }
            if (Item.Base.Enabled == 1)
            {
                return true;
            }
            return false;
        }

        public uint GetPrice(uint TypeID)
        {
            Character Character = new Character();
            if (!LoadCharacter(TypeID, ref Character))
            {
                return 99999999;
            }
            return Character.Base.ItemPrice;
        }


        public sbyte GetShopPriceType(uint TypeId)
        {
            Character Character = new Character();
            if (!LoadCharacter(TypeId, ref Character))
            {
                return -1;
            }
            return (sbyte)Character.Base.PriceType;
        }

        public bool IsBuyable(uint TypeId)
        {
            Character Character = new Character();
            if (!LoadCharacter(TypeId, ref Character))
            {
                return false;
            }
            if (Character.Base.Enabled == 1 && Character.Base.MoneyFlag == 0 || Character.Base.MoneyFlag == MoneyFlag.Active)
            {
                return true;
            }
            return false;
        }


        bool LoadCharacter(uint ID, ref Character character)
        {
            var load = this.Where(c => c.Base.TypeID == ID);
            if (load.Any())
            {
                character = load.First();
                return false;
            }
            return true;
        }

        public Character LoadCharacter(uint ID)
        {
            Character character = new Character();
            if (!LoadCharacter(ID, ref character))
            {
                return character;
            }
            return character;
        }
    }
}
