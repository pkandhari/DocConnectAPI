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
    [EnableCors(origins: "http://localhost:3001", headers: "*", methods: "*")]
    public class AppointmentsController : ApiController
    {
        [Route("patients/{patientId}/appointments", Name = "GetPatientAppointments")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPatientAppointments(int patientId)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            List<AppointmentModel> lstAppointments = new List<AppointmentModel>();
            AppointmentModel objAppointment = null;

            sbSQL.AppendFormat("SELECT APPOINTMENT_ID, PATIENT_ID, DOCTOR_ID, DURATION, TITLE, APPOINTMENT_DATE, DOCTOR_NOTES, REMARKS FROM APPOINTMENTS WHERE PATIENT_ID = {0}", patientId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objAppointment = new AppointmentModel();
                objAppointment.AppointmentId = dataReader.GetInt32(0);
                objAppointment.PatientId = dataReader.GetInt32(1);
                objAppointment.DoctorId = dataReader.GetInt32(2);
                objAppointment.Duration = dataReader.GetString(3);
                objAppointment.Title = dataReader.GetString(4);
                objAppointment.DateAndTime = dataReader.GetString(5);
                objAppointment.DoctorNotes = dataReader.GetString(6);
                objAppointment.Remarks = dataReader.GetString(7);
                lstAppointments.Add(objAppointment);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(lstAppointments));
        }

        [Route("doctors/{doctorId}/appointments", Name = "GetDoctorAppointments")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDoctorAppointments(int doctorId)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            List<AppointmentModel> lstAppointments = new List<AppointmentModel>();
            AppointmentModel objAppointment = null;

            sbSQL.AppendFormat("SELECT APPOINTMENT_ID, PATIENT_ID, DOCTOR_ID, DURATION, TITLE, APPOINTMENT_DATE, DOCTOR_NOTES, REMARKS FROM APPOINTMENTS WHERE DOCTOR_ID = {0}", doctorId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objAppointment = new AppointmentModel();
                objAppointment.AppointmentId = dataReader.GetInt32(0);
                objAppointment.PatientId = dataReader.GetInt32(1);
                objAppointment.DoctorId = dataReader.GetInt32(2);
                objAppointment.Duration = dataReader.GetString(3);
                objAppointment.Title = dataReader.GetString(4);
                objAppointment.DateAndTime = dataReader.GetString(5);
                objAppointment.DoctorNotes = dataReader.GetString(6);
                objAppointment.Remarks = dataReader.GetString(7);
                lstAppointments.Add(objAppointment);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(lstAppointments));
        }

        [Route("appointments/{appointmentId}", Name = "GetAppointment")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAppointment(int appointmentId)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            AppointmentModel objAppointment = new AppointmentModel();

            sbSQL.AppendFormat("SELECT APPOINTMENT_ID, PATIENT_ID, DOCTOR_ID, DURATION, TITLE, APPOINTMENT_DATE, DOCTOR_NOTES, REMARKS FROM APPOINTMENTS WHERE APPOINTMENT_ID = {0}", appointmentId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                objAppointment.AppointmentId = dataReader.GetInt32(0);
                objAppointment.PatientId = dataReader.GetInt32(1);
                objAppointment.DoctorId = dataReader.GetInt32(2);
                objAppointment.Duration = dataReader.GetString(3);
                objAppointment.Title = dataReader.GetString(4);
                objAppointment.DateAndTime = dataReader.GetString(5);
                objAppointment.DoctorNotes = dataReader.GetString(6);
                objAppointment.Remarks = dataReader.GetString(7);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(objAppointment));
        }

        [Route("appointments", Name = "SaveAppointment")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveAppointment(AppointmentModel objAppointment)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Clear();
            sbSQL.AppendFormat("INSERT INTO APPOINTMENTS(PATIENT_ID, DOCTOR_ID, DURATION, TITLE, APPOINTMENT_DATE, DOCTOR_NOTES, REMARKS) ");
            sbSQL.AppendFormat("VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6})", objAppointment.PatientId, objAppointment.DoctorId, objAppointment.Duration, objAppointment.Title, objAppointment.DateAndTime, objAppointment.DoctorNotes, objAppointment.Remarks);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            command.ExecuteNonQuery();

            cnn.Close();

            return await Task.Run(() => this.Ok());
        }

        [Route("appointments", Name = "UpdateAppointment")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateAppointment(AppointmentModel objAppointment)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Clear();
            sbSQL.AppendFormat("UPDATE APPOINTMENTS SET DOCTOR_ID = {0}, DURATION = {1}, TITLE = '{2}', APPOINTMENT_DATE = '{3}', DOCTOR_NOTES = '{4}', REMARKS = '{5}' WHERE APPOINTMENT_ID = {6}", objAppointment.DoctorId, objAppointment.Duration, objAppointment.Title, objAppointment.DateAndTime, objAppointment.DoctorNotes, objAppointment.Remarks, objAppointment.AppointmentId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            command.ExecuteNonQuery();

            cnn.Close();

            return await Task.Run(() => this.Ok());
        }

        [Route("appointments/{appointmentId}", Name = "DeleteAppointment")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAppointment(int appointmentId)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.AppendFormat("DELETE FROM APPOINTMENTS WHERE APPOINTMENT_ID = {0}", appointmentId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            command.ExecuteNonQuery();

            cnn.Close();

            return await Task.Run(() => this.Ok());
        }
    }
}
