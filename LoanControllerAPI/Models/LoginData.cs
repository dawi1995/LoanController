using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanControllerAPI.Models
{
    public class LoginData
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
