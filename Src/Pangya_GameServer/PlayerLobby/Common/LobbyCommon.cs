using System.Runtime.InteropServices;
namespace Pangya_GameServer.PlayerLobby.Common
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class LobbyInfo
    {
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Name { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 44)]
        public byte[] Unknown0 { get; set; }
        public ushort MaxPlayers { get; set; }
        public ushort PlayersCount { get; set; }
        public byte Id { get; set; }
        public uint Flag { get; set; }
        public uint Unknown { get; set; }

        public void SetPlayerCounter(int Count)
        {
            PlayersCount = System.Convert.ToUInt16(Count);
        }
    }
}
