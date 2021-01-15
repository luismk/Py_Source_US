using Pangya_LoginServer.LoginPlayer;
namespace Pangya_LoginServer.Handles
{
    public static class PlayerLoginSucess
    {
        public static void LoginSucess(this LPlayer session)
        {
            session.AuthKeyLogin();

            session.GameMacro();

           // session.Login();

            session.GameServerList();
        }

        static void Login(this LPlayer session)
        {
            session.Response.Write(new byte[] { 0x01, 0x00 });
            session.Response.WriteByte((byte)0);//code for login sucess
            session.Response.WritePStr(session.GetLogin);
            session.Response.WriteUInt32(session.GetUID);
            session.Response.WriteUInt32(session.GetCapability);//Capacity
            session.Response.WriteUInt32(session.GetLevel); // Level
            session.Response.WriteUInt32(10);
            session.Response.WriteUInt16(12);
            session.Response.WritePStr(session.GetNickname);
            session.SendResponse();
        }
    }
}
