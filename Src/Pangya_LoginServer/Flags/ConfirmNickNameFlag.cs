namespace Pangya_LoginServer.Flags
{
    /// <summary>
    /// { 0x0E, 0x00, enumValue, 0x00, 0x00, 0x00 }
    /// </summary>
    public enum ConfirmNickNameFlag
    {
        Disponivel = 0x00, //Nickname disponível
        OcorreuUmErro = 0x01, //Ocorreu um erro ao verificar
        Indisponivel = 0x03,
        FormatoOuTamanhoInvalido = 0x04,
        PointsInsuficientes = 0x05,
        PalavasInapropriadas = 0x06,
        DBError = 0x07,
        MesmoNickNameSeraUsado = 0x09
    }
}
