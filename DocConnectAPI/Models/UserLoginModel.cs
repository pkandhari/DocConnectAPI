﻿namespace DocConnectAPI.Models
{
    public class UserLoginModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string DisplayName { get; set; }
    }
}