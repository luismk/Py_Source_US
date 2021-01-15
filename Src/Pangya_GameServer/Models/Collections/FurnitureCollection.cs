using PangyaAPI.Helper.BinaryModels;
using System.Collections.Generic;
using Pangya_GameServer.Models.Data;
using PangyaAPI.PangyaClient.Data;
namespace Pangya_GameServer.Models.Collections
{
    public class FurnitureCollection : List<FurnitureData>
    {
        public FurnitureCollection(int UID)
        {
            Build(UID);
        }
        public int FurnitureAdd(FurnitureData Value)
        {
            Value.NeedUpdate = false;
            Add(Value);
            return Count;
        }

        void Build(int UID)
        {
            var packet = new PangyaBinaryReader(MemoryStreamExtension.Memory(new byte[] { 0x82, 0x48, 0x00, 0x00, 0x20, 0x68, 0x00, 0x48, 0x00, 0x00, 0x33, 0x33, 0x73, 0x41, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0x41, 0x00, 0x00, 0x18, 0x43, 0x00, 0x83, 0x48, 0x00, 0x00, 0x1D, 0x50, 0x00, 0x48, 0x00, 0x00, 0x00, 0x00, 0x62, 0x41, 0x00, 0x00, 0x80, 0x3F, 0x9E, 0xEF, 0x27, 0x3D, 0x00, 0x00, 0x00, 0x00, 0x00 }));
            for (int i = 0; i < 2; i++)
            {
                FurnitureAdd(new FurnitureData() { Header = packet.Read<PlayerFurniture>() });
            }
        }

        public byte[] GetItemInfo()
        {
            var Packet = new PangyaBinaryWriter();
            Packet.Write(new byte[] { 0x2D, 0x01 });
            Packet.WriteUInt32(1);
            Packet.WriteUInt16(Count);
            foreach (var item in this)
            {
                Packet.WriteStruct(item.Header);
            }
            return Packet.GetBytes();
        }
    }
}
