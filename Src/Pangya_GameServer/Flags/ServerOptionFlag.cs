namespace Pangya_GameServer.Flags
{
    public enum ServerOptionFlag : long
    {
        MAINTENANCE_FLAG_PAPELSHOP = 1 << 4,
        // VS
        MAINTENANCE_FLAG_VS_STROKE = 1 << 6,
        MAINTENANCE_FLAG_VS_MATCH = 1 << 7,
        // Tourney
        MAINTENANCE_FLAG_TOURNEY_TOURNEY = 1 << 8,
        MAINTENANCE_FLAG_TOURNEY_SHORTGAME = 1 << 9,
        MAINTENANCE_FLAG_TOURNEY_GUILD = 1 << 10,
        // Battle
        MAINTENANCE_FLAG_BATTLE_PANGBATTLE = 1 << 11,
        MAINTENANCE_FLAG_BATTLE_APPROACH = 1 << 12,
        // Lounge
        MAINTENANCE_FLAG_LOUNGE = 1 << 13,
        MAINTENANCE_FLAG_SCRATCHY = 1 << 14,
        MAINTENANCE_FLAG_MAILBOX = 1 << 18,
        MAINTENANCE_FLAG_MEMORIAL_SHOP = 1 << 28,
        MAINTENANCE_FLAG_CHAR_MASTERY = 1 << 30,
    }
}
