using PangyaAPI.Auth.Flags;
using System;
namespace PangyaAPI.Auth.AuthPacket
{
    [Serializable]
    public class AuthPacketInfo
    {
        public AuthPacketEnum ID { get; set; }

        public dynamic Message { get; set; }

        public AuthClientTypeEnum ServerType { get; set; }
    }
}
