using kd_pw_transfers_backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace kd_pw_transfers_backend.Services
{
    public interface IPublicService
    {
        public User Authenticate(string username, string password);
        public IEnumerable<User> GetOthers(int userId);
        public User GetById(int id);
        public User GetByEmail(string email);
        public User CreateUser(User user, string password);
        public int UserBalance(User user);
    }
}
