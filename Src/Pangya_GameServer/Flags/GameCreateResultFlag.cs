﻿namespace Pangya_GameServer.Flags
{
    /// <summary>
    /// Result Game Create
    /// </summary>
    public enum GameCreateResultFlag : byte
    {
        CREATE_GAME_RESULT_SUCCESS = 0x00,
        CREATE_GAME_RESULT_FULL = 0x02,
        CREATE_GAME_ROOM_DONT_EXISTS = 0x03,
        CREATE_GAME_INCORRECT_PASSWORD = 0x04,
        CREATE_GAME_INVALID_LEVEL = 0x05,
        CREATE_GAME_CREATE_FAILED = 0x07,
        CREATE_GAME_ALREADY_STARTED = 0x08,
        CREATE_GAME_CREATE_FAILED2 = 0x09,
        CREATE_GAME_NEED_REGISTER_WITH_GUILD = 0x0D,
        CREATE_GAME_PANG_BATTLE_INSSUFICENT_PANGS = 0x0F,
        CREATE_GAME_APPROACH_INSSUFICENT_PANGS = 0x11,
        CREATE_GAME_CANT_CREATE = 0x12
    }
}
