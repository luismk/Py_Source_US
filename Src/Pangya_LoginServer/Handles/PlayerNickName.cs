using Pangya_LoginServer.LoginPlayer;
using PangyaAPI.PangyaPacket;
using System;
using System.IO;

namespace Pangya_LoginServer.Handles
{
    public static class PlayerNickName
    {
        //Sucess = 0x00,
        //InvalidoIdPw = 0x01,
        //InvalidoId = 0x02,
        //UsuarioEmUso = 0x04,
        //Banido = 0x05,
        //InvalidoUsernameOuSenha = 0x06,
        //ContaSuspensa = 0x07,
        //Player13AnosOuMenos = 0x09,
        //SSNIncorreto = 0x0C,
        //UsuarioIncorreto = 0x0D,
        //OnlyUserAllowed = 0x0E,
        //ServerInMaintenance = 0x0F, //By LuisMk
        //NaoDisponivelNaSuaArea = 0x10, //By LuisMk
        //CreateNickName_US = 0xD8, //by LuisMK (usado no US)
        //CreateNickName = 0xD9, //by LuisMK (usado no TH)
        /// <summary>
        /// Reference: https://github.com/luismk/Py_Source_US/blob/master/Src/Py_Login/Py_Login/LPlayer.cs#L254
        /// </summary>
        /// <param name="session"></param>
        /// <param name="packet"></param>
        public static void SetNickName(this LPlayer session, Packet packet)
        {
            if (!packet.ReadPStr(out string Nickname))
            {
                return;
            }

            session.GetNickname = Nickname;

            var check = Nickname == session.GetNickname;

            session.Response.Write(new byte[] { 0x01, 0x00 });
            session.Response.WriteByte((byte)0xD9);//Caller ID for character creation
            session.Response.WriteInt32(0);
            session.SendResponse();
        }
        //Disponivel = 0x00, //Nickname disponível
        //OcorreuUmErro = 0x01, //Ocorreu um erro ao verificar
        //Indisponivel = 0x03,
        //FormatoOuTamanhoInvalido = 0x04,
        //PointsInsuficientes = 0x05,
        //PalavasInapropriadas = 0x06,
        //DBError = 0x07,
        //MesmoNickNameSeraUsado = 0x09
        /// <summary>
        /// Reference: https://github.com/luismk/Py_Source_US/blob/master/Src/Py_Login/Py_Login/LPlayer.cs#L291
        /// </summary>
        /// <param name="session"></param>
        /// <param name="packet"></param>
        public static void ConfirmNickName(this LPlayer session, Packet packet)
        {
            Byte Code = 0;

            if (!packet.ReadPStr(out string Nickname))
            {
                return;
            }

            session.Response.Write(new byte[] { 0x0E, 0x00 });
            session.Response.WriteUInt32((uint)0);//Nickname disponivel
            session.Response.WritePStr(Nickname);
            session.SendResponse();
        }
    }
}
