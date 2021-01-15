using Pangya_GameServer.GamePlayer;
using Pangya_GameServer.PacketCreator;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.PangyaPacket;
using PangyaAPI.Helper.Tools;
namespace Pangya_GameServer.Handle.LobbyPacket
{
    public static class SystemLobby
    {
       public static void ProcessLobby(this GPlayer player, Packet packet, bool RequestJoinGameList = false)
        {
            var lp = player.Lobby;

            //Lê Id do Lobby
            if (!packet.ReadByte(out byte lobbyId))
            {
                return;
            }

            var lobby = Program.GameServer.GetLobbyByID(lobbyId);

            if (lp != null)
            {
                lobby.RemovePlayer(player);
            }

            //Caso o lobby não existir
            if (lobby == null)
            {
                player.SendResponse(new byte[] { 0x95, 0x00, 0x02, 0x01, 0x00 });
                WriteConsole.WriteLine("Player Select Invalid Lobby");
                return;
            }
            //Se estiver lotado
            if (lobby.IsFull)
            {
                player.SendResponse(new byte[] { 0x4E, 0x00, 0x02 });
                WriteConsole.WriteLine("Player Selected Lobby Full");
                return;
            }
            // ## add player
            lobby.AddPlayer(player);

            player.SendResponse(new byte[] { 0x95, 0x00, lobbyId, 0x01, 0x00 });

            player.SendResponse(new byte[] { 0x4E, 0x00, 0x01 });

            player.SendResponse(new byte[] { 0x48, 0x02, 0x00, 0x00, 0x00, 0x00, 0x01, 0x08, 0x02, 0x00, 0x1A, 0x01, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x18, 0x03, 0x00, 0x00, 0x00, 0x1E, 0x00, 0x00, 0x00 });
        }      
    }
}
