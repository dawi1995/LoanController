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
            Debt result = _debtRepository.Insert(debt);
            return result;
        }
    }
}
