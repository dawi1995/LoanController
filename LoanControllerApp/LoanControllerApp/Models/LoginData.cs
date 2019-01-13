using System;
using System.Collections.Generic;
using System.Text;

namespace LoanControllerApp.Models
{
    public class LoginData
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
