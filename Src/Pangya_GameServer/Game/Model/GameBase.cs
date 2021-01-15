using System;
using System.Linq;
using System.Collections.Generic;
using Pangya_GameServer.Common;
using Pangya_GameServer.Flags;
using Pangya_GameServer.GamePlayer;
using Pangya_GameServer.PacketCreator;
using PangyaAPI.Helper.BinaryModels;
using PangyaAPI.PangyaPacket;
using Pangya_GameServer.Game.Collections;
using Pangya_GameServer.Game.Common;
using Pangya_GameServer.PlayerLobby;
using PangyaAPI.Helper;
namespace Pangya_GameServer.Game.Model
{
    public abstract partial class GameBase
    {
        #region Fields
        public uint ID { get; set; }

        public string Password { get { return fGameData.Password; } set { fGameData.Password = value; } }

        public GameCommon GameData { get { return fGameData; } set { fGameData = value; } }

        public GameTypeFlag GameType { get { return fGameData.GameType; } }

        public bool GMEvent;
        // Hole Repeater
        public byte HoleNumber;
        public uint LockHole;
        // Game Data

        // Grandprix
        public bool IsGP;
        public UInt32 GPTypeID;
        public UInt32 GPTypeIDA;
        public UInt32 GPTime;
        public DateTime GPStart;

        public DateTime GameStart { get; set; }
        public DateTime GameEnd { get; set; }

        public uint GetTrophy { get { { return Trophy; } } }
        public byte Count { get { { return (byte)Players.Count; } } }
        public bool HoleComplete { get; set; }
        // Terminating
        public bool Terminating { get; set; } = false;
        public DateTime TerminateTime { get; set; }
        public bool GameStarted { get { return Started; } }


        protected GameCommon fGameData;
        public Point3D CurrentHole;
        protected uint Owner { get; set; }
        protected bool Started { get; set; }
        protected bool Await { get; set; }
        protected uint Trophy { get; set; }
        protected byte Idle;
        // Trophy Showing
        protected uint Gold { get; set; }
        protected uint Silver1 { get; set; }
        protected uint Silver2 { get; set; }
        protected uint Bronze1 { get; set; }
        protected uint Bronze2 { get; set; }
        protected uint Bronze3 { get; set; }
        // Medal Showing
        protected uint BestRecovery { get; set; }
        protected uint BestChipIn { get; set; }

        protected uint BestDrive { get; set; }
        protected uint BestSpeeder { get; set; }
        protected uint LongestPutt { get; set; }
        protected uint LuckyAward { get; set; }
        protected bool FirstShot { get; set; }
        // Map
        protected byte Map { get; set; }
        public List<GPlayer> Players { get; set; }

        protected List<GameData> PlayerData { get; set; }
        // UID AND GAMEScoreDATA
        protected Dictionary<uint, GameScoreData> Scores { get; set; }
        protected GameHolesCollection Holes { get; set; }
        /// <summary>
        /// Room Key 
        /// </summary>
        protected byte[] GameKey { get; set; }
        // Event
        protected GameEvent Create { get; set; }//cria sala
        protected GameEvent Update { get; set; }//update sala
        protected GameEvent Destroy { get; set; }//destroy sala
        // Player Event
        protected PlayerEvent PlayerJoin { get; set; }
        protected PlayerEvent PlayerLeave { get; set; }

        #endregion

        #region Delegate
        public delegate void LobbyEvent(Lobby Lobby, GameBase Game);

        public delegate void GameEvent(GameBase Game);

        public delegate void PlayerEvent(GameBase Game, GPlayer Player);

        #endregion

        #region Abstract Method's
        //Disconnect Game
        public abstract void PlayerGameDisconnect(GPlayer player);
        //create player 
        public abstract void SendPlayerOnCreate(GPlayer player);
        //join player
        public abstract void SendPlayerOnJoin(GPlayer player);
        public abstract void SendHoleData(GPlayer player);
        public abstract void OnPlayerLeave();
        //check room
        public abstract bool Validate();
        //Gera experiencia(XP)
        public abstract void GenerateExperience();
        public abstract void PlayerLoading(GPlayer player, Packet CP);
        public abstract void PlayerShotInfo(GPlayer player, Packet CP);
        public abstract void PlayerShotData(GPlayer player, Packet CP);
        public abstract void PlayerLoadSuccess(GPlayer player);
        public abstract void PlayerLeavePractice();
        public abstract void PlayerStartGame();
        public abstract void PlayerSyncShot(GPlayer player, Packet CP);
        // Final Result       
        public abstract void PlayerSendFinalResult(GPlayer player, Packet CP);
        public abstract byte[] GameInformation();
        public abstract byte[] GetGameHeadData();
        public abstract void DestroyRoom();
        public abstract void AcquireData(GPlayer player);

        #endregion

        #region Construtor

        public GameBase(GPlayer player, GameCommon GameInfo, GameEvent CreateEvent, GameEvent UpdateEvent, GameEvent DestroyEvent, PlayerEvent OnJoin, PlayerEvent OnLeave, ushort GameID)
        {
            player.InGame = true;
            //{ Create Game Data }
            Players = new List<GPlayer>();
            PlayerData = new List<GameData>();
            Scores = new Dictionary<uint, GameScoreData>();
            Holes = new GameHolesCollection();
            //{ Game Data }
            fGameData = GameInfo;
            ID = GameID;
            Create = CreateEvent;
            Update = UpdateEvent;
            Destroy = DestroyEvent;
            PlayerJoin = OnJoin;
            PlayerLeave = OnLeave;
            HoleComplete = false;
            CreateKey();
            Terminating = false;
            Started = false;
            Await = false;
            FirstShot = false;
            Trophy = 0;
            Idle = 0;
            if (Validate() == false)
            {
                player.SendResponse(Creator.ShowRoomError(GameCreateResultFlag.CREATE_GAME_CREATE_FAILED));
                return;
            }

            if (Players.Count > fGameData.MaxPlayers)
            {
                player.SendResponse(Creator.ShowRoomError(GameCreateResultFlag.CREATE_GAME_RESULT_FULL));
                return;
            }
            if (Add(player))
            {
                SetOwner(player.GetUID);
                player.SetGameID((ushort)ID);
                player.GameInfo.SetDefault();
                SetRole(player, true);
                GenerateTrophy();
                GameUpdate();
                SendGameInfo();
                ComposePlayer();
                Create(this);
                PlayerJoin(this, player);
                if (GameType == GameTypeFlag.CHAT_ROOM)
                {
                    player.GameInfo.ReadyToPlay = true;
                }
                SendPlayerOnCreate(player);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add Player in List
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool AddPlayer(GPlayer player)
        {
            if (null == player)
            {
                if (GameType == GameTypeFlag.GM_EVENT)
                {
                    SetOwner(uint.MaxValue);
                    return false;
                }
                return false;
            }
            if (Players.Any(c => c.GetLogin == player.GetLogin) == false)
            {

                player.Game = this;
                Players.Add(player);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// set the id of the player who just created the room
        /// </summary>
        /// <param name="UID">Player.GetUID</param>
        protected void SetOwner(uint UID)
        {
            Owner = UID;
        }

        /// <summary>
        /// Arrow the type of player is in the room(byte 8 == adm/gm || byte 1 == normal player)
        /// </summary>
        /// <param name="player"></param>
        /// <param name="IsAdmin"></param>
        protected void SetRole(GPlayer player, bool IsAdmin)
        {
            switch (IsAdmin)
            {
                case true: { player.GameInfo.Role = PlayerTypeFlag.Master; } break;
                case false: { player.GameInfo.Role = PlayerTypeFlag.Normal; } break;
            }
            if (GameType == GameTypeFlag.CHAT_ROOM)
            {
                player.GameInfo.ReadyToPlay = true;
            }
            if (Players.Count > 1 && player.GetCapability == 4 || player.GetCapability == 15)
            {
                foreach (var client in Players.Where(c => c.GameInfo.Role == PlayerTypeFlag.Master))
                {
                    client.GameInfo.Role = PlayerTypeFlag.Normal;
                }
                player.GameInfo.Role = PlayerTypeFlag.Master;
            }
        }

        protected void ComposePlayer()
        {
            byte i = 0;
            foreach (var P in Players)
            {
                i += 1;
                P.GameInfo.SlotNumber = i;
            }
        }

        /// <summary>
        /// Creates a key with 16 bytes
        /// I realized that the key is made up of 17 bytes but the last byte is zero, while the other 16 bytes are random
        /// </summary>
        protected void CreateKey()
        {
            var result = new byte[16];

            new Random().NextBytes(result);

            GameKey = result;
        }

        /// <summary>
        /// Update the game head
        /// </summary>
        protected void GameUpdate()
        {
            Send(this.GetGameHeadData());
        }


        /// <summary>
        /// decrypt the shot using the room key
        /// </summary>
        /// <param name="data">bytes packet ShotData</param>
        /// <returns></returns>
        protected byte[] DecryptShot(byte[] data)
        {
            var decrypted = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                decrypted[i] = (byte)(data[i] ^ GameKey[i % 16]);
            }
            return decrypted;
        }

        /// <summary>
        /// Generates a Type ID of the trophy for each player's level
        /// </summary>
        protected void GenerateTrophy()
        {
            uint SumLevel, AvgLevel;
            SumLevel = 0;

            foreach (var P in Players)
            {
                SumLevel += P.GetLevel;
            }
            if (SumLevel <= 0)
            {
                AvgLevel = 0;
            }
            else
            {
                AvgLevel = (uint)(SumLevel / Players.Count);
            }
            switch (AvgLevel)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    {
                        Trophy = 738197504;
                    }
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    {
                        Trophy = 738263040;
                    }
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    {
                        Trophy = 738328576;
                    }
                    break;
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                    {
                        Trophy = 738394112;
                    }
                    break;
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                    {
                        Trophy = 738459648;
                    }
                    break;
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                    {
                        Trophy = 738525184;
                    }
                    break;
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                    {
                        Trophy = 738590720;
                    }
                    break;
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                    {
                        Trophy = 738656256;
                    }
                    break;
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                    {
                        Trophy = 738721792;
                    }
                    break;
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                    {
                        Trophy = 738787328;
                    }
                    break;
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                    {
                        Trophy = 738852864;
                    }
                    break;
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                    {
                        Trophy = 738918400;
                    }
                    break;
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                    {
                        Trophy = 738983936;
                    }
                    break;
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                    {
                        Trophy = 738983936;
                    }
                    break;
            }
            switch (GameType)
            {
                case GameTypeFlag.CHAT_ROOM:
                case GameTypeFlag.VERSUS_MATCH:
                case GameTypeFlag.VERSUS_STROKE:
                    Trophy = 0;
                    break;
            }
        }


        /// <summary>
        /// Create Holes data
        /// </summary>
        protected void BuildHole()
        {
            Holes.Init(GameData.Mode, GameData.GameType, HoleNumber > 0, fGameData.Map, fGameData.HoleTotal);
        }

        /// <summary>
        /// Gets the data from the Holes created
        /// </summary>
        /// <returns></returns>
        protected byte[] GetHoleBuild()
        {
            return Holes.GetData();
        }

        public byte[] GetGameInfo()
        {
            using (var packet = new PangyaBinaryWriter())
            {
                packet.Write(new byte[] { 0x86, 0x00 });
                packet.Write(Started == true ? 0 : 1);
                packet.Write(fGameData.HoleTotal);
                if (fGameData.VSTime > 0)
                {
                    packet.Write(fGameData.VSTime);
                }
                else
                {
                    packet.Write(fGameData.GameTime);//fGameData.VSTime
                }
                packet.Write((byte)fGameData.Map);
                packet.Write((byte)GameType);
                packet.WriteByte((byte)fGameData.Mode);
                packet.Write(Trophy);
                packet.Write(0);
                packet.Write(0);
                packet.WriteZero(6);//unknown
                packet.WriteUInt32(1000);//1000
                return packet.GetBytes();
            }
        }


        /// <summary>
        /// Checks if there is an item to play in Practice Mode GP
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        protected bool CheckItemPractice(uint TypeID)
        {
            var player = Players.First();
            if (player.Inventory.IsExist(TypeID))
            {
                player.Inventory.Remove(TypeID, (uint)1, true);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Send Room information to all players in the room
        /// </summary>
        protected void SendGameInfo()
        {
            PangyaBinaryWriter Response;
            Response = new PangyaBinaryWriter();
            try
            {
                Response.Write(new byte[] { 0x49, 0x00, 0x00, 0x00 }); // TODO
                Response.Write(GameInformation());
                Send(Response.GetBytes());
            }
            finally
            {
                Response.Dispose();
            }
        }

        /// <summary>
        /// Send Packet Response
        /// </summary>
        /// <param name="Data">bytes for respose</param>
        public void Send(byte[] Data)
        {
            foreach (var p in Players)
            {
                p.SendResponse(Data);
            }
        }

        public void Send(PangyaBinaryWriter resp)
        {
            foreach (var p in Players)
            {
                p.SendResponse(resp.GetBytes());
            }
            resp.Clear();
        }

        public void Write(byte[] Data)
        {
            foreach (var p in Players)
            {
                p.SendResponse(Data);
            }
        }

        public void ClearPlayerData()
        {
            PlayerData.Clear();
        }

        public void ClearScoresData()
        {
            Scores.Clear();
        }

        public void SendUnfinishedData()
        {
            foreach (var P in PlayerData)
                if (!P.GameCompleted)
                    Send(Creator.ShowHoleData(P.ConnectionID, P.HolePos, (byte)P.ScoreData.TotalShot, (uint)P.ScoreData.Score, P.ScoreData.Pang, P.ScoreData.BonusPang, false));
        }

        public bool _allFinished()
        {
            foreach (var P in Players)
            {
                if (!P.GameInfo.GameCompleted)
                    return false;
            }
            return true;
        }

        public void CopyScore()
        {
            GameData S;

            ClearPlayerData();
            ClearScoresData();
            foreach (var P in Players)
            {
                S = new GameData();

                S = P.GameInfo;
                PlayerData.Add(S);
                Scores.Add(P.GetUID, S.ScoreData);
            }
        }

        public void ClearHole()
        {
            Holes.Dispose();
        }

        protected bool Add(GPlayer player)
        {
            if (null == player)
            {
                if (GameType == GameTypeFlag.GM_EVENT)
                {
                    SetOwner(uint.MaxValue);
                    return false;
                }
                return false;
            }
            if (Players.Any(c => c.GetLogin == player.GetLogin) == false)
            {

                player.Game = this;
                Players.Add(player);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddPlayerInEvent(GPlayer player)
        {
            AddPlayer(player);

            player.SetGameID((ushort)ID);
            player.GameInfo.SetDefault();
            SetRole(player, true);

            GameUpdate();
            SendGameInfo();
            ComposePlayer();
            SendPlayerOnJoin(player);
        }

        public bool RemovePlayer(GPlayer session)
        {
            if (session == null)
            {
                return false;
            }
            else
            {
                Players.Remove(session);
                PlayerGameDisconnect(session);
                return true;
            }
        }


        public void FindNewMaster()
        {
            foreach (var P in Players)
            {
                Write(Creator.ShowNewMaster(P.ConnectionID));
                Owner = P.GetUID;
                break;
            }
        }

        #endregion
    }
}
