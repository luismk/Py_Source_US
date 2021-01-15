namespace Pangya_LoginServer.Flags
{
    /// <summary>
    /// 0x01, 0x00, EnumValue, 0x00, 0x00, 0x00, 0x00 
    /// </summary>
    public enum LoginCodeFlag
    {

        Sucess = 0x00,
        InvalidoIdPw = 0x01,
        InvalidoId = 0x02,
        UsuarioEmUso = 0x04,
        Banido = 0x05,
        InvalidoUsernameOuSenha = 0x06,
        ContaSuspensa = 0x07,
        Player13AnosOuMenos = 0x09,
        SSNIncorreto = 0x0C,
        UsuarioIncorreto = 0x0D,
        OnlyUserAllowed = 0x0E,
        ServerInMaintenance = 0x0F, //By LuisMk
        NaoDisponivelNaSuaArea = 0x10, //By LuisMk
        CreateNickName_US = 0xD8, //by LuisMK (usado no US)
        CreateNickName = 0xD9, //by LuisMK (usado no TH)
    }
}
