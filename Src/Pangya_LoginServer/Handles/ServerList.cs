using Pangya_LoginServer.LoginPlayer;
using PangyaAPI.Helper.BinaryModels;
namespace Pangya_LoginServer.Handles
{
    public static  class ServerList
    {
        /// <summary>
        /// Lista dos Servidores Game Ativos !
        /// </summary>
        /// <param name="session">Jogador/Conexao</param>
        public static void GameServerList(this LPlayer session)
        {
            session.Response.Write(new byte[] { 0x02, 0x00 });
            session.Response.WriteByte((byte)1);//count servers 
             //data for game server list
            session.Response.WriteStr("PangYa S7", 40);
            session.Response.WriteInt32(20201);//serverID
            session.Response.WriteInt32(2000);//max user
            session.Response.WriteInt32(1);//players online
            session.Response.WriteStr("127.0.0.1", 18);//ip server
            session.Response.WriteInt32(7997);//port 
            session.Response.WriteInt32(2048);//property
            session.Response.WriteUInt32(0); // Angelic Number
            session.Response.WriteUInt16((ushort)0);//Flag event
            session.Response.WriteUInt16(0);//unknown
            session.Response.WriteInt32(100);//pang rate?
            session.Response.WriteUInt16(0);//Icon Server    
            session.SendResponse();
        }


        /// <summary>
        /// Lista dos Servidores Mensageiro Ativos !
        /// </summary>
        /// <param name="session">Jogador/Conexao</param>
        public static void MessangerServerList(this LPlayer session)
        {
            session.Response.Write(new byte[] { 0x09, 0x00 });
            session.Response.WriteByte((byte)1);//count servers 
            //data
            session.Response.WriteStr("PangYa S6", 40);
            session.Response.WriteInt32(30303);//serverID
            session.Response.WriteInt32(2000);//max user
            session.Response.WriteInt32(1);
            session.Response.WriteStr("127.0.0.1", 18);
            session.Response.WriteInt32(30303);//port 
            session.Response.WriteInt32(4096);
            session.Response.WriteUInt32(0); // Angelic Number
            session.Response.WriteUInt16((ushort)0);//Flag event
            session.Response.WriteUInt16(0);
            session.Response.WriteInt32(0);
            session.Response.WriteUInt16(0);//Icon Server    
            session.SendResponse();
        }
    }
}
