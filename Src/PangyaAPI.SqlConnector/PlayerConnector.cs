using PangyaAPI.SqlConnector.DataBase;
using PangyaAPI.SqlConnector.DataBase.Procedures;
using System;
using System.Linq;
using System.Windows.Forms;
namespace PangyaAPI.SqlConnector
{
    public class PlayerConnector
    {
        static readonly PangyaEntities DB;
        static PlayerConnector()
        {
            try
            {
                DB = new PangyaEntities();
            }
            catch
            {
                MessageBox.Show("failed To Connect DB", "Pang.SqlConnector");
            }
        }

        public static USP_LOGIN_SERVER_US_Result USP_LOGIN_SERVER_US(string user, string pwd, string iPAddress, string auth1, string auth2)
        {
            try
            {
                var result = DB.USP_LOGIN_SERVER_US(user, pwd, iPAddress, auth1, auth2).FirstOrDefault();

                if (result == null)
                {
                    throw new Exception("failed to log in");
                }
                else
                {
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public static USP_GAME_LOGIN_Result GameResult(string user, int? ID, string code1, string code2)
        {
            return DB.USP_GAME_LOGIN(user, ID, code1, code2).FirstOrDefault();
        }
    }
}
