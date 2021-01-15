namespace Pangya_GameServer.Common
{
    public struct GameReward
    {
        public bool BestRecovery { get; set; }
        public bool BestChipIn { get; set; }
        public bool BestDrive { get; set; }
        public bool BestSpeeder { get; set; }
        public bool BestLongPutt { get; set; }
        public bool Lucky { get; set; }

        public void Initial()
        {
            BestChipIn = false;
            BestDrive = false;
            BestSpeeder = false;
            BestLongPutt = false;
            Lucky = false;
        }
    }
}
