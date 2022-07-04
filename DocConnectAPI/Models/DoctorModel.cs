namespace DocConnectAPI.Models
{
    public class DoctorModel
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string Degree { get; set; }
        public string GraduatedFrom { get; set; }
        public int YearsOfExp { get; set; }
        public string FieldOfPractice { get; set; }
        public bool IsFullTime { get; set; }
        public string CurrentWorkingStatus { get; set; }
        public string Department { get; set; }
        public string DOJ { get; set; }
        public string Availability { get; set; }
        public bool IsActive { get; set; }
        public UserModel UserDetails { get; set; }
    }
}
