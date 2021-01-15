using System;
using System.Text;
using System.Threading;
using Pangya_LoginServer.Flags;
using Pangya_LoginServer.Handles;
using Pangya_LoginServer.LoginPlayer;
using Pangya_LoginServer.ServerTcp;
using PangyaAPI.Crypt;
using PangyaAPI.Helper.Tools;
using PangyaAPI.PangyaClient;
using PangyaAPI.PangyaPacket;
namespace Pangya_LoginServer
{
    class Program
    {
        static ServerTcp.LoginServer LoginServer;
        public static void Main()
        {
          Console.WriteLine(Cryptor.GenerateKeyId().HexDump());
            LoginServer = new LoginServer();
            LoginServer.ServerStart();
            LoginServer.OnPacketReceived += LoginServer_OnPacketReceived;
            LoginServer.OnClientConnected += ClientConnected;
            LoginServer.OnClientDisconnected += ClientDisconnected;
            while (true)
            {
                Thread.Sleep(200);
            }
        }

        private static void ClientDisconnected(Player client)
        {
            var session = (LPlayer)client;
            WriteConsole.WriteLine($"[PLAYER_DISCONNETED]: {session.GetAdress}:{session.GetPort}", ConsoleColor.Red);
        }

        private static void ClientConnected(Player client)
        {
            var session = (LPlayer)client;
            WriteConsole.WriteLine($"[PLAYER_CONNETED]: {session.GetAdress}:{session.GetPort}", ConsoleColor.Green);
        }

        public static void LoginServer_OnPacketReceived(Player LP, Packet ProcessPacket)
        {
            var PacketID = (LoginPacketFlag)ProcessPacket.Id;
            var player = (LPlayer)LP;

            WriteConsole.Packet($"{PacketID}:{player.GetLogin}");
                       
            switch (PacketID)
            {
                case LoginPacketFlag.PLAYER_LOGIN:
                    player.LoginSucess();
                    break;
                case LoginPacketFlag.PLAYER_SELECT_SERVER:
                    player.SelectServer(ProcessPacket);
                    break;
                case (LoginPacketFlag.PLAYER_DUPLICATE_LOGIN):
                    break;
                case (LoginPacketFlag.PLAYER_SET_NICKNAME):
                    player.SetNickName(ProcessPacket);
                    break;
                case LoginPacketFlag.PLAYER_CONFIRM_NICKNAME:
                    player.ConfirmNickName(ProcessPacket);
                    break;
                case LoginPacketFlag.PLAYER_SELECT_CHARACTER:
                    player.CharacaterCreate(ProcessPacket);
                    break;
                case LoginPacketFlag.PLAYER_RECONNECT:
                    break;
                case LoginPacketFlag.NOTHING:
                default:
                    {
                        StringBuilder sb = new StringBuilder();

                        for (int i = 0; i < ProcessPacket.GetRemainingData.Length; i++)
                        {
                            if ((i + 1) == ProcessPacket.GetRemainingData.Length)
                            {
                                sb.Append("0x" + ProcessPacket.GetRemainingData[i].ToString("X2") + "");
                            }
                            else
                            {
                                sb.Append("0x" + ProcessPacket.GetRemainingData[i].ToString("X2") + ", ");
                            }
                        }

                        WriteConsole.WriteLine("{Unknown Packet} -> " + sb.ToString(), ConsoleColor.Red);
                        player.Disconnect();
                    }
                    break;
            }
        }
    }
}
