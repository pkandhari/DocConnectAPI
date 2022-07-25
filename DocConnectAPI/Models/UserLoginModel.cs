namespace DocConnectAPI.Models
{
    public class UserLoginModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDoctor { get; set; }
    }
}