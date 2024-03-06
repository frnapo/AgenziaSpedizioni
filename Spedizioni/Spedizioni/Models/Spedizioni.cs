using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spedizioni.Models
{
    public class Spedizioni
    {
        public int IdSpedizione { get; set; }
        public int IdCliente { get; set; }
        [DisplayName("Crea codice tracciamento")]
        [Required(ErrorMessage = "Il codice è obbligatorio")]
        [StringLength(12, MinimumLength = 12,
        ErrorMessage = "Il codice deve essere di 12 caratteri")]
        public string codTracciamento { get; set; }
        [DisplayName("Data di spedizione")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ValidateCurrentDate(ErrorMessage = "La data dev'essere maggiore o uguale a quella odierna.")]
        public DateTime dataSpedizione { get; set; }
        [DisplayName("Inserisci il peso della spedizione")]
        [Required(ErrorMessage = "Il codice è obbligatorio")]
        public decimal pesoSpedizione { get; set; }
        [DisplayName("Inserisci la citta di destinazione")]
        [Required(ErrorMessage = "la destinazione e' obbligatoria")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "la citta deve essere compresa tra 3 e 50 caratteri")]
        public string cittaDestinazione { get; set; }
        [DisplayName("Inserisci l'indirizzo di destinazione")]
        [Required(ErrorMessage = "l'indirizzo di destinazione e' obbligatorio")]
        public string indirizzoDestinazione { get; set; }
        [DisplayName("Inserisci il nominativo del destinatario")]
        [Required(ErrorMessage = "Il nominativo del destinatario e' obbligatorio")]
        [StringLength(255, MinimumLength = 3,
            ErrorMessage = "Il nominativo del destinatario deve essere compreso tra 3 e 255 caratteri")]
        public string nominativoDestinatario { get; set; }
        [DisplayName("Inserisci il costo della spedizione")]
        [Required(ErrorMessage = "Il costo della spedizione e' obbligatorio")]
        public decimal costoSpedizione { get; set; }
        [DisplayName("Data di consegna")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ValidateCurrentDate(ErrorMessage = "La data dev'essere maggiore o uguale a quella odierna.")]
        public DateTime dataConsegna { get; set; }

        public int IdStatoSpedizione { get; set; }
    }
}