namespace Pangya_GameServer.Flags
{
    /// <summary>
    ///CODE MESSAGE BOX for GAME PLAY START    
    /// </summary>
    public enum GameStartFlag
    {
        /// <summary>
        /// chip in practice is needed
        /// </summary>
        ITEM_NO_EXIST = 972821518,
        /// <summary>
        ///  not enough players to start the game
        /// </summary>
        NOT_ENOUGH_PLAYERS = 1,
        /// <summary>
        /// there are not enough players to start the game
        /// </summary>
        ENOUGH_PLAYERS = 2,
        /// <summary>
        /// failed game already started
        /// </summary>
        START_GAME_FALIED = 07,
        /// <summary>
        /// you need to update pangya to the latest version
        /// </summary>
        UPDATE_GAME = 08
    }
}
