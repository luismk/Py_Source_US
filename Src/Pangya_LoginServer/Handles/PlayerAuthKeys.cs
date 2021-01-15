using Pangya_LoginServer.LoginPlayer;
namespace Pangya_LoginServer.Handles
{
    public static class PlayerAuthKeys
    {
        /// <summary>
        /// Envia o pacote de chave de autenficacao A
        /// </summary>
        /// <param name="session"></param>
        public static void AuthKeyLogin(this LPlayer session)
        {
            session.Response.WriteUInt16(0x0010);
            session.Response.WritePStr("7430F52");//chave de autenficacao
            session.SendResponse();
        }
        /// <summary>
        /// Envia o pacote de chave de autenficacao B
        /// </summary>
        /// <param name="session">Jogador</param>
        public static void AuthKeyGame(this LPlayer session)
        {
            session.Response.Write(new byte[] { 0x03, 0x00 });
            session.Response.WriteInt32(0);
            session.Response.WritePStr("5130B52");//chave de autenficacao
            session.SendResponse();
        }
    }
}
