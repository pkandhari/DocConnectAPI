﻿using DocConnectAPI.Helpers;
using DocConnectAPI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocConnectAPI.Controllers
{
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

            sbSQL.AppendFormat("SELECT UT.USER_ID USER_ID, PATIENT_ID, FIRST_NAME, LAST_NAME, CONTACT, EMAIL, MARITAL_STATUS, GENDER, DOB, ADDRESS, POSTAL_CODE, CITY, PROVINCE, COUNTRY, IS_DOCTOR, ALLERGIES, HEALTH_ISSUES ");
            sbSQL.AppendFormat("FROM PATIENT INNER JOIN USER_TABLE UT ON UT.USER_ID = PATIENT.USER_ID");
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objPatient = new PatientModel();
                objPatient.UserDetails = new UserModel();
                objPatient.UserId = dataReader.GetInt32(0);
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
                objPatient.Allergies = dataReader.IsDBNull(15) ? string.Empty : dataReader.GetString(15);
                objPatient.HealthIssues = dataReader.IsDBNull(16) ? string.Empty : dataReader.GetString(16);
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

            sbSQL.AppendFormat("SELECT UT.USER_ID USER_ID, PATIENT_ID, FIRST_NAME, LAST_NAME, CONTACT, EMAIL, MARITAL_STATUS, GENDER, DOB, ADDRESS, POSTAL_CODE, CITY, PROVINCE, COUNTRY, IS_DOCTOR, ALLERGIES, HEALTH_ISSUES ");
            sbSQL.AppendFormat("FROM PATIENT INNER JOIN USER_TABLE UT ON UT.USER_ID = PATIENT.USER_ID WHERE PATIENT_ID = {0}", patientId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                objPatient = new PatientModel();
                objPatient.UserDetails = new UserModel();
                objPatient.UserId = dataReader.GetInt32(0);
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
                objPatient.Allergies = dataReader.IsDBNull(15) ? string.Empty : dataReader.GetString(15);
                objPatient.HealthIssues = dataReader.IsDBNull(16) ? string.Empty : dataReader.GetString(16);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(objPatient));
        }
    }
}
