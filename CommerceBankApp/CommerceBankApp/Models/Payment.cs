using CommerceBankApp.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class Payment
    {
        public int paymentID { get; set; }

        [Required(ErrorMessage = "Please enter an amount to donate")]
        [DisplayName("Payment Amount")]
        public float donatedAmount { get; set; }

        // FOREIGN KEYS
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Organization Organization { get; set; }
        
        public virtual PaymentInfo PaymentInfo { get; set; }

        public Payment()
        {

        }
    }
}
