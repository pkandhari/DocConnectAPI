using DocConnectAPI.Helpers;
using DocConnectAPI.Models;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocConnectAPI.Controllers
{
    public class SignupController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Signup(SignupModel objSignup)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.AppendFormat("SELECT USER_ID FROM LOGIN_DETAILS WHERE USER_NAME = '{0}'", objSignup.UserName);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                throw new Exception("Username already exist, please sign in or choose another username.");
            }
            else
            {
                dataReader.Close();
                sbSQL.Clear();
                sbSQL.AppendFormat("INSERT INTO LOGIN_DETAILS(USER_NAME, PASSWORD) VALUES('{0}', '{1}')", objSignup.UserName, objSignup.UserPassword);
                command = new SqlCommand(sbSQL.ToString(), cnn);
                command.ExecuteNonQuery();

                dataReader.Close();
                sbSQL.Clear();
                sbSQL.AppendFormat("SELECT USER_ID FROM LOGIN_DETAILS WHERE USER_NAME = '{0}'", objSignup.UserName);
                command = new SqlCommand(sbSQL.ToString(), cnn);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    objSignup.UserId = dataReader.GetInt32(0);
                }

                dataReader.Close();
                sbSQL.Clear();
                sbSQL.AppendFormat("INSERT INTO USER_TABLE(USER_ID, FIRST_NAME, LAST_NAME, EMAIL, IS_DOCTOR) VALUES({0}, '{1}', '{2}', '{3}', '{4}')", objSignup.UserId, objSignup.FirstName, objSignup.LastName, objSignup.Email, objSignup.IsDoctor);
                command = new SqlCommand(sbSQL.ToString(), cnn);
                command.ExecuteNonQuery();

                dataReader.Close();
                sbSQL.Clear();
                if (objSignup.IsDoctor)
                    sbSQL.AppendFormat("INSERT INTO DOCTOR(USER_ID) VALUES({0})", objSignup.UserId);
                else
                    sbSQL.AppendFormat("INSERT INTO PATIENT(USER_ID) VALUES({0})", objSignup.UserId);
                command = new SqlCommand(sbSQL.ToString(), cnn);
                command.ExecuteNonQuery();
            }

            cnn.Close();

            return await Task.Run(() => this.Ok(objSignup));
        }
    }
}
