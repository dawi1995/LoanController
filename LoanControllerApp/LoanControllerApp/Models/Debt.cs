using System;
using System.Collections.Generic;
using System.Text;

namespace LoanControllerApp.Models
{
    public class Debt
    {
        public int Id {get;set;}
        public int UserId { get; set; }
        public double DebtAmount { get; set; }
        public double InstallmentAmount { get; set; }
        public int PaymentDay { get; set; }
        public int ReminderDay { get; set; }
        public string ReminderTime { get; set; }
        public bool NotificationEnabled { get; set; }
        public DateTime InsertDt { get; set; }
        public DateTime UpdateDt { get; set; }
        public int UpdatedByUser { get; set; }
    }
}
