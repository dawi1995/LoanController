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

            User user = _context.User.Add(userLogin).Entity;
            _context.SaveChanges();
            return user;
        }

        public User GetUserById(int userId)
        {
            return _context.User.FirstOrDefault(u => u.Id == userId);
        }

        public User Update(User updateUser)
        {
            User userToUpdate = _context.User.FirstOrDefault(u => u.Id == updateUser.Id);
            if (userToUpdate != null)
            {
                userToUpdate.Password = updateUser.Password;
                _context.SaveChanges();
            }
            return userToUpdate;
        }

        public User GetUserByEmail(string email)
        {
            User user = _context.User.FirstOrDefault(u => u.Email == email);
            return user;
        }
    }
}
