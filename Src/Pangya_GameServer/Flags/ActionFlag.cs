namespace Pangya_GameServer.Flags
{
    /// <summary>
    /// Used in Packet.ID = 0x
    /// </summary>
    public enum ActionFlag
    {
        /// <summary>
        /// Jogador faz uma rotação(USED IN GAMETYPE_VS)
        /// </summary>
        PLAYER_ACTION_ROTATION = 0x00,
        /// <summary>
        /// ação desconhecida
        /// </summary>
        PLAYER_ACTION_UNK = 0x01,
        /// <summary>
        /// Jogador aparece na sala
        /// </summary>
        PLAYER_ACTION_APPEAR = 0x04,
        PLAYER_ACTION_SUB = 0x05,
        /// <summary>
        /// Ação de mover
        /// </summary>
        PLAYER_ACTION_WALK = 0x06,
        /// <summary>
        /// Animação do jogador na sala
        /// </summary>
        PLAYER_ACTION_ANIMATION = 0x07,
        /// <summary>
        /// 
        /// </summary>
        PLAYER_ACTION_HEADER = 0x08,
        /// <summary>
        /// 
        /// </summary>
        PLAYER_ACTION_NULL = 0x9,
        /// <summary>
        /// Animação 2 do jogador na sala(CHAT_ROOM)
        /// </summary>
        PLAYER_ANIMATION_WITH_EFFECTS = 0x0A
    }
    public enum Action_Sub
    {
        PLAYER_ACTION_SUB_STAND = 0x00,
        PLAYER_ACTION_SUB_SIT = 0x01,
        PLAYER_ACTION_SUB_SLEEP = 0x02
    }
    public enum Action_Sub_Header
    {
        PLAYER_ACTION_HEADER_SCRATCHCARD = 0x0A
    }
}
