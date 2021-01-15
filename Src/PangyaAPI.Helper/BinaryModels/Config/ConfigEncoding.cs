using System.Configuration;
using System.Text;
namespace PangyaAPI.Helper.BinaryModels
{
    public static class ConfigEncoding
    {
        public static Encoding GetEncoding()
        {
            var EncondingName = ConfigurationManager.AppSettings["Encoding"];
            return Encoding.GetEncoding(EncondingName);
        }
    }
}
