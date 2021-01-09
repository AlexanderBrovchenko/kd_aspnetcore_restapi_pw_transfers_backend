using System.ComponentModel.DataAnnotations;

namespace kd_pw_transfers_backend.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
