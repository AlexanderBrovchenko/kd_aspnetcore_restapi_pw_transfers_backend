using System;
using System.Collections.Generic;
using System.Linq;
using kd_pw_transfers_backend.Models;
using kd_pw_transfers_backend.Services.Authentication;

namespace kd_pw_transfers_backend.Services
{
    public class PublicService : IPublicService
    {
        protected ApplicationContext _context;

        public PublicService(ApplicationContext context)
        {
            _context = context;
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            User user = GetByEmail(email);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash))
                return null;

            return user;
        }

        public IEnumerable<User> GetOthers(int userId)
        {
            return _context.Users.Where(x => x.Id != userId);
        }

        public User GetById(int id)
        {
            User user = _context.Users.Find(id);
            if (user == null)
                return null;

            return user;
        }

        public User GetByEmail(string email)
        {
            User user = _context.Users.Where(x => x.Email == email).First<User>();
            if (user == null)
                return null;

            return user;
        }
        public User CreateUser(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new Exception("Email \"" + user.Email + "\" is already taken");

            if (_context.Users.Any(x => x.Name == user.Name))
                throw new Exception("Username \"" + user.Name + "\" is already taken");

            string passwordHash;
            CreatePasswordHash(password, out passwordHash);

            user.PasswordHash = passwordHash;

            _context.Users.Add(user);
            _context.SaveChanges();

            if(user.Id > 0)
            {
                Transfer welcomeTransfer = new Transfer
                {
                    Amount = AuthOptions.WELCOME_AMOUNT,
                    OperatedAt = DateTime.UtcNow,
                    PayerId = null,
                    PayeeId = user.Id
                };
                _context.Transfers.Add(welcomeTransfer);
                _context.SaveChanges();
            }

            return user;
        }

        public int UserBalance(User user)
        {
            return _context.UserBalance(user);
        }
        private static void CreatePasswordHash(string password, out string passwordHash)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool VerifyPasswordHash(string password, string storedHash)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
