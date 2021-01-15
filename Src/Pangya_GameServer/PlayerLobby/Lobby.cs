using System;
using System.Linq;
using PangyaAPI.Helper;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.Helper.Tools;
using PangyaAPI.PangyaPacket;
using Pangya_GameServer.Flags;
using Pangya_GameServer.Game.Collections;
using Pangya_GameServer.Game.Common;
using Pangya_GameServer.Game.Model;
using Pangya_GameServer.GamePlayer;
using Pangya_GameServer.PacketCreator;
using Pangya_GameServer.PlayerLobby.Common;
namespace Pangya_GameServer.PlayerLobby
{
    public class Lobby
    {
        public LobbyInfo Info { get; set; }
        public GenericDisposableCollection<GPlayer> Players { get; set; }
        public GameCollection Games { get; set; }
        public Lobby this[GPlayer player] => Program.GameServer.LobbyList.GetLobby(player.Lobby);
        public GameBase this[ushort GameID] => GetGameHandle(GameID);
        public bool IsFull { get { return Players.Count >= Info.MaxPlayers; } }
        public Lobby(LobbyInfo info)
        {
            Info = info;
            Games = new GameCollection(this);
            Players = new GenericDisposableCollection<GPlayer>();
        }

        public bool AddPlayer(GPlayer player)
        {
            if (Players.Model.Any(c => c == player) == false)
            {
                Players.Add(player);
                Info.SetPlayerCounter(Players.Count);
                player.Lobby = this; //Define no Player qual lobby ele está
                return true;
            }
            return false;
        }

        public byte[] BuildGameLists()
        {
            return Games.GameAction();
        }

        public byte[] BuildPlayerLists()
        {
            Info.SetPlayerCounter(Players.Count);

            var result = new PangyaBinaryWriter();
            result.Write(new byte[] { 0x46, 0x00 });
            result.WriteByte(4);
            result.WriteByte(Info.PlayersCount);
            foreach (GPlayer session in Players.Model)
            {
                if (session.InLobby)
                {
                    result.Write(session.GetLobbyInfo());
                }
            }
            return result.GetBytes();
        }


        public void DestroyGame(GameBase GameHandle)
        {
            DestroyGameEvent(GameHandle);

            GameHandle.Terminating = true;
            GameHandle.TerminateTime = DateTime.Now;
            //{ remove from game list }
            Games.GameRemove(GameHandle);
        }


        public GameBase GetGameHandle(GPlayer session)
        {
            return Games.GetByID(session.GameID);
        }

        public GameBase GetGameHandle(ushort GameID)
        {
            return Games.GetByID(GameID);
        }

        public void PlayerLeaveGame(GPlayer session)
        {
            var GameHandle = session.Game;

            if (GameHandle == null)
            {
                return;
            }
            GameHandle.RemovePlayer(session);
        }

        public void PlayerLeaveGP(GPlayer session)
        {
            var GameHandle = session.Game;

            if (GameHandle == null)
            {
                return;
            }

            GameHandle.RemovePlayer(session);

            PlayerGetTime(session);
            session.SendResponse(new byte[] { 0x54, 0x02, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF });
        }

        public void PlayerJoinGame(GPlayer session, Packet packet)
        {
            packet.ReadUInt16(out ushort GameID);
            packet.ReadPStr(out string Pass);

            GameBase GameHandle = this[GameID];

            if (GameHandle == null)
            {
                session.SendResponse(Creator.ShowRoomError(GameCreateResultFlag.CREATE_GAME_ROOM_DONT_EXISTS));
                return;
            }

            if (GameHandle.Password.Length > 0 && session.GetCapability < 4)
            {
                if (GameHandle.Password != Pass)
                {
                    session.SendResponse(Creator.ShowRoomError(GameCreateResultFlag.CREATE_GAME_INCORRECT_PASSWORD));

                    return;
                }
            }

            if (GameHandle.Count == 0 && GameHandle.GameType == GameTypeFlag.GM_EVENT)
            {
                GameHandle.AddPlayerInEvent(session);
            }
            else
            {
                GameHandle.AddPlayer(session);
            }

        }

        public void PlayerRequestGameInfo(GPlayer session, Packet packet)
        {
            packet.ReadUInt16(out ushort GameID);

            GameBase GameHandle = this[GameID];
            if (GameHandle == null)
                return;


            session.SendResponse(GameHandle.GetGameInfo()); // : TODO ---------------------
        }

        public void JoinMultiplayerGamesList(GPlayer session)
        {
            WriteConsole.WriteLine($"[PLAYER_LOBBY_LIST]: TOTAL GAMES IN LOBBY => {Games.Count}", ConsoleColor.Green);

            if (session.InLobby)
                return;
            // Set Current User To Lobby
            session.InLobby = true;
            //Send List Players in Lobby
            Send(BuildPlayerLists());

            session.SendResponse(BuildGameLists());

            session.SendResponse(new byte[] { 0xF5, 0x00 });

            if (Players.Count > 1)
                Send(Creator.ShowPlayerAction(session, LobbyActionFlag.CREATE));
        }

        public void LeaveMultiplayerGamesList(GPlayer session)
        {
            if (session.InLobby)
            {
                session.InLobby = false; // Set Current User To Lobby
                // Send to All player
                Send(Creator.ShowPlayerAction(session, LobbyActionFlag.DESTROY));
            }
        }

        internal GameBase CreateGame(GPlayer session, GameCommon GameData)
        {
            if (Games.Limit)
            {
                session.SendResponse(Creator.ShowRoomError(GameCreateResultFlag.CREATE_GAME_CREATE_FAILED2));
                return null;
            }

            GameBase result = null;
            switch (GameData.GameType)
            {
                case GameTypeFlag.VERSUS_STROKE:
                case GameTypeFlag.VERSUS_MATCH:
                    {
                        //  result = new ModeVersus(session, GameData, CreateGameEvent, UpdateGameEvent, DestroyGame, PlayerJoinGameEvent, PlayerLeaveGameEvent, Games.GetID);
                    }
                    break;
                case GameTypeFlag.TOURNEY_TEAM:
                case GameTypeFlag.TOURNEY:
                    {
                        //result = new ModeTorney(session, GameData, CreateGameEvent, UpdateGameEvent, DestroyGame, PlayerJoinGameEvent, PlayerLeaveGameEvent, Games.GetID);
                    }
                    break;
                case GameTypeFlag.CHAT_ROOM:
                    {
                        // result = new ModeChatRoom(session, GameData, CreateGameEvent, UpdateGameEvent, DestroyGame, PlayerJoinGameEvent, PlayerLeaveGameEvent, Games.GetID);
                    }
                    break;
                case GameTypeFlag.TOURNEY_GUILD:
                    {
                        //  result = new ModeGuildBattle(session, GameData, CreateGameEvent, UpdateGameEvent, DestroyGame, PlayerJoinGameEvent, PlayerLeaveGameEvent, Games.GetID);
                    }
                    break;
                case GameTypeFlag.GM_EVENT:
                    {
                        //  result = new ModeGMEvent(session, GameData, CreateGameEvent, UpdateGameEvent, DestroyGame, PlayerJoinGameEvent, PlayerLeaveGameEvent, Games.GetID);
                    }
                    break;
                case GameTypeFlag.CHIP_IN_PRACTICE:
                    {
                        //  result = new ModeChipInPractice(session, GameData, CreateGameEvent, UpdateGameEvent, DestroyGame, PlayerJoinGameEvent, PlayerLeaveGameEvent, Games.GetID);
                    }
                    break;
                case GameTypeFlag.SSC:
                    {
                        //  result = new ModeSSC(session, GameData, CreateGameEvent, UpdateGameEvent, DestroyGame, PlayerJoinGameEvent, PlayerLeaveGameEvent, Games.GetID);
                    }
                    break;
                case GameTypeFlag.HOLE_REPEAT:
                    {
                        // result = new ModePractice(session, GameData, CreateGameEvent, UpdateGameEvent, DestroyGame, PlayerJoinGameEvent, PlayerLeaveGameEvent, Games.GetID);
                    }
                    break;
                default:
                    {
                        session.SendResponse(Creator.ShowRoomError(GameCreateResultFlag.CREATE_GAME_CANT_CREATE));
                        return null;
                    }

            }
            Games.Create(result);
            return result;
        }

        public void CreateGameEvent(GameBase GameHandle)
        {
            if (GameHandle == null || GameHandle.GameType == GameTypeFlag.HOLE_REPEAT)
            {
                return;
            }
            Send(Creator.ShowGameAction(GameHandle.GameInformation(), GameActionFlag.CREATE));
        }

        public void DestroyGameEvent(GameBase GameHandle)
        {
            if (GameHandle == null || GameHandle.GameType == GameTypeFlag.HOLE_REPEAT)
            {
                return;
            }
            Send(Creator.ShowGameAction(GameHandle.GameInformation(), GameActionFlag.DESTROY));
        }


        public void PlayerJoinGameEvent(GameBase GameHandle, GPlayer session)
        {
            if (GameHandle == null || GameHandle.GameType == GameTypeFlag.HOLE_REPEAT)
            {
                return;
            }
            Send(Creator.ShowPlayerAction(session, LobbyActionFlag.UPDATE));
        }

        public void PlayerLeaveGameEvent(GameBase GameHandle, GPlayer session)
        {
            if (GameHandle == null || GameHandle.GameType == GameTypeFlag.HOLE_REPEAT)
            {
                return;
            }
            Send(Creator.ShowPlayerAction(session, LobbyActionFlag.UPDATE));
        }

        public void UpdateGameEvent(GameBase GameHandle)
        {
            if (GameHandle == null || GameHandle.GameType == GameTypeFlag.HOLE_REPEAT)
            {
                return;
            }
            Send(Creator.ShowGameAction(GameHandle.GameInformation(), GameActionFlag.UPDATE));
        }

        public void PlayerSendChat(GPlayer session, string Messages)
        {
            if (session.Game != null)
            {
                var GameHandle = session.Game;
                GameHandle.Write(Creator.ChatText(session.GetNickname, Messages, session.GetCapability == 4 || session.GetCapability == 15));
                DebugCommand(session, Messages, session.GetCapability == 4 || session.GetCapability == 15);

            }
            else
            {
                foreach (GPlayer Client in this.Players.Model)
                {
                    if (Client.InLobby && Client.GameID == 0xFFFF)
                    {
                        Client.SendResponse(Creator.ChatText(session.GetNickname, Messages, session.GetCapability == 4 || session.GetCapability == 15));
                    }
                }
                DebugCommand(session, Messages, session.GetCapability == 4 || session.GetCapability == 15);
            }
        }


        public void PlayerSendWhisper(GPlayer session, string Nickname, string Messages)
        {
            PangyaBinaryWriter Response;
            GPlayer Client;


            Response = new PangyaBinaryWriter();

            try
            {
                Client = (GPlayer)Program.GameServer.GetPlayerByNickname(Nickname);


                if (Client == null)
                {
                    Response.Write(new byte[] { 0x40, 0x00 });
                    Response.WriteByte(5); //Status          
                    Response.WritePStr(Nickname);
                    session.SendResponse(Response);
                    return;
                }
                if (!Client.InLobby)
                {
                    Response.Write(new byte[] { 0x40, 0x00 });
                    Response.WriteByte(4); //Status          
                    Response.WritePStr(Nickname);
                    session.SendResponse(Response);
                    return;
                }

                Response = new PangyaBinaryWriter();
                Response.Write(new byte[] { 0x84, 0x00 });
                Response.WriteByte(0); //atingir player       
                Response.WritePStr(session.GetNickname);
                Response.WritePStr(Messages);
                Client.SendResponse(Response);

                Response = new PangyaBinaryWriter();
                Response.Write(new byte[] { 0x84, 0x00 });
                Response.WriteByte(1);//atingir player             
                Response.WritePStr(Nickname);
                Response.WritePStr(Messages);
                session.SendResponse(Response);
            }
            finally
            {
                if (Response != null)
                {
                    Response.Dispose();
                }
            }
        }
        public void RemovePlayer(GPlayer session)
        {
            Program.GameServer.RemoveLobbyPlayer(session);
            Info.SetPlayerCounter(Players.Count);

            GameBase GameHandle = session.Game;

            if (GameHandle != null)
            {
                GameHandle.RemovePlayer(session);
            }

            if (session.InLobby)
            {
                LeaveMultiplayerGamesList(session);
            }
        }
        public void PlayerGetTime(GPlayer session)
        {
            session.Response.Write(new byte[] { 0xBA, 0x00 });
            session.Response.WriteTime();
            session.SendResponse();
        }
        public void UpdatePlayerLobbyInfo(GPlayer session)
        {
            if (session.InLobby)
            {
                Send(Creator.ShowPlayerAction(session, LobbyActionFlag.UPDATE));
                session.SendResponse(new byte[] { 0x0F, 0x00 });
            }
        }
        public int PlayersInLobby()
        {
            return Players.Model.Where(c => c.InLobby == true).ToList().Count;
        }

        public void Send(byte[] data)
        {
            foreach (GPlayer client in Players.Model)
                client.SendResponse(data);
        }

        /// <summary>
        /// Command Chat Debug, Using in chat : command @sendpangs 1000
        /// </summary>
        protected void DebugCommand(GPlayer client, string message, bool IsAdmin)
        {
            string[] Command;
            string ReadCommand = "";

            Command = message.Split(new char[] { ' ' });


            if (Command.Length > 1)
            {
                ReadCommand = Command[1].ToUpper();
            }
            if (Command[0].ToUpper() == "COMMAND" && IsAdmin)
            {
                switch (ReadCommand)
                {
                    case "@SENDPANGS":
                        {
                            if (Command.Length >= 3)
                            {
                                var pang = uint.Parse(Command[2]);

                                if (pang <= 0) { return; }

                                foreach (GPlayer session in Players.Model)
                                {
                                    if (session.InLobby)
                                    {
                                        session.AddPang(pang);
                                    }
                                    session.SendPang();
                                }
                            }
                        }
                        break;
                    case "@SENDCOOKIES":
                        {
                            if (Command.Length >= 3)
                            {
                                var cookies = uint.Parse(Command[2]);

                                if (cookies <= 0) { return; }

                                foreach (GPlayer session in Players.Model)
                                {
                                    if (session.InLobby)
                                    {
                                        session.AddCookie(cookies);
                                    }
                                    session.SendCookies();
                                }
                            }
                        }
                        break;
                    case "@WAREHOUSE":
                        {
                            if (Command.Length >= 3)
                            {
                                var typeID = uint.Parse(Command[2]);

                                if (typeID <= 0) { return; }

                            }
                        }
                        break;
                    default:
                        {
                            WriteConsole.WriteLine("[COMMAND GM] Unknown !");
                            return;
                        }
                }
                WriteConsole.WriteLine("[COMMAND GM] Comando de GM recebido de: " + client.GetLogin);
            }
        }
    }
}
