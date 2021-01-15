using Pangya_GameServer.Flags;
using Pangya_GameServer.Game.Model;
using Pangya_GameServer.GamePlayer;
using PangyaAPI.PangyaPacket;
namespace Pangya_GameServer.Handle.GamePacket
{
    public static class HandlePacketGame
    {
        public static void PacketGame(GameBase Game,GamePacketFlag ID, GPlayer player, Packet packet)
        {
            Game.HandlePacket(ID, player, packet);
        }
    }
}