using System;
namespace Pangya_GameServer.Flags
{
    /// <summary>
    /// GameType ID's
    /// </summary>
    public enum GameTypeFlag : byte
    {
        VERSUS_STROKE = 0x00,
        VERSUS_MATCH = 0x01,
        CHAT_ROOM = 0x02,
        GAME_TYPE_03 = 0x03,
        TOURNEY = 0x04,
        TOURNEY_TEAM = 0x05,
        TOURNEY_GUILD = 0x06,
        PANG_BATTLE = 0x07,
        GAME_TYPE_08 = 0x08,
        GAME_TYPE_09 = 0x09,
        GAME_APROACH = 0x0A,
        GM_EVENT = 0x0B,
        GAME_TYPE_0C = 0x0C,
        GAME_ZOD_OFF = 0x0D,
        CHIP_IN_PRACTICE = 0x0E,
        GAME_TYPE_0F = 0x0F,
        GAME_TYPE_10 = 0x10,
        GAME_TYPE_11 = 0x11,
        SSC = 0x12,
        HOLE_REPEAT = 0x13,
        GRANDPRIX = 0x14
    }
}
