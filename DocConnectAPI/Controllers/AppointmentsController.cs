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

            sbSQL.AppendFormat("SELECT APPOINTMENT_ID, A.PATIENT_ID, A.DOCTOR_ID, DURATION, TITLE, APPOINTMENT_DATE, DOCTOR_NOTES, REMARKS, ");
            sbSQL.AppendFormat("UD.FIRST_NAME + ' ' + UD.LAST_NAME, UP.FIRST_NAME + ' ' + UP.LAST_NAME, UD.USER_ID, UP.USER_ID FROM APPOINTMENTS A ");
            sbSQL.AppendFormat("INNER JOIN PATIENT P ON P.PATIENT_ID = A.PATIENT_ID INNER JOIN DOCTOR D ON D.DOCTOR_ID = A.DOCTOR_ID ");
            sbSQL.AppendFormat("INNER JOIN USER_TABLE UD ON UD.USER_ID = D.USER_ID INNER JOIN USER_TABLE UP ON UP.USER_ID = P.USER_ID WHERE P.USER_ID = {0}", patientId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objAppointment = new AppointmentModel();
                objAppointment.AppointmentId = dataReader.GetInt32(0);
                objAppointment.PatientId = dataReader.GetInt32(1);
                objAppointment.DoctorId = dataReader.GetInt32(2);
                objAppointment.Duration = dataReader.GetInt32(3);
                objAppointment.Title = dataReader.GetString(4);
                objAppointment.DateAndTime = dataReader.GetString(5);
                objAppointment.DoctorNotes = dataReader.GetString(6);
                objAppointment.Remarks = dataReader.GetString(7);
                objAppointment.DoctorName = dataReader.GetString(8);
                objAppointment.PatientName = dataReader.GetString(9);
                objAppointment.DoctorUserId = dataReader.GetInt32(10);
                objAppointment.PatientUserId = dataReader.GetInt32(11);
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

            sbSQL.AppendFormat("SELECT APPOINTMENT_ID, A.PATIENT_ID, A.DOCTOR_ID, DURATION, TITLE, APPOINTMENT_DATE, DOCTOR_NOTES, REMARKS, ");
            sbSQL.AppendFormat("UD.FIRST_NAME + ' ' + UD.LAST_NAME, UP.FIRST_NAME + ' ' + UP.LAST_NAME, UD.USER_ID, UP.USER_ID FROM APPOINTMENTS A ");
            sbSQL.AppendFormat("INNER JOIN PATIENT P ON P.PATIENT_ID = A.PATIENT_ID INNER JOIN DOCTOR D ON D.DOCTOR_ID = A.DOCTOR_ID ");
            sbSQL.AppendFormat("INNER JOIN USER_TABLE UD ON UD.USER_ID = D.USER_ID INNER JOIN USER_TABLE UP ON UP.USER_ID = P.USER_ID WHERE D.USER_ID = {0}", doctorId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objAppointment = new AppointmentModel();
                objAppointment.AppointmentId = dataReader.GetInt32(0);
                objAppointment.PatientId = dataReader.GetInt32(1);
                objAppointment.DoctorId = dataReader.GetInt32(2);
                objAppointment.Duration = dataReader.GetInt32(3);
                objAppointment.Title = dataReader.GetString(4);
                objAppointment.DateAndTime = dataReader.GetString(5);
                objAppointment.DoctorNotes = dataReader.GetString(6);
                objAppointment.Remarks = dataReader.GetString(7);
                objAppointment.DoctorName = dataReader.GetString(8);
                objAppointment.PatientName = dataReader.GetString(9);
                objAppointment.DoctorUserId = dataReader.GetInt32(10);
                objAppointment.PatientUserId = dataReader.GetInt32(11);
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

            sbSQL.AppendFormat("SELECT APPOINTMENT_ID, A.PATIENT_ID, A.DOCTOR_ID, DURATION, TITLE, APPOINTMENT_DATE, DOCTOR_NOTES, REMARKS, ");
            sbSQL.AppendFormat("UD.FIRST_NAME + ' ' + UD.LAST_NAME, UP.FIRST_NAME + ' ' + UP.LAST_NAME, UD.USER_ID, UP.USER_ID FROM APPOINTMENTS A ");
            sbSQL.AppendFormat("INNER JOIN PATIENT P ON P.PATIENT_ID = A.PATIENT_ID INNER JOIN DOCTOR D ON D.DOCTOR_ID = A.DOCTOR_ID ");
            sbSQL.AppendFormat("INNER JOIN USER_TABLE UD ON UD.USER_ID = D.USER_ID INNER JOIN USER_TABLE UP ON UP.USER_ID = P.USER_ID WHERE A.APPOINTMENT_ID = {0}", appointmentId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                objAppointment.AppointmentId = dataReader.GetInt32(0);
                objAppointment.PatientId = dataReader.GetInt32(1);
                objAppointment.DoctorId = dataReader.GetInt32(2);
                objAppointment.Duration = dataReader.GetInt32(3);
                objAppointment.Title = dataReader.GetString(4);
                objAppointment.DateAndTime = dataReader.GetString(5);
                objAppointment.DoctorNotes = dataReader.GetString(6);
                objAppointment.Remarks = dataReader.GetString(7);
                objAppointment.DoctorName = dataReader.GetString(8);
                objAppointment.PatientName = dataReader.GetString(9);
                objAppointment.DoctorUserId = dataReader.GetInt32(10);
                objAppointment.PatientUserId = dataReader.GetInt32(11);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(objAppointment));
        }

        [Route("appointments", Name = "GetAppointments")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAppointments()
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();
            List<AppointmentModel> lstAppointments = new List<AppointmentModel>();
            AppointmentModel objAppointment = null;

            sbSQL.AppendFormat("SELECT APPOINTMENT_ID, A.PATIENT_ID, A.DOCTOR_ID, DURATION, TITLE, APPOINTMENT_DATE, DOCTOR_NOTES, REMARKS, ");
            sbSQL.AppendFormat("UD.FIRST_NAME + ' ' + UD.LAST_NAME, UP.FIRST_NAME + ' ' + UP.LAST_NAME, UD.USER_ID, UP.USER_ID FROM APPOINTMENTS A ");
            sbSQL.AppendFormat("INNER JOIN PATIENT P ON P.PATIENT_ID = A.PATIENT_ID INNER JOIN DOCTOR D ON D.DOCTOR_ID = A.DOCTOR_ID ");
            sbSQL.AppendFormat("INNER JOIN USER_TABLE UD ON UD.USER_ID = D.USER_ID INNER JOIN USER_TABLE UP ON UP.USER_ID = P.USER_ID");
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                objAppointment = new AppointmentModel();
                objAppointment.AppointmentId = dataReader.GetInt32(0);
                objAppointment.PatientId = dataReader.GetInt32(1);
                objAppointment.DoctorId = dataReader.GetInt32(2);
                objAppointment.Duration = dataReader.GetInt32(3);
                objAppointment.Title = dataReader.GetString(4);
                objAppointment.DateAndTime = dataReader.GetString(5);
                objAppointment.DoctorNotes = dataReader.GetString(6);
                objAppointment.Remarks = dataReader.GetString(7);
                objAppointment.DoctorName = dataReader.GetString(8);
                objAppointment.PatientName = dataReader.GetString(9);
                objAppointment.DoctorUserId = dataReader.GetInt32(10);
                objAppointment.PatientUserId = dataReader.GetInt32(11);
                lstAppointments.Add(objAppointment);
            }

            dataReader.Close();
            cnn.Close();

            return await Task.Run(() => this.Ok(lstAppointments));
        }

        [Route("appointments", Name = "SaveAppointment")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveAppointment(AppointmentModel objAppointment)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString.GetConnectionString());
            cnn.Open();

            SqlCommand command;
            SqlDataReader dataReader;
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Clear();

            sbSQL.AppendFormat("SELECT PATIENT_ID FROM PATIENT WHERE USER_ID = {0}", objAppointment.PatientId);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                objAppointment.PatientId = dataReader.GetInt32(0);
            }
            dataReader.Close();

            sbSQL.Clear();
            sbSQL.AppendFormat("INSERT INTO APPOINTMENTS(PATIENT_ID, DOCTOR_ID, DURATION, TITLE, APPOINTMENT_DATE, DOCTOR_NOTES, REMARKS) ");
            sbSQL.AppendFormat("VALUES({0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}')", objAppointment.PatientId, objAppointment.DoctorId, objAppointment.Duration, objAppointment.Title, objAppointment.DateAndTime, objAppointment.DoctorNotes, objAppointment.Remarks);
            command = new SqlCommand(sbSQL.ToString(), cnn);
            command.ExecuteNonQuery();

            dataReader.Close();
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
