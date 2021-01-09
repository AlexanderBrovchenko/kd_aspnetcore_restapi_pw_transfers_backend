using AutoMapper.QueryableExtensions;
using kd_pw_transfers_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace kd_pw_transfers_backend.Services
{
    public class ProtectedService : PublicService, IProtectedService
    {
        public ProtectedService(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Transfer> GetByUserId(int userId)
        {
            User user = _context.Users.Find(userId);
            user.TransfersPayer = _context.Transfers
                .Where(x => x.PayerId == userId)
                .OrderByDescending(x => x.OperatedAt)
                .ToList<Transfer>();
            user.TransfersPayee = _context.Transfers
                .Where(x => x.PayeeId == userId)
                .OrderByDescending(x => x.OperatedAt)
                .ToList<Transfer>();
            foreach (Transfer tr in user.TransfersPayer)
            {
                if (tr.PayerId > 0)
                {
                    tr.Payer = user;
                }
                tr.Payee = _context.Users.Find(tr.PayeeId);
            }
            return user.TransfersPayer;
        }


        public Transfer CreateTransfer(User payer, int payeeId, int amount, out string payeename)
        {
            // validation
            if (payer == null)
                throw new Exception("No payer is set");

            if (amount <= 0)
                throw new Exception("Only positive amount values allowed");

            if (amount > _context.UserBalance(payer))
                throw new Exception("You aren't that rich anymore!");

            User payee = _context.Users.Find(payeeId);
            if (payee == null)
                throw new Exception("No such sailor on our boat!");
            payeename = payee.Name;

            Transfer result = new Transfer
            {
                Amount = amount,
                OperatedAt = DateTime.UtcNow,
                PayeeId = payeeId,
                PayerId = payer.Id
            };
            _context.Transfers.Add(result);
            _context.SaveChanges();

            return result;
        }
    }
}
