using LoanControllerAPI.DAL;
using LoanControllerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanControllerAPI.Repositories
{
    public class SecurityRepository
    {
        LoanControllerContext _context = new LoanControllerContext();
        public User GetUser(User userLogin)
        {
            return _context.User.FirstOrDefault(u => u.Email == userLogin.Email && u.Password == userLogin.Password);
        }

        public User Insert(User userLogin)
        {

            User user = _context.User.Add(userLogin).CurrentValues.ToObject() as User;
            _context.SaveChanges();
            return user;
        }
    }
}
