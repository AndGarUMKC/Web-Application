using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CommerceBankApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class PaymentInfo2
    {
        public int PaymentInfo2Id { get; set; }

        [DisplayName("Payment Name")]
        [Required(ErrorMessage = "Please enter a bank account name")]
        public string PaymentInfo2Name { get; set; }

        [DisplayName("Account Number")]
        [Required(ErrorMessage = "Please enter the bank account number")]
        public string BankAccount { get; set; }

        [DisplayName("Routing Number")]
        [Required(ErrorMessage = "Please enter the routing number")]
        [MinLength(9), MaxLength(9)]
        public string Routing { get; set; }

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

        public PaymentInfo2()
        {

        }

    }
}
