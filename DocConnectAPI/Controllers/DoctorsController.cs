using DocConnectAPI.Helpers;
using DocConnectAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocConnectAPI.Controllers
{
    public class DoctorsController : ApiController
    {
        [Route("doctors", Name = "GetDoctors")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDoctors()
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            List<DoctorModel> lstDoctors = new List<DoctorModel>();
            DoctorModel objDoctors = null;

            sbSQL.AppendFormat("SELECT UT.USER_ID USER_ID, DOCTOR_ID, FIRST_NAME, LAST_NAME, CONTACT, EMAIL, MARITAL_STATUS, GENDER, DOB, ADDRESS, POSTAL_CODE, CITY, PROVINCE, COUNTRY, ");
            sbSQL.AppendFormat("IS_DOCTOR, DEGREE, GRADUATED_FROM, YEARS_OF_EXP, FIELD_OF_PRACTICE, IS_FULL_TIME, CURRENT_WORKING_STATUS, DEPARTMENT, DOJ, AVAILABILITY, IS_ACTIVE ");
            sbSQL.AppendFormat("FROM DOCTOR INNER JOIN USER_TABLE UT ON UT.USER_ID = DOCTOR.USER_ID");
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objDoctors = new DoctorModel();
                objDoctors.UserDetails = new UserModel();
                objDoctors.UserId = dataReader.GetInt32(0);
                objDoctors.DoctorId = dataReader.GetInt32(1);
                objDoctors.UserDetails.FirstName = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                objDoctors.UserDetails.LastName = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                objDoctors.UserDetails.Contact = dataReader.GetInt32(4);
                objDoctors.UserDetails.Email = dataReader.IsDBNull(5) ? string.Empty : dataReader.GetString(5);
                objDoctors.UserDetails.MaritalStatus = dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6);
                objDoctors.UserDetails.Gender = dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7);
                objDoctors.UserDetails.DOB = dataReader.IsDBNull(8) ? string.Empty : dataReader.GetString(8);
                objDoctors.UserDetails.Address = dataReader.IsDBNull(9) ? string.Empty : dataReader.GetString(9);
                objDoctors.UserDetails.PostalCode = dataReader.IsDBNull(10) ? string.Empty : dataReader.GetString(10);
                objDoctors.UserDetails.City = dataReader.IsDBNull(11) ? string.Empty : dataReader.GetString(11);
                objDoctors.UserDetails.Province = dataReader.IsDBNull(12) ? string.Empty : dataReader.GetString(12);
                objDoctors.UserDetails.Country = dataReader.IsDBNull(13) ? string.Empty : dataReader.GetString(13);
                objDoctors.UserDetails.IsDoctor = dataReader.GetBoolean(14);
                objDoctors.Degree = dataReader.IsDBNull(15) ? string.Empty : dataReader.GetString(15);
                objDoctors.GraduatedFrom = dataReader.IsDBNull(16) ? string.Empty : dataReader.GetString(16);
                objDoctors.YearsOfExp = dataReader.GetInt32(17);
                objDoctors.FieldOfPractice = dataReader.IsDBNull(18) ? string.Empty : dataReader.GetString(18);
                objDoctors.IsFullTime = dataReader.GetBoolean(19);
                objDoctors.CurrentWorkingStatus = dataReader.IsDBNull(20) ? string.Empty : dataReader.GetString(20);
                objDoctors.Department = dataReader.IsDBNull(21) ? string.Empty : dataReader.GetString(21);
                objDoctors.DOJ = dataReader.IsDBNull(22) ? string.Empty : dataReader.GetString(22);
                objDoctors.Availability = dataReader.IsDBNull(23) ? string.Empty : dataReader.GetString(23);
                objDoctors.IsActive = dataReader.GetBoolean(24);
                lstDoctors.Add(objDoctors);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(lstDoctors));
        }

        [Route("doctors/{doctorId}", Name = "GetDoctor")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDoctor(int doctorId)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            DoctorModel objDoctors = null;

            sbSQL.AppendFormat("SELECT UT.USER_ID USER_ID, DOCTOR_ID, FIRST_NAME, LAST_NAME, CONTACT, EMAIL, MARITAL_STATUS, GENDER, DOB, ADDRESS, POSTAL_CODE, CITY, PROVINCE, COUNTRY, ");
            sbSQL.AppendFormat("IS_DOCTOR, DEGREE, GRADUATED_FROM, YEARS_OF_EXP, FIELD_OF_PRACTICE, IS_FULL_TIME, CURRENT_WORKING_STATUS, DEPARTMENT, DOJ, AVAILABILITY, IS_ACTIVE ");
            sbSQL.AppendFormat("FROM DOCTOR INNER JOIN USER_TABLE UT ON UT.USER_ID = DOCTOR.USER_ID WHERE DOCTOR_ID = {0}", doctorId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                objDoctors = new DoctorModel();
                objDoctors.UserDetails = new UserModel();
                objDoctors.UserId = dataReader.GetInt32(0);
                objDoctors.DoctorId = dataReader.GetInt32(1);
                objDoctors.UserDetails.FirstName = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);
                objDoctors.UserDetails.LastName = dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3);
                objDoctors.UserDetails.Contact = dataReader.GetInt32(4);
                objDoctors.UserDetails.Email = dataReader.IsDBNull(5) ? string.Empty : dataReader.GetString(5);
                objDoctors.UserDetails.MaritalStatus = dataReader.IsDBNull(6) ? string.Empty : dataReader.GetString(6);
                objDoctors.UserDetails.Gender = dataReader.IsDBNull(7) ? string.Empty : dataReader.GetString(7);
                objDoctors.UserDetails.DOB = dataReader.IsDBNull(8) ? string.Empty : dataReader.GetString(8);
                objDoctors.UserDetails.Address = dataReader.IsDBNull(9) ? string.Empty : dataReader.GetString(9);
                objDoctors.UserDetails.PostalCode = dataReader.IsDBNull(10) ? string.Empty : dataReader.GetString(10);
                objDoctors.UserDetails.City = dataReader.IsDBNull(11) ? string.Empty : dataReader.GetString(11);
                objDoctors.UserDetails.Province = dataReader.IsDBNull(12) ? string.Empty : dataReader.GetString(12);
                objDoctors.UserDetails.Country = dataReader.IsDBNull(13) ? string.Empty : dataReader.GetString(13);
                objDoctors.UserDetails.IsDoctor = dataReader.GetBoolean(14);
                objDoctors.Degree = dataReader.IsDBNull(15) ? string.Empty : dataReader.GetString(15);
                objDoctors.GraduatedFrom = dataReader.IsDBNull(16) ? string.Empty : dataReader.GetString(16);
                objDoctors.YearsOfExp = dataReader.GetInt32(17);
                objDoctors.FieldOfPractice = dataReader.IsDBNull(18) ? string.Empty : dataReader.GetString(18);
                objDoctors.IsFullTime = dataReader.GetBoolean(19);
                objDoctors.CurrentWorkingStatus = dataReader.IsDBNull(20) ? string.Empty : dataReader.GetString(20);
                objDoctors.Department = dataReader.IsDBNull(21) ? string.Empty : dataReader.GetString(21);
                objDoctors.DOJ = dataReader.IsDBNull(22) ? string.Empty : dataReader.GetString(22);
                objDoctors.Availability = dataReader.IsDBNull(23) ? string.Empty : dataReader.GetString(23);
                objDoctors.IsActive = dataReader.GetBoolean(24);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(objDoctors));
        }

        [Route("doctors", Name = "UpdateDoctor")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateDoctor(DoctorModel objDoctor)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.AppendFormat("UPDATE USER_TABLE SET FIRST_NAME = '{0}', LAST_NAME = '{1}', CONTACT = {2}, EMAIL = '{3}', MARITAL_STATUS = '{4}', GENDER = '{5}', ", objDoctor.UserDetails.FirstName, objDoctor.UserDetails.LastName, objDoctor.UserDetails.Contact, objDoctor.UserDetails.Email, objDoctor.UserDetails.MaritalStatus, objDoctor.UserDetails.Gender);
            sbSQL.AppendFormat("DOB = '{0}', ADDRESS = '{1}', POSTAL_CODE = '{2}', CITY = '{3}', PROVINCE = '{4}', COUNTRY = '{5}' WHERE USER_ID = {6}", objDoctor.UserDetails.DOB, objDoctor.UserDetails.Address, objDoctor.UserDetails.PostalCode, objDoctor.UserDetails.City, objDoctor.UserDetails.Province, objDoctor.UserDetails.Country, objDoctor.UserId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            command.ExecuteNonQuery();

            sbSQL.AppendFormat("UPDATE DOCTOR SET DEGREE = '{0}', GRADUATED_FROM = '{1}', YEARS_OF_EXP = {2}, FIELD_OF_PRACTICE = '{3}', ", objDoctor.Degree, objDoctor.GraduatedFrom, objDoctor.YearsOfExp, objDoctor.FieldOfPractice);
            sbSQL.AppendFormat("IS_FULL_TIME = '{0}', CURRENT_WORKING_STATUS = '{1}', DEPARTMENT = '{2}', DOJ = '{3}', AVAILABILITY = '{4}' WHERE DOCTOR_ID = {5}", Convert.ToInt16(objDoctor.IsFullTime), objDoctor.CurrentWorkingStatus, objDoctor.Department, objDoctor.DOJ, objDoctor.Availability, objDoctor.DoctorId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            command.ExecuteNonQuery();

            cnn.Close();

            return await Task.Run(() => this.Ok(objDoctor));
        }
    }
}
