namespace DocConnectAPI.Models
{
    public class SignupModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsPractitioner { get; set; }
        public string Specialty { get; set; }
        public string Credentials { get; set; }
    }
}
