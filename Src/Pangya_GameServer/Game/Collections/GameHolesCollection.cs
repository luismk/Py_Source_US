using Pangya_GameServer.Common;
using Pangya_GameServer.Flags;
using PangyaAPI.Helper.Tools;
using PangyaAPI.Helper.BinaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Pangya_GameServer.Game.Collections
{
    public class GameHolesCollection : List<HoleInformation>
    {
        protected Random rnd;
        byte m_currentHole;
        byte m_holeCount;
        public HoleInformation CurrentHole
        {
            get
            {
                return FGetCurrentHole();
            }
        }

        //Constructor
        public GameHolesCollection()
        {
            m_currentHole = 0;
            m_holeCount = 0;

            for (int I = 0; I < 18; I++)
            {
                this.Add(new HoleInformation());
            }
            rnd = new Random();
        }
       
        internal void RebuildGameHole(GameMapFlag Map)
        {
            byte WP;
            byte WD;
            byte P;

            for (int I = 0; I < 17; I++)
            {
                WP = (byte)rnd.Next(0, 8);
                WD = (byte)rnd.Next(255);
                P = (byte)(rnd.Next(1, 3));

                this[I].Hole = (byte)(I + 1);
                this[I].Weather = (byte)rnd.Next(0, 3);
                this[I].WindPower = WP;
                this[I].WindDirection = WD;
                this[I].Map = Map;
                this[I].Pos = P;
            }
        }
        void InitGameHole(GameModeFlag gameMode, GameTypeFlag gameType, bool IsRepeted = false, GameMapFlag Map = GameMapFlag.Blue_Lagoon)
        {
            byte WP;
            byte WD;
            byte P;
            int x;
            int[] H;
            int[] M;

            if (Map == GameMapFlag.Unknown)
            {
                Map = (GameMapFlag)GameTools.GetMap();
            }

            if (gameType == GameTypeFlag.HOLE_REPEAT && IsRepeted)
            {

                for (x = 0; x <= 17; x++)
                {
                    WP = (byte)rnd.Next(0, 8);
                    WD = (byte)rnd.Next(255);
                    P = (byte)rnd.Next(1, 3);

                    this[x].Hole = (byte)(x + 1);
                    this[x].Weather = (byte)rnd.Next(0, 3);
                    this[x].WindPower = WP;
                    this[x].WindDirection = WD;
                    this[x].Map = Map;
                    this[x].Pos = P;
                }
                // leave
                return;
            }
            switch (gameMode)
            {
                case GameModeFlag.GAME_MODE_FRONT:
                    for (x = 0; x <= 17; x++)
                    {
                        this[x].Hole = (byte)(x + 1);
                        this[x].Weather = (byte)rnd.Next(0, 3);
                        this[x].WindPower = (byte)rnd.Next(0, 8);
                        this[x].WindDirection = (byte)rnd.Next(255);
                        this[x].Map = Map;
                        this[x].Pos = (byte)rnd.Next(1, 3);
                    }
                    break;
                case GameModeFlag.GAME_MODE_BACK:
                    for (x = 0; x <= 17; x++)
                    {
                        this[x].Hole = (byte)(18 - x);
                        this[x].Weather = (byte)rnd.Next(0, 3);
                        this[x].WindPower = (byte)rnd.Next(0, 8);
                        this[x].WindDirection = (byte)rnd.Next(255);
                        this[x].Pos = (byte)rnd.Next(1, 3);
                    }
                    break;
                case GameModeFlag.GAME_MODE_SHUFFLE:
                case GameModeFlag.GAME_MODE_RANDOM:
                    H = GameTools.RandomHole();
                    for (x = 0; x <= 17; x++)
                    {
                        this[x].Hole = (byte)H[x];
                        this[x].Weather = (byte)rnd.Next(0, 3);
                        this[x].WindPower = (byte)rnd.Next(0, 8);
                        this[x].WindDirection = (byte)rnd.Next(255);
                        this[x].Map = Map;
                        this[x].Pos = (byte)rnd.Next(1, 3);
                    }
                    break;
                case GameModeFlag.GAME_MODE_SSC:
                    H = GameTools.RandomHole();
                    M = GameTools.RandomMap();
                    for (x = 0; x <= 17; x++)
                    {
                        this[x].Hole = (byte)H[x];
                        this[x].Weather = (byte)rnd.Next(0, 3);
                        this[x].WindPower = (byte)rnd.Next(0, 8);
                        this[x].WindDirection = (byte)rnd.Next(255);
                        this[x].Map = (GameMapFlag)M[x];
                        this[x].Pos = (byte)rnd.Next(1, 3);
                    }
                    this.Last().Hole = (byte)(rnd.Next(0, 2) + 1);
                    this.Last().Map = GameMapFlag.Special_Flag;
                    break;
                default:
                    for (x = 0; x <= 17; x++)
                    {
                        this[x].Hole = (byte)(x + 1);
                        this[x].Weather = (byte)rnd.Next(0, 3);
                        this[x].WindPower = (byte)rnd.Next(0, 8);
                        this[x].WindDirection = (byte)rnd.Next(255);
                        this[x].Map = Map;
                        this[x].Pos = (byte)rnd.Next(1, 3);
                    }
                    break;
            }
        }

        public void Init(GameModeFlag Mode, GameTypeFlag Type, bool Repeted, GameMapFlag Map, byte holeCount)
        {
            m_holeCount = holeCount;
            InitGameHole(Mode, Type, Repeted, Map);
        }

        private HoleInformation FGetCurrentHole()
        {
            HoleInformation result;
            result = this[m_currentHole];
            return result;
        }

        public bool GoToNext()
        {
            bool result;
            m_currentHole++;
            result = m_currentHole < m_holeCount;
            if (!result)
            {
                m_currentHole = 0;
            }
            return result;
        }


        public byte[] GetData()
        {
            var rnd = new Random();
            using (var result = new PangyaBinaryWriter())
            {
                foreach (var H in this)
                {
                    result.Write(rnd.Next());
                    result.Write(H.Pos);
                    result.Write((byte)H.Map);
                    result.Write(H.Hole);
                }
                //CoinData
                result.Write(new byte[]
                {
                    0x03, 0x4F, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00
                });
                var GetBytes = result.GetBytes();
                return GetBytes;
            }
        }


        #region IDisposable Support
        private bool disposedValue = false; // Para detectar chamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Clear();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~GameHolesCollection()
        {
            Dispose(false);
        }

        #endregion

    }
}
