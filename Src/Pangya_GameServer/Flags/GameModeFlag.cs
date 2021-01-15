namespace Pangya_GameServer.Flags
{
    /// <summary>
    /// Game Mode ID's
    /// </summary>
   public enum GameModeFlag : byte
    {
        GAME_MODE_FRONT = 0x00,
        GAME_MODE_BACK = 0x01,
        GAME_MODE_RANDOM = 0x02,
        GAME_MODE_SHUFFLE = 0x03,
        GAME_MODE_REPEAT = 0x04,
        GAME_MODE_SSC = 0x05
    }
}
