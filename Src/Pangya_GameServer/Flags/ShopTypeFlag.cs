using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_GameServer.Flags
{
    public enum ShopTypeFlag : byte
    {
        Unknown = 0x1,
        Normal = 0x2,
        OB = 0x3,
        Success = 0x4
    }
}
