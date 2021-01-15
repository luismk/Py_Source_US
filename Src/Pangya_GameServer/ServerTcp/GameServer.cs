using Pangya_GameServer.Common;
using Pangya_GameServer.GamePlayer;
using Pangya_GameServer.Models.Collections;
using Pangya_GameServer.PlayerLobby;
using Pangya_GameServer.PlayerLobby.Collection;
using PangyaAPI.Auth.AuthPacket;
using PangyaAPI.Auth.Client;
using PangyaAPI.Auth.Common;
using PangyaAPI.Auth.Flags;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.Helper.Tools;
using PangyaAPI.IFF;
using PangyaAPI.PangyaClient;
using PangyaAPI.Server;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Pangya_GameServer.ServerTcp
{
    public class GameServer : TcpServer
    {
        public LobbyList LobbyList { get; set; }

        public ServerOptions Options;
        public GameServer()
        {
            try
            {
                var Ini = new IniFile(ConfigurationManager.AppSettings["ServerConfig"]);
                Data = new ServerInfo
                {
                    Name = Ini.ReadString("Config", "Name", "Pippin"),
                    Version = Ini.ReadString("Config", "Version", "SV_GS_Release_2.0"),
                    UID = Ini.ReadUInt32("Config", "UID", 20201),
                    MaxPlayers = Ini.ReadUInt32("Config", "MaxPlayers", 3000),
                    Port = Ini.ReadUInt32("Config", "Port", 20201),
                    IP = Ini.ReadString("Config", "IP", "127.0.0.1"),
                    Property = Ini.ReadUInt32("Config", "Property", 2048),
                    BlockFunc = Ini.ReadInt64("Config", "BlockFuncSystem", 0),
                    EventFlag = Ini.ReadInt16("Config", "EventFlag", 0),
                    ImgNo = Ini.ReadInt16("Config", "Icon", 1),
                    GameVersion = "824.00",
                    Type = AuthClientTypeEnum.GameServer,
                    AuthServer_Ip = Ini.ReadString("Config", "AuthServer_IP", "127.0.0.1"),
                    AuthServer_Port = Ini.ReadInt32("Config", "AuthServer_Port", 7997),
                    Key = "3493ef7ca4d69f54de682bee58be4f93"
                };
                ShowLog = Ini.ReadBool("Config", "PacketLog", false);
                Console.Title = $"Pangya Fresh Up! GameServer - {Data.Name} - Players: {Players.Count} ";

                Options = new ServerOptions(Flags.ServerOptionFlag.MAINTENANCE_FLAG_VS_STROKE, 2048);
                //if (ConnectToAuthServer(AuthServerConstructor()) == false)
                //{
                //    WriteConsole.WriteLine("[ERROR_START_AUTH]: Não foi possível se conectar ao AuthServer");
                //    Console.ReadKey();
                //    Environment.Exit(1);
                //}


            }
            catch (Exception erro)
            {
                WriteConsole.WriteLine($"[ERROR_START]: {erro.Message}");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public override void ServerStart()
        {
            try
            {
                //Inicia a leitura dos arquivos .iff
                IFFEntry.Load();//is 100% work? test for iff
                 //Data.InsertServer();
                Start();
               //Ler as configuracoes do lobby's
                var Ini = new IniFile(ConfigurationManager.AppSettings["ChannelConfig"]);
                //Inicia os Lobby's
                LobbyList = new LobbyList(Ini);
                WriteConsole.WriteLine($"[SERVER_START]: PORT {Data.Port}", ConsoleColor.Green);
            }
            catch (Exception erro)
            {
                WriteConsole.WriteLine($"[ERROR_START]: {erro.Message}");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
        protected override Player OnConnectPlayer(TcpClient tcp)
        {
            var player = new GPlayer(tcp)
            {
                ConnectionID = NextConnectionId, Server = this
            };

            NextConnectionId += 1;

            SendKey(player);

            WriteConsole.WriteLine($"[PLAYER_CONNECT]: {player.GetAdress}:{player.GetPort}", ConsoleColor.Green);

            Players.Add(player);
            UpdateServer();
            Console.Title = $"Pangya Fresh Up! GameServer - {Data.Name} - Players: {Players.Count} ";

            return player;
        }

        protected override ClientAuth AuthServerConstructor()
        {
            return new ClientAuth(Data);
        }

        protected override void SendKey(Player player)
        {
            var Player = (GPlayer)player;
            try
            {
                if (Player.Tcp.Connected && Player.Connected)
                {
                    Player.Response = new PangyaBinaryWriter();
                    ////Gera Packet com chave de criptografia 
                    Player.Response.Write(new byte[] { 0x00, 0x16, 0x00, 0x00, 0x3F, 0x00, 0x01, 0x01, player.Key, 0x0E, 0x00, 0x77, 0x77, 0x77, 0x2E, 0x67, 0x6F, 0x6F, 0x67, 0x6C, 0x65, 0x2E, 0x63, 0x6F, 0x6D, });
                    //Player.Response.Write(new byte[] { 0x00 });
                    //player.Response.WriteUInt16(Player.GetAdress.Length + 8);
                    //Player.Response.Write(new byte[] { 0x00, 0x3F, 0x00, 0x01, 0x01, player.Key });
                    //Player.Response.WritePStr(Player.GetAdress);
                    Player.SendPacket(Player.Response.GetBytes());//Envia packet com a chave
                }
            }
            catch
            {
                Player.Disconnect();
            }
        }

        public override void DisconnectPlayer(Player Player)
        {
            var Client = (GPlayer)Player;
            if (Client != null && Client.Connected)
            {
                var PLobby = Client.Lobby;

                if (PLobby != null)
                {
                    PLobby.RemovePlayer(Client);
                }
               // Client.PlayerLeave(); //{ push player to offline }

                Players.Remove(Client); //{ remove from player lists }
                Player.Connected = false;
                Player.Dispose();
                Player.Tcp.Close();
            }
            WriteConsole.WriteLine(string.Format("[PLAYER_DISCONNECT]: {0} is disconnected", Client?.GetLogin), ConsoleColor.Red);

            UpdateServer();
            Console.Title = $"Pangya Fresh Up! GameServer - {Data.Name} - Players: {Players.Count} ";
        }

        protected override void ServerExpection(Player Client, Exception Ex)
        {
            var player = (GPlayer)Client;
            try
            {
                if (player.Connected)
                {
                    DisconnectPlayer(player);
                }

                Utils.Log($"GameServer-{Data.Port}", $"Exception: {Ex.Message}", "GameServer");

                
            }
            catch
            {
            }
        }


        public override Player GetClientByConnectionId(uint ConnectionId)
        {
            var Client = (GPlayer)Players.Model.Where(c => c.ConnectionID == ConnectionId).FirstOrDefault();

            return Client;
        }

        public override Player GetPlayerByNickname(string Nickname)
        {
            var Client = (GPlayer)Players.Model.Where(c => c.GetNickname == Nickname).FirstOrDefault();

            return Client;
        }

        public override Player GetPlayerByUsername(string Username)
        {
            var Client = (GPlayer)Players.Model.Where(c => c.GetLogin == Username).FirstOrDefault();

            return Client;
        }

        public override Player GetPlayerByUID(uint UID)
        {
            var Client = (GPlayer)Players.Model.Where(c => c.GetUID == UID).FirstOrDefault();

            return Client;
        }

        public override bool GetPlayerDuplicate(uint UID)
        {
            throw new NotImplementedException();
        }

        public override bool PlayerDuplicateDisconnect(uint UID)
        {
            throw new NotImplementedException();
        }

        protected override void OnAuthServerPacketReceive(ClientAuth client, AuthPacketInfo packet)
        {
            if (packet.ID != AuthPacketEnum.SERVER_KEEPALIVE)
            {
                WriteConsole.WriteLine("[SYNC_RECEIVED_PACKET]:  " + packet.ID);
            }
            switch (packet.ID)
            {
                case AuthPacketEnum.SERVER_KEEPALIVE: //KeepAlive
                    {
                    }
                    break;
                case AuthPacketEnum.SERVER_CONNECT:
                    {
                    }
                    break;
                case AuthPacketEnum.SERVER_RELEASE_CHAT:
                    {
                        string GetNickName = packet.Message.PlayerNick;
                        string GetMessage = packet.Message.PlayerMessage;
                        PacketCreator.Creator.ChatText(GetNickName, GetMessage, true);
                    }
                    break;
                case AuthPacketEnum.RECEIVES_USER_UID:
                    break;
                case AuthPacketEnum.SEND_DISCONNECT_PLAYER:
                    {
                        uint UID = packet.Message.ID;

                        var player = GetPlayerByUID(UID);

                        if (player != null)
                        {
                            DisconnectPlayer(player);
                        }
                    }
                    break;
                case AuthPacketEnum.SERVER_RELEASE_TICKET:
                    {
                        string GetNickName = packet.Message.GetNickName;
                        string GetMessage = packet.Message.GetMessage;
                        using (var result = new PangyaBinaryWriter())
                        {
                            result.Write(new byte[] { 0xC9, 0x00 });
                            result.WritePStr(GetNickName);
                            result.WritePStr(GetMessage);
                            SendToAll(result.GetBytes());
                        }
                    }
                    break;
                case AuthPacketEnum.SERVER_RELEASE_BOXRANDOM:
                    {
                        string GetMessage = packet.Message.GetMessage;
                        Notice(GetMessage);
                    }
                    break;
                case AuthPacketEnum.SERVER_RELEASE_NOTICE_GM:
                    {
                        string Nick = packet.Message.GetNick;
                        string message = packet.Message.mensagem;
                        HandleStaffSendNotice(Nick, message);
                    }
                    break;
                case AuthPacketEnum.SERVER_RELEASE_NOTICE:
                    {
                        string message = packet.Message.mensagem;
                        using (var result = new PangyaBinaryWriter())
                        {
                            result.Write(new byte[] { 0x42, 0x00 });
                            result.WritePStr("Aviso: " + message);
                            SendToAll(result.GetBytes());
                        }
                    }
                    break;
                case AuthPacketEnum.PLAYER_LOGIN_RESULT:
                    {
                        LoginResultEnum loginresult = packet.Message.Type;

                        if (loginresult == LoginResultEnum.Error || loginresult == LoginResultEnum.Exception)
                        {
                            WriteConsole.WriteLine("[CLIENT_ERROR]: Sorry", ConsoleColor.Red);
                            return;
                        }
                    }
                    break;
                case AuthPacketEnum.SERVER_COMMAND:
                    break;
                default:
                    WriteConsole.WriteLine("[AUTH_PACKET]:  " + packet.ID);
                    break;
            }
        }
        public void HandleStaffSendNotice(string Nickname, string Msg)
        {
            var response = new PangyaBinaryWriter();
            try
            {
                if (Nickname.Length <= 0 || Msg.Length <= 0)
                {
                    return;
                }

                response.Write(new byte[] { 0x40, 0x00, 0x07 });
                response.WritePStr(Nickname);
                response.WritePStr(Msg);
                this.SendToAll(response.GetBytes());
            }
            finally
            {
                response.Dispose();
            }
        }

        public override void RunCommand(string[] Command)
        {
            string ReadCommand;
            GPlayer P;

            if (Command.Length > 1)
            {
                ReadCommand = Command[1];
            }
            else
            {
                ReadCommand = "";
            }
            switch (Command[0])
            {
                case "cls":
                case "limpar":
                case "clear":
                    {
                        Console.Clear();
                    }
                    break;
                case "kickuid":
                    {
                        P = (GPlayer)GetPlayerByUID(uint.Parse(ReadCommand.ToString()));
                        if (P == null)
                        {
                            WriteConsole.WriteLine("[SYSTEM_COMMAND]: THIS UID IS NOT ONLINE!", ConsoleColor.Red);
                            break;
                        }
                        DisconnectPlayer(P);
                    }
                    break;
                case "kickname":
                    {
                        P = (GPlayer)GetPlayerByNickname(ReadCommand);
                        if (P == null)
                        {
                            WriteConsole.WriteLine("[SYSTEM_COMMAND]: THIS NICKNAME IS NOT ONLINE!", ConsoleColor.Red);
                            return;
                        }
                        DisconnectPlayer(P);
                    }
                    break;
                case "kickuser":
                    {
                        P = (GPlayer)GetPlayerByUsername(ReadCommand);
                        if (P == null)
                        {
                            WriteConsole.WriteLine("[SYSTEM_COMMAND]: THIS USERNAME IS NOT ONLINE!", ConsoleColor.Red);
                            return;
                        }
                        DisconnectPlayer(P);
                    }
                    break;
                case "topnotice":
                    {
                        if (ReadCommand.Length > 0)
                            Notice(ReadCommand);
                    }
                    break;
                case "lobby":
                case "listalobby":
                case "canais":
                case "listacanais":
                case "showlobby":
                case "showchannel":
                    {
                        LobbyList.ShowLobby();
                    }
                    break;
                case "comandos":
                case "commands":
                case "ajuda":
                case "help":
                    {
                        ShowHelp();
                    }
                    break;
                case "quit":
                case "exit":
                case "close":
                case "sair":
                case "fechar":
                    {
                        Console.WriteLine("The server was stopped!");
                        Environment.Exit(1);
                    }
                    break;
                default:
                    {
                        WriteConsole.WriteLine("[SYSTEM_COMMAND]: Sorry Unknown Command, type 'help' to get the list of commands", ConsoleColor.Red);
                    }
                    break;
            }
        }

        public void RemoveLobbyPlayer(GPlayer player)
        {
            //remove to player in lobby list
            LobbyList.HandleRemoveLobbyPlayer(player);
        }

        public Lobby GetLobbyByID(byte ID)
        {
            foreach (var lobby in LobbyList)
            {
                if (lobby.Info.Id == ID)
                {
                    return lobby;
                }
            }
            return null;
        }
        public Lobby GetLobbyByName(string Name)
        {
            foreach (var lobby in LobbyList)
            {
                if (lobby.Info.Name == Name)
                {
                    return lobby;
                }
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SendTarget(byte ID, byte[] Data)
        {
            foreach (GPlayer Client in Players.Model)
            {
                if (Client.Lobby.Info.Id == ID)
                {
                    Client.SendResponse(Data);
                }
            }
        }

        public byte[] CreateLobbyList()
        {
            return LobbyList.Build(true);
        }

    }
}
