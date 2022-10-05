using CommerceBankApp.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class PaymentInfo
    {
        public int PaymentInfoId { get; set; }

        [DisplayName("Card Number")]
        [Required(ErrorMessage = "Please enter the credit card number")]
        [CreditCard]
        public string cardNumber { get; set; }

        [DisplayName("CVC Number")]
        [Required(ErrorMessage = "Please enter the CVC number")]
        public int cvcNumber { get; set; }

        [DisplayName("Expiration Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter an expiration date")]
        public DateTime cardExpiration { get; set; }

        // FOREIGN KEYS
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<Payment>? Payment { get; set; }

        public PaymentInfo ()
        {

        }
	}
}
