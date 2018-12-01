using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanControllerAPI.DAL;
using LoanControllerAPI.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanControllerAPI.Controllers
{
    [Route("api/debt")]
    [ApiController]
    public class DebtController : Controller
    {
        DebtManager _debtManager = new DebtManager();
        [Authorize("Bearer")]
        [HttpGet("AddDebt")]
        public ActionResult AddDebt(Debt debt)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.User.Identity.Name);
                debt.UserId = userId;
                Debt result = _debtManager.AddDebt(debt);
                if (result != null)
                    return Ok();
                else
                    return StatusCode(500, "Result is null");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
    }
}