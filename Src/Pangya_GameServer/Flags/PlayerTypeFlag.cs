using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangya_GameServer.Flags
{
   public enum PlayerTypeFlag : byte
    {
        /// <summary>
        /// Player who entered the room
        /// </summary>
        Normal = 0x01,

        Default = 0,
        /// <summary>
        /// Player who created the room
        /// </summary>
        Master = 0x08,
    }
}
