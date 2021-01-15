using PangyaAPI.Helper.BinaryModels;
using System.Collections.Generic;
using PangyaAPI.PangyaClient.Data;
namespace Pangya_GameServer.Models.Collections
{
    public class TrophyCollection : List<PlayerTrophy>
    {
        public TrophyCollection()
        {
            
        }

        public byte[] GetTrophy()
        {
            var result = new PangyaBinaryWriter();

            if (Count > 0)
            {
                foreach (var trophies in this)
                {
                    result.WriteStruct(trophies);
                }
            }
            else
            {
                result.Write(78);
            }
            return result.GetBytes();
        }

        public byte[] Build(byte Code)
        {
            var result = new PangyaBinaryWriter();
            result.Write(new byte[] { 0x69, 0x01, Code });
            if (Count > 0)
            {
                foreach (var trophies in this)
                {
                    result.WriteStruct(trophies);
                }
            }
            else
            {
                result.Write(78);
            }
            return result.GetBytes();
        }
    }
}
