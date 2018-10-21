using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using LoanControllerAPI.DAL;
using LoanControllerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using LoanControllerAPI.Managers;

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
                return StatusCode(500);
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
                return StatusCode(500);
            }
        }

        // PUT api/values/5
        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok(true);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
