using LoanControllerAPI.DAL;
using LoanControllerAPI.Managers;
using LoanControllerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LoanControllerAPI.Controllers
{
    [Route("api/security")]
    public class SecurityController : Controller
    {
        LoanControllerContext _context = new LoanControllerContext();
        SecurityManager _securityManager = new SecurityManager();

        // GET api/security
        [HttpGet]
        public IEnumerable<string> Get()
        { 
            return new string[] { "value1", "value2" };
        }

        // GET api/security/register
        [HttpPost("register")]
        public ActionResult Register([FromBody]User user)
        {
            try
            {
                User result = _securityManager.RegisterUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST api/values
        [HttpPost("login")]
        public ActionResult Login([FromBody]User loginUser)
        {
            try
            {
                LoginData result = _securityManager.LoginUser(loginUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT api/values/5
        [Authorize("Bearer")]
        [HttpGet("test")]
        public ActionResult ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                bool status = _securityManager.ChangePassword(changePasswordRequest);
                if (status)
                {
                    return Ok(status);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
