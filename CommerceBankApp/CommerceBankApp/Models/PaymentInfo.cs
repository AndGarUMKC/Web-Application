using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommerceBankApp.Models
{
    public class PaymentInfo
    {
        public int Id { get; set; }

        [DisplayName("Card Number")]
        [Required(ErrorMessage = "Please enter the credit card number")]
        [MinLength(16)]
        [MaxLength(16)]
        public int cardNumber { get; set; }

        [DisplayName("CVC Number")]
        [Required(ErrorMessage = "Please enter the CVC number")]
        [MinLength(3)]
        [MaxLength(3)]
        public int cvcNumber { get; set; }

        [DisplayName("Expiration Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter an expiration date")]
        public DateTime cardExpiration { get; set; }

        // FOREIGN KEYS
        public virtual ICollection<Payment> Payment { get; set; }

        public PaymentInfo ()
        {

        }
	}
}
