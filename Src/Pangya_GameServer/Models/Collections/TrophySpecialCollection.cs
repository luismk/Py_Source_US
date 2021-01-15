using PangyaAPI.Helper.BinaryModels;
using System.Collections.Generic;
using PangyaAPI.PangyaClient.Data;
namespace Pangya_GameServer.Models.Collections
{
    public class TrophySpecialCollection : List<PlayerTrophySpecial>
    {
        /// <summary>
        /// Cria as informações
        /// </summary>
        /// <param name="Code"> 0 == Todas as sessoes, Sessão 5 </param>
        /// <returns></returns>
        public byte[] Build(byte Code = 5)
        {
            var result = new PangyaBinaryWriter();
            result.Write(new byte[] { 0xB4, 0x00, Code });
            result.Write((ushort)Count);
            foreach (var data in this)
            {
                result.WriteStruct(data);
            }
            return result.GetBytes();
        }

        public byte[] GetInfo()
        {
            var result = new PangyaBinaryWriter();
            result.Write((ushort)Count);
            foreach (var data in this)
            {
                result.WriteStruct(data);
            }
            return result.GetBytes();
        }
    }
}
