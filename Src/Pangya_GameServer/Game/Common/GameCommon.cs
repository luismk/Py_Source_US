using Pangya_GameServer.Flags;
using System.Runtime.InteropServices;
namespace Pangya_GameServer.Game.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class GameCommon
    {
        public byte Unknown { get; set; }
        public uint VSTime { get; set; }
        public uint GameTime { get; set; }
        public byte MaxPlayers { get; set; }
        public GameTypeFlag GameType { get; set; }
        public byte HoleTotal { get; set; }
        public GameMapFlag Map { get; set; }
        public GameModeFlag Mode { get; set; }
        public uint NaturalMode { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public uint ArtifactID { get; set; }

        public static GameCommon Init()
        {
            return new GameCommon
            {
                Name = "",
                Password = "",
                ArtifactID = 0
            }; 
        }
    }
}
