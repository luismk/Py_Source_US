using Pangya_LoginServer.LoginPlayer;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.PangyaPacket;
using System;
using System.IO;
using System.Linq;

namespace Pangya_LoginServer.Handles
{
    public static class PlayerLogin
    {
        public static bool LoginResult(this LPlayer session, Packet packet)
        {
            session.GetLogin = packet.ReadPStr();           
            if (string.IsNullOrEmpty(session.GetNickname))
            {
                session.Response.Write(new byte[] { 0x01, 0x00 });
                session.Response.WriteByte((byte)0xD8);//Call Create NickName
                session.Response.WriteInt32(0);
                session.SendResponse();
                return false;
            }
            return true;
        }
    }
}
