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
                if (result != null)
                    return Ok(result);
                else
                    return BadRequest("Login is not available");
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
        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword([FromBody]ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                bool result = _securityManager.ChangePassword(changePasswordRequest);
                if (result)
                {
                    return Ok(result);
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

        [HttpGet("ResetPassword")]
        public ActionResult ResetPassword(string email)
        {
            try
            {
                bool result = _securityManager.ResetPassword(email);
                if (result)
                {
                    return Ok(result);
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
    }
}
