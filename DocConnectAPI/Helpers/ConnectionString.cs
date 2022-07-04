using System.Configuration;

namespace DocConnectAPI.Helpers
{
    public class ConnectionString
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["docconnect"].ConnectionString;
        }
    }
}
