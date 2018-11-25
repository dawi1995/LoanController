using System;
using System.Collections.Generic;

namespace LoanControllerAPI.DAL
{
    public partial class Debt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal DebtAmount { get; set; }
        public decimal? InstallmentAmount { get; set; }
        public int? PaymentDay { get; set; }
        public int? ReminderDay { get; set; }
        public TimeSpan? ReminderTime { get; set; }
        public bool NotificationEnabled { get; set; }
    }
}
