using Pangya_LoginServer.LoginPlayer;
using PangyaAPI.PangyaPacket;
namespace Pangya_LoginServer.Handles
{
    public static class PlayerSelectServer
    {
        public static void SelectServer(this LPlayer session, Packet packet)
        {
            if (!packet.ReadUInt32(out uint ServerID))
            { 
            
            }

            session.AuthKeyGame();
        }
    }
}
                                                                                                                                                                                                                                                                                                                                        