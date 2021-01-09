using System;
using System.Collections.Generic;
using kd_pw_transfers_backend.Models;

namespace kd_pw_transfers_backend.Services
{
    public interface IProtectedService : IPublicService
    {
        public IEnumerable<Transfer> GetByUserId(int userId);
        public Transfer CreateTransfer(User payer, int payeeId, int amount, out string payeename);
    }
}
