using DocConnectAPI.Helpers;
using DocConnectAPI.Models;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocConnectAPI.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Login(UserLoginModel objuserlogin)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("SELECT LD.USER_ID, FIRST_NAME + ' ' + LAST_NAME DISPLAY_NAME FROM USER_TABLE UT ");
            sbSQL.AppendFormat("INNER JOIN LOGIN_DETAILS LD ON LD.USER_ID = UT.USER_ID WHERE USER_NAME = '{0}' AND PASSWORD = '{1}'", objuserlogin.UserName, objuserlogin.UserPassword);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                objuserlogin.UserId = dataReader.GetInt32(0);
                objuserlogin.DisplayName = dataReader.GetString(1).Trim();
            }

            cnn.Close();

            return await Task.Run(() => this.Ok(objuserlogin));
        }
    }
}