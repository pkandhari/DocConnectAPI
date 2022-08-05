using DocConnectAPI.Helpers;
using DocConnectAPI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DocConnectAPI.Controllers
{
    [Authorize]
    [EnableCors(origins: "http://3.84.17.160", headers: "*", methods: "*")]
    public class AdminsController : ApiController
    {
        [Route("admins/{adminId}", Name = "GetAdmin")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAdmin(int adminId)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            AdminModel objAdmin = null;

            sbSQL.AppendFormat("SELECT USER_ID, FIRST_NAME, LAST_NAME, CONTACT, EMAIL, MARITAL_STATUS, GENDER, DOB, ADDRESS, POSTAL_CODE, CITY, PROVINCE, COUNTRY, IS_DOCTOR, IS_ADMIN FROM USER_TABLE WHERE USER_ID = {0}", adminId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                objAdmin = new AdminModel();
                objAdmin.UserDetails = new UserModel();
                objAdmin.UserDetails.UserId = dataReader.GetInt32(0);
                objAdmin.UserDetails.FirstName = dataReader.IsDBNull(1) ? string.Empty : dataReader.GetString(1);
                objAdmin.UserDetails.LastName = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                objAdmin.UserDetails.Contact = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                objAdmin.UserDetails.Email = dataReader.IsDBNull(4) ? string.Empty : dataReader.GetString(4);
                objAdmin.UserDetails.MaritalStatus = dataReader.GetInt32(5);
                objAdmin.UserDetails.Gender = dataReader.GetInt32(6);
                objAdmin.UserDetails.DOB = dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7);
                objAdmin.UserDetails.Address = dataReader.IsDBNull(8) ? string.Empty : dataReader.GetString(8);
                objAdmin.UserDetails.PostalCode = dataReader.IsDBNull(9) ? string.Empty : dataReader.GetString(9);
                objAdmin.UserDetails.City = dataReader.IsDBNull(10) ? string.Empty : dataReader.GetString(10);
                objAdmin.UserDetails.Province = dataReader.IsDBNull(11) ? string.Empty : dataReader.GetString(11);
                objAdmin.UserDetails.Country = dataReader.IsDBNull(12) ? string.Empty : dataReader.GetString(12);
                objAdmin.UserDetails.IsDoctor = dataReader.GetBoolean(13);
                objAdmin.UserDetails.IsAdmin = dataReader.GetBoolean(14);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(objAdmin));
        }

        [Route("admins", Name = "UpdateAdmin")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateAdmin(AdminModel objAdmin)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.AppendFormat("UPDATE USER_TABLE SET FIRST_NAME = '{0}', LAST_NAME = '{1}', CONTACT = '{2}', EMAIL = '{3}', MARITAL_STATUS = '{4}', GENDER = '{5}', ", objAdmin.UserDetails.FirstName, objAdmin.UserDetails.LastName, objAdmin.UserDetails.Contact, objAdmin.UserDetails.Email, objAdmin.UserDetails.MaritalStatus, objAdmin.UserDetails.Gender);
            sbSQL.AppendFormat("DOB = '{0}', ADDRESS = '{1}', POSTAL_CODE = '{2}', CITY = '{3}', PROVINCE = '{4}', COUNTRY = '{5}' WHERE USER_ID = {6}", objAdmin.UserDetails.DOB, objAdmin.UserDetails.Address, objAdmin.UserDetails.PostalCode, objAdmin.UserDetails.City, objAdmin.UserDetails.Province, objAdmin.UserDetails.Country, objAdmin.UserDetails.UserId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            command.ExecuteNonQuery();

            cnn.Close();

            return await Task.Run(() => this.Ok(objAdmin));
        }
    }
}
