namespace DocConnectAPI.Models
{
    public class LoginDetailsModel
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string DisplayName { get; set; }
    }
}
