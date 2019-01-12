using LoanControllerAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanControllerAPI.Repositories
{
    public class DebtRepository
    {
        LoanControllerContext _context = new LoanControllerContext();

        public Debt Insert(Debt debt)
        {
            _context.Debt.Add(debt);
            _context.SaveChanges();
            return debt;
        }

        public Debt Update(Debt debt)
        {
            Debt debtToUpdate = _context.Debt.FirstOrDefault(u => u.Id == debt.Id);
            if (debtToUpdate != null)
            {
                debtToUpdate.DebtAmount = debt.DebtAmount;
                debtToUpdate.InstallmentAmount = debt.InstallmentAmount;
                debtToUpdate.NotificationEnabled = debt.NotificationEnabled;
                debtToUpdate.PaymentDay = debt.PaymentDay;
                debtToUpdate.ReminderDay = debt.ReminderDay;
                debtToUpdate.ReminderTime = debt.ReminderTime;
                _context.SaveChanges();
            }
            return debtToUpdate;
        }

        public Debt SelectById(int debtId)
        {
            Debt result = _context.Debt.FirstOrDefault(d => d.Id == debtId);
            return result;
        }

        public IEnumerable<Debt> SelectForUser(int userId)
        {
            IEnumerable<Debt> result = _context.Debt.Where(d => d.UserId == userId);
            return result;
        }

        public bool Delete(int debtId)
        {
            Debt debtToDelete = _context.Debt.FirstOrDefault(d => d.Id == debtId);
            if (debtToDelete != null)
            {
                _context.Debt.Remove(debtToDelete);
                return true;
            }
            else
                return false;
        }
    }
}
