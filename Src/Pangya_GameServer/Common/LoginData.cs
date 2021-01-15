using System.Runtime.InteropServices;
namespace Pangya_GameServer.Common
{
    public class LoginData
    {
        public string UserName { get; set; }
        public uint UID { get; set; }
        public uint Blank1 { get; set; }
        public ushort CheckUnknow1 { get; set; }
        public string AuthKeyLogin { get; set; }
        public string ClientVersion { get; set; }
        public uint ClientBuildDate { get; set; }
        public uint Blank2 { get; set; }
        public string AuthKeyGame { get; set; }
    }
}
