using System;
using System.Collections.Generic;

namespace kd_pw_transfers_backend.Models
{
    public partial class Transfer
    {
        public int Id { get; set; }
#nullable enable
        public int? PayerId { get; set; }
        public int PayeeId { get; set; }
        public int Amount { get; set; }
        public DateTime OperatedAt { get; set; }
#nullable enable
        public virtual User? Payee { get; set; }
#nullable enable
        public virtual User? Payer { get; set; }
    }
}
