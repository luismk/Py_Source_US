using PangyaAPI.PangyaClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_GameServer.Common
{
    public struct GameScoreData
    {
        public uint Pang { get; set; }
        public uint BonusPang { get; set; }
        public sbyte Score { get; set; }
        public sbyte ParCount { get; set; }
        public sbyte ShotCount { get; set; }
        public ushort TotalShot { get; set; }
        public bool HoleComplete { get; set; }
        public byte HoleCompletedCount { get; set; }
        public PlayerStatistic Statistic { get; set; }
        public byte Rate { get; set; }
        public uint EXP { get; set; }
        public GameReward Reward;
        public bool Quited { get; set; }

        public GameScoreData Initial()
        {
            Statistic = new PlayerStatistic();
            Reward = new GameReward();
            Pang = 0;
            BonusPang = 0;
            Score = 0;
            ParCount = 0;
            ShotCount = 0;
            TotalShot = 1; // { Total shot default is 1 }
            EXP = 0;
            HoleComplete = false;
            HoleCompletedCount = 0;
            Reward.Initial();
            Quited = false;
            Rate = 0;
            return this;
        }
        public void Reverse()
        {
            Initial();
        }
    }
}
