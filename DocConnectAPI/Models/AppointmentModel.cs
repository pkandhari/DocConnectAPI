﻿namespace DocConnectAPI.Models
{
    public class AppointmentModel
    {
        public int PatientId { get; set; }
        public int PatientUserId { get; set; }
        public int DoctorId { get; set; }
        public int DoctorUserId { get; set; }
        public int AppointmentId { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public string DoctorNotes { get; set; }
        public string DateAndTime { get; set; }
        public int Duration { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
    }
}
