namespace Pangya_LoginServer.Flags
{
    public enum LoginPacketFlag
    {
        /// <summary>
        /// Player digita o usuário e senha e clica em login
        /// </summary>
        PLAYER_LOGIN = 0x01,

        /// <summary>
        /// Player Seleciona um Servidor para entrar
        /// </summary>
        PLAYER_SELECT_SERVER = 0x03,

        /// <summary>
        /// login com duplicidade 
        /// </summary>
        PLAYER_DUPLICATE_LOGIN = 0x04,

        /// <summary>
        /// Seta primeiro nickname do usuário
        /// </summary>
        PLAYER_SET_NICKNAME = 0x06,

        /// <summary>
        /// Ocorre quando o cliente clica em Confirmar (se o nickname está disponível), 
        /// </summary>
        PLAYER_CONFIRM_NICKNAME = 0x07,

        /// <summary>
        /// Player selecionou seu primeiro personagem
        /// </summary>
        PLAYER_SELECT_CHARACTER = 0x08,

        /// <summary>
        /// envia chave de autenficação do login e lista novamente os servers
        /// </summary>
        PLAYER_RECONNECT = 0x0B,

        /// <summary>
        /// ?????????
        /// </summary>
        NOTHING = 0xFF
    }
}
