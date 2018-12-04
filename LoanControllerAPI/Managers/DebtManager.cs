using LoanControllerAPI.DAL;
using LoanControllerAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanControllerAPI.Managers
{
    public class DebtManager
    {
        DebtRepository _debtRepository = new DebtRepository();
        public Debt AddDebt(Debt debt)
        {
            debt.InsertDt = DateTime.Now;
            debt.UpdateDt = DateTime.Now;
            debt.UpdatedByUser = debt.UserId;
            Debt result = _debtRepository.Insert(debt);
            return result;
        }

        public IEnumerable<Debt> SelectDebtForUser(int userId, int limit, int offset)
        {
            IEnumerable<Debt> result = _debtRepository.SelectForUser(userId).OrderByDescending(d => d.InsertDt).Skip(offset).Take(limit);
            return result;
        }
    }
}
