using System.Threading;
using Pangya_GameServer.Flags;
using Pangya_GameServer.GamePlayer;
using Pangya_GameServer.Handle.LobbyPacket;
using Pangya_GameServer.Handle.LoginPacket;
using Pangya_GameServer.PlayerLobby;
using Pangya_GameServer.PlayerLobby.Common;
using Pangya_GameServer.ServerTcp;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.Helper.Tools;
using PangyaAPI.PangyaClient;
using PangyaAPI.PangyaClient.Data;
using PangyaAPI.PangyaPacket;
namespace Pangya_GameServer
{
    class Program
    {
        public static GameServer GameServer;
        public static void Main(string[] args)
        {
            GameServer = new GameServer();
            GameServer.ServerStart();
            GameServer.OnPacketReceived += GameServer_OnPacketReceived;
            GameServer.OnClientConnected += ClientConnected;
            GameServer.OnClientDisconnected += ClientDisconnected;
            while (true)
            {
                Thread.Sleep(200);
            }
        }
        private static void ClientDisconnected(Player client)
        {
            var session = (GPlayer)client;
            WriteConsole.WriteLine($"{session.GetAdress}:{session.GetPort}");
        }

        private static void ClientConnected(Player client)
        {
            var session = (GPlayer)client;
            WriteConsole.Connected($"{session.GetAdress}:{session.GetPort}");
        }
        private static void GameServer_OnPacketReceived(Player session, Packet packet)
        {
            var player = (GPlayer)session;

            var packetId = (GamePacketFlag)packet.Id;

            WriteConsole.Packet($" -> [{packetId}:{player.GetLogin}]");
            switch (packetId)
            {
                case GamePacketFlag.PLAYER_LOGIN:
                    {
                        SystemLogin.ProcessLogin(player, packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_SELECT_LOBBY:
                    {
                       SystemLobby.ProcessLobby(player,packet);
                    }
                    break;
                case GamePacketFlag.PLAYER_EXCEPTION:
                    {
                    }
                    break;
                case GamePacketFlag.PLAYER_REQUEST_TIME:
                    {
                    }
                    break;
                default:
                   HandlePacketLobby.PacketLobby(packetId, player, packet);
                    break;
            }
        }
    }
}
