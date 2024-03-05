using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spedizioni.Models
{
    public class Utenti
    {
        public int IdUtente { get; set; }
        [DisplayName("Username")]
        [Required(ErrorMessage = "L'username e' obbligatorio")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "L'username dev'essere compreso tra 3 e 50 caratteri")]
        public string NomeUtente { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "La password e' obbligatoria")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "La password dev'essere compresa tra 3 e 50 caratteri")]
        public string Password { get; set; }
        public string Ruolo { get; set; }
    }
}