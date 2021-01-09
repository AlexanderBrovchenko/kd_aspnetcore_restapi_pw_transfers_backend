using System.ComponentModel.DataAnnotations;

namespace kd_pw_transfers_backend.Models
{
    public class RegisterModel
    {
        [Required (ErrorMessage = "No anonymous users allowed!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "We use human names here, from 3 to 50 chars please!")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Only latin letters and spaces allowed!")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Email should be filled, lad!")]
        [EmailAddress(ErrorMessage = "Tell us your real email address!")]
        public string Email { get; set; }

        [Required]
        [MinLength (8, ErrorMessage="Do u have that shorty in ur paints? Enlarge your password!")]
        public string Password { get; set; }
    }
}
