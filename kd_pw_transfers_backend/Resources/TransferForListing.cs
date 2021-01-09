using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kd_pw_transfers_backend.Resources
{
    public class TransferForListing
    {
        public int Id { get; set; }
        public string PayeeName { get; set; }
        public DateTime OperatedAt { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
    }
}
