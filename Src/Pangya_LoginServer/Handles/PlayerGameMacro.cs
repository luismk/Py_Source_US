using Pangya_LoginServer.LoginPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_LoginServer.Handles
{
    public static class PlayerGameMacro
    {
        /// <summary>
        /// Envia o macro(frases rapidas usadas no LobbyPlayer)
        /// </summary>
        /// <param name="session"></param>
        public static void GameMacro(this LPlayer session) 
        {
            session.Response.Clear();//limpa a resposta anterior

            session.Response.Write(new byte[] { 0x06, 0x00 });
            session.Response.WriteStr("Pangya 1!", 64);
            session.Response.WriteStr("Pangya 2!", 64);
            session.Response.WriteStr("Pangya 3!", 64);
            session.Response.WriteStr("Pangya 4!", 64);
            session.Response.WriteStr("Pangya 5!", 64);
            session.Response.WriteStr("Pangya 6!", 64);
            session.Response.WriteStr("Pangya 7!", 64);
            session.Response.WriteStr("Pangya 8!", 64);
            session.Response.WriteStr("Pangya 9!", 64);
            session.SendResponse();
        }
    }
}
