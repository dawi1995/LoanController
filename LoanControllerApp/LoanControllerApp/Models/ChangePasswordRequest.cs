using System;
using System.Collections.Generic;
using System.Text;

namespace LoanControllerApp.Models
{
    public class ChangePasswordRequest
    {
        public int userId { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
