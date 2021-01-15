using Pangya_GameServer.Common;
using Pangya_GameServer.GamePlayer;
using PangyaAPI.Helper.Tools;
using PangyaAPI.PangyaPacket;
namespace Pangya_GameServer.Handle.LoginPacket
{
    public static class SystemLogin
    {
        public static void ProcessLogin(this GPlayer session, Packet packet)
        {
            try
            {
                var loginresult = (LoginData)packet.ReadObject(new LoginData());

                var ClientBuildDate = packet.Deserialize(loginresult.ClientBuildDate);

                if (ClientBuildDate != 2015031200 || loginresult.ClientVersion != "824.00")
                {
                    WriteConsole.WriteLine($"[CLIENT_ERROR]: Build Date => {ClientBuildDate}, Version => {loginresult.ClientVersion}");
                    session.Send(new byte[] { 0x44, 0x00, 0x0B });
                    session.Disconnect();
                    return;
                }
                session.SetLogin(loginresult.UserName);
                session.SetNickname("MK(e7)");
                session.SetSex(0);
                session.SetCapabilities(0);
                session.SetUID(1);//loginresult.UID
                session.SetCookie(10000000);
                session.LockerPang = 100000;
                session.LockerPWD = "1234";
                session.SetAuthKey1(loginresult.AuthKeyLogin);
                session.SetAuthKey2(loginresult.AuthKeyGame);
                if (true)
                {
                    session.InventoryLoad();

                    session.LoadStatistic();

                    session.LoadGuildData();

                    PlayerRequestInfo(session, loginresult.ClientVersion);
                }
            }
            catch
            {
                session.Disconnect();
            }
        }

        static void PlayerRequestInfo(GPlayer session, string version)
        {
            ///SEND PLAYER_MAIN_DATA == 0x0044
            session.SendLoginMainData(version, Program.GameServer.Options);

            /////SEND PLAYER_CHARACTERS_DATA == 0x0070
           session.SendCharacterData();

            /////SEND PLAYER_ITEMS_DATA == 0x0073
            session.SendItemsData();

            /////SEND PLAYER_CADDIES_DATA == 0x0071
            session.SendCaddiesData();

            /////SEND PLAYER_EQUIP_DATA == 0x0072
            session.SendEquipmentData();

            /////SEND PLAYER_MASCOTS_DATA == 0x00E1
            session.SendMascotsData();

            ///SEND PLAYER_COOKIES_DATA == 0x0096
            session.SendCookies();

            ///SEND SERVER_LOBBIES_LIST == 0x004D
            session.SendLobbyList();
        }
    }
}
