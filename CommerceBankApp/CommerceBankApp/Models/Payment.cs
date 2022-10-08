using CommerceBankApp.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter an amount to donate")]
        [DisplayName("Payment Amount")]
        public float DonatedAmount { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DonatedDate { get; set; }

        // FOREIGN KEYS
        [ForeignKey("UserName")]
        public string UserName { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey("OrganizationID")]
        public int OrganizationID { get; set; }
        public virtual Organization? Organization { get; set; }

        [ForeignKey("PaymentInfoId")]
        [DisplayName("Credit Card")]
        public int PaymentInfoId { get; set; }
        public virtual PaymentInfo? PaymentInfo { get; set; }

        public Payment()
        {
            DonatedDate = DateTime.Now;
        }
    }
}
