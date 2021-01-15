using Pangya_GameServer.Common;
using Pangya_GameServer.Game.Model;
using Pangya_GameServer.Models;
using Pangya_GameServer.PlayerLobby;
using PangyaAPI.PangyaClient;
using System.Net.Sockets;
namespace Pangya_GameServer.GamePlayer
{
    public partial class GPlayer : Player
    {
        public ushort GameID { get; set; }
        public ulong LockerPang { get; set; }
        public bool InGame { get; set; }
        public bool InLobby { get; set; }
        public string LockerPWD { get; set; }
        public string GetSubLogin { get { return "@" + GetLogin; } }
        public uint Visible { get; set; }
        public uint GetCookie { get; set; }
        public uint GetPang { get { return (uint)UserStatistic.Pang; } }
        public uint GetExpPoint { get { return UserStatistic.EXP; } }
        public GameBase Game { get; set; }
        public Lobby Lobby { get; set; }

        public GameData GameInfo;

        public GuildData GuildInfo;
         public Inventory Inventory { get; set; }

        public GPlayer(TcpClient tcp) : base(tcp)
        {
            GameID = ushort.MaxValue;
        }
    }
}
