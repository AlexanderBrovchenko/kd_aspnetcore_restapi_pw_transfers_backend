using System.ComponentModel.DataAnnotations;

namespace kd_pw_transfers_backend.Models
{
    public class AmountForPayeeModel
    {
        [Required (ErrorMessage = "Don't throw your wings to the sea! Name your buddy to be happier!")]
        public int PayeeId { get; set; }

        [Required (ErrorMessage = "Don't pack just an air, lad!")]
        public int Amount { get; set; }
    }
}
