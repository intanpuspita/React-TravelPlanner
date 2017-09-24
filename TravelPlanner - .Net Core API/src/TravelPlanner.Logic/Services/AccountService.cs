using System;
using System.Security.Cryptography;
using System.Text;
using TravelPlanner.Logic.Interfaces;
using TravelPlanner.Objects.Context;
using TravelPlanner.Objects.Models;
using TravelPlanner.Objects.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace TravelPlanner.Logic.Services
{
    public class AccountService : IAccountService
    {
        ApplicationDbContext db;
        IEmailService _emailService;

        public AccountService(ApplicationDbContext appDbContext, IEmailService emailService)
        {
            db = appDbContext;
            _emailService = emailService;
        }

        #region Public Methods
        public UserModel GetUserById(string id)
        {
            var user = db.Users.Where(u => u.Id.ToString() == id).FirstOrDefault();
            return new UserModel {
                Email = user.Email,
                Name = user.Name,
                Id = user.Id.ToString()
            };
        }

        public bool ValidatePassword(string email, string password)
        {
            //TODO : Login Story
            return true;
        }

        public UserModel GetUserByEmail(string email)
        {
            UserModel result = new UserModel();

            var user = db.Users.Where(dt => dt.Email == email).FirstOrDefault();

            if (user != null)
            {
                result.Id = user.Id.ToString();
                result.Email = user.Email.ToString();
            }
            
            return result;
        }

        public ResponseModel RegisterEmail(UserModel user)
        {
            var result = new ResponseModel();

            try
            {
                result = ValidateUser(user);
                if(result.IsError)
                {
                    return result;
                }
                
                string salt = GetSalt();
                string hashedPassword = Convert.ToBase64String(GetHash(user.Password, salt));

                User newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = user.Name,
                    Email = user.Email,
                    Active = false,
                    Password = hashedPassword,
                    Salt = salt
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                _emailService.SendEmail(user.Email, user.Name);

                //SendEmailUsingSendGrid(user.Email, user.Name).Wait();

                return null;
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;

                return result;
            }
        }

        public Tuple<ResponseModel, UserModel> RegisterGoogleAccount(UserModel user)
        {
            ResponseModel result = new ResponseModel();

            try
            {
                User newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = user.Name,
                    Email = user.Email,
                    Active = true
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                return new Tuple<ResponseModel, UserModel>(result, new UserModel { Id = newUser.Id.ToString() });
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
                return new Tuple<ResponseModel, UserModel>(result, null);
            }
        }

        public void ConfirmAccount(Guid id)
        {
            var user = db.Users.Where(dt => dt.Id == id).FirstOrDefault();
            user.Active = true;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
        #endregion

        #region Private Methods
        
        static async Task SendEmailUsingSendGrid(string email, string name)
        {
            var apiKey = "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";//Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("apptest.intanpuspita@gmail.com", "Travel Planner App");
            var subject = "Confirm Your Account";
            var to = new EmailAddress(email, name);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        private ResponseModel ValidateUser(UserModel user)
        {
            var result = new ResponseModel();

            if (db.Users.Where(u => u.Email == user.Email).Count() == 1)
            {
                result.IsError = true;
                result.Message = "Email already exist";
            }

            return result;
        }

        private static string GetSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private static byte[] GetHash(string password, string salt)
        {
            byte[] unhashedBytes = Encoding.Unicode.GetBytes(String.Concat(salt, password));

            var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);

            return hashedBytes;
        }

        private static bool CompareHash(string attemptedPassword, byte[] hash, string salt)
        {
            string base64Hash = Convert.ToBase64String(hash);
            string base64AttemptedHash = Convert.ToBase64String(GetHash(attemptedPassword, salt));

            return base64Hash == base64AttemptedHash;
        }
        #endregion
    }
}
