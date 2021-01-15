using System.Runtime.InteropServices;
namespace PangyaAPI.PangyaClient.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PlayerStatistic
    {
        public uint Drive { get; set; }
        public uint Putt { get; set; }
        public uint PlayTime { get; set; }
        // Second
        public uint ShotTime { get; set; }
        public float LongestDistance { get; set; }
        public uint Pangya { get; set; }
        public uint TimeOut { get; set; }
        public uint OB { get; set; }
        public uint DistanceTotal { get; set; }
        public uint Hole { get; set; }
        public uint TeamHole { get; set; }
        public uint HIO { get; set; }
        public ushort Bunker { get; set; }
        public uint Fairway { get; set; }
        public uint Albratoss { get; set; }
        public uint Holein { get; set; }
        public uint Puttin { get; set; }
        public float LongestPutt { get; set; }
        public float LongestChip { get; set; }
        public uint EXP { get; set; }
        public byte Level { get; set; }
        public long Pang { get; set; }
        public uint TotalScore { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public byte[] Score;
        public byte Unknown { get; set; }
        public ulong MaxPang0 { get; set; }
        public ulong MaxPang1 { get; set; }
        public ulong MaxPang2 { get; set; }
        public ulong MaxPang3 { get; set; }
        public ulong MaxPang4 { get; set; }
        public ulong SumPang { get; set; }
        public uint GamePlayed { get; set; }
        public uint Disconnected { get; set; }
        public uint TeamWin { get; set; }
        public uint TeamGame { get; set; }
        public uint LadderPoint { get; set; }
        public uint LadderWin { get; set; }
        public uint LadderLose { get; set; }
        public uint LadderDraw { get; set; }
        public uint LadderHole { get; set; }
        public uint ComboCount { get; set; }
        public uint MaxCombo { get; set; }
        public uint NoMannerGameCount { get; set; }
        public ulong SkinsPang { get; set; }
        public uint SkinsWin { get; set; }
        public uint SkinsLose { get; set; }
        public uint SkinsRunHole { get; set; }
        public uint SkinsStrikePoint { get; set; }
        public uint SKinsAllinCount { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x06)]
        public byte[] Unknown1;
        public uint GameCountSeason { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x08)]
        public byte[] Unknown2;

        public PlayerStatistic Init()
        {
            return new PlayerStatistic()
            {
                Score = new byte[5],
                Unknown1 = new byte[6],
                Unknown2 = new byte[8]
            };
        }
        public static PlayerStatistic operator +(PlayerStatistic Left, PlayerStatistic Right)
        {
            var Result = new PlayerStatistic()
            {
                //{ Drive }
                Drive = Left.Drive + Right.Drive,
                //{ Putt}
                Putt = Left.Putt + Right.Putt,
                //{ Player Time Do Nothing }
                PlayTime = Left.PlayTime,
                //{ Shot Time }
                ShotTime = Left.ShotTime + Right.ShotTime
            };
            //{ Longest }
            if (Right.LongestDistance > Left.LongestDistance)
            {
                Result.LongestDistance = Right.LongestDistance;
            }
            else
            {
                Result.LongestDistance = Left.LongestDistance;
            }
            //{ Hit Pangya }
            Result.Pangya = Left.Pangya + Right.Pangya;
            //{ Timeout }
            Result.TimeOut = Left.TimeOut;
            //{ OB }
            Result.OB = Left.OB + Right.OB;
            //{ Total Distance }
            Result.DistanceTotal = Left.DistanceTotal + Right.DistanceTotal;
            //{ Hole Total }
            Result.Hole = Left.Hole + Right.Hole;
            //{ Team Hole }
            Result.TeamHole = Left.TeamHole;
            //{ Hole In One }
            Result.HIO = Left.HIO;
            //{ Bunker }
            Result.Bunker = (ushort)(Left.Bunker + Right.Bunker);
            //{ Fairway }
            Result.Fairway = Left.Fairway + Right.Fairway;
            //{ Albratoss }
            Result.Albratoss = Left.Albratoss + Right.Albratoss;
            //{ Holein ? }
            Result.Holein = Left.Holein + (Result.Hole - Right.Holein);
            //{ Puttin }
            Result.Puttin = Left.Puttin + Right.Puttin;
            //{ Longest Putt }
            if (Right.LongestPutt > Left.LongestPutt)
            {
                Result.LongestPutt = Right.LongestPutt;
            }
            else
            {
                Result.LongestPutt = Left.LongestPutt;
            }
            //{ Longest Chip }
            if (Right.LongestChip > Left.LongestChip)
            {
                Result.LongestChip = Right.LongestChip;
            }
            else
            {
                Result.LongestChip = Left.LongestChip;
            }
            return Result;
        }
    }
}
