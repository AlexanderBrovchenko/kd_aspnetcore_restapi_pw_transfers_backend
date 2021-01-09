using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kd_pw_transfers_backend.Models
{
    public partial class User
    {
        public User()
        {
            TransfersPayee = new HashSet<Transfer>();
            TransfersPayer = new HashSet<Transfer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [Required (ErrorMessage="Email should be filled, lad!")]
        [EmailAddress (ErrorMessage = "Tell us your real email address!")]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<Transfer> TransfersPayee { get; set; }
        public virtual ICollection<Transfer> TransfersPayer { get; set; }
    }
}
