using DocConnectAPI.Helpers;
using DocConnectAPI.Models;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DocConnectAPI.Controllers
{
    [EnableCors(origins: "http://localhost:3001", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        [Route("login", Name = "UserLogin")]
        [HttpPost]
        public async Task<IHttpActionResult> UserLogin(UserLoginModel objuserlogin)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("SELECT LD.USER_ID, FIRST_NAME + ' ' + LAST_NAME DISPLAY_NAME, EMAIL, IS_ADMIN, IS_DOCTOR FROM USER_TABLE UT ");
            sbSQL.AppendFormat("INNER JOIN LOGIN_DETAILS LD ON LD.USER_ID = UT.USER_ID WHERE USER_NAME = '{0}' AND PASSWORD = '{1}'", objuserlogin.UserName, objuserlogin.UserPassword);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                objuserlogin.UserId = dataReader.GetInt32(0);
                objuserlogin.DisplayName = dataReader.GetString(1).Trim();
                objuserlogin.Email = dataReader.GetString(2);
                objuserlogin.IsAdmin = dataReader.GetBoolean(3);
                objuserlogin.IsDoctor = dataReader.GetBoolean(4);
            }
            else
            {
                throw new Exception("Invalid username or password.");
            }

            cnn.Close();

            return await Task.Run(() => this.Ok(objuserlogin));
        }

        [Route("login", Name = "UpdateLoginDetails")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateLoginDetails(LoginDetailsModel objLoginDetails)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            bool isAuthenticated = false;

            sbSQL.AppendFormat("SELECT USER_ID FROM LOGIN_DETAILS WHERE USER_ID = {0} AND PASSWORD = '{1}'", objLoginDetails.UserId, objLoginDetails.OldPassword);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
                isAuthenticated = true;

            dataReader.Close();
            if (isAuthenticated)
            {
                sbSQL.AppendFormat("UPDATE LOGIN_DETAILS SET PASSWORD = '{0}' WHERE USER_ID = {1}", objLoginDetails.NewPassword, objLoginDetails.UserId);
                command = new SqlCommand(sbSQL.ToString(), cnn);
                command.ExecuteNonQuery();
                dataReader.Close();
            }
            else
            {
                throw new Exception("Old password is not correct.");
            }

            cnn.Close();

            return await Task.Run(() => this.Ok(objLoginDetails));
        }
    }
}