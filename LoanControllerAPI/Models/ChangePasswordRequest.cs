using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanControllerAPI.Models
{
    public class ChangePasswordRequest
    {
        public int UserID { get; set; }
        public string OldPassword {get;set;}
        public string NewPassword { get; set; }
    }
}
