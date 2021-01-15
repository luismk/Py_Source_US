using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Pangya_GameServer.Models.Data;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.PangyaClient.Data;
namespace Pangya_GameServer.Models.Collections
{
    public class MascotCollection : List<MascotData>
    {
        public MascotCollection(int UID)
        {
            Build(UID);
        }
        // SerialMascotData
        public int MascotAdd(MascotData Value)
        {
            Value.MascotNeedUpdate = false;
            Add(Value);
            return Count;
        }

        void Build(int UID)
        {
           var packet = new PangyaBinaryReader(new MemoryStream(new byte[] { 0xA9, 0x89, 0xBE, 0x3D, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x50, 0x41, 0x4E, 0x47, 0x59, 0x41, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, }));

            MascotAdd(new MascotData() { Header = packet.Read<PlayerMascot>()});
        }
        
        public byte[] Build()
        {
            PangyaBinaryWriter Packet;

            using (Packet = new PangyaBinaryWriter())
            {
                Packet.Write(new byte[] { 0xE1, 0x00 });
                Packet.WriteByte((byte)Count);
                foreach (var Mascot in this)
                {
                    Packet.Write(Mascot.GetMascotInfo());
                }
                return Packet.GetBytes();
            }

        }
        public MascotData GetMascotByIndex(UInt32 MascotIndex)
        {
            foreach (MascotData Mascot in this)
            {
                if ((Mascot.Header.Index == MascotIndex) && (Mascot.MascotEndDate > DateTime.MinValue))
                {
                    return Mascot;
                }
            }
            return null;
        }

        public MascotData GetMascotByTypeId(UInt32 MascotTypeId)
        {
            foreach (MascotData Mascot in this)
            {
                if ((Mascot.Header.TypeID == MascotTypeId) && (Mascot.MascotEndDate > DateTime.Now))
                {
                    return Mascot;
                }
            }
            return null;
        }

        public bool MascotExist(UInt32 TypeId)
        {
            foreach (MascotData Mascot in this)
            {
                if ((Mascot.Header.TypeID == TypeId) && (Mascot.MascotEndDate > DateTime.Now))
                {
                    return true;
                }
            }
            return false;
        }

        public string GetSqlUpdateMascots()
        {

            StringBuilder SQLString;
            SQLString = new StringBuilder();
            try
            {
                foreach (var mascot in this)
                {
                    if (mascot.MascotNeedUpdate)
                    {
                        SQLString.Append(mascot.GetSqlUpdateString());
                        // ## set update to false when request string
                        mascot.MascotNeedUpdate = false;
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
