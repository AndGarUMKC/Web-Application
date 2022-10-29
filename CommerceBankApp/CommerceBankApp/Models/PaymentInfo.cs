using CommerceBankApp.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class PaymentInfo
    {
        public int PaymentInfoId { get; set; }

        [DisplayName("Payment Name")]
        [Required(ErrorMessage = "Please enter a credit card name")]
        public string PaymentInfoName { get; set; }

        [DisplayName("Card Number")]
        [Required(ErrorMessage = "Please enter the credit card number")]
        [CreditCard]
        public string CardNumber { get; set; }

        [DisplayName("CVC Number")]
        [Required(ErrorMessage = "Please enter the CVC number")]
        [MinLength(3), MaxLength(3)]
        public string CvcNumber { get; set; }

        [DisplayName("Expiration Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter an expiration date")]
        public DateTime CardExpiration { get; set; }

        [DisplayName("Billing Address")]
        [Required(ErrorMessage = "Please enter the billing address")]
        public string Address { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "Please enter the city")]
        public string City { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = "Please enter the state")]
        public string State { get; set; }

        [DisplayName("Zip Code")]
        [Required(ErrorMessage = "Please enter the zip code")]
        public int ZipCode { get; set; }

        // FOREIGN KEYS
        [ForeignKey("ApplicationUserId")]
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<Payment>? Payment { get; set; }

        public PaymentInfo ()
        {

        }
	}
}
