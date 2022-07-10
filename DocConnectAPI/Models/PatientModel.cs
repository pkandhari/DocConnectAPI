namespace DocConnectAPI.Models
{
    public class PatientModel
    {
        public int PatientId { get; set; }
        public string HealthIssues { get; set; }
        public string Allergies { get; set; }
        public UserModel UserDetails { get; set; }
    }
}
