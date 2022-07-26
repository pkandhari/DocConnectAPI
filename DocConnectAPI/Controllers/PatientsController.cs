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
    [EnableCors(origins: "http://localhost:3001", headers: "*", methods: "*")]
    public class PatientsController : ApiController
    {
        [Route("patients", Name = "GetPatients")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPatients()
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            List<PatientModel> lstPatients = new List<PatientModel>();
            PatientModel objPatient = null;

            sbSQL.AppendFormat("SELECT UT.USER_ID USER_ID, PATIENT_ID, FIRST_NAME, LAST_NAME, CONTACT, EMAIL, MARITAL_STATUS, GENDER, DOB, ADDRESS, POSTAL_CODE, CITY, PROVINCE, COUNTRY, IS_DOCTOR, IS_ADMIN, ALLERGIES, HEALTH_ISSUES ");
            sbSQL.AppendFormat("FROM PATIENT INNER JOIN USER_TABLE UT ON UT.USER_ID = PATIENT.USER_ID");
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objPatient = new PatientModel();
                objPatient.UserDetails = new UserModel();
                objPatient.UserDetails.UserId = dataReader.GetInt32(0);
                objPatient.PatientId = dataReader.GetInt32(1);
                objPatient.UserDetails.FirstName = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                objPatient.UserDetails.LastName = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                objPatient.UserDetails.Contact = dataReader.GetInt32(4);
                objPatient.UserDetails.Email = dataReader.IsDBNull(5) ? string.Empty : dataReader.GetString(5);
                objPatient.UserDetails.MaritalStatus = dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6);
                objPatient.UserDetails.Gender = dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7);
                objPatient.UserDetails.DOB = dataReader.IsDBNull(8) ? string.Empty : dataReader.GetString(8);
                objPatient.UserDetails.Address = dataReader.IsDBNull(9) ? string.Empty : dataReader.GetString(9);
                objPatient.UserDetails.PostalCode = dataReader.IsDBNull(10) ? string.Empty : dataReader.GetString(10);
                objPatient.UserDetails.City = dataReader.IsDBNull(11) ? string.Empty : dataReader.GetString(11);
                objPatient.UserDetails.Province = dataReader.IsDBNull(12) ? string.Empty : dataReader.GetString(12);
                objPatient.UserDetails.Country = dataReader.IsDBNull(13) ? string.Empty : dataReader.GetString(13);
                objPatient.UserDetails.IsDoctor = dataReader.GetBoolean(14);
                objPatient.UserDetails.IsAdmin = dataReader.GetBoolean(15);
                objPatient.Allergies = dataReader.IsDBNull(16) ? string.Empty : dataReader.GetString(16);
                objPatient.HealthIssues = dataReader.IsDBNull(17) ? string.Empty : dataReader.GetString(17);
                lstPatients.Add(objPatient);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(lstPatients));
        }

        [Route("patients/{patientId}", Name = "GetPatient")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPatient(int patientId)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            PatientModel objPatient = null;

            sbSQL.AppendFormat("SELECT UT.USER_ID USER_ID, PATIENT_ID, FIRST_NAME, LAST_NAME, CONTACT, EMAIL, MARITAL_STATUS, GENDER, DOB, ADDRESS, POSTAL_CODE, CITY, PROVINCE, COUNTRY, IS_DOCTOR, IS_ADMIN, ALLERGIES, HEALTH_ISSUES ");
            sbSQL.AppendFormat("FROM PATIENT INNER JOIN USER_TABLE UT ON UT.USER_ID = PATIENT.USER_ID WHERE PATIENT.USER_ID = {0}", patientId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                objPatient = new PatientModel();
                objPatient.UserDetails = new UserModel();
                objPatient.UserDetails.UserId = dataReader.GetInt32(0);
                objPatient.PatientId = dataReader.GetInt32(1);
                objPatient.UserDetails.FirstName = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                objPatient.UserDetails.LastName = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                objPatient.UserDetails.Contact = dataReader.GetInt32(4);
                objPatient.UserDetails.Email = dataReader.IsDBNull(5) ? string.Empty : dataReader.GetString(5);
                objPatient.UserDetails.MaritalStatus = dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6);
                objPatient.UserDetails.Gender = dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7);
                objPatient.UserDetails.DOB = dataReader.IsDBNull(8) ? string.Empty : dataReader.GetString(8);
                objPatient.UserDetails.Address = dataReader.IsDBNull(9) ? string.Empty : dataReader.GetString(9);
                objPatient.UserDetails.PostalCode = dataReader.IsDBNull(10) ? string.Empty : dataReader.GetString(10);
                objPatient.UserDetails.City = dataReader.IsDBNull(11) ? string.Empty : dataReader.GetString(11);
                objPatient.UserDetails.Province = dataReader.IsDBNull(12) ? string.Empty : dataReader.GetString(12);
                objPatient.UserDetails.Country = dataReader.IsDBNull(13) ? string.Empty : dataReader.GetString(13);
                objPatient.UserDetails.IsDoctor = dataReader.GetBoolean(14);
                objPatient.UserDetails.IsAdmin = dataReader.GetBoolean(15);
                objPatient.Allergies = dataReader.IsDBNull(16) ? string.Empty : dataReader.GetString(16);
                objPatient.HealthIssues = dataReader.IsDBNull(17) ? string.Empty : dataReader.GetString(17);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(objPatient));
        }

        [Route("patients", Name = "UpdatePatient")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePatient(PatientModel objPatient)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.AppendFormat("UPDATE USER_TABLE SET FIRST_NAME = '{0}', LAST_NAME = '{1}', CONTACT = {2}, EMAIL = '{3}', MARITAL_STATUS = '{4}', GENDER = '{5}', ", objPatient.UserDetails.FirstName, objPatient.UserDetails.LastName, objPatient.UserDetails.Contact, objPatient.UserDetails.Email, objPatient.UserDetails.MaritalStatus, objPatient.UserDetails.Gender);
            sbSQL.AppendFormat("DOB = '{0}', ADDRESS = '{1}', POSTAL_CODE = '{2}', CITY = '{3}', PROVINCE = '{4}', COUNTRY = '{5}' WHERE USER_ID = {6}", objPatient.UserDetails.DOB, objPatient.UserDetails.Address, objPatient.UserDetails.PostalCode, objPatient.UserDetails.City, objPatient.UserDetails.Province, objPatient.UserDetails.Country, objPatient.UserDetails.UserId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            command.ExecuteNonQuery();

            sbSQL.AppendFormat("UPDATE PATIENT SET HEALTH_ISSUES = '{0}', ALLERGIES = '{1}' WHERE PATIENT_ID = {2}", objPatient.HealthIssues, objPatient.Allergies, objPatient.PatientId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            command.ExecuteNonQuery();

            cnn.Close();

            return await Task.Run(() => this.Ok(objPatient));
        }
    }
}
