using LoanControllerAPI.DAL;
using LoanControllerAPI.Helpers;
using LoanControllerAPI.Models;
using LoanControllerAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoanControllerAPI.Managers
{
    public class SecurityManager
    {
        SecurityRepository _securityRepository = new SecurityRepository();
        public LoginData LoginUser(User loginUser)
        {
            User userAfterEncoding = new User { Email = loginUser.Email, Password = SecurityHelper.EncodePassword(loginUser.Password, SecurityHelper.SALT) };
            var user = _securityRepository.GetUser(userAfterEncoding);


            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecurityHelper.SALT);
            var expires = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = expires
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            LoginData result = new LoginData();
            result.Expires = expires;
            result.Token = tokenString;
            result.UserId = user.Id;
            result.UserName = user.Email;

            return result;
        }

        public User RegisterUser(User user)
        {
            User userAfterEncoding = new User { Email = user.Email, Password = SecurityHelper.EncodePassword(user.Password, SecurityHelper.SALT) };
            User result = _securityRepository.Insert(userAfterEncoding);
            return result;
        }

        public bool ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            User userToChangePassword = _securityRepository.GetUserById(changePasswordRequest.UserID);
            if (userToChangePassword != null)
            {
                if (userToChangePassword.Password == SecurityHelper.EncodePassword(changePasswordRequest.OldPassword, SecurityHelper.SALT))
                {
                    User updateUser = new User { Id = userToChangePassword.Id, Email = userToChangePassword.Email, Password = SecurityHelper.EncodePassword(changePasswordRequest.NewPassword, SecurityHelper.SALT) };
                    User updatedUser = _securityRepository.Update(updateUser);
                    if (updatedUser != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        public bool ResetPassword(string email)
        {
            User user = _securityRepository.GetUserByEmail(email);
            if (user != null)
            {
                var generatedPassword = Guid.NewGuid().ToString("d").Substring(1, 8);
                User userEdit = new User();
                userEdit.Id = user.Id;
                userEdit.Email = user.Email;
                userEdit.Password = SecurityHelper.EncodePassword(generatedPassword, SecurityHelper.SALT);
                User updatedUser = _securityRepository.Update(userEdit);
                if (updatedUser != null)
                {
                    EmailHelper.SendEmail("Reset Password", String.Format("Your new password: {0}", generatedPassword), userEdit.Email);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
