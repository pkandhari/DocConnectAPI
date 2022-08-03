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
    //[Authorize]
    [EnableCors(origins: "http://localhost:3001", headers: "*", methods: "*")]
    public class ComboBoxController : ApiController
    {
        [Route("genders", Name = "GetGenders")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGenders()
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            List<ComboBox> lstGenders = new List<ComboBox>();
            ComboBox objGender = null;

            sbSQL.AppendFormat("SELECT GENDER_ID, DESCRIPTION FROM GENDER");
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objGender = new ComboBox();
                objGender.Id = dataReader.GetInt32(0);
                objGender.Description = dataReader.GetString(1);
                lstGenders.Add(objGender);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(lstGenders));
        }

        [Route("maritalstatus", Name = "GetMaritalStatus")]
        [HttpGet]
        public async Task<IHttpActionResult> GetMaritalStatus()
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            List<ComboBox> lstMaritalStatus = new List<ComboBox>();
            ComboBox objMaritalStatus = null;

            sbSQL.AppendFormat("SELECT STATUS_ID, DESCRIPTION FROM MARITAL_STATUS");
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objMaritalStatus = new ComboBox();
                objMaritalStatus.Id = dataReader.GetInt32(0);
                objMaritalStatus.Description = dataReader.GetString(1);
                lstMaritalStatus.Add(objMaritalStatus);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(lstMaritalStatus));
        }
    }
}
