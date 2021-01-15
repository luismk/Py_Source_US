using Pangya_GameServer.Flags;
using Pangya_GameServer.PlayerLobby;
using PangyaAPI.Helper.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Pangya_GameServer.Game.Model;
namespace Pangya_GameServer.Game.Collections
{
    public class GameCollection : List<GameBase>
    {
        #region Fields    

        private Lobby Lobby { get; set; }
        ushort GameID { get; set; }
        public bool Limit { get { return Count >= 5; } }

        #endregion

        #region Constructor
        public GameCollection(Lobby lobby)
        {
            Lobby = lobby;
        }
        #endregion

        public byte[] GameAction(GameActionFlag gameAction = GameActionFlag.LIST)
        {
            byte count = Convert.ToByte(Count);
            if (gameAction != GameActionFlag.LIST)
            {
                count = 1;
            }
            using (var result = new PangyaBinaryWriter())
            {
                result.Write(new byte[] { 0x47, 0x00 });
                result.WriteByte(count);
                result.WriteByte((byte)gameAction);//action(0) = list
                result.WriteUInt16(0xFFFF);
                foreach (var Game in this)
                {
                    if (Game.GameType == GameTypeFlag.HOLE_REPEAT || Game.Terminating)
                        continue;
                    result.Write(Game.GameInformation());
                }
                return result.GetBytes();
            }           
        }

        public GameBase GetByID(ushort gameID)
        {
            foreach (var game in this)
            {
                if (game.ID == gameID)
                {
                    return game;
                }
            }
            return null;
        }

        public void Create(GameBase game)
        {
            if (Limit)
            {
                game.Send(PacketCreator.Creator.ShowRoomError(GameCreateResultFlag.CREATE_GAME_CANT_CREATE));
                return;
            }
            this.Add(game);
        }

        public void CreateRomID()
        {
            GameID = 0;
            while (this.Any(c => c.ID == GameID))
            {
                GameID += 1;
            }
        }
        public ushort GetID
        {
            get
            {
                CreateRomID();
                return GameID;
            }
        }

        public int GameRemove(GameBase game)
        {
            if (Remove(game))
            {
                return 1;
            }
            else
                return 0;
        }
    }
}
