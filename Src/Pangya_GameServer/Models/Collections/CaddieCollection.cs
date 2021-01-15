using System;
using System.Collections.Generic;
using System.Text;
using Pangya_GameServer.Models.Data;
using PangyaAPI.Helper.BinaryModels;
namespace Pangya_GameServer.Models.Collections
{
    public class CaddieCollection : List<CaddieData>
    {
        public CaddieCollection(int UID)
        {
            Build(UID);
        }
        // SerialCaddieData
        public int CadieAdd(CaddieData Value)
        {
            Value.NeedUpdate = false;
            Add(Value);
            return Count;
        }


        void Build(int UID)
        {
        }


        public byte[] Build()
        {
            PangyaBinaryWriter Reply;

            using (Reply = new PangyaBinaryWriter())
            {
                Reply.Write(new byte[] { 0x71, 0x00 });
                Reply.WriteUInt16(Count);
                Reply.WriteUInt16(Count);
                foreach (CaddieData CaddieInfo in this)
                {
                    Reply.WriteStruct(CaddieInfo.Header);
                }
                return Reply.GetBytes();
            }
        }

        public byte[] BuildExpiration()
        {
            PangyaBinaryWriter Reply;

            using (Reply = new PangyaBinaryWriter())
            {
                Reply.Write(new byte[] { 0xD4, 0x00 });
                foreach (CaddieData CaddieInfo in this)
                {
                    Reply.Write(CaddieInfo.GetExpirationNotice());
                }
                return Reply.GetBytes();
            }
        }
        public byte[] GetCaddie()
        {
            PangyaBinaryWriter Result;
            Result = new PangyaBinaryWriter();
            try
            {
                foreach (CaddieData CaddieInfo in this)
                {
                    Result.WriteStruct(CaddieInfo.Header);
                }
                return Result.GetBytes();
            }
            finally
            {
                Result.Dispose();
            }
        }

        public bool IsExist(UInt32 TypeId)
        {
            foreach (CaddieData CaddieInfo in this)
            {
                if ((CaddieInfo.Header.TypeID == TypeId))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CanHaveSkin(UInt32 SkinTypeId)
        {
            foreach (CaddieData CaddieInfo in this)
            {
                if (CaddieInfo.Exist(SkinTypeId))
                {
                    return true;
                }
            }
            return false;
        }

        public CaddieData GetCaddieByIndex(UInt32 Index)
        {
            foreach (CaddieData CaddieInfo in this)
            {
                if (CaddieInfo.Header.Index == Index)
                {
                    return CaddieInfo;
                }
            }
            return null;
        }

        public CaddieData GetCaddieBySkinId(UInt32 SkinTypeId)
        {
            foreach (CaddieData CaddieInfo in this)
            {
                if (CaddieInfo.Exist(SkinTypeId))
                {
                    return CaddieInfo;
                }
            }
            return null;
        }


        public string GetSqlUpdateCaddie()
        {
            StringBuilder SQLString;
            SQLString = new StringBuilder();
            try
            {
                foreach (CaddieData CaddieInfo in this)
                {
                    if (CaddieInfo.NeedUpdate)
                    {
                        SQLString.Append(CaddieInfo.GetSQLUpdateString());
                        // update to false when get string
                        CaddieInfo.NeedUpdate = false;
                    }
                }
                return SQLString.ToString();
            }
            finally
            {
                SQLString.Clear();
            }
        }
    }
}
